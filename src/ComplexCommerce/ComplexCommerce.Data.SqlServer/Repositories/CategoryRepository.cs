using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComplexCommerce.Data.Context;
using ComplexCommerce.Data.Repositories;
using ComplexCommerce.Data.Dto;
using ComplexCommerce.Data.SqlServer.Context;

namespace ComplexCommerce.Data.SqlServer.Repositories
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

        public CategoryDto Fetch(Guid Id)
        {
            using (var ctx = ((IEntityFrameworkObjectContext)contextFactory.GetContext()).ContextManager)
            {

                var result = (from category in ctx.ObjectContext.Category
                        join page in ctx.ObjectContext.Page
                            on category.Id equals page.ContentId
                            
                        where category.Id == Id
                        where page.ContentType == 2 //category type

                        select new CategoryDto
                        {
                            Id = category.Id,
                            Title = page.Title,
                            Description = category.Description,
                            MetaKeywords = page.MetaKeywords,
                            MetaDescription = page.MetaDescription
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

        #endregion
    }
}
