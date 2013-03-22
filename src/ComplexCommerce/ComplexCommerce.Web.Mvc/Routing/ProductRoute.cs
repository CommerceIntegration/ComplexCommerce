using System;
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

        //private bool TryFindMatch(RouteUrlProductList pages, RouteValueDictionary values, out RouteUrlProductInfo page)
        //{
        //    page = null;
        //    Guid productXTenantLocaleId = Guid.Empty;

        //    if (!Guid.TryParse(Convert.ToString(values["id"]), out productXTenantLocaleId))
        //    {
        //        return false;
        //    }

        //    var controller = Convert.ToString(values["controller"]);
        //    var action = Convert.ToString(values["action"]);

        //    if (action == "Details" && controller == "Product")
        //    {
        //        page = pages.Where(x => x.ProductXTenantLocaleId.Equals(productXTenantLocaleId)).FirstOrDefault();
        //        if (page != null)
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        private bool TryFindMatch(RouteUrlProductList pages, RouteValueDictionary values, out RouteUrlProductInfo page)
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
