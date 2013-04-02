using System;
using System.ComponentModel;
using Csla;
using ComplexCommerce.Csla;
using ComplexCommerce.Data.Repositories;
using ComplexCommerce.Data.Dto;
using ComplexCommerce.Business.Context;

namespace ComplexCommerce.Business
{
    public interface IParentUrlPageListFactory
    {
        ParentUrlPageList EmptyParentUrlPageList();
        //ParentUrlPageList GetParentUrlPageList();
        ParentUrlPageList GetParentUrlPageList(int tenantId, int localeId, int defaultLocaleId);
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

        //public ParentUrlPageList GetParentUrlPageList()
        //{
        //    return ParentUrlPageList.GetRequestCachedParentUrlPageList(
        //        appContext.CurrentTenant.Id, appContext.CurrentLocaleId, appContext.CurrentTenant.DefaultLocale.LCID);
        //    //return ParentUrlPageList.GetParentUrlPageList(
        //    //    appContext.CurrentTenant.Id, appContext.CurrentLocaleId, appContext.CurrentTenant.DefaultLocale.LCID);
        //}

        public ParentUrlPageList GetParentUrlPageList(int tenantId, int localeId, int defaultLocaleId)
        {
            return ParentUrlPageList.GetRequestCachedParentUrlPageList(tenantId, localeId, defaultLocaleId);
            //return ParentUrlPageList.GetParentUrlPageList(tenantId, localeId, defaultLocaleId);
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

        internal static ParentUrlPageList GetParentUrlPageList(int tenantId, int localeId, int defaultLocaleId)
        {
            return DataPortal.Fetch<ParentUrlPageList>(new TenantLocaleCriteria(tenantId, localeId, defaultLocaleId));
        }

        internal static ParentUrlPageList GetRequestCachedParentUrlPageList(int tenantId, int localeId, int defaultLocaleId)
        {
            var cmd = new GetRequestCachedParentUrlPageListCommand(tenantId, localeId, defaultLocaleId);
            cmd = DataPortal.Execute<GetRequestCachedParentUrlPageListCommand>(cmd);
            return cmd.ParentUrlPageList;
        }

        private void DataPortal_Fetch(TenantLocaleCriteria criteria)
        {
            using (var ctx = ContextFactory.GetContext())
            {
                var rlce = RaiseListChangedEvents;
                RaiseListChangedEvents = false;
                IsReadOnly = false;

                //var list = repository.ListForParentUrl(criteria.TenantId, criteria.LocaleId);
                var list = repository.ListForParentUrl(criteria.TenantId);

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
            public GetRequestCachedParentUrlPageListCommand(int tenantId, int localeId, int defaultLocaleId)
            {
                if (tenantId < 1)
                    throw new ArgumentOutOfRangeException("tenantId");
                if (localeId < 1)
                    throw new ArgumentOutOfRangeException("localeId");
                if (defaultLocaleId < 1)
                    throw new ArgumentOutOfRangeException("defaultLocaleId");

                this.TenantId = tenantId;
                this.LocaleId = localeId;
                this.DefaultLocaleId = defaultLocaleId;
            }


            public static PropertyInfo<int> TenantIdProperty = RegisterProperty<int>(c => c.TenantId);
            public int TenantId
            {
                get { return ReadProperty(TenantIdProperty); }
                private set { LoadProperty(TenantIdProperty, value); }
            }

            public static PropertyInfo<int> LocaleIdProperty = RegisterProperty<int>(c => c.LocaleId);
            public int LocaleId
            {
                get { return ReadProperty(LocaleIdProperty); }
                private set { LoadProperty(LocaleIdProperty, value); }
            }

            public static PropertyInfo<int> DefaultLocaleIdProperty = RegisterProperty<int>(c => c.DefaultLocaleId);
            public int DefaultLocaleId
            {
                get { return ReadProperty(DefaultLocaleIdProperty); }
                private set { LoadProperty(DefaultLocaleIdProperty, value); }
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
                //var key = "__ML_ParentUrlPageList_" + this.TenantId + "_" + this.LocaleId + "__";
                var key = "__ML_ParentUrlPageList_" + this.TenantId + "__";
                this.ParentUrlPageList = appContext.GetOrAdd(key,
                    () => ParentUrlPageList.GetParentUrlPageList(this.TenantId, this.LocaleId, this.DefaultLocaleId));
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
