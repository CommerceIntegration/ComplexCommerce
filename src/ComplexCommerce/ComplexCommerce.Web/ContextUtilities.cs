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
            var cultureName = context.Request.Url.Segments[0];
            if (IsValidCultureInfoName(cultureName))
            {
                result = new CultureInfo(cultureName);
            }
            // TODO: Possibly fall back on a cookie setting if not found in URL
            return result;
        }

        // TODO: Move this to a CSLA business rule ?
        private static bool IsValidCultureInfoName(string name)
        {
            return
                CultureInfo
                .GetCultures(CultureTypes.AllCultures)
                .Any(c => c.Name == name);
        }
    }
}
