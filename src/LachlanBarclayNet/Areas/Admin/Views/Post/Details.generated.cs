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
    using LachlanBarclayNet.Areas.Admin.ViewModels;
    using LachlanBarclayNet.DAO;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Admin/Views/Post/Details.cshtml")]
    public partial class _Areas_Admin_Views_Post_Details_cshtml : System.Web.Mvc.WebViewPage<LachlanBarclayNet.DAO.Post>
    {
        public _Areas_Admin_Views_Post_Details_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 3 "..\..\Areas\Admin\Views\Post\Details.cshtml"
  
    ViewBag.Title = "Details";

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n<h2>Preview</h2>\r\n\r\n\r\n    <h2");

WriteLiteral(" style=\"margin-bottom: 0px;font-size: 24pt\"");

WriteLiteral(">");

            
            #line 10 "..\..\Areas\Admin\Views\Post\Details.cshtml"
                                              Write(Model.PostTitle);

            
            #line default
            #line hidden
WriteLiteral("</h2>\r\n    \r\n    <span");

WriteLiteral(" style=\"color: gray\"");

WriteLiteral(">");

            
            #line 12 "..\..\Areas\Admin\Views\Post\Details.cshtml"
                         Write(Model.PostDate);

            
            #line default
            #line hidden
WriteLiteral("</span>\r\n    <br /><br />\r\n\r\n");

WriteLiteral("    ");

            
            #line 15 "..\..\Areas\Admin\Views\Post\Details.cshtml"
Write(Html.Raw(Model.PostText));

            
            #line default
            #line hidden
WriteLiteral("\r\n    \r\n<br /><br />\r\n<p>\r\n");

WriteLiteral("    ");

            
            #line 19 "..\..\Areas\Admin\Views\Post\Details.cshtml"
Write(Html.ActionLink("Edit", "Edit", new { id=Model.PostID }));

            
            #line default
            #line hidden
WriteLiteral(" |\r\n");

WriteLiteral("    ");

            
            #line 20 "..\..\Areas\Admin\Views\Post\Details.cshtml"
Write(Html.ActionLink("Back to List", "Index"));

            
            #line default
            #line hidden
WriteLiteral("\r\n</p>\r\n");

        }
    }
}
#pragma warning restore 1591
