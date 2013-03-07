using System;
using Csla;
using ComplexCommerce.Csla;
using ComplexCommerce.Data.Repositories;
using ComplexCommerce.Data.Dto;

namespace ComplexCommerce.Business
{
    public interface IProductFactory
    {
        IProduct NewProduct();
        IProduct GetProduct(Guid productXTenantLocaleId);
    }

    public class ProductFactory
        : IProductFactory
    {

        #region ICategoryFactory Members

        public IProduct NewProduct()
        {
            return Product.NewProduct();
        }

        public IProduct GetProduct(Guid productXTenantLocaleId)
        {
            return Product.GetProduct(productXTenantLocaleId);
        }

        #endregion
    }

    public interface IProduct
    {
        Guid Id { get; }
        string Name { get; }
        string Description { get; }
        string MetaKeywords { get; } // Move to another interface that is inherited?
        string MetaDescription { get; }
        string SKU { get; }
        string ImageUrl { get; }
        decimal Price { get; }
        string DefaultCategoryRouteUrl { get; }
        string CanonicalRouteUrl { get; }
    }

    [Serializable]
    public class Product
        : CslaReadOnlyBase<Product>, IProduct
    {
        public static readonly PropertyInfo<Guid> IdProperty = RegisterProperty<Guid>(p => p.Id);
        public Guid Id
        {
            get { return GetProperty(IdProperty); }
            private set { LoadProperty(IdProperty, value); }
        }

        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(p => p.Name);
        public string Name
        {
            get { return GetProperty(NameProperty); }
            private set { LoadProperty(NameProperty, value); }
        }

        public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(p => p.Description);
        public string Description
        {
            get { return GetProperty(DescriptionProperty); }
            private set { LoadProperty(DescriptionProperty, value); }
        }

        public static readonly PropertyInfo<string> MetaKeywordsProperty = RegisterProperty<string>(p => p.MetaKeywords);
        public string MetaKeywords
        {
            get { return GetProperty(MetaKeywordsProperty); }
            private set { LoadProperty(MetaKeywordsProperty, value); }
        }

        public static readonly PropertyInfo<string> MetaDescriptionProperty = RegisterProperty<string>(p => p.MetaDescription);
        public string MetaDescription
        {
            get { return GetProperty(MetaDescriptionProperty); }
            private set { LoadProperty(MetaDescriptionProperty, value); }
        }

        public static readonly PropertyInfo<string> SKUProperty = RegisterProperty<string>(p => p.SKU);
        public string SKU
        {
            get { return GetProperty(SKUProperty); }
            private set { LoadProperty(SKUProperty, value); }
        }

        public static readonly PropertyInfo<string> ImageUrlProperty = RegisterProperty<string>(p => p.ImageUrl);
        public string ImageUrl
        {
            get { return GetProperty(ImageUrlProperty); }
            private set { LoadProperty(ImageUrlProperty, value); }
        }

        public static readonly PropertyInfo<decimal> PriceProperty = RegisterProperty<decimal>(p => p.Price);
        public decimal Price
        {
            get { return GetProperty(PriceProperty); }
            private set { LoadProperty(PriceProperty, value); }
        }

        public static readonly PropertyInfo<string> DefaultCategoryRouteUrlProperty = RegisterProperty<string>(p => p.DefaultCategoryRouteUrl);
        public string DefaultCategoryRouteUrl
        {
            get { return GetProperty(DefaultCategoryRouteUrlProperty); }
            private set { LoadProperty(DefaultCategoryRouteUrlProperty, value); }
        }

        // TODO: Eliminate this property here and populate the canonical URL when building the SiteMapTree
        // This can eliminate an unnecessary join when users are browsing the product pages.
        public static readonly PropertyInfo<string> CanonicalRouteUrlProperty = RegisterProperty<string>(p => p.CanonicalRouteUrl);
        public string CanonicalRouteUrl
        {
            get { return GetProperty(CanonicalRouteUrlProperty); }
            private set { LoadProperty(CanonicalRouteUrlProperty, value); }
        }


        internal static IProduct GetProduct(Guid productXTenantLocaleId)
        {
            return DataPortal.Fetch<Product>(productXTenantLocaleId);
        }

        internal static IProduct NewProduct()
        {
            //return DataPortal.Create<Product>();
            return new Product();
        }


        private void DataPortal_Fetch(Guid productXTenantLocaleId)
        {
            using (var ctx = ContextFactory.GetContext())
            {
                var data = repository.Fetch(productXTenantLocaleId);

                if (data != null)
                {
                    Id = data.Id;
                    Name = data.Name;
                    Description = data.Description;
                    MetaKeywords = data.MetaKeywords;
                    MetaDescription = data.MetaDescription;
                    SKU = data.SKU;
                    ImageUrl = data.ImageUrl;
                    Price = data.Price;
                    DefaultCategoryRouteUrl = data.DefaultCategoryRouteUrl;
                }
            }

            BusinessRules.CheckRules();
        }


        #region Dependency Injection

        [NonSerialized]
        [NotUndoable]
        private IProductRepository repository;
        public IProductRepository Repository
        {
            set
            {
                // Don't allow the value to be set to null
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                // Don't allow the value to be set more than once
                if (this.repository != null)
                {
                    throw new InvalidOperationException();
                }
                this.repository = value;
            }
        }

        #endregion
    }
}
