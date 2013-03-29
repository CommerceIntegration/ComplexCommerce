using System;
using System.Linq;
using System.Linq.Expressions;
using System.Data;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace ComplexCommerce.Data.Entity.Model
{
    /// <summary>
    /// The interface for the generic DB context. This contains all of
    /// the <code>DbContext</code> properties that are implemented in the 
    /// concrete DbContext class. This interface was created so these members
    /// can be mocked, as DbContext doesn't have a default public constructor.
    /// </summary>
    public interface IDbContext
            : IDisposable, IObjectContextAdapter
    {
        DbChangeTracker ChangeTracker { get; }
        DbContextConfiguration Configuration { get; }
        Database Database { get; }
        DbEntityEntry Entry(object entity);
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        bool Equals(object obj);
        int GetHashCode();
        Type GetType();
        IEnumerable<DbEntityValidationResult> GetValidationErrors();
        int SaveChanges();
        DbSet Set(Type entityType);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
