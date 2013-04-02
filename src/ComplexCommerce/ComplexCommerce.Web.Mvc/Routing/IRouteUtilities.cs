using System;
using System.Web;
using System.Web.Routing;

namespace ComplexCommerce.Web.Mvc.Routing
{
    public interface IRouteUtilities
    {
        RouteData RedirectPermanent(string destinationUrl, RouteData routeData, HttpContextBase httpContext);
        void AddQueryStringParametersToRouteData(RouteData routeData, HttpContextBase httpContext);
        RouteData CreateRouteData(RouteBase route);
        VirtualPathData CreateVirtualPathData(RouteBase route, string virtualPath);
    }
}
