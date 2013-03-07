using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Threading;
using System.Globalization;
using ComplexCommerce.Business;
using ComplexCommerce.Business.Context;


namespace ComplexCommerce.Web.Mvc
{
    public class DefaultLocaleRedirectRoute
        : RedirectRouteBase
    {
        public DefaultLocaleRedirectRoute(
            IApplicationContext appContext,
            IContextUtilities contextUtilities
            )
        {
            if (appContext == null)
                throw new ArgumentNullException("appContext");
            if (contextUtilities == null)
                throw new ArgumentNullException("contextUtilities");
            this.appContext = appContext;
            this.contextUtilities = contextUtilities;
        }

        private readonly IApplicationContext appContext;
        private readonly IContextUtilities contextUtilities;

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            var tenant = appContext.CurrentTenant;
            var url = httpContext.Request.Url;

            // Get the culture name
            var cultureName = contextUtilities.GetCultureNameFromUrl(url).ToLowerInvariant();

            if (this.IsDefaultUICulture(cultureName, tenant))
            {
                var urlWithoutCulture = httpContext.Request.RawUrl.ToString().ToLowerInvariant().Replace("/" + cultureName, "");

                // If the current culture matches the URL, we need to 301 redirect to the default URL
                return this.RedirectPermanent(urlWithoutCulture, httpContext);
            }
            return null;
        }

        // TODO: Move this to a shared localization class..?
        private bool IsDefaultUICulture(string cultureName, ITenant tenant)
        {
            return tenant.DefaultLocale.Name.Equals(cultureName, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
