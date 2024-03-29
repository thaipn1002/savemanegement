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
    /// Repository for table base_VirtualFolder 
    /// </summary>
    public partial class base_VirtualFolderRepository
    {
        #region Auto Generate Code

        #region Constructors

        // Default constructor
        public base_VirtualFolderRepository()
        {
        }

        #endregion

        #region Basic C.R.U.D. Operations

        /// <summary>
        /// Add new base_VirtualFolder.
        /// </summary>
        /// <param name="base_VirtualFolder">base_VirtualFolder to add.</param>
        /// <returns>base_VirtualFolder have been added.</returns>
        public base_VirtualFolder Add(base_VirtualFolder base_VirtualFolder)
        {
            UnitOfWork.Add<base_VirtualFolder>(base_VirtualFolder);
            return base_VirtualFolder;
        }

        /// <summary>
        /// Adds a sequence of new base_VirtualFolder.
        /// </summary>
        /// <param name="base_VirtualFolder">Sequence of new base_VirtualFolder to add.</param>
        /// <returns>Sequence of new base_VirtualFolder have been added.</returns>
        public IEnumerable<base_VirtualFolder> Add(IEnumerable<base_VirtualFolder> base_VirtualFolder)
        {
            UnitOfWork.Add<base_VirtualFolder>(base_VirtualFolder);
            return base_VirtualFolder;
        }

        /// <summary>
        /// Delete a existed base_VirtualFolder.
        /// </summary>
        /// <param name="base_VirtualFolder">base_VirtualFolder to delete.</param>
        public void Delete(base_VirtualFolder base_VirtualFolder)
        {
            Refresh(base_VirtualFolder);
            if (base_VirtualFolder.EntityState != System.Data.EntityState.Detached)
                UnitOfWork.Delete<base_VirtualFolder>(base_VirtualFolder);
        }

        /// <summary>
        /// Delete a sequence of existed base_VirtualFolder.
        /// </summary>
        /// <param name="base_VirtualFolder">Sequence of existed base_VirtualFolder to delete.</param>
        public void Delete(IEnumerable<base_VirtualFolder> base_VirtualFolder)
        {
            int total = base_VirtualFolder.Count();
            for (int i = total - 1; i >= 0; i--)
                Delete(base_VirtualFolder.ElementAt(i));
        }

        /// <summary>
        /// Returns the first base_VirtualFolder of a sequence that satisfies a specified condition or 
        /// a default value if no such base_VirtualFolder is found.
        /// </summary>
        /// <param name="expression">A function to test each base_VirtualFolder for a condition.</param>
        /// <returns>    
        /// Null if source is empty or if no base_VirtualFolder passes the test specified by expression; 
        /// otherwise, the first base_VirtualFolder in source that passes the test specified by expression.
        /// </returns>
        public base_VirtualFolder Get(Expression<Func<base_VirtualFolder, bool>> expression)
        {
            return UnitOfWork.Get<base_VirtualFolder>(expression);
        }

        /// <summary>
        /// Get all base_VirtualFolder.
        /// </summary>
        /// <returns>The new IList&lt;base_VirtualFolder&gt; instance.</returns>
        public IList<base_VirtualFolder> GetAll()
        {
            return UnitOfWork.GetAll<base_VirtualFolder>().ToList();
        }

        /// <summary>
        /// Get all base_VirtualFolder that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_VirtualFolder for a condition.</param>
        /// <returns>The new IList&lt;base_VirtualFolder&gt; instance.</returns>
        public IList<base_VirtualFolder> GetAll(Expression<Func<base_VirtualFolder, bool>> expression)
        {
            return UnitOfWork.GetAll<base_VirtualFolder>(expression).ToList();
        }

        /// <summary>
        /// Get all base_VirtualFolder.
        /// </summary>
        /// <returns>The new IEnumerable&lt;base_VirtualFolder&gt; instance.</returns>
        public IEnumerable<base_VirtualFolder> GetIEnumerable()
        {
            return UnitOfWork.GetIEnumerable<base_VirtualFolder>();
        }

        /// <summary>
        /// Get all base_VirtualFolder that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_VirtualFolder for a condition.</param>
        /// <returns>The new IEnumerable&lt;base_VirtualFolder&gt; instance.</returns>
        public IEnumerable<base_VirtualFolder> GetIEnumerable(Expression<Func<base_VirtualFolder, bool>> expression)
        {
            return UnitOfWork.GetIEnumerable<base_VirtualFolder>(expression);
        }

        /// <summary>
        /// Get all base_VirtualFolder.
        /// </summary>
        /// <returns>The new IQueryable&lt;base_VirtualFolder&gt; instance.</returns>
        public IQueryable<base_VirtualFolder> GetIQueryable()
        {
            return UnitOfWork.GetIQueryable<base_VirtualFolder>();
        }

        /// <summary>
        /// Get all base_VirtualFolder that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_VirtualFolder for a condition.</param>
        /// <returns>The new IQueryable&lt;base_VirtualFolder&gt; instance.</returns>
        public IQueryable<base_VirtualFolder> GetIQueryable(Expression<Func<base_VirtualFolder, bool>> expression)
        {
            return UnitOfWork.GetIQueryable<base_VirtualFolder>(expression);
        }

        /// <summary>
        /// Take a few base_VirtualFolder in a sequence was sorted on server.
        /// </summary>
        /// <param name="ignoreCount">Number of base_VirtualFolder will ignore.</param>
        /// <param name="takeCount">Number of base_VirtualFolder will take.</param>
        /// <param name="keys">The key columns by which to order the results.</param>
        /// <returns>The new IList&lt;base_VirtualFolder&gt; instance.</returns>
        public IList<base_VirtualFolder> GetRange(int ignoreCount, int takeCount, string keys)
        {
            return UnitOfWork.GetRange<base_VirtualFolder>(ignoreCount, takeCount, keys);
        }

        /// <summary>
        /// Take a few base_VirtualFolder in a sequence was sorted on server.
        /// </summary>
        /// <param name="ignoreCount">Number of base_VirtualFolder will ignore.</param>
        /// <param name="takeCount">Number of base_VirtualFolder will take.</param>
        /// <param name="keys">The key columns by which to order the results.</param>
        /// <param name="expression">A function to test each base_VirtualFolder for a condition.</param>
        /// <returns>The new IList&lt;base_VirtualFolder&gt; instance.</returns>
        public IList<base_VirtualFolder> GetRange(int ignoreCount, int takeCount, string keys, Expression<Func<base_VirtualFolder, bool>> expression)
        {
            return UnitOfWork.GetRange<base_VirtualFolder>(ignoreCount, takeCount, keys, expression);
        }

        /// <summary>
        /// Updates an base_VirtualFolder in the object context with data from the data source.
        /// </summary>
        /// <param name="base_VirtualFolder">The base_VirtualFolder to be refreshed.</param>
        public base_VirtualFolder Refresh(base_VirtualFolder base_VirtualFolder)
        {
            UnitOfWork.Refresh<base_VirtualFolder>(base_VirtualFolder);
            if (base_VirtualFolder.EntityState != System.Data.EntityState.Detached)
                return base_VirtualFolder;
            return null;
        }

        /// <summary>
        /// Updates a sequence of base_VirtualFolder in the object context with data from the data source.
        /// </summary>
        /// <typeparam name="base_VirtualFolder">Type of object in a sequence to refresh.</typeparam>
        /// <param name="base_VirtualFolder">Object collection to be refreshed.</param>
        public void Refresh(IEnumerable<base_VirtualFolder> base_VirtualFolder)
        {
            UnitOfWork.Refresh<base_VirtualFolder>(base_VirtualFolder);
        }

        /// <summary>
        /// Updates a sequence of base_VirtualFolder in the object context with data from the data source.
        /// </summary>
        public void Refresh()
        {
            UnitOfWork.Refresh<base_VirtualFolder>();
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
