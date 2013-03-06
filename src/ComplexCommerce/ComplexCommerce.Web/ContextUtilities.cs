using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Web;
using System.Diagnostics.Contracts;
using ComplexCommerce.Business;

namespace ComplexCommerce.Web
{
    public class ContextUtilities
        : IContextUtilities
    {
        public ContextUtilities(
            ITenantFactory tenantFactory
            )
        {
            //Contract.Requires<ArgumentNullException>(tenantFactory == null);
            if (tenantFactory == null)
                throw new ArgumentNullException("tenantFactory");

            this.tenantFactory = tenantFactory;
        }

        private readonly ITenantFactory tenantFactory;

        public ITenant GetTenantFromContext(HttpContextBase context)
        {
            return tenantFactory.GetTenant(context.Request.Url.DnsSafeHost);
        }

        public CultureInfo GetLocaleFromContext(HttpContextBase context, CultureInfo defaultLocale)
        {
            var result = defaultLocale;
            var cultureName = string.Empty;

            var url = context.Request.Url;
            //if (url.Segments.Length > 1)
            //{
            //    cultureName = RemoveTrailingSlash(url.Segments[1]);
            //}

            //if (IsValidCultureInfoName(cultureName))
            //{
            //    // TODO: Check whether culture is configured
            //    result = new CultureInfo(cultureName);
            //}

            cultureName = GetCultureNameFromUrl(url);
            if (!string.IsNullOrEmpty(cultureName))
            {
                result = new CultureInfo(cultureName);
            }

            // TODO: Possibly fall back on a cookie setting if not found in URL
            return result;
        }

        public string GetCultureNameFromUrl(Uri url)
        {
            string cultureName = string.Empty;
            if (url.Segments.Length > 1)
            {
                cultureName = RemoveTrailingSlash(url.Segments[1]);
            }
            if (IsValidCultureInfoName(cultureName))
            {
                return cultureName;
            }
            return string.Empty;
        }

        // TODO: Move this to a CSLA business rule ?
        private bool IsValidCultureInfoName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                return
                    CultureInfo
                    .GetCultures(CultureTypes.SpecificCultures)
                    .Any(c => c.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
            }
            return false;
        }

        private string RemoveTrailingSlash(string url)
        {
            if (url.EndsWith("/"))
            {
                return url.Substring(0, url.Length - 1);
            }
            return url;
        }
    }
}
