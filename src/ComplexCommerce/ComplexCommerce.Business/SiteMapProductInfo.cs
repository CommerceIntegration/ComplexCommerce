using System;
using Csla;
using ComplexCommerce.Csla;
using ComplexCommerce.Data.Dto;
using ComplexCommerce.Data.Repositories;
using ComplexCommerce.Business.Rules;
using ComplexCommerce.Business.Context;

namespace ComplexCommerce.Business
{
    [Serializable]
    public class SiteMapProductInfo
        : CslaReadOnlyBase<SiteMapProductInfo>
    {
        public static readonly PropertyInfo<Guid> CategoryIdProperty = RegisterProperty<Guid>(p => p.CategoryId);
        public Guid CategoryId
        {
            get { return GetProperty(CategoryIdProperty); }
            private set { LoadProperty(CategoryIdProperty, value); }
        }

        public static readonly PropertyInfo<int> LocaleIdProperty = RegisterProperty<int>(p => p.LocaleId);
        public int LocaleId
        {
            get { return GetProperty(LocaleIdProperty); }
            private set { LoadProperty(LocaleIdProperty, value); }
        }

        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(p => p.Name);
        public string Name
        {
            get { return GetProperty(NameProperty); }
            private set { LoadProperty(NameProperty, value); }
        }

        public static readonly PropertyInfo<string> ProductUrlSlugProperty = RegisterProperty<string>(p => p.ProductUrlSlug);
        public string ProductUrlSlug
        {
            get { return GetProperty(ProductUrlSlugProperty); }
            private set { LoadProperty(ProductUrlSlugProperty, value); }
        }

        public static readonly PropertyInfo<string> ParentPageRouteUrlProperty = RegisterProperty<string>(p => p.ParentPageRouteUrl);
        public string ParentPageRouteUrl
        {
            get { return GetProperty(ParentPageRouteUrlProperty); }
            private set { LoadProperty(ParentPageRouteUrlProperty, value); }
        }

        public static readonly PropertyInfo<string> MetaRobotsProperty = RegisterProperty<string>(p => p.MetaRobots);
        public string MetaRobots
        {
            get { return GetProperty(MetaRobotsProperty); }
            private set { LoadProperty(MetaRobotsProperty, value); }
        }

        public static readonly PropertyInfo<string> DefaultCategoryRouteUrlProperty = RegisterProperty<string>(p => p.DefaultCategoryRouteUrl);
        public string DefaultCategoryRouteUrl
        {
            get { return GetProperty(DefaultCategoryRouteUrlProperty); }
            private set { LoadProperty(DefaultCategoryRouteUrlProperty, value); }
        }


        #region Constructed Properties

        public static readonly PropertyInfo<string> RouteUrlProperty = RegisterProperty<string>(p => p.RouteUrl);
        public string RouteUrl
        {
            get { return GetProperty(RouteUrlProperty); }
            private set { LoadProperty(RouteUrlProperty, value); }
        }

        public static readonly PropertyInfo<string> CanonicalRouteUrlProperty = RegisterProperty<string>(p => p.CanonicalRouteUrl);
        public string CanonicalRouteUrl
        {
            get { return GetProperty(CanonicalRouteUrlProperty); }
            private set { LoadProperty(CanonicalRouteUrlProperty, value); }
        }

        #endregion


        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();

            // Route URL
            BusinessRules.AddRule(new UrlPathProductRule(RouteUrlProperty, ParentPageRouteUrlProperty, ProductUrlSlugProperty) { Priority = 1 });
            BusinessRules.AddRule(new UrlPathLeadingSlashRule(RouteUrlProperty) { Priority = 2 });
            BusinessRules.AddRule(new UrlPathTrailingSlashRule(RouteUrlProperty) { Priority = 3 });
            BusinessRules.AddRule(new UrlPathLocaleRule(RouteUrlProperty, LocaleIdProperty, appContext) { Priority = 4 });

            // Canonical URL
            BusinessRules.AddRule(new UrlPathProductRule(CanonicalRouteUrlProperty, DefaultCategoryRouteUrlProperty, ProductUrlSlugProperty) { Priority = 1 });
            BusinessRules.AddRule(new UrlPathLeadingSlashRule(CanonicalRouteUrlProperty) { Priority = 2 });
            BusinessRules.AddRule(new UrlPathTrailingSlashRule(CanonicalRouteUrlProperty) { Priority = 3 });
            BusinessRules.AddRule(new UrlPathLocaleRule(CanonicalRouteUrlProperty, LocaleIdProperty, appContext) { Priority = 4 });
        }

        private void Child_Fetch(SiteMapProductDto item)
        {
            //ProductXTenantLocaleId = item.ProductXTenantLocaleId;
            CategoryId = item.CategoryId;
            LocaleId = item.LocaleId;
            Name = item.Name;
            ProductUrlSlug = item.ProductUrlSlug;
            ParentPageRouteUrl = item.ParentPageRouteUrl;
            MetaRobots = item.MetaRobots;
            DefaultCategoryRouteUrl = item.DefaultCategoryRouteUrl;
            
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
