using System;
using System.Collections.Generic;
using System.Linq;
using Csla;
using ComplexCommerce.Csla;
using ComplexCommerce.Data.Repositories;
using ComplexCommerce.Data.Dto;

namespace ComplexCommerce.Business.SiteMap
{
    [Serializable]
    public class SiteMapPageList
        : CslaReadOnlyListBase<SiteMapPageList, SiteMapPageTree>
    {
        // Used for nested calls
        private void Child_Fetch(Guid parentId, IEnumerable<SiteMapPageDto> pageList, IEnumerable<SiteMapProductDto> productList)
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            IsReadOnly = false;

            var childPages = pageList.Where(x => x.ParentId == parentId);
            foreach (var page in childPages)
                Add(DataPortal.FetchChild<SiteMapPageTree>(page, pageList, productList));

            IsReadOnly = true;
            RaiseListChangedEvents = rlce;
        }
    }
}
