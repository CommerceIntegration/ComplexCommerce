using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ComplexCommerce.Web.Mvc
{
    public abstract class RedirectRouteBase
        : RouteBase
    {
        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            return null;
        }

        protected virtual RouteData RedirectPermanent(string destinationUrl, HttpContextBase httpContext)
        {
            var response = httpContext.Response;

            response.CacheControl = "no-cache";
            response.StatusCode = 301;
            response.RedirectLocation = destinationUrl;

            // TODO: Find a way to create route handler using a factory or DI
            var routeData = new RouteData(this, new MvcRouteHandler());
            routeData.Values["controller"] = "Home";
            routeData.Values["action"] = "Redirect301";
            routeData.Values["url"] = destinationUrl;

            return routeData;
        }
    }
}
