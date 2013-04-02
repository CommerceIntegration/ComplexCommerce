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
                var result = (from categoryXProduct in ctx.ObjectContext.CategoryXProduct
                              join product in ctx.ObjectContext.Product
                                  on categoryXProduct.ProductId equals product.Id
                              join productXTenantXlocale in ctx.ObjectContext.ProductXTenantXLocale
                                  on product.Id equals productXTenantXlocale.ProductId
                              
                              where categoryXProduct.CategoryId == categoryId
                              where productXTenantXlocale.LocaleId == localeId
                              select new CategoryProductDto
                              {
                                  CategoryXProductId = categoryXProduct.Id,
                                  Name = productXTenantXlocale.Name,
                                  SKU = product.SKU,
                                  ImageUrl = product.ImageUrl,
                                  Price = product.Price
                              });

                return result.ToList();
            }
        }

        public IList<RouteUrlProductDto> ListForRouteUrl(int tenantId)
        {
            using (var ctx = ((IEntityFrameworkObjectContext)contextFactory.GetContext()).ContextManager)
            {
                var result = (from categoryXProduct in ctx.ObjectContext.CategoryXProduct
                              join product in ctx.ObjectContext.Product
                                  on categoryXProduct.ProductId equals product.Id
                              join productXTenantXlocale in ctx.ObjectContext.ProductXTenantXLocale
                                  on product.Id equals productXTenantXlocale.ProductId
                              join parentPage in ctx.ObjectContext.Page
                                  on categoryXProduct.CategoryId equals parentPage.ContentId
                              //join parentPageLocale in ctx.ObjectContext.PageLocale
                              //    on parentPage.Id equals parentPageLocale.PageId

                              where productXTenantXlocale.TenantId == tenantId

                              select new RouteUrlProductDto
                              {
                                  CategoryXProductId = categoryXProduct.Id, // TODO: Fix ID
                                  ParentId = parentPage.Id,
                                  Url = productXTenantXlocale.Url,
                                  IsUrlAbsolute = productXTenantXlocale.IsUrlAbsolute,
                                  LocaleId = productXTenantXlocale.LocaleId
                              });

                return result.ToList();
            }
        }

        //public IList<RouteUrlProductDto> ListForRouteUrl(int tenantId, int localeId)
        //{
        //    using (var ctx = ((IEntityFrameworkObjectContext)contextFactory.GetContext()).ContextManager)
        //    {
        //        var result = (from categoryXProduct in ctx.ObjectContext.CategoryXProduct
        //                      join product in ctx.ObjectContext.Product
        //                          on categoryXProduct.ProductId equals product.Id
        //                      join productXTenantXlocale in ctx.ObjectContext.ProductXTenantXLocale
        //                          on product.Id equals productXTenantXlocale.ProductId
        //                      join parentPage in ctx.ObjectContext.Page
        //                          on categoryXProduct.CategoryId equals parentPage.ContentId
        //                      join parentPageLocale in ctx.ObjectContext.PageLocale
        //                          on parentPage.Id equals parentPageLocale.PageId

        //                      where productXTenantXlocale.TenantId == tenantId
        //                      where productXTenantXlocale.LocaleId == localeId
        //                      where parentPageLocale.LocaleId == localeId
                              
        //                      select new RouteUrlProductDto
        //                      {
        //                          CategoryXProductId = categoryXProduct.Id, // TODO: Fix ID
        //                          ParentId = parentPage.Id,
        //                          Url = productXTenantXlocale.Url,
        //                          IsUrlAbsolute = productXTenantXlocale.IsUrlAbsolute
        //                      });

        //        return result.ToList();
        //    }
        //}

        public IList<SiteMapProductDto> ListForSiteMap(int tenantId, int localeId)
        {
            using (var ctx = ((IEntityFrameworkObjectContext)contextFactory.GetContext()).ContextManager)
            {
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
                                  CategoryXProductId = categoryXProduct.Id,
                                  CategoryId = categoryXProduct.CategoryId,
                                  ParentPageId = parentPage.Id,
                                  Name = productXTenantXlocale.Name,
                                  Url = productXTenantXlocale.Url,
                                  IsUrlAbsolute = productXTenantXlocale.IsUrlAbsolute,
                                  MetaRobots = product.MetaRobots,
                                  DefaultCategoryPageId = defaultCategoryPage.Id
                              });

                return result.ToList();
            }
        }

        public IList<ProductLocaleDto> ListLocales(Guid productId, int tenantId)
        {
            using (var ctx = ((IEntityFrameworkObjectContext)contextFactory.GetContext()).ContextManager)
            {
                var result = (from productXTenantXLocale in ctx.ObjectContext.ProductXTenantXLocale
                              where productXTenantXLocale.ProductId == productId
                              where productXTenantXLocale.TenantId == tenantId
                              select new ProductLocaleDto
                              {
                                  LocaleId = productXTenantXLocale.LocaleId
                              });

                return result.ToList();
            }
        }

        public ProductDto Fetch(Guid categoryXProductId, int localeId)
        {
            using (var ctx = ((IEntityFrameworkObjectContext)contextFactory.GetContext()).ContextManager)
            {
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
                                  TenantId = productXTenantXLocale.TenantId,
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
