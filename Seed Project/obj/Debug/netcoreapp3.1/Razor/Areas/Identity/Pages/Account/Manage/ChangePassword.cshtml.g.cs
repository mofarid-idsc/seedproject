#pragma checksum "F:\IDSC\16-4-2020\seedproject\Seed Project\Areas\Identity\Pages\Account\Manage\ChangePassword.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6c914e3c47152860f73fa3b40eed8f865759bc07"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Identity_Pages_Account_Manage_ChangePassword), @"mvc.1.0.razor-page", @"/Areas/Identity/Pages/Account/Manage/ChangePassword.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "F:\IDSC\16-4-2020\seedproject\Seed Project\Areas\Identity\Pages\Account\_ViewImports.cshtml"
using Seed_Project.Areas.Identity.Pages.Account;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "F:\IDSC\16-4-2020\seedproject\Seed Project\Areas\Identity\Pages\Account\Manage\_ViewImports.cshtml"
using Seed_Project.Areas.Identity.Pages.Account.Manage;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "F:\IDSC\16-4-2020\seedproject\Seed Project\Areas\Identity\Pages\Account\Manage\_ViewImports.cshtml"
using ApplicationCore.Entities;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6c914e3c47152860f73fa3b40eed8f865759bc07", @"/Areas/Identity/Pages/Account/Manage/ChangePassword.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fd0bfc1df194bd912855ddd0242d9d54a03df7da", @"/Areas/Identity/Pages/Account/_ViewImports.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c95f2b1832e239adc66ad12a05400ba38163db4a", @"/Areas/Identity/Pages/Account/Manage/_ViewImports.cshtml")]
    public class Areas_Identity_Pages_Account_Manage_ChangePassword : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "F:\IDSC\16-4-2020\seedproject\Seed Project\Areas\Identity\Pages\Account\Manage\ChangePassword.cshtml"
  
    ViewData["Title"] = "Change password";
    ViewData["ActivePage"] = ManageNavPages.ChangePassword;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h4>");
#nullable restore
#line 8 "F:\IDSC\16-4-2020\seedproject\Seed Project\Areas\Identity\Pages\Account\Manage\ChangePassword.cshtml"
Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h4>
<partial name=""_StatusMessage"" for=""StatusMessage"" />
<div class=""row"">
    <div class=""col-md-6"">
        <form id=""change-password-form"" method=""post"">
            <div asp-validation-summary=""All"" class=""text-danger""></div>
            <div class=""form-group"">
                <label asp-for=""Input.OldPassword""></label>
                <input asp-for=""Input.OldPassword"" class=""form-control"" />
                <span asp-validation-for=""Input.OldPassword"" class=""text-danger""></span>
            </div>
            <div class=""form-group"">
                <label asp-for=""Input.NewPassword""></label>
                <input asp-for=""Input.NewPassword"" class=""form-control"" />
                <span asp-validation-for=""Input.NewPassword"" class=""text-danger""></span>
            </div>
            <div class=""form-group"">
                <label asp-for=""Input.ConfirmPassword""></label>
                <input asp-for=""Input.ConfirmPassword"" class=""form-control"" />
                <span asp-valida");
            WriteLiteral("tion-for=\"Input.ConfirmPassword\" class=\"text-danger\"></span>\r\n            </div>\r\n            <button type=\"submit\" class=\"btn btn-primary\">Update password</button>\r\n        </form>\r\n    </div>\r\n</div>\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n    <partial name=\"_ValidationScriptsPartial\" />\r\n");
            }
            );
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ChangePasswordModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<ChangePasswordModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<ChangePasswordModel>)PageContext?.ViewData;
        public ChangePasswordModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
