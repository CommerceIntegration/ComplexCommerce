using System;
using System.Globalization;
using Csla;
using ComplexCommerce.Csla;
using ComplexCommerce.Data.Dto;

namespace ComplexCommerce.Business.Globalization
{
    [Serializable]
    public class PageLocaleInfo
        : CslaReadOnlyBase<PageLocaleInfo>
    {
        public static PropertyInfo<int> LocaleIdProperty = RegisterProperty<int>(c => c.LocaleId);
        public int LocaleId
        {
            get { return GetProperty(LocaleIdProperty); }
            private set { LoadProperty(LocaleIdProperty, value); }
        }

        public static PropertyInfo<string> NativeNameProperty = RegisterProperty<string>(c => c.NativeName);
        public string NativeName
        {
            get { return GetProperty(NativeNameProperty); }
            private set { LoadProperty(NativeNameProperty, value); }
        }

        private void Child_Fetch(PageLocaleDto item)
        {
            this.LocaleId = item.LocaleId;
            this.NativeName = new CultureInfo(item.LocaleId).NativeName;
        }
    }
}
