using System;
using System.Linq;
using System.Globalization;
using Csla;
using ComplexCommerce.Csla;
using ComplexCommerce.Data.Dto;
using ComplexCommerce.Business.Rules;

namespace ComplexCommerce.Business
{
    public class RouteUrlProductInfo
        : CslaReadOnlyBase<RouteUrlProductInfo>
    {
        public static PropertyInfo<int> LocaleIdProperty = RegisterProperty<int>(c => c.LocaleId);
        public int LocaleId
        {
            get { return GetProperty(LocaleIdProperty); }
            private set { LoadProperty(LocaleIdProperty, value); }
        }

        public static PropertyInfo<string> RouteUrlProperty = RegisterProperty<string>(c => c.RouteUrl);
        public string RouteUrl
        {
            get { return GetProperty(RouteUrlProperty); }
            private set { LoadProperty(RouteUrlProperty, value); }
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

                var path = GetProperty(RouteUrlProperty);
                if (path.Length > 1)
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

        public static PropertyInfo<string> ParentPageRouteUrlProperty = RegisterProperty<string>(c => c.ParentPageRouteUrl);
        public string ParentPageRouteUrl
        {
            get { return GetProperty(ParentPageRouteUrlProperty); }
            private set { LoadProperty(ParentPageRouteUrlProperty, value); }
        }

        public static PropertyInfo<string> ProductUrlSlugProperty = RegisterProperty<string>(c => c.ProductUrlSlug);
        public string ProductUrlSlug
        {
            get { return GetProperty(ProductUrlSlugProperty); }
            private set { LoadProperty(ProductUrlSlugProperty, value); }
        }

        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();

            BusinessRules.AddRule(new UrlPathProductRule(RouteUrlProperty, ParentPageRouteUrlProperty, ProductUrlSlugProperty) { Priority = 1 });
            BusinessRules.AddRule(new UrlPathTrailingSlashRule(RouteUrlProperty) { Priority = 2 });
            BusinessRules.AddRule(new UrlPathLeadingSlashRule(RouteUrlProperty) { Priority = 3 });
            BusinessRules.AddRule(new UrlPathLocaleRule(RouteUrlProperty, LocaleIdProperty, appContext) { Priority = 4 });
        }

        private void Child_Fetch(RouteUrlProductDto item)
        {
            LocaleId = item.LocaleId;
            ParentPageRouteUrl = item.ParentPageRouteUrl;
            ProductUrlSlug = item.ProductUrlSlug;
            CategoryXProductXTenantLocaleId = item.CategoryXProductXTenantLocaleId;
            //ProductXTenantLocaleId = item.ProductXTenantLocaleId;

            // Force the BusinessRules to execute
            this.BusinessRules.CheckRules();
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

        #endregion

    }
}
