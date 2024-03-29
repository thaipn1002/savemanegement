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
using CPC.POSReport.Database;

namespace CPC.POSReport.Repository
{
    /// <summary>
    /// Repository for table base_PricingChange 
    /// </summary>
    public partial class base_PricingChangeRepository
    {
        #region Auto Generate Code

        #region Constructors

        // Default constructor
        public base_PricingChangeRepository()
        {
        }

        #endregion

        #region Basic C.R.U.D. Operations

        /// <summary>
        /// Add new base_PricingChange.
        /// </summary>
        /// <param name="base_PricingChange">base_PricingChange to add.</param>
        /// <returns>base_PricingChange have been added.</returns>
        public base_PricingChange Add(base_PricingChange base_PricingChange)
        {
            UnitOfWork.Add<base_PricingChange>(base_PricingChange);
            return base_PricingChange;
        }

        /// <summary>
        /// Adds a sequence of new base_PricingChange.
        /// </summary>
        /// <param name="base_PricingChange">Sequence of new base_PricingChange to add.</param>
        /// <returns>Sequence of new base_PricingChange have been added.</returns>
        public IEnumerable<base_PricingChange> Add(IEnumerable<base_PricingChange> base_PricingChange)
        {
            UnitOfWork.Add<base_PricingChange>(base_PricingChange);
            return base_PricingChange;
        }

        /// <summary>
        /// Delete a existed base_PricingChange.
        /// </summary>
        /// <param name="base_PricingChange">base_PricingChange to delete.</param>
        public void Delete(base_PricingChange base_PricingChange)
        {
            Refresh(base_PricingChange);
            if (base_PricingChange.EntityState != System.Data.EntityState.Detached)
                UnitOfWork.Delete<base_PricingChange>(base_PricingChange);
        }

        /// <summary>
        /// Delete a sequence of existed base_PricingChange.
        /// </summary>
        /// <param name="base_PricingChange">Sequence of existed base_PricingChange to delete.</param>
        public void Delete(IEnumerable<base_PricingChange> base_PricingChange)
        {
            int total = base_PricingChange.Count();
            for (int i = total - 1; i >= 0; i--)
                Delete(base_PricingChange.ElementAt(i));
        }

        /// <summary>
        /// Returns the first base_PricingChange of a sequence that satisfies a specified condition or 
        /// a default value if no such base_PricingChange is found.
        /// </summary>
        /// <param name="expression">A function to test each base_PricingChange for a condition.</param>
        /// <returns>    
        /// Null if source is empty or if no base_PricingChange passes the test specified by expression; 
        /// otherwise, the first base_PricingChange in source that passes the test specified by expression.
        /// </returns>
        public base_PricingChange Get(Expression<Func<base_PricingChange, bool>> expression)
        {
            return UnitOfWork.Get<base_PricingChange>(expression);
        }

        /// <summary>
        /// Get all base_PricingChange.
        /// </summary>
        /// <returns>The new IList&lt;base_PricingChange&gt; instance.</returns>
        public IList<base_PricingChange> GetAll()
        {
            return UnitOfWork.GetAll<base_PricingChange>().ToList();
        }

        /// <summary>
        /// Get all base_PricingChange that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_PricingChange for a condition.</param>
        /// <returns>The new IList&lt;base_PricingChange&gt; instance.</returns>
        public IList<base_PricingChange> GetAll(Expression<Func<base_PricingChange, bool>> expression)
        {
            return UnitOfWork.GetAll<base_PricingChange>(expression).ToList();
        }

        /// <summary>
        /// Get all base_PricingChange.
        /// </summary>
        /// <returns>The new IEnumerable&lt;base_PricingChange&gt; instance.</returns>
        public IEnumerable<base_PricingChange> GetIEnumerable()
        {
            return UnitOfWork.GetIEnumerable<base_PricingChange>();
        }

        /// <summary>
        /// Get all base_PricingChange that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_PricingChange for a condition.</param>
        /// <returns>The new IEnumerable&lt;base_PricingChange&gt; instance.</returns>
        public IEnumerable<base_PricingChange> GetIEnumerable(Expression<Func<base_PricingChange, bool>> expression)
        {
            return UnitOfWork.GetIEnumerable<base_PricingChange>(expression);
        }

        /// <summary>
        /// Get all base_PricingChange.
        /// </summary>
        /// <returns>The new IQueryable&lt;base_PricingChange&gt; instance.</returns>
        public IQueryable<base_PricingChange> GetIQueryable()
        {
            return UnitOfWork.GetIQueryable<base_PricingChange>();
        }

        /// <summary>
        /// Get all base_PricingChange that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_PricingChange for a condition.</param>
        /// <returns>The new IQueryable&lt;base_PricingChange&gt; instance.</returns>
        public IQueryable<base_PricingChange> GetIQueryable(Expression<Func<base_PricingChange, bool>> expression)
        {
            return UnitOfWork.GetIQueryable<base_PricingChange>(expression);
        }

        /// <summary>
        /// Take a few base_PricingChange in a sequence was sorted on server.
        /// </summary>
        /// <param name="ignoreCount">Number of base_PricingChange will ignore.</param>
        /// <param name="takeCount">Number of base_PricingChange will take.</param>
        /// <param name="keys">The key columns by which to order the results.</param>
        /// <returns>The new IList&lt;base_PricingChange&gt; instance.</returns>
        public IList<base_PricingChange> GetRange(int ignoreCount, int takeCount, string keys)
        {
            return UnitOfWork.GetRange<base_PricingChange>(ignoreCount, takeCount, keys);
        }

        /// <summary>
        /// Take a few base_PricingChange in a sequence was sorted on server.
        /// </summary>
        /// <param name="ignoreCount">Number of base_PricingChange will ignore.</param>
        /// <param name="takeCount">Number of base_PricingChange will take.</param>
        /// <param name="keys">The key columns by which to order the results.</param>
        /// <param name="expression">A function to test each base_PricingChange for a condition.</param>
        /// <returns>The new IList&lt;base_PricingChange&gt; instance.</returns>
        public IList<base_PricingChange> GetRange(int ignoreCount, int takeCount, string keys, Expression<Func<base_PricingChange, bool>> expression)
        {
            return UnitOfWork.GetRange<base_PricingChange>(ignoreCount, takeCount, keys, expression);
        }

        /// <summary>
        /// Updates an base_PricingChange in the object context with data from the data source.
        /// </summary>
        /// <param name="base_PricingChange">The base_PricingChange to be refreshed.</param>
        public base_PricingChange Refresh(base_PricingChange base_PricingChange)
        {
            UnitOfWork.Refresh<base_PricingChange>(base_PricingChange);
            if (base_PricingChange.EntityState != System.Data.EntityState.Detached)
                return base_PricingChange;
            return null;
        }

        /// <summary>
        /// Updates a sequence of base_PricingChange in the object context with data from the data source.
        /// </summary>
        /// <typeparam name="base_PricingChange">Type of object in a sequence to refresh.</typeparam>
        /// <param name="base_PricingChange">Object collection to be refreshed.</param>
        public void Refresh(IEnumerable<base_PricingChange> base_PricingChange)
        {
            UnitOfWork.Refresh<base_PricingChange>(base_PricingChange);
        }

        /// <summary>
        /// Updates a sequence of base_PricingChange in the object context with data from the data source.
        /// </summary>
        public void Refresh()
        {
            UnitOfWork.Refresh<base_PricingChange>();
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
