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
    /// Repository for table base_PurchaseOrderDetail 
    /// </summary>
    public partial class base_PurchaseOrderDetailRepository
    {
        #region Auto Generate Code

        #region Constructors

        // Default constructor
        public base_PurchaseOrderDetailRepository()
        {
        }

        #endregion

        #region Basic C.R.U.D. Operations

        /// <summary>
        /// Add new base_PurchaseOrderDetail.
        /// </summary>
        /// <param name="base_PurchaseOrderDetail">base_PurchaseOrderDetail to add.</param>
        /// <returns>base_PurchaseOrderDetail have been added.</returns>
        public base_PurchaseOrderDetail Add(base_PurchaseOrderDetail base_PurchaseOrderDetail)
        {
            UnitOfWork.Add<base_PurchaseOrderDetail>(base_PurchaseOrderDetail);
            return base_PurchaseOrderDetail;
        }

        /// <summary>
        /// Adds a sequence of new base_PurchaseOrderDetail.
        /// </summary>
        /// <param name="base_PurchaseOrderDetail">Sequence of new base_PurchaseOrderDetail to add.</param>
        /// <returns>Sequence of new base_PurchaseOrderDetail have been added.</returns>
        public IEnumerable<base_PurchaseOrderDetail> Add(IEnumerable<base_PurchaseOrderDetail> base_PurchaseOrderDetail)
        {
            UnitOfWork.Add<base_PurchaseOrderDetail>(base_PurchaseOrderDetail);
            return base_PurchaseOrderDetail;
        }

        /// <summary>
        /// Delete a existed base_PurchaseOrderDetail.
        /// </summary>
        /// <param name="base_PurchaseOrderDetail">base_PurchaseOrderDetail to delete.</param>
        public void Delete(base_PurchaseOrderDetail base_PurchaseOrderDetail)
        {
            Refresh(base_PurchaseOrderDetail);
            if (base_PurchaseOrderDetail.EntityState != System.Data.EntityState.Detached)
                UnitOfWork.Delete<base_PurchaseOrderDetail>(base_PurchaseOrderDetail);
        }

        /// <summary>
        /// Delete a sequence of existed base_PurchaseOrderDetail.
        /// </summary>
        /// <param name="base_PurchaseOrderDetail">Sequence of existed base_PurchaseOrderDetail to delete.</param>
        public void Delete(IEnumerable<base_PurchaseOrderDetail> base_PurchaseOrderDetail)
        {
            int total = base_PurchaseOrderDetail.Count();
            for (int i = total - 1; i >= 0; i--)
                Delete(base_PurchaseOrderDetail.ElementAt(i));
        }

        /// <summary>
        /// Returns the first base_PurchaseOrderDetail of a sequence that satisfies a specified condition or 
        /// a default value if no such base_PurchaseOrderDetail is found.
        /// </summary>
        /// <param name="expression">A function to test each base_PurchaseOrderDetail for a condition.</param>
        /// <returns>    
        /// Null if source is empty or if no base_PurchaseOrderDetail passes the test specified by expression; 
        /// otherwise, the first base_PurchaseOrderDetail in source that passes the test specified by expression.
        /// </returns>
        public base_PurchaseOrderDetail Get(Expression<Func<base_PurchaseOrderDetail, bool>> expression)
        {
            return UnitOfWork.Get<base_PurchaseOrderDetail>(expression);
        }

        /// <summary>
        /// Get all base_PurchaseOrderDetail.
        /// </summary>
        /// <returns>The new IList&lt;base_PurchaseOrderDetail&gt; instance.</returns>
        public IList<base_PurchaseOrderDetail> GetAll()
        {
            return UnitOfWork.GetAll<base_PurchaseOrderDetail>();
        }

        /// <summary>
        /// Get all base_PurchaseOrderDetail that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_PurchaseOrderDetail for a condition.</param>
        /// <returns>The new IList&lt;base_PurchaseOrderDetail&gt; instance.</returns>
        public IList<base_PurchaseOrderDetail> GetAll(Expression<Func<base_PurchaseOrderDetail, bool>> expression)
        {
            return UnitOfWork.GetAll<base_PurchaseOrderDetail>(expression);
        }

        /// <summary>
        /// Get all base_PurchaseOrderDetail.
        /// </summary>
        /// <returns>The new IEnumerable&lt;base_PurchaseOrderDetail&gt; instance.</returns>
        public IEnumerable<base_PurchaseOrderDetail> GetIEnumerable()
        {
            return UnitOfWork.GetIEnumerable<base_PurchaseOrderDetail>();
        }

        /// <summary>
        /// Get all base_PurchaseOrderDetail that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_PurchaseOrderDetail for a condition.</param>
        /// <returns>The new IEnumerable&lt;base_PurchaseOrderDetail&gt; instance.</returns>
        public IEnumerable<base_PurchaseOrderDetail> GetIEnumerable(Expression<Func<base_PurchaseOrderDetail, bool>> expression)
        {
            return UnitOfWork.GetIEnumerable<base_PurchaseOrderDetail>(expression);
        }

        /// <summary>
        /// Get all base_PurchaseOrderDetail.
        /// </summary>
        /// <returns>The new IQueryable&lt;base_PurchaseOrderDetail&gt; instance.</returns>
        public IQueryable<base_PurchaseOrderDetail> GetIQueryable()
        {
            return UnitOfWork.GetIQueryable<base_PurchaseOrderDetail>();
        }

        /// <summary>
        /// Get all base_PurchaseOrderDetail that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_PurchaseOrderDetail for a condition.</param>
        /// <returns>The new IQueryable&lt;base_PurchaseOrderDetail&gt; instance.</returns>
        public IQueryable<base_PurchaseOrderDetail> GetIQueryable(Expression<Func<base_PurchaseOrderDetail, bool>> expression)
        {
            return UnitOfWork.GetIQueryable<base_PurchaseOrderDetail>(expression);
        }

        /// <summary>
        /// Take a few base_PurchaseOrderDetail in a sequence was sorted on server.
        /// </summary>
        /// <param name="ignoreCount">Number of base_PurchaseOrderDetail will ignore.</param>
        /// <param name="takeCount">Number of base_PurchaseOrderDetail will take.</param>
        /// <param name="keys">The key columns by which to order the results.</param>
        /// <returns>The new IList&lt;base_PurchaseOrderDetail&gt; instance.</returns>
        public IList<base_PurchaseOrderDetail> GetRange(int ignoreCount, int takeCount, string keys)
        {
            return UnitOfWork.GetRange<base_PurchaseOrderDetail>(ignoreCount, takeCount, keys);
        }

        /// <summary>
        /// Take a few base_PurchaseOrderDetail in a sequence was sorted on server.
        /// </summary>
        /// <param name="ignoreCount">Number of base_PurchaseOrderDetail will ignore.</param>
        /// <param name="takeCount">Number of base_PurchaseOrderDetail will take.</param>
        /// <param name="keys">The key columns by which to order the results.</param>
        /// <param name="expression">A function to test each base_PurchaseOrderDetail for a condition.</param>
        /// <returns>The new IList&lt;base_PurchaseOrderDetail&gt; instance.</returns>
        public IList<base_PurchaseOrderDetail> GetRange(int ignoreCount, int takeCount, string keys, Expression<Func<base_PurchaseOrderDetail, bool>> expression)
        {
            return UnitOfWork.GetRange<base_PurchaseOrderDetail>(ignoreCount, takeCount, keys, expression);
        }

        /// <summary>
        /// Take a few base_PurchaseOrderDetail in sequence was sorted by descending on server.
        /// </summary>
        /// <typeparam name="TKey">Type of base_PurchaseOrderDetail to sort</typeparam>
        /// <param name="ignoreCount">Number of base_PurchaseOrderDetail will ignore.</param>
        /// <param name="takeCount">Number of base_PurchaseOrderDetail will take.</param>
        /// <param name="keySelector">The key columns by which to order the results.</param>
        /// <returns>The new IList&lt;base_PurchaseOrderDetail&gt; instance.</returns>
        public IList<base_PurchaseOrderDetail> GetRangeDescending<TKey>(int ignoreCount, int takeCount, Expression<Func<base_PurchaseOrderDetail, TKey>> keySelector)
        {
            return UnitOfWork.GetRangeDescending(ignoreCount, takeCount, keySelector);
        }

        /// <summary>
        /// Take a few base_PurchaseOrderDetail in sequence was sorted by descending on server.
        /// </summary>
        /// <typeparam name="TKey">Type of base_PurchaseOrderDetail to sort</typeparam>
        /// <param name="ignoreCount">Number of base_PurchaseOrderDetail will ignore.</param>
        /// <param name="takeCount">Number of base_PurchaseOrderDetail will take.</param>
        /// <param name="keySelector">The key columns by which to order the results.</param>
        /// <param name="expression">A function to test each object for a condition.</param>
        /// <returns>The new IList&lt;base_PurchaseOrderDetail&gt; instance.</returns>
        public IList<base_PurchaseOrderDetail> GetRangeDescending<TKey>(int ignoreCount, int takeCount, Expression<Func<base_PurchaseOrderDetail, TKey>> keySelector, Expression<Func<base_PurchaseOrderDetail, bool>> expression)
        {
            return UnitOfWork.GetRangeDescending(ignoreCount, takeCount, keySelector, expression);
        }

        /// <summary>
        /// Updates an base_PurchaseOrderDetail in the object context with data from the data source.
        /// </summary>
        /// <param name="base_PurchaseOrderDetail">The base_PurchaseOrderDetail to be refreshed.</param>
        public base_PurchaseOrderDetail Refresh(base_PurchaseOrderDetail base_PurchaseOrderDetail)
        {
            UnitOfWork.Refresh<base_PurchaseOrderDetail>(base_PurchaseOrderDetail);
            if (base_PurchaseOrderDetail.EntityState != System.Data.EntityState.Detached)
                return base_PurchaseOrderDetail;
            return null;
        }

        /// <summary>
        /// Updates a sequence of base_PurchaseOrderDetail in the object context with data from the data source.
        /// </summary>
        /// <typeparam name="base_PurchaseOrderDetail">Type of object in a sequence to refresh.</typeparam>
        /// <param name="base_PurchaseOrderDetail">Object collection to be refreshed.</param>
        public void Refresh(IEnumerable<base_PurchaseOrderDetail> base_PurchaseOrderDetail)
        {
            UnitOfWork.Refresh<base_PurchaseOrderDetail>(base_PurchaseOrderDetail);
        }

        /// <summary>
        /// Updates a sequence of base_PurchaseOrderDetail in the object context with data from the data source.
        /// </summary>
        public void Refresh()
        {
            UnitOfWork.Refresh<base_PurchaseOrderDetail>();
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
