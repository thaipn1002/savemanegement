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
    /// Repository for table base_ResourceNote 
    /// </summary>
    public partial class base_ResourceNoteRepository
    {
        #region Auto Generate Code

        #region Constructors

        // Default constructor
        public base_ResourceNoteRepository()
        {
        }

        #endregion

        #region Basic C.R.U.D. Operations

        /// <summary>
        /// Add new base_ResourceNote.
        /// </summary>
        /// <param name="base_ResourceNote">base_ResourceNote to add.</param>
        /// <returns>base_ResourceNote have been added.</returns>
        public base_ResourceNote Add(base_ResourceNote base_ResourceNote)
        {
            UnitOfWork.Add<base_ResourceNote>(base_ResourceNote);
            return base_ResourceNote;
        }

        /// <summary>
        /// Adds a sequence of new base_ResourceNote.
        /// </summary>
        /// <param name="base_ResourceNote">Sequence of new base_ResourceNote to add.</param>
        /// <returns>Sequence of new base_ResourceNote have been added.</returns>
        public IEnumerable<base_ResourceNote> Add(IEnumerable<base_ResourceNote> base_ResourceNote)
        {
            UnitOfWork.Add<base_ResourceNote>(base_ResourceNote);
            return base_ResourceNote;
        }

        /// <summary>
        /// Delete a existed base_ResourceNote.
        /// </summary>
        /// <param name="base_ResourceNote">base_ResourceNote to delete.</param>
        public void Delete(base_ResourceNote base_ResourceNote)
        {
            Refresh(base_ResourceNote);
            if (base_ResourceNote.EntityState != System.Data.EntityState.Detached)
                UnitOfWork.Delete<base_ResourceNote>(base_ResourceNote);
        }

        /// <summary>
        /// Delete a sequence of existed base_ResourceNote.
        /// </summary>
        /// <param name="base_ResourceNote">Sequence of existed base_ResourceNote to delete.</param>
        public void Delete(IEnumerable<base_ResourceNote> base_ResourceNote)
        {
            int total = base_ResourceNote.Count();
            for (int i = total - 1; i >= 0; i--)
                Delete(base_ResourceNote.ElementAt(i));
        }

        /// <summary>
        /// Returns the first base_ResourceNote of a sequence that satisfies a specified condition or 
        /// a default value if no such base_ResourceNote is found.
        /// </summary>
        /// <param name="expression">A function to test each base_ResourceNote for a condition.</param>
        /// <returns>    
        /// Null if source is empty or if no base_ResourceNote passes the test specified by expression; 
        /// otherwise, the first base_ResourceNote in source that passes the test specified by expression.
        /// </returns>
        public base_ResourceNote Get(Expression<Func<base_ResourceNote, bool>> expression)
        {
            return UnitOfWork.Get<base_ResourceNote>(expression);
        }

        /// <summary>
        /// Get all base_ResourceNote.
        /// </summary>
        /// <returns>The new IList&lt;base_ResourceNote&gt; instance.</returns>
        public IList<base_ResourceNote> GetAll()
        {
            return UnitOfWork.GetAll<base_ResourceNote>();
        }

        /// <summary>
        /// Get all base_ResourceNote that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_ResourceNote for a condition.</param>
        /// <returns>The new IList&lt;base_ResourceNote&gt; instance.</returns>
        public IList<base_ResourceNote> GetAll(Expression<Func<base_ResourceNote, bool>> expression)
        {
            return UnitOfWork.GetAll<base_ResourceNote>(expression);
        }

        /// <summary>
        /// Get all base_ResourceNote.
        /// </summary>
        /// <returns>The new IEnumerable&lt;base_ResourceNote&gt; instance.</returns>
        public IEnumerable<base_ResourceNote> GetIEnumerable()
        {
            return UnitOfWork.GetIEnumerable<base_ResourceNote>();
        }

        /// <summary>
        /// Get all base_ResourceNote that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_ResourceNote for a condition.</param>
        /// <returns>The new IEnumerable&lt;base_ResourceNote&gt; instance.</returns>
        public IEnumerable<base_ResourceNote> GetIEnumerable(Expression<Func<base_ResourceNote, bool>> expression)
        {
            return UnitOfWork.GetIEnumerable<base_ResourceNote>(expression);
        }

        /// <summary>
        /// Get all base_ResourceNote.
        /// </summary>
        /// <returns>The new IQueryable&lt;base_ResourceNote&gt; instance.</returns>
        public IQueryable<base_ResourceNote> GetIQueryable()
        {
            return UnitOfWork.GetIQueryable<base_ResourceNote>();
        }

        /// <summary>
        /// Get all base_ResourceNote that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_ResourceNote for a condition.</param>
        /// <returns>The new IQueryable&lt;base_ResourceNote&gt; instance.</returns>
        public IQueryable<base_ResourceNote> GetIQueryable(Expression<Func<base_ResourceNote, bool>> expression)
        {
            return UnitOfWork.GetIQueryable<base_ResourceNote>(expression);
        }

        /// <summary>
        /// Take a few base_ResourceNote in a sequence was sorted on server.
        /// </summary>
        /// <param name="ignoreCount">Number of base_ResourceNote will ignore.</param>
        /// <param name="takeCount">Number of base_ResourceNote will take.</param>
        /// <param name="keys">The key columns by which to order the results.</param>
        /// <returns>The new IList&lt;base_ResourceNote&gt; instance.</returns>
        public IList<base_ResourceNote> GetRange(int ignoreCount, int takeCount, string keys)
        {
            return UnitOfWork.GetRange<base_ResourceNote>(ignoreCount, takeCount, keys);
        }

        /// <summary>
        /// Take a few base_ResourceNote in a sequence was sorted on server.
        /// </summary>
        /// <param name="ignoreCount">Number of base_ResourceNote will ignore.</param>
        /// <param name="takeCount">Number of base_ResourceNote will take.</param>
        /// <param name="keys">The key columns by which to order the results.</param>
        /// <param name="expression">A function to test each base_ResourceNote for a condition.</param>
        /// <returns>The new IList&lt;base_ResourceNote&gt; instance.</returns>
        public IList<base_ResourceNote> GetRange(int ignoreCount, int takeCount, string keys, Expression<Func<base_ResourceNote, bool>> expression)
        {
            return UnitOfWork.GetRange<base_ResourceNote>(ignoreCount, takeCount, keys, expression);
        }

        /// <summary>
        /// Take a few base_ResourceNote in sequence was sorted by descending on server.
        /// </summary>
        /// <typeparam name="TKey">Type of base_ResourceNote to sort</typeparam>
        /// <param name="ignoreCount">Number of base_ResourceNote will ignore.</param>
        /// <param name="takeCount">Number of base_ResourceNote will take.</param>
        /// <param name="keySelector">The key columns by which to order the results.</param>
        /// <returns>The new IList&lt;base_ResourceNote&gt; instance.</returns>
        public IList<base_ResourceNote> GetRangeDescending<TKey>(int ignoreCount, int takeCount, Expression<Func<base_ResourceNote, TKey>> keySelector)
        {
            return UnitOfWork.GetRangeDescending(ignoreCount, takeCount, keySelector);
        }

        /// <summary>
        /// Take a few base_ResourceNote in sequence was sorted by descending on server.
        /// </summary>
        /// <typeparam name="TKey">Type of base_ResourceNote to sort</typeparam>
        /// <param name="ignoreCount">Number of base_ResourceNote will ignore.</param>
        /// <param name="takeCount">Number of base_ResourceNote will take.</param>
        /// <param name="keySelector">The key columns by which to order the results.</param>
        /// <param name="expression">A function to test each object for a condition.</param>
        /// <returns>The new IList&lt;base_ResourceNote&gt; instance.</returns>
        public IList<base_ResourceNote> GetRangeDescending<TKey>(int ignoreCount, int takeCount, Expression<Func<base_ResourceNote, TKey>> keySelector, Expression<Func<base_ResourceNote, bool>> expression)
        {
            return UnitOfWork.GetRangeDescending(ignoreCount, takeCount, keySelector, expression);
        }

        /// <summary>
        /// Updates an base_ResourceNote in the object context with data from the data source.
        /// </summary>
        /// <param name="base_ResourceNote">The base_ResourceNote to be refreshed.</param>
        public base_ResourceNote Refresh(base_ResourceNote base_ResourceNote)
        {
            UnitOfWork.Refresh<base_ResourceNote>(base_ResourceNote);
            if (base_ResourceNote.EntityState != System.Data.EntityState.Detached)
                return base_ResourceNote;
            return null;
        }

        /// <summary>
        /// Updates a sequence of base_ResourceNote in the object context with data from the data source.
        /// </summary>
        /// <typeparam name="base_ResourceNote">Type of object in a sequence to refresh.</typeparam>
        /// <param name="base_ResourceNote">Object collection to be refreshed.</param>
        public void Refresh(IEnumerable<base_ResourceNote> base_ResourceNote)
        {
            UnitOfWork.Refresh<base_ResourceNote>(base_ResourceNote);
        }

        /// <summary>
        /// Updates a sequence of base_ResourceNote in the object context with data from the data source.
        /// </summary>
        public void Refresh()
        {
            UnitOfWork.Refresh<base_ResourceNote>();
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