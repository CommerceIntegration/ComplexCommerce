using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Threading;
using System.Globalization;
using ComplexCommerce.Business;
using ComplexCommerce.Business.Context;


namespace ComplexCommerce.Web.Mvc.Routing
{
    public class DefaultLocaleRedirectRoute
        : RouteBase
    {
        public DefaultLocaleRedirectRoute(
            IApplicationContext appContext,
            IRouteUtilities routeUtilities,
            IContextUtilities contextUtilities
            )
        {
            if (appContext == null)
                throw new ArgumentNullException("appContext");
            if (routeUtilities == null)
                throw new ArgumentNullException("routeUtilities");
            if (contextUtilities == null)
                throw new ArgumentNullException("contextUtilities");
            this.appContext = appContext;
            this.routeUtilities = routeUtilities;
            this.contextUtilities = contextUtilities;
        }

        private readonly IApplicationContext appContext;
        private readonly IRouteUtilities routeUtilities;
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

                var routeData = routeUtilities.CreateRouteData(this);
                // If the current culture matches the URL, we need to 301 redirect to the default URL
                return routeUtilities.RedirectPermanent(urlWithoutCulture, routeData, httpContext);
            }
            return null;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            return null;
        }

        // TODO: Move this to a shared localization class..?
        private bool IsDefaultUICulture(string cultureName, ITenant tenant)
        {
            return tenant.DefaultLocale.Name.Equals(cultureName, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
