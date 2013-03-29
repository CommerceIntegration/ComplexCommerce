using System;
using System.Collections.Generic;
using System.Linq;
using ComplexCommerce.Data.Context;
using ComplexCommerce.Data.Repositories;
using ComplexCommerce.Data.Dto;
using ComplexCommerce.Data.Entity.Context;

namespace ComplexCommerce.Data.Entity.Repositories
{
    public class ProductRepository
        : IProductRepository
    {
        public ProductRepository(IPersistenceContextFactory contextFactory)
        {
            //Contract.Requires<ArgumentNullException>(contextFactory == null);
            if (contextFactory == null)
                throw new ArgumentNullException("contextFactory");

            this.contextFactory = contextFactory;
        }

        private readonly IPersistenceContextFactory contextFactory;

        #region IProductRepository Members

        public IList<CategoryProductDto> ListForCategory(Guid categoryId, int localeId)
        {
            using (var ctx = ((IEntityFrameworkObjectContext)contextFactory.GetContext()).ContextManager)
            {

                //var result = (from categoryXProduct in ctx.ObjectContext.CategoryXProductXTenantLocale
                //              join productXlocale in ctx.ObjectContext.ProductXTenantLocale
                //                  on categoryXProduct.ProductXTenantLocaleId equals productXlocale.Id
                //              join product in ctx.ObjectContext.Product
                //                  on productXlocale.ProductId equals product.Id
                //              where categoryXProduct.CategoryId == categoryId

                var result = (from categoryXProduct in ctx.ObjectContext.CategoryXProduct
                              join product in ctx.ObjectContext.Product
                                  on categoryXProduct.ProductId equals product.Id
                              join productXTenantXlocale in ctx.ObjectContext.ProductXTenantXLocale
                                  on product.Id equals productXTenantXlocale.ProductId
                              
                              where categoryXProduct.CategoryId == categoryId
                              where productXTenantXlocale.LocaleId == localeId
                              select new CategoryProductDto
                              {
                                  //CategoryXProductXTenantLocaleId = categoryXProduct.Id,
                                  CategoryXProductId = categoryXProduct.Id,
                                  Name = productXTenantXlocale.Name,
                                  SKU = product.SKU,
                                  ImageUrl = product.ImageUrl,
                                  Price = product.Price
                              });

                return result.ToList();
            }
        }

        public IList<RouteUrlProductDto> ListForRouteUrl(int tenantId, int localeId)
        {
            using (var ctx = ((IEntityFrameworkObjectContext)contextFactory.GetContext()).ContextManager)
            {

                //var result = (from categoryXProduct in ctx.ObjectContext.CategoryXProductXTenantLocale
                //              join productXlocale in ctx.ObjectContext.ProductXTenantLocale
                //                  on categoryXProduct.ProductXTenantLocaleId equals productXlocale.Id
                //              join tenantLocale in ctx.ObjectContext.TenantLocale
                //                  on productXlocale.TenantLocaleId equals tenantLocale.Id
                //              join parentPage in ctx.ObjectContext.Page
                //                  on categoryXProduct.CategoryId equals parentPage.ContentId
                //              where tenantLocale.TenantId == tenantId && tenantLocale.LocaleId == localeId
                //              //where parentPage.ContentType == 2

                var result = (from categoryXProduct in ctx.ObjectContext.CategoryXProduct
                              join product in ctx.ObjectContext.Product
                                  on categoryXProduct.ProductId equals product.Id
                              join productXTenantXlocale in ctx.ObjectContext.ProductXTenantXLocale
                                  on product.Id equals productXTenantXlocale.ProductId

                              //join productXlocale in ctx.ObjectContext.ProductXTenantLocale
                              //    on categoryXProduct.ProductXTenantLocaleId equals productXlocale.Id
                              //join tenantLocale in ctx.ObjectContext.TenantLocale
                              //    on productXlocale.TenantLocaleId equals tenantLocale.Id
                              join parentPage in ctx.ObjectContext.Page
                                  on categoryXProduct.CategoryId equals parentPage.ContentId
                              join parentPageLocale in ctx.ObjectContext.PageLocale
                                  on parentPage.Id equals parentPageLocale.PageId
                              //where tenantLocale.TenantId == tenantId && tenantLocale.LocaleId == localeId
                              //where parentPage.ContentType == 2

                              where productXTenantXlocale.TenantId == tenantId
                              where productXTenantXlocale.LocaleId == localeId
                              where parentPageLocale.LocaleId == localeId
                              
                              select new RouteUrlProductDto
                              {
                                  CategoryXProductXTenantLocaleId = categoryXProduct.Id, // TODO: Fix ID
                                  ParentId = parentPage.Id,
                                  //LocaleId = localeId, // TODO: Remove from DTO
                                  Url = productXTenantXlocale.Url,
                                  IsUrlAbsolute = productXTenantXlocale.IsUrlAbsolute
                                  //ParentPageUrl = parentPage.Url
                              });

                return result.ToList();
            }
        }

        public IList<SiteMapProductDto> ListForSiteMap(int tenantId, int localeId)
        {
            using (var ctx = ((IEntityFrameworkObjectContext)contextFactory.GetContext()).ContextManager)
            {

                //var result = (from categoryXProduct in ctx.ObjectContext.CategoryXProductXTenantLocale
                //              join productXlocale in ctx.ObjectContext.ProductXTenantLocale
                //                  on categoryXProduct.ProductXTenantLocaleId equals productXlocale.Id
                //              join tenantLocale in ctx.ObjectContext.TenantLocale
                //                  on productXlocale.TenantLocaleId equals tenantLocale.Id
                //              join parentPage in ctx.ObjectContext.Page
                //                  on categoryXProduct.CategoryId equals parentPage.ContentId
                //              join defaultCategoryPage in ctx.ObjectContext.Page
                //                  on productXlocale.DefaultCategoryId equals defaultCategoryPage.ContentId

                //              where tenantLocale.TenantId == tenantId && tenantLocale.LocaleId == localeId
                //              //where parentPage.ContentType == 2

                var result = (from categoryXProduct in ctx.ObjectContext.CategoryXProduct
                              join product in ctx.ObjectContext.Product
                                  on categoryXProduct.ProductId equals product.Id
                              join productXTenantXlocale in ctx.ObjectContext.ProductXTenantXLocale
                                  on product.Id equals productXTenantXlocale.ProductId
                              join parentPage in ctx.ObjectContext.Page
                                  on categoryXProduct.CategoryId equals parentPage.ContentId
                              join parentPageLocale in ctx.ObjectContext.PageLocale
                                  on parentPage.Id equals parentPageLocale.PageId
                              join defaultCategoryPage in ctx.ObjectContext.Page
                                on product.DefaultCategoryId equals defaultCategoryPage.ContentId

                              where productXTenantXlocale.TenantId == tenantId
                              where productXTenantXlocale.LocaleId == localeId
                              where parentPageLocale.LocaleId == localeId

                              select new SiteMapProductDto
                              {
                                  ////ProductXTenantLocaleId = categoryXProduct.ProductId, // TODO: Fix ID
                                  //ParentPageId = parentPage.Id,
                                  ////CategoryId = categoryXProduct.CategoryId,
                                  ////TenantId = tenantId, // TODO: Remove from DTO
                                  ////LocaleId = localeId, // TODO: Remove from DTO
                                  //Name = productXTenantXlocale.Name,
                                  //Url = productXTenantXlocale.Url,
                                  //IsUrlAbsolute = productXTenantXlocale.IsUrlAbsolute,
                                  ////MetaRobots = // TODO: Finish MetaRobots
                                  //DefaultCategoryPageId = defaultCategoryPage.Id


                                  CategoryXProductId = categoryXProduct.Id,
                                  CategoryId = categoryXProduct.CategoryId,
                                  ParentPageId = parentPage.Id,
                                  Name = productXTenantXlocale.Name,
                                  Url = productXTenantXlocale.Url,
                                  IsUrlAbsolute = productXTenantXlocale.IsUrlAbsolute,
                                  //MetaRobots = // TODO: Finish MetaRobots
                                  DefaultCategoryPageId = defaultCategoryPage.Id
                              });

                return result.ToList();
            }
        }

        public ProductDto Fetch(Guid categoryXProductId, int localeId)
        {
            using (var ctx = ((IEntityFrameworkObjectContext)contextFactory.GetContext()).ContextManager)
            {

                //var result = (from categoryXProductXTenantLocale in ctx.ObjectContext.CategoryXProductXTenantLocale
                //            join productXTenantLocale in ctx.ObjectContext.ProductXTenantLocale
                //                on categoryXProductXTenantLocale.ProductXTenantLocaleId equals productXTenantLocale.Id
                //            join product in ctx.ObjectContext.Product
                //                  on productXTenantLocale.ProductId equals product.Id


                //              //join categoryXProductXTenantLocale in ctx.ObjectContext.CategoryXProductXTenantLocale
                //              //      on productXTenantLocale.Id equals categoryXProductXTenantLocale.ProductXTenantLocaleId

                //              where categoryXProductXTenantLocale.Id == categoryXProductXTenantLocaleId
                //              //where productXTenantLocale.Id == productXTenantLocaleId


                var result = (from categoryXProduct in ctx.ObjectContext.CategoryXProduct
                              join product in ctx.ObjectContext.Product
                                    on categoryXProduct.ProductId equals product.Id
                              join productXTenantXLocale in ctx.ObjectContext.ProductXTenantXLocale
                                  on product.Id equals productXTenantXLocale.ProductId
                              
                              where categoryXProduct.Id == categoryXProductId
                              where productXTenantXLocale.LocaleId == localeId

                              select new ProductDto
                              {
                                  Id = product.Id,
                                  ProductXTenantLocaleId = productXTenantXLocale.Id, // TODO: Fix id
                                  Name = productXTenantXLocale.Name,
                                  Description = productXTenantXLocale.Description,
                                  MetaKeywords = productXTenantXLocale.MetaKeywords,
                                  MetaDescription = productXTenantXLocale.MetaDescription,
                                  SKU = product.SKU,
                                  ImageUrl = product.ImageUrl,
                                  Price = product.Price
                              }).FirstOrDefault();

                if (result == null)
                    throw new DataNotFoundException("Product");
                return result;
            }
        }

        #endregion
    }
}
