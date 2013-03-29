using System;
using Csla;
using ComplexCommerce.Csla;
using ComplexCommerce.Data.Dto;

namespace ComplexCommerce.Business
{
    public class CategoryProductInfo
        : CslaReadOnlyBase<CategoryProductInfo>
    {

        //public static PropertyInfo<Guid> ProductXTenantLocaleIdProperty = RegisterProperty<Guid>(c => c.ProductXTenantLocaleId);
        //public Guid ProductXTenantLocaleId
        //{
        //    get { return GetProperty(ProductXTenantLocaleIdProperty); }
        //    private set { LoadProperty(ProductXTenantLocaleIdProperty, value); }
        //}

        //public static PropertyInfo<Guid> CategoryXProductXTenantLocaleIdProperty = RegisterProperty<Guid>(c => c.CategoryXProductXTenantLocaleId);
        //public Guid CategoryXProductXTenantLocaleId
        //{
        //    get { return GetProperty(CategoryXProductXTenantLocaleIdProperty); }
        //    private set { LoadProperty(CategoryXProductXTenantLocaleIdProperty, value); }
        //}

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


        //public static PropertyInfo<string> RouteUrlProperty = RegisterProperty<string>(c => c.RouteUrl);
        //public string RouteUrl
        //{
        //    get { return GetProperty(RouteUrlProperty); }
        //    private set { LoadProperty(RouteUrlProperty, value); }
        //}


        private void Child_Fetch(CategoryProductDto item)
        {
            //this.ProductXTenantLocaleId = item.ProductXTenantLocaleId;
            //this.CategoryXProductXTenantLocaleId = item.CategoryXProductXTenantLocaleId;
            this.CategoryXProductId = item.CategoryXProductId;
            this.Name = item.Name;
            this.SKU = item.SKU;
            this.ImageUrl = item.ImageUrl;
            this.Price = item.Price;
        }

    }
}
