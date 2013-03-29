using System;
using Csla;
using ComplexCommerce.Csla;
using ComplexCommerce.Data.Repositories;
using ComplexCommerce.Data.Dto;
using ComplexCommerce.Business.Context;

namespace ComplexCommerce.Business
{
    public interface ICategoryFactory
    {
        ICategory NewCategory();
        ICategory GetCategory(Guid categoryId);
        ICategory GetCategory(Guid categoryId, int localeId);
    }

    public class CategoryFactory
        : ICategoryFactory
    {
        public CategoryFactory(
            IApplicationContext appContext
            )
        {
            if (appContext == null)
                throw new ArgumentNullException("appContext");
            this.appContext = appContext;
        }

        private readonly IApplicationContext appContext;

        #region ICategoryFactory Members

        public ICategory NewCategory()
        {
            return Category.NewCategory();
        }

        public ICategory GetCategory(Guid categoryId)
        {
            return GetCategory(categoryId, appContext.CurrentLocaleId);
        }

        public ICategory GetCategory(Guid categoryId, int localeId)
        {
            return Category.GetCategory(categoryId, localeId);
        }

        #endregion
    }

    public interface ICategory
    {
        Guid Id { get; }
        string Title { get; }
        string Description { get; }
        string MetaKeywords { get; } // Move to another interface that is inherited?
        string MetaDescription { get; }
    }

    [Serializable]
    public class Category
        : CslaReadOnlyBase<Category>, ICategory
    {
        internal static Category GetCategory(Guid categoryId, int localeId)
        {
            return DataPortal.Fetch<Category>(new Criteria(categoryId, localeId));
        }

        internal static Category NewCategory()
        {
            //return DataPortal.Create<Category>();
            return new Category();
        }

        public static readonly PropertyInfo<Guid> IdProperty = RegisterProperty<Guid>(p => p.Id);
        public Guid Id
        {
            get { return GetProperty(IdProperty); }
            private set { LoadProperty(IdProperty, value); }
        }

        public static readonly PropertyInfo<string> TitleProperty = RegisterProperty<string>(p => p.Title);
        public string Title
        {
            get { return GetProperty(TitleProperty); }
            private set { LoadProperty(TitleProperty, value); }
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

        public static readonly PropertyInfo<CategoryProductList> ProductsProperty = RegisterProperty<CategoryProductList>(c => c.Products);
        public CategoryProductList Products
        {
            get
            {
                //if (!(FieldManager.FieldExists(ProductsProperty)))
                //    LoadProperty(ProductsProperty, DataPortal.CreateChild<CategoryProductList>());
                return GetProperty(ProductsProperty);
            }
            private set { LoadProperty(ProductsProperty, value); }
        }
        




        private void DataPortal_Fetch(Criteria criteria)
        {
            using (var ctx = ContextFactory.GetContext())
            {
                var data = repository.Fetch(criteria.CategoryId, criteria.LocaleId);

                if (data != null)
                {
                    Id = data.Id;
                    Title = data.Title;
                    Description = data.Description;
                    MetaKeywords = data.MetaKeywords;
                    MetaDescription = data.MetaDescription;
                    Products = DataPortal.FetchChild<CategoryProductList>(criteria.CategoryId, criteria.LocaleId);
                }
            }
        }


        #region Dependency Injection

        [NonSerialized]
        [NotUndoable]
        private ICategoryRepository repository;
        public ICategoryRepository Repository
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
            public Criteria(Guid categoryId, int localeId)
            {
                this.CategoryId = categoryId;
                this.LocaleId = localeId;
            }

            public static readonly PropertyInfo<Guid> CategoryIdProperty = RegisterProperty<Guid>(p => p.CategoryId);
            public Guid CategoryId
            {
                get { return ReadProperty(CategoryIdProperty); }
                private set { LoadProperty(CategoryIdProperty, value); }
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
