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

        public IList<SiteMapPageDto> List(int tenantId, int localeId)
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
                                 RouteUrl = page.RouteUrl,
                                 ContentType = page.ContentType,
                                 ContentId = page.ContentId
                             }).ToList();

                if (result.Count == 0)
                {
                    (from tenantLocale in ctx.ObjectContext.TenantLocale
                     join page in ctx.ObjectContext.Page
                         on tenantLocale.Id equals page.TenantLocaleId
                     join tenant in ctx.ObjectContext.Tenant
                         on tenantLocale.TenantId equals tenant.Id
                     where tenantLocale.TenantId == tenantId
                     where tenantLocale.LocaleId == tenant.DefaultLocaleId
                     select new SiteMapPageDto
                     {
                         Id = page.Id,
                         ParentId = page.ParentId == null ? Guid.Empty : (Guid)page.ParentId,
                         RouteUrl = page.RouteUrl,
                         ContentType = page.ContentType,
                         ContentId = page.ContentId
                     }).ToList();
                }


                // TODO: Consolidate this into a single query with default locale
                return result.ToList();


                //var result = from product in ctx.ObjectContext.Product
                //             join tenantXproductXlocale in ctx.ObjectContext.StoreXProductXLocale
                //                 on product.Id equals tenantXproductXlocale.ProductId
                //             where tenantXproductXlocale.StoreId == tenantId
                //             where tenantXproductXlocale.LocaleId == localeId
                //             select new ProductDto
                //             {
                //                 Id = product.Id,
                //                 Name = tenantXproductXlocale.Name,
                //                 Description = tenantXproductXlocale.Description,
                //                 Sku = product.SKU
                //             };


                //// TODO: Consolidate this into a single query with default locale
                //return result.ToList();
            }
        }

        #endregion
    }
}
