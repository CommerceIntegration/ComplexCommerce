using System;
using System.Web;
using System.Web.Routing;
using System.Globalization;
using ComplexCommerce.Business;

namespace ComplexCommerce.Web
{
    public interface IContextUtilities
    {
        ITenant GetTenantFromContext(HttpContextBase context);
        CultureInfo GetLocaleFromContext(HttpContextBase context, CultureInfo defaultLocale);
        string GetCultureNameFromUrl(Uri url);
    }
}
