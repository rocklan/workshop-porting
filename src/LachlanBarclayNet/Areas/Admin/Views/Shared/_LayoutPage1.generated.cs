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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Admin/Views/Shared/_LayoutPage1.cshtml")]
    public partial class _Areas_Admin_Views_Shared__LayoutPage1_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Areas_Admin_Views_Shared__LayoutPage1_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<!DOCTYPE html>\r\n\r\n<html>\r\n<head>\r\n    <meta");

WriteLiteral(" name=\"viewport\"");

WriteLiteral(" content=\"width=device-width\"");

WriteLiteral(" />\r\n    <title>");

            
            #line 6 "..\..\Areas\Admin\Views\Shared\_LayoutPage1.cshtml"
      Write(ViewBag.Title);

            
            #line default
            #line hidden
WriteLiteral("</title>\r\n</head>\r\n<body>\r\n    <h1>Admin</h1>\r\n    <a");

WriteLiteral(" style=\'margin-right: 20px\'");

WriteAttribute("href", Tuple.Create(" href=\"", 199), Tuple.Create("\"", 258)
            
            #line 10 "..\..\Areas\Admin\Views\Shared\_LayoutPage1.cshtml"
, Tuple.Create(Tuple.Create("", 206), Tuple.Create<System.Object, System.Int32>(Url.Action("Index", "Post", new { area = "admin" })
            
            #line default
            #line hidden
, 206), false)
);

WriteLiteral(">Posts</a>\r\n    <a");

WriteLiteral(" style=\'margin-right: 20px\'");

WriteAttribute("href", Tuple.Create(" href=\"", 304), Tuple.Create("\"", 364)
            
            #line 11 "..\..\Areas\Admin\Views\Shared\_LayoutPage1.cshtml"
, Tuple.Create(Tuple.Create("", 311), Tuple.Create<System.Object, System.Int32>(Url.Action("Search", "Post", new { area = "admin" })
            
            #line default
            #line hidden
, 311), false)
);

WriteLiteral(">Search</a>\r\n    <a");

WriteLiteral(" style=\'margin-right: 20px\'");

WriteAttribute("href", Tuple.Create(" href=\"", 411), Tuple.Create("\"", 468)
            
            #line 12 "..\..\Areas\Admin\Views\Shared\_LayoutPage1.cshtml"
, Tuple.Create(Tuple.Create("", 418), Tuple.Create<System.Object, System.Int32>(Url.Action("Img", "Post", new { area = "admin" })
            
            #line default
            #line hidden
, 418), false)
);

WriteLiteral(">Image Links</a>\r\n    <a");

WriteLiteral(" style=\'margin-right: 20px\'");

WriteAttribute("href", Tuple.Create(" href=\"", 520), Tuple.Create("\"", 588)
            
            #line 13 "..\..\Areas\Admin\Views\Shared\_LayoutPage1.cshtml"
, Tuple.Create(Tuple.Create("", 527), Tuple.Create<System.Object, System.Int32>(Url.Action("SetPassword", "Account", new { area = "admin" })
            
            #line default
            #line hidden
, 527), false)
);

WriteLiteral(">Set Password</a>\r\n    <a");

WriteLiteral(" style=\'margin-right: 20px\'");

WriteAttribute("href", Tuple.Create(" href=\"", 641), Tuple.Create("\"", 704)
            
            #line 14 "..\..\Areas\Admin\Views\Shared\_LayoutPage1.cshtml"
, Tuple.Create(Tuple.Create("", 648), Tuple.Create<System.Object, System.Int32>(Url.Action("QrCode", "Account", new { area = "admin" })
            
            #line default
            #line hidden
, 648), false)
);

WriteLiteral(">Qr Code</a>\r\n    <a");

WriteAttribute("href", Tuple.Create(" href=\"", 725), Tuple.Create("\"", 786)
            
            #line 15 "..\..\Areas\Admin\Views\Shared\_LayoutPage1.cshtml"
, Tuple.Create(Tuple.Create("", 732), Tuple.Create<System.Object, System.Int32>(Url.Action("Logout", "Login", new { area = "admin" })
            
            #line default
            #line hidden
, 732), false)
);

WriteLiteral(">Logout</a>\r\n    <div>\r\n");

WriteLiteral("        ");

            
            #line 17 "..\..\Areas\Admin\Views\Shared\_LayoutPage1.cshtml"
   Write(RenderBody());

            
            #line default
            #line hidden
WriteLiteral("\r\n    </div>\r\n</body>\r\n</html>\r\n");

        }
    }
}
#pragma warning restore 1591
