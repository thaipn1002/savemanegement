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
    /// Repository for table base_UserLogDetail 
    /// </summary>
    public partial class base_UserLogDetailRepository
    {
        #region Auto Generate Code

        #region Constructors

        // Default constructor
        public base_UserLogDetailRepository()
        {
        }

        #endregion

        #region Basic C.R.U.D. Operations

        /// <summary>
        /// Add new base_UserLogDetail.
        /// </summary>
        /// <param name="base_UserLogDetail">base_UserLogDetail to add.</param>
        /// <returns>base_UserLogDetail have been added.</returns>
        public base_UserLogDetail Add(base_UserLogDetail base_UserLogDetail)
        {
            UnitOfWork.Add<base_UserLogDetail>(base_UserLogDetail);
            return base_UserLogDetail;
        }

        /// <summary>
        /// Adds a sequence of new base_UserLogDetail.
        /// </summary>
        /// <param name="base_UserLogDetail">Sequence of new base_UserLogDetail to add.</param>
        /// <returns>Sequence of new base_UserLogDetail have been added.</returns>
        public IEnumerable<base_UserLogDetail> Add(IEnumerable<base_UserLogDetail> base_UserLogDetail)
        {
            UnitOfWork.Add<base_UserLogDetail>(base_UserLogDetail);
            return base_UserLogDetail;
        }

        /// <summary>
        /// Delete a existed base_UserLogDetail.
        /// </summary>
        /// <param name="base_UserLogDetail">base_UserLogDetail to delete.</param>
        public void Delete(base_UserLogDetail base_UserLogDetail)
        {
            Refresh(base_UserLogDetail);
            if (base_UserLogDetail.EntityState != System.Data.EntityState.Detached)
                UnitOfWork.Delete<base_UserLogDetail>(base_UserLogDetail);
        }

        /// <summary>
        /// Delete a sequence of existed base_UserLogDetail.
        /// </summary>
        /// <param name="base_UserLogDetail">Sequence of existed base_UserLogDetail to delete.</param>
        public void Delete(IEnumerable<base_UserLogDetail> base_UserLogDetail)
        {
            int total = base_UserLogDetail.Count();
            for (int i = total - 1; i >= 0; i--)
                Delete(base_UserLogDetail.ElementAt(i));
        }

        /// <summary>
        /// Returns the first base_UserLogDetail of a sequence that satisfies a specified condition or 
        /// a default value if no such base_UserLogDetail is found.
        /// </summary>
        /// <param name="expression">A function to test each base_UserLogDetail for a condition.</param>
        /// <returns>    
        /// Null if source is empty or if no base_UserLogDetail passes the test specified by expression; 
        /// otherwise, the first base_UserLogDetail in source that passes the test specified by expression.
        /// </returns>
        public base_UserLogDetail Get(Expression<Func<base_UserLogDetail, bool>> expression)
        {
            return UnitOfWork.Get<base_UserLogDetail>(expression);
        }

        /// <summary>
        /// Get all base_UserLogDetail.
        /// </summary>
        /// <returns>The new IList&lt;base_UserLogDetail&gt; instance.</returns>
        public IList<base_UserLogDetail> GetAll()
        {
            return UnitOfWork.GetAll<base_UserLogDetail>().ToList();
        }

        /// <summary>
        /// Get all base_UserLogDetail that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_UserLogDetail for a condition.</param>
        /// <returns>The new IList&lt;base_UserLogDetail&gt; instance.</returns>
        public IList<base_UserLogDetail> GetAll(Expression<Func<base_UserLogDetail, bool>> expression)
        {
            return UnitOfWork.GetAll<base_UserLogDetail>(expression).ToList();
        }

        /// <summary>
        /// Get all base_UserLogDetail.
        /// </summary>
        /// <returns>The new IEnumerable&lt;base_UserLogDetail&gt; instance.</returns>
        public IEnumerable<base_UserLogDetail> GetIEnumerable()
        {
            return UnitOfWork.GetIEnumerable<base_UserLogDetail>();
        }

        /// <summary>
        /// Get all base_UserLogDetail that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_UserLogDetail for a condition.</param>
        /// <returns>The new IEnumerable&lt;base_UserLogDetail&gt; instance.</returns>
        public IEnumerable<base_UserLogDetail> GetIEnumerable(Expression<Func<base_UserLogDetail, bool>> expression)
        {
            return UnitOfWork.GetIEnumerable<base_UserLogDetail>(expression);
        }

        /// <summary>
        /// Get all base_UserLogDetail.
        /// </summary>
        /// <returns>The new IQueryable&lt;base_UserLogDetail&gt; instance.</returns>
        public IQueryable<base_UserLogDetail> GetIQueryable()
        {
            return UnitOfWork.GetIQueryable<base_UserLogDetail>();
        }

        /// <summary>
        /// Get all base_UserLogDetail that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_UserLogDetail for a condition.</param>
        /// <returns>The new IQueryable&lt;base_UserLogDetail&gt; instance.</returns>
        public IQueryable<base_UserLogDetail> GetIQueryable(Expression<Func<base_UserLogDetail, bool>> expression)
        {
            return UnitOfWork.GetIQueryable<base_UserLogDetail>(expression);
        }

        /// <summary>
        /// Take a few base_UserLogDetail in a sequence was sorted on server.
        /// </summary>
        /// <param name="ignoreCount">Number of base_UserLogDetail will ignore.</param>
        /// <param name="takeCount">Number of base_UserLogDetail will take.</param>
        /// <param name="keys">The key columns by which to order the results.</param>
        /// <returns>The new IList&lt;base_UserLogDetail&gt; instance.</returns>
        public IList<base_UserLogDetail> GetRange(int ignoreCount, int takeCount, string keys)
        {
            return UnitOfWork.GetRange<base_UserLogDetail>(ignoreCount, takeCount, keys);
        }

        /// <summary>
        /// Take a few base_UserLogDetail in a sequence was sorted on server.
        /// </summary>
        /// <param name="ignoreCount">Number of base_UserLogDetail will ignore.</param>
        /// <param name="takeCount">Number of base_UserLogDetail will take.</param>
        /// <param name="keys">The key columns by which to order the results.</param>
        /// <param name="expression">A function to test each base_UserLogDetail for a condition.</param>
        /// <returns>The new IList&lt;base_UserLogDetail&gt; instance.</returns>
        public IList<base_UserLogDetail> GetRange(int ignoreCount, int takeCount, string keys, Expression<Func<base_UserLogDetail, bool>> expression)
        {
            return UnitOfWork.GetRange<base_UserLogDetail>(ignoreCount, takeCount, keys, expression);
        }

        /// <summary>
        /// Updates an base_UserLogDetail in the object context with data from the data source.
        /// </summary>
        /// <param name="base_UserLogDetail">The base_UserLogDetail to be refreshed.</param>
        public base_UserLogDetail Refresh(base_UserLogDetail base_UserLogDetail)
        {
            UnitOfWork.Refresh<base_UserLogDetail>(base_UserLogDetail);
            if (base_UserLogDetail.EntityState != System.Data.EntityState.Detached)
                return base_UserLogDetail;
            return null;
        }

        /// <summary>
        /// Updates a sequence of base_UserLogDetail in the object context with data from the data source.
        /// </summary>
        /// <typeparam name="base_UserLogDetail">Type of object in a sequence to refresh.</typeparam>
        /// <param name="base_UserLogDetail">Object collection to be refreshed.</param>
        public void Refresh(IEnumerable<base_UserLogDetail> base_UserLogDetail)
        {
            UnitOfWork.Refresh<base_UserLogDetail>(base_UserLogDetail);
        }

        /// <summary>
        /// Updates a sequence of base_UserLogDetail in the object context with data from the data source.
        /// </summary>
        public void Refresh()
        {
            UnitOfWork.Refresh<base_UserLogDetail>();
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
