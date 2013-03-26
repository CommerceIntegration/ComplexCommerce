//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Linq;
//using Csla;
//using Csla.Rules;
//using Csla.Core;
//using ComplexCommerce.Business.Context;

//namespace ComplexCommerce.Business.Rules
//{
//    public class UrlPathLocaleRule
//        : BusinessRule
//    {
//        public UrlPathLocaleRule(IPropertyInfo routePathProperty, IPropertyInfo localeIdProperty, IApplicationContext appContext)
//            : base(routePathProperty)
//        {
//            //if (appContext == null)
//            //    throw new ArgumentNullException("appContext");

//            InputProperties = new List<IPropertyInfo> { routePathProperty, localeIdProperty };
//            this.routePathProperty = routePathProperty;
//            this.localeIdProperty = localeIdProperty;
//            this.appContext = appContext;
//        }

//        private readonly IPropertyInfo routePathProperty;
//        private readonly IPropertyInfo localeIdProperty;
//        private readonly IApplicationContext appContext;

//        protected override void Execute(RuleContext context)
//        {
//            var path = (string)context.InputPropertyValues[routePathProperty];
//            var localeId = (int)context.InputPropertyValues[localeIdProperty];

//            var defaultLocale = appContext.CurrentTenant.DefaultLocale;

//            // If this is the default locale, leave the path alone
//            if (localeId != defaultLocale.LCID && IsValidLocaleId(localeId))
//            {
//                var locale = new CultureInfo(localeId);

//                // We assume here that the slug already contains a forward slash -
//                // the UrlPathForwardSlashRule must be run before this one.
//                path = "/" + locale.Name + path;
//            }

//            context.AddOutValue(path);
//        }

//        private bool IsValidLocaleId(int localeId)
//        {
//            return
//                CultureInfo
//                .GetCultures(CultureTypes.SpecificCultures)
//                .Any(c => c.LCID == localeId);
//        }
//    }
//}
