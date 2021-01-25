using LachlanBarclayNet.DAO;
using LachlanBarclayNet.ViewModel;

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace LachlanBarclayNet.Controllers
{
    public class SitemapController : Controller
    {
        [Route("sitemap.xml")]
        [ResponseCache(Duration = 3600, VaryByQueryKeys = new[] { "none" })]
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
