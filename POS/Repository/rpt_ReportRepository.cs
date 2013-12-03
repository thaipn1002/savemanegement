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
    /// Repository for table rpt_Report 
    /// </summary>
    public partial class rpt_ReportRepository
    {
        #region Auto Generate Code

        #region Constructors

        // Default constructor
        public rpt_ReportRepository()
        {
        }

        #endregion

        #region Basic C.R.U.D. Operations

        /// <summary>
        /// Add new rpt_Report.
        /// </summary>
        /// <param name="rpt_Report">rpt_Report to add.</param>
        /// <returns>rpt_Report have been added.</returns>
        public rpt_Report Add(rpt_Report rpt_Report)
        {
            UnitOfWork.Add<rpt_Report>(rpt_Report);
            return rpt_Report;
        }

        /// <summary>
        /// Adds a sequence of new rpt_Report.
        /// </summary>
        /// <param name="rpt_Report">Sequence of new rpt_Report to add.</param>
        /// <returns>Sequence of new rpt_Report have been added.</returns>
        public IEnumerable<rpt_Report> Add(IEnumerable<rpt_Report> rpt_Report)
        {
            UnitOfWork.Add<rpt_Report>(rpt_Report);
            return rpt_Report;
        }

        /// <summary>
        /// Delete a existed rpt_Report.
        /// </summary>
        /// <param name="rpt_Report">rpt_Report to delete.</param>
        public void Delete(rpt_Report rpt_Report)
        {
            Refresh(rpt_Report);
            if (rpt_Report.EntityState != System.Data.EntityState.Detached)
                UnitOfWork.Delete<rpt_Report>(rpt_Report);
        }

        /// <summary>
        /// Delete a sequence of existed rpt_Report.
        /// </summary>
        /// <param name="rpt_Report">Sequence of existed rpt_Report to delete.</param>
        public void Delete(IEnumerable<rpt_Report> rpt_Report)
        {
            int total = rpt_Report.Count();
            for (int i = total - 1; i >= 0; i--)
                Delete(rpt_Report.ElementAt(i));
        }

        /// <summary>
        /// Returns the first rpt_Report of a sequence that satisfies a specified condition or 
        /// a default value if no such rpt_Report is found.
        /// </summary>
        /// <param name="expression">A function to test each rpt_Report for a condition.</param>
        /// <returns>    
        /// Null if source is empty or if no rpt_Report passes the test specified by expression; 
        /// otherwise, the first rpt_Report in source that passes the test specified by expression.
        /// </returns>
        public rpt_Report Get(Expression<Func<rpt_Report, bool>> expression)
        {
            return UnitOfWork.Get<rpt_Report>(expression);
        }

        /// <summary>
        /// Get all rpt_Report.
        /// </summary>
        /// <returns>The new IList&lt;rpt_Report&gt; instance.</returns>
        public IList<rpt_Report> GetAll()
        {
            return UnitOfWork.GetAll<rpt_Report>();
        }

        /// <summary>
        /// Get all rpt_Report that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each rpt_Report for a condition.</param>
        /// <returns>The new IList&lt;rpt_Report&gt; instance.</returns>
        public IList<rpt_Report> GetAll(Expression<Func<rpt_Report, bool>> expression)
        {
            return UnitOfWork.GetAll<rpt_Report>(expression);
        }

        /// <summary>
        /// Get all rpt_Report.
        /// </summary>
        /// <returns>The new IEnumerable&lt;rpt_Report&gt; instance.</returns>
        public IEnumerable<rpt_Report> GetIEnumerable()
        {
            return UnitOfWork.GetIEnumerable<rpt_Report>();
        }

        /// <summary>
        /// Get all rpt_Report that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each rpt_Report for a condition.</param>
        /// <returns>The new IEnumerable&lt;rpt_Report&gt; instance.</returns>
        public IEnumerable<rpt_Report> GetIEnumerable(Expression<Func<rpt_Report, bool>> expression)
        {
            return UnitOfWork.GetIEnumerable<rpt_Report>(expression);
        }

        /// <summary>
        /// Get all rpt_Report.
        /// </summary>
        /// <returns>The new IQueryable&lt;rpt_Report&gt; instance.</returns>
        public IQueryable<rpt_Report> GetIQueryable()
        {
            return UnitOfWork.GetIQueryable<rpt_Report>();
        }

        /// <summary>
        /// Get all rpt_Report that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each rpt_Report for a condition.</param>
        /// <returns>The new IQueryable&lt;rpt_Report&gt; instance.</returns>
        public IQueryable<rpt_Report> GetIQueryable(Expression<Func<rpt_Report, bool>> expression)
        {
            return UnitOfWork.GetIQueryable<rpt_Report>(expression);
        }

        /// <summary>
        /// Take a few rpt_Report in a sequence was sorted on server.
        /// </summary>
        /// <param name="ignoreCount">Number of rpt_Report will ignore.</param>
        /// <param name="takeCount">Number of rpt_Report will take.</param>
        /// <param name="keys">The key columns by which to order the results.</param>
        /// <returns>The new IList&lt;rpt_Report&gt; instance.</returns>
        public IList<rpt_Report> GetRange(int ignoreCount, int takeCount, string keys)
        {
            return UnitOfWork.GetRange<rpt_Report>(ignoreCount, takeCount, keys);
        }

        /// <summary>
        /// Take a few rpt_Report in a sequence was sorted on server.
        /// </summary>
        /// <param name="ignoreCount">Number of rpt_Report will ignore.</param>
        /// <param name="takeCount">Number of rpt_Report will take.</param>
        /// <param name="keys">The key columns by which to order the results.</param>
        /// <param name="expression">A function to test each rpt_Report for a condition.</param>
        /// <returns>The new IList&lt;rpt_Report&gt; instance.</returns>
        public IList<rpt_Report> GetRange(int ignoreCount, int takeCount, string keys, Expression<Func<rpt_Report, bool>> expression)
        {
            return UnitOfWork.GetRange<rpt_Report>(ignoreCount, takeCount, keys, expression);
        }

        /// <summary>
        /// Take a few rpt_Report in sequence was sorted by descending on server.
        /// </summary>
        /// <typeparam name="TKey">Type of rpt_Report to sort</typeparam>
        /// <param name="ignoreCount">Number of rpt_Report will ignore.</param>
        /// <param name="takeCount">Number of rpt_Report will take.</param>
        /// <param name="keySelector">The key columns by which to order the results.</param>
        /// <returns>The new IList&lt;rpt_Report&gt; instance.</returns>
        public IList<rpt_Report> GetRangeDescending<TKey>(int ignoreCount, int takeCount, Expression<Func<rpt_Report, TKey>> keySelector)
        {
            return UnitOfWork.GetRangeDescending(ignoreCount, takeCount, keySelector);
        }

        /// <summary>
        /// Take a few rpt_Report in sequence was sorted by descending on server.
        /// </summary>
        /// <typeparam name="TKey">Type of rpt_Report to sort</typeparam>
        /// <param name="ignoreCount">Number of rpt_Report will ignore.</param>
        /// <param name="takeCount">Number of rpt_Report will take.</param>
        /// <param name="keySelector">The key columns by which to order the results.</param>
        /// <param name="expression">A function to test each object for a condition.</param>
        /// <returns>The new IList&lt;rpt_Report&gt; instance.</returns>
        public IList<rpt_Report> GetRangeDescending<TKey>(int ignoreCount, int takeCount, Expression<Func<rpt_Report, TKey>> keySelector, Expression<Func<rpt_Report, bool>> expression)
        {
            return UnitOfWork.GetRangeDescending(ignoreCount, takeCount, keySelector, expression);
        }

        /// <summary>
        /// Updates an rpt_Report in the object context with data from the data source.
        /// </summary>
        /// <param name="rpt_Report">The rpt_Report to be refreshed.</param>
        public rpt_Report Refresh(rpt_Report rpt_Report)
        {
            UnitOfWork.Refresh<rpt_Report>(rpt_Report);
            if (rpt_Report.EntityState != System.Data.EntityState.Detached)
                return rpt_Report;
            return null;
        }

        /// <summary>
        /// Updates a sequence of rpt_Report in the object context with data from the data source.
        /// </summary>
        /// <typeparam name="rpt_Report">Type of object in a sequence to refresh.</typeparam>
        /// <param name="rpt_Report">Object collection to be refreshed.</param>
        public void Refresh(IEnumerable<rpt_Report> rpt_Report)
        {
            UnitOfWork.Refresh<rpt_Report>(rpt_Report);
        }

        /// <summary>
        /// Updates a sequence of rpt_Report in the object context with data from the data source.
        /// </summary>
        public void Refresh()
        {
            UnitOfWork.Refresh<rpt_Report>();
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
