//using System;
//using System.Collections.Generic;
//using System.Linq;
//using ComplexCommerce.Data.Context;
//using ComplexCommerce.Data.Repositories;
//using ComplexCommerce.Data.Dto;
//using ComplexCommerce.Data.SqlServer.Context;

//namespace ComplexCommerce.Data.SqlServer.Repositories
//{
//    public class ProductRepository
//        : IProductRepository
//    {
//        public ProductRepository(IPersistenceContextFactory contextFactory)
//        {
//            //Contract.Requires<ArgumentNullException>(contextFactory == null);
//            if (contextFactory == null)
//                throw new ArgumentNullException("contextFactory");

//            this.contextFactory = contextFactory;
//        }

//        private readonly IPersistenceContextFactory contextFactory;

//        #region IProductRepository Members

//        public IList<CategoryProductDto> ListForCategory(Guid categoryId)
//        {
//            using (var ctx = ((IEntityFrameworkObjectContext)contextFactory.GetContext()).ContextManager)
//            {

//                var result = (from categoryXProduct in ctx.ObjectContext.CategoryXProductXTenantLocale
//                              join productXlocale in ctx.ObjectContext.ProductXTenantLocale
//                                  on categoryXProduct.ProductXTenantLocaleId equals productXlocale.Id
//                              join product in ctx.ObjectContext.Product
//                                  on productXlocale.ProductId equals product.Id
//                              where categoryXProduct.CategoryId == categoryId
//                              select new CategoryProductDto
//                              {
//                                  CategoryXProductXTenantLocaleId = categoryXProduct.Id,
//                                  Name = productXlocale.Name,
//                                  SKU = product.SKU,
//                                  ImageUrl = product.ImageUrl,
//                                  Price = product.Price
//                              });

//                return result.ToList();
//            }
//        }

//        public IList<RouteUrlProductDto> ListForRouteUrl(int tenantId, int localeId)
//        {
//            using (var ctx = ((IEntityFrameworkObjectContext)contextFactory.GetContext()).ContextManager)
//            {

//                var result = (from categoryXProduct in ctx.ObjectContext.CategoryXProductXTenantLocale
//                              join productXlocale in ctx.ObjectContext.ProductXTenantLocale
//                                  on categoryXProduct.ProductXTenantLocaleId equals productXlocale.Id
//                              join tenantLocale in ctx.ObjectContext.TenantLocale
//                                  on productXlocale.TenantLocaleId equals tenantLocale.Id
//                              join parentPage in ctx.ObjectContext.Page
//                                  on categoryXProduct.CategoryId equals parentPage.ContentId
//                              where tenantLocale.TenantId == tenantId && tenantLocale.LocaleId == localeId
//                              //where parentPage.ContentType == 2
//                              select new RouteUrlProductDto
//                              {
//                                  CategoryXProductXTenantLocaleId = categoryXProduct.Id,
//                                  ParentId = parentPage.Id,
//                                  LocaleId = localeId,
//                                  Url = productXlocale.Url,
//                                  IsUrlAbsolute = productXlocale.IsUrlAbsolute
//                                  //ParentPageUrl = parentPage.Url
//                              });

//                return result.ToList();
//            }
//        }

//        public IList<SiteMapProductDto> ListForSiteMap(int tenantId, int localeId)
//        {
//            using (var ctx = ((IEntityFrameworkObjectContext)contextFactory.GetContext()).ContextManager)
//            {

//                var result = (from categoryXProduct in ctx.ObjectContext.CategoryXProductXTenantLocale
//                              join productXlocale in ctx.ObjectContext.ProductXTenantLocale
//                                  on categoryXProduct.ProductXTenantLocaleId equals productXlocale.Id
//                              join tenantLocale in ctx.ObjectContext.TenantLocale
//                                  on productXlocale.TenantLocaleId equals tenantLocale.Id
//                              join parentPage in ctx.ObjectContext.Page
//                                  on categoryXProduct.CategoryId equals parentPage.ContentId
//                              join defaultCategoryPage in ctx.ObjectContext.Page
//                                  on productXlocale.DefaultCategoryId equals defaultCategoryPage.ContentId

//                              where tenantLocale.TenantId == tenantId && tenantLocale.LocaleId == localeId
//                              //where parentPage.ContentType == 2

//                              select new SiteMapProductDto
//                              {
//                                  ProductXTenantLocaleId = categoryXProduct.ProductXTenantLocaleId,
//                                  ParentPageId = parentPage.Id,
//                                  CategoryId = categoryXProduct.CategoryId,
//                                  TenantId = tenantId,
//                                  LocaleId = localeId,
//                                  Name = productXlocale.Name,
//                                  Url = productXlocale.Url,
//                                  IsUrlAbsolute = productXlocale.IsUrlAbsolute,
//                                  //ParentPageUrl = page.Url,
//                                  //MetaRobots = // TODO: Finish MetaRobots
//                                  //DefaultCategoryUrl = defaultCategory.Url
//                                  DefaultCategoryPageId = defaultCategoryPage.Id
//                              });

//                return result.ToList();
//            }
//        }

//        public ProductDto Fetch(Guid categoryXProductXTenantLocaleId)
//        {
//            using (var ctx = ((IEntityFrameworkObjectContext)contextFactory.GetContext()).ContextManager)
//            {

//                var result = (from categoryXProductXTenantLocale in ctx.ObjectContext.CategoryXProductXTenantLocale
//                            join productXTenantLocale in ctx.ObjectContext.ProductXTenantLocale
//                                on categoryXProductXTenantLocale.ProductXTenantLocaleId equals productXTenantLocale.Id
//                            join product in ctx.ObjectContext.Product
//                                  on productXTenantLocale.ProductId equals product.Id


//                              //join categoryXProductXTenantLocale in ctx.ObjectContext.CategoryXProductXTenantLocale
//                              //      on productXTenantLocale.Id equals categoryXProductXTenantLocale.ProductXTenantLocaleId

//                              where categoryXProductXTenantLocale.Id == categoryXProductXTenantLocaleId
//                              //where productXTenantLocale.Id == productXTenantLocaleId

//                              select new ProductDto
//                              {
//                                  Id = product.Id,
//                                  ProductXTenantLocaleId = productXTenantLocale.Id,
//                                  Name = productXTenantLocale.Name,
//                                  Description = productXTenantLocale.Description,
//                                  MetaKeywords = productXTenantLocale.MetaKeywords,
//                                  MetaDescription = productXTenantLocale.MetaDescription,
//                                  SKU = product.SKU,
//                                  ImageUrl = product.ImageUrl,
//                                  Price = product.Price
//                              }).FirstOrDefault();

//                if (result == null)
//                    throw new DataNotFoundException("Product");
//                return result;
//            }
//        }

//        #endregion
//    }
//}
