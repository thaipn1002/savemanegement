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
    /// Repository for table tims_WorkSchedule 
    /// </summary>
    public partial class tims_WorkScheduleRepository
    {
        #region Auto Generate Code

        #region Constructors

        // Default constructor
        public tims_WorkScheduleRepository()
        {
        }

        #endregion

        #region Basic C.R.U.D. Operations

        /// <summary>
        /// Add new tims_WorkSchedule.
        /// </summary>
        /// <param name="tims_WorkSchedule">tims_WorkSchedule to add.</param>
        /// <returns>tims_WorkSchedule have been added.</returns>
        public tims_WorkSchedule Add(tims_WorkSchedule tims_WorkSchedule)
        {
            UnitOfWork.Add<tims_WorkSchedule>(tims_WorkSchedule);
            return tims_WorkSchedule;
        }

        /// <summary>
        /// Adds a sequence of new tims_WorkSchedule.
        /// </summary>
        /// <param name="tims_WorkSchedule">Sequence of new tims_WorkSchedule to add.</param>
        /// <returns>Sequence of new tims_WorkSchedule have been added.</returns>
        public IEnumerable<tims_WorkSchedule> Add(IEnumerable<tims_WorkSchedule> tims_WorkSchedule)
        {
            UnitOfWork.Add<tims_WorkSchedule>(tims_WorkSchedule);
            return tims_WorkSchedule;
        }

        /// <summary>
        /// Delete a existed tims_WorkSchedule.
        /// </summary>
        /// <param name="tims_WorkSchedule">tims_WorkSchedule to delete.</param>
        public void Delete(tims_WorkSchedule tims_WorkSchedule)
        {
            Refresh(tims_WorkSchedule);
            if (tims_WorkSchedule.EntityState != System.Data.EntityState.Detached)
                UnitOfWork.Delete<tims_WorkSchedule>(tims_WorkSchedule);
        }

        /// <summary>
        /// Delete a sequence of existed tims_WorkSchedule.
        /// </summary>
        /// <param name="tims_WorkSchedule">Sequence of existed tims_WorkSchedule to delete.</param>
        public void Delete(IEnumerable<tims_WorkSchedule> tims_WorkSchedule)
        {
            int total = tims_WorkSchedule.Count();
            for (int i = total - 1; i >= 0; i--)
                Delete(tims_WorkSchedule.ElementAt(i));
        }

        /// <summary>
        /// Returns the first tims_WorkSchedule of a sequence that satisfies a specified condition or 
        /// a default value if no such tims_WorkSchedule is found.
        /// </summary>
        /// <param name="expression">A function to test each tims_WorkSchedule for a condition.</param>
        /// <returns>    
        /// Null if source is empty or if no tims_WorkSchedule passes the test specified by expression; 
        /// otherwise, the first tims_WorkSchedule in source that passes the test specified by expression.
        /// </returns>
        public tims_WorkSchedule Get(Expression<Func<tims_WorkSchedule, bool>> expression)
        {
            return UnitOfWork.Get<tims_WorkSchedule>(expression);
        }

        /// <summary>
        /// Get all tims_WorkSchedule.
        /// </summary>
        /// <returns>The new IList&lt;tims_WorkSchedule&gt; instance.</returns>
        public IList<tims_WorkSchedule> GetAll()
        {
            return UnitOfWork.GetAll<tims_WorkSchedule>().ToList();
        }

        /// <summary>
        /// Get all tims_WorkSchedule that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each tims_WorkSchedule for a condition.</param>
        /// <returns>The new IList&lt;tims_WorkSchedule&gt; instance.</returns>
        public IList<tims_WorkSchedule> GetAll(Expression<Func<tims_WorkSchedule, bool>> expression)
        {
            return UnitOfWork.GetAll<tims_WorkSchedule>(expression).ToList();
        }

        /// <summary>
        /// Get all tims_WorkSchedule.
        /// </summary>
        /// <returns>The new IEnumerable&lt;tims_WorkSchedule&gt; instance.</returns>
        public IEnumerable<tims_WorkSchedule> GetIEnumerable()
        {
            return UnitOfWork.GetIEnumerable<tims_WorkSchedule>();
        }

        /// <summary>
        /// Get all tims_WorkSchedule that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each tims_WorkSchedule for a condition.</param>
        /// <returns>The new IEnumerable&lt;tims_WorkSchedule&gt; instance.</returns>
        public IEnumerable<tims_WorkSchedule> GetIEnumerable(Expression<Func<tims_WorkSchedule, bool>> expression)
        {
            return UnitOfWork.GetIEnumerable<tims_WorkSchedule>(expression);
        }

        /// <summary>
        /// Get all tims_WorkSchedule.
        /// </summary>
        /// <returns>The new IQueryable&lt;tims_WorkSchedule&gt; instance.</returns>
        public IQueryable<tims_WorkSchedule> GetIQueryable()
        {
            return UnitOfWork.GetIQueryable<tims_WorkSchedule>();
        }

        /// <summary>
        /// Get all tims_WorkSchedule that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each tims_WorkSchedule for a condition.</param>
        /// <returns>The new IQueryable&lt;tims_WorkSchedule&gt; instance.</returns>
        public IQueryable<tims_WorkSchedule> GetIQueryable(Expression<Func<tims_WorkSchedule, bool>> expression)
        {
            return UnitOfWork.GetIQueryable<tims_WorkSchedule>(expression);
        }

        /// <summary>
        /// Take a few tims_WorkSchedule in a sequence was sorted on server.
        /// </summary>
        /// <param name="ignoreCount">Number of tims_WorkSchedule will ignore.</param>
        /// <param name="takeCount">Number of tims_WorkSchedule will take.</param>
        /// <param name="keys">The key columns by which to order the results.</param>
        /// <returns>The new IList&lt;tims_WorkSchedule&gt; instance.</returns>
        public IList<tims_WorkSchedule> GetRange(int ignoreCount, int takeCount, string keys)
        {
            return UnitOfWork.GetRange<tims_WorkSchedule>(ignoreCount, takeCount, keys);
        }

        /// <summary>
        /// Take a few tims_WorkSchedule in a sequence was sorted on server.
        /// </summary>
        /// <param name="ignoreCount">Number of tims_WorkSchedule will ignore.</param>
        /// <param name="takeCount">Number of tims_WorkSchedule will take.</param>
        /// <param name="keys">The key columns by which to order the results.</param>
        /// <param name="expression">A function to test each tims_WorkSchedule for a condition.</param>
        /// <returns>The new IList&lt;tims_WorkSchedule&gt; instance.</returns>
        public IList<tims_WorkSchedule> GetRange(int ignoreCount, int takeCount, string keys, Expression<Func<tims_WorkSchedule, bool>> expression)
        {
            return UnitOfWork.GetRange<tims_WorkSchedule>(ignoreCount, takeCount, keys, expression);
        }

        /// <summary>
        /// Updates an tims_WorkSchedule in the object context with data from the data source.
        /// </summary>
        /// <param name="tims_WorkSchedule">The tims_WorkSchedule to be refreshed.</param>
        public tims_WorkSchedule Refresh(tims_WorkSchedule tims_WorkSchedule)
        {
            UnitOfWork.Refresh<tims_WorkSchedule>(tims_WorkSchedule);
            if (tims_WorkSchedule.EntityState != System.Data.EntityState.Detached)
                return tims_WorkSchedule;
            return null;
        }

        /// <summary>
        /// Updates a sequence of tims_WorkSchedule in the object context with data from the data source.
        /// </summary>
        /// <typeparam name="tims_WorkSchedule">Type of object in a sequence to refresh.</typeparam>
        /// <param name="tims_WorkSchedule">Object collection to be refreshed.</param>
        public void Refresh(IEnumerable<tims_WorkSchedule> tims_WorkSchedule)
        {
            UnitOfWork.Refresh<tims_WorkSchedule>(tims_WorkSchedule);
        }

        /// <summary>
        /// Updates a sequence of tims_WorkSchedule in the object context with data from the data source.
        /// </summary>
        public void Refresh()
        {
            UnitOfWork.Refresh<tims_WorkSchedule>();
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
