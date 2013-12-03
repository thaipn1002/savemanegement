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
    /// Repository for table base_ResourceReturn 
    /// </summary>
    public partial class base_ResourceReturnRepository
    {
        #region Auto Generate Code

        #region Constructors

        // Default constructor
        public base_ResourceReturnRepository()
        {
        }

        #endregion

        #region Basic C.R.U.D. Operations

        /// <summary>
        /// Add new base_ResourceReturn.
        /// </summary>
        /// <param name="base_ResourceReturn">base_ResourceReturn to add.</param>
        /// <returns>base_ResourceReturn have been added.</returns>
        public base_ResourceReturn Add(base_ResourceReturn base_ResourceReturn)
        {
            UnitOfWork.Add<base_ResourceReturn>(base_ResourceReturn);
            return base_ResourceReturn;
        }

        /// <summary>
        /// Adds a sequence of new base_ResourceReturn.
        /// </summary>
        /// <param name="base_ResourceReturn">Sequence of new base_ResourceReturn to add.</param>
        /// <returns>Sequence of new base_ResourceReturn have been added.</returns>
        public IEnumerable<base_ResourceReturn> Add(IEnumerable<base_ResourceReturn> base_ResourceReturn)
        {
            UnitOfWork.Add<base_ResourceReturn>(base_ResourceReturn);
            return base_ResourceReturn;
        }

        /// <summary>
        /// Delete a existed base_ResourceReturn.
        /// </summary>
        /// <param name="base_ResourceReturn">base_ResourceReturn to delete.</param>
        public void Delete(base_ResourceReturn base_ResourceReturn)
        {
            Refresh(base_ResourceReturn);
            if (base_ResourceReturn.EntityState != System.Data.EntityState.Detached)
                UnitOfWork.Delete<base_ResourceReturn>(base_ResourceReturn);
        }

        /// <summary>
        /// Delete a sequence of existed base_ResourceReturn.
        /// </summary>
        /// <param name="base_ResourceReturn">Sequence of existed base_ResourceReturn to delete.</param>
        public void Delete(IEnumerable<base_ResourceReturn> base_ResourceReturn)
        {
            int total = base_ResourceReturn.Count();
            for (int i = total - 1; i >= 0; i--)
                Delete(base_ResourceReturn.ElementAt(i));
        }

        /// <summary>
        /// Returns the first base_ResourceReturn of a sequence that satisfies a specified condition or 
        /// a default value if no such base_ResourceReturn is found.
        /// </summary>
        /// <param name="expression">A function to test each base_ResourceReturn for a condition.</param>
        /// <returns>    
        /// Null if source is empty or if no base_ResourceReturn passes the test specified by expression; 
        /// otherwise, the first base_ResourceReturn in source that passes the test specified by expression.
        /// </returns>
        public base_ResourceReturn Get(Expression<Func<base_ResourceReturn, bool>> expression)
        {
            return UnitOfWork.Get<base_ResourceReturn>(expression);
        }

        /// <summary>
        /// Get all base_ResourceReturn.
        /// </summary>
        /// <returns>The new IList&lt;base_ResourceReturn&gt; instance.</returns>
        public IList<base_ResourceReturn> GetAll()
        {
            return UnitOfWork.GetAll<base_ResourceReturn>();
        }

        /// <summary>
        /// Get all base_ResourceReturn that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_ResourceReturn for a condition.</param>
        /// <returns>The new IList&lt;base_ResourceReturn&gt; instance.</returns>
        public IList<base_ResourceReturn> GetAll(Expression<Func<base_ResourceReturn, bool>> expression)
        {
            return UnitOfWork.GetAll<base_ResourceReturn>(expression);
        }

        /// <summary>
        /// Get all base_ResourceReturn.
        /// </summary>
        /// <returns>The new IEnumerable&lt;base_ResourceReturn&gt; instance.</returns>
        public IEnumerable<base_ResourceReturn> GetIEnumerable()
        {
            return UnitOfWork.GetIEnumerable<base_ResourceReturn>();
        }

        /// <summary>
        /// Get all base_ResourceReturn that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_ResourceReturn for a condition.</param>
        /// <returns>The new IEnumerable&lt;base_ResourceReturn&gt; instance.</returns>
        public IEnumerable<base_ResourceReturn> GetIEnumerable(Expression<Func<base_ResourceReturn, bool>> expression)
        {
            return UnitOfWork.GetIEnumerable<base_ResourceReturn>(expression);
        }

        /// <summary>
        /// Get all base_ResourceReturn.
        /// </summary>
        /// <returns>The new IQueryable&lt;base_ResourceReturn&gt; instance.</returns>
        public IQueryable<base_ResourceReturn> GetIQueryable()
        {
            return UnitOfWork.GetIQueryable<base_ResourceReturn>();
        }

        /// <summary>
        /// Get all base_ResourceReturn that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_ResourceReturn for a condition.</param>
        /// <returns>The new IQueryable&lt;base_ResourceReturn&gt; instance.</returns>
        public IQueryable<base_ResourceReturn> GetIQueryable(Expression<Func<base_ResourceReturn, bool>> expression)
        {
            return UnitOfWork.GetIQueryable<base_ResourceReturn>(expression);
        }

        /// <summary>
        /// Take a few base_ResourceReturn in a sequence was sorted on server.
        /// </summary>
        /// <param name="ignoreCount">Number of base_ResourceReturn will ignore.</param>
        /// <param name="takeCount">Number of base_ResourceReturn will take.</param>
        /// <param name="keys">The key columns by which to order the results.</param>
        /// <returns>The new IList&lt;base_ResourceReturn&gt; instance.</returns>
        public IList<base_ResourceReturn> GetRange(int ignoreCount, int takeCount, string keys)
        {
            return UnitOfWork.GetRange<base_ResourceReturn>(ignoreCount, takeCount, keys);
        }

        /// <summary>
        /// Take a few base_ResourceReturn in a sequence was sorted on server.
        /// </summary>
        /// <param name="ignoreCount">Number of base_ResourceReturn will ignore.</param>
        /// <param name="takeCount">Number of base_ResourceReturn will take.</param>
        /// <param name="keys">The key columns by which to order the results.</param>
        /// <param name="expression">A function to test each base_ResourceReturn for a condition.</param>
        /// <returns>The new IList&lt;base_ResourceReturn&gt; instance.</returns>
        public IList<base_ResourceReturn> GetRange(int ignoreCount, int takeCount, string keys, Expression<Func<base_ResourceReturn, bool>> expression)
        {
            return UnitOfWork.GetRange<base_ResourceReturn>(ignoreCount, takeCount, keys, expression);
        }

        /// <summary>
        /// Take a few base_ResourceReturn in sequence was sorted by descending on server.
        /// </summary>
        /// <typeparam name="TKey">Type of base_ResourceReturn to sort</typeparam>
        /// <param name="ignoreCount">Number of base_ResourceReturn will ignore.</param>
        /// <param name="takeCount">Number of base_ResourceReturn will take.</param>
        /// <param name="keySelector">The key columns by which to order the results.</param>
        /// <returns>The new IList&lt;base_ResourceReturn&gt; instance.</returns>
        public IList<base_ResourceReturn> GetRangeDescending<TKey>(int ignoreCount, int takeCount, Expression<Func<base_ResourceReturn, TKey>> keySelector)
        {
            return UnitOfWork.GetRangeDescending(ignoreCount, takeCount, keySelector);
        }

        /// <summary>
        /// Take a few base_ResourceReturn in sequence was sorted by descending on server.
        /// </summary>
        /// <typeparam name="TKey">Type of base_ResourceReturn to sort</typeparam>
        /// <param name="ignoreCount">Number of base_ResourceReturn will ignore.</param>
        /// <param name="takeCount">Number of base_ResourceReturn will take.</param>
        /// <param name="keySelector">The key columns by which to order the results.</param>
        /// <param name="expression">A function to test each object for a condition.</param>
        /// <returns>The new IList&lt;base_ResourceReturn&gt; instance.</returns>
        public IList<base_ResourceReturn> GetRangeDescending<TKey>(int ignoreCount, int takeCount, Expression<Func<base_ResourceReturn, TKey>> keySelector, Expression<Func<base_ResourceReturn, bool>> expression)
        {
            return UnitOfWork.GetRangeDescending(ignoreCount, takeCount, keySelector, expression);
        }

        /// <summary>
        /// Updates an base_ResourceReturn in the object context with data from the data source.
        /// </summary>
        /// <param name="base_ResourceReturn">The base_ResourceReturn to be refreshed.</param>
        public base_ResourceReturn Refresh(base_ResourceReturn base_ResourceReturn)
        {
            UnitOfWork.Refresh<base_ResourceReturn>(base_ResourceReturn);
            if (base_ResourceReturn.EntityState != System.Data.EntityState.Detached)
                return base_ResourceReturn;
            return null;
        }

        /// <summary>
        /// Updates a sequence of base_ResourceReturn in the object context with data from the data source.
        /// </summary>
        /// <typeparam name="base_ResourceReturn">Type of object in a sequence to refresh.</typeparam>
        /// <param name="base_ResourceReturn">Object collection to be refreshed.</param>
        public void Refresh(IEnumerable<base_ResourceReturn> base_ResourceReturn)
        {
            UnitOfWork.Refresh<base_ResourceReturn>(base_ResourceReturn);
        }

        /// <summary>
        /// Updates a sequence of base_ResourceReturn in the object context with data from the data source.
        /// </summary>
        public void Refresh()
        {
            UnitOfWork.Refresh<base_ResourceReturn>();
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
