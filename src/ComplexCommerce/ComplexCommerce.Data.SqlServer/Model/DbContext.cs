//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Data.Entity;

//using System.Data.Objects;
//using System.Data.Entity.Infrastructure;
//using System.Diagnostics.CodeAnalysis;

//namespace ComplexCommerce.Data.SqlServer.Model
//{
//    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Casing is intentional")]
//    public class DbContext : IDisposable, IObjectContextAdapter, ComplexCommerce.Data.SqlServer.Model.IDbContext1
//    {
//        // Fields
//        private InternalContext _internalContext;

//        // Methods
//        protected DbContext()
//        {
//            this.InitializeLazyInternalContext(new LazyInternalConnection(this.GetType().DatabaseName()), null);
//        }

//        protected DbContext(DbCompiledModel model)
//        {
//            RuntimeFailureMethods.Requires(model != null, null, "model != null");
//            this.InitializeLazyInternalContext(new LazyInternalConnection(this.GetType().DatabaseName()), model);
//        }

//        public DbContext(string nameOrConnectionString)
//        {
//            RuntimeFailureMethods.Requires(!string.IsNullOrWhiteSpace(nameOrConnectionString), null, "!string.IsNullOrWhiteSpace(nameOrConnectionString)");
//            this.InitializeLazyInternalContext(new LazyInternalConnection(nameOrConnectionString), null);
//        }

//        public DbContext(DbConnection existingConnection, bool contextOwnsConnection)
//        {
//            RuntimeFailureMethods.Requires(existingConnection != null, null, "existingConnection != null");
//            this.InitializeLazyInternalContext(new EagerInternalConnection(existingConnection, contextOwnsConnection), null);
//        }

//        public DbContext(ObjectContext objectContext, bool dbContextOwnsObjectContext)
//        {
//            RuntimeFailureMethods.Requires(objectContext != null, null, "objectContext != null");
//            this._internalContext = new EagerInternalContext(this, objectContext, dbContextOwnsObjectContext);
//            this.DiscoverAndInitializeSets();
//        }

//        public DbContext(string nameOrConnectionString, DbCompiledModel model)
//        {
//            RuntimeFailureMethods.Requires(!string.IsNullOrWhiteSpace(nameOrConnectionString), null, "!string.IsNullOrWhiteSpace(nameOrConnectionString)");
//            RuntimeFailureMethods.Requires(model != null, null, "model != null");
//            this.InitializeLazyInternalContext(new LazyInternalConnection(nameOrConnectionString), model);
//        }

//        public DbContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection)
//        {
//            RuntimeFailureMethods.Requires(existingConnection != null, null, "existingConnection != null");
//            RuntimeFailureMethods.Requires(model != null, null, "model != null");
//            this.InitializeLazyInternalContext(new EagerInternalConnection(existingConnection, contextOwnsConnection), model);
//        }

//        internal void CallOnModelCreating(DbModelBuilder modelBuilder)
//        {
//            this.OnModelCreating(modelBuilder);
//        }

//        internal virtual DbEntityValidationResult CallValidateEntity(DbEntityEntry entityEntry)
//        {
//            return this.ValidateEntity(entityEntry, new Dictionary<object, object>());
//        }

//        private void DiscoverAndInitializeSets()
//        {
//            new DbSetDiscoveryService(this).InitializeSets();
//        }

//        public void Dispose()
//        {
//            bool disposing = true;
//            this.Dispose(disposing);
//            GC.SuppressFinalize(this);
//        }

//        protected virtual void Dispose(bool disposing)
//        {
//            this.InternalContext.Dispose();
//        }

//        public DbEntityEntry Entry(object entity)
//        {
//            RuntimeFailureMethods.Requires(entity != null, null, "entity != null");
//            return new DbEntityEntry(new InternalEntityEntry(this.InternalContext, entity));
//        }

//        public DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
//        {
//            RuntimeFailureMethods.Requires(entity != null, null, "entity != null");
//            return new DbEntityEntry<TEntity>(new InternalEntityEntry(this.InternalContext, entity));
//        }

//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public override bool Equals(object obj)
//        {
//            return base.Equals(obj);
//        }

//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public override int GetHashCode()
//        {
//            return base.GetHashCode();
//        }

//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public Type GetType()
//        {
//            return base.GetType();
//        }

//        public IEnumerable<DbEntityValidationResult> GetValidationErrors()
//        {
//            List<DbEntityValidationResult> list = new List<DbEntityValidationResult>();
//            foreach (DbEntityEntry entry in this.ChangeTracker.Entries())
//            {
//                if ((entry.InternalEntry.EntityType != typeof(EdmMetadata)) && this.ShouldValidateEntity(entry))
//                {
//                    DbEntityValidationResult item = this.ValidateEntity(entry, new Dictionary<object, object>());
//                    if ((item != null) && !item.IsValid)
//                    {
//                        list.Add(item);
//                    }
//                }
//            }
//            return list;
//        }

//        private void InitializeLazyInternalContext(IInternalConnection internalConnection, DbCompiledModel model = null)
//        {
//            this._internalContext = new LazyInternalContext(this, internalConnection, model);
//            this.DiscoverAndInitializeSets();
//        }

//        protected virtual void OnModelCreating(DbModelBuilder modelBuilder)
//        {
//        }

//        public virtual int SaveChanges()
//        {
//            return this.InternalContext.SaveChanges();
//        }

//        public DbSet<TEntity> Set<TEntity>() where TEntity : class
//        {
//            return (DbSet<TEntity>)this.InternalContext.Set<TEntity>();
//        }

//        public DbSet Set(Type entityType)
//        {
//            if (__ContractsRuntime.insideContractEvaluation <= 4)
//            {
//                try
//                {
//                    __ContractsRuntime.insideContractEvaluation++;
//                    RuntimeFailureMethods.Requires(entityType != null, null, "entityType != null");
//                }
//                finally
//                {
//                    __ContractsRuntime.insideContractEvaluation--;
//                }
//            }
//            return (DbSet)this.InternalContext.Set(entityType);
//        }

//        protected virtual bool ShouldValidateEntity(DbEntityEntry entityEntry)
//        {
//            RuntimeFailureMethods.Requires(entityEntry != null, null, "entityEntry != null");
//            return ((entityEntry.State & (EntityState.Modified | EntityState.Added)) != 0);
//        }

//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public override string ToString()
//        {
//            return base.ToString();
//        }

//        protected virtual DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items)
//        {
//            RuntimeFailureMethods.Requires(entityEntry != null, null, "entityEntry != null");
//            return entityEntry.InternalEntry.GetValidationResult(items);
//        }

//        // Properties
//        public DbChangeTracker ChangeTracker
//        {
//            get
//            {
//                return new DbChangeTracker(this.InternalContext);
//            }
//        }

//        public DbContextConfiguration Configuration
//        {
//            get
//            {
//                return new DbContextConfiguration(this.InternalContext);
//            }
//        }

//        public Database Database
//        {
//            get
//            {
//                return new Database(this.InternalContext);
//            }
//        }

//        internal virtual InternalContext InternalContext
//        {
//            get
//            {
//                return this._internalContext;
//            }
//        }

//        ObjectContext IObjectContextAdapter.ObjectContext
//        {
//            get
//            {
//                this.InternalContext.ForceOSpaceLoadingForKnownEntityTypes();
//                return this.InternalContext.ObjectContext;
//            }
//        }
//    }

 

//}
