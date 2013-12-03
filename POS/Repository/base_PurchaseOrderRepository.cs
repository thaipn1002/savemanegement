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
    /// Repository for table base_PurchaseOrder 
    /// </summary>
    public partial class base_PurchaseOrderRepository
    {
        #region Auto Generate Code

        #region Constructors

        // Default constructor
        public base_PurchaseOrderRepository()
        {
        }

        #endregion

        #region Basic C.R.U.D. Operations

        /// <summary>
        /// Add new base_PurchaseOrder.
        /// </summary>
        /// <param name="base_PurchaseOrder">base_PurchaseOrder to add.</param>
        /// <returns>base_PurchaseOrder have been added.</returns>
        public base_PurchaseOrder Add(base_PurchaseOrder base_PurchaseOrder)
        {
            UnitOfWork.Add<base_PurchaseOrder>(base_PurchaseOrder);
            return base_PurchaseOrder;
        }

        /// <summary>
        /// Adds a sequence of new base_PurchaseOrder.
        /// </summary>
        /// <param name="base_PurchaseOrder">Sequence of new base_PurchaseOrder to add.</param>
        /// <returns>Sequence of new base_PurchaseOrder have been added.</returns>
        public IEnumerable<base_PurchaseOrder> Add(IEnumerable<base_PurchaseOrder> base_PurchaseOrder)
        {
            UnitOfWork.Add<base_PurchaseOrder>(base_PurchaseOrder);
            return base_PurchaseOrder;
        }

        /// <summary>
        /// Delete a existed base_PurchaseOrder.
        /// </summary>
        /// <param name="base_PurchaseOrder">base_PurchaseOrder to delete.</param>
        public void Delete(base_PurchaseOrder base_PurchaseOrder)
        {
            Refresh(base_PurchaseOrder);
            if (base_PurchaseOrder.EntityState != System.Data.EntityState.Detached)
                UnitOfWork.Delete<base_PurchaseOrder>(base_PurchaseOrder);
        }

        /// <summary>
        /// Delete a sequence of existed base_PurchaseOrder.
        /// </summary>
        /// <param name="base_PurchaseOrder">Sequence of existed base_PurchaseOrder to delete.</param>
        public void Delete(IEnumerable<base_PurchaseOrder> base_PurchaseOrder)
        {
            int total = base_PurchaseOrder.Count();
            for (int i = total - 1; i >= 0; i--)
                Delete(base_PurchaseOrder.ElementAt(i));
        }

        /// <summary>
        /// Returns the first base_PurchaseOrder of a sequence that satisfies a specified condition or 
        /// a default value if no such base_PurchaseOrder is found.
        /// </summary>
        /// <param name="expression">A function to test each base_PurchaseOrder for a condition.</param>
        /// <returns>    
        /// Null if source is empty or if no base_PurchaseOrder passes the test specified by expression; 
        /// otherwise, the first base_PurchaseOrder in source that passes the test specified by expression.
        /// </returns>
        public base_PurchaseOrder Get(Expression<Func<base_PurchaseOrder, bool>> expression)
        {
            return UnitOfWork.Get<base_PurchaseOrder>(expression);
        }

        /// <summary>
        /// Get all base_PurchaseOrder.
        /// </summary>
        /// <returns>The new IList&lt;base_PurchaseOrder&gt; instance.</returns>
        public IList<base_PurchaseOrder> GetAll()
        {
            return UnitOfWork.GetAll<base_PurchaseOrder>();
        }

        /// <summary>
        /// Get all base_PurchaseOrder that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_PurchaseOrder for a condition.</param>
        /// <returns>The new IList&lt;base_PurchaseOrder&gt; instance.</returns>
        public IList<base_PurchaseOrder> GetAll(Expression<Func<base_PurchaseOrder, bool>> expression)
        {
            return UnitOfWork.GetAll<base_PurchaseOrder>(expression);
        }

        /// <summary>
        /// Get all base_PurchaseOrder.
        /// </summary>
        /// <returns>The new IEnumerable&lt;base_PurchaseOrder&gt; instance.</returns>
        public IEnumerable<base_PurchaseOrder> GetIEnumerable()
        {
            return UnitOfWork.GetIEnumerable<base_PurchaseOrder>();
        }

        /// <summary>
        /// Get all base_PurchaseOrder that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_PurchaseOrder for a condition.</param>
        /// <returns>The new IEnumerable&lt;base_PurchaseOrder&gt; instance.</returns>
        public IEnumerable<base_PurchaseOrder> GetIEnumerable(Expression<Func<base_PurchaseOrder, bool>> expression)
        {
            return UnitOfWork.GetIEnumerable<base_PurchaseOrder>(expression);
        }

        /// <summary>
        /// Get all base_PurchaseOrder.
        /// </summary>
        /// <returns>The new IQueryable&lt;base_PurchaseOrder&gt; instance.</returns>
        public IQueryable<base_PurchaseOrder> GetIQueryable()
        {
            return UnitOfWork.GetIQueryable<base_PurchaseOrder>();
        }

        /// <summary>
        /// Get all base_PurchaseOrder that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_PurchaseOrder for a condition.</param>
        /// <returns>The new IQueryable&lt;base_PurchaseOrder&gt; instance.</returns>
        public IQueryable<base_PurchaseOrder> GetIQueryable(Expression<Func<base_PurchaseOrder, bool>> expression)
        {
            return UnitOfWork.GetIQueryable<base_PurchaseOrder>(expression);
        }

        /// <summary>
        /// Take a few base_PurchaseOrder in a sequence was sorted on server.
        /// </summary>
        /// <param name="ignoreCount">Number of base_PurchaseOrder will ignore.</param>
        /// <param name="takeCount">Number of base_PurchaseOrder will take.</param>
        /// <param name="keys">The key columns by which to order the results.</param>
        /// <returns>The new IList&lt;base_PurchaseOrder&gt; instance.</returns>
        public IList<base_PurchaseOrder> GetRange(int ignoreCount, int takeCount, string keys)
        {
            return UnitOfWork.GetRange<base_PurchaseOrder>(ignoreCount, takeCount, keys);
        }

        /// <summary>
        /// Take a few base_PurchaseOrder in a sequence was sorted on server.
        /// </summary>
        /// <param name="ignoreCount">Number of base_PurchaseOrder will ignore.</param>
        /// <param name="takeCount">Number of base_PurchaseOrder will take.</param>
        /// <param name="keys">The key columns by which to order the results.</param>
        /// <param name="expression">A function to test each base_PurchaseOrder for a condition.</param>
        /// <returns>The new IList&lt;base_PurchaseOrder&gt; instance.</returns>
        public IList<base_PurchaseOrder> GetRange(int ignoreCount, int takeCount, string keys, Expression<Func<base_PurchaseOrder, bool>> expression)
        {
            return UnitOfWork.GetRange<base_PurchaseOrder>(ignoreCount, takeCount, keys, expression);
        }

        /// <summary>
        /// Take a few base_PurchaseOrder in sequence was sorted by descending on server.
        /// </summary>
        /// <typeparam name="TKey">Type of base_PurchaseOrder to sort</typeparam>
        /// <param name="ignoreCount">Number of base_PurchaseOrder will ignore.</param>
        /// <param name="takeCount">Number of base_PurchaseOrder will take.</param>
        /// <param name="keySelector">The key columns by which to order the results.</param>
        /// <returns>The new IList&lt;base_PurchaseOrder&gt; instance.</returns>
        public IList<base_PurchaseOrder> GetRangeDescending<TKey>(int ignoreCount, int takeCount, Expression<Func<base_PurchaseOrder, TKey>> keySelector)
        {
            return UnitOfWork.GetRangeDescending(ignoreCount, takeCount, keySelector);
        }

        /// <summary>
        /// Take a few base_PurchaseOrder in sequence was sorted by descending on server.
        /// </summary>
        /// <typeparam name="TKey">Type of base_PurchaseOrder to sort</typeparam>
        /// <param name="ignoreCount">Number of base_PurchaseOrder will ignore.</param>
        /// <param name="takeCount">Number of base_PurchaseOrder will take.</param>
        /// <param name="keySelector">The key columns by which to order the results.</param>
        /// <param name="expression">A function to test each object for a condition.</param>
        /// <returns>The new IList&lt;base_PurchaseOrder&gt; instance.</returns>
        public IList<base_PurchaseOrder> GetRangeDescending<TKey>(int ignoreCount, int takeCount, Expression<Func<base_PurchaseOrder, TKey>> keySelector, Expression<Func<base_PurchaseOrder, bool>> expression)
        {
            return UnitOfWork.GetRangeDescending(ignoreCount, takeCount, keySelector, expression);
        }

        /// <summary>
        /// Updates an base_PurchaseOrder in the object context with data from the data source.
        /// </summary>
        /// <param name="base_PurchaseOrder">The base_PurchaseOrder to be refreshed.</param>
        public base_PurchaseOrder Refresh(base_PurchaseOrder base_PurchaseOrder)
        {
            UnitOfWork.Refresh<base_PurchaseOrder>(base_PurchaseOrder);
            if (base_PurchaseOrder.EntityState != System.Data.EntityState.Detached)
                return base_PurchaseOrder;
            return null;
        }

        /// <summary>
        /// Updates a sequence of base_PurchaseOrder in the object context with data from the data source.
        /// </summary>
        /// <typeparam name="base_PurchaseOrder">Type of object in a sequence to refresh.</typeparam>
        /// <param name="base_PurchaseOrder">Object collection to be refreshed.</param>
        public void Refresh(IEnumerable<base_PurchaseOrder> base_PurchaseOrder)
        {
            UnitOfWork.Refresh<base_PurchaseOrder>(base_PurchaseOrder);
        }

        /// <summary>
        /// Updates a sequence of base_PurchaseOrder in the object context with data from the data source.
        /// </summary>
        public void Refresh()
        {
            UnitOfWork.Refresh<base_PurchaseOrder>();
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
