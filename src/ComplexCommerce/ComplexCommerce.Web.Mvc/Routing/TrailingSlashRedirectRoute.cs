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
            //if (!request.Path.EndsWith("/"))
            //{
            //    var builder = new UriBuilder(request.Url)
            //    {
            //        Path = request.Path + "/"
            //    };
            //    var destinationUrl = builder.Uri.ToString();
            //    var routeData = routeUtilities.CreateRouteData(this);
            //    return routeUtilities.RedirectPermanent(destinationUrl, routeData, httpContext);
            //}

            string destinationUrl = string.Empty;

            // NOTE: It is more user friendly (although technically incorrect) to 
            // remove the trailing slash than to add it. This also means less characters
            // to work with, so it is more efficient too.
            if (request.Path.Length > 1 && request.Path.EndsWith("/"))
            {
                var builder = new UriBuilder(request.Url)
                {
                    Path = request.Path.Substring(0, request.Path.Length - 1)
                };
                destinationUrl = builder.Uri.ToString();
            }
            else if (request.Path.Length == 0)
            {
                // Home page is a special case - always use a trailing slash
                var builder = new UriBuilder(request.Url)
                {
                    Path = "/"
                };
                destinationUrl = builder.Uri.ToString();
            }
            if (!string.IsNullOrEmpty(destinationUrl))
            {
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
