using System;
using Csla;
using ComplexCommerce.Csla;
using ComplexCommerce.Data.Repositories;
using ComplexCommerce.Data.Dto;

namespace ComplexCommerce.Business
{
    public interface ICategoryFactory
    {
        ICategory NewCategory();
        ICategory GetCategory(Guid categoryId);
    }

    public class CategoryFactory
        : ICategoryFactory
    {

        #region ICategoryFactory Members

        public ICategory NewCategory()
        {
            return Category.NewCategory();
        }

        public ICategory GetCategory(Guid categoryId)
        {
            return Category.GetCategory(categoryId);
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
        

        internal static Category GetCategory(Guid categoryId)
        {
            return DataPortal.Fetch<Category>(categoryId);
        }

        internal static Category NewCategory()
        {
            //return DataPortal.Create<Category>();
            return new Category();
        }


        private void DataPortal_Fetch(Guid categoryId)
        {
            using (var ctx = ContextFactory.GetContext())
            {
                var data = repository.Fetch(categoryId);

                if (data != null)
                {
                    Id = data.Id;
                    Title = data.Title;
                    Description = data.Description;
                    MetaKeywords = data.MetaKeywords;
                    MetaDescription = data.MetaDescription;
                    Products = DataPortal.FetchChild<CategoryProductList>(categoryId);
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
    }
}
