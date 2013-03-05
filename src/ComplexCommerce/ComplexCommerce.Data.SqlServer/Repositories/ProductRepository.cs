﻿using System;
using System.Collections.Generic;
using System.Linq;
using ComplexCommerce.Data.Context;
using ComplexCommerce.Data.Repositories;
using ComplexCommerce.Data.Dto;
using ComplexCommerce.Data.SqlServer.Context;

namespace ComplexCommerce.Data.SqlServer.Repositories
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

        public IList<CategoryProductDto> ListForCategory(Guid categoryId)
        {
            using (var ctx = ((IEntityFrameworkObjectContext)contextFactory.GetContext()).ContextManager)
            {

                var result = (from categoryXProduct in ctx.ObjectContext.CategoryXProductXTenantLocale
                            join productXlocale in ctx.ObjectContext.ProductXTenantLocale
                                on categoryXProduct.ProductXTenantLocaleId equals productXlocale.Id
                            join product in ctx.ObjectContext.Product
                                on productXlocale.ProductId equals product.Id
                            where categoryXProduct.CategoryId == categoryId
                            select new CategoryProductDto
                            {
                                ProductXTenantLocaleId = categoryXProduct.ProductXTenantLocaleId,
                                Name = productXlocale.Name,
                                SKU = product.SKU,
                                ImageUrl = product.ImageUrl,
                                Price = product.Price
                            });

                return result.ToList();
            }
        }

        #endregion
    }
}