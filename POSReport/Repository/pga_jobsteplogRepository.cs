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
    /// Repository for table pga_jobsteplog 
    /// </summary>
    public partial class pga_jobsteplogRepository
    {
        #region Auto Generate Code

        #region Constructors

        // Default constructor
        public pga_jobsteplogRepository()
        {
        }

        #endregion

        #region Basic C.R.U.D. Operations

        /// <summary>
        /// Add new pga_jobsteplog.
        /// </summary>
        /// <param name="pga_jobsteplog">pga_jobsteplog to add.</param>
        /// <returns>pga_jobsteplog have been added.</returns>
        public pga_jobsteplog Add(pga_jobsteplog pga_jobsteplog)
        {
            UnitOfWork.Add<pga_jobsteplog>(pga_jobsteplog);
            return pga_jobsteplog;
        }

        /// <summary>
        /// Adds a sequence of new pga_jobsteplog.
        /// </summary>
        /// <param name="pga_jobsteplog">Sequence of new pga_jobsteplog to add.</param>
        /// <returns>Sequence of new pga_jobsteplog have been added.</returns>
        public IEnumerable<pga_jobsteplog> Add(IEnumerable<pga_jobsteplog> pga_jobsteplog)
        {
            UnitOfWork.Add<pga_jobsteplog>(pga_jobsteplog);
            return pga_jobsteplog;
        }

        /// <summary>
        /// Delete a existed pga_jobsteplog.
        /// </summary>
        /// <param name="pga_jobsteplog">pga_jobsteplog to delete.</param>
        public void Delete(pga_jobsteplog pga_jobsteplog)
        {
            Refresh(pga_jobsteplog);
            if (pga_jobsteplog.EntityState != System.Data.EntityState.Detached)
                UnitOfWork.Delete<pga_jobsteplog>(pga_jobsteplog);
        }

        /// <summary>
        /// Delete a sequence of existed pga_jobsteplog.
        /// </summary>
        /// <param name="pga_jobsteplog">Sequence of existed pga_jobsteplog to delete.</param>
        public void Delete(IEnumerable<pga_jobsteplog> pga_jobsteplog)
        {
            int total = pga_jobsteplog.Count();
            for (int i = total - 1; i >= 0; i--)
                Delete(pga_jobsteplog.ElementAt(i));
        }

        /// <summary>
        /// Returns the first pga_jobsteplog of a sequence that satisfies a specified condition or 
        /// a default value if no such pga_jobsteplog is found.
        /// </summary>
        /// <param name="expression">A function to test each pga_jobsteplog for a condition.</param>
        /// <returns>    
        /// Null if source is empty or if no pga_jobsteplog passes the test specified by expression; 
        /// otherwise, the first pga_jobsteplog in source that passes the test specified by expression.
        /// </returns>
        public pga_jobsteplog Get(Expression<Func<pga_jobsteplog, bool>> expression)
        {
            return UnitOfWork.Get<pga_jobsteplog>(expression);
        }

        /// <summary>
        /// Get all pga_jobsteplog.
        /// </summary>
        /// <returns>The new IList&lt;pga_jobsteplog&gt; instance.</returns>
        public IList<pga_jobsteplog> GetAll()
        {
            return UnitOfWork.GetAll<pga_jobsteplog>().ToList();
        }

        /// <summary>
        /// Get all pga_jobsteplog that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each pga_jobsteplog for a condition.</param>
        /// <returns>The new IList&lt;pga_jobsteplog&gt; instance.</returns>
        public IList<pga_jobsteplog> GetAll(Expression<Func<pga_jobsteplog, bool>> expression)
        {
            return UnitOfWork.GetAll<pga_jobsteplog>(expression).ToList();
        }

        /// <summary>
        /// Get all pga_jobsteplog.
        /// </summary>
        /// <returns>The new IEnumerable&lt;pga_jobsteplog&gt; instance.</returns>
        public IEnumerable<pga_jobsteplog> GetIEnumerable()
        {
            return UnitOfWork.GetIEnumerable<pga_jobsteplog>();
        }

        /// <summary>
        /// Get all pga_jobsteplog that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each pga_jobsteplog for a condition.</param>
        /// <returns>The new IEnumerable&lt;pga_jobsteplog&gt; instance.</returns>
        public IEnumerable<pga_jobsteplog> GetIEnumerable(Expression<Func<pga_jobsteplog, bool>> expression)
        {
            return UnitOfWork.GetIEnumerable<pga_jobsteplog>(expression);
        }

        /// <summary>
        /// Get all pga_jobsteplog.
        /// </summary>
        /// <returns>The new IQueryable&lt;pga_jobsteplog&gt; instance.</returns>
        public IQueryable<pga_jobsteplog> GetIQueryable()
        {
            return UnitOfWork.GetIQueryable<pga_jobsteplog>();
        }

        /// <summary>
        /// Get all pga_jobsteplog that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each pga_jobsteplog for a condition.</param>
        /// <returns>The new IQueryable&lt;pga_jobsteplog&gt; instance.</returns>
        public IQueryable<pga_jobsteplog> GetIQueryable(Expression<Func<pga_jobsteplog, bool>> expression)
        {
            return UnitOfWork.GetIQueryable<pga_jobsteplog>(expression);
        }

        /// <summary>
        /// Take a few pga_jobsteplog in a sequence was sorted on server.
        /// </summary>
        /// <param name="ignoreCount">Number of pga_jobsteplog will ignore.</param>
        /// <param name="takeCount">Number of pga_jobsteplog will take.</param>
        /// <param name="keys">The key columns by which to order the results.</param>
        /// <returns>The new IList&lt;pga_jobsteplog&gt; instance.</returns>
        public IList<pga_jobsteplog> GetRange(int ignoreCount, int takeCount, string keys)
        {
            return UnitOfWork.GetRange<pga_jobsteplog>(ignoreCount, takeCount, keys);
        }

        /// <summary>
        /// Take a few pga_jobsteplog in a sequence was sorted on server.
        /// </summary>
        /// <param name="ignoreCount">Number of pga_jobsteplog will ignore.</param>
        /// <param name="takeCount">Number of pga_jobsteplog will take.</param>
        /// <param name="keys">The key columns by which to order the results.</param>
        /// <param name="expression">A function to test each pga_jobsteplog for a condition.</param>
        /// <returns>The new IList&lt;pga_jobsteplog&gt; instance.</returns>
        public IList<pga_jobsteplog> GetRange(int ignoreCount, int takeCount, string keys, Expression<Func<pga_jobsteplog, bool>> expression)
        {
            return UnitOfWork.GetRange<pga_jobsteplog>(ignoreCount, takeCount, keys, expression);
        }

        /// <summary>
        /// Updates an pga_jobsteplog in the object context with data from the data source.
        /// </summary>
        /// <param name="pga_jobsteplog">The pga_jobsteplog to be refreshed.</param>
        public pga_jobsteplog Refresh(pga_jobsteplog pga_jobsteplog)
        {
            UnitOfWork.Refresh<pga_jobsteplog>(pga_jobsteplog);
            if (pga_jobsteplog.EntityState != System.Data.EntityState.Detached)
                return pga_jobsteplog;
            return null;
        }

        /// <summary>
        /// Updates a sequence of pga_jobsteplog in the object context with data from the data source.
        /// </summary>
        /// <typeparam name="pga_jobsteplog">Type of object in a sequence to refresh.</typeparam>
        /// <param name="pga_jobsteplog">Object collection to be refreshed.</param>
        public void Refresh(IEnumerable<pga_jobsteplog> pga_jobsteplog)
        {
            UnitOfWork.Refresh<pga_jobsteplog>(pga_jobsteplog);
        }

        /// <summary>
        /// Updates a sequence of pga_jobsteplog in the object context with data from the data source.
        /// </summary>
        public void Refresh()
        {
            UnitOfWork.Refresh<pga_jobsteplog>();
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
