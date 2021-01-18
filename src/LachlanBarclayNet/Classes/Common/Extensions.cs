using LachlanBarclayNet.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CustomExtensions
{
    //Extension methods must be defined in a static class
    public static class Extensions
    {
        public static HtmlString TechArchive(this HtmlHelper htmlHelper, int PostTypeID)
        {
            return GetHtmlForPostsCached(htmlHelper, PostTypeID);
        }

        private static HtmlString GetHtmlForPostsCached(HtmlHelper htmlHelper, int PostTypeID)
        {
            string key = $"SideBar.{PostTypeID}";
            HtmlString htmlString = (HtmlString)MemoryCache.Default.Get(key);

            if (htmlString == null)
            {
                PostDAO postDao = new PostDAO();

                List<Post> sidebarPosts = postDao.GetPostsForSideBar(PostTypeID);

                string rawHtml = GetHtmlForPosts(htmlHelper, sidebarPosts);

                htmlString = new HtmlString(rawHtml);

                //MemoryCache.Default.Add(key, htmlString,
                  //  new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.UtcNow.AddHours(24) });
            }

            return htmlString;
        }

        private static string GetHtmlForPosts(HtmlHelper htmlHelper, List<Post> sidebarPosts)
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
                    string resolvedUrl =
                        VirtualPathUtility.ToAbsolute("~/" + post.FriendlyUrl);

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
            string nicedate = thedate.ToNiceDate() + " "+ thedate.ToString("h:mm");
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