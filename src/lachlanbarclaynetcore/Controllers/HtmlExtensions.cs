using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using LachlanBarclayNet.DAO;
using LachlanBarclayNet.DAO.Standard;

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Caching.Memory;

using Newtonsoft.Json.Linq;

namespace lachlanbarclaynetcore.Extensions
{
    public static class HtmlExtensions
    {
        public static string AppBaseUrl = "lachlanbarclaynetcore";

        public static HtmlString ScriptBundles(this IHtmlHelper helper, string bundleName)
        {
            var includes = new StringBuilder();
            var finalPaths = GetFinalPaths(bundleName);
            
            foreach (var path in finalPaths)
                includes.AppendLine($"<script src='{path}'></script>");

            return new HtmlString(includes.ToString());
        }

        public static HtmlString StyleBundles(this IHtmlHelper helper, string bundleName)
        {
            var includes = new StringBuilder();
            var finalPaths = GetFinalPaths(bundleName);

            foreach (var path in finalPaths)
                includes.AppendLine($"<link rel='stylesheet' href='{path}' />");

            return new HtmlString(includes.ToString());
        }


        private static IEnumerable<string> GetFinalPaths(string bundleName)
        {
            string bundleFile = Path.Combine(Environment.CurrentDirectory, "bundleconfig.json");
            string json = File.ReadAllText(bundleFile);
            dynamic bundles = JArray.Parse(json);

            List<string> paths = new List<string>();

            for (int i = 0; i < bundles.Count; i++)
                if (bundles[i].outputFileName == bundleName.Replace("~", "wwwroot"))
                    for (int j = 0; j < bundles[i].inputFiles.Count; j++)
                        paths.Add(bundles[i].inputFiles[j].ToString().Replace("wwwroot", "/" + AppBaseUrl));

            return paths;
        }


        public static HtmlString TechArchive(this IHtmlHelper htmlHelper, int PostTypeID)
        {
            return GetHtmlForPostsCached(htmlHelper, PostTypeID);
        }

        private static HtmlString GetHtmlForPostsCached(IHtmlHelper htmlHelper, int PostTypeID)
        {
            string key = $"SideBar.{PostTypeID}";

            var myCache = new MemoryCache(new MemoryCacheOptions());

            return myCache.GetOrCreate(key, entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromSeconds(3);

                PostDAO postDao = new PostDAO();

                var sidebarPosts = postDao.GetPostsForSideBar(PostTypeID);

                string rawHtml = GetHtmlForPosts(htmlHelper, sidebarPosts);

                return new HtmlString(rawHtml);
            });
        }

        private static string GetHtmlForPosts(IHtmlHelper htmlHelper, List<Post> sidebarPosts)
        {
            StringBuilder sb = new StringBuilder();

            var years = sidebarPosts.Select(x => x.PostDate.Year).Distinct();
            foreach (var year in years)
            {
                sb.Append(year);
                sb.Append("<br />");
                sb.Append("<ul>");
                foreach (var post in sidebarPosts.Where(x => x.PostDate.Year == year))
                {
                    string resolvedUrl = "/" + AppBaseUrl + "/" + post.FriendlyUrl;

                    sb.Append("<li>");
                    sb.Append($"<a href='{resolvedUrl}'>{post.PostTitle}</a>");
                    sb.Append("</li>");
                }
                sb.Append("</ul>");
            }
            return sb.ToString();
        }


        public static string ToNiceDate(this DateTime thedate)
        {
            string suffix = GetDaySuffix(thedate.Day);
            string finalString = thedate.Day.ToString() + suffix + " of " + thedate.ToString("MMM, yyyy");
            return finalString;
        }

        public static string ToNiceDateTime(this DateTime thedate)
        {
            //TODO: add AM/PM tostring thing here
            string nicedate = thedate.ToNiceDate() + " " + thedate.ToString("h:mm");
            return nicedate;
        }

        private static string GetDaySuffix(int day)
        {
            switch (day)
            {
                case 1:
                case 21:
                case 31:
                    return "st";
                case 2:
                case 22:
                    return "nd";
                case 3:
                case 23:
                    return "rd";
                default:
                    return "th";
            }
        }

    }
}
