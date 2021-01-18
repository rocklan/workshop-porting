using LachlanBarclayNet.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LachlanBarclayNet.Areas.Admin.ViewModels
{
    public class AdminIndexViewModel
    {
        public List<Post> Posts { get; set; }
        public string FromString { get; set; }
    }

    public class AdminQrCode
    {
        public string BarcodeUrl { get; set; }
        public string SecretKey { get; set; }
        public string Code { get; set; }

        public bool? Result { get; set; }
    }

    public class AdminSetPassword
    {
        public string Password { get; set; }
    }

    public class AdminImgViewModel
    {
        public List<AdminImgPost> Posts { get; set; }
        public string FromString { get; set; }
    }

    public class AdminImgPost
    {
        public int PostID { get; set; }
        public string PostTitle { get; set; }

        public List<AdminImgPostImg> images { get; set; }
    }

    public class AdminImgPostImg
    {
        public string Img { get; set; }
        public string ImgFixed { get; set; }
    }
}