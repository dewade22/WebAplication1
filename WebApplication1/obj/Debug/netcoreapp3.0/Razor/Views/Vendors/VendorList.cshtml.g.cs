#pragma checksum "D:\ASP.NET Program\WebApplication1\WebApplication1\Views\Vendors\VendorList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "43437ea2bddf339137fb60e198ee76f84c08c2b4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Vendors_VendorList), @"mvc.1.0.view", @"/Views/Vendors/VendorList.cshtml")]
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
#line 1 "D:\ASP.NET Program\WebApplication1\WebApplication1\Views\_ViewImports.cshtml"
using WebApplication1;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\ASP.NET Program\WebApplication1\WebApplication1\Views\_ViewImports.cshtml"
using WebApplication1.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"43437ea2bddf339137fb60e198ee76f84c08c2b4", @"/Views/Vendors/VendorList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"729efaa87342638aecfe1a972ce9f9f8dff55b4c", @"/Views/_ViewImports.cshtml")]
    public class Views_Vendors_VendorList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<WebApplication1.Models.ViewModel.VendorViewModel>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("text/javascript"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/ThemeForest/vendors/bootgrid/jquery.bootgrid.updated.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/ThemeForest/vendors/bootgrid/jquery.bootgrid.min.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\ASP.NET Program\WebApplication1\WebApplication1\Views\Vendors\VendorList.cshtml"
  
    ViewData["Title"] = "Offline Vendors";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<section id=""content"">
    <div class=""container"">
        <div class=""card"">
            <div class=""card-header"">
                <h2>Offline Vendor List</h2>
            </div>
            <div class=""card-body card-padding"">
                <div class=""table-responsive"">
                    <table id=""tabel-List-vendors"" class=""table table-vmiddle"" style=""table-layout: auto;"">
                        <thead>
                            <tr>
                                <th data-column-id=""id"" data-type=""numeric"" data-visible=""false"" data-order=""asc"">ID</th>
                                <th data-column-id=""tes"" data-visible=""false"" data-sortable=""false"">TestingGetVID</th>
                                <th data-column-id=""No"">No</th>
                                <th data-column-id=""CompanyName"">Company Name</th>
                                <th data-column-id=""CompanyAddress"">Company Address</th>
                                <th data-column-id=""Status"" data-formatter=""Stat");
            WriteLiteral(@"usV"" data-sortable=""false"">Status</th>
                                <th data-column-id=""commands"" data-formatter=""commands"" data-sortable=""false"">Commands</th>
                            </tr>
                        </thead>
                        <tbody>
");
#nullable restore
#line 29 "D:\ASP.NET Program\WebApplication1\WebApplication1\Views\Vendors\VendorList.cshtml"
                               int i = 1;

#line default
#line hidden
#nullable disable
#nullable restore
#line 30 "D:\ASP.NET Program\WebApplication1\WebApplication1\Views\Vendors\VendorList.cshtml"
                             foreach (var item in Model)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <tr>\r\n                                    <td>");
#nullable restore
#line 33 "D:\ASP.NET Program\WebApplication1\WebApplication1\Views\Vendors\VendorList.cshtml"
                                   Write(item.vendors.VendorId);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                    <td>");
#nullable restore
#line 34 "D:\ASP.NET Program\WebApplication1\WebApplication1\Views\Vendors\VendorList.cshtml"
                                   Write(Html.DisplayFor(a => item.aspVendors.Id));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                    <td>");
#nullable restore
#line 35 "D:\ASP.NET Program\WebApplication1\WebApplication1\Views\Vendors\VendorList.cshtml"
                                    Write(i++);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                    <td>");
#nullable restore
#line 36 "D:\ASP.NET Program\WebApplication1\WebApplication1\Views\Vendors\VendorList.cshtml"
                                   Write(item.vendors.VendorName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                    <td>");
#nullable restore
#line 37 "D:\ASP.NET Program\WebApplication1\WebApplication1\Views\Vendors\VendorList.cshtml"
                                   Write(item.vendors.VendorAddress);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                    <td></td>\r\n                                    <td></td>\r\n                                </tr>\r\n");
#nullable restore
#line 41 "D:\ASP.NET Program\WebApplication1\WebApplication1\Views\Vendors\VendorList.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </tbody>\r\n                    </table>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</section>\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "43437ea2bddf339137fb60e198ee76f84c08c2b49058", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"
    <!--Section Custom Script-->
    <script>
        $(document).ready(function () {
            $(""#tabel-List-vendors"").bootgrid({
                caseSensitive: false,
                css: {
                    icon: 'zmdi icon',
                    iconColumns: 'zmdi-view-module',
                    iconDown: 'zmdi-expand-more',
                    iconRefresh: 'zmdi-refresh',
                    iconUp: 'zmdi-expand-less'
                },
                formatters: {
                    ""commands"": function (column, row) {
                        return ""<button type=\""button\"" class=\""btn btn-icon command-delete waves-effect waves-circle\"" onclick='ShowVendor(\"""" + row.id + ""\"")'><span class=\""zmdi zmdi-eye\""></span></button>"";
                    },
                    ""StatusV"": function (column, row) {
                        if (row.tes == null || row.tes == """") {
                            return ""<button type=\""button\"" class=\""btn btn-danger waves-effect\""><span class=\""");
                WriteLiteral(@"zmdi zmdi-lock\""></span> Offline Vendor</button>"";
                        }
                        else {
                            return ""<button type=\""button\"" class=\""btn palette-Blue bg waves-effect\""><span class=\""zmdi zmdi-lock-open\""></span> Online Vendor</button>"";
                        }
                    }
                }
            });
        })
    </script>
<script>
    function ShowVendor(id) {
        window.location.href = ""/Vendors/ShowAllVendorListDetail?ILUghasFvwyeUGdmeLanyonevajhsdhjvid=""+id;
    }
</script>
");
            }
            );
            WriteLiteral("\r\n");
            DefineSection("Styles", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "43437ea2bddf339137fb60e198ee76f84c08c2b412058", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<WebApplication1.Models.ViewModel.VendorViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
