using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.WebPages;
using ComplexCommerce.Business.Globalization;
using ComplexCommerce.Business.Context;

namespace ComplexCommerce.Web.Mvc.Helpers
{
    public static class WebPageExecutingBaseExtensions
    {
        public static MvcHtmlString T(this WebPageExecutingBase view, string textName, params object[] formatterArguments)
        {
            var translated = GetLocalizedText(view.VirtualPath, textName);
            return MvcHtmlString.Create(formatterArguments.Length == 0
                        ? translated
                        : string.Format(translated, formatterArguments));
        }

        private static string GetLocalizedText(string virtualPath, string textName)
        {
            var appContext = new ApplicationContext();
            var list = ViewTextLocaleList.GetCachedViewTextLocaleList(
                appContext.CurrentTenant.Id, appContext.CurrentLocaleId, virtualPath);
            return list.GetLocalizedText(textName);
        }
    }
}
