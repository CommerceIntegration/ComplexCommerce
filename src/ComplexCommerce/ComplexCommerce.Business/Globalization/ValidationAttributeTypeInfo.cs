//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Csla;
//using ComplexCommerce.Csla;

//namespace ComplexCommerce.Business.Globalization
//{
//    [Serializable]
//    public class ValidationAttributeTypeInfo
//        : CslaReadOnlyBase<ValidationAttributeTypeInfo>
//    {
//        public static readonly PropertyInfo<string> TypeNameProperty = RegisterProperty<string>(p => p.TypeName);
//        public string TypeName
//        {
//            get { return GetProperty(TypeNameProperty); }
//            private set { LoadProperty(TypeNameProperty, value); }
//        }

//        public static readonly PropertyInfo<string> TypeFullNameProperty = RegisterProperty<string>(p => p.TypeFullName);
//        public string TypeFullName
//        {
//            get { return GetProperty(TypeFullNameProperty); }
//            private set { LoadProperty(TypeFullNameProperty, value); }
//        }

//        public static readonly PropertyInfo<string> TextNameProperty = RegisterProperty<string>(p => p.TextName);
//        public string TextName
//        {
//            get { return GetProperty(TextNameProperty); }
//            private set { LoadProperty(TextNameProperty, value); }
//        }

//        public static PropertyInfo<int> LocaleIdProperty = RegisterProperty<int>(c => c.LocaleId);
//        public int LocaleId
//        {
//            get { return GetProperty(LocaleIdProperty); }
//            private set { LoadProperty(LocaleIdProperty, value); }
//        }

//        // TODO: Determine what makes up the hash code
//        public static readonly PropertyInfo<int> HashCodeProperty = RegisterProperty<int>(p => p.HashCode);
//        public int HashCode
//        {
//            get { return GetProperty(HashCodeProperty); }
//            private set { LoadProperty(HashCodeProperty, value); }
//        }
//    }
//}
