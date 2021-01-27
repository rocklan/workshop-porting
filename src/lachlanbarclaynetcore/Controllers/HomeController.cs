using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

using LachlanBarclayNet.DAO;
using LachlanBarclayNet.ViewModel;

using lachlanbarclaynetcore.Models;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace lachlanbarclaynetcore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostDAO _postDAO;
        private readonly AppSettings _appSettings;

        public HomeController(ILogger<HomeController> logger, IPostDAO postDAO, AppSettings appSettings)
        {
            _logger = logger;
            _postDAO = postDAO;
            _appSettings = appSettings;
        }

     

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            var exception = exceptionHandlerPathFeature?.Error;

            if (exception != null)
            {
                // Log this exception to disk or database or just post a notification somewhere
                Debug.WriteLine(exception.ToString());
            }

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [Route("")]
        [HttpGet]
        public ActionResult Index()
        {
            var ViewModel = GetIndexViewModel(null, null);
            return View(ViewModel);
        }

        [Route("search")]
        [HttpGet]
        public ActionResult Search(string category, string from)
        {
            var ViewModel = GetIndexViewModel(category, from);
            return View("Index", ViewModel);
        }

        [Route("{year:int}/{month:int}/{title}")]
        public ActionResult ViewPost(int year, int month, string title)
        {
            var postToView = _postDAO.Get(year, month, title);

            if (postToView == null)
                return NotFound();

            if (!User.Identity.IsAuthenticated)
            {
                if (postToView.Published == null)
                    return NotFound();

                if (postToView.Published.HasValue && !postToView.Published.Value)
                    return NotFound();
            }

            if (postToView.Published == null && !User.Identity.IsAuthenticated)
                return NotFound();

            return View(postToView);
        }

        private IndexBlogViewModel GetIndexViewModel(string category, string from)
        {
            DateTime searchFromDate = GetSearchDate(from);
            var posts = _postDAO.PostSearch(searchFromDate, category, _appSettings.NumberOfPostsOnHomeScreen);

            IndexBlogViewModel ViewModel = new IndexBlogViewModel()
            {
                Posts = posts,
                Category = category
            };

            PopulateNextButtonParameters(ViewModel);

            return ViewModel;
        }

        private void PopulateNextButtonParameters(IndexBlogViewModel ViewModel)
        {
            if (ViewModel.Posts.Count == _appSettings.NumberOfPostsOnHomeScreen)
            {
                ViewModel.NextButtonSearchParams = "from=" + ViewModel.Posts.Last().PostDate.ToString("yyyyMMdd");

                if (ViewModel.Category != null)
                {
                    string cat = $"category={ViewModel.Category}";

                    if (ViewModel.NextButtonSearchParams == null)
                        ViewModel.NextButtonSearchParams = cat;
                    else
                        ViewModel.NextButtonSearchParams += "&" + cat;

                }
            }
        }

        private DateTime GetSearchDate(string from)
        {
            if (from == null)
                return DateTime.Now;

            if (DateTime.TryParseExact(from, "yyyyMMdd", CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal, out DateTime parsedDate))
                return parsedDate;

            return DateTime.Now;
        }

    }
}
