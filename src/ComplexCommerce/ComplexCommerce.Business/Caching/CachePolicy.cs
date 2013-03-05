using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexCommerce.Business.Caching
{
    public class CachePolicy : ComplexCommerce.Business.Caching.ICachePolicy
    {
        private TimeSpan absoluteExpiration = TimeSpan.MinValue;
        private TimeSpan slidingExpiration = TimeSpan.MinValue;

        public TimeSpan AbsoluteExpiration 
        { 
            get { return absoluteExpiration; }
            set { absoluteExpiration = value; }
        }
        
        public TimeSpan SlidingExpiration 
        { 
            get { return slidingExpiration; }
            set { slidingExpiration = value; }
        }
    }
}
