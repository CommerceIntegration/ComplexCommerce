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
        ContentTypeEnum ContentType { get; }
        Guid ContentId { get; }
    }

    [Serializable]
    public class RouteUrlPageInfo
        : CslaReadOnlyBase<RouteUrlPageInfo>, IRouteUrlPageInfo
    {
        //public static PropertyInfo<Guid> ParentIdProperty = RegisterProperty<Guid>(c => c.ParentId);
        //public Guid ParentId
        //{
        //    get { return GetProperty(ParentIdProperty); }
        //    private set { LoadProperty(ParentIdProperty, value); }
        //}

        //public static PropertyInfo<int> TenantIdProperty = RegisterProperty<int>(c => c.TenantId);
        //public int TenantId
        //{
        //    get { return GetProperty(TenantIdProperty); }
        //    private set { LoadProperty(TenantIdProperty, value); }
        //}

        //public static PropertyInfo<int> LocaleIdProperty = RegisterProperty<int>(c => c.LocaleId);
        //public int LocaleId
        //{
        //    get { return GetProperty(LocaleIdProperty); }
        //    private set { LoadProperty(LocaleIdProperty, value); }
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


        #region Calculated Properties

        public static PropertyInfo<string> UrlPathProperty = RegisterProperty<string>(c => c.UrlPath);
        public string UrlPath
        {
            get { return GetProperty(UrlPathProperty); }
            private set { LoadProperty(UrlPathProperty, value); }
        }

        #endregion


        //protected override void AddBusinessRules()
        //{
        //    base.AddBusinessRules();

        //    // IMPORTANT: For pages we need to ensure the trailing slash rule comes after the leading slash rule.

        //    BusinessRules.AddRule(new UrlPathPageRule(UrlPathProperty, UrlProperty, IsUrlAbsoluteProperty, ParentIdProperty, TenantIdProperty, LocaleIdProperty, appContext) { Priority = 1 });
        //    BusinessRules.AddRule(new UrlPathLeadingSlashRule(UrlPathProperty) { Priority = 2 });
        //    BusinessRules.AddRule(new UrlPathTrailingSlashRule(UrlPathProperty) { Priority = 3 });
        //    BusinessRules.AddRule(new UrlPathLocaleRule(UrlPathProperty, LocaleIdProperty, appContext) { Priority = 4 });
        //}


        //private void Child_Fetch(RouteUrlPageDto item)
        //{
        //    ParentId = item.ParentId;
        //    LocaleId = item.LocaleId;
        //    Url = item.Url;
        //    IsUrlAbsolute = item.IsUrlAbsolute;
        //    ContentType = (ContentTypeEnum)item.ContentType;
        //    ContentId = item.ContentId;

        //    // Force the Rules to execute
        //    this.BusinessRules.CheckRules();
        //}

        private void Child_Fetch(ParentUrlPageInfo item)
        {
            //ParentId = item.ParentId;
            //LocaleId = item.LocaleId;
            //Url = item.Url;
            //IsUrlAbsolute = item.IsUrlAbsolute;


            this.UrlPath = urlBuilder.BuildPath(
                item.Url, 
                item.IsUrlAbsolute, 
                item.ParentId, 
                appContext.CurrentTenant.Id, 
                appContext.CurrentLocaleId, 
                appContext.CurrentTenant.DefaultLocale.LCID);

            this.ContentType = (ContentTypeEnum)item.ContentType;
            this.ContentId = item.ContentId;

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
