using System;
using System.Collections.Generic;
using Csla;
using Csla.Rules;
using Csla.Core;

namespace ComplexCommerce.Business.Rules
{
    public class UrlPathTrailingSlashRule
        : BusinessRule
    {
        public UrlPathTrailingSlashRule(IPropertyInfo routePathProperty)
            : base(routePathProperty)
        {
            InputProperties = new List<IPropertyInfo> { routePathProperty };
            this.routePathProperty = routePathProperty;
        }

        private IPropertyInfo routePathProperty;

        protected override void Execute(RuleContext context)
        {
            var path = (string)context.InputPropertyValues[routePathProperty];

            if (!path.EndsWith("/"))
            {
                path += "/";
            }

            context.AddOutValue(path);
        }
    }
}
