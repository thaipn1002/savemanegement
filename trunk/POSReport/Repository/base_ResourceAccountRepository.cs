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
    /// Repository for table base_ResourceAccount 
    /// </summary>
    public partial class base_ResourceAccountRepository
    {
        #region Auto Generate Code

        #region Constructors

        // Default constructor
        public base_ResourceAccountRepository()
        {
        }

        #endregion

        #region Basic C.R.U.D. Operations

        /// <summary>
        /// Add new base_ResourceAccount.
        /// </summary>
        /// <param name="base_ResourceAccount">base_ResourceAccount to add.</param>
        /// <returns>base_ResourceAccount have been added.</returns>
        public base_ResourceAccount Add(base_ResourceAccount base_ResourceAccount)
        {
            UnitOfWork.Add<base_ResourceAccount>(base_ResourceAccount);
            return base_ResourceAccount;
        }

        /// <summary>
        /// Adds a sequence of new base_ResourceAccount.
        /// </summary>
        /// <param name="base_ResourceAccount">Sequence of new base_ResourceAccount to add.</param>
        /// <returns>Sequence of new base_ResourceAccount have been added.</returns>
        public IEnumerable<base_ResourceAccount> Add(IEnumerable<base_ResourceAccount> base_ResourceAccount)
        {
            UnitOfWork.Add<base_ResourceAccount>(base_ResourceAccount);
            return base_ResourceAccount;
        }

        /// <summary>
        /// Delete a existed base_ResourceAccount.
        /// </summary>
        /// <param name="base_ResourceAccount">base_ResourceAccount to delete.</param>
        public void Delete(base_ResourceAccount base_ResourceAccount)
        {
            Refresh(base_ResourceAccount);
            if (base_ResourceAccount.EntityState != System.Data.EntityState.Detached)
                UnitOfWork.Delete<base_ResourceAccount>(base_ResourceAccount);
        }

        /// <summary>
        /// Delete a sequence of existed base_ResourceAccount.
        /// </summary>
        /// <param name="base_ResourceAccount">Sequence of existed base_ResourceAccount to delete.</param>
        public void Delete(IEnumerable<base_ResourceAccount> base_ResourceAccount)
        {
            int total = base_ResourceAccount.Count();
            for (int i = total - 1; i >= 0; i--)
                Delete(base_ResourceAccount.ElementAt(i));
        }

        /// <summary>
        /// Returns the first base_ResourceAccount of a sequence that satisfies a specified condition or 
        /// a default value if no such base_ResourceAccount is found.
        /// </summary>
        /// <param name="expression">A function to test each base_ResourceAccount for a condition.</param>
        /// <returns>    
        /// Null if source is empty or if no base_ResourceAccount passes the test specified by expression; 
        /// otherwise, the first base_ResourceAccount in source that passes the test specified by expression.
        /// </returns>
        public base_ResourceAccount Get(Expression<Func<base_ResourceAccount, bool>> expression)
        {
            return UnitOfWork.Get<base_ResourceAccount>(expression);
        }

        /// <summary>
        /// Get all base_ResourceAccount.
        /// </summary>
        /// <returns>The new IList&lt;base_ResourceAccount&gt; instance.</returns>
        public IList<base_ResourceAccount> GetAll()
        {
            return UnitOfWork.GetAll<base_ResourceAccount>().ToList();
        }

        /// <summary>
        /// Get all base_ResourceAccount that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_ResourceAccount for a condition.</param>
        /// <returns>The new IList&lt;base_ResourceAccount&gt; instance.</returns>
        public IList<base_ResourceAccount> GetAll(Expression<Func<base_ResourceAccount, bool>> expression)
        {
            return UnitOfWork.GetAll<base_ResourceAccount>(expression).ToList();
        }

        /// <summary>
        /// Get all base_ResourceAccount.
        /// </summary>
        /// <returns>The new IEnumerable&lt;base_ResourceAccount&gt; instance.</returns>
        public IEnumerable<base_ResourceAccount> GetIEnumerable()
        {
            return UnitOfWork.GetIEnumerable<base_ResourceAccount>();
        }

        /// <summary>
        /// Get all base_ResourceAccount that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_ResourceAccount for a condition.</param>
        /// <returns>The new IEnumerable&lt;base_ResourceAccount&gt; instance.</returns>
        public IEnumerable<base_ResourceAccount> GetIEnumerable(Expression<Func<base_ResourceAccount, bool>> expression)
        {
            return UnitOfWork.GetIEnumerable<base_ResourceAccount>(expression);
        }

        /// <summary>
        /// Get all base_ResourceAccount.
        /// </summary>
        /// <returns>The new IQueryable&lt;base_ResourceAccount&gt; instance.</returns>
        public IQueryable<base_ResourceAccount> GetIQueryable()
        {
            return UnitOfWork.GetIQueryable<base_ResourceAccount>();
        }

        /// <summary>
        /// Get all base_ResourceAccount that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_ResourceAccount for a condition.</param>
        /// <returns>The new IQueryable&lt;base_ResourceAccount&gt; instance.</returns>
        public IQueryable<base_ResourceAccount> GetIQueryable(Expression<Func<base_ResourceAccount, bool>> expression)
        {
            return UnitOfWork.GetIQueryable<base_ResourceAccount>(expression);
        }

        /// <summary>
        /// Take a few base_ResourceAccount in a sequence was sorted on server.
        /// </summary>
        /// <param name="ignoreCount">Number of base_ResourceAccount will ignore.</param>
        /// <param name="takeCount">Number of base_ResourceAccount will take.</param>
        /// <param name="keys">The key columns by which to order the results.</param>
        /// <returns>The new IList&lt;base_ResourceAccount&gt; instance.</returns>
        public IList<base_ResourceAccount> GetRange(int ignoreCount, int takeCount, string keys)
        {
            return UnitOfWork.GetRange<base_ResourceAccount>(ignoreCount, takeCount, keys);
        }

        /// <summary>
        /// Take a few base_ResourceAccount in a sequence was sorted on server.
        /// </summary>
        /// <param name="ignoreCount">Number of base_ResourceAccount will ignore.</param>
        /// <param name="takeCount">Number of base_ResourceAccount will take.</param>
        /// <param name="keys">The key columns by which to order the results.</param>
        /// <param name="expression">A function to test each base_ResourceAccount for a condition.</param>
        /// <returns>The new IList&lt;base_ResourceAccount&gt; instance.</returns>
        public IList<base_ResourceAccount> GetRange(int ignoreCount, int takeCount, string keys, Expression<Func<base_ResourceAccount, bool>> expression)
        {
            return UnitOfWork.GetRange<base_ResourceAccount>(ignoreCount, takeCount, keys, expression);
        }

        /// <summary>
        /// Updates an base_ResourceAccount in the object context with data from the data source.
        /// </summary>
        /// <param name="base_ResourceAccount">The base_ResourceAccount to be refreshed.</param>
        public base_ResourceAccount Refresh(base_ResourceAccount base_ResourceAccount)
        {
            UnitOfWork.Refresh<base_ResourceAccount>(base_ResourceAccount);
            if (base_ResourceAccount.EntityState != System.Data.EntityState.Detached)
                return base_ResourceAccount;
            return null;
        }

        /// <summary>
        /// Updates a sequence of base_ResourceAccount in the object context with data from the data source.
        /// </summary>
        /// <typeparam name="base_ResourceAccount">Type of object in a sequence to refresh.</typeparam>
        /// <param name="base_ResourceAccount">Object collection to be refreshed.</param>
        public void Refresh(IEnumerable<base_ResourceAccount> base_ResourceAccount)
        {
            UnitOfWork.Refresh<base_ResourceAccount>(base_ResourceAccount);
        }

        /// <summary>
        /// Updates a sequence of base_ResourceAccount in the object context with data from the data source.
        /// </summary>
        public void Refresh()
        {
            UnitOfWork.Refresh<base_ResourceAccount>();
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
