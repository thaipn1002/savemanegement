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
    /// Repository for table base_ResourcePayment 
    /// </summary>
    public partial class base_ResourcePaymentRepository
    {
        #region Auto Generate Code

        #region Constructors

        // Default constructor
        public base_ResourcePaymentRepository()
        {
        }

        #endregion

        #region Basic C.R.U.D. Operations

        /// <summary>
        /// Add new base_ResourcePayment.
        /// </summary>
        /// <param name="base_ResourcePayment">base_ResourcePayment to add.</param>
        /// <returns>base_ResourcePayment have been added.</returns>
        public base_ResourcePayment Add(base_ResourcePayment base_ResourcePayment)
        {
            UnitOfWork.Add<base_ResourcePayment>(base_ResourcePayment);
            return base_ResourcePayment;
        }

        /// <summary>
        /// Adds a sequence of new base_ResourcePayment.
        /// </summary>
        /// <param name="base_ResourcePayment">Sequence of new base_ResourcePayment to add.</param>
        /// <returns>Sequence of new base_ResourcePayment have been added.</returns>
        public IEnumerable<base_ResourcePayment> Add(IEnumerable<base_ResourcePayment> base_ResourcePayment)
        {
            UnitOfWork.Add<base_ResourcePayment>(base_ResourcePayment);
            return base_ResourcePayment;
        }

        /// <summary>
        /// Delete a existed base_ResourcePayment.
        /// </summary>
        /// <param name="base_ResourcePayment">base_ResourcePayment to delete.</param>
        public void Delete(base_ResourcePayment base_ResourcePayment)
        {
            Refresh(base_ResourcePayment);
            if (base_ResourcePayment.EntityState != System.Data.EntityState.Detached)
                UnitOfWork.Delete<base_ResourcePayment>(base_ResourcePayment);
        }

        /// <summary>
        /// Delete a sequence of existed base_ResourcePayment.
        /// </summary>
        /// <param name="base_ResourcePayment">Sequence of existed base_ResourcePayment to delete.</param>
        public void Delete(IEnumerable<base_ResourcePayment> base_ResourcePayment)
        {
            int total = base_ResourcePayment.Count();
            for (int i = total - 1; i >= 0; i--)
                Delete(base_ResourcePayment.ElementAt(i));
        }

        /// <summary>
        /// Returns the first base_ResourcePayment of a sequence that satisfies a specified condition or 
        /// a default value if no such base_ResourcePayment is found.
        /// </summary>
        /// <param name="expression">A function to test each base_ResourcePayment for a condition.</param>
        /// <returns>    
        /// Null if source is empty or if no base_ResourcePayment passes the test specified by expression; 
        /// otherwise, the first base_ResourcePayment in source that passes the test specified by expression.
        /// </returns>
        public base_ResourcePayment Get(Expression<Func<base_ResourcePayment, bool>> expression)
        {
            return UnitOfWork.Get<base_ResourcePayment>(expression);
        }

        /// <summary>
        /// Get all base_ResourcePayment.
        /// </summary>
        /// <returns>The new IList&lt;base_ResourcePayment&gt; instance.</returns>
        public IList<base_ResourcePayment> GetAll()
        {
            return UnitOfWork.GetAll<base_ResourcePayment>().ToList();
        }

        /// <summary>
        /// Get all base_ResourcePayment that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_ResourcePayment for a condition.</param>
        /// <returns>The new IList&lt;base_ResourcePayment&gt; instance.</returns>
        public IList<base_ResourcePayment> GetAll(Expression<Func<base_ResourcePayment, bool>> expression)
        {
            return UnitOfWork.GetAll<base_ResourcePayment>(expression).ToList();
        }

        /// <summary>
        /// Get all base_ResourcePayment.
        /// </summary>
        /// <returns>The new IEnumerable&lt;base_ResourcePayment&gt; instance.</returns>
        public IEnumerable<base_ResourcePayment> GetIEnumerable()
        {
            return UnitOfWork.GetIEnumerable<base_ResourcePayment>();
        }

        /// <summary>
        /// Get all base_ResourcePayment that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_ResourcePayment for a condition.</param>
        /// <returns>The new IEnumerable&lt;base_ResourcePayment&gt; instance.</returns>
        public IEnumerable<base_ResourcePayment> GetIEnumerable(Expression<Func<base_ResourcePayment, bool>> expression)
        {
            return UnitOfWork.GetIEnumerable<base_ResourcePayment>(expression);
        }

        /// <summary>
        /// Get all base_ResourcePayment.
        /// </summary>
        /// <returns>The new IQueryable&lt;base_ResourcePayment&gt; instance.</returns>
        public IQueryable<base_ResourcePayment> GetIQueryable()
        {
            return UnitOfWork.GetIQueryable<base_ResourcePayment>();
        }

        /// <summary>
        /// Get all base_ResourcePayment that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_ResourcePayment for a condition.</param>
        /// <returns>The new IQueryable&lt;base_ResourcePayment&gt; instance.</returns>
        public IQueryable<base_ResourcePayment> GetIQueryable(Expression<Func<base_ResourcePayment, bool>> expression)
        {
            return UnitOfWork.GetIQueryable<base_ResourcePayment>(expression);
        }

        /// <summary>
        /// Take a few base_ResourcePayment in a sequence was sorted on server.
        /// </summary>
        /// <param name="ignoreCount">Number of base_ResourcePayment will ignore.</param>
        /// <param name="takeCount">Number of base_ResourcePayment will take.</param>
        /// <param name="keys">The key columns by which to order the results.</param>
        /// <returns>The new IList&lt;base_ResourcePayment&gt; instance.</returns>
        public IList<base_ResourcePayment> GetRange(int ignoreCount, int takeCount, string keys)
        {
            return UnitOfWork.GetRange<base_ResourcePayment>(ignoreCount, takeCount, keys);
        }

        /// <summary>
        /// Take a few base_ResourcePayment in a sequence was sorted on server.
        /// </summary>
        /// <param name="ignoreCount">Number of base_ResourcePayment will ignore.</param>
        /// <param name="takeCount">Number of base_ResourcePayment will take.</param>
        /// <param name="keys">The key columns by which to order the results.</param>
        /// <param name="expression">A function to test each base_ResourcePayment for a condition.</param>
        /// <returns>The new IList&lt;base_ResourcePayment&gt; instance.</returns>
        public IList<base_ResourcePayment> GetRange(int ignoreCount, int takeCount, string keys, Expression<Func<base_ResourcePayment, bool>> expression)
        {
            return UnitOfWork.GetRange<base_ResourcePayment>(ignoreCount, takeCount, keys, expression);
        }

        /// <summary>
        /// Updates an base_ResourcePayment in the object context with data from the data source.
        /// </summary>
        /// <param name="base_ResourcePayment">The base_ResourcePayment to be refreshed.</param>
        public base_ResourcePayment Refresh(base_ResourcePayment base_ResourcePayment)
        {
            UnitOfWork.Refresh<base_ResourcePayment>(base_ResourcePayment);
            if (base_ResourcePayment.EntityState != System.Data.EntityState.Detached)
                return base_ResourcePayment;
            return null;
        }

        /// <summary>
        /// Updates a sequence of base_ResourcePayment in the object context with data from the data source.
        /// </summary>
        /// <typeparam name="base_ResourcePayment">Type of object in a sequence to refresh.</typeparam>
        /// <param name="base_ResourcePayment">Object collection to be refreshed.</param>
        public void Refresh(IEnumerable<base_ResourcePayment> base_ResourcePayment)
        {
            UnitOfWork.Refresh<base_ResourcePayment>(base_ResourcePayment);
        }

        /// <summary>
        /// Updates a sequence of base_ResourcePayment in the object context with data from the data source.
        /// </summary>
        public void Refresh()
        {
            UnitOfWork.Refresh<base_ResourcePayment>();
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
