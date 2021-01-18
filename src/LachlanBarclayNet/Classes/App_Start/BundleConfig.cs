using System.Web;
using System.Web.Optimization;

namespace LachlanBarclayNet
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            if (bundles == null)
                return;

            bundles.Add(new ScriptBundle("~/bundles/jscript").Include(
                        "~/Scripts/jquery-3.4.1.js", 
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/highlight.pack.js",
                        "~/Scripts/site.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
               "~/Content/bootstrap.css",
               "~/Content/highlightjs-default.css",
               "~/Content/solarized_dark.css",
                "~/Content/site.css"
                ));

            
        }
    }
}