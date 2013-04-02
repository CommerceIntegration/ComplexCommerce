using System;
using System.Collections.Generic;
using Csla;
using ComplexCommerce.Csla;
using ComplexCommerce.Data.Repositories;
using ComplexCommerce.Data.Dto;
using ComplexCommerce.Business.Context;

namespace ComplexCommerce.Business.Catalog
{
    public interface IProductFactory
    {
        IProduct NewProduct();
        IProduct GetProduct(Guid categoryXProductId);
        IProduct GetProduct(Guid categoryXProductId, int localeId);
    }

    public class ProductFactory
        : IProductFactory
    {
        public ProductFactory(
            IApplicationContext appContext
            )
        {
            if (appContext == null)
                throw new ArgumentNullException("appContext");
            this.appContext = appContext;
        }

        private readonly IApplicationContext appContext;

        #region ICategoryFactory Members

        public IProduct NewProduct()
        {
            return Product.NewProduct();
        }

        public IProduct GetProduct(Guid categoryXProductId)
        {
            return GetProduct(categoryXProductId, appContext.CurrentLocaleId);
        }

        public IProduct GetProduct(Guid categoryXProductId, int localeId)
        {
            return Product.GetProduct(categoryXProductId, localeId);
        }

        #endregion
    }

    public interface IProduct
    {
        Guid Id { get; }
        int LocaleId { get; }
        int TenantId { get; }
        string Name { get; }
        string Description { get; }
        string MetaKeywords { get; } // Move to another interface that is inherited?
        string MetaDescription { get; }
        string SKU { get; }
        string ImageUrl { get; }
        decimal Price { get; }
        IEnumerable<IProductCategory> Categories { get; }
    }

    [Serializable]
    public class Product
        : CslaReadOnlyBase<Product>, IProduct
    {
        internal static IProduct GetProduct(Guid categoryXProductId, int localeId)
        {
            return DataPortal.Fetch<Product>(new Criteria(categoryXProductId, localeId));
        }

        internal static IProduct NewProduct()
        {
            //return DataPortal.Create<Product>();
            return new Product();
        }

        public static readonly PropertyInfo<Guid> IdProperty = RegisterProperty<Guid>(p => p.Id);
        public Guid Id
        {
            get { return GetProperty(IdProperty); }
            private set { LoadProperty(IdProperty, value); }
        }

        public static readonly PropertyInfo<int> LocaleIdProperty = RegisterProperty<int>(p => p.LocaleId);
        public int LocaleId
        {
            get { return GetProperty(LocaleIdProperty); }
            private set { LoadProperty(LocaleIdProperty, value); }
        }

        public static readonly PropertyInfo<int> TenantIdProperty = RegisterProperty<int>(p => p.TenantId);
        public int TenantId
        {
            get { return GetProperty(TenantIdProperty); }
            private set { LoadProperty(TenantIdProperty, value); }
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

        public static readonly PropertyInfo<IEnumerable<IProductCategory>> CategoriesProperty = RegisterProperty<IEnumerable<IProductCategory>>(p => p.Categories);
        public IEnumerable<IProductCategory> Categories
        {
            get { return GetProperty(CategoriesProperty); }
            private set { LoadProperty(CategoriesProperty, value); }
        }



        private void DataPortal_Fetch(Criteria criteria)
        {
            using (var ctx = ContextFactory.GetContext())
            {
                var data = repository.Fetch(criteria.CategoryXProductId, criteria.LocaleId);

                if (data != null)
                {
                    Id = data.Id;
                    LocaleId = criteria.LocaleId;
                    TenantId = data.TenantId;
                    Name = data.Name;
                    Description = data.Description;
                    MetaKeywords = data.MetaKeywords;
                    MetaDescription = data.MetaDescription;
                    SKU = data.SKU;
                    ImageUrl = data.ImageUrl;
                    Price = data.Price;

                    Categories = DataPortal.FetchChild<ProductCategoryList>(data.Id, criteria.LocaleId);
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

        [Serializable]
        private class Criteria
            : CriteriaBase<Criteria>
        {
            public Criteria(Guid categoryXProductId, int localeId)
            {
                this.CategoryXProductId = categoryXProductId;
                this.LocaleId = localeId;
            }

            public static readonly PropertyInfo<Guid> CategoryXProductIdProperty = RegisterProperty<Guid>(p => p.CategoryXProductId);
            public Guid CategoryXProductId
            {
                get { return ReadProperty(CategoryXProductIdProperty); }
                private set { LoadProperty(CategoryXProductIdProperty, value); }
            }

            public static readonly PropertyInfo<int> LocaleIdProperty = RegisterProperty<int>(p => p.LocaleId);
            public int LocaleId
            {
                get { return ReadProperty(LocaleIdProperty); }
                private set { LoadProperty(LocaleIdProperty, value); }
            }
        }
    }
}
