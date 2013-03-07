using System;
using System.Linq;
using System.Collections.Generic;
using Csla;
using ComplexCommerce.Csla;
using ComplexCommerce.Data.Dto;

namespace ComplexCommerce.Business
{
    [Serializable]
    public class SiteMapProductList
        : CslaReadOnlyListBase<SiteMapProductList, SiteMapProductInfo>
    {
        private void Child_Fetch(Guid categoryId, IEnumerable<SiteMapProductDto> productList)
        {
            using (var ctx = ContextFactory.GetContext())
            {
                var rlce = RaiseListChangedEvents;
                RaiseListChangedEvents = false;
                IsReadOnly = false;

                var category = productList.Where(x => x.CategoryId == categoryId);
                foreach (var product in category)
                    Add(DataPortal.FetchChild<SiteMapProductInfo>(product));

                IsReadOnly = true;
                RaiseListChangedEvents = rlce;
            }
        }
    }
}
