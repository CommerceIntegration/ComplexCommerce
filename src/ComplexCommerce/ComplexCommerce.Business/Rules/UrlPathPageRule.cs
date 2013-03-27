//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Csla;
//using Csla.Rules;
//using Csla.Core;
//using ComplexCommerce.Business.Context;
//using ComplexCommerce.Data.Dto;
//using ComplexCommerce.Business.Text;

//namespace ComplexCommerce.Business.Rules
//{
//    public class UrlPathPageRule
//        : BusinessRule
//    {
//        public UrlPathPageRule(
//            IPropertyInfo urlPathProperty,
//            IPropertyInfo urlProperty,
//            IPropertyInfo isUrlAbsoluteProperty,
//            IPropertyInfo parentPageIdProperty,
//            IPropertyInfo tenantIdProperty,
//            IPropertyInfo localeIdProperty,
//            IParentUrlPageListFactory parentUrlPageListFactory
//            )
//            : base(urlPathProperty)
//        {
//            if (urlPathProperty == null)
//                throw new ArgumentNullException("urlPathProperty");
//            if (urlProperty == null)
//                throw new ArgumentNullException("urlProperty");
//            if (isUrlAbsoluteProperty == null)
//                throw new ArgumentNullException("isUrlAbsoluteProperty");
//            if (parentPageIdProperty == null)
//                throw new ArgumentNullException("parentPageIdProperty");
//            if (tenantIdProperty == null)
//                throw new ArgumentNullException("tenantIdProperty");
//            if (localeIdProperty == null)
//                throw new ArgumentNullException("localeIdProperty");
//            if (parentUrlPageListFactory == null)
//                throw new ArgumentNullException("parentUrlPageListFactory");

//            this.urlProperty = urlProperty;
//            this.isUrlAbsoluteProperty = isUrlAbsoluteProperty;
//            this.parentPageIdProperty = parentPageIdProperty;
//            this.tenantIdProperty = tenantIdProperty;
//            this.localeIdProperty = localeIdProperty;
//            this.parentUrlPageListFactory = parentUrlPageListFactory;

//            InputProperties = new List<IPropertyInfo> { urlPathProperty, urlProperty, isUrlAbsoluteProperty, parentPageIdProperty, tenantIdProperty, localeIdProperty };
//        }

//        private readonly IPropertyInfo urlProperty;
//        private readonly IPropertyInfo isUrlAbsoluteProperty;
//        private readonly IPropertyInfo parentPageIdProperty;
//        private readonly IPropertyInfo tenantIdProperty;
//        private readonly IPropertyInfo localeIdProperty;
//        private readonly IParentUrlPageListFactory parentUrlPageListFactory;

//        protected override void Execute(RuleContext context)
//        {
//            var url = (string)context.InputPropertyValues[urlProperty];
//            var isUrlAbsolute = (bool)context.InputPropertyValues[isUrlAbsoluteProperty];
//            var parentPageId = (Guid)context.InputPropertyValues[parentPageIdProperty];
//            var tenantId = (int)context.InputPropertyValues[tenantIdProperty];
//            var localeId = (int)context.InputPropertyValues[localeIdProperty];

//            var result = url;

//            if (!isUrlAbsolute)
//            {
//                // This list is pulled from the request cache.
//                var pageList = parentUrlPageListFactory.GetParentUrlPageList(tenantId, localeId);
//                var parentUrl = GetParentPageUrl(parentPageId, pageList);
//                result = JoinUrlSegments(parentUrl, url);
//            }
//            context.AddOutValue(result);
//        }

//        private string GetParentPageUrl(Guid parentPageId, ParentUrlPageList pageList)
//        {   
//            var result = "";
//            if (!parentPageId.Equals(Guid.Empty))
//            {
//                var parentPage = pageList.FirstOrDefault(x => x.Id == parentPageId);
//                if (parentPage != null)
//                {
//                    if (!parentPage.IsUrlAbsolute)
//                    {
//                        result = JoinUrlSegments(GetParentPageUrl(parentPage.ParentId, pageList), parentPage.Url);
//                    }
//                    else
//                    {
//                        result = parentPage.Url;
//                    }
//                }
//            }
//            return result;
//        }

//        private string JoinUrlSegments(string firstSegment, string secondSegment)
//        {
//            if (String.IsNullOrEmpty(firstSegment))
//            {
//                return secondSegment;
//            }
//            if (String.IsNullOrEmpty(secondSegment))
//            {
//                return firstSegment;
//            }
//            bool firstHasSlash = firstSegment.EndsWith("/");
//            bool lastHasSlash = secondSegment.StartsWith("/");
//            if (!firstHasSlash && !lastHasSlash)
//            {
//                return firstSegment + "/" + secondSegment;
//            }
//            else if (firstHasSlash && lastHasSlash)
//            {
//                return firstSegment + secondSegment.Substring(1);
//            }
//            else
//            {
//                return firstSegment + secondSegment;
//            }
//        }
//    }
//}
