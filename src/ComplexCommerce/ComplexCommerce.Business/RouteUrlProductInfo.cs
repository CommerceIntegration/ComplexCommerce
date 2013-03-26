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
        Guid CategoryXProductXTenantLocaleId { get; }
        string UrlPath { get; }
        string VirtualPath { get; }
    }

    public class RouteUrlProductInfo
        : CslaReadOnlyBase<RouteUrlProductInfo>, IRouteUrlProductInfo
    {
        //public static PropertyInfo<Guid> ParentIdProperty = RegisterProperty<Guid>(c => c.ParentId);
        //public Guid ParentId
        //{
        //    get { return GetProperty(ParentIdProperty); }
        //    private set { LoadProperty(ParentIdProperty, value); }
        //}

        //public static PropertyInfo<int> LocaleIdProperty = RegisterProperty<int>(c => c.LocaleId);
        //public int LocaleId
        //{
        //    get { return GetProperty(LocaleIdProperty); }
        //    private set { LoadProperty(LocaleIdProperty, value); }
        //}

        //public static PropertyInfo<string> RouteUrlProperty = RegisterProperty<string>(c => c.RouteUrl);
        //public string RouteUrl
        //{
        //    get { return GetProperty(RouteUrlProperty); }
        //    private set { LoadProperty(RouteUrlProperty, value); }
        //}

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
                //var path = GetProperty(RouteUrlProperty);
                //if (path.Length > 1)
                //{
                //    return path.Substring(1, path.Length - 2);
                //}
                //return path;

                var path = GetProperty(UrlPathProperty);
                if (path.Length > 1 && path.StartsWith("/"))
                {
                    return path.Substring(1, path.Length - 1);
                }
                return path;
            }
        }

        //// Route = {id}
        //public static PropertyInfo<Guid> ProductXTenantLocaleIdProperty = RegisterProperty<Guid>(c => c.ProductXTenantLocaleId);
        //public Guid ProductXTenantLocaleId
        //{
        //    get { return GetProperty(ProductXTenantLocaleIdProperty); }
        //    private set { LoadProperty(ProductXTenantLocaleIdProperty, value); }
        //}

        // Route = {id}
        public static PropertyInfo<Guid> CategoryXProductXTenantLocaleIdProperty = RegisterProperty<Guid>(c => c.CategoryXProductXTenantLocaleId);
        public Guid CategoryXProductXTenantLocaleId
        {
            get { return GetProperty(CategoryXProductXTenantLocaleIdProperty); }
            private set { LoadProperty(CategoryXProductXTenantLocaleIdProperty, value); }
        }

        //public static PropertyInfo<string> ParentPageUrlProperty = RegisterProperty<string>(c => c.ParentPageUrl);
        //public string ParentPageUrl
        //{
        //    get { return GetProperty(ParentPageUrlProperty); }
        //    private set { LoadProperty(ParentPageUrlProperty, value); }
        //}

        //public static PropertyInfo<string> UrlProperty = RegisterProperty<string>(c => c.Url);
        //public string Url
        //{
        //    get { return GetProperty(UrlProperty); }
        //    private set { LoadProperty(UrlProperty, value); }
        //}

        //public static PropertyInfo<bool> IsUrlAbsoluteProperty = RegisterProperty<bool>(c => c.IsUrlAbsolute);
        //public bool IsUrlAbsolute
        //{
        //    get { return GetProperty(IsUrlAbsoluteProperty); }
        //    private set { LoadProperty(IsUrlAbsoluteProperty, value); }
        //}

        //protected override void AddBusinessRules()
        //{
        //    base.AddBusinessRules();


        //    //BusinessRules.AddRule(new UrlPathProductRule(RouteUrlProperty, ParentPageUrlProperty, UrlProperty) { Priority = 1 });
        //    BusinessRules.AddRule(new UrlPathPageRule(RouteUrlProperty, UrlProperty, IsUrlAbsoluteProperty, ParentIdProperty, appContext) { Priority = 1 });
        //    BusinessRules.AddRule(new UrlPathTrailingSlashRule(RouteUrlProperty) { Priority = 2 });
        //    BusinessRules.AddRule(new UrlPathLeadingSlashRule(RouteUrlProperty) { Priority = 3 });
        //    BusinessRules.AddRule(new UrlPathLocaleRule(RouteUrlProperty, LocaleIdProperty, appContext) { Priority = 4 });
        //}

        private void Child_Fetch(RouteUrlProductDto item)
        {
            //ParentId = item.ParentId;
            //LocaleId = item.LocaleId;
            ////ParentPageUrl = item.ParentPageUrl;
            //Url = item.Url;
            //IsUrlAbsolute = item.IsUrlAbsolute;
            
            ////ProductXTenantLocaleId = item.ProductXTenantLocaleId;


            this.CategoryXProductXTenantLocaleId = item.CategoryXProductXTenantLocaleId;
            this.UrlPath = urlBuilder.BuildPath(
                item.Url, 
                item.IsUrlAbsolute, 
                item.ParentId, 
                appContext.CurrentTenant.Id, 
                appContext.CurrentLocaleId, 
                appContext.CurrentTenant.DefaultLocale.LCID);

            //// Force the BusinessRules to execute
            //this.BusinessRules.CheckRules();
        }

        #region Dependency Injection

        [NonSerialized]
        [NotUndoable]
        private Context.IApplicationContext appContext;
        public Context.IApplicationContext AppContext
        {
            set
            {
                // Don't allow the value to be set to null
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                // Don't allow the value to be set more than once
                if (this.appContext != null)
                {
                    throw new InvalidOperationException();
                }
                this.appContext = value;
            }
        }

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
