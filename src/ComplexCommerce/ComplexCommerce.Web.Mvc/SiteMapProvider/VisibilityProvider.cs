using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcSiteMapProvider;

namespace ComplexCommerce.Web.Mvc.SiteMapProvider
{
    public class VisibilityProvider
        : SiteMapNodeVisibilityProviderBase
    {
        #region ISiteMapNodeVisibilityProvider Members

        public override bool IsVisible(ISiteMapNode node, IDictionary<string, object> sourceMetadata)
        {
            if (sourceMetadata.ContainsKey("name"))
            {
                var name = sourceMetadata["name"].ToString();
                var attributeName = "isVisibleOn" + name;
                if (node.Attributes.ContainsKey(attributeName))
                {
                    return bool.Parse(node.Attributes[attributeName]);
                }
            }
            return true;
        }

        #endregion
    }
}
