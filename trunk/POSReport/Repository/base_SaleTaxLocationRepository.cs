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
    /// Repository for table base_SaleTaxLocation 
    /// </summary>
    public partial class base_SaleTaxLocationRepository
    {
        #region Auto Generate Code

        #region Constructors

        // Default constructor
        public base_SaleTaxLocationRepository()
        {
        }

        #endregion

        #region Basic C.R.U.D. Operations

        /// <summary>
        /// Add new base_SaleTaxLocation.
        /// </summary>
        /// <param name="base_SaleTaxLocation">base_SaleTaxLocation to add.</param>
        /// <returns>base_SaleTaxLocation have been added.</returns>
        public base_SaleTaxLocation Add(base_SaleTaxLocation base_SaleTaxLocation)
        {
            UnitOfWork.Add<base_SaleTaxLocation>(base_SaleTaxLocation);
            return base_SaleTaxLocation;
        }

        /// <summary>
        /// Adds a sequence of new base_SaleTaxLocation.
        /// </summary>
        /// <param name="base_SaleTaxLocation">Sequence of new base_SaleTaxLocation to add.</param>
        /// <returns>Sequence of new base_SaleTaxLocation have been added.</returns>
        public IEnumerable<base_SaleTaxLocation> Add(IEnumerable<base_SaleTaxLocation> base_SaleTaxLocation)
        {
            UnitOfWork.Add<base_SaleTaxLocation>(base_SaleTaxLocation);
            return base_SaleTaxLocation;
        }

        /// <summary>
        /// Delete a existed base_SaleTaxLocation.
        /// </summary>
        /// <param name="base_SaleTaxLocation">base_SaleTaxLocation to delete.</param>
        public void Delete(base_SaleTaxLocation base_SaleTaxLocation)
        {
            Refresh(base_SaleTaxLocation);
            if (base_SaleTaxLocation.EntityState != System.Data.EntityState.Detached)
                UnitOfWork.Delete<base_SaleTaxLocation>(base_SaleTaxLocation);
        }

        /// <summary>
        /// Delete a sequence of existed base_SaleTaxLocation.
        /// </summary>
        /// <param name="base_SaleTaxLocation">Sequence of existed base_SaleTaxLocation to delete.</param>
        public void Delete(IEnumerable<base_SaleTaxLocation> base_SaleTaxLocation)
        {
            int total = base_SaleTaxLocation.Count();
            for (int i = total - 1; i >= 0; i--)
                Delete(base_SaleTaxLocation.ElementAt(i));
        }

        /// <summary>
        /// Returns the first base_SaleTaxLocation of a sequence that satisfies a specified condition or 
        /// a default value if no such base_SaleTaxLocation is found.
        /// </summary>
        /// <param name="expression">A function to test each base_SaleTaxLocation for a condition.</param>
        /// <returns>    
        /// Null if source is empty or if no base_SaleTaxLocation passes the test specified by expression; 
        /// otherwise, the first base_SaleTaxLocation in source that passes the test specified by expression.
        /// </returns>
        public base_SaleTaxLocation Get(Expression<Func<base_SaleTaxLocation, bool>> expression)
        {
            return UnitOfWork.Get<base_SaleTaxLocation>(expression);
        }

        /// <summary>
        /// Get all base_SaleTaxLocation.
        /// </summary>
        /// <returns>The new IList&lt;base_SaleTaxLocation&gt; instance.</returns>
        public IList<base_SaleTaxLocation> GetAll()
        {
            return UnitOfWork.GetAll<base_SaleTaxLocation>().ToList();
        }

        /// <summary>
        /// Get all base_SaleTaxLocation that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_SaleTaxLocation for a condition.</param>
        /// <returns>The new IList&lt;base_SaleTaxLocation&gt; instance.</returns>
        public IList<base_SaleTaxLocation> GetAll(Expression<Func<base_SaleTaxLocation, bool>> expression)
        {
            return UnitOfWork.GetAll<base_SaleTaxLocation>(expression).ToList();
        }

        /// <summary>
        /// Get all base_SaleTaxLocation.
        /// </summary>
        /// <returns>The new IEnumerable&lt;base_SaleTaxLocation&gt; instance.</returns>
        public IEnumerable<base_SaleTaxLocation> GetIEnumerable()
        {
            return UnitOfWork.GetIEnumerable<base_SaleTaxLocation>();
        }

        /// <summary>
        /// Get all base_SaleTaxLocation that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_SaleTaxLocation for a condition.</param>
        /// <returns>The new IEnumerable&lt;base_SaleTaxLocation&gt; instance.</returns>
        public IEnumerable<base_SaleTaxLocation> GetIEnumerable(Expression<Func<base_SaleTaxLocation, bool>> expression)
        {
            return UnitOfWork.GetIEnumerable<base_SaleTaxLocation>(expression);
        }

        /// <summary>
        /// Get all base_SaleTaxLocation.
        /// </summary>
        /// <returns>The new IQueryable&lt;base_SaleTaxLocation&gt; instance.</returns>
        public IQueryable<base_SaleTaxLocation> GetIQueryable()
        {
            return UnitOfWork.GetIQueryable<base_SaleTaxLocation>();
        }

        /// <summary>
        /// Get all base_SaleTaxLocation that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_SaleTaxLocation for a condition.</param>
        /// <returns>The new IQueryable&lt;base_SaleTaxLocation&gt; instance.</returns>
        public IQueryable<base_SaleTaxLocation> GetIQueryable(Expression<Func<base_SaleTaxLocation, bool>> expression)
        {
            return UnitOfWork.GetIQueryable<base_SaleTaxLocation>(expression);
        }

        /// <summary>
        /// Take a few base_SaleTaxLocation in a sequence was sorted on server.
        /// </summary>
        /// <param name="ignoreCount">Number of base_SaleTaxLocation will ignore.</param>
        /// <param name="takeCount">Number of base_SaleTaxLocation will take.</param>
        /// <param name="keys">The key columns by which to order the results.</param>
        /// <returns>The new IList&lt;base_SaleTaxLocation&gt; instance.</returns>
        public IList<base_SaleTaxLocation> GetRange(int ignoreCount, int takeCount, string keys)
        {
            return UnitOfWork.GetRange<base_SaleTaxLocation>(ignoreCount, takeCount, keys);
        }

        /// <summary>
        /// Take a few base_SaleTaxLocation in a sequence was sorted on server.
        /// </summary>
        /// <param name="ignoreCount">Number of base_SaleTaxLocation will ignore.</param>
        /// <param name="takeCount">Number of base_SaleTaxLocation will take.</param>
        /// <param name="keys">The key columns by which to order the results.</param>
        /// <param name="expression">A function to test each base_SaleTaxLocation for a condition.</param>
        /// <returns>The new IList&lt;base_SaleTaxLocation&gt; instance.</returns>
        public IList<base_SaleTaxLocation> GetRange(int ignoreCount, int takeCount, string keys, Expression<Func<base_SaleTaxLocation, bool>> expression)
        {
            return UnitOfWork.GetRange<base_SaleTaxLocation>(ignoreCount, takeCount, keys, expression);
        }

        /// <summary>
        /// Updates an base_SaleTaxLocation in the object context with data from the data source.
        /// </summary>
        /// <param name="base_SaleTaxLocation">The base_SaleTaxLocation to be refreshed.</param>
        public base_SaleTaxLocation Refresh(base_SaleTaxLocation base_SaleTaxLocation)
        {
            UnitOfWork.Refresh<base_SaleTaxLocation>(base_SaleTaxLocation);
            if (base_SaleTaxLocation.EntityState != System.Data.EntityState.Detached)
                return base_SaleTaxLocation;
            return null;
        }

        /// <summary>
        /// Updates a sequence of base_SaleTaxLocation in the object context with data from the data source.
        /// </summary>
        /// <typeparam name="base_SaleTaxLocation">Type of object in a sequence to refresh.</typeparam>
        /// <param name="base_SaleTaxLocation">Object collection to be refreshed.</param>
        public void Refresh(IEnumerable<base_SaleTaxLocation> base_SaleTaxLocation)
        {
            UnitOfWork.Refresh<base_SaleTaxLocation>(base_SaleTaxLocation);
        }

        /// <summary>
        /// Updates a sequence of base_SaleTaxLocation in the object context with data from the data source.
        /// </summary>
        public void Refresh()
        {
            UnitOfWork.Refresh<base_SaleTaxLocation>();
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
