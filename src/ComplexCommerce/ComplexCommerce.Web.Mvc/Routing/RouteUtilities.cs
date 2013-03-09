using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ComplexCommerce.Web.Mvc.Routing
{
    public class RouteUtilities
        : IRouteUtilities
    {

        #region IRouteUtilities Members

        public RouteData RedirectPermanent(string destinationUrl, RouteData routeData, HttpContextBase httpContext)
        {
            var response = httpContext.Response;

            response.CacheControl = "no-cache";
            response.StatusCode = 301;
            response.RedirectLocation = destinationUrl;

            routeData.Values["controller"] = "System";
            routeData.Values["action"] = "Status301";
            routeData.Values["url"] = destinationUrl;

            return routeData;
        }

        public void AddQueryStringParametersToRouteData(RouteData routeData, HttpContextBase httpContext)
        {
            var queryString = httpContext.Request.QueryString;
            if (queryString.Keys.Count > 0)
            {
                foreach (var key in queryString.AllKeys)
                {
                    routeData.Values[key] = queryString[key];
                }
            }
        }

        public RouteData CreateRouteData(RouteBase route)
        {
            return new RouteData(route, new MvcRouteHandler());
        }

        #endregion
    }
}
