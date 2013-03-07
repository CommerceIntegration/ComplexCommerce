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
        private void Child_Fetch(Guid parentId, IEnumerable<SiteMapPageDto> list)
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            IsReadOnly = false;
            var childList = list.Where(x => x.ParentId == parentId);
            foreach (var item in childList)
            {
                Add(DataPortal.FetchChild<SiteMapPageTree>(item, list));
            }
            IsReadOnly = true;
            RaiseListChangedEvents = rlce;
        }
    }
}
