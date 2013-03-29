using System;
using System.Linq;
using System.Data;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;

namespace ComplexCommerce.Data.Entity.Model
{
    /// <summary>
    /// Additional extension for interface <code>IQueryableExtension</code>, to
    /// allow includes on <code>IObjectSet</code> when using mocking contexts.
    /// </summary>
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Include<T>
            (this IQueryable<T> source, string path)
            where T : class
        {
            ObjectQuery<T> objectQuery = source as ObjectQuery<T>;
            if (objectQuery != null)
            {
                return objectQuery.Include(path);
            }
            return source;
        }
    }
}
