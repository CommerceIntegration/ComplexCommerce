using System;
using Csla;
using ComplexCommerce.Csla;
using ComplexCommerce.Data.Dto;
using ComplexCommerce.Data.Repositories;
using ComplexCommerce.Business.Context;
using ComplexCommerce.Business.Text;

namespace ComplexCommerce.Business
{
    [Serializable]
    public class SiteMapProductInfo
        : CslaReadOnlyBase<SiteMapProductInfo>
    {
        public static readonly PropertyInfo<Guid> ProductXTenantLocaleIdProperty = RegisterProperty<Guid>(p => p.ProductXTenantLocaleId);
        public Guid ProductXTenantLocaleId
        {
            get { return GetProperty(ProductXTenantLocaleIdProperty); }
            private set { LoadProperty(ProductXTenantLocaleIdProperty, value); }
        }

        //public static readonly PropertyInfo<Guid> ParentPageIdProperty = RegisterProperty<Guid>(p => p.ParentPageId);
        //public Guid ParentPageId
        //{
        //    get { return GetProperty(ParentPageIdProperty); }
        //    private set { LoadProperty(ParentPageIdProperty, value); }
        //}

        public static readonly PropertyInfo<Guid> CategoryIdProperty = RegisterProperty<Guid>(p => p.CategoryId);
        public Guid CategoryId
        {
            get { return GetProperty(CategoryIdProperty); }
            private set { LoadProperty(CategoryIdProperty, value); }
        }

        //public static readonly PropertyInfo<int> TenantIdProperty = RegisterProperty<int>(p => p.TenantId);
        //public int TenantId
        //{
        //    get { return GetProperty(TenantIdProperty); }
        //    private set { LoadProperty(TenantIdProperty, value); }
        //}

        //public static readonly PropertyInfo<int> LocaleIdProperty = RegisterProperty<int>(p => p.LocaleId);
        //public int LocaleId
        //{
        //    get { return GetProperty(LocaleIdProperty); }
        //    private set { LoadProperty(LocaleIdProperty, value); }
        //}

        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(p => p.Name);
        public string Name
        {
            get { return GetProperty(NameProperty); }
            private set { LoadProperty(NameProperty, value); }
        }

        //public static readonly PropertyInfo<string> UrlProperty = RegisterProperty<string>(p => p.Url);
        //public string Url
        //{
        //    get { return GetProperty(UrlProperty); }
        //    private set { LoadProperty(UrlProperty, value); }
        //}

        //public static readonly PropertyInfo<bool> IsUrlAbsoluteProperty = RegisterProperty<bool>(p => p.IsUrlAbsolute);
        //public bool IsUrlAbsolute
        //{
        //    get { return GetProperty(IsUrlAbsoluteProperty); }
        //    private set { LoadProperty(IsUrlAbsoluteProperty, value); }
        //}

        //public static readonly PropertyInfo<string> ParentPageUrlProperty = RegisterProperty<string>(p => p.ParentPageUrl);
        //public string ParentPageUrl
        //{
        //    get { return GetProperty(ParentPageUrlProperty); }
        //    private set { LoadProperty(ParentPageUrlProperty, value); }
        //}

        public static readonly PropertyInfo<string> MetaRobotsProperty = RegisterProperty<string>(p => p.MetaRobots);
        public string MetaRobots
        {
            get { return GetProperty(MetaRobotsProperty); }
            private set { LoadProperty(MetaRobotsProperty, value); }
        }

        //public static readonly PropertyInfo<string> DefaultCategoryUrlProperty = RegisterProperty<string>(p => p.DefaultCategoryUrl);
        //public string DefaultCategoryUrl
        //{
        //    get { return GetProperty(DefaultCategoryUrlProperty); }
        //    private set { LoadProperty(DefaultCategoryUrlProperty, value); }
        //}

        //public static readonly PropertyInfo<Guid> DefaultCategoryPageIdProperty = RegisterProperty<Guid>(p => p.DefaultCategoryPageId);
        //public Guid DefaultCategoryPageId
        //{
        //    get { return GetProperty(DefaultCategoryPageIdProperty); }
        //    private set { LoadProperty(DefaultCategoryPageIdProperty, value); }
        //}

        #region Constructed Properties

        public static readonly PropertyInfo<string> UrlPathProperty = RegisterProperty<string>(p => p.UrlPath);
        public string UrlPath
        {
            get { return GetProperty(UrlPathProperty); }
            private set { LoadProperty(UrlPathProperty, value); }
        }

        public static readonly PropertyInfo<string> CanonicalUrlPathProperty = RegisterProperty<string>(p => p.CanonicalUrlPath);
        public string CanonicalUrlPath
        {
            get { return GetProperty(CanonicalUrlPathProperty); }
            private set { LoadProperty(CanonicalUrlPathProperty, value); }
        }

        #endregion


        //protected override void AddBusinessRules()
        //{
        //    base.AddBusinessRules();

        //    // Route URL
        //    //BusinessRules.AddRule(new UrlPathProductRule(UrlPathProperty, ParentPageUrlProperty, UrlProperty) { Priority = 1 });
        //    BusinessRules.AddRule(new UrlPathPageRule(UrlPathProperty, UrlProperty, IsUrlAbsoluteProperty, ParentPageIdProperty, TenantIdProperty, LocaleIdProperty, appContext ) { Priority = 1 });
        //    BusinessRules.AddRule(new UrlPathTrailingSlashRule(UrlPathProperty) { Priority = 2 });
        //    BusinessRules.AddRule(new UrlPathLeadingSlashRule(UrlPathProperty) { Priority = 3 });
        //    BusinessRules.AddRule(new UrlPathLocaleRule(UrlPathProperty, LocaleIdProperty, appContext) { Priority = 4 });

        //    // Canonical URL
        //    //BusinessRules.AddRule(new UrlPathProductRule(CanonicalUrlPathProperty, DefaultCategoryUrlProperty, UrlProperty) { Priority = 1 });
        //    BusinessRules.AddRule(new UrlPathPageRule(CanonicalUrlPathProperty, UrlProperty, IsUrlAbsoluteProperty, DefaultCategoryPageIdProperty, TenantIdProperty, LocaleIdProperty, appContext) { Priority = 1 });
        //    BusinessRules.AddRule(new UrlPathTrailingSlashRule(CanonicalUrlPathProperty) { Priority = 2 });
        //    BusinessRules.AddRule(new UrlPathLeadingSlashRule(CanonicalUrlPathProperty) { Priority = 3 });
        //    BusinessRules.AddRule(new UrlPathLocaleRule(CanonicalUrlPathProperty, LocaleIdProperty, appContext) { Priority = 4 });
        //}

        private void Child_Fetch(SiteMapProductDto item)
        {
            ProductXTenantLocaleId = item.ProductXTenantLocaleId;
            //ParentPageId = item.ParentPageId;
            CategoryId = item.CategoryId;
            //TenantId = item.TenantId;
            //LocaleId = item.LocaleId;
            Name = item.Name;
            //Url = item.Url;
            //IsUrlAbsolute = item.IsUrlAbsolute;
            //ParentPageUrl = item.ParentPageUrl;
            MetaRobots = item.MetaRobots;
            //DefaultCategoryUrl = item.DefaultCategoryUrl;
            //DefaultCategoryPageId = item.DefaultCategoryPageId;

            this.UrlPath = urlBuilder.BuildPath(
                item.Url, 
                item.IsUrlAbsolute, 
                item.ParentPageId, 
                appContext.CurrentTenant.Id, 
                appContext.CurrentLocaleId, 
                appContext.CurrentTenant.DefaultLocale.LCID);

            this.CanonicalUrlPath = urlBuilder.BuildPath(
                item.Url, 
                item.IsUrlAbsolute, 
                item.DefaultCategoryPageId, 
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
