﻿using System;
using Csla;
using ComplexCommerce.Csla;
using ComplexCommerce.Data.Dto;
using ComplexCommerce.Data.Repositories;
using ComplexCommerce.Business.Context;
using ComplexCommerce.Business.Text;

namespace ComplexCommerce.Business.SiteMap
{
    [Serializable]
    public class SiteMapProductInfo
        : CslaReadOnlyBase<SiteMapProductInfo>
    {
        public static readonly PropertyInfo<Guid> CategoryXProductIdProperty = RegisterProperty<Guid>(p => p.CategoryXProductId);
        public Guid CategoryXProductId
        {
            get { return GetProperty(CategoryXProductIdProperty); }
            private set { LoadProperty(CategoryXProductIdProperty, value); }
        }

        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(p => p.Name);
        public string Name
        {
            get { return GetProperty(NameProperty); }
            private set { LoadProperty(NameProperty, value); }
        }

        public static readonly PropertyInfo<string> MetaRobotsProperty = RegisterProperty<string>(p => p.MetaRobots);
        public string MetaRobots
        {
            get { return GetProperty(MetaRobotsProperty); }
            private set { LoadProperty(MetaRobotsProperty, value); }
        }

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

        private void Child_Fetch(SiteMapProductDto item)
        {
            CategoryXProductId = item.CategoryXProductId;
            Name = item.Name;
            MetaRobots = item.MetaRobots;

            this.UrlPath = urlBuilder.BuildPath(
                item.Url, 
                item.IsUrlAbsolute, 
                item.ParentPageId, 
                item.TenantId,
                item.LocaleId);

            this.CanonicalUrlPath = urlBuilder.BuildPath(
                item.Url, 
                item.IsUrlAbsolute, 
                item.DefaultCategoryPageId, 
                item.TenantId,
                item.LocaleId);
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
