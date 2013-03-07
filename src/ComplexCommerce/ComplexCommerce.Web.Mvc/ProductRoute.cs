using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ComplexCommerce.Business;
using ComplexCommerce.Business.Context;

namespace ComplexCommerce.Web.Mvc
{
    public class ProductRoute
        : RouteBase
    {
        public ProductRoute(
            IApplicationContext appContext,
            IRouteUrlProductListFactory routeUrlProductListFactory,
            IContextUtilities contextUtilities
            )
        {
            if (appContext == null)
                throw new ArgumentNullException("appContext");
            if (routeUrlProductListFactory == null)
                throw new ArgumentNullException("routeUrlProductListFactory");
            if (contextUtilities == null)
                throw new ArgumentNullException("contextUtilities");
            this.appContext = appContext;
            this.routeUrlProductListFactory = routeUrlProductListFactory;
            this.contextUtilities = contextUtilities;
        }

        private readonly IApplicationContext appContext;
        private readonly IRouteUrlProductListFactory routeUrlProductListFactory;
        private readonly IContextUtilities contextUtilities;

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            RouteData result = null;
            var tenant = appContext.CurrentTenant;

            if (tenant.TenantType == TenantTypeEnum.Store)
            {
                // Get all of the pages
                var pages = routeUrlProductListFactory.GetRouteUrlProductList(tenant.Id, tenant.DefaultLocale.LCID);
                var path = httpContext.Request.Path;
                var page = pages
                    .Where(x => x.RouteUrl.Equals(path))
                    .FirstOrDefault();
                if (page != null)
                {
                    result = new RouteData(this, new MvcRouteHandler());
                    // TODO: Add area for different tenant types

                    // TODO: add the querystring information to the route values
                    result.Values["controller"] = "Product";
                    result.Values["action"] = "Index";
                    result.Values["id"] = page.ProductXTenantLocaleId;
                }
            }
            return result;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            VirtualPathData result = null;
            var tenant = appContext.CurrentTenant;

            if (tenant.TenantType == TenantTypeEnum.Store)
            {
                RouteUrlProductInfo page = null;
                string virtualPath = string.Empty;

                // Get all of the pages
                var pages = routeUrlProductListFactory.GetRouteUrlProductList(tenant.Id, tenant.DefaultLocale.LCID);

                if (TryFindMatch(pages, values, out page))
                {
                    virtualPath = page.VirtualPath;
                }

                if (!string.IsNullOrEmpty(virtualPath))
                {
                    result = new VirtualPathData(this, virtualPath);
                }
            }
            return result;
        }

        private bool TryFindMatch(RouteUrlProductList pages, RouteValueDictionary values, out RouteUrlProductInfo page)
        {
            page = null;
            Guid productXTenantLocaleId = Guid.Empty;

            if (!Guid.TryParse(Convert.ToString(values["id"]), out productXTenantLocaleId))
            {
                return false;
            }

            var controller = Convert.ToString(values["controller"]);
            var action = Convert.ToString(values["action"]);

            if (action == "Index" && controller == "Product")
            {
                page = pages.Where(x => x.ProductXTenantLocaleId.Equals(productXTenantLocaleId)).FirstOrDefault();
                if (page != null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
