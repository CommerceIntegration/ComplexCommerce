using System;
using System.ComponentModel;
using Csla;
using ComplexCommerce.Csla;
using ComplexCommerce.Data.Repositories;
using ComplexCommerce.Data.Dto;

namespace ComplexCommerce.Business
{
    public interface IParentUrlPageListFactory
    {
        ParentUrlPageList EmptyParentUrlPageList();
        ParentUrlPageList GetParentUrlPageList(int tenantId, int localeId);
    }

    public class ParentUrlPageListFactory
        : IParentUrlPageListFactory
    {

        #region IParentUrlPageListFactory Members

        public ParentUrlPageList EmptyParentUrlPageList()
        {
            return ParentUrlPageList.EmptyParentUrlPageList();
        }

        public ParentUrlPageList GetParentUrlPageList(int tenantId, int localeId)
        {
            return ParentUrlPageList.GetRequestCachedParentUrlPageList(tenantId, localeId);
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

        internal static ParentUrlPageList GetParentUrlPageList(int tenantId, int localeId)
        {
            return DataPortal.Fetch<ParentUrlPageList>(new Criteria { TenantId = tenantId, LocaleId = localeId });
        }

        internal static ParentUrlPageList GetRequestCachedParentUrlPageList(int tenantId, int localeId)
        {
            var cmd = new GetRequestCachedParentUrlPageListCommand(tenantId, localeId);
            cmd = DataPortal.Execute<GetRequestCachedParentUrlPageListCommand>(cmd);
            return cmd.ParentUrlPageList;
        }

        private void DataPortal_Fetch(Criteria criteria)
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

        // TODO: Determine best location for criteria class
        [Serializable()]
        public class Criteria : CriteriaBase<Criteria>
        {
            public static readonly PropertyInfo<int> TenantIdProperty = RegisterProperty<int>(c => c.TenantId);
            public int TenantId
            {
                get { return ReadProperty(TenantIdProperty); }
                set { LoadProperty(TenantIdProperty, value); }
            }

            public static readonly PropertyInfo<int> LocaleIdProperty = RegisterProperty<int>(c => c.LocaleId);
            public int LocaleId
            {
                get { return ReadProperty(LocaleIdProperty); }
                set { LoadProperty(LocaleIdProperty, value); }
            }
        }
    }
}
