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
    /// Repository for table base_CountStockDetail 
    /// </summary>
    public partial class base_CountStockDetailRepository
    {
        #region Auto Generate Code

        #region Constructors

        // Default constructor
        public base_CountStockDetailRepository()
        {
        }

        #endregion

        #region Basic C.R.U.D. Operations

        /// <summary>
        /// Add new base_CountStockDetail.
        /// </summary>
        /// <param name="base_CountStockDetail">base_CountStockDetail to add.</param>
        /// <returns>base_CountStockDetail have been added.</returns>
        public base_CountStockDetail Add(base_CountStockDetail base_CountStockDetail)
        {
            UnitOfWork.Add<base_CountStockDetail>(base_CountStockDetail);
            return base_CountStockDetail;
        }

        /// <summary>
        /// Adds a sequence of new base_CountStockDetail.
        /// </summary>
        /// <param name="base_CountStockDetail">Sequence of new base_CountStockDetail to add.</param>
        /// <returns>Sequence of new base_CountStockDetail have been added.</returns>
        public IEnumerable<base_CountStockDetail> Add(IEnumerable<base_CountStockDetail> base_CountStockDetail)
        {
            UnitOfWork.Add<base_CountStockDetail>(base_CountStockDetail);
            return base_CountStockDetail;
        }

        /// <summary>
        /// Delete a existed base_CountStockDetail.
        /// </summary>
        /// <param name="base_CountStockDetail">base_CountStockDetail to delete.</param>
        public void Delete(base_CountStockDetail base_CountStockDetail)
        {
            Refresh(base_CountStockDetail);
            if (base_CountStockDetail.EntityState != System.Data.EntityState.Detached)
                UnitOfWork.Delete<base_CountStockDetail>(base_CountStockDetail);
        }

        /// <summary>
        /// Delete a sequence of existed base_CountStockDetail.
        /// </summary>
        /// <param name="base_CountStockDetail">Sequence of existed base_CountStockDetail to delete.</param>
        public void Delete(IEnumerable<base_CountStockDetail> base_CountStockDetail)
        {
            int total = base_CountStockDetail.Count();
            for (int i = total - 1; i >= 0; i--)
                Delete(base_CountStockDetail.ElementAt(i));
        }

        /// <summary>
        /// Returns the first base_CountStockDetail of a sequence that satisfies a specified condition or 
        /// a default value if no such base_CountStockDetail is found.
        /// </summary>
        /// <param name="expression">A function to test each base_CountStockDetail for a condition.</param>
        /// <returns>    
        /// Null if source is empty or if no base_CountStockDetail passes the test specified by expression; 
        /// otherwise, the first base_CountStockDetail in source that passes the test specified by expression.
        /// </returns>
        public base_CountStockDetail Get(Expression<Func<base_CountStockDetail, bool>> expression)
        {
            return UnitOfWork.Get<base_CountStockDetail>(expression);
        }

        /// <summary>
        /// Get all base_CountStockDetail.
        /// </summary>
        /// <returns>The new IList&lt;base_CountStockDetail&gt; instance.</returns>
        public IList<base_CountStockDetail> GetAll()
        {
            return UnitOfWork.GetAll<base_CountStockDetail>().ToList();
        }

        /// <summary>
        /// Get all base_CountStockDetail that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_CountStockDetail for a condition.</param>
        /// <returns>The new IList&lt;base_CountStockDetail&gt; instance.</returns>
        public IList<base_CountStockDetail> GetAll(Expression<Func<base_CountStockDetail, bool>> expression)
        {
            return UnitOfWork.GetAll<base_CountStockDetail>(expression).ToList();
        }

        /// <summary>
        /// Get all base_CountStockDetail.
        /// </summary>
        /// <returns>The new IEnumerable&lt;base_CountStockDetail&gt; instance.</returns>
        public IEnumerable<base_CountStockDetail> GetIEnumerable()
        {
            return UnitOfWork.GetIEnumerable<base_CountStockDetail>();
        }

        /// <summary>
        /// Get all base_CountStockDetail that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_CountStockDetail for a condition.</param>
        /// <returns>The new IEnumerable&lt;base_CountStockDetail&gt; instance.</returns>
        public IEnumerable<base_CountStockDetail> GetIEnumerable(Expression<Func<base_CountStockDetail, bool>> expression)
        {
            return UnitOfWork.GetIEnumerable<base_CountStockDetail>(expression);
        }

        /// <summary>
        /// Get all base_CountStockDetail.
        /// </summary>
        /// <returns>The new IQueryable&lt;base_CountStockDetail&gt; instance.</returns>
        public IQueryable<base_CountStockDetail> GetIQueryable()
        {
            return UnitOfWork.GetIQueryable<base_CountStockDetail>();
        }

        /// <summary>
        /// Get all base_CountStockDetail that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_CountStockDetail for a condition.</param>
        /// <returns>The new IQueryable&lt;base_CountStockDetail&gt; instance.</returns>
        public IQueryable<base_CountStockDetail> GetIQueryable(Expression<Func<base_CountStockDetail, bool>> expression)
        {
            return UnitOfWork.GetIQueryable<base_CountStockDetail>(expression);
        }

        /// <summary>
        /// Take a few base_CountStockDetail in a sequence was sorted on server.
        /// </summary>
        /// <param name="ignoreCount">Number of base_CountStockDetail will ignore.</param>
        /// <param name="takeCount">Number of base_CountStockDetail will take.</param>
        /// <param name="keys">The key columns by which to order the results.</param>
        /// <returns>The new IList&lt;base_CountStockDetail&gt; instance.</returns>
        public IList<base_CountStockDetail> GetRange(int ignoreCount, int takeCount, string keys)
        {
            return UnitOfWork.GetRange<base_CountStockDetail>(ignoreCount, takeCount, keys);
        }

        /// <summary>
        /// Take a few base_CountStockDetail in a sequence was sorted on server.
        /// </summary>
        /// <param name="ignoreCount">Number of base_CountStockDetail will ignore.</param>
        /// <param name="takeCount">Number of base_CountStockDetail will take.</param>
        /// <param name="keys">The key columns by which to order the results.</param>
        /// <param name="expression">A function to test each base_CountStockDetail for a condition.</param>
        /// <returns>The new IList&lt;base_CountStockDetail&gt; instance.</returns>
        public IList<base_CountStockDetail> GetRange(int ignoreCount, int takeCount, string keys, Expression<Func<base_CountStockDetail, bool>> expression)
        {
            return UnitOfWork.GetRange<base_CountStockDetail>(ignoreCount, takeCount, keys, expression);
        }

        /// <summary>
        /// Updates an base_CountStockDetail in the object context with data from the data source.
        /// </summary>
        /// <param name="base_CountStockDetail">The base_CountStockDetail to be refreshed.</param>
        public base_CountStockDetail Refresh(base_CountStockDetail base_CountStockDetail)
        {
            UnitOfWork.Refresh<base_CountStockDetail>(base_CountStockDetail);
            if (base_CountStockDetail.EntityState != System.Data.EntityState.Detached)
                return base_CountStockDetail;
            return null;
        }

        /// <summary>
        /// Updates a sequence of base_CountStockDetail in the object context with data from the data source.
        /// </summary>
        /// <typeparam name="base_CountStockDetail">Type of object in a sequence to refresh.</typeparam>
        /// <param name="base_CountStockDetail">Object collection to be refreshed.</param>
        public void Refresh(IEnumerable<base_CountStockDetail> base_CountStockDetail)
        {
            UnitOfWork.Refresh<base_CountStockDetail>(base_CountStockDetail);
        }

        /// <summary>
        /// Updates a sequence of base_CountStockDetail in the object context with data from the data source.
        /// </summary>
        public void Refresh()
        {
            UnitOfWork.Refresh<base_CountStockDetail>();
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
