using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using Csla;
using ComplexCommerce.Csla;
using ComplexCommerce.Data.Dto;
using ComplexCommerce.Business.Rules;

namespace ComplexCommerce.Business
{
    [Serializable]
    public class RouteUrlPageInfo
        : CslaReadOnlyBase<RouteUrlPageInfo>
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

        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();

            BusinessRules.AddRule(new UrlPathTrailingSlashRule(RouteUrlProperty) { Priority = 1 });
            BusinessRules.AddRule(new UrlPathLeadingSlashRule(RouteUrlProperty) { Priority = 2 });
            BusinessRules.AddRule(new UrlPathLocaleRule(RouteUrlProperty, LocaleIdProperty, appContext) { Priority = 3 });
        }


        private void Child_Fetch(RouteUrlPageDto item)
        {
            LocaleId = item.LocaleId;
            RouteUrl = item.RouteUrl;
            ContentType = (ContentTypeEnum)item.ContentType;
            ContentId = item.ContentId;

            // Force the Rules to execute
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
