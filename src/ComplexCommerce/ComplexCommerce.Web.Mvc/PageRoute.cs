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
    public class PageRoute
        : RouteBase
    {
        public PageRoute(
            IApplicationContext appContext,
            IRouteUrlListFactory routeUrlListFactory,
            IContextUtilities contextUtilities
            )
        {
            if (appContext == null)
                throw new ArgumentNullException("appContext");
            if (routeUrlListFactory == null)
                throw new ArgumentNullException("routeUrlListFactory");
            if (contextUtilities == null)
                throw new ArgumentNullException("contextUtilities");
            this.appContext = appContext;
            this.routeUrlListFactory = routeUrlListFactory;
            this.contextUtilities = contextUtilities;
        }

        private readonly IApplicationContext appContext;
        private readonly IRouteUrlListFactory routeUrlListFactory;
        private readonly IContextUtilities contextUtilities;

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            RouteData result = null;
            var tenant = appContext.CurrentTenant;

            // Get all of the pages
            var pages = routeUrlListFactory.GetRouteUrlPageList(tenant.Id, tenant.DefaultLocale.LCID);
            var path = httpContext.Request.Path;
            var page = pages
                .Where(x => x.RouteUrl.Equals(path))
                .FirstOrDefault();
            if (page != null)
            {
                result = new RouteData(this, new MvcRouteHandler());
                // TODO: Add area for different tenant types

                // TODO: add the querystring information to the route values
                result.Values["controller"] = page.ContentType.ToString();
                result.Values["action"] = "Index";
                result.Values["id"] = page.ContentId;
            }
            return result;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            VirtualPathData result = null;
            RouteUrlPageInfo page = null;
            string virtualPath = string.Empty;
            var tenant = appContext.CurrentTenant;

            // Get all of the pages
            var pages = routeUrlListFactory.GetRouteUrlPageList(tenant.Id, tenant.DefaultLocale.LCID);

            if (TryFindMatch(pages, values, out page))
            {
                virtualPath = page.VirtualPath;
            }

            if (!string.IsNullOrEmpty(virtualPath))
            {
                result = new VirtualPathData(this, virtualPath);
            }

            return result;
        }

        private bool TryFindMatch(RouteUrlPageList pages, RouteValueDictionary values, out RouteUrlPageInfo page)
        {
            page = null;
            Guid contentId = Guid.Empty;

            if (!Guid.TryParse(Convert.ToString(values["id"]), out contentId))
            {
                return false;
            }

            var action = Convert.ToString(values["action"]);
            if (action == "Index")
            {
                var controller = Convert.ToString(values["controller"]);
                page = pages.Where(x => x.ContentId.Equals(contentId) && x.ContentType.ToString().Equals(controller)).FirstOrDefault();
                if (page != null)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
