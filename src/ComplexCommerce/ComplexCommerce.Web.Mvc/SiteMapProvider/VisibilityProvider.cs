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
            if (sourceMetadata["HtmlHelper"].ToString().Equals("MvcSiteMapProvider.Web.Html.MenuHelper"))
            {
                return bool.Parse(node.Attributes["isVisibleOnMainMenu"]);
            }
            return true;
        }

        #endregion
    }
}
