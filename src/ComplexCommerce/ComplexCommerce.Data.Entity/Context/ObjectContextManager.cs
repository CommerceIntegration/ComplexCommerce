using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csla;
using ComplexCommerce.Data.Entity.Model;

namespace ComplexCommerce.Data.Entity.Context
{
    /// <summary>
    /// Provides an automated way to reuse 
    /// Entity Framework object context objects 
    /// within the context
    /// of a single data portal operation.
    /// </summary>
    /// <remarks>
    /// This type stores the object context object 
    /// in <see cref="Csla.ApplicationContext.LocalContext" />
    /// and uses reference counting through
    /// <see cref="IDisposable" /> to keep the data context object
    /// open for reuse by child objects, and to automatically
    /// dispose the object when the last consumer
    /// has called Dispose."
    /// </remarks>
    public class ObjectContextManager 
        : IObjectContextManager
    {
        private static object _lock = new object();
        private IComplexCommerce _context;
        private string _connectionString;
        private string _label;

        /// <summary>
        /// Gets the ObjectContextManager object for the 
        /// specified database.
        /// </summary>
        /// <param name="connectionString">
        /// A database connection string.
        /// </param>
        public static IObjectContextManager GetManager(string connectionString)
        {
            return GetManager(connectionString, "default");
        }

        /// <summary>
        /// Gets the ObjectContextManager object for the 
        /// specified database.
        /// </summary>
        /// <param name="connectionString">
        /// A database connection string.
        /// </param>
        /// <param name="label">Label for this context.</param>
        public static IObjectContextManager GetManager(string connectionString, string label)
        {
            lock (_lock)
            {
                var contextLabel = GetContextName(connectionString, label);
                ObjectContextManager mgr = null;
                if (ApplicationContext.LocalContext.Contains(contextLabel))
                {
                    mgr = (ObjectContextManager)(ApplicationContext.LocalContext[contextLabel]);

                }
                else
                {
                    mgr = new ObjectContextManager(connectionString, label);
                    ApplicationContext.LocalContext[contextLabel] = mgr;
                }
                mgr.AddRef();
                return (IObjectContextManager)mgr;
            }
        }

        // Test to see if StructureMap is choking on the fact this has no constructr
        public ObjectContextManager()
        {
        }


        private ObjectContextManager(string connectionString, string label)
        {
            _label = label;
            _connectionString = connectionString;

            _context = new Model.ComplexCommerce(connectionString);
            //_context.Connection.Open();

            //_context = new Model.ComplexCommerce();
            //_context.Database.Connection.Open();
        }

        private static string GetContextName(string connectionString, string label)
        {
            return "__octx:" + label + "-" + connectionString;
        }


        /// <summary>
        /// Gets the EF object context object.
        /// </summary>
        public IComplexCommerce ObjectContext
        {
            get
            {
                return _context;
            }
        }

        #region  Reference counting

        private int _refCount;

        /// <summary>
        /// Gets the current reference count for this
        /// object.
        /// </summary>
        public int RefCount
        {
            get { return _refCount; }
        }

        private void AddRef()
        {
            _refCount += 1;
        }

        private void DeRef()
        {

            lock (_lock)
            {
                _refCount -= 1;
                if (_refCount == 0)
                {
                    //_context.Connection.Close();
                    _context.Database.Connection.Close();
                    _context.Dispose();
                    ApplicationContext.LocalContext.Remove(GetContextName(_connectionString, _label));
                }
            }

        }

        #endregion

        #region  IDisposable

        /// <summary>
        /// Dispose object, dereferencing or
        /// disposing the context it is
        /// managing.
        /// </summary>
        public void Dispose()
        {
            DeRef();
        }

        #endregion

    }
}
