using System;
using System.Collections.Generic;
using Csla;
using ComplexCommerce.Csla;
using ComplexCommerce.Data.Repositories;
using ComplexCommerce.Data.Dto;

namespace ComplexCommerce.Business.Catalog
{
    [Serializable]
    public class CategoryProductList
        : CslaReadOnlyListBase<CategoryProductList, CategoryProductInfo>
    {

        private void Child_Fetch(Guid categoryId, int localeId)
        {
            using (var ctx = ContextFactory.GetContext())
            {
                var rlce = RaiseListChangedEvents;
                RaiseListChangedEvents = false;
                IsReadOnly = false;

                var list = repository.ListForCategory(categoryId, localeId);
                foreach (var item in list)
                    Add(DataPortal.FetchChild<CategoryProductInfo>(item));

                IsReadOnly = true;
                RaiseListChangedEvents = rlce;
            }
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
