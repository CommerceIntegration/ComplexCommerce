using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Globalization;
using Csla;
using ComplexCommerce.Csla;
using ComplexCommerce.Data.Repositories;
using ComplexCommerce.Business.Caching;

namespace ComplexCommerce.Business.Globalization
{
    [Serializable]
    public class ViewTextLocaleList
        : CslaReadOnlyListBase<ViewTextLocaleList, ViewTextLocaleInfo>
    {
        public static ViewTextLocaleList GetViewTextLocaleList(int tenantId, int localeId, string virtualPath)
        {
            return DataPortal.Fetch<ViewTextLocaleList>(
                new Criteria(tenantId, localeId, virtualPath.GetHashCode(), virtualPath));
        }

        public static ViewTextLocaleList GetCachedViewTextLocaleList(int tenantId, int localeId, string virtualPath)
        {
            return DataPortal.Fetch<ViewTextLocaleList>(
                new Criteria(tenantId, localeId, virtualPath.GetHashCode(), virtualPath));
        }

        public string GetLocalizedText(string textName)
        {
            var textItem = this.FirstOrDefault(x => x.TextName == textName);
            if (textItem != null)
            {
                return textItem.Value;
            }
            return string.Format("{1}:[{0}]", textName, Thread.CurrentThread.CurrentUICulture);
        }

        private void DataPortal_Fetch(Criteria criteria)
        {
            using (var ctx = ContextFactory.GetContext())
            {
                var rlce = RaiseListChangedEvents;
                RaiseListChangedEvents = false;
                IsReadOnly = false;

                var list = repository
                    .List(criteria.TenantId, criteria.LocaleId, criteria.HashCode, criteria.VirtualPath)
                    .OrderBy(x => x.LocaleId == criteria.LocaleId ? 0 : 1); // Requested locale first, default locale second
                var textNamesAdded = new List<string>();

                foreach (var item in list)
                {
                    if (item.LocaleId == criteria.LocaleId)
                    {
                        Add(DataPortal.FetchChild<ViewTextLocaleInfo>(item));
                        textNamesAdded.Add(item.TextName);
                    }
                    else
                    {
                        if (!textNamesAdded.Contains(item.TextName))
                        {
                            // Format phrase for non-default locale
                            item.Value = string.Format("{1}:[{0}]", item.Value, new CultureInfo(criteria.LocaleId));
                            Add(DataPortal.FetchChild<ViewTextLocaleInfo>(item));
                        }
                    }
                }

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
                int tenantId,
                int localeId,
                int hashCode,
                string virtualPath)
            {
                if (tenantId < 1)
                    throw new ArgumentOutOfRangeException("tenantId");
                if (localeId < 1)
                    throw new ArgumentOutOfRangeException("localeId");
                if (string.IsNullOrEmpty(virtualPath))
                    throw new ArgumentNullException("virtualPath");

                this.TenantId = tenantId;
                this.LocaleId = localeId;
                this.HashCode = hashCode;
                this.VirtualPath = virtualPath;
            }

            public static readonly PropertyInfo<int> TenantIdProperty = RegisterProperty<int>(c => c.TenantId);
            public int TenantId
            {
                get { return ReadProperty(TenantIdProperty); }
                private set { LoadProperty(TenantIdProperty, value); }
            }

            public static readonly PropertyInfo<int> LocaleIdProperty = RegisterProperty<int>(c => c.LocaleId);
            public int LocaleId
            {
                get { return ReadProperty(LocaleIdProperty); }
                private set { LoadProperty(LocaleIdProperty, value); }
            }

            public static readonly PropertyInfo<int> HashCodeProperty = RegisterProperty<int>(c => c.HashCode);
            public int HashCode
            {
                get { return ReadProperty(HashCodeProperty); }
                private set { LoadProperty(HashCodeProperty, value); }
            }

            public static readonly PropertyInfo<string> VirtualPathProperty = RegisterProperty<string>(p => p.VirtualPath);
            public string VirtualPath
            {
                get { return ReadProperty(VirtualPathProperty); }
                private set { LoadProperty(VirtualPathProperty, value); }
            }
        }

        #endregion

        #region Dependency Injection

        [NonSerialized]
        [NotUndoable]
        private IViewRepository repository;
        public IViewRepository Repository
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

        #region GetCachedViewTextLocaleListCommand

        [Serializable]
        private class GetCachedViewTextLocaleListCommand
            : CslaCommandBase<GetCachedViewTextLocaleListCommand>
        {
            public GetCachedViewTextLocaleListCommand(int tenantId, int localeId, string virtualPath)
            {
                if (tenantId < 1)
                    throw new ArgumentOutOfRangeException("tenantId");
                if (localeId < 1)
                    throw new ArgumentOutOfRangeException("localeId");
                if (string.IsNullOrEmpty(virtualPath))
                    throw new ArgumentNullException("virtualPath");

                this.TenantId = tenantId;
                this.LocaleId = localeId;
                this.VirtualPath = virtualPath;
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

            public static PropertyInfo<string> VirtualPathProperty = RegisterProperty<string>(c => c.VirtualPath);
            public string VirtualPath
            {
                get { return ReadProperty(VirtualPathProperty); }
                private set { LoadProperty(VirtualPathProperty, value); }
            }

            public static PropertyInfo<ViewTextLocaleList> ViewTextLocaleListProperty = RegisterProperty<ViewTextLocaleList>(c => c.ViewTextLocaleList);
            public ViewTextLocaleList ViewTextLocaleList
            {
                get { return ReadProperty(ViewTextLocaleListProperty); }
                private set { LoadProperty(ViewTextLocaleListProperty, value); }
            }

            /// <summary>
            /// We work with the cache on the server side of the DataPortal
            /// </summary>
            protected override void DataPortal_Execute()
            {
                // TODO: Add a way to control cache timeouts on a per object basis. Need some way to 
                // categorize between resources that are expensive to keep in memory and resources that are
                // expensive to pull from the database.

                var key = "__ML_ViewTextLocaleList_" + 
                    this.TenantId.ToString() + "_" + 
                    this.LocaleId.ToString() + "_" + 
                    this.VirtualPath + "__";
                this.ViewTextLocaleList = cache.GetOrAdd(key,
                    () => ViewTextLocaleList.GetViewTextLocaleList(this.TenantId, this.LocaleId, this.VirtualPath));
            }

            #region Dependency Injection

            [NonSerialized]
            [NotUndoable]
            private IMicroObjectCache<ViewTextLocaleList> cache;
            public IMicroObjectCache<ViewTextLocaleList> Cache
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
