#pragma checksum "C:\Users\user\source\repos\FurnitureStore\FurnitureStore\Views\Home\About.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "311ceceeb6516fa50404920db5e8e9e5faa62793"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_About), @"mvc.1.0.view", @"/Views/Home/About.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"311ceceeb6516fa50404920db5e8e9e5faa62793", @"/Views/Home/About.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c4949b416ae3a1d7f65eeec5515146fa5b1aa937", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Home_About : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<FurnitureStore.Models.Pages>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\user\source\repos\FurnitureStore\FurnitureStore\Views\Home\About.cshtml"
  
    ViewData["Title"] = "About";
    Layout = "~/Views/Shared/_HomeLayout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n\r\n<!--sofa-collection start -->\r\n<section id=\"sofa-collection\" style=\"margin-top:120px;\">\r\n\t<div class=\"owl-carousel owl-theme\" id=\"collection-carousel\">\r\n\t\t<div class=\"sofa-collection collectionbg1\"");
            BeginWriteAttribute("style", " style=\"", 331, "\"", 385, 3);
            WriteAttributeValue("", 339, "background:url(/Images/", 339, 23, true);
#nullable restore
#line 12 "C:\Users\user\source\repos\FurnitureStore\FurnitureStore\Views\Home\About.cshtml"
WriteAttributeValue("", 362, Model.AboutImagePath, 362, 21, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 383, ");", 383, 2, true);
            EndWriteAttribute();
            WriteLiteral(">\r\n");
            WriteLiteral("\t\t\t<div class=\"container\">\r\n\t\t\t\t<div class=\"sofa-collection-txt\">\r\n\t\t\t\t\t<h2>");
#nullable restore
#line 16 "C:\Users\user\source\repos\FurnitureStore\FurnitureStore\Views\Home\About.cshtml"
                   Write(Model.Greeting);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n\t\t\t\t\t<p>\r\n\t\t\t\t\t\t");
#nullable restore
#line 18 "C:\Users\user\source\repos\FurnitureStore\FurnitureStore\Views\Home\About.cshtml"
                   Write(Model.About);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\t\t\t\t\t</p>\r\n\t\t\t\t</div>\r\n\t\t\t</div>\r\n\t\t</div><!--/.sofa-collection-->\r\n\t</div><!--/.collection-carousel-->\r\n\r\n</section><!--/.sofa-collection-->\r\n<!--sofa-collection end -->\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<FurnitureStore.Models.Pages> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
