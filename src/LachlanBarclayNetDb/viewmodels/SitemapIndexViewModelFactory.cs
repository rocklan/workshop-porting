using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace LachlanBarclayNet.ViewModel
{
    public class SitemapIndexViewModelFactory
    {
        public static SitemapIndexViewModel Create(IEnumerable<LachlanBarclayNet.DAO.Standard.Post> Posts)
        {
            SitemapIndexViewModel ViewModel = new SitemapIndexViewModel
            {
                Urls = Posts.Select(x => new SitemapUrl { Url = x.FullUrl }).ToList()
            };

            AddUrl(ViewModel, "contact");

            return ViewModel;
        }

        private static void AddUrl(SitemapIndexViewModel ViewModel, string uri)
        {
            ViewModel.Urls.Add(new SitemapUrl { Url = ConfigurationManager.AppSettings["domain"] + "/" + uri });
        }
    }
}