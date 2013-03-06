using System;
using System.Web;
using System.Web.Routing;
using ComplexCommerce.Business.Text;

namespace ComplexCommerce.Web.Mvc
{
    public class LowerCaseRedirectRoute
        : RedirectRouteBase
    {
        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            var request = httpContext.Request;
            if (request.RawUrl.ContainsUpper())
            {
                var destinationUrl = request.RawUrl.ToLowerInvariant();
                return this.RedirectPermanent(destinationUrl, httpContext);
            }
            return null;
        }
    }
}
