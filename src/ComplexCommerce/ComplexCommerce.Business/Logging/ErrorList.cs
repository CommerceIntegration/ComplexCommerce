using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla;
using Csla.Core;
using ComplexCommerce.Csla;
using ComplexCommerce.Data.Repositories;

namespace ComplexCommerce.Business.Logging
{
    [Serializable]
    public class ErrorList
        : CslaReadOnlyListBase<ErrorList, ErrorInfo>, IReportTotalRowCount
    {
        public static ErrorList GetErrorList(string application, int pageIndex, int pageSize)
        {
            return DataPortal.Fetch<ErrorList>(new Criteria(application, pageIndex, pageSize));
        }

        // Asynchronous fetch
        public static void GetErrorList(string application, int pageIndex, int pageSize, EventHandler<DataPortalResult<ErrorList>> callback, object userState)
        {
            DataPortal.BeginFetch<ErrorList>(new Criteria(application, pageIndex, pageSize), callback, userState);
        }

        //public static ErrorList GetCachedProductLocaleList(Guid productId, int tenantId)
        //{
        //    var cmd = new GetCachedProductLocaleListCommand(productId, tenantId);
        //    cmd = DataPortal.Execute<GetCachedProductLocaleListCommand>(cmd);
        //    return cmd.ProductLocaleList;
        //}

        #region IReportTotalRowCount Members

        private int totalRowCount;
        public int TotalRowCount
        {
            get { return totalRowCount; }
        }

        #endregion

        private void DataPortal_Fetch(Criteria criteria)
        {
            using (var ctx = ContextFactory.GetContext())
            {
                var rlce = RaiseListChangedEvents;
                RaiseListChangedEvents = false;
                IsReadOnly = false;

                int skip = (criteria.PageIndex * criteria.PageSize) + 1;
                int take = (criteria.PageSize);

                var list = repository.ListForApplication(criteria.Application, skip, take, out this.totalRowCount);

                foreach (var item in list)
                    Add(DataPortal.FetchChild<ErrorInfo>(item));

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
                string application,
                int pageIndex,
                int pageSize)
            {
                if (string.IsNullOrEmpty(application))
                    throw new ArgumentNullException("application");
                if (pageIndex < 0)
                    throw new ArgumentOutOfRangeException("pageIndex");
                if (pageSize < 1)
                    throw new ArgumentOutOfRangeException("pageSize");

                this.Application = application;
                this.PageIndex = pageIndex;
                this.PageSize = pageSize;
            }

            public static readonly PropertyInfo<string> ApplicationProperty = RegisterProperty<string>(c => c.Application);
            public string Application
            {
                get { return ReadProperty(ApplicationProperty); }
                private set { LoadProperty(ApplicationProperty, value); }
            }

            public static readonly PropertyInfo<int> PageIndexProperty = RegisterProperty<int>(c => c.PageIndex);
            public int PageIndex
            {
                get { return ReadProperty(PageIndexProperty); }
                private set { LoadProperty(PageIndexProperty, value); }
            }

            public static readonly PropertyInfo<int> PageSizeProperty = RegisterProperty<int>(c => c.PageSize);
            public int PageSize
            {
                get { return ReadProperty(PageSizeProperty); }
                private set { LoadProperty(PageSizeProperty, value); }
            }
        }

        #endregion

        #region Dependency Injection

        [NonSerialized]
        [NotUndoable]
        private IErrorRepository repository;
        public IErrorRepository Repository
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
