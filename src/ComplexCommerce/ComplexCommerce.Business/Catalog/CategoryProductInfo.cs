using System;
using Csla;
using ComplexCommerce.Csla;
using ComplexCommerce.Data.Dto;

namespace ComplexCommerce.Business.Catalog
{
    public interface ICategoryProduct
    {
        Guid CategoryXProductId { get; }
        string Name { get; }
        string SKU { get; }
        string ImageUrl { get; }
        Decimal Price { get; }
    }

    public class CategoryProductInfo
        : CslaReadOnlyBase<CategoryProductInfo>, ICategoryProduct
    {
        public static PropertyInfo<Guid> CategoryXProductIdProperty = RegisterProperty<Guid>(c => c.CategoryXProductId);
        public Guid CategoryXProductId
        {
            get { return GetProperty(CategoryXProductIdProperty); }
            private set { LoadProperty(CategoryXProductIdProperty, value); }
        }

        public static PropertyInfo<string> NameProperty = RegisterProperty<string>(c => c.Name);
        public string Name
        {
            get { return GetProperty(NameProperty); }
            private set { LoadProperty(NameProperty, value); }
        }

        public static PropertyInfo<string> SKUProperty = RegisterProperty<string>(c => c.SKU);
        public string SKU
        {
            get { return GetProperty(SKUProperty); }
            private set { LoadProperty(SKUProperty, value); }
        }

        public static PropertyInfo<string> ImageUrlProperty = RegisterProperty<string>(c => c.ImageUrl);
        public string ImageUrl
        {
            get { return GetProperty(ImageUrlProperty); }
            private set { LoadProperty(ImageUrlProperty, value); }
        }

        public static PropertyInfo<Decimal> PriceProperty = RegisterProperty<Decimal>(c => c.Price);
        public Decimal Price
        {
            get { return GetProperty(PriceProperty); }
            private set { LoadProperty(PriceProperty, value); }
        }

        private void Child_Fetch(CategoryProductDto item)
        {
            this.CategoryXProductId = item.CategoryXProductId;
            this.Name = item.Name;
            this.SKU = item.SKU;
            this.ImageUrl = item.ImageUrl;
            this.Price = item.Price;
        }

    }
}
