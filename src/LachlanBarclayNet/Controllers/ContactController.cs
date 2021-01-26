using System.Linq;
using System.Web.Mvc;
using System.Globalization;
using LachlanBarclayNet.ViewModel;
using LachlanBarclayNet.DAO;
using System.Configuration;
using System.Threading.Tasks;

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

            RecaptureResult recaptureResult = await recaptchaApi.RecaptchaIsOkAsync(
                ViewModel.RecaptchaToken,
                GetRemoteIp()
            );

            if (!recaptureResult.Success)
                ModelState.AddModelError("RecaptchaToken", $"Invalid recapture: {recaptureResult.Errors}");

            if (ModelState.IsValid)
            {
                await recaptchaApi.SendEmailAsync(ViewModel, recaptureResult.BotScore);

                ViewModel.EmailSent = true;
            }

            return View(ViewModel);
        }

        private string GetRemoteIp()
        {
            string ipAddress = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                    return addresses[0];
            }

            return System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }
    }

}
