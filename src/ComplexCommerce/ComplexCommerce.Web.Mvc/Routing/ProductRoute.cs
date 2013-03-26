using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ComplexCommerce.Business;
using ComplexCommerce.Business.Context;

namespace ComplexCommerce.Web.Mvc.Routing
{
    public class ProductRoute
        : RouteBase
    {
        public ProductRoute(
            IApplicationContext appContext,
            IRouteUrlProductListFactory routeUrlProductListFactory,
            IRouteUtilities routeUtilities
            )
        {
            if (appContext == null)
                throw new ArgumentNullException("appContext");
            if (routeUrlProductListFactory == null)
                throw new ArgumentNullException("routeUrlProductListFactory");
            if (routeUtilities == null)
                throw new ArgumentNullException("routeUtilities");
            this.appContext = appContext;
            this.routeUrlProductListFactory = routeUrlProductListFactory;
            this.routeUtilities = routeUtilities;
        }

        private readonly IApplicationContext appContext;
        private readonly IRouteUrlProductListFactory routeUrlProductListFactory;
        private readonly IRouteUtilities routeUtilities;

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            RouteData result = null;
            var tenant = appContext.CurrentTenant;
            var localeId = appContext.CurrentLocaleId;

            if (tenant.TenantType == TenantTypeEnum.Store)
            {
                // Get all of the pages
                var path = httpContext.Request.Path;
                var pathLength = path.Length;

                var page = routeUrlProductListFactory
                    .GetRouteUrlProductList(tenant.Id, localeId)
                    .Where(x => x.UrlPath.Length.Equals(pathLength))
                    .Where(x => x.UrlPath.Equals(path))
                    .FirstOrDefault();
                
                if (page != null)
                {
                    //result = new RouteData(this, new MvcRouteHandler());
                    result = routeUtilities.CreateRouteData(this);

                    routeUtilities.AddQueryStringParametersToRouteData(result, httpContext);

                    // TODO: Add area for different tenant types
                    result.Values["controller"] = "Product";
                    result.Values["action"] = "Details";
                    //result.Values["id"] = page.ProductXTenantLocaleId;

                    // NOTE: May need a compound key here (ProductXTenantLocaleID and 
                    // CategoryId) to allow product to be hosted on pages that are not 
                    // below categories.
                    result.Values["id"] = page.CategoryXProductXTenantLocaleId;
                }
            }
            return result;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            VirtualPathData result = null;
            var tenant = appContext.CurrentTenant;
            var localeId = appContext.CurrentLocaleId;

            if (tenant.TenantType == TenantTypeEnum.Store)
            {
                IRouteUrlProductInfo page = null;

                // Get all of the pages
                var pages = routeUrlProductListFactory.GetRouteUrlProductList(tenant.Id, localeId);

                if (TryFindMatch(pages, values, out page))
                {
                    if (!string.IsNullOrEmpty(page.VirtualPath))
                    {
                        result = new VirtualPathData(this, page.VirtualPath);
                    }
                }
            }
            return result;
        }

        private bool TryFindMatch(IEnumerable<IRouteUrlProductInfo> pages, RouteValueDictionary values, out IRouteUrlProductInfo page)
        {
            page = null;
            Guid categoryXProductXTenantLocaleId = Guid.Empty;

            if (!Guid.TryParse(Convert.ToString(values["id"]), out categoryXProductXTenantLocaleId))
            {
                return false;
            }

            var controller = Convert.ToString(values["controller"]);
            var action = Convert.ToString(values["action"]);

            if (action == "Details" && controller == "Product")
            {
                page = pages.Where(x => x.CategoryXProductXTenantLocaleId.Equals(categoryXProductXTenantLocaleId)).FirstOrDefault();
                if (page != null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
