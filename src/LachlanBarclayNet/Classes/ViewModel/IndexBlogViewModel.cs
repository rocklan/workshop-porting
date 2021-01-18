using LachlanBarclayNet.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LachlanBarclayNet.ViewModel
{
    public class IndexBlogViewModel
    {
        public List<Post> Posts { get; set; }
        public List<YearPost> Years { get; set; }
        public string Category { get; set; }
        public string NextButtonSearchParams { get; set; }
    }
}