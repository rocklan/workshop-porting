using System.Linq;
using System.Web.Mvc;
using System.Globalization;
using LachlanBarclayNet.ViewModel;
using LachlanBarclayNet.DAO;
using System.Configuration;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Web;

namespace LachlanBarclayNet.Controllers
{
    public class ContactController : Controller
    {
        [Route("contact")]
        public ActionResult Index()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Index(IndexContactViewModel ViewModel)
        {
            if (ViewModel == null)
                return RedirectToAction("Index");

            RecaptchaApi recaptchaApi = new RecaptchaApi();
            RecaptureResult recaptureResult = await recaptchaApi.RecaptchaIsOkAsync(ViewModel.RecaptchaToken, GetRemoteIp());

            if (!recaptureResult.Success)
                ModelState.AddModelError("RecaptchaToken", $"Invalid recapture: {recaptureResult.Errors}");

            if (ModelState.IsValid)
            {
                await SendEmailAsync(ViewModel, recaptureResult.BotScore);

                ViewModel.EmailSent = true;
            }

            return View(ViewModel);
        }


        private async Task SendEmailAsync(IndexContactViewModel ViewModel, decimal BotScore)
        {
            var from = new EmailAddress("do-not-reply@lachlanbarclay.net");
            var to = new EmailAddress("clockwise.music@gmail.com");
            string subject = "lachlanbarclay.net contact form submission";
            var plainTextContent = ViewModel.Message;

            var htmlEncoded = HttpUtility.HtmlEncode(ViewModel.Message);
            var nameEncoded = HttpUtility.HtmlEncode(ViewModel.Name);

            var htmlContent = $"Bot Score: {BotScore}<br /> " +
                $"From: {nameEncoded}<br />" +
                $"Email: {ViewModel.Email}<br />" +
                $"Body: {htmlEncoded}";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            var apiKey = ConfigurationManager.AppSettings["sendgridapikey"];
            var client = new SendGridClient(apiKey);

            await client.SendEmailAsync(msg);
        }

        private string GetRemoteIp()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                    return addresses[0];
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }
    }

}
