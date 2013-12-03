//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;
using CPC.POS.Database;

namespace CPC.POS.Repository
{
    /// <summary>
    /// Repository for table base_ProductUOM 
    /// </summary>
    public partial class base_ProductUOMRepository
    {
        #region Auto Generate Code

        #region Constructors

        // Default constructor
        public base_ProductUOMRepository()
        {
        }

        #endregion

        #region Basic C.R.U.D. Operations

        /// <summary>
        /// Add new base_ProductUOM.
        /// </summary>
        /// <param name="base_ProductUOM">base_ProductUOM to add.</param>
        /// <returns>base_ProductUOM have been added.</returns>
        public base_ProductUOM Add(base_ProductUOM base_ProductUOM)
        {
            UnitOfWork.Add<base_ProductUOM>(base_ProductUOM);
            return base_ProductUOM;
        }

        /// <summary>
        /// Adds a sequence of new base_ProductUOM.
        /// </summary>
        /// <param name="base_ProductUOM">Sequence of new base_ProductUOM to add.</param>
        /// <returns>Sequence of new base_ProductUOM have been added.</returns>
        public IEnumerable<base_ProductUOM> Add(IEnumerable<base_ProductUOM> base_ProductUOM)
        {
            UnitOfWork.Add<base_ProductUOM>(base_ProductUOM);
            return base_ProductUOM;
        }

        /// <summary>
        /// Delete a existed base_ProductUOM.
        /// </summary>
        /// <param name="base_ProductUOM">base_ProductUOM to delete.</param>
        public void Delete(base_ProductUOM base_ProductUOM)
        {
            Refresh(base_ProductUOM);
            if (base_ProductUOM.EntityState != System.Data.EntityState.Detached)
                UnitOfWork.Delete<base_ProductUOM>(base_ProductUOM);
        }

        /// <summary>
        /// Delete a sequence of existed base_ProductUOM.
        /// </summary>
        /// <param name="base_ProductUOM">Sequence of existed base_ProductUOM to delete.</param>
        public void Delete(IEnumerable<base_ProductUOM> base_ProductUOM)
        {
            int total = base_ProductUOM.Count();
            for (int i = total - 1; i >= 0; i--)
                Delete(base_ProductUOM.ElementAt(i));
        }

        /// <summary>
        /// Returns the first base_ProductUOM of a sequence that satisfies a specified condition or 
        /// a default value if no such base_ProductUOM is found.
        /// </summary>
        /// <param name="expression">A function to test each base_ProductUOM for a condition.</param>
        /// <returns>    
        /// Null if source is empty or if no base_ProductUOM passes the test specified by expression; 
        /// otherwise, the first base_ProductUOM in source that passes the test specified by expression.
        /// </returns>
        public base_ProductUOM Get(Expression<Func<base_ProductUOM, bool>> expression)
        {
            return UnitOfWork.Get<base_ProductUOM>(expression);
        }

        /// <summary>
        /// Get all base_ProductUOM.
        /// </summary>
        /// <returns>The new IList&lt;base_ProductUOM&gt; instance.</returns>
        public IList<base_ProductUOM> GetAll()
        {
            return UnitOfWork.GetAll<base_ProductUOM>();
        }

        /// <summary>
        /// Get all base_ProductUOM that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_ProductUOM for a condition.</param>
        /// <returns>The new IList&lt;base_ProductUOM&gt; instance.</returns>
        public IList<base_ProductUOM> GetAll(Expression<Func<base_ProductUOM, bool>> expression)
        {
            return UnitOfWork.GetAll<base_ProductUOM>(expression);
        }

        /// <summary>
        /// Get all base_ProductUOM.
        /// </summary>
        /// <returns>The new IEnumerable&lt;base_ProductUOM&gt; instance.</returns>
        public IEnumerable<base_ProductUOM> GetIEnumerable()
        {
            return UnitOfWork.GetIEnumerable<base_ProductUOM>();
        }

        /// <summary>
        /// Get all base_ProductUOM that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_ProductUOM for a condition.</param>
        /// <returns>The new IEnumerable&lt;base_ProductUOM&gt; instance.</returns>
        public IEnumerable<base_ProductUOM> GetIEnumerable(Expression<Func<base_ProductUOM, bool>> expression)
        {
            return UnitOfWork.GetIEnumerable<base_ProductUOM>(expression);
        }

        /// <summary>
        /// Get all base_ProductUOM.
        /// </summary>
        /// <returns>The new IQueryable&lt;base_ProductUOM&gt; instance.</returns>
        public IQueryable<base_ProductUOM> GetIQueryable()
        {
            return UnitOfWork.GetIQueryable<base_ProductUOM>();
        }

        /// <summary>
        /// Get all base_ProductUOM that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_ProductUOM for a condition.</param>
        /// <returns>The new IQueryable&lt;base_ProductUOM&gt; instance.</returns>
        public IQueryable<base_ProductUOM> GetIQueryable(Expression<Func<base_ProductUOM, bool>> expression)
        {
            return UnitOfWork.GetIQueryable<base_ProductUOM>(expression);
        }

        /// <summary>
        /// Take a few base_ProductUOM in a sequence was sorted on server.
        /// </summary>
        /// <param name="ignoreCount">Number of base_ProductUOM will ignore.</param>
        /// <param name="takeCount">Number of base_ProductUOM will take.</param>
        /// <param name="keys">The key columns by which to order the results.</param>
        /// <returns>The new IList&lt;base_ProductUOM&gt; instance.</returns>
        public IList<base_ProductUOM> GetRange(int ignoreCount, int takeCount, string keys)
        {
            return UnitOfWork.GetRange<base_ProductUOM>(ignoreCount, takeCount, keys);
        }

        /// <summary>
        /// Take a few base_ProductUOM in a sequence was sorted on server.
        /// </summary>
        /// <param name="ignoreCount">Number of base_ProductUOM will ignore.</param>
        /// <param name="takeCount">Number of base_ProductUOM will take.</param>
        /// <param name="keys">The key columns by which to order the results.</param>
        /// <param name="expression">A function to test each base_ProductUOM for a condition.</param>
        /// <returns>The new IList&lt;base_ProductUOM&gt; instance.</returns>
        public IList<base_ProductUOM> GetRange(int ignoreCount, int takeCount, string keys, Expression<Func<base_ProductUOM, bool>> expression)
        {
            return UnitOfWork.GetRange<base_ProductUOM>(ignoreCount, takeCount, keys, expression);
        }

        /// <summary>
        /// Take a few base_ProductUOM in sequence was sorted by descending on server.
        /// </summary>
        /// <typeparam name="TKey">Type of base_ProductUOM to sort</typeparam>
        /// <param name="ignoreCount">Number of base_ProductUOM will ignore.</param>
        /// <param name="takeCount">Number of base_ProductUOM will take.</param>
        /// <param name="keySelector">The key columns by which to order the results.</param>
        /// <returns>The new IList&lt;base_ProductUOM&gt; instance.</returns>
        public IList<base_ProductUOM> GetRangeDescending<TKey>(int ignoreCount, int takeCount, Expression<Func<base_ProductUOM, TKey>> keySelector)
        {
            return UnitOfWork.GetRangeDescending(ignoreCount, takeCount, keySelector);
        }

        /// <summary>
        /// Take a few base_ProductUOM in sequence was sorted by descending on server.
        /// </summary>
        /// <typeparam name="TKey">Type of base_ProductUOM to sort</typeparam>
        /// <param name="ignoreCount">Number of base_ProductUOM will ignore.</param>
        /// <param name="takeCount">Number of base_ProductUOM will take.</param>
        /// <param name="keySelector">The key columns by which to order the results.</param>
        /// <param name="expression">A function to test each object for a condition.</param>
        /// <returns>The new IList&lt;base_ProductUOM&gt; instance.</returns>
        public IList<base_ProductUOM> GetRangeDescending<TKey>(int ignoreCount, int takeCount, Expression<Func<base_ProductUOM, TKey>> keySelector, Expression<Func<base_ProductUOM, bool>> expression)
        {
            return UnitOfWork.GetRangeDescending(ignoreCount, takeCount, keySelector, expression);
        }

        /// <summary>
        /// Updates an base_ProductUOM in the object context with data from the data source.
        /// </summary>
        /// <param name="base_ProductUOM">The base_ProductUOM to be refreshed.</param>
        public base_ProductUOM Refresh(base_ProductUOM base_ProductUOM)
        {
            UnitOfWork.Refresh<base_ProductUOM>(base_ProductUOM);
            if (base_ProductUOM.EntityState != System.Data.EntityState.Detached)
                return base_ProductUOM;
            return null;
        }

        /// <summary>
        /// Updates a sequence of base_ProductUOM in the object context with data from the data source.
        /// </summary>
        /// <typeparam name="base_ProductUOM">Type of object in a sequence to refresh.</typeparam>
        /// <param name="base_ProductUOM">Object collection to be refreshed.</param>
        public void Refresh(IEnumerable<base_ProductUOM> base_ProductUOM)
        {
            UnitOfWork.Refresh<base_ProductUOM>(base_ProductUOM);
        }

        /// <summary>
        /// Updates a sequence of base_ProductUOM in the object context with data from the data source.
        /// </summary>
        public void Refresh()
        {
            UnitOfWork.Refresh<base_ProductUOM>();
        }

        /// <summary>
        /// Persists all updates to the data source and resets change tracking in the object context.
        /// </summary>
        public void Commit()
        {
            UnitOfWork.Commit();
        }

        /// <summary>
        /// Persists all updates to the data source with the specified System.Data.Objects.SaveOptions.
        /// </summary>
        /// <param name="options">A System.Data.Objects.SaveOptions value that determines the behavior of the operation.</param>
        public void Commit(SaveOptions options)
        {
            UnitOfWork.Commit(options);
        }

        /// <summary>
        /// Starts a database transaction.
        /// </summary>
        public void BeginTransaction()
        {
            UnitOfWork.BeginTransaction();
        }

        /// <summary>
        /// Commits the database transaction.
        /// </summary>
        public void CommitTransaction()
        {
            UnitOfWork.CommitTransaction();
        }

        /// <summary>
        /// Rolls back a transaction from a pending state.
        /// </summary>
        public void RollbackTransaction()
        {
            UnitOfWork.RollbackTransaction();
        }

        #endregion

        #endregion

        #region Custom Code


        #endregion
    }
}
