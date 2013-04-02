using System;
using System.Collections.Generic;
using System.Globalization;
using Csla;
using ComplexCommerce.Csla;
using ComplexCommerce.Data.Dto;

namespace ComplexCommerce.Business.Globalization
{
    [Serializable]
    public class ProductLocaleInfo
        : CslaReadOnlyBase<ProductLocaleInfo>
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

        private void Child_Fetch(ProductLocaleDto item)
        {
            this.LocaleId = item.LocaleId;

            // TODO: Made an interface-based locale name provider to centralize the business logic.
            this.NativeName = new CultureInfo(item.LocaleId).NativeName;
        }
    }
}
