using System;
using System.Web;
using System.Web.Routing;
using ComplexCommerce.Business.Text;

namespace ComplexCommerce.Web.Mvc.Routing
{
    public class LowerCaseRedirectRoute
        : RouteBase
    {
        public LowerCaseRedirectRoute(
            IRouteUtilities routeUtilities
            )
        {
            if (routeUtilities == null)
                throw new ArgumentNullException("routeUtilities");
            this.routeUtilities = routeUtilities;
        }

        private readonly IRouteUtilities routeUtilities;

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            var request = httpContext.Request;
            if (request.RawUrl.ContainsUpper())
            {
                var destinationUrl = request.RawUrl.ToLowerInvariant();
                var routeData = routeUtilities.CreateRouteData(this);
                return routeUtilities.RedirectPermanent(destinationUrl, routeData, httpContext);
            }
            return null;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            return null;
        }
    }
}
