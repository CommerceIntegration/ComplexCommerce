using System;
using System.Collections.Generic;
using System.Linq;
using Csla;
using ComplexCommerce.Csla;
using ComplexCommerce.Data.Repositories;
using ComplexCommerce.Business.Caching;

namespace ComplexCommerce.Business.Globalization
{
    [Serializable]
    public class ProductLocaleList
        : CslaReadOnlyListBase<ProductLocaleList, ProductLocaleInfo>
    {
        //internal static ProductLocaleList EmptyProductLocaleList()
        //{
        //    return new ProductLocaleList();
        //}

        internal static ProductLocaleList GetProductLocaleList(Guid productId, int tenantId)
        {
            return DataPortal.Fetch<ProductLocaleList>(new Criteria(productId, tenantId));
        }

        public static ProductLocaleList GetCachedProductLocaleList(Guid productId, int tenantId)
        {
            var cmd = new GetCachedProductLocaleListCommand(productId, tenantId);
            cmd = DataPortal.Execute<GetCachedProductLocaleListCommand>(cmd);
            return cmd.ProductLocaleList;
        }

        private void DataPortal_Fetch(Criteria criteria)
        {
            using (var ctx = ContextFactory.GetContext())
            {
                var rlce = RaiseListChangedEvents;
                RaiseListChangedEvents = false;
                IsReadOnly = false;

                var list = repository.ListLocales(criteria.ProductId, criteria.TenantId);

                foreach (var item in list)
                    Add(DataPortal.FetchChild<ProductLocaleInfo>(item));

                IsReadOnly = true;
                RaiseListChangedEvents = rlce;
            }
        }

        #region Criteria

        [Serializable]
        private class Criteria
            : CriteriaBase<Criteria>
        {
            public Criteria(
                Guid productId,
                int tenantId)
            {
                if (productId == null)
                    throw new ArgumentNullException("productId");
                if (tenantId < 1)
                    throw new ArgumentOutOfRangeException("tenantId");

                this.ProductId = productId;
                this.TenantId = tenantId;
            }

            public static readonly PropertyInfo<Guid> ProductIdProperty = RegisterProperty<Guid>(c => c.ProductId);
            public Guid ProductId
            {
                get { return ReadProperty(ProductIdProperty); }
                private set { LoadProperty(ProductIdProperty, value); }
            }

            public static readonly PropertyInfo<int> TenantIdProperty = RegisterProperty<int>(c => c.TenantId);
            public int TenantId
            {
                get { return ReadProperty(TenantIdProperty); }
                private set { LoadProperty(TenantIdProperty, value); }
            }
        }
           
        #endregion

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

        #region GetCachedProductLocaleListCommand

        [Serializable]
        private class GetCachedProductLocaleListCommand
            : CslaCommandBase<GetCachedProductLocaleListCommand>
        {
            public GetCachedProductLocaleListCommand(Guid productId, int tenantId)
            {
                if (productId == null)
                    throw new ArgumentNullException("productId");
                if (tenantId < 1)
                    throw new ArgumentOutOfRangeException("tenantId");

                this.ProductId = productId;
                this.TenantId = tenantId;
            }


            public static PropertyInfo<Guid> ProductIdProperty = RegisterProperty<Guid>(c => c.ProductId);
            public Guid ProductId
            {
                get { return ReadProperty(ProductIdProperty); }
                private set { LoadProperty(ProductIdProperty, value); }
            }

            public static PropertyInfo<int> TenantIdProperty = RegisterProperty<int>(c => c.TenantId);
            public int TenantId
            {
                get { return ReadProperty(TenantIdProperty); }
                private set { LoadProperty(TenantIdProperty, value); }
            }

            public static PropertyInfo<ProductLocaleList> ProductLocaleListProperty = RegisterProperty<ProductLocaleList>(c => c.ProductLocaleList);
            public ProductLocaleList ProductLocaleList
            {
                get { return ReadProperty(ProductLocaleListProperty); }
                private set { LoadProperty(ProductLocaleListProperty, value); }
            }

            /// <summary>
            /// We work with the cache on the server side of the DataPortal
            /// </summary>
            protected override void DataPortal_Execute()
            {
                var key = "__ML_ProductLocaleList_" + this.ProductId.ToString() + "_" + this.TenantId.ToString() + "__";
                this.ProductLocaleList = cache.GetOrAdd(key,
                    () => ProductLocaleList.GetProductLocaleList(this.ProductId, this.TenantId));
            }

            #region Dependency Injection

            [NonSerialized]
            [NotUndoable]
            private IMicroObjectCache<ProductLocaleList> cache;
            public IMicroObjectCache<ProductLocaleList> Cache
            {
                set
                {
                    // Don't allow the value to be set to null
                    if (value == null)
                    {
                        throw new ArgumentNullException("value");
                    }
                    // Don't allow the value to be set more than once
                    if (this.cache != null)
                    {
                        throw new InvalidOperationException();
                    }
                    this.cache = value;
                }
            }

            #endregion
        }

        #endregion
    }
}
