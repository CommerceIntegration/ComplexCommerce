using System;
using System.ComponentModel;
using Csla;
using ComplexCommerce.Csla;
using ComplexCommerce.Data.Repositories;
using ComplexCommerce.Data.Dto;
using ComplexCommerce.Business.Context;

namespace ComplexCommerce.Business.Routing
{
    public interface IParentUrlPageListFactory
    {
        ParentUrlPageList EmptyParentUrlPageList();
        ParentUrlPageList GetParentUrlPageList(int tenantId);
    }

    public class ParentUrlPageListFactory
        : IParentUrlPageListFactory
    {
        public ParentUrlPageListFactory(
            IApplicationContext appContext
            )
        {
            if (appContext == null)
                throw new ArgumentNullException("appContext");
            this.appContext = appContext;
        }

        private readonly IApplicationContext appContext;

        #region IParentUrlPageListFactory Members

        public ParentUrlPageList EmptyParentUrlPageList()
        {
            return ParentUrlPageList.EmptyParentUrlPageList();
        }

        public ParentUrlPageList GetParentUrlPageList(int tenantId)
        {
            return ParentUrlPageList.GetRequestCachedParentUrlPageList(tenantId);
        }

        #endregion
    }

    [Serializable]
    public class ParentUrlPageList
        : CslaReadOnlyListBase<ParentUrlPageList, ParentUrlPageInfo>
    {
        internal static ParentUrlPageList EmptyParentUrlPageList()
        {
            return new ParentUrlPageList();
        }

        internal static ParentUrlPageList GetParentUrlPageList(int tenantId)
        {
            return DataPortal.Fetch<ParentUrlPageList>(tenantId);
        }

        internal static ParentUrlPageList GetRequestCachedParentUrlPageList(int tenantId)
        {
            var cmd = new GetRequestCachedParentUrlPageListCommand(tenantId);
            cmd = DataPortal.Execute<GetRequestCachedParentUrlPageListCommand>(cmd);
            return cmd.ParentUrlPageList;
        }

        private void DataPortal_Fetch(int tenantId)
        {
            using (var ctx = ContextFactory.GetContext())
            {
                var rlce = RaiseListChangedEvents;
                RaiseListChangedEvents = false;
                IsReadOnly = false;

                var list = repository.ListForParentUrl(tenantId);

                foreach (var item in list)
                    Add(DataPortal.FetchChild<ParentUrlPageInfo>(item));

                IsReadOnly = true;
                RaiseListChangedEvents = rlce;
            }
        }

        #region Dependency Injection

        [NonSerialized]
        [NotUndoable]
        private IPageRepository repository;
        public IPageRepository Repository
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

        #region GetRequestCachedParentUrlPageListCommand

        [Serializable]
        private class GetRequestCachedParentUrlPageListCommand
            : CslaCommandBase<GetRequestCachedParentUrlPageListCommand>
        {
            public GetRequestCachedParentUrlPageListCommand(int tenantId)
            {
                if (tenantId < 1)
                    throw new ArgumentOutOfRangeException("tenantId");

                this.TenantId = tenantId;
            }

            public static PropertyInfo<int> TenantIdProperty = RegisterProperty<int>(c => c.TenantId);
            public int TenantId
            {
                get { return ReadProperty(TenantIdProperty); }
                private set { LoadProperty(TenantIdProperty, value); }
            }

            public static PropertyInfo<ParentUrlPageList> ParentUrlPageListProperty = RegisterProperty<ParentUrlPageList>(c => c.ParentUrlPageList);
            public ParentUrlPageList ParentUrlPageList
            {
                get { return ReadProperty(ParentUrlPageListProperty); }
                private set { LoadProperty(ParentUrlPageListProperty, value); }
            }

            /// <summary>
            /// We work with the cache on the server side of the DataPortal
            /// </summary>
            protected override void DataPortal_Execute()
            {
                var key = "__ML_ParentUrlPageList_" + this.TenantId + "__";
                this.ParentUrlPageList = appContext.GetOrAdd(key,
                    () => ParentUrlPageList.GetParentUrlPageList(this.TenantId));
            }

            #region Dependency Injection

            [NonSerialized]
            [NotUndoable]
            private Context.IApplicationContext appContext;
            public Context.IApplicationContext AppContext
            {
                set
                {
                    // Don't allow the value to be set to null
                    if (value == null)
                    {
                        throw new ArgumentNullException("value");
                    }
                    // Don't allow the value to be set more than once
                    if (this.appContext != null)
                    {
                        throw new InvalidOperationException();
                    }
                    this.appContext = value;
                }
            }

            #endregion
        }

        #endregion
    }
}
