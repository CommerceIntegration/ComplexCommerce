﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Data.Entity;
//using System.Collections.ObjectModel;
//using System.Collections;
//using System.Linq.Expressions;

//namespace ComplexCommerce.Data.SqlServer.Model
//{
//    /// <summary>
//    /// Concrete data set for use with Mock contexts. Implements all of the
//    /// required interfaces, but performs no database functionality; instead
//    /// merely stores the data for testing.
//    /// </summary>
//    public partial class MockDbSet<T>
//        : IDbSet<T>
//        where T : class
//    {
//        //private readonly IList<T> _container = new List<T>();
//        private readonly ObservableCollection<T> _container = new ObservableCollection<T>();

//        #region IDbSet<T> Members

//        public virtual T Add(T entity)
//        {
//            _container.Add(entity);
//            return entity;
//        }

//        public virtual T Attach(T entity)
//        {
//            _container.Add(entity);
//            return entity;
//        }

//        public virtual TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
//        {
//            return (TDerivedEntity)Activator.CreateInstance(typeof(TDerivedEntity));
//        }

//        public virtual T Create()
//        {
//            return (T)Activator.CreateInstance(typeof(T));
//        }

//        public virtual T Find(params object[] keyValues)
//        {
//            throw new NotImplementedException();
//        }

//        public virtual ObservableCollection<T> Local
//        {
//            get { return _container; }
//        }

//        public virtual T Remove(T entity)
//        {
//            _container.Remove(entity);
//            return entity;
//        }

//        #endregion

//        #region IEnumerable<T> Members

//        public virtual IEnumerator<T> GetEnumerator()
//        {
//            return _container.GetEnumerator();
//        }

//        #endregion

//        #region IEnumerable Members

//        virtual IEnumerator IEnumerable.GetEnumerator()
//        {
//            return _container.GetEnumerator();
//        }

//        #endregion

//        #region IQueryable Members

//        public virtual Type ElementType
//        {
//            get { return _container.AsQueryable<T>().ElementType; }
//        }

//        public virtual Expression Expression
//        {
//            get { return _container.AsQueryable<T>().Expression; }
//        }

//        public virtual IQueryProvider Provider
//        {
//            get { return _container.AsQueryable<T>().Provider; }
//        }

//        #endregion
//    }
//}
