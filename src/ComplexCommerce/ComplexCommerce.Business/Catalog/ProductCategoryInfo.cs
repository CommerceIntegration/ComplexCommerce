using System;
using Csla;
using ComplexCommerce.Csla;
using ComplexCommerce.Data.Dto;

namespace ComplexCommerce.Business.Catalog
{
    public interface IProductCategory
    {
        Guid PageId { get; }
    }

    public class ProductCategoryInfo
        : CslaReadOnlyBase<ProductCategoryInfo>, IProductCategory
    {
        public static PropertyInfo<Guid> PageIdProperty = RegisterProperty<Guid>(c => c.PageId);
        public Guid PageId
        {
            get { return GetProperty(PageIdProperty); }
            private set { LoadProperty(PageIdProperty, value); }
        }

        private void Child_Fetch(ProductCategoryDto item)
        {
            this.PageId = item.PageId;
        }
    }
}
