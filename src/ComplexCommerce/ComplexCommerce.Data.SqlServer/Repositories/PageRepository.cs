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
        private readonly IPersistenceContextFactory contextFactory;

        public PageRepository(IPersistenceContextFactory contextFactory)
        {
            Contract.Requires<ArgumentNullException>(contextFactory == null);

            this.contextFactory = contextFactory;
        }

        #region IPageRepository Members

        public List<SiteMapPageDto> List(int storeId, int localeId)
        {
            using (var ctx = ((IEntityFrameworkObjectContext)contextFactory.GetContext()).ContextManager)
            {
                var result = (from storeLocale in ctx.ObjectContext.StoreLocale
                             join page in ctx.ObjectContext.Page
                                 on storeLocale.Id equals page.StoreLocaleId
                             where storeLocale.StoreId == storeId
                             where storeLocale.LocaleId == localeId
                             select new SiteMapPageDto
                             {
                                 Id = page.Id,
                                 ParentId = page.ParentId == null ? Guid.Empty : (Guid)page.ParentId,
                                 RouteUrl = page.RouteUrl,
                                 ContentTypeId = page.ContentTypeId,
                                 ContentId = page.ContentId
                             }).ToList();

                if (result.Count == 0)
                {
                    (from storeLocale in ctx.ObjectContext.StoreLocale
                     join page in ctx.ObjectContext.Page
                         on storeLocale.Id equals page.StoreLocaleId
                     join store in ctx.ObjectContext.Store
                         on storeLocale.StoreId equals store.Id
                     where storeLocale.StoreId == storeId
                     where storeLocale.LocaleId == store.DefaultLocaleId
                     select new SiteMapPageDto
                     {
                         Id = page.Id,
                         ParentId = page.ParentId == null ? Guid.Empty : (Guid)page.ParentId,
                         RouteUrl = page.RouteUrl,
                         ContentTypeId = page.ContentTypeId,
                         ContentId = page.ContentId
                     }).ToList();
                }


                // TODO: Consolidate this into a single query with default locale
                return result.ToList();


                //var result = from product in ctx.ObjectContext.Product
                //             join storeXproductXlocale in ctx.ObjectContext.StoreXProductXLocale
                //                 on product.Id equals storeXproductXlocale.ProductId
                //             where storeXproductXlocale.StoreId == storeId
                //             where storeXproductXlocale.LocaleId == localeId
                //             select new ProductDto
                //             {
                //                 Id = product.Id,
                //                 Name = storeXproductXlocale.Name,
                //                 Description = storeXproductXlocale.Description,
                //                 Sku = product.SKU
                //             };


                //// TODO: Consolidate this into a single query with default locale
                //return result.ToList();
            }
        }

        #endregion
    }
}
