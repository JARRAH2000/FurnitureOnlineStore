#pragma checksum "C:\Users\user\source\repos\FurnitureStore\FurnitureStore\Views\Users\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "179342ec3b8db98e7990331a0c7f567d6c69d766"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Users_Details), @"mvc.1.0.view", @"/Views/Users/Details.cshtml")]
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
#line 1 "C:\Users\user\source\repos\FurnitureStore\FurnitureStore\Views\_ViewImports.cshtml"
using FurnitureStore;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\user\source\repos\FurnitureStore\FurnitureStore\Views\_ViewImports.cshtml"
using FurnitureStore.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"179342ec3b8db98e7990331a0c7f567d6c69d766", @"/Views/Users/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c4949b416ae3a1d7f65eeec5515146fa5b1aa937", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Users_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<FurnitureStore.Models.User>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "ListOfUsers", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Admin", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\user\source\repos\FurnitureStore\FurnitureStore\Views\Users\Details.cshtml"
  
    ViewData["Title"] = "Details";
    Layout = "_AdminLayout";

#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"main-content\">\r\n    <div class=\"section__content section__content--p30\">\r\n        <div class=\"container-fluid\">\r\n            <h1>Details of ");
#nullable restore
#line 10 "C:\Users\user\source\repos\FurnitureStore\FurnitureStore\Views\Users\Details.cshtml"
                      Write(Model.Firstname);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 10 "C:\Users\user\source\repos\FurnitureStore\FurnitureStore\Views\Users\Details.cshtml"
                                       Write(Model.Lastname);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n\r\n            <div>\r\n                <hr />\r\n                <dl class=\"row\">\r\n                    <dt class = \"col-sm-2\">\r\n                        ");
#nullable restore
#line 16 "C:\Users\user\source\repos\FurnitureStore\FurnitureStore\Views\Users\Details.cshtml"
                   Write(Html.DisplayNameFor(model => model.Firstname));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </dt>\r\n                    <dd class = \"col-sm-10\">\r\n                        ");
#nullable restore
#line 19 "C:\Users\user\source\repos\FurnitureStore\FurnitureStore\Views\Users\Details.cshtml"
                   Write(Html.DisplayFor(model => model.Firstname));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </dd>\r\n                    <dt class = \"col-sm-2\">\r\n                        ");
#nullable restore
#line 22 "C:\Users\user\source\repos\FurnitureStore\FurnitureStore\Views\Users\Details.cshtml"
                   Write(Html.DisplayNameFor(model => model.Lastname));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </dt>\r\n                    <dd class = \"col-sm-10\">\r\n                        ");
#nullable restore
#line 25 "C:\Users\user\source\repos\FurnitureStore\FurnitureStore\Views\Users\Details.cshtml"
                   Write(Html.DisplayFor(model => model.Lastname));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </dd>\r\n                    <dt class = \"col-sm-2\">\r\n                        ");
#nullable restore
#line 28 "C:\Users\user\source\repos\FurnitureStore\FurnitureStore\Views\Users\Details.cshtml"
                   Write(Html.DisplayNameFor(model => model.Birthdate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </dt>\r\n                    <dd class = \"col-sm-10\">\r\n                        ");
#nullable restore
#line 31 "C:\Users\user\source\repos\FurnitureStore\FurnitureStore\Views\Users\Details.cshtml"
                   Write(Html.DisplayFor(model => model.Birthdate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </dd>\r\n                    <dt class = \"col-sm-2\">\r\n                        ");
#nullable restore
#line 34 "C:\Users\user\source\repos\FurnitureStore\FurnitureStore\Views\Users\Details.cshtml"
                   Write(Html.DisplayNameFor(model => model.Sex));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </dt>\r\n                    <dd class = \"col-sm-10\">\r\n                        ");
#nullable restore
#line 37 "C:\Users\user\source\repos\FurnitureStore\FurnitureStore\Views\Users\Details.cshtml"
                   Write(Html.DisplayFor(model => model.Sex));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </dd>\r\n                    <dt class = \"col-sm-2\">\r\n                        ");
#nullable restore
#line 40 "C:\Users\user\source\repos\FurnitureStore\FurnitureStore\Views\Users\Details.cshtml"
                   Write(Html.DisplayNameFor(model => model.Imagepath));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </dt>\r\n                    <dd class = \"col-sm-10\">\r\n                        ");
#nullable restore
#line 43 "C:\Users\user\source\repos\FurnitureStore\FurnitureStore\Views\Users\Details.cshtml"
                   Write(Html.DisplayFor(model => model.Imagepath));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </dd>\r\n                </dl>\r\n            </div>\r\n            <div>\r\n");
            WriteLiteral("                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "179342ec3b8db98e7990331a0c7f567d6c69d7668644", async() => {
                WriteLiteral("Back to List");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<FurnitureStore.Models.User> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591