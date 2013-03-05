using System;

namespace ComplexCommerce.Business.Caching
{
    public interface ICachePolicy
    {
        TimeSpan AbsoluteExpiration { get; set; }
        TimeSpan SlidingExpiration { get; set; }
    }
}
