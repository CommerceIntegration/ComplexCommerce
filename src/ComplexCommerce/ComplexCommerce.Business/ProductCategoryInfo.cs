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
        public static PropertyInfo<Guid> PageLocaleIdProperty = RegisterProperty<Guid>(c => c.PageLocaleId);
        public Guid PageLocaleId
        {
            get { return GetProperty(PageLocaleIdProperty); }
            private set { LoadProperty(PageLocaleIdProperty, value); }
        }

        private void Child_Fetch(ProductCategoryDto item)
        {
            this.PageLocaleId = item.PageLocaleId;
        }
    }
}
