using System;
using System.Collections.Generic;
using System.Web;

namespace LachlanBarclayNet.ViewModel
{
    public class SitemapIndexViewModel
    {
        public SitemapIndexViewModel()
        {
            this.Urls = new List<SitemapUrl>();
        }

        public List<SitemapUrl> Urls { get; set; }
    }

    public class SitemapUrl
    {
        public string Url { get; set; }
    }
}