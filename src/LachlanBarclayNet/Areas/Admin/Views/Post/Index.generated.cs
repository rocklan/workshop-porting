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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Admin/Views/Post/Index.cshtml")]
    public partial class _Areas_Admin_Views_Post_Index_cshtml : System.Web.Mvc.WebViewPage<AdminIndexViewModel>
    {
        public _Areas_Admin_Views_Post_Index_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 3 "..\..\Areas\Admin\Views\Post\Index.cshtml"
  
    ViewBag.Title = "Index";

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n<h2>Index</h2>\r\n\r\n<p>\r\n");

WriteLiteral("    ");

            
            #line 10 "..\..\Areas\Admin\Views\Post\Index.cshtml"
Write(Html.ActionLink("Create New", "Edit"));

            
            #line default
            #line hidden
WriteLiteral("\r\n</p>\r\n<table>\r\n    <tr>\r\n        <th");

WriteLiteral(" width=\"200px\"");

WriteLiteral(">\r\n            Display Date</th>\r\n        <th>Published</th>\r\n        <th");

WriteLiteral(" width=\"100px\"");

WriteLiteral(">\r\n            Post Type</th>\r\n        <th>\r\n            Post\r\n        </th>\r\n   " +
"     <th");

WriteLiteral(" width=\"250px\"");

WriteLiteral("></th>\r\n    </tr>\r\n\r\n");

            
            #line 25 "..\..\Areas\Admin\Views\Post\Index.cshtml"
 foreach (var item in Model.Posts) {

            
            #line default
            #line hidden
WriteLiteral("<tr>\r\n    <td>\r\n");

WriteLiteral("        ");

            
            #line 28 "..\..\Areas\Admin\Views\Post\Index.cshtml"
   Write(Html.DisplayFor(modelItem => item.PostDate));

            
            #line default
            #line hidden
WriteLiteral("\r\n    </td>\r\n    <td>\r\n");

WriteLiteral("        ");

            
            #line 31 "..\..\Areas\Admin\Views\Post\Index.cshtml"
   Write(Html.DisplayFor(modelItem => item.Published));

            
            #line default
            #line hidden
WriteLiteral("\r\n    </td>\r\n    <td>\r\n");

WriteLiteral("        ");

            
            #line 34 "..\..\Areas\Admin\Views\Post\Index.cshtml"
   Write(Html.DisplayFor(modelItem => item.PostType.PostTypeName));

            
            #line default
            #line hidden
WriteLiteral("\r\n    </td>\r\n    <td>\r\n");

WriteLiteral("        ");

            
            #line 37 "..\..\Areas\Admin\Views\Post\Index.cshtml"
   Write(Html.ActionLink(item.PostTitle, "Edit", new { id = item.PostID }));

            
            #line default
            #line hidden
WriteLiteral("\r\n    </td>\r\n    <td>\r\n");

WriteLiteral("        ");

            
            #line 40 "..\..\Areas\Admin\Views\Post\Index.cshtml"
   Write(Html.ActionLink("Preview", "Details", new { id = item.PostID }, new { style = "margin-right: 10px" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("        ");

            
            #line 41 "..\..\Areas\Admin\Views\Post\Index.cshtml"
   Write(Html.ActionLink("Delete", "Delete", new { id = item.PostID }, new { style = "margin-right: 10px" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n        <a");

WriteLiteral(" target=\"_blank\"");

WriteAttribute("href", Tuple.Create(" href=\"", 1035), Tuple.Create("\"", 1059)
            
            #line 42 "..\..\Areas\Admin\Views\Post\Index.cshtml"
, Tuple.Create(Tuple.Create("", 1042), Tuple.Create<System.Object, System.Int32>(item.FriendlyUrl
            
            #line default
            #line hidden
, 1042), false)
);

WriteLiteral(">Link</a>\r\n    </td>\r\n</tr>\r\n");

            
            #line 45 "..\..\Areas\Admin\Views\Post\Index.cshtml"
}

            
            #line default
            #line hidden
WriteLiteral("\r\n</table>\r\n<br />\r\n<br />\r\n<li><a");

WriteAttribute("href", Tuple.Create(" href=\"", 1126), Tuple.Create("\"", 1209)
            
            #line 50 "..\..\Areas\Admin\Views\Post\Index.cshtml"
, Tuple.Create(Tuple.Create("", 1133), Tuple.Create<System.Object, System.Int32>(Url.Action("Index", "Post", new { area="Admin", from = Model.FromString })
            
            #line default
            #line hidden
, 1133), false)
, Tuple.Create(Tuple.Create(" ", 1208), Tuple.Create("", 1208), true)
);

WriteLiteral(">Next</a></li>\r\n\r\n<br />\r\n");

        }
    }
}
#pragma warning restore 1591
