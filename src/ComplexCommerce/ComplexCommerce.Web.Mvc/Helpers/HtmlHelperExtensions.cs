using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using ComplexCommerce.Business.Globalization;

namespace ComplexCommerce.Web.Mvc.Helpers
{
    public static class HtmlHelperExtensions
    {
        //// Sample
        //public static string MyExtensionMethod(this HtmlHelper html)
        //{
        //    return "Hello, world!";
        //}

        public static MvcHtmlString PageLanguages(this HtmlHelper html, Guid pageId, string templateName)
        {
            var model = PageLocaleList.GetCachedPageLocaleList(pageId).OrderBy(x => x.NativeName);
            var helper = new HtmlHelper<IEnumerable<PageLocaleInfo>>(html.ViewContext, new ViewDataContainer<IEnumerable<PageLocaleInfo>>(model));
            return helper.DisplayFor(m => model, templateName);
        }

        public static MvcHtmlString ProductLanguages(this HtmlHelper html, Guid productId, int tenantId, string templateName)
        {
            var model = ProductLocaleList.GetCachedProductLocaleList(productId, tenantId).OrderBy(x => x.NativeName);
            var helper = new HtmlHelper<IEnumerable<ProductLocaleInfo>>(html.ViewContext, new ViewDataContainer<IEnumerable<ProductLocaleInfo>>(model));
            return helper.DisplayFor(m => model, templateName);
        }
    }
}
