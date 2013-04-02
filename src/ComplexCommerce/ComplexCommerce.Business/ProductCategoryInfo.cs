using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csla;
using ComplexCommerce.Csla;
using ComplexCommerce.Data.Dto;

namespace ComplexCommerce.Business
{
    public class ProductCategoryInfo
        : CslaReadOnlyBase<ProductCategoryInfo>
    {
        //public static PropertyInfo<Guid> PageLocaleIdProperty = RegisterProperty<Guid>(c => c.PageLocaleId);
        //public Guid PageLocaleId
        //{
        //    get { return GetProperty(PageLocaleIdProperty); }
        //    private set { LoadProperty(PageLocaleIdProperty, value); }
        //}

        public static PropertyInfo<Guid> PageIdProperty = RegisterProperty<Guid>(c => c.PageId);
        public Guid PageId
        {
            get { return GetProperty(PageIdProperty); }
            private set { LoadProperty(PageIdProperty, value); }
        }


        private void Child_Fetch(ProductCategoryDto item)
        {
            this.PageId = item.PageId;
        }
    }
}
