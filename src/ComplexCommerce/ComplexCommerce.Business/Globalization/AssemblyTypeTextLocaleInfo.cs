using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla;
using ComplexCommerce.Csla;
using ComplexCommerce.Data.Dto;

namespace ComplexCommerce.Business.Globalization
{
    [Serializable]
    public class AssemblyTypeTextLocaleInfo
        : CslaReadOnlyBase<AssemblyTypeTextLocaleInfo>
    {
        public static readonly PropertyInfo<string> TextNameProperty = RegisterProperty<string>(p => p.TextName);
        public string TextName
        {
            get { return GetProperty(TextNameProperty); }
            private set { LoadProperty(TextNameProperty, value); }
        }

        public static readonly PropertyInfo<string> ValueProperty = RegisterProperty<string>(p => p.Value);
        public string Value
        {
            get { return GetProperty(ValueProperty); }
            private set { LoadProperty(ValueProperty, value); }
        }

        private void Child_Fetch(AssemblyTypeLocaleDto item)
        {
            this.TextName = item.TextName;
            this.Value = item.Value;
        }
    }
}
