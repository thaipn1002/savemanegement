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
    /// Repository for table base_PricingManager 
    /// </summary>
    public partial class base_PricingManagerRepository
    {
        #region Auto Generate Code

        #region Constructors

        // Default constructor
        public base_PricingManagerRepository()
        {
        }

        #endregion

        #region Basic C.R.U.D. Operations

        /// <summary>
        /// Add new base_PricingManager.
        /// </summary>
        /// <param name="base_PricingManager">base_PricingManager to add.</param>
        /// <returns>base_PricingManager have been added.</returns>
        public base_PricingManager Add(base_PricingManager base_PricingManager)
        {
            UnitOfWork.Add<base_PricingManager>(base_PricingManager);
            return base_PricingManager;
        }

        /// <summary>
        /// Adds a sequence of new base_PricingManager.
        /// </summary>
        /// <param name="base_PricingManager">Sequence of new base_PricingManager to add.</param>
        /// <returns>Sequence of new base_PricingManager have been added.</returns>
        public IEnumerable<base_PricingManager> Add(IEnumerable<base_PricingManager> base_PricingManager)
        {
            UnitOfWork.Add<base_PricingManager>(base_PricingManager);
            return base_PricingManager;
        }

        /// <summary>
        /// Delete a existed base_PricingManager.
        /// </summary>
        /// <param name="base_PricingManager">base_PricingManager to delete.</param>
        public void Delete(base_PricingManager base_PricingManager)
        {
            Refresh(base_PricingManager);
            if (base_PricingManager.EntityState != System.Data.EntityState.Detached)
                UnitOfWork.Delete<base_PricingManager>(base_PricingManager);
        }

        /// <summary>
        /// Delete a sequence of existed base_PricingManager.
        /// </summary>
        /// <param name="base_PricingManager">Sequence of existed base_PricingManager to delete.</param>
        public void Delete(IEnumerable<base_PricingManager> base_PricingManager)
        {
            int total = base_PricingManager.Count();
            for (int i = total - 1; i >= 0; i--)
                Delete(base_PricingManager.ElementAt(i));
        }

        /// <summary>
        /// Returns the first base_PricingManager of a sequence that satisfies a specified condition or 
        /// a default value if no such base_PricingManager is found.
        /// </summary>
        /// <param name="expression">A function to test each base_PricingManager for a condition.</param>
        /// <returns>    
        /// Null if source is empty or if no base_PricingManager passes the test specified by expression; 
        /// otherwise, the first base_PricingManager in source that passes the test specified by expression.
        /// </returns>
        public base_PricingManager Get(Expression<Func<base_PricingManager, bool>> expression)
        {
            return UnitOfWork.Get<base_PricingManager>(expression);
        }

        /// <summary>
        /// Get all base_PricingManager.
        /// </summary>
        /// <returns>The new IList&lt;base_PricingManager&gt; instance.</returns>
        public IList<base_PricingManager> GetAll()
        {
            return UnitOfWork.GetAll<base_PricingManager>();
        }

        /// <summary>
        /// Get all base_PricingManager that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_PricingManager for a condition.</param>
        /// <returns>The new IList&lt;base_PricingManager&gt; instance.</returns>
        public IList<base_PricingManager> GetAll(Expression<Func<base_PricingManager, bool>> expression)
        {
            return UnitOfWork.GetAll<base_PricingManager>(expression);
        }

        /// <summary>
        /// Get all base_PricingManager.
        /// </summary>
        /// <returns>The new IEnumerable&lt;base_PricingManager&gt; instance.</returns>
        public IEnumerable<base_PricingManager> GetIEnumerable()
        {
            return UnitOfWork.GetIEnumerable<base_PricingManager>();
        }

        /// <summary>
        /// Get all base_PricingManager that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_PricingManager for a condition.</param>
        /// <returns>The new IEnumerable&lt;base_PricingManager&gt; instance.</returns>
        public IEnumerable<base_PricingManager> GetIEnumerable(Expression<Func<base_PricingManager, bool>> expression)
        {
            return UnitOfWork.GetIEnumerable<base_PricingManager>(expression);
        }

        /// <summary>
        /// Get all base_PricingManager.
        /// </summary>
        /// <returns>The new IQueryable&lt;base_PricingManager&gt; instance.</returns>
        public IQueryable<base_PricingManager> GetIQueryable()
        {
            return UnitOfWork.GetIQueryable<base_PricingManager>();
        }

        /// <summary>
        /// Get all base_PricingManager that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_PricingManager for a condition.</param>
        /// <returns>The new IQueryable&lt;base_PricingManager&gt; instance.</returns>
        public IQueryable<base_PricingManager> GetIQueryable(Expression<Func<base_PricingManager, bool>> expression)
        {
            return UnitOfWork.GetIQueryable<base_PricingManager>(expression);
        }

        /// <summary>
        /// Take a few base_PricingManager in a sequence was sorted on server.
        /// </summary>
        /// <param name="ignoreCount">Number of base_PricingManager will ignore.</param>
        /// <param name="takeCount">Number of base_PricingManager will take.</param>
        /// <param name="keys">The key columns by which to order the results.</param>
        /// <returns>The new IList&lt;base_PricingManager&gt; instance.</returns>
        public IList<base_PricingManager> GetRange(int ignoreCount, int takeCount, string keys)
        {
            return UnitOfWork.GetRange<base_PricingManager>(ignoreCount, takeCount, keys);
        }

        /// <summary>
        /// Take a few base_PricingManager in a sequence was sorted on server.
        /// </summary>
        /// <param name="ignoreCount">Number of base_PricingManager will ignore.</param>
        /// <param name="takeCount">Number of base_PricingManager will take.</param>
        /// <param name="keys">The key columns by which to order the results.</param>
        /// <param name="expression">A function to test each base_PricingManager for a condition.</param>
        /// <returns>The new IList&lt;base_PricingManager&gt; instance.</returns>
        public IList<base_PricingManager> GetRange(int ignoreCount, int takeCount, string keys, Expression<Func<base_PricingManager, bool>> expression)
        {
            return UnitOfWork.GetRange<base_PricingManager>(ignoreCount, takeCount, keys, expression);
        }

        /// <summary>
        /// Take a few base_PricingManager in sequence was sorted by descending on server.
        /// </summary>
        /// <typeparam name="TKey">Type of base_PricingManager to sort</typeparam>
        /// <param name="ignoreCount">Number of base_PricingManager will ignore.</param>
        /// <param name="takeCount">Number of base_PricingManager will take.</param>
        /// <param name="keySelector">The key columns by which to order the results.</param>
        /// <returns>The new IList&lt;base_PricingManager&gt; instance.</returns>
        public IList<base_PricingManager> GetRangeDescending<TKey>(int ignoreCount, int takeCount, Expression<Func<base_PricingManager, TKey>> keySelector)
        {
            return UnitOfWork.GetRangeDescending(ignoreCount, takeCount, keySelector);
        }

        /// <summary>
        /// Take a few base_PricingManager in sequence was sorted by descending on server.
        /// </summary>
        /// <typeparam name="TKey">Type of base_PricingManager to sort</typeparam>
        /// <param name="ignoreCount">Number of base_PricingManager will ignore.</param>
        /// <param name="takeCount">Number of base_PricingManager will take.</param>
        /// <param name="keySelector">The key columns by which to order the results.</param>
        /// <param name="expression">A function to test each object for a condition.</param>
        /// <returns>The new IList&lt;base_PricingManager&gt; instance.</returns>
        public IList<base_PricingManager> GetRangeDescending<TKey>(int ignoreCount, int takeCount, Expression<Func<base_PricingManager, TKey>> keySelector, Expression<Func<base_PricingManager, bool>> expression)
        {
            return UnitOfWork.GetRangeDescending(ignoreCount, takeCount, keySelector, expression);
        }

        /// <summary>
        /// Updates an base_PricingManager in the object context with data from the data source.
        /// </summary>
        /// <param name="base_PricingManager">The base_PricingManager to be refreshed.</param>
        public base_PricingManager Refresh(base_PricingManager base_PricingManager)
        {
            UnitOfWork.Refresh<base_PricingManager>(base_PricingManager);
            if (base_PricingManager.EntityState != System.Data.EntityState.Detached)
                return base_PricingManager;
            return null;
        }

        /// <summary>
        /// Updates a sequence of base_PricingManager in the object context with data from the data source.
        /// </summary>
        /// <typeparam name="base_PricingManager">Type of object in a sequence to refresh.</typeparam>
        /// <param name="base_PricingManager">Object collection to be refreshed.</param>
        public void Refresh(IEnumerable<base_PricingManager> base_PricingManager)
        {
            UnitOfWork.Refresh<base_PricingManager>(base_PricingManager);
        }

        /// <summary>
        /// Updates a sequence of base_PricingManager in the object context with data from the data source.
        /// </summary>
        public void Refresh()
        {
            UnitOfWork.Refresh<base_PricingManager>();
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
