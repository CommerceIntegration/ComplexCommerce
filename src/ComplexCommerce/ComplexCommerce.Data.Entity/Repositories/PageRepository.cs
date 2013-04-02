using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;
using ComplexCommerce.Data.Context;
using ComplexCommerce.Data.Repositories;
using ComplexCommerce.Data.Dto;
using ComplexCommerce.Data.Entity.Context;

namespace ComplexCommerce.Data.Entity.Repositories
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

        public IList<ParentUrlPageDto> ListForParentUrl(int tenantId)
        {
            using (var ctx = ((IEntityFrameworkObjectContext)contextFactory.GetContext()).ContextManager)
            {
                var result = (from pageLocale in ctx.ObjectContext.PageLocale
                              join page in ctx.ObjectContext.Page
                                  on pageLocale.PageId equals page.Id
                              where page.TenantId == tenantId
                              select new ParentUrlPageDto
                              {
                                  Id = page.Id,
                                  ParentId = page.ParentId == null ? Guid.Empty : (Guid)page.ParentId,
                                  TenantId = tenantId,
                                  LocaleId = pageLocale.LocaleId,
                                  ContentType = page.ContentType,
                                  ContentId = page.ContentId,
                                  Url = pageLocale.Url,
                                  IsUrlAbsolute = pageLocale.IsUrlAbsolute
                              });

                return result.ToList();
            }
        }

        public IList<SiteMapPageDto> ListForSiteMap(int tenantId, int localeId)
        {
            using (var ctx = ((IEntityFrameworkObjectContext)contextFactory.GetContext()).ContextManager)
            {
                var result = (from pageLocale in ctx.ObjectContext.PageLocale
                              join page in ctx.ObjectContext.Page
                                  on pageLocale.PageId equals page.Id
                              where page.TenantId == tenantId
                              where pageLocale.LocaleId == localeId
                              select new SiteMapPageDto
                              {
                                  Id = page.Id,
                                  ParentId = page.ParentId == null ? Guid.Empty : (Guid)page.ParentId,
                                  ContentId = page.ContentId,
                                  Title = pageLocale.Title,
                                  Url = pageLocale.Url,
                                  IsUrlAbsolute = pageLocale.IsUrlAbsolute,
                                  MetaRobots = page.MetaRobots,
                                  IsVisibleOnMainMenu = page.IsVisibleOnMainMenu,
                                  TenantId = tenantId,
                                  LocaleId = localeId
                              });

                return result.ToList();
            }
        }

        public IList<PageLocaleDto> ListLocales(Guid pageId)
        {
            using (var ctx = ((IEntityFrameworkObjectContext)contextFactory.GetContext()).ContextManager)
            {
                var result = (from pageLocale in ctx.ObjectContext.PageLocale
                              where pageLocale.PageId == pageId
                              select new PageLocaleDto
                              {
                                  LocaleId = pageLocale.LocaleId
                              });

                return result.ToList();
            }
        }

        #endregion
    }
}
