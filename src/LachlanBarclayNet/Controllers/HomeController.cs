using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using LachlanBarclayNet.ViewModel;
using LachlanBarclayNet.DAO;

namespace LachlanBarclayNet.Controllers
{
    public class HomeController : Controller
    {
        private static readonly int _searchLimit = 10;

        public static int SearchLimit { get { return _searchLimit; } }

        private readonly IPostDAO _postDAO;

        public HomeController()
        {
            _postDAO = new PostDAO();
        }

        public HomeController(IPostDAO postDAO)
        {
            _postDAO = postDAO;
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
                return HttpNotFound();

            if (!postToView.Published && !User.Identity.IsAuthenticated)
                return HttpNotFound();

            return View(postToView);
        }

        private IndexBlogViewModel GetIndexViewModel(string category, string from)
        {
            DateTime searchFromDate = GetSearchDate(from);
            var posts = _postDAO.PostSearch(searchFromDate, category, _searchLimit);

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
            if (ViewModel.Posts.Count == _searchLimit)
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
