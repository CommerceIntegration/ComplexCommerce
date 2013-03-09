using System;
using MvcSiteMapProvider;
using MvcSiteMapProvider.Caching;
using ComplexCommerce.Business.Context;

namespace ComplexCommerce.Web.Mvc.SiteMapProvider
{
    public class SiteMapCacheKeyGenerator
        : ISiteMapCacheKeyGenerator
    {
        public SiteMapCacheKeyGenerator(
            IApplicationContext appContext
            )
        {
            if (appContext == null)
                throw new ArgumentNullException("appContext");
            this.appContext = appContext;
        }

        private readonly IApplicationContext appContext;

        #region ISiteMapCacheKeyGenerator Members

        public string GenerateKey()
        {
            return appContext.CurrentTenant.Id.ToString() + "|" + appContext.CurrentLocaleId.ToString();
        }

        #endregion
    }
}
