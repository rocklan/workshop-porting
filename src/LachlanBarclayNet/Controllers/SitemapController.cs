using LachlanBarclayNet.DAO;
using LachlanBarclayNet.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace LachlanBarclayNet.Controllers
{
    public class SitemapController : Controller
    {
        [System.Web.Mvc.Route("sitemap.xml")]
        [OutputCache(Duration = 3600, VaryByParam = "none")]
        public ActionResult Index()
        {
            using (LbNet context = new LbNet())
            {
                PostDAO postDAO = new PostDAO();
                var posts = postDAO.GetAllLivePosts();

                SitemapIndexViewModel ViewModel = new SitemapIndexViewModel
                {
                    Urls = posts.Select(x => new SitemapUrl { Url = x.FullUrl }).ToList()
                };

                AddUrl(ViewModel, "contact");
                
                Response.ContentType = "application/xml";

                return View(ViewModel);
            }
        }

        private void AddUrl(SitemapIndexViewModel ViewModel, string uri)
        {
            ViewModel.Urls.Add(new SitemapUrl { Url = ConfigurationManager.AppSettings["domain"] + "/" + uri });
        }

    }

    
}
