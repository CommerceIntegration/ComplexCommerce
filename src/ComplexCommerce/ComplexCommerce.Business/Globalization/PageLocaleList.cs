using System;
using System.Collections.Generic;
using System.Linq;
using Csla;
using ComplexCommerce.Csla;
using ComplexCommerce.Data.Repositories;
using ComplexCommerce.Data.Dto;
using ComplexCommerce.Business.Caching;

namespace ComplexCommerce.Business.Globalization
{
    //public interface IPageLocaleListFactory
    //{
    //    PageLocaleList EmptyPageLocaleList();
    //    PageLocaleList GetPageLocaleList(Guid pageId);
    //}

    //public class PageLocaleListFactory
    //    : IPageLocaleListFactory
    //{

    //    #region IPageLocaleListFactory Members

    //    public PageLocaleList EmptyPageLocaleList()
    //    {
    //        return PageLocaleList.EmptyPageLocaleList();
    //    }

    //    public PageLocaleList GetPageLocaleList(Guid pageId)
    //    {
    //        return PageLocaleList.GetPageLocaleList(pageId);
    //    }

    //    #endregion
    //}

    [Serializable]
    public class PageLocaleList
        : CslaReadOnlyListBase<PageLocaleList, PageLocaleInfo>
    {
        //internal static PageLocaleList EmptyPageLocaleList()
        //{
        //    return new PageLocaleList();
        //}

        internal static PageLocaleList GetPageLocaleList(Guid pageId)
        {
            return DataPortal.Fetch<PageLocaleList>(pageId);
        }

        public static PageLocaleList GetCachedPageLocaleList(Guid pageId)
        {
            var cmd = new GetCachedPageLocaleListCommand(pageId);
            cmd = DataPortal.Execute<GetCachedPageLocaleListCommand>(cmd);
            return cmd.PageLocaleList;
        }

        private void DataPortal_Fetch(Guid pageId)
        {
            using (var ctx = ContextFactory.GetContext())
            {
                var rlce = RaiseListChangedEvents;
                RaiseListChangedEvents = false;
                IsReadOnly = false;

                var list = repository.ListLocales(pageId);

                foreach (var item in list)
                    Add(DataPortal.FetchChild<PageLocaleInfo>(item));

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

        #region GetCachedPageLocaleListCommand

        [Serializable]
        private class GetCachedPageLocaleListCommand
            : CslaCommandBase<GetCachedPageLocaleListCommand>
        {
            public GetCachedPageLocaleListCommand(Guid pageId)
            {
                if (pageId == null)
                    throw new ArgumentNullException("pageId");

                this.PageId = pageId;
            }


            public static PropertyInfo<Guid> PageIdProperty = RegisterProperty<Guid>(c => c.PageId);
            public Guid PageId
            {
                get { return ReadProperty(PageIdProperty); }
                private set { LoadProperty(PageIdProperty, value); }
            }

            public static PropertyInfo<PageLocaleList> PageLocaleListProperty = RegisterProperty<PageLocaleList>(c => c.PageLocaleList);
            public PageLocaleList PageLocaleList
            {
                get { return ReadProperty(PageLocaleListProperty); }
                private set { LoadProperty(PageLocaleListProperty, value); }
            }

            /// <summary>
            /// We work with the cache on the server side of the DataPortal
            /// </summary>
            protected override void DataPortal_Execute()
            {
                var key = "__ML_PageLocaleList_" + this.PageId.ToString() + "__";
                this.PageLocaleList = cache.GetOrAdd(key,
                    () => PageLocaleList.GetPageLocaleList(this.PageId));
            }

            #region Dependency Injection

            [NonSerialized]
            [NotUndoable]
            private IMicroObjectCache<PageLocaleList> cache;
            public IMicroObjectCache<PageLocaleList> Cache
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
