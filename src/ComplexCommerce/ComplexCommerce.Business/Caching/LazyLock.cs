using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexCommerce.Business.Caching
{
    sealed class LazyLock
    {
        private volatile bool got;
        private object value;

        public TValue Get<TValue>(Func<TValue> activator)
        {
            if (!got)
            {
                lock (this)
                {
                    if (!got)
                    {
                        value = activator();

                        got = true;
                    }
                }
            }

            return (TValue)value;
        }
    }
}
