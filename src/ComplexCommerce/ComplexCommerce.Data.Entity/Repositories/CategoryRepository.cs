using System;
using System.Collections.Generic;
using System.Linq;
using ComplexCommerce.Data.Context;
using ComplexCommerce.Data.Repositories;
using ComplexCommerce.Data.Dto;
using ComplexCommerce.Data.Entity.Context;

namespace ComplexCommerce.Data.Entity.Repositories
{
    public class CategoryRepository
        : ICategoryRepository
    {
        public CategoryRepository(IPersistenceContextFactory contextFactory)
        {
            //Contract.Requires<ArgumentNullException>(contextFactory == null);
            if (contextFactory == null)
                throw new ArgumentNullException("contextFactory");

            this.contextFactory = contextFactory;
        }

        private readonly IPersistenceContextFactory contextFactory;

        #region ICategoryRepository Members

        public CategoryDto Fetch(Guid Id, int localeId)
        {
            using (var ctx = ((IEntityFrameworkObjectContext)contextFactory.GetContext()).ContextManager)
            {

                var result = (from category in ctx.ObjectContext.Category
                        join categoryLocale in ctx.ObjectContext.CategoryLocale
                            on category.Id equals categoryLocale.CategoryId
                        join page in ctx.ObjectContext.Page
                            on category.Id equals page.ContentId
                        join pageLocale in ctx.ObjectContext.PageLocale
                            on page.Id equals pageLocale.PageId
                            
                        where category.Id == Id
                        where categoryLocale.LocaleId == localeId
                        where pageLocale.LocaleId == localeId
                        where page.ContentType == 2 //category type

                        select new CategoryDto
                        {
                            Id = category.Id,
                            Title = pageLocale.Title,
                            Description = categoryLocale.Description,
                            MetaKeywords = pageLocale.MetaKeywords,
                            MetaDescription = pageLocale.MetaDescription
                        }).FirstOrDefault();
                            
                if (result == null)
                    throw new DataNotFoundException("Category");
                return result;
            }
        }


        // Category Product List
        //var c = (from category in ctx.ObjectContext.Category
        //                join categoryXproduct in ctx.ObjectContext.CategoryXProductXTenantLocale
        //                    on category.Id equals categoryXproduct.CategoryId
        //                join localizedProduct in ctx.ObjectContext.ProductXTenantLocale
        //                    on categoryXproduct.ProductXTenantLocaleId equals localizedProduct.Id
        //                    where category.TenantLocaleId == localizedProduct.TenantLocaleId
        //                    //on category.TenantLocaleId equals localizedProduct.TenantLocaleId
        //                join product in ctx.ObjectContext.Product
        //                    on localizedProduct.ProductId equals product.Id

        public IList<ProductCategoryDto> ListForProduct(Guid productId, int localeId)
        {
            using (var ctx = ((IEntityFrameworkObjectContext)contextFactory.GetContext()).ContextManager)
            {

                var result = (from categoryXProduct in ctx.ObjectContext.CategoryXProduct
                              join page in ctx.ObjectContext.Page
                                  on categoryXProduct.CategoryId equals page.ContentId
                              join pageLocale in ctx.ObjectContext.PageLocale
                                  on page.Id equals pageLocale.PageId
                              //where page.ContentType == 2 // Filter for categories only
                              where categoryXProduct.ProductId == productId
                              where pageLocale.LocaleId == localeId

                              select new ProductCategoryDto
                              {
                                  PageLocaleId = pageLocale.Id
                              });

                return result.ToList();
            }
        }


        #endregion
    }
}
