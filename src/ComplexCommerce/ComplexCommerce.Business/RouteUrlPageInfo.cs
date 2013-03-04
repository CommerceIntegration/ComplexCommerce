using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using Csla;
using ComplexCommerce.Csla;
using ComplexCommerce.Data.Dto;

namespace ComplexCommerce.Business
{
    [Serializable]
    public class RouteUrlPageInfo
        : CslaReadOnlyBase<RouteUrlPageInfo>
    {

        //public static PropertyInfo<int> LocaleIdProperty = RegisterProperty<int>(c => c.LocaleId);
        //public int LocaleId
        //{
        //    get { return GetProperty(LocaleIdProperty); }
        //    private set { LoadProperty(LocaleIdProperty, value); }
        //}

        public static PropertyInfo<string> RouteUrlProperty = RegisterProperty<string>(c => c.RouteUrl);
        public string RouteUrl
        {
            get { return GetProperty(RouteUrlProperty); }
            private set { LoadProperty(RouteUrlProperty, value); }
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


        // TODO: Move logic into business rule
        private bool IsValidLocaleId(int localeId)
        {
            return
                CultureInfo
                .GetCultures(CultureTypes.SpecificCultures)
                .Any(c => c.LCID == localeId);
        }



        private void Child_Fetch(SiteMapPageDto item, bool defaultUrl)
        {
            if (defaultUrl)
            {
                RouteUrl = item.RouteUrl;
            }
            else
            {
                var locale = appContext.CurrentTenant.DefaultLocale;
                if (IsValidLocaleId(item.LocaleId))
                {
                    locale = new CultureInfo(item.LocaleId);
                }

                // Append the locale name as the first segment of the URL
                RouteUrl = locale.Name + "/" + item.RouteUrl;
            }

            ContentType = (ContentTypeEnum)item.ContentType;
            ContentId = item.ContentId;


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
