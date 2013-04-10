using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexCommerce.Web.Mvc.Globalization.ValidationMessages
{
    public class CompositeValidationMessageProvider
        : IValidationMessageProvider
    {
        public CompositeValidationMessageProvider(
            params IValidationMessageProvider[] dataSources
            )
        {
            if (dataSources == null)
                throw new ArgumentNullException("dataSources");
            this.dataSources = dataSources;
        }

        private readonly IValidationMessageProvider[] dataSources;

        #region IValidationMessageProvider Members

        public string GetMessage(IMessageContext context)
        {
            foreach (var dataSource in this.dataSources)
            {
                var msg = dataSource.GetMessage(context);
                if (msg != null)
                    return msg;
            }

            return null;
        }

        #endregion
    }
}
