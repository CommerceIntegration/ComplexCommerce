using System;
using Csla;
using ComplexCommerce.Csla;
using ComplexCommerce.Data.Dto;

namespace ComplexCommerce.Business
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

        public static PropertyInfo<Guid> PageLocaleIdProperty = RegisterProperty<Guid>(c => c.PageLocaleId);
        public Guid PageLocaleId
        {
            get { return GetProperty(PageLocaleIdProperty); }
            private set { LoadProperty(PageLocaleIdProperty, value); }
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

        public static PropertyInfo<string> TitleProperty = RegisterProperty<string>(c => c.Title);
        public string Title
        {
            get { return GetProperty(TitleProperty); }
            private set { LoadProperty(TitleProperty, value); }
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

        public static PropertyInfo<string> MetaRobotsProperty = RegisterProperty<string>(c => c.MetaRobots);
        public string MetaRobots
        {
            get { return GetProperty(MetaRobotsProperty); }
            private set { LoadProperty(MetaRobotsProperty, value); }
        }

        public static PropertyInfo<bool> IsVisibleOnMainMenuProperty = RegisterProperty<bool>(c => c.IsVisibleOnMainMenu);
        public bool IsVisibleOnMainMenu
        {
            get { return GetProperty(IsVisibleOnMainMenuProperty); }
            private set { LoadProperty(IsVisibleOnMainMenuProperty, value); }
        }

        private void Child_Fetch(ParentUrlPageDto item)
        {
            Id = item.Id;
            ParentId = item.ParentId;
            PageLocaleId = item.PageLocaleId;
            ContentType = (ContentTypeEnum)item.ContentType;
            ContentId = item.ContentId;
            Title = item.Title;
            Url = item.Url;
            IsUrlAbsolute = item.IsUrlAbsolute;
            MetaRobots = item.MetaRobots;
            IsVisibleOnMainMenu = item.IsVisibleOnMainMenu;
        }
    }
}
