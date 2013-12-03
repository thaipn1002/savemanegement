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
    /// Repository for table base_GenericCode 
    /// </summary>
    public partial class base_GenericCodeRepository
    {
        #region Auto Generate Code

        #region Constructors

        // Default constructor
        public base_GenericCodeRepository()
        {
        }

        #endregion

        #region Basic C.R.U.D. Operations

        /// <summary>
        /// Add new base_GenericCode.
        /// </summary>
        /// <param name="base_GenericCode">base_GenericCode to add.</param>
        /// <returns>base_GenericCode have been added.</returns>
        public base_GenericCode Add(base_GenericCode base_GenericCode)
        {
            UnitOfWork.Add<base_GenericCode>(base_GenericCode);
            return base_GenericCode;
        }

        /// <summary>
        /// Adds a sequence of new base_GenericCode.
        /// </summary>
        /// <param name="base_GenericCode">Sequence of new base_GenericCode to add.</param>
        /// <returns>Sequence of new base_GenericCode have been added.</returns>
        public IEnumerable<base_GenericCode> Add(IEnumerable<base_GenericCode> base_GenericCode)
        {
            UnitOfWork.Add<base_GenericCode>(base_GenericCode);
            return base_GenericCode;
        }

        /// <summary>
        /// Delete a existed base_GenericCode.
        /// </summary>
        /// <param name="base_GenericCode">base_GenericCode to delete.</param>
        public void Delete(base_GenericCode base_GenericCode)
        {
            Refresh(base_GenericCode);
            if (base_GenericCode.EntityState != System.Data.EntityState.Detached)
                UnitOfWork.Delete<base_GenericCode>(base_GenericCode);
        }

        /// <summary>
        /// Delete a sequence of existed base_GenericCode.
        /// </summary>
        /// <param name="base_GenericCode">Sequence of existed base_GenericCode to delete.</param>
        public void Delete(IEnumerable<base_GenericCode> base_GenericCode)
        {
            int total = base_GenericCode.Count();
            for (int i = total - 1; i >= 0; i--)
                Delete(base_GenericCode.ElementAt(i));
        }

        /// <summary>
        /// Returns the first base_GenericCode of a sequence that satisfies a specified condition or 
        /// a default value if no such base_GenericCode is found.
        /// </summary>
        /// <param name="expression">A function to test each base_GenericCode for a condition.</param>
        /// <returns>    
        /// Null if source is empty or if no base_GenericCode passes the test specified by expression; 
        /// otherwise, the first base_GenericCode in source that passes the test specified by expression.
        /// </returns>
        public base_GenericCode Get(Expression<Func<base_GenericCode, bool>> expression)
        {
            return UnitOfWork.Get<base_GenericCode>(expression);
        }

        /// <summary>
        /// Get all base_GenericCode.
        /// </summary>
        /// <returns>The new IList&lt;base_GenericCode&gt; instance.</returns>
        public IList<base_GenericCode> GetAll()
        {
            return UnitOfWork.GetAll<base_GenericCode>();
        }

        /// <summary>
        /// Get all base_GenericCode that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_GenericCode for a condition.</param>
        /// <returns>The new IList&lt;base_GenericCode&gt; instance.</returns>
        public IList<base_GenericCode> GetAll(Expression<Func<base_GenericCode, bool>> expression)
        {
            return UnitOfWork.GetAll<base_GenericCode>(expression);
        }

        /// <summary>
        /// Get all base_GenericCode.
        /// </summary>
        /// <returns>The new IEnumerable&lt;base_GenericCode&gt; instance.</returns>
        public IEnumerable<base_GenericCode> GetIEnumerable()
        {
            return UnitOfWork.GetIEnumerable<base_GenericCode>();
        }

        /// <summary>
        /// Get all base_GenericCode that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_GenericCode for a condition.</param>
        /// <returns>The new IEnumerable&lt;base_GenericCode&gt; instance.</returns>
        public IEnumerable<base_GenericCode> GetIEnumerable(Expression<Func<base_GenericCode, bool>> expression)
        {
            return UnitOfWork.GetIEnumerable<base_GenericCode>(expression);
        }

        /// <summary>
        /// Get all base_GenericCode.
        /// </summary>
        /// <returns>The new IQueryable&lt;base_GenericCode&gt; instance.</returns>
        public IQueryable<base_GenericCode> GetIQueryable()
        {
            return UnitOfWork.GetIQueryable<base_GenericCode>();
        }

        /// <summary>
        /// Get all base_GenericCode that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_GenericCode for a condition.</param>
        /// <returns>The new IQueryable&lt;base_GenericCode&gt; instance.</returns>
        public IQueryable<base_GenericCode> GetIQueryable(Expression<Func<base_GenericCode, bool>> expression)
        {
            return UnitOfWork.GetIQueryable<base_GenericCode>(expression);
        }

        /// <summary>
        /// Take a few base_GenericCode in a sequence was sorted on server.
        /// </summary>
        /// <param name="ignoreCount">Number of base_GenericCode will ignore.</param>
        /// <param name="takeCount">Number of base_GenericCode will take.</param>
        /// <param name="keys">The key columns by which to order the results.</param>
        /// <returns>The new IList&lt;base_GenericCode&gt; instance.</returns>
        public IList<base_GenericCode> GetRange(int ignoreCount, int takeCount, string keys)
        {
            return UnitOfWork.GetRange<base_GenericCode>(ignoreCount, takeCount, keys);
        }

        /// <summary>
        /// Take a few base_GenericCode in a sequence was sorted on server.
        /// </summary>
        /// <param name="ignoreCount">Number of base_GenericCode will ignore.</param>
        /// <param name="takeCount">Number of base_GenericCode will take.</param>
        /// <param name="keys">The key columns by which to order the results.</param>
        /// <param name="expression">A function to test each base_GenericCode for a condition.</param>
        /// <returns>The new IList&lt;base_GenericCode&gt; instance.</returns>
        public IList<base_GenericCode> GetRange(int ignoreCount, int takeCount, string keys, Expression<Func<base_GenericCode, bool>> expression)
        {
            return UnitOfWork.GetRange<base_GenericCode>(ignoreCount, takeCount, keys, expression);
        }

        /// <summary>
        /// Take a few base_GenericCode in sequence was sorted by descending on server.
        /// </summary>
        /// <typeparam name="TKey">Type of base_GenericCode to sort</typeparam>
        /// <param name="ignoreCount">Number of base_GenericCode will ignore.</param>
        /// <param name="takeCount">Number of base_GenericCode will take.</param>
        /// <param name="keySelector">The key columns by which to order the results.</param>
        /// <returns>The new IList&lt;base_GenericCode&gt; instance.</returns>
        public IList<base_GenericCode> GetRangeDescending<TKey>(int ignoreCount, int takeCount, Expression<Func<base_GenericCode, TKey>> keySelector)
        {
            return UnitOfWork.GetRangeDescending(ignoreCount, takeCount, keySelector);
        }

        /// <summary>
        /// Take a few base_GenericCode in sequence was sorted by descending on server.
        /// </summary>
        /// <typeparam name="TKey">Type of base_GenericCode to sort</typeparam>
        /// <param name="ignoreCount">Number of base_GenericCode will ignore.</param>
        /// <param name="takeCount">Number of base_GenericCode will take.</param>
        /// <param name="keySelector">The key columns by which to order the results.</param>
        /// <param name="expression">A function to test each object for a condition.</param>
        /// <returns>The new IList&lt;base_GenericCode&gt; instance.</returns>
        public IList<base_GenericCode> GetRangeDescending<TKey>(int ignoreCount, int takeCount, Expression<Func<base_GenericCode, TKey>> keySelector, Expression<Func<base_GenericCode, bool>> expression)
        {
            return UnitOfWork.GetRangeDescending(ignoreCount, takeCount, keySelector, expression);
        }

        /// <summary>
        /// Updates an base_GenericCode in the object context with data from the data source.
        /// </summary>
        /// <param name="base_GenericCode">The base_GenericCode to be refreshed.</param>
        public base_GenericCode Refresh(base_GenericCode base_GenericCode)
        {
            UnitOfWork.Refresh<base_GenericCode>(base_GenericCode);
            if (base_GenericCode.EntityState != System.Data.EntityState.Detached)
                return base_GenericCode;
            return null;
        }

        /// <summary>
        /// Updates a sequence of base_GenericCode in the object context with data from the data source.
        /// </summary>
        /// <typeparam name="base_GenericCode">Type of object in a sequence to refresh.</typeparam>
        /// <param name="base_GenericCode">Object collection to be refreshed.</param>
        public void Refresh(IEnumerable<base_GenericCode> base_GenericCode)
        {
            UnitOfWork.Refresh<base_GenericCode>(base_GenericCode);
        }

        /// <summary>
        /// Updates a sequence of base_GenericCode in the object context with data from the data source.
        /// </summary>
        public void Refresh()
        {
            UnitOfWork.Refresh<base_GenericCode>();
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
