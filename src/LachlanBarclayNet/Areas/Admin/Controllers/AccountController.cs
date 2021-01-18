using Base32;
using LachlanBarclayNet.Areas.Admin.ViewModels;
using LachlanBarclayNet.DAO;
using OtpSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace LachlanBarclayNet.Areas.Admin.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {


        public ActionResult QrCode()
        {
            byte[] secretKey = KeyGeneration.GenerateRandomKey(20);
            string userName = "lachlan";
            string issuerEncoded = HttpUtility.UrlEncode("lachlanbarclay.net");
            string barcodeUrl = KeyUrl.GetTotpUrl(secretKey, userName) + "&issuer=" + issuerEncoded;

            var model = new AdminQrCode
            {
                SecretKey = Base32Encoder.Encode(secretKey),
                BarcodeUrl = barcodeUrl
            };

            return View(model);
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult QrCode(AdminQrCode viewModel)
        {
            long timeStepMatched = 0;

            byte[] decodedKey = Base32Encoder.Decode(viewModel.SecretKey);
            var otp = new Totp(decodedKey);

            viewModel.Result = otp.VerifyTotp(viewModel.Code, out timeStepMatched, new VerificationWindow(2, 2));

            if (viewModel.Result.Value)
            {
                var userDAO = new UserDAO();
                userDAO.SetQrCode(User.Identity.Name,  viewModel.SecretKey);
            }

            return View(viewModel);
        }

        public ActionResult SetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SetPassword(AdminSetPassword viewModel)
        {
            Hasher h = new Hasher();
            string hashed = h.HashPassword(viewModel.Password);

            UserDAO userDAO = new UserDAO();
            userDAO.SetPassword("lachlan", hashed);

            return RedirectToAction("SetPassword");
        }

    }
}