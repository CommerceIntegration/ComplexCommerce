using System;
using Csla;
using ComplexCommerce.Csla;
using ComplexCommerce.Data.Dto;

namespace ComplexCommerce.Business.Routing
{
    [Serializable]
    public class ParentUrlPageInfo
        : CslaReadOnlyBase<ParentUrlPageInfo>
    {
        public static PropertyInfo<Guid> IdProperty = RegisterProperty<Guid>(c => c.Id);
        public Guid Id
        {
            get { return GetProperty(IdProperty); }
            private set { LoadProperty(IdProperty, value); }
        }

        public static PropertyInfo<Guid> ParentIdProperty = RegisterProperty<Guid>(c => c.ParentId);
        public Guid ParentId
        {
            get { return GetProperty(ParentIdProperty); }
            private set { LoadProperty(ParentIdProperty, value); }
        }

        public static PropertyInfo<int> TenantIdProperty = RegisterProperty<int>(c => c.TenantId);
        public int TenantId
        {
            get { return GetProperty(TenantIdProperty); }
            private set { LoadProperty(TenantIdProperty, value); }
        }

        public static PropertyInfo<int> LocaleIdProperty = RegisterProperty<int>(c => c.LocaleId);
        public int LocaleId
        {
            get { return GetProperty(LocaleIdProperty); }
            private set { LoadProperty(LocaleIdProperty, value); }
        }

        public static PropertyInfo<ContentTypeEnum> ContentTypeProperty = RegisterProperty<ContentTypeEnum>(c => c.ContentType);
        public ContentTypeEnum ContentType
        {
            get { return GetProperty(ContentTypeProperty); }
            private set { LoadProperty(ContentTypeProperty, value); }
        }

        public static PropertyInfo<Guid> ContentIdProperty = RegisterProperty<Guid>(c => c.ContentId);
        public Guid ContentId
        {
            get { return GetProperty(ContentIdProperty); }
            private set { LoadProperty(ContentIdProperty, value); }
        }

        public static PropertyInfo<string> UrlProperty = RegisterProperty<string>(c => c.Url);
        public string Url
        {
            get { return GetProperty(UrlProperty); }
            private set { LoadProperty(UrlProperty, value); }
        }

        public static PropertyInfo<bool> IsUrlAbsoluteProperty = RegisterProperty<bool>(c => c.IsUrlAbsolute);
        public bool IsUrlAbsolute
        {
            get { return GetProperty(IsUrlAbsoluteProperty); }
            private set { LoadProperty(IsUrlAbsoluteProperty, value); }
        }

        private void Child_Fetch(ParentUrlPageDto item)
        {
            Id = item.Id;
            ParentId = item.ParentId;
            TenantId = item.TenantId;
            LocaleId = item.LocaleId;
            ContentType = (ContentTypeEnum)item.ContentType;
            ContentId = item.ContentId;
            Url = item.Url;
            IsUrlAbsolute = item.IsUrlAbsolute;
        }
    }
}
