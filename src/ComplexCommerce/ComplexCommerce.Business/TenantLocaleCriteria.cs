using System;
using Csla;

namespace ComplexCommerce.Business
{
    [Serializable()]
    internal class TenantLocaleCriteria
        : CriteriaBase<TenantLocaleCriteria>, ITenantLocale
    {
        public TenantLocaleCriteria(
            int tenantId,
            int localeId,
            int defaultLocaleId)
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

        public static readonly PropertyInfo<int> DefaultLocaleIdProperty = RegisterProperty<int>(c => c.DefaultLocaleId);
        public int DefaultLocaleId
        {
            get { return ReadProperty(DefaultLocaleIdProperty); }
            private set { LoadProperty(DefaultLocaleIdProperty, value); }
        }
    }
}
