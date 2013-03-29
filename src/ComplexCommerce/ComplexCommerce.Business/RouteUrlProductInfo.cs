using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using Csla;
using ComplexCommerce.Csla;
using ComplexCommerce.Data.Dto;
using ComplexCommerce.Business.Text;

namespace ComplexCommerce.Business
{
    public interface IRouteUrlProductInfo
    {
        Guid CategoryXProductId { get; }
        string UrlPath { get; }
        string VirtualPath { get; }
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
                var path = GetProperty(UrlPathProperty);
                if (path.Length > 1 && path.StartsWith("/"))
                {
                    return path.Substring(1, path.Length - 1);
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

        private void Child_Fetch(RouteUrlProductDto item, ITenantLocale tenantLocale)
        {
            this.CategoryXProductId = item.CategoryXProductId;
            this.UrlPath = urlBuilder.BuildPath(
                item.Url, 
                item.IsUrlAbsolute, 
                item.ParentId, 
                tenantLocale);
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
