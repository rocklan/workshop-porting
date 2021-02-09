using System.Linq;

using System.Globalization;
using LachlanBarclayNet.ViewModel;
using LachlanBarclayNet.DAO;
using System.Configuration;
using System.Threading.Tasks;

using System.Web;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using lachlanbarclaynetcore;

namespace LachlanBarclayNet.Controllers
{
    public class ContactController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly AppSettings _appSettings;

        public ContactController(IHttpClientFactory httpClientFactory, AppSettings appSettings)
        {
            _httpClientFactory = httpClientFactory;
            _appSettings = appSettings;
        }


private async Task<SessionData> GetSessionData(string key)
{
    var sessionCookie = Request.Cookies[_appSettings.SessionCookieName];

    var httpClient = _httpClientFactory.CreateClient("lachlanbarclaynet");
    httpClient.DefaultRequestHeaders.Add("cookie", _appSettings.SessionCookieName + "=" + sessionCookie);

    var response = await httpClient.GetAsync($"session/get?key={key}&cookie={sessionCookie}");
    response.EnsureSuccessStatusCode();

    string sessiondatajson = await response.Content.ReadAsStringAsync();
    return Newtonsoft.Json.JsonConvert.DeserializeObject<SessionData>(sessiondatajson);
}

        [Route("contact")]
        public async Task<ActionResult> Index()
        {
            ViewBag.lastviewed = await GetSessionData("last-viewed");

            return View();
        }

        [Route("contact")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Index(IndexContactViewModel ViewModel)
        {
            if (ViewModel == null)
                return RedirectToAction("Index");

            if (ModelState.IsValid)
            {
                ContactDAO contactDAO = new ContactDAO();
                contactDAO.InsertContact(ViewModel.Name, ViewModel.Email, ViewModel.Message);

                ViewModel.EmailSent = true;
            }

            return View(ViewModel);
        }

    }

}
