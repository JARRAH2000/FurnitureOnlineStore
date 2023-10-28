#pragma checksum "C:\Users\user\source\repos\FurnitureStore\FurnitureStore\Views\Furnitures\FurnituresOfSection.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "90de407eac81923ee4bfb4505755aad299f5a630"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Furnitures_FurnituresOfSection), @"mvc.1.0.view", @"/Views/Furnitures/FurnituresOfSection.cshtml")]
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
#nullable restore
#line 2 "C:\Users\user\source\repos\FurnitureStore\FurnitureStore\Views\Furnitures\FurnituresOfSection.cshtml"
using Microsoft.EntityFrameworkCore;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"90de407eac81923ee4bfb4505755aad299f5a630", @"/Views/Furnitures/FurnituresOfSection.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c4949b416ae3a1d7f65eeec5515146fa5b1aa937", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Furnitures_FurnituresOfSection : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<FurnitureStore.Models.Furniture>>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString("feature image"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("width:300px;height:300px"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "C:\Users\user\source\repos\FurnitureStore\FurnitureStore\Views\Furnitures\FurnituresOfSection.cshtml"
  
	ViewData["Title"] = "Search";
	Layout = "_HomeLayout";

#line default
#line hidden
#nullable disable
            WriteLiteral("<section id=\"feature\" class=\"feature\">\r\n\t<div class=\"container\">\r\n\t\t<div class=\"section-header\">\r\n\t\t\t<h2>Furnitures</h2>\r\n\t\t</div><!--/.section-header-->\r\n\t\t<div class=\"feature-content\">\r\n\t\t\t<div class=\"row\">\r\n");
#nullable restore
#line 68 "C:\Users\user\source\repos\FurnitureStore\FurnitureStore\Views\Furnitures\FurnituresOfSection.cshtml"
                  
					await Product();
				

#line default
#line hidden
#nullable disable
            WriteLiteral("\t\t\t</div>\r\n\t\t</div>\r\n\t</div><!--/.container-->\r\n</section><!--/.feature-->\r\n<!--feature end -->");
        }
        #pragma warning restore 1998
#nullable restore
#line 7 "C:\Users\user\source\repos\FurnitureStore\FurnitureStore\Views\Furnitures\FurnituresOfSection.cshtml"
            

	async Task Product()
	{
		ModelContext context = new ModelContext();
		

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "C:\Users\user\source\repos\FurnitureStore\FurnitureStore\Views\Furnitures\FurnituresOfSection.cshtml"
         foreach (Furniture furniture in Model)
		{
			var offer = await context.Offers.Where(offer => offer.FurnitureId == furniture.Id && offer.StartDate <= DateTime.Today && offer.EndDate >= DateTime.Today).OrderByDescending(offer => offer.Id).FirstOrDefaultAsync();
			var reviews = await context.Ratings.Where(rating => rating.FurnitureId == furniture.Id).ToListAsync();
			var count = (int)reviews.Sum(review => review.Stars) / (reviews.Count() == 0 ? 1 : reviews.Count());

#line default
#line hidden
#nullable disable
        WriteLiteral("\t\t\t<div class=\"col-sm-3\" style=\"border:groove;cursor:pointer;\" onclick=window.open(\"/Furnitures/ShowProduct/");
#nullable restore
#line 17 "C:\Users\user\source\repos\FurnitureStore\FurnitureStore\Views\Furnitures\FurnituresOfSection.cshtml"
                                                                                                                Write(furniture.Id);

#line default
#line hidden
#nullable disable
        WriteLiteral("\")>\r\n\t\t\t\t<div class=\"single-feature\">\r\n");
#nullable restore
#line 19 "C:\Users\user\source\repos\FurnitureStore\FurnitureStore\Views\Furnitures\FurnituresOfSection.cshtml"
                     if (offer != null)
					{

#line default
#line hidden
#nullable disable
        WriteLiteral("\t\t\t\t\t\t<div class=\"sale bg-2\">\r\n\t\t\t\t\t\t\t<p>sale</p>\r\n\t\t\t\t\t\t</div>\r\n");
#nullable restore
#line 24 "C:\Users\user\source\repos\FurnitureStore\FurnitureStore\Views\Furnitures\FurnituresOfSection.cshtml"
					}

#line default
#line hidden
#nullable disable
        WriteLiteral("\t\t\t\t\t");
        __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "90de407eac81923ee4bfb4505755aad299f5a6307179", async() => {
        }
        );
        __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
        __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
        BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        AddHtmlAttributeValue("", 1007, "~/Images/", 1007, 9, true);
#nullable restore
#line 25 "C:\Users\user\source\repos\FurnitureStore\FurnitureStore\Views\Furnitures\FurnituresOfSection.cshtml"
AddHtmlAttributeValue("", 1016, furniture.Imagepath, 1016, 20, false);

#line default
#line hidden
#nullable disable
        EndAddHtmlAttributeValues(__tagHelperExecutionContext);
        __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
        __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
        await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
        if (!__tagHelperExecutionContext.Output.IsContentModified)
        {
            await __tagHelperExecutionContext.SetOutputContentAsync();
        }
        Write(__tagHelperExecutionContext.Output);
        __tagHelperExecutionContext = __tagHelperScopeManager.End();
        WriteLiteral("\r\n\t\t\t\t\t<div class=\"single-feature-txt text-center\">\r\n\t\t\t\t\t\t<p>\r\n");
#nullable restore
#line 28 "C:\Users\user\source\repos\FurnitureStore\FurnitureStore\Views\Furnitures\FurnituresOfSection.cshtml"
                             for (int i = 1; i <= 5; i++)
							{
								if (i <= count)
								{

#line default
#line hidden
#nullable disable
        WriteLiteral("\t\t\t\t\t\t\t\t\t<i class=\"fa fa-star\"></i>\r\n");
#nullable restore
#line 33 "C:\Users\user\source\repos\FurnitureStore\FurnitureStore\Views\Furnitures\FurnituresOfSection.cshtml"
								}
								else
								{

#line default
#line hidden
#nullable disable
        WriteLiteral("\t\t\t\t\t\t\t\t\t<span class=\"spacial-feature-icon\"><i class=\"fa fa-star\"></i></span>\r\n");
#nullable restore
#line 37 "C:\Users\user\source\repos\FurnitureStore\FurnitureStore\Views\Furnitures\FurnituresOfSection.cshtml"
								}
							}

#line default
#line hidden
#nullable disable
        WriteLiteral("\t\t\t\t\t\t\t<span class=\"feature-review\">(");
#nullable restore
#line 39 "C:\Users\user\source\repos\FurnitureStore\FurnitureStore\Views\Furnitures\FurnituresOfSection.cshtml"
                                                     Write(reviews.Count());

#line default
#line hidden
#nullable disable
        WriteLiteral(" review)</span>\r\n\t\t\t\t\t\t</p>\r\n\t\t\t\t\t\t<h1>");
#nullable restore
#line 41 "C:\Users\user\source\repos\FurnitureStore\FurnitureStore\Views\Furnitures\FurnituresOfSection.cshtml"
                       Write(furniture.Name);

#line default
#line hidden
#nullable disable
        WriteLiteral("</h1>\r\n");
#nullable restore
#line 42 "C:\Users\user\source\repos\FurnitureStore\FurnitureStore\Views\Furnitures\FurnituresOfSection.cshtml"
                         if (offer != null)
						{

#line default
#line hidden
#nullable disable
        WriteLiteral("\t\t\t\t\t\t\t<p style=\"font-size:20px;color:black\">\r\n\t\t\t\t\t\t\t\t<span title=\"Current Price\">");
#nullable restore
#line 45 "C:\Users\user\source\repos\FurnitureStore\FurnitureStore\Views\Furnitures\FurnituresOfSection.cshtml"
                                                       Write(string.Format("{0:C}", furniture.Price - furniture.Price * offer.Percentage / 100m));

#line default
#line hidden
#nullable disable
        WriteLiteral("</span>\r\n\t\t\t\t\t\t\t\t<del title=\"Old Price\">");
#nullable restore
#line 46 "C:\Users\user\source\repos\FurnitureStore\FurnitureStore\Views\Furnitures\FurnituresOfSection.cshtml"
                                                  Write(string.Format("{0:C}", furniture.Price));

#line default
#line hidden
#nullable disable
        WriteLiteral("</del>\r\n\t\t\t\t\t\t\t</p>\r\n");
#nullable restore
#line 48 "C:\Users\user\source\repos\FurnitureStore\FurnitureStore\Views\Furnitures\FurnituresOfSection.cshtml"
						}
						else
						{

#line default
#line hidden
#nullable disable
        WriteLiteral("\t\t\t\t\t\t\t<p title=\"Current Price\" style=\"font-size:20px;color:black\">\r\n\t\t\t\t\t\t\t\t$");
#nullable restore
#line 52 "C:\Users\user\source\repos\FurnitureStore\FurnitureStore\Views\Furnitures\FurnituresOfSection.cshtml"
                            Write(furniture.Price);

#line default
#line hidden
#nullable disable
        WriteLiteral("\r\n\t\t\t\t\t\t\t</p>\r\n");
#nullable restore
#line 54 "C:\Users\user\source\repos\FurnitureStore\FurnitureStore\Views\Furnitures\FurnituresOfSection.cshtml"
						}

#line default
#line hidden
#nullable disable
        WriteLiteral("\t\t\t\t\t</div>\r\n\t\t\t\t</div>\r\n\t\t\t</div>\r\n");
#nullable restore
#line 58 "C:\Users\user\source\repos\FurnitureStore\FurnitureStore\Views\Furnitures\FurnituresOfSection.cshtml"
		}

#line default
#line hidden
#nullable disable
#nullable restore
#line 58 "C:\Users\user\source\repos\FurnitureStore\FurnitureStore\Views\Furnitures\FurnituresOfSection.cshtml"
         
	}

#line default
#line hidden
#nullable disable
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<FurnitureStore.Models.Furniture>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591