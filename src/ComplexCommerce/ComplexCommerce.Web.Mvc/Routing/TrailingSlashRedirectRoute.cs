using System;
using System.Web;
using System.Web.Routing;

namespace ComplexCommerce.Web.Mvc.Routing
{
    public class TrailingSlashRedirectRoute
        : RouteBase
    {
        public TrailingSlashRedirectRoute(
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
            if (!request.Path.EndsWith("/"))
            {
                var builder = new UriBuilder(request.Url)
                {
                    Path = request.Path + "/"
                };
                var destinationUrl = builder.Uri.ToString();
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
