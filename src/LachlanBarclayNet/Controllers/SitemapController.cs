using LachlanBarclayNet.DAO;
using LachlanBarclayNet.ViewModel;

using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LachlanBarclayNet.Controllers
{
    public class SitemapController : Controller
    {
        [System.Web.Mvc.Route("sitemap.xml")]
        [OutputCache(Duration = 3600, VaryByParam = "none")]
        public ActionResult Index()
        {
            PostDAO postDAO = new PostDAO();

            List<LachlanBarclayNet.DAO.Standard.Post> posts = postDAO.GetAllLivePosts();

            SitemapIndexViewModel ViewModel = SitemapIndexViewModelFactory.Create(posts);
                
            Response.ContentType = "application/xml";

            return View(ViewModel);
        }
    }
}
