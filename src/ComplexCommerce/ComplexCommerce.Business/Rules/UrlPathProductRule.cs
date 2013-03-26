//using System;
//using System.Collections.Generic;
//using Csla;
//using Csla.Rules;
//using Csla.Core;

//namespace ComplexCommerce.Business.Rules
//{
//    public class UrlPathProductRule
//        : BusinessRule
//    {
//        public UrlPathProductRule(
//            IPropertyInfo urlPathProperty,
//            IPropertyInfo productUrlProperty,
//            IPropertyInfo isProductUrl
//            IPropertyInfo parentPageRoutePathProperty, 
//            )
//            : base(urlPathProperty)
//        {
//            InputProperties = new List<IPropertyInfo> { urlPathProperty, parentPageRoutePathProperty, productUrlSlugProperty };
//            this.parentPageRoutePathProperty = parentPageRoutePathProperty;
//            this.productUrlSlugProperty = productUrlSlugProperty;
//        }

//        private readonly IPropertyInfo parentPageRoutePathProperty;
//        private readonly IPropertyInfo productUrlSlugProperty;

//        protected override void Execute(RuleContext context)
//        {
//            var parentPath = (string)context.InputPropertyValues[parentPageRoutePathProperty];
//            var slug = (string)context.InputPropertyValues[productUrlSlugProperty];
//            string path = slug;

//            if (!string.IsNullOrEmpty(parentPath))
//            {
//                // Join the parent path with the product slug,
//                // being mindful of the forward slashes.
//                bool firstHasSlash = parentPath.EndsWith("/");
//                bool lastHasSlash = slug.StartsWith("/");
//                if (!firstHasSlash && !lastHasSlash)
//                {
//                    path = parentPath + "/" + slug;
//                }
//                else if (firstHasSlash && lastHasSlash)
//                {
//                    path = parentPath + slug.Substring(1);
//                }
//                else
//                {
//                    path = parentPath + slug;
//                }
//            }

//            context.AddOutValue(path);
//        }



//        //public UrlPathProductRule(IPropertyInfo routePathProperty, IPropertyInfo parentPageRoutePathProperty, IPropertyInfo productUrlSlugProperty)
//        //    : base(routePathProperty)
//        //{
//        //    InputProperties = new List<IPropertyInfo> { routePathProperty, parentPageRoutePathProperty, productUrlSlugProperty };
//        //    this.parentPageRoutePathProperty = parentPageRoutePathProperty;
//        //    this.productUrlSlugProperty = productUrlSlugProperty;
//        //}

//        //private readonly IPropertyInfo parentPageRoutePathProperty;
//        //private readonly IPropertyInfo productUrlSlugProperty;

//        //protected override void Execute(RuleContext context)
//        //{
//        //    var parentPath = (string)context.InputPropertyValues[parentPageRoutePathProperty];
//        //    var slug = (string)context.InputPropertyValues[productUrlSlugProperty];
//        //    string path = slug;

//        //    if (!string.IsNullOrEmpty(parentPath))
//        //    {
//        //        // Join the parent path with the product slug,
//        //        // being mindful of the forward slashes.
//        //        bool firstHasSlash = parentPath.EndsWith("/");
//        //        bool lastHasSlash = slug.StartsWith("/");
//        //        if (!firstHasSlash && !lastHasSlash)
//        //        {
//        //            path = parentPath + "/" + slug;
//        //        }
//        //        else if (firstHasSlash && lastHasSlash)
//        //        {
//        //            path = parentPath + slug.Substring(1);
//        //        }
//        //        else
//        //        {
//        //            path = parentPath + slug;
//        //        }
//        //    }

//        //    context.AddOutValue(path);
//        //}

//        private string JoinUrlSegments(string firstSegment, string secondSegment)
//        {
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
