//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Data.Entity.Infrastructure;
//using System.Data.Entity;
//using System.Data.Objects;
//using System.Data.Entity.Validation;

//namespace ComplexCommerce.Data.SqlServer.Model
//{
//    class DbContextMockBase
//        : IDbContext
//    {
//        //private IDbContext _dbContext;

        


//        #region IDbContext Members

//        public DbChangeTracker ChangeTracker
//        {
//            get { return _dbContext.ChangeTracker; }
//        }

//        public DbContextConfiguration Configuration
//        {
//            get { return _dbContext.Configuration; }
//        }

//        public Database Database
//        {
//            get { return _dbContext.Database; }
//        }

//        public DbEntityEntry Entry(object entity)
//        {
//            return _dbContext.Entry(entity);
//        }

//        public DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
//        {
//            return _dbContext.Entry<TEntity>(entity);
//        }

//        public IEnumerable<DbEntityValidationResult> GetValidationErrors()
//        {
//            return _dbContext.GetValidationErrors();
//        }

//        public DbSet Set(Type entityType)
//        {
//            return _dbContext.Set(entityType);
//        }

//        public DbSet<TEntity> Set<TEntity>() where TEntity : class
//        {
//            return _dbContext.Set<TEntity>();
//        }

//        #endregion

//        #region IObjectContextAdapter Members

//        public ObjectContext ObjectContext
//        {
//            get { return _dbContext.ObjectContext; }
//        }

//        #endregion
//    }
//}
