using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Globalization;
using ComplexCommerce.Business;

namespace ComplexCommerce.Web
{
    public interface IContextUtilities
    {
        ITenant GetTenantFromContext(HttpContextBase context);
        CultureInfo GetLocaleFromContext(HttpContextBase context, CultureInfo defaultLocale);
    }
}
