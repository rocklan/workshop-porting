using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LachlanBarclayNet.Areas.Admin.ViewModels
{
    public class LoginIndexViewModel
    {
        public string username { get; set; }
        public string password { get; set; }
        public string qrcode { get; set; }

        public int? FailCount { get; set; }
        public DateTime? LockedOut { get; set; }
    }
}