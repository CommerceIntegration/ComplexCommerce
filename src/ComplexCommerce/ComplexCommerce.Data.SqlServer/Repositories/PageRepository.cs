using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;
using ComplexCommerce.Data.Context;
using ComplexCommerce.Data.Repositories;
using ComplexCommerce.Data.Dto;
using ComplexCommerce.Data.SqlServer.Context;

namespace ComplexCommerce.Data.SqlServer.Repositories
{
    public class PageRepository
        : IPageRepository
    {
        public PageRepository(IPersistenceContextFactory contextFactory)
        {
            //Contract.Requires<ArgumentNullException>(contextFactory == null);
            if (contextFactory == null)
                throw new ArgumentNullException("contextFactory");

            this.contextFactory = contextFactory;
        }

        private readonly IPersistenceContextFactory contextFactory;

        #region IPageRepository Members

        public IList<RouteUrlPageDto> ListForRouteUrl(int tenantId, int localeId)
        {
            using (var ctx = ((IEntityFrameworkObjectContext)contextFactory.GetContext()).ContextManager)
            {
                var result = (from tenantLocale in ctx.ObjectContext.TenantLocale
                             join page in ctx.ObjectContext.Page
                                 on tenantLocale.Id equals page.TenantLocaleId
                             where tenantLocale.TenantId == tenantId
                             where tenantLocale.LocaleId == localeId
                             select new RouteUrlPageDto
                             {
                                 LocaleId = localeId,
                                 RouteUrl = page.RouteUrl,
                                 ContentType = page.ContentType,
                                 ContentId = page.ContentId
                             });

                return result.ToList();
            }
        }

        public IList<SiteMapPageDto> ListForSiteMap(int tenantId, int localeId)
        {
            using (var ctx = ((IEntityFrameworkObjectContext)contextFactory.GetContext()).ContextManager)
            {
                var result = (from tenantLocale in ctx.ObjectContext.TenantLocale
                              join page in ctx.ObjectContext.Page
                                  on tenantLocale.Id equals page.TenantLocaleId
                              where tenantLocale.TenantId == tenantId
                              where tenantLocale.LocaleId == localeId
                              select new SiteMapPageDto
                              {
                                  Id = page.Id,
                                  ParentId = page.ParentId == null ? Guid.Empty : (Guid)page.ParentId,
                                  LocaleId = localeId,
                                  Title = page.Title,
                                  RouteUrl = page.RouteUrl,
                                  MetaRobots = page.MetaRobots,
                                  ContentType = page.ContentType,
                                  ContentId = page.ContentId
                              });

                return result.ToList();
            }
        }

        #endregion
    }
}
