using System;
using System.Web;
using System.Web.Routing;

namespace ComplexCommerce.Web.Mvc
{
    public class TrailingSlashRedirectRoute
        : RedirectRouteBase
    {
        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            var request = httpContext.Request;
            if (!request.Path.EndsWith("/"))
            {
                var path = request.Path.ToLowerInvariant();
                var destinationUrl = request.RawUrl.ToLowerInvariant().Replace(path, path + "/");
                return this.RedirectPermanent(destinationUrl, httpContext);
            }
            return null;
        }
    }
}
