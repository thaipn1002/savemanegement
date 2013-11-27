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
    /// Repository for table base_PromotionSchedule 
    /// </summary>
    public partial class base_PromotionScheduleRepository
    {
        #region Auto Generate Code

        #region Constructors

        // Default constructor
        public base_PromotionScheduleRepository()
        {
        }

        #endregion

        #region Basic C.R.U.D. Operations

        /// <summary>
        /// Add new base_PromotionSchedule.
        /// </summary>
        /// <param name="base_PromotionSchedule">base_PromotionSchedule to add.</param>
        /// <returns>base_PromotionSchedule have been added.</returns>
        public base_PromotionSchedule Add(base_PromotionSchedule base_PromotionSchedule)
        {
            UnitOfWork.Add<base_PromotionSchedule>(base_PromotionSchedule);
            return base_PromotionSchedule;
        }

        /// <summary>
        /// Adds a sequence of new base_PromotionSchedule.
        /// </summary>
        /// <param name="base_PromotionSchedule">Sequence of new base_PromotionSchedule to add.</param>
        /// <returns>Sequence of new base_PromotionSchedule have been added.</returns>
        public IEnumerable<base_PromotionSchedule> Add(IEnumerable<base_PromotionSchedule> base_PromotionSchedule)
        {
            UnitOfWork.Add<base_PromotionSchedule>(base_PromotionSchedule);
            return base_PromotionSchedule;
        }

        /// <summary>
        /// Delete a existed base_PromotionSchedule.
        /// </summary>
        /// <param name="base_PromotionSchedule">base_PromotionSchedule to delete.</param>
        public void Delete(base_PromotionSchedule base_PromotionSchedule)
        {
            Refresh(base_PromotionSchedule);
            if (base_PromotionSchedule.EntityState != System.Data.EntityState.Detached)
                UnitOfWork.Delete<base_PromotionSchedule>(base_PromotionSchedule);
        }

        /// <summary>
        /// Delete a sequence of existed base_PromotionSchedule.
        /// </summary>
        /// <param name="base_PromotionSchedule">Sequence of existed base_PromotionSchedule to delete.</param>
        public void Delete(IEnumerable<base_PromotionSchedule> base_PromotionSchedule)
        {
            int total = base_PromotionSchedule.Count();
            for (int i = total - 1; i >= 0; i--)
                Delete(base_PromotionSchedule.ElementAt(i));
        }

        /// <summary>
        /// Returns the first base_PromotionSchedule of a sequence that satisfies a specified condition or 
        /// a default value if no such base_PromotionSchedule is found.
        /// </summary>
        /// <param name="expression">A function to test each base_PromotionSchedule for a condition.</param>
        /// <returns>    
        /// Null if source is empty or if no base_PromotionSchedule passes the test specified by expression; 
        /// otherwise, the first base_PromotionSchedule in source that passes the test specified by expression.
        /// </returns>
        public base_PromotionSchedule Get(Expression<Func<base_PromotionSchedule, bool>> expression)
        {
            return UnitOfWork.Get<base_PromotionSchedule>(expression);
        }

        /// <summary>
        /// Get all base_PromotionSchedule.
        /// </summary>
        /// <returns>The new IList&lt;base_PromotionSchedule&gt; instance.</returns>
        public IList<base_PromotionSchedule> GetAll()
        {
            return UnitOfWork.GetAll<base_PromotionSchedule>().ToList();
        }

        /// <summary>
        /// Get all base_PromotionSchedule that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_PromotionSchedule for a condition.</param>
        /// <returns>The new IList&lt;base_PromotionSchedule&gt; instance.</returns>
        public IList<base_PromotionSchedule> GetAll(Expression<Func<base_PromotionSchedule, bool>> expression)
        {
            return UnitOfWork.GetAll<base_PromotionSchedule>(expression).ToList();
        }

        /// <summary>
        /// Get all base_PromotionSchedule.
        /// </summary>
        /// <returns>The new IEnumerable&lt;base_PromotionSchedule&gt; instance.</returns>
        public IEnumerable<base_PromotionSchedule> GetIEnumerable()
        {
            return UnitOfWork.GetIEnumerable<base_PromotionSchedule>();
        }

        /// <summary>
        /// Get all base_PromotionSchedule that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_PromotionSchedule for a condition.</param>
        /// <returns>The new IEnumerable&lt;base_PromotionSchedule&gt; instance.</returns>
        public IEnumerable<base_PromotionSchedule> GetIEnumerable(Expression<Func<base_PromotionSchedule, bool>> expression)
        {
            return UnitOfWork.GetIEnumerable<base_PromotionSchedule>(expression);
        }

        /// <summary>
        /// Get all base_PromotionSchedule.
        /// </summary>
        /// <returns>The new IQueryable&lt;base_PromotionSchedule&gt; instance.</returns>
        public IQueryable<base_PromotionSchedule> GetIQueryable()
        {
            return UnitOfWork.GetIQueryable<base_PromotionSchedule>();
        }

        /// <summary>
        /// Get all base_PromotionSchedule that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_PromotionSchedule for a condition.</param>
        /// <returns>The new IQueryable&lt;base_PromotionSchedule&gt; instance.</returns>
        public IQueryable<base_PromotionSchedule> GetIQueryable(Expression<Func<base_PromotionSchedule, bool>> expression)
        {
            return UnitOfWork.GetIQueryable<base_PromotionSchedule>(expression);
        }

        /// <summary>
        /// Take a few base_PromotionSchedule in a sequence was sorted on server.
        /// </summary>
        /// <param name="ignoreCount">Number of base_PromotionSchedule will ignore.</param>
        /// <param name="takeCount">Number of base_PromotionSchedule will take.</param>
        /// <param name="keys">The key columns by which to order the results.</param>
        /// <returns>The new IList&lt;base_PromotionSchedule&gt; instance.</returns>
        public IList<base_PromotionSchedule> GetRange(int ignoreCount, int takeCount, string keys)
        {
            return UnitOfWork.GetRange<base_PromotionSchedule>(ignoreCount, takeCount, keys);
        }

        /// <summary>
        /// Take a few base_PromotionSchedule in a sequence was sorted on server.
        /// </summary>
        /// <param name="ignoreCount">Number of base_PromotionSchedule will ignore.</param>
        /// <param name="takeCount">Number of base_PromotionSchedule will take.</param>
        /// <param name="keys">The key columns by which to order the results.</param>
        /// <param name="expression">A function to test each base_PromotionSchedule for a condition.</param>
        /// <returns>The new IList&lt;base_PromotionSchedule&gt; instance.</returns>
        public IList<base_PromotionSchedule> GetRange(int ignoreCount, int takeCount, string keys, Expression<Func<base_PromotionSchedule, bool>> expression)
        {
            return UnitOfWork.GetRange<base_PromotionSchedule>(ignoreCount, takeCount, keys, expression);
        }

        /// <summary>
        /// Updates an base_PromotionSchedule in the object context with data from the data source.
        /// </summary>
        /// <param name="base_PromotionSchedule">The base_PromotionSchedule to be refreshed.</param>
        public base_PromotionSchedule Refresh(base_PromotionSchedule base_PromotionSchedule)
        {
            UnitOfWork.Refresh<base_PromotionSchedule>(base_PromotionSchedule);
            if (base_PromotionSchedule.EntityState != System.Data.EntityState.Detached)
                return base_PromotionSchedule;
            return null;
        }

        /// <summary>
        /// Updates a sequence of base_PromotionSchedule in the object context with data from the data source.
        /// </summary>
        /// <typeparam name="base_PromotionSchedule">Type of object in a sequence to refresh.</typeparam>
        /// <param name="base_PromotionSchedule">Object collection to be refreshed.</param>
        public void Refresh(IEnumerable<base_PromotionSchedule> base_PromotionSchedule)
        {
            UnitOfWork.Refresh<base_PromotionSchedule>(base_PromotionSchedule);
        }

        /// <summary>
        /// Updates a sequence of base_PromotionSchedule in the object context with data from the data source.
        /// </summary>
        public void Refresh()
        {
            UnitOfWork.Refresh<base_PromotionSchedule>();
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
