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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Contact/Index.cshtml")]
    public partial class _Views_Contact_Index_cshtml : System.Web.Mvc.WebViewPage<LachlanBarclayNet.ViewModel.IndexContactViewModel>
    {
        public _Views_Contact_Index_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 2 "..\..\Views\Contact\Index.cshtml"
  
    ViewBag.Title = "Contact Me";
    ViewBag.MenuItemClass4 = "active";

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n<style");

WriteLiteral(" type=\"text/css\"");

WriteLiteral(">\r\n    .field-validation-error {\r\n        color: Red;\r\n    }\r\n\r\n    #Message {\r\n " +
"       height: 200px;\r\n    }\r\n</style>\r\n\r\n");

            
            #line 17 "..\..\Views\Contact\Index.cshtml"
 if (Model != null && Model.EmailSent)
{

            
            #line default
            #line hidden
WriteLiteral("    <div>\r\n        Thanks for your message, I\'ll get back to you soon.\r\n    </div" +
">\r\n");

            
            #line 22 "..\..\Views\Contact\Index.cshtml"
}
else
{
    using (Html.BeginForm())
    {
        
            
            #line default
            #line hidden
            
            #line 27 "..\..\Views\Contact\Index.cshtml"
   Write(Html.AntiForgeryToken());

            
            #line default
            #line hidden
            
            #line 27 "..\..\Views\Contact\Index.cshtml"
                                


            
            #line default
            #line hidden
WriteLiteral("        <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"col-sm-6\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n                    <label");

WriteLiteral(" for=\"comment\"");

WriteLiteral(">Name</label>\r\n");

WriteLiteral("                    ");

            
            #line 33 "..\..\Views\Contact\Index.cshtml"
               Write(Html.TextBoxFor(x => x.Name, new { @class = "form-control", maxlength = "20" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                    ");

            
            #line 34 "..\..\Views\Contact\Index.cshtml"
               Write(Html.ValidationMessageFor(x => x.Name));

            
            #line default
            #line hidden
WriteLiteral("\r\n                </div>\r\n            </div>\r\n        </div>\r\n");

WriteLiteral("        <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"col-sm-6\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n                    <label");

WriteLiteral(" for=\"comment\"");

WriteLiteral(">Email</label>\r\n");

WriteLiteral("                    ");

            
            #line 42 "..\..\Views\Contact\Index.cshtml"
               Write(Html.TextBoxFor(x => x.Email, new { @class = "form-control", maxlength = "100" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                    ");

            
            #line 43 "..\..\Views\Contact\Index.cshtml"
               Write(Html.ValidationMessageFor(x => x.Email));

            
            #line default
            #line hidden
WriteLiteral("\r\n                </div>\r\n            </div>\r\n        </div>\r\n");

WriteLiteral("        <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"col-sm-9\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n                    <label");

WriteLiteral(" for=\"comment\"");

WriteLiteral(">Comment</label>\r\n");

WriteLiteral("                    ");

            
            #line 51 "..\..\Views\Contact\Index.cshtml"
               Write(Html.TextAreaFor(x => x.Message, new { @class = "form-control" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                    ");

            
            #line 52 "..\..\Views\Contact\Index.cshtml"
               Write(Html.ValidationMessageFor(x => x.Message));

            
            #line default
            #line hidden
WriteLiteral("\r\n                </div>\r\n");

            
            #line 54 "..\..\Views\Contact\Index.cshtml"
                
            
            #line default
            #line hidden
            
            #line 54 "..\..\Views\Contact\Index.cshtml"
                   
                    if (ViewBag.lastviewed != null)
                    {
                        LachlanBarclayNet.Controllers.SessionData sd = (LachlanBarclayNet.Controllers.SessionData)ViewBag.lastviewed;

            
            #line default
            #line hidden
WriteLiteral("                        <p>(BTW if this was about <strong>");

            
            #line 58 "..\..\Views\Contact\Index.cshtml"
                                                     Write(sd.LastTitle);

            
            #line default
            #line hidden
WriteLiteral("</strong> which you clicked on at ");

            
            #line 58 "..\..\Views\Contact\Index.cshtml"
                                                                                                    Write(sd.LastRead);

            
            #line default
            #line hidden
WriteLiteral(" it might be helpful to mention)</p>\r\n");

            
            #line 59 "..\..\Views\Contact\Index.cshtml"
                    }
                
            
            #line default
            #line hidden
WriteLiteral("\r\n            </div>\r\n        </div>\r\n");

WriteLiteral("        <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"col-sm-9\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" type=\"submit\"");

WriteLiteral(" class=\"button\"");

WriteLiteral(" value=\"submit\"");

WriteLiteral(" />\r\n            </div>\r\n        </div>\r\n");

            
            #line 68 "..\..\Views\Contact\Index.cshtml"
    }
}

            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591
