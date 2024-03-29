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
    /// Repository for table base_GuestReward 
    /// </summary>
    public partial class base_GuestRewardRepository
    {
        #region Auto Generate Code

        #region Constructors

        // Default constructor
        public base_GuestRewardRepository()
        {
        }

        #endregion

        #region Basic C.R.U.D. Operations

        /// <summary>
        /// Add new base_GuestReward.
        /// </summary>
        /// <param name="base_GuestReward">base_GuestReward to add.</param>
        /// <returns>base_GuestReward have been added.</returns>
        public base_GuestReward Add(base_GuestReward base_GuestReward)
        {
            UnitOfWork.Add<base_GuestReward>(base_GuestReward);
            return base_GuestReward;
        }

        /// <summary>
        /// Adds a sequence of new base_GuestReward.
        /// </summary>
        /// <param name="base_GuestReward">Sequence of new base_GuestReward to add.</param>
        /// <returns>Sequence of new base_GuestReward have been added.</returns>
        public IEnumerable<base_GuestReward> Add(IEnumerable<base_GuestReward> base_GuestReward)
        {
            UnitOfWork.Add<base_GuestReward>(base_GuestReward);
            return base_GuestReward;
        }

        /// <summary>
        /// Delete a existed base_GuestReward.
        /// </summary>
        /// <param name="base_GuestReward">base_GuestReward to delete.</param>
        public void Delete(base_GuestReward base_GuestReward)
        {
            Refresh(base_GuestReward);
            if (base_GuestReward.EntityState != System.Data.EntityState.Detached)
                UnitOfWork.Delete<base_GuestReward>(base_GuestReward);
        }

        /// <summary>
        /// Delete a sequence of existed base_GuestReward.
        /// </summary>
        /// <param name="base_GuestReward">Sequence of existed base_GuestReward to delete.</param>
        public void Delete(IEnumerable<base_GuestReward> base_GuestReward)
        {
            int total = base_GuestReward.Count();
            for (int i = total - 1; i >= 0; i--)
                Delete(base_GuestReward.ElementAt(i));
        }

        /// <summary>
        /// Returns the first base_GuestReward of a sequence that satisfies a specified condition or 
        /// a default value if no such base_GuestReward is found.
        /// </summary>
        /// <param name="expression">A function to test each base_GuestReward for a condition.</param>
        /// <returns>    
        /// Null if source is empty or if no base_GuestReward passes the test specified by expression; 
        /// otherwise, the first base_GuestReward in source that passes the test specified by expression.
        /// </returns>
        public base_GuestReward Get(Expression<Func<base_GuestReward, bool>> expression)
        {
            return UnitOfWork.Get<base_GuestReward>(expression);
        }

        /// <summary>
        /// Get all base_GuestReward.
        /// </summary>
        /// <returns>The new IList&lt;base_GuestReward&gt; instance.</returns>
        public IList<base_GuestReward> GetAll()
        {
            return UnitOfWork.GetAll<base_GuestReward>().ToList();
        }

        /// <summary>
        /// Get all base_GuestReward that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_GuestReward for a condition.</param>
        /// <returns>The new IList&lt;base_GuestReward&gt; instance.</returns>
        public IList<base_GuestReward> GetAll(Expression<Func<base_GuestReward, bool>> expression)
        {
            return UnitOfWork.GetAll<base_GuestReward>(expression).ToList();
        }

        /// <summary>
        /// Get all base_GuestReward.
        /// </summary>
        /// <returns>The new IEnumerable&lt;base_GuestReward&gt; instance.</returns>
        public IEnumerable<base_GuestReward> GetIEnumerable()
        {
            return UnitOfWork.GetIEnumerable<base_GuestReward>();
        }

        /// <summary>
        /// Get all base_GuestReward that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_GuestReward for a condition.</param>
        /// <returns>The new IEnumerable&lt;base_GuestReward&gt; instance.</returns>
        public IEnumerable<base_GuestReward> GetIEnumerable(Expression<Func<base_GuestReward, bool>> expression)
        {
            return UnitOfWork.GetIEnumerable<base_GuestReward>(expression);
        }

        /// <summary>
        /// Get all base_GuestReward.
        /// </summary>
        /// <returns>The new IQueryable&lt;base_GuestReward&gt; instance.</returns>
        public IQueryable<base_GuestReward> GetIQueryable()
        {
            return UnitOfWork.GetIQueryable<base_GuestReward>();
        }

        /// <summary>
        /// Get all base_GuestReward that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_GuestReward for a condition.</param>
        /// <returns>The new IQueryable&lt;base_GuestReward&gt; instance.</returns>
        public IQueryable<base_GuestReward> GetIQueryable(Expression<Func<base_GuestReward, bool>> expression)
        {
            return UnitOfWork.GetIQueryable<base_GuestReward>(expression);
        }

        /// <summary>
        /// Take a few base_GuestReward in a sequence was sorted on server.
        /// </summary>
        /// <param name="ignoreCount">Number of base_GuestReward will ignore.</param>
        /// <param name="takeCount">Number of base_GuestReward will take.</param>
        /// <param name="keys">The key columns by which to order the results.</param>
        /// <returns>The new IList&lt;base_GuestReward&gt; instance.</returns>
        public IList<base_GuestReward> GetRange(int ignoreCount, int takeCount, string keys)
        {
            return UnitOfWork.GetRange<base_GuestReward>(ignoreCount, takeCount, keys);
        }

        /// <summary>
        /// Take a few base_GuestReward in a sequence was sorted on server.
        /// </summary>
        /// <param name="ignoreCount">Number of base_GuestReward will ignore.</param>
        /// <param name="takeCount">Number of base_GuestReward will take.</param>
        /// <param name="keys">The key columns by which to order the results.</param>
        /// <param name="expression">A function to test each base_GuestReward for a condition.</param>
        /// <returns>The new IList&lt;base_GuestReward&gt; instance.</returns>
        public IList<base_GuestReward> GetRange(int ignoreCount, int takeCount, string keys, Expression<Func<base_GuestReward, bool>> expression)
        {
            return UnitOfWork.GetRange<base_GuestReward>(ignoreCount, takeCount, keys, expression);
        }

        /// <summary>
        /// Updates an base_GuestReward in the object context with data from the data source.
        /// </summary>
        /// <param name="base_GuestReward">The base_GuestReward to be refreshed.</param>
        public base_GuestReward Refresh(base_GuestReward base_GuestReward)
        {
            UnitOfWork.Refresh<base_GuestReward>(base_GuestReward);
            if (base_GuestReward.EntityState != System.Data.EntityState.Detached)
                return base_GuestReward;
            return null;
        }

        /// <summary>
        /// Updates a sequence of base_GuestReward in the object context with data from the data source.
        /// </summary>
        /// <typeparam name="base_GuestReward">Type of object in a sequence to refresh.</typeparam>
        /// <param name="base_GuestReward">Object collection to be refreshed.</param>
        public void Refresh(IEnumerable<base_GuestReward> base_GuestReward)
        {
            UnitOfWork.Refresh<base_GuestReward>(base_GuestReward);
        }

        /// <summary>
        /// Updates a sequence of base_GuestReward in the object context with data from the data source.
        /// </summary>
        public void Refresh()
        {
            UnitOfWork.Refresh<base_GuestReward>();
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
