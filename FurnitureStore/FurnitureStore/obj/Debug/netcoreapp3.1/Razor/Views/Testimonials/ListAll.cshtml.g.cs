#pragma checksum "C:\Users\user\source\repos\FurnitureStore\FurnitureStore\Views\Testimonials\ListAll.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9b17ca2848ede3799303649c9689c65b01674e59"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Testimonials_ListAll), @"mvc.1.0.view", @"/Views/Testimonials/ListAll.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9b17ca2848ede3799303649c9689c65b01674e59", @"/Views/Testimonials/ListAll.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c4949b416ae3a1d7f65eeec5515146fa5b1aa937", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Testimonials_ListAll : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<FurnitureStore.Models.Testimonial>>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\user\source\repos\FurnitureStore\FurnitureStore\Views\Testimonials\ListAll.cshtml"
  
    ViewData["Title"] = "ListAll";
    Layout = "_HomeLayout";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<section id=\"sofa-collection\" style=\"margin-top:100px\">\r\n\t<div class=\"section-header\" style=\"margin-left:20px\">\r\n\t\t<h2>Testimonials</h2>\r\n\t</div><!--/.section-header-->\r\n");
#nullable restore
#line 12 "C:\Users\user\source\repos\FurnitureStore\FurnitureStore\Views\Testimonials\ListAll.cshtml"
     foreach (Testimonial testimonial in Model)
	{

#line default
#line hidden
#nullable disable
            WriteLiteral("\t\t<div class=\"container\">\r\n\t\t\t<div class=\"row\" style=\"background-color:#e99c2e;padding:20px;\">\r\n\t\t\t\t<div class=\"col-md-2 col-sm-12\">\r\n\t\t\t\t\t<center><img");
            BeginWriteAttribute("src", " src=", 502, "", 562, 1);
#nullable restore
#line 17 "C:\Users\user\source\repos\FurnitureStore\FurnitureStore\Views\Testimonials\ListAll.cshtml"
WriteAttributeValue("", 507, Url.Content("~/Images/"+ testimonial.Sender.Imagepath), 507, 55, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" alt=\"brand-image\" style=\"width:200px;height:200px\" /></center>\r\n\t\t\t\t\t<center><h1>");
#nullable restore
#line 18 "C:\Users\user\source\repos\FurnitureStore\FurnitureStore\Views\Testimonials\ListAll.cshtml"
                           Write(testimonial.Sender.Firstname);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 18 "C:\Users\user\source\repos\FurnitureStore\FurnitureStore\Views\Testimonials\ListAll.cshtml"
                                                         Write(testimonial.Sender.Lastname);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1></center>\r\n\t\t\t\t</div>\r\n\t\t\t\t<div class=\"col-md-9 offset-md-1 col-sm-12\">\r\n\t\t\t\t\t<p style=\"padding:20px;font-size:20px;color:black\">\r\n\t\t\t\t\t\t");
#nullable restore
#line 22 "C:\Users\user\source\repos\FurnitureStore\FurnitureStore\Views\Testimonials\ListAll.cshtml"
                   Write(testimonial.Text);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\t\t\t\t\t</p>\r\n\t\t\t\t</div>\r\n\t\t\t</div>\r\n\t\t</div>\r\n\t\t<br />\r\n");
#nullable restore
#line 28 "C:\Users\user\source\repos\FurnitureStore\FurnitureStore\Views\Testimonials\ListAll.cshtml"
	}

#line default
#line hidden
#nullable disable
            WriteLiteral("</section><!--/.sofa-collection-->\r\n<!--sofa-collection end -->");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<FurnitureStore.Models.Testimonial>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591