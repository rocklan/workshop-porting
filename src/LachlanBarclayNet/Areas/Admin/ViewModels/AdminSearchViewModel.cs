using LachlanBarclayNet.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LachlanBarclayNet.Areas.Admin.ViewModels
{
    public class AdminSearchViewModel
    {
        public List<LachlanBarclayNet.DAO.Standard.Post> Posts { get; set; }
        public string SearchString { get; set; }
    }
}