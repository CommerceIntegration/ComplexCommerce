using System;
using System.Linq;
using System.Globalization;
using Csla;
using ComplexCommerce.Csla;
using ComplexCommerce.Data.Dto;

namespace ComplexCommerce.Business
{
    public class RouteUrlProductInfo
        : CslaReadOnlyBase<RouteUrlProductInfo>
    {
        public static PropertyInfo<string> RouteUrlProperty = RegisterProperty<string>(c => c.RouteUrl);
        public string RouteUrl
        {
            get { return GetProperty(RouteUrlProperty); }
            private set { LoadProperty(RouteUrlProperty, value); }
        }

        // Route = {id}
        public static PropertyInfo<Guid> ProductXTenantLocaleIdProperty = RegisterProperty<Guid>(c => c.ProductXTenantLocaleId);
        public Guid ProductXTenantLocaleId
        {
            get { return GetProperty(ProductXTenantLocaleIdProperty); }
            private set { LoadProperty(ProductXTenantLocaleIdProperty, value); }
        }


        // TODO: Move logic into business rule
        private bool IsValidLocaleId(int localeId)
        {
            return
                CultureInfo
                .GetCultures(CultureTypes.SpecificCultures)
                .Any(c => c.LCID == localeId);
        }



        private void Child_Fetch(RouteUrlProductDto item, bool defaultUrl)
        {
            // TODO: Move this logic to business rule
            RouteUrl = item.ParentPageRouteUrl + "/" + item.ProductUrlSlug;
            if (!defaultUrl)
            {
                var locale = appContext.CurrentTenant.DefaultLocale;
                if (IsValidLocaleId(item.LocaleId))
                {
                    locale = new CultureInfo(item.LocaleId);
                }

                // Append the locale name as the first segment of the URL
                RouteUrl = locale.Name + "/" + RouteUrl;
            }

            ProductXTenantLocaleId = item.ProductXTenantLocaleId;


            // Force the PublishActionRule to execute
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

        #endregion

    }
}
