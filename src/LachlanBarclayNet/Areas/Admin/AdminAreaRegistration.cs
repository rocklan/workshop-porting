using System.Web.Mvc;

namespace LachlanBarclayNet.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            string[] namespaces = { "LachlanBarclayNet.Areas.Admin.Controllers" };

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional, controller="Post" }, 
                namespaces: namespaces
            );
        }
    }
}
