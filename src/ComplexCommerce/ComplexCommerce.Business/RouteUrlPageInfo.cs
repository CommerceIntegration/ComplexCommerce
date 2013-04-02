using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using Csla;
using Csla.Core;
using ComplexCommerce.Csla;
using ComplexCommerce.Data.Dto;
using ComplexCommerce.Business.Text;

namespace ComplexCommerce.Business
{
    public interface IRouteUrlPageInfo
    {
        string VirtualPath { get; }
        string UrlPath { get; }
        int LocaleId { get; }
        ContentTypeEnum ContentType { get; }
        Guid ContentId { get; }
    }

    [Serializable]
    public class RouteUrlPageInfo
        : CslaReadOnlyBase<RouteUrlPageInfo>, IRouteUrlPageInfo
    {
        public string VirtualPath
        {
            get
            {
                var path = GetProperty(UrlPathProperty);
                if (path.StartsWith("/"))
                {
                    return path.Substring(1);
                }
                return path;
            }
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

        #region Constructed Properties

        public static PropertyInfo<string> UrlPathProperty = RegisterProperty<string>(c => c.UrlPath);
        public string UrlPath
        {
            get { return GetProperty(UrlPathProperty); }
            private set { LoadProperty(UrlPathProperty, value); }
        }

        #endregion

        private void Child_Fetch(ParentUrlPageInfo item, ITenantLocale tenantLocale)
        {
            this.LocaleId = item.LocaleId;

            this.UrlPath = urlBuilder.BuildPath(
                item.Url, 
                item.IsUrlAbsolute, 
                item.ParentId, 
                tenantLocale.TenantId, 
                item.LocaleId, 
                tenantLocale.DefaultLocaleId);

            this.ContentType = (ContentTypeEnum)item.ContentType;
            this.ContentId = item.ContentId;
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
