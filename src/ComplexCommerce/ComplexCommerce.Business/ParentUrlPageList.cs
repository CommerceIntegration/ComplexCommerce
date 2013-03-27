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
        ParentUrlPageList GetParentUrlPageList();
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

        public ParentUrlPageList GetParentUrlPageList()
        {
            return ParentUrlPageList.GetRequestCachedParentUrlPageList(
                appContext.CurrentTenant.Id, appContext.CurrentLocaleId, appContext.CurrentTenant.DefaultLocale.LCID);
        }

        public ParentUrlPageList GetParentUrlPageList(int tenantId, int localeId, int defaultLocaleId)
        {
            return ParentUrlPageList.GetRequestCachedParentUrlPageList(tenantId, localeId, defaultLocaleId);
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

                var list = repository.ListForParentUrl(criteria.TenantId, criteria.LocaleId);

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
    }
}
