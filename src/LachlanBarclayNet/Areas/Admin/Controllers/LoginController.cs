using LachlanBarclayNet.Areas.Admin.ViewModels;
using LachlanBarclayNet.DAO;
using System;
using System.Collections.Generic;
using System.Data;

using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LachlanBarclayNet.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Admin/Post/
        public ActionResult Index()
        {
            return View();
        }

   

        [HttpPost]
        public ActionResult Index(LoginIndexViewModel ViewModel)
        {
            UserDAO userDAO = new UserDAO();

            bool verified = userDAO.Verify(ViewModel.username, ViewModel.password, ViewModel.qrcode);

            if (verified)
                FormsAuthentication.RedirectFromLoginPage(ViewModel.username, true);
           
            return View(ViewModel);
        }

        public ActionResult Logout()
        {
            if (User.Identity.IsAuthenticated)
                FormsAuthentication.SignOut();

            return View();
        }
    }
}