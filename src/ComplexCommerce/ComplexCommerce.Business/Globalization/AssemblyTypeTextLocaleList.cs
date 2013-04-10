using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Threading;
using Csla;
using ComplexCommerce.Csla;
using ComplexCommerce.Business.Caching;
using ComplexCommerce.Data.Repositories;

namespace ComplexCommerce.Business.Globalization
{
    [Serializable]
    public class AssemblyTypeTextLocaleList
        : CslaReadOnlyListBase<AssemblyTypeTextLocaleList, AssemblyTypeTextLocaleInfo>
    {
        internal static AssemblyTypeTextLocaleList GetAssemblyTypeTextLocaleList(int tenantId, int localeId, string typeName)
        {
            return DataPortal.Fetch<AssemblyTypeTextLocaleList>(
                new Criteria(tenantId, localeId, typeName.GetHashCode(), typeName));
        }

        public static AssemblyTypeTextLocaleList GetCachedAssemblyTypeTextLocaleList(int tenantId, int localeId, string typeName)
        {
            var cmd = new GetCachedAssemblyTypeTextLocaleListCommand(tenantId, localeId, typeName);
            cmd = DataPortal.Execute<GetCachedAssemblyTypeTextLocaleListCommand>(cmd);
            return cmd.AssemblyTypeTextLocaleList;
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
                    .List(criteria.TenantId, criteria.LocaleId, criteria.HashCode, criteria.TypeName)
                    .OrderBy(x => x.LocaleId == criteria.LocaleId ? 0 : 1); // Requested locale first, default locale second
                var textNamesAdded = new List<string>();

                foreach (var item in list)
                {
                    if (item.LocaleId == criteria.LocaleId)
                    {
                        Add(DataPortal.FetchChild<AssemblyTypeTextLocaleInfo>(item));
                        textNamesAdded.Add(item.TextName);
                    }
                    else
                    {
                        if (!textNamesAdded.Contains(item.TextName))
                        {
                            // Format phrase for non-default locale
                            item.Value = string.Format("{1}:[{0}]", item.Value, new CultureInfo(criteria.LocaleId));
                            Add(DataPortal.FetchChild<AssemblyTypeTextLocaleInfo>(item));
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
                string typeName)
            {
                if (tenantId < 1)
                    throw new ArgumentOutOfRangeException("tenantId");
                if (localeId < 1)
                    throw new ArgumentOutOfRangeException("localeId");
                if (string.IsNullOrEmpty(typeName))
                    throw new ArgumentNullException("typeName");

                this.TenantId = tenantId;
                this.LocaleId = localeId;
                this.HashCode = hashCode;
                this.TypeName = typeName;
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

            public static readonly PropertyInfo<string> TypeNameProperty = RegisterProperty<string>(p => p.TypeName);
            public string TypeName
            {
                get { return ReadProperty(TypeNameProperty); }
                private set { LoadProperty(TypeNameProperty, value); }
            }
        }

        #endregion

        #region Dependency Injection

        [NonSerialized]
        [NotUndoable]
        private IAssemblyTypeRepository repository;
        public IAssemblyTypeRepository Repository
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
        private class GetCachedAssemblyTypeTextLocaleListCommand
            : CslaCommandBase<GetCachedAssemblyTypeTextLocaleListCommand>
        {
            public GetCachedAssemblyTypeTextLocaleListCommand(int tenantId, int localeId, string typeName)
            {
                if (tenantId < 1)
                    throw new ArgumentOutOfRangeException("tenantId");
                if (localeId < 1)
                    throw new ArgumentOutOfRangeException("localeId");
                if (string.IsNullOrEmpty(typeName))
                    throw new ArgumentNullException("typeName");

                this.TenantId = tenantId;
                this.LocaleId = localeId;
                this.TypeName = typeName;
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

            public static PropertyInfo<string> TypeNameProperty = RegisterProperty<string>(c => c.TypeName);
            public string TypeName
            {
                get { return ReadProperty(TypeNameProperty); }
                private set { LoadProperty(TypeNameProperty, value); }
            }

            public static PropertyInfo<AssemblyTypeTextLocaleList> AssemblyTypeTextLocaleListProperty = RegisterProperty<AssemblyTypeTextLocaleList>(c => c.AssemblyTypeTextLocaleList);
            public AssemblyTypeTextLocaleList AssemblyTypeTextLocaleList
            {
                get { return ReadProperty(AssemblyTypeTextLocaleListProperty); }
                private set { LoadProperty(AssemblyTypeTextLocaleListProperty, value); }
            }

            /// <summary>
            /// We work with the cache on the server side of the DataPortal
            /// </summary>
            protected override void DataPortal_Execute()
            {
                // TODO: Add a way to control cache timeouts on a per object basis. Need some way to 
                // categorize between resources that are expensive to keep in memory and resources that are
                // expensive to pull from the database.

                var key = "__ML_AssemblyTypeTextLocaleList_" +
                    this.TenantId.ToString() + "_" +
                    this.LocaleId.ToString() + "_" +
                    this.TypeName + "__";
                this.AssemblyTypeTextLocaleList = cache.GetOrAdd(key,
                    () => AssemblyTypeTextLocaleList.GetAssemblyTypeTextLocaleList(this.TenantId, this.LocaleId, this.TypeName));
            }

            #region Dependency Injection

            [NonSerialized]
            [NotUndoable]
            private IMicroObjectCache<AssemblyTypeTextLocaleList> cache;
            public IMicroObjectCache<AssemblyTypeTextLocaleList> Cache
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
