﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ASP
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Optimization;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    using CustomExtensions;
    using LachlanBarclayNet.DAO;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Home/ViewPost.cshtml")]
    public partial class _Views_Home_ViewPost_cshtml : System.Web.Mvc.WebViewPage<Post>
    {
        public _Views_Home_ViewPost_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 3 "..\..\Views\Home\ViewPost.cshtml"
  
    ViewBag.Title = Model.PostTitle;
    ViewBag.Description = Model.PostDescription;

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n\r\n<div");

WriteLiteral(" class=\"blog-post\"");

WriteLiteral(">\r\n    <h2");

WriteLiteral(" class=\"blog-post-title\"");

WriteLiteral(">");

            
            #line 10 "..\..\Views\Home\ViewPost.cshtml"
                           Write(Model.PostTitle);

            
            #line default
            #line hidden
WriteLiteral("</h2>\r\n    <p");

WriteLiteral(" class=\"blog-post-meta\"");

WriteLiteral(">");

            
            #line 11 "..\..\Views\Home\ViewPost.cshtml"
                         Write(Model.PostDate.ToNiceDate());

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n");

WriteLiteral("    ");

            
            #line 12 "..\..\Views\Home\ViewPost.cshtml"
Write(Html.Raw(Model.PostText));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n");

            
            #line 14 "..\..\Views\Home\ViewPost.cshtml"
    
            
            #line default
            #line hidden
            
            #line 14 "..\..\Views\Home\ViewPost.cshtml"
     if (Model.PostComments.Count > 0)
    {

            
            #line default
            #line hidden
WriteLiteral("        <hr />\r\n");

            
            #line 17 "..\..\Views\Home\ViewPost.cshtml"
        foreach (var comment in Model.PostComments)
        {

            
            #line default
            #line hidden
WriteLiteral("            <p>\r\n                <b>");

            
            #line 20 "..\..\Views\Home\ViewPost.cshtml"
              Write(comment.Username);

            
            #line default
            #line hidden
WriteLiteral("</b>: ");

            
            #line 20 "..\..\Views\Home\ViewPost.cshtml"
                                     Write(Html.Raw(comment.Comment));

            
            #line default
            #line hidden
WriteLiteral("<br />\r\n                <i>");

            
            #line 21 "..\..\Views\Home\ViewPost.cshtml"
              Write(comment.PostCommentDate.ToNiceDateTime());

            
            #line default
            #line hidden
WriteLiteral("</i>\r\n            </p>\r\n");

            
            #line 23 "..\..\Views\Home\ViewPost.cshtml"
        }
    }

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 26 "..\..\Views\Home\ViewPost.cshtml"
    
            
            #line default
            #line hidden
            
            #line 26 "..\..\Views\Home\ViewPost.cshtml"
     if (!string.Equals((string)System.Configuration.ConfigurationManager.AppSettings["environment"], "dev"))
    {

            
            #line default
            #line hidden
WriteLiteral("    <div");

WriteLiteral(" style=\'margin-top:20px\'");

WriteLiteral(" id=\"post_disqus_thread\"");

WriteLiteral("></div>\r\n");

WriteLiteral("    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n        var disqus_shortname = \'metaltheater\'; \r\n        var disqus_identifier" +
" = \'");

            
            #line 31 "..\..\Views\Home\ViewPost.cshtml"
                            Write(Model.PostID);

            
            #line default
            #line hidden
WriteLiteral("\';\r\n        var disqus_title = \'");

            
            #line 32 "..\..\Views\Home\ViewPost.cshtml"
                       Write(Model.PostTitle.Replace("'", "\\'"));

            
            #line default
            #line hidden
WriteLiteral("\';\r\n        var disqus_url = \'");

            
            #line 33 "..\..\Views\Home\ViewPost.cshtml"
                     Write(Model.FullUrl);

            
            #line default
            #line hidden
WriteLiteral("\';\r\n    </script>\r\n");

            
            #line 35 "..\..\Views\Home\ViewPost.cshtml"
    }

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n");

        }
    }
}
#pragma warning restore 1591
