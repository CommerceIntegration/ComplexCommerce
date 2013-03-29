using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;
using ComplexCommerce.Data.Context;

namespace ComplexCommerce.Data.Entity.Context
{
    public class PersistenceDetails 
        : IPersistenceDetails
    {
        private readonly string location;

        public PersistenceDetails(string location)
        {
            //Contract.Requires<ArgumentNullException>(string.IsNullOrEmpty(location));
            if (string.IsNullOrEmpty(location))
                throw new ArgumentNullException("location");

            this.location = location;
        }

        #region IPersistenceDetails Members

        public string Location
        {
            get { return this.location; }
        }

        #endregion
    }
}
