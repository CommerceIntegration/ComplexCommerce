using System;
using System.Linq;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Routing;
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
            Uri url = null;
            Uri relativeUrl = null;

            // Try to build a Uri from the RawUrl
            bool success = Uri.TryCreate(context.Request.RawUrl, UriKind.Relative, out relativeUrl);
            if (success)
            {
                success = Uri.TryCreate(context.Request.Url, relativeUrl, out url);
            }

            // RawUrl couldn't be used, use request Url
            if (!success)
            {
                url = context.Request.Url;
            }

            // TODO: Check whether culture is configured ??

            // Get Culture Name from Url
            cultureName = GetCultureNameFromUrl(url);

            // If culture name couldn't be found in the normal place, 
            // try parsing from the query string.
            if (String.IsNullOrEmpty(cultureName) && url.Query.Length > 1)
            {
                cultureName = GetCultureNameFromErrorUrl(url);
            }
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

        private string GetCultureNameFromErrorUrl(Uri url)
        {
            string query = url.Query;
            var re = new Regex(@"(?:https?://[^\s]*||aspxerrorpath=)/(?'cultureName'[^/-]{2,3}(?:-[^/-]{2,4})?(?:-[^/]{2})?)/?");
            var match = re.Match(query);
            if (match.Success)
            {
                var cultureName = match.Groups["cultureName"].Value;
                if (IsValidCultureInfoName(cultureName))
                {
                    return cultureName;
                }
            }
            return string.Empty;
        }

        // TODO: Move this to a CSLA business rule ?
        private bool IsValidCultureInfoName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                return
                    CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                    .Union(
                        CultureInfo.GetCultures(CultureTypes.NeutralCultures)
                        .Where(x => x.Name.Length != 0) // exclude the invariant culture
                    )
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
