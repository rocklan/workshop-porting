using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

using LachlanBarclayNet.ViewModel;

using lachlanbarclaynetcore;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace LachlanBarclayNet.Controllers
{
    public class AdminController: Controller
    {
        private readonly AppSettings _appSettings;

        public AdminController(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        [Route("admin/login")]
        public ActionResult Login()
        {
            return View();
        }

        [Route("admin/login")]
        [HttpPost]
        public async Task<ActionResult> Login([FromForm]AdminLoginViewModel viewModel)
        {
            var identity = new ClaimsIdentity(_appSettings.AuthScheme, ClaimTypes.Name, ClaimTypes.Role);

            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, viewModel.Username));
            identity.AddClaim(new Claim(ClaimTypes.Name, viewModel.Username));
            identity.AddClaim(new Claim(ClaimTypes.Role, "User"));

            var principal = new ClaimsPrincipal(identity);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.Now.AddDays(1),
                IsPersistent = true,
            };

            await HttpContext.SignInAsync(_appSettings.AuthScheme, new ClaimsPrincipal(principal), authProperties);

            return Redirect("done");
        }

        [Route("admin/done")]
        public ActionResult done()
        {
            return View();
        }

        [Route("admin/logout")]
        public async Task<ActionResult> logout()
        {
            await HttpContext.SignOutAsync(_appSettings.AuthScheme);
            return Redirect("done");
        }
    }

}
