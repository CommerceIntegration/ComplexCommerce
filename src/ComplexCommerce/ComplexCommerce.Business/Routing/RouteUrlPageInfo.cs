using System;
using Csla;
using Csla.Core;
using ComplexCommerce.Csla;
using ComplexCommerce.Data.Dto;
using ComplexCommerce.Business.Text;

namespace ComplexCommerce.Business.Routing
{
    public interface IRouteUrlPageInfo
    {
        string VirtualPath { get; }
        string UrlPath { get; }
        ContentTypeEnum ContentType { get; }
        Guid ContentId { get; }
        int LocaleId { get; }
    }

    [Serializable]
    public class RouteUrlPageInfo
        : CslaReadOnlyBase<RouteUrlPageInfo>, IRouteUrlPageInfo
    {
        public string VirtualPath
        {
            get
            {
                var path = this.UrlPath;
                if (path.StartsWith("/"))
                {
                    return path.Substring(1);
                }
                return path;
            }
        }

        public static PropertyInfo<string> UrlPathProperty = RegisterProperty<string>(c => c.UrlPath);
        public string UrlPath
        {
            get { return GetProperty(UrlPathProperty); }
            private set { LoadProperty(UrlPathProperty, value); }
        }

        // Route = {controller}
        public static PropertyInfo<ContentTypeEnum> ContentTypeProperty = RegisterProperty<ContentTypeEnum>(c => c.ContentType);
        public ContentTypeEnum ContentType
        {
            get { return GetProperty(ContentTypeProperty); }
            private set { LoadProperty(ContentTypeProperty, value); }
        }

        // Route = {id}
        public static PropertyInfo<Guid> ContentIdProperty = RegisterProperty<Guid>(c => c.ContentId);
        public Guid ContentId
        {
            get { return GetProperty(ContentIdProperty); }
            private set { LoadProperty(ContentIdProperty, value); }
        }

        // Route = {localeId}
        public static PropertyInfo<int> LocaleIdProperty = RegisterProperty<int>(c => c.LocaleId);
        public int LocaleId
        {
            get { return GetProperty(LocaleIdProperty); }
            private set { LoadProperty(LocaleIdProperty, value); }
        }

        private void Child_Fetch(ParentUrlPageInfo item)
        {
            this.UrlPath = urlBuilder.BuildPath(
                item.Url,
                item.IsUrlAbsolute,
                item.ParentId,
                item.TenantId,
                item.LocaleId);

            this.ContentType = (ContentTypeEnum)item.ContentType;
            this.ContentId = item.ContentId;
            this.LocaleId = item.LocaleId;
        }

        #region Dependency Injection

        [NonSerialized]
        [NotUndoable]
        private IUrlBuilder urlBuilder;
        public IUrlBuilder UrlBuilder
        {
            set
            {
                // Don't allow the value to be set to null
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                // Don't allow the value to be set more than once
                if (this.urlBuilder != null)
                {
                    throw new InvalidOperationException();
                }
                this.urlBuilder = value;
            }
        }

        #endregion
    }
}
