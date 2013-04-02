using System;
using Csla;
using ComplexCommerce.Csla;
using ComplexCommerce.Data.Dto;
using ComplexCommerce.Business.Text;

namespace ComplexCommerce.Business.Routing
{
    public interface IRouteUrlProductInfo
    {
        string VirtualPath { get; }
        string UrlPath { get; }
        Guid CategoryXProductId { get; }
        int LocaleId { get; }
    }

    public class RouteUrlProductInfo
        : CslaReadOnlyBase<RouteUrlProductInfo>, IRouteUrlProductInfo
    {
        public static PropertyInfo<string> UrlPathProperty = RegisterProperty<string>(c => c.UrlPath);
        public string UrlPath
        {
            get { return GetProperty(UrlPathProperty); }
            private set { LoadProperty(UrlPathProperty, value); }
        }

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

        // Route = {id}
        public static PropertyInfo<Guid> CategoryXProductIdProperty = RegisterProperty<Guid>(c => c.CategoryXProductId);
        public Guid CategoryXProductId
        {
            get { return GetProperty(CategoryXProductIdProperty); }
            private set { LoadProperty(CategoryXProductIdProperty, value); }
        }

        // Route = {localeId}
        public static PropertyInfo<int> LocaleIdProperty = RegisterProperty<int>(c => c.LocaleId);
        public int LocaleId
        {
            get { return GetProperty(LocaleIdProperty); }
            private set { LoadProperty(LocaleIdProperty, value); }
        }

        private void Child_Fetch(RouteUrlProductDto item)
        {
            this.UrlPath = urlBuilder.BuildPath(
                item.Url,
                item.IsUrlAbsolute,
                item.ParentId,
                item.TenantId,
                item.LocaleId);

            this.CategoryXProductId = item.CategoryXProductId;
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
