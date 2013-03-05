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

            // Get all of the pages
            var pages = routeUrlProductListFactory.GetRouteUrlProductList(tenant.Id, tenant.DefaultLocale.LCID);

            // Get the culture name
            var url = httpContext.Request.Url;
            var cultureName = contextUtilities.GetCultureNameFromUrl(url).ToLowerInvariant();

            string localizedUrl;

            // Get the default Route URL
            string defaultUrl = url.AbsolutePath.Substring(1).ToLowerInvariant();

            // Get the localized Route URL
            if (!string.IsNullOrEmpty(cultureName))
            {
                localizedUrl = cultureName + url.AbsolutePath.ToLowerInvariant();
            }
            else
            {
                localizedUrl = defaultUrl;
            }

            // TODO: Add 301 redirect when using the default locale so the default url takes priority
            // over the localized url

            var page = pages.Where(x => x.RouteUrl.Equals(localizedUrl, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            if (page == null && !localizedUrl.Equals(defaultUrl))
            {
                page = pages.Where(x => x.RouteUrl.Equals(defaultUrl, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            }

            if (page != null)
            {
                result = new RouteData(this, new MvcRouteHandler());
                // TODO: Add area for different tenant types

                result.Values["controller"] = "Product";
                result.Values["action"] = "Index";
                result.Values["id"] = page.ProductXTenantLocaleId;
            }

            return result;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            VirtualPathData result = null;
            RouteUrlProductInfo page = null;
            string virtualPath = string.Empty;
            var tenant = appContext.CurrentTenant;

            // Get all of the pages
            var pages = routeUrlProductListFactory.GetRouteUrlProductList(tenant.Id, tenant.DefaultLocale.LCID);

            if (TryFindMatch(pages, values, out page))
            {
                virtualPath = page.RouteUrl;
            }

            if (!string.IsNullOrEmpty(virtualPath))
            {
                result = new VirtualPathData(this, virtualPath);
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
                foreach (var item in pages)
                {
                    if (item.ProductXTenantLocaleId.Equals(productXTenantLocaleId))
                    {
                        page = item;
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
