//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Data.Entity.Validation;
//using System.Data.Entity.Infrastructure;


//namespace ComplexCommerce.Data.SqlServer.Model
//{
//    interface IDbContext1
//    {
//        DbChangeTracker ChangeTracker { get; }
//        DbContextConfiguration Configuration { get; }
//        Database Database { get; }
//        DbEntityEntry Entry(object entity);
//        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
//        bool Equals(object obj);
//        int GetHashCode();
//        Type GetType();
//        IEnumerable<DbEntityValidationResult> GetValidationErrors();
//        int SaveChanges();
//        DbSet Set(Type entityType);
//        DbSet<TEntity> Set<TEntity>() where TEntity : class;
//    }
//}
