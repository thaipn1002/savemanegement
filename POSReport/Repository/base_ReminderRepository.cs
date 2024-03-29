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
    /// Repository for table base_Reminder 
    /// </summary>
    public partial class base_ReminderRepository
    {
        #region Auto Generate Code

        #region Constructors

        // Default constructor
        public base_ReminderRepository()
        {
        }

        #endregion

        #region Basic C.R.U.D. Operations

        /// <summary>
        /// Add new base_Reminder.
        /// </summary>
        /// <param name="base_Reminder">base_Reminder to add.</param>
        /// <returns>base_Reminder have been added.</returns>
        public base_Reminder Add(base_Reminder base_Reminder)
        {
            UnitOfWork.Add<base_Reminder>(base_Reminder);
            return base_Reminder;
        }

        /// <summary>
        /// Adds a sequence of new base_Reminder.
        /// </summary>
        /// <param name="base_Reminder">Sequence of new base_Reminder to add.</param>
        /// <returns>Sequence of new base_Reminder have been added.</returns>
        public IEnumerable<base_Reminder> Add(IEnumerable<base_Reminder> base_Reminder)
        {
            UnitOfWork.Add<base_Reminder>(base_Reminder);
            return base_Reminder;
        }

        /// <summary>
        /// Delete a existed base_Reminder.
        /// </summary>
        /// <param name="base_Reminder">base_Reminder to delete.</param>
        public void Delete(base_Reminder base_Reminder)
        {
            Refresh(base_Reminder);
            if (base_Reminder.EntityState != System.Data.EntityState.Detached)
                UnitOfWork.Delete<base_Reminder>(base_Reminder);
        }

        /// <summary>
        /// Delete a sequence of existed base_Reminder.
        /// </summary>
        /// <param name="base_Reminder">Sequence of existed base_Reminder to delete.</param>
        public void Delete(IEnumerable<base_Reminder> base_Reminder)
        {
            int total = base_Reminder.Count();
            for (int i = total - 1; i >= 0; i--)
                Delete(base_Reminder.ElementAt(i));
        }

        /// <summary>
        /// Returns the first base_Reminder of a sequence that satisfies a specified condition or 
        /// a default value if no such base_Reminder is found.
        /// </summary>
        /// <param name="expression">A function to test each base_Reminder for a condition.</param>
        /// <returns>    
        /// Null if source is empty or if no base_Reminder passes the test specified by expression; 
        /// otherwise, the first base_Reminder in source that passes the test specified by expression.
        /// </returns>
        public base_Reminder Get(Expression<Func<base_Reminder, bool>> expression)
        {
            return UnitOfWork.Get<base_Reminder>(expression);
        }

        /// <summary>
        /// Get all base_Reminder.
        /// </summary>
        /// <returns>The new IList&lt;base_Reminder&gt; instance.</returns>
        public IList<base_Reminder> GetAll()
        {
            return UnitOfWork.GetAll<base_Reminder>().ToList();
        }

        /// <summary>
        /// Get all base_Reminder that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_Reminder for a condition.</param>
        /// <returns>The new IList&lt;base_Reminder&gt; instance.</returns>
        public IList<base_Reminder> GetAll(Expression<Func<base_Reminder, bool>> expression)
        {
            return UnitOfWork.GetAll<base_Reminder>(expression).ToList();
        }

        /// <summary>
        /// Get all base_Reminder.
        /// </summary>
        /// <returns>The new IEnumerable&lt;base_Reminder&gt; instance.</returns>
        public IEnumerable<base_Reminder> GetIEnumerable()
        {
            return UnitOfWork.GetIEnumerable<base_Reminder>();
        }

        /// <summary>
        /// Get all base_Reminder that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_Reminder for a condition.</param>
        /// <returns>The new IEnumerable&lt;base_Reminder&gt; instance.</returns>
        public IEnumerable<base_Reminder> GetIEnumerable(Expression<Func<base_Reminder, bool>> expression)
        {
            return UnitOfWork.GetIEnumerable<base_Reminder>(expression);
        }

        /// <summary>
        /// Get all base_Reminder.
        /// </summary>
        /// <returns>The new IQueryable&lt;base_Reminder&gt; instance.</returns>
        public IQueryable<base_Reminder> GetIQueryable()
        {
            return UnitOfWork.GetIQueryable<base_Reminder>();
        }

        /// <summary>
        /// Get all base_Reminder that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_Reminder for a condition.</param>
        /// <returns>The new IQueryable&lt;base_Reminder&gt; instance.</returns>
        public IQueryable<base_Reminder> GetIQueryable(Expression<Func<base_Reminder, bool>> expression)
        {
            return UnitOfWork.GetIQueryable<base_Reminder>(expression);
        }

        /// <summary>
        /// Take a few base_Reminder in a sequence was sorted on server.
        /// </summary>
        /// <param name="ignoreCount">Number of base_Reminder will ignore.</param>
        /// <param name="takeCount">Number of base_Reminder will take.</param>
        /// <param name="keys">The key columns by which to order the results.</param>
        /// <returns>The new IList&lt;base_Reminder&gt; instance.</returns>
        public IList<base_Reminder> GetRange(int ignoreCount, int takeCount, string keys)
        {
            return UnitOfWork.GetRange<base_Reminder>(ignoreCount, takeCount, keys);
        }

        /// <summary>
        /// Take a few base_Reminder in a sequence was sorted on server.
        /// </summary>
        /// <param name="ignoreCount">Number of base_Reminder will ignore.</param>
        /// <param name="takeCount">Number of base_Reminder will take.</param>
        /// <param name="keys">The key columns by which to order the results.</param>
        /// <param name="expression">A function to test each base_Reminder for a condition.</param>
        /// <returns>The new IList&lt;base_Reminder&gt; instance.</returns>
        public IList<base_Reminder> GetRange(int ignoreCount, int takeCount, string keys, Expression<Func<base_Reminder, bool>> expression)
        {
            return UnitOfWork.GetRange<base_Reminder>(ignoreCount, takeCount, keys, expression);
        }

        /// <summary>
        /// Updates an base_Reminder in the object context with data from the data source.
        /// </summary>
        /// <param name="base_Reminder">The base_Reminder to be refreshed.</param>
        public base_Reminder Refresh(base_Reminder base_Reminder)
        {
            UnitOfWork.Refresh<base_Reminder>(base_Reminder);
            if (base_Reminder.EntityState != System.Data.EntityState.Detached)
                return base_Reminder;
            return null;
        }

        /// <summary>
        /// Updates a sequence of base_Reminder in the object context with data from the data source.
        /// </summary>
        /// <typeparam name="base_Reminder">Type of object in a sequence to refresh.</typeparam>
        /// <param name="base_Reminder">Object collection to be refreshed.</param>
        public void Refresh(IEnumerable<base_Reminder> base_Reminder)
        {
            UnitOfWork.Refresh<base_Reminder>(base_Reminder);
        }

        /// <summary>
        /// Updates a sequence of base_Reminder in the object context with data from the data source.
        /// </summary>
        public void Refresh()
        {
            UnitOfWork.Refresh<base_Reminder>();
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
