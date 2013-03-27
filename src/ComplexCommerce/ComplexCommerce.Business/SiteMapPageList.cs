using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csla;
using ComplexCommerce.Csla;
using ComplexCommerce.Data.Repositories;
using ComplexCommerce.Data.Dto;

namespace ComplexCommerce.Business
{
    [Serializable]
    public class SiteMapPageList
        : CslaReadOnlyListBase<SiteMapPageList, SiteMapPageTree>
    {
        // Used for nested calls
        private void Child_Fetch(Guid parentId, IEnumerable<ParentUrlPageInfo> pageList, IEnumerable<SiteMapProductDto> productList, ITenantLocale tenantLocale)
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            IsReadOnly = false;

            var childPages = pageList.Where(x => x.ParentId == parentId);
            foreach (var page in childPages)
                Add(DataPortal.FetchChild<SiteMapPageTree>(page, pageList, productList, tenantLocale));

            IsReadOnly = true;
            RaiseListChangedEvents = rlce;
        }
    }
}
