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
    /// Repository for table rpt_Department 
    /// </summary>
    public partial class rpt_DepartmentRepository
    {
        #region Auto Generate Code

        #region Constructors

        // Default constructor
        public rpt_DepartmentRepository()
        {
        }

        #endregion

        #region Basic C.R.U.D. Operations

        /// <summary>
        /// Add new rpt_Department.
        /// </summary>
        /// <param name="rpt_Department">rpt_Department to add.</param>
        /// <returns>rpt_Department have been added.</returns>
        public rpt_Department Add(rpt_Department rpt_Department)
        {
            UnitOfWork.Add<rpt_Department>(rpt_Department);
            return rpt_Department;
        }

        /// <summary>
        /// Adds a sequence of new rpt_Department.
        /// </summary>
        /// <param name="rpt_Department">Sequence of new rpt_Department to add.</param>
        /// <returns>Sequence of new rpt_Department have been added.</returns>
        public IEnumerable<rpt_Department> Add(IEnumerable<rpt_Department> rpt_Department)
        {
            UnitOfWork.Add<rpt_Department>(rpt_Department);
            return rpt_Department;
        }

        /// <summary>
        /// Delete a existed rpt_Department.
        /// </summary>
        /// <param name="rpt_Department">rpt_Department to delete.</param>
        public void Delete(rpt_Department rpt_Department)
        {
            Refresh(rpt_Department);
            if (rpt_Department.EntityState != System.Data.EntityState.Detached)
                UnitOfWork.Delete<rpt_Department>(rpt_Department);
        }

        /// <summary>
        /// Delete a sequence of existed rpt_Department.
        /// </summary>
        /// <param name="rpt_Department">Sequence of existed rpt_Department to delete.</param>
        public void Delete(IEnumerable<rpt_Department> rpt_Department)
        {
            int total = rpt_Department.Count();
            for (int i = total - 1; i >= 0; i--)
                Delete(rpt_Department.ElementAt(i));
        }

        /// <summary>
        /// Returns the first rpt_Department of a sequence that satisfies a specified condition or 
        /// a default value if no such rpt_Department is found.
        /// </summary>
        /// <param name="expression">A function to test each rpt_Department for a condition.</param>
        /// <returns>    
        /// Null if source is empty or if no rpt_Department passes the test specified by expression; 
        /// otherwise, the first rpt_Department in source that passes the test specified by expression.
        /// </returns>
        public rpt_Department Get(Expression<Func<rpt_Department, bool>> expression)
        {
            return UnitOfWork.Get<rpt_Department>(expression);
        }

        /// <summary>
        /// Get all rpt_Department.
        /// </summary>
        /// <returns>The new IList&lt;rpt_Department&gt; instance.</returns>
        public IList<rpt_Department> GetAll()
        {
            return UnitOfWork.GetAll<rpt_Department>();
        }

        /// <summary>
        /// Get all rpt_Department that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each rpt_Department for a condition.</param>
        /// <returns>The new IList&lt;rpt_Department&gt; instance.</returns>
        public IList<rpt_Department> GetAll(Expression<Func<rpt_Department, bool>> expression)
        {
            return UnitOfWork.GetAll<rpt_Department>(expression);
        }

        /// <summary>
        /// Get all rpt_Department.
        /// </summary>
        /// <returns>The new IEnumerable&lt;rpt_Department&gt; instance.</returns>
        public IEnumerable<rpt_Department> GetIEnumerable()
        {
            return UnitOfWork.GetIEnumerable<rpt_Department>();
        }

        /// <summary>
        /// Get all rpt_Department that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each rpt_Department for a condition.</param>
        /// <returns>The new IEnumerable&lt;rpt_Department&gt; instance.</returns>
        public IEnumerable<rpt_Department> GetIEnumerable(Expression<Func<rpt_Department, bool>> expression)
        {
            return UnitOfWork.GetIEnumerable<rpt_Department>(expression);
        }

        /// <summary>
        /// Get all rpt_Department.
        /// </summary>
        /// <returns>The new IQueryable&lt;rpt_Department&gt; instance.</returns>
        public IQueryable<rpt_Department> GetIQueryable()
        {
            return UnitOfWork.GetIQueryable<rpt_Department>();
        }

        /// <summary>
        /// Get all rpt_Department that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each rpt_Department for a condition.</param>
        /// <returns>The new IQueryable&lt;rpt_Department&gt; instance.</returns>
        public IQueryable<rpt_Department> GetIQueryable(Expression<Func<rpt_Department, bool>> expression)
        {
            return UnitOfWork.GetIQueryable<rpt_Department>(expression);
        }

        /// <summary>
        /// Take a few rpt_Department in a sequence was sorted on server.
        /// </summary>
        /// <param name="ignoreCount">Number of rpt_Department will ignore.</param>
        /// <param name="takeCount">Number of rpt_Department will take.</param>
        /// <param name="keys">The key columns by which to order the results.</param>
        /// <returns>The new IList&lt;rpt_Department&gt; instance.</returns>
        public IList<rpt_Department> GetRange(int ignoreCount, int takeCount, string keys)
        {
            return UnitOfWork.GetRange<rpt_Department>(ignoreCount, takeCount, keys);
        }

        /// <summary>
        /// Take a few rpt_Department in a sequence was sorted on server.
        /// </summary>
        /// <param name="ignoreCount">Number of rpt_Department will ignore.</param>
        /// <param name="takeCount">Number of rpt_Department will take.</param>
        /// <param name="keys">The key columns by which to order the results.</param>
        /// <param name="expression">A function to test each rpt_Department for a condition.</param>
        /// <returns>The new IList&lt;rpt_Department&gt; instance.</returns>
        public IList<rpt_Department> GetRange(int ignoreCount, int takeCount, string keys, Expression<Func<rpt_Department, bool>> expression)
        {
            return UnitOfWork.GetRange<rpt_Department>(ignoreCount, takeCount, keys, expression);
        }

        /// <summary>
        /// Take a few rpt_Department in sequence was sorted by descending on server.
        /// </summary>
        /// <typeparam name="TKey">Type of rpt_Department to sort</typeparam>
        /// <param name="ignoreCount">Number of rpt_Department will ignore.</param>
        /// <param name="takeCount">Number of rpt_Department will take.</param>
        /// <param name="keySelector">The key columns by which to order the results.</param>
        /// <returns>The new IList&lt;rpt_Department&gt; instance.</returns>
        public IList<rpt_Department> GetRangeDescending<TKey>(int ignoreCount, int takeCount, Expression<Func<rpt_Department, TKey>> keySelector)
        {
            return UnitOfWork.GetRangeDescending(ignoreCount, takeCount, keySelector);
        }

        /// <summary>
        /// Take a few rpt_Department in sequence was sorted by descending on server.
        /// </summary>
        /// <typeparam name="TKey">Type of rpt_Department to sort</typeparam>
        /// <param name="ignoreCount">Number of rpt_Department will ignore.</param>
        /// <param name="takeCount">Number of rpt_Department will take.</param>
        /// <param name="keySelector">The key columns by which to order the results.</param>
        /// <param name="expression">A function to test each object for a condition.</param>
        /// <returns>The new IList&lt;rpt_Department&gt; instance.</returns>
        public IList<rpt_Department> GetRangeDescending<TKey>(int ignoreCount, int takeCount, Expression<Func<rpt_Department, TKey>> keySelector, Expression<Func<rpt_Department, bool>> expression)
        {
            return UnitOfWork.GetRangeDescending(ignoreCount, takeCount, keySelector, expression);
        }

        /// <summary>
        /// Updates an rpt_Department in the object context with data from the data source.
        /// </summary>
        /// <param name="rpt_Department">The rpt_Department to be refreshed.</param>
        public rpt_Department Refresh(rpt_Department rpt_Department)
        {
            UnitOfWork.Refresh<rpt_Department>(rpt_Department);
            if (rpt_Department.EntityState != System.Data.EntityState.Detached)
                return rpt_Department;
            return null;
        }

        /// <summary>
        /// Updates a sequence of rpt_Department in the object context with data from the data source.
        /// </summary>
        /// <typeparam name="rpt_Department">Type of object in a sequence to refresh.</typeparam>
        /// <param name="rpt_Department">Object collection to be refreshed.</param>
        public void Refresh(IEnumerable<rpt_Department> rpt_Department)
        {
            UnitOfWork.Refresh<rpt_Department>(rpt_Department);
        }

        /// <summary>
        /// Updates a sequence of rpt_Department in the object context with data from the data source.
        /// </summary>
        public void Refresh()
        {
            UnitOfWork.Refresh<rpt_Department>();
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
