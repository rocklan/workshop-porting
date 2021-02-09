using System;
using System.Configuration;
using System.IO;

using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Interop;

using Owin;

[assembly: OwinStartup(typeof(LachlanBarclayNet.Startup))]
namespace LachlanBarclayNet
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var DataProtectionDir = (String)ConfigurationManager.AppSettings["DataProtectionDir"];
            var AuthScheme = (String)ConfigurationManager.AppSettings["AuthScheme"];
            var CookieName = (String)ConfigurationManager.AppSettings["AuthCookieName"];

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = AuthScheme,
                CookieName = CookieName,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider(),
                TicketDataFormat = new AspNetTicketDataFormat(
                    new DataProtectorShim(
                        DataProtectionProvider.Create(new DirectoryInfo(DataProtectionDir),
                            (builder) => { builder.SetApplicationName("SharedCookieApp"); })
                        .CreateProtector(
                            "Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationMiddleware",
                            AuthScheme,
                            "v2"))),
                CookieManager = new ChunkingCookieManager()
            });
        }
    }
    
}