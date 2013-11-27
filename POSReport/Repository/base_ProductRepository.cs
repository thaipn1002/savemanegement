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
    /// Repository for table base_Product 
    /// </summary>
    public partial class base_ProductRepository
    {
        #region Auto Generate Code

        #region Constructors

        // Default constructor
        public base_ProductRepository()
        {
        }

        #endregion

        #region Basic C.R.U.D. Operations

        /// <summary>
        /// Add new base_Product.
        /// </summary>
        /// <param name="base_Product">base_Product to add.</param>
        /// <returns>base_Product have been added.</returns>
        public base_Product Add(base_Product base_Product)
        {
            UnitOfWork.Add<base_Product>(base_Product);
            return base_Product;
        }

        /// <summary>
        /// Adds a sequence of new base_Product.
        /// </summary>
        /// <param name="base_Product">Sequence of new base_Product to add.</param>
        /// <returns>Sequence of new base_Product have been added.</returns>
        public IEnumerable<base_Product> Add(IEnumerable<base_Product> base_Product)
        {
            UnitOfWork.Add<base_Product>(base_Product);
            return base_Product;
        }

        /// <summary>
        /// Delete a existed base_Product.
        /// </summary>
        /// <param name="base_Product">base_Product to delete.</param>
        public void Delete(base_Product base_Product)
        {
            Refresh(base_Product);
            if (base_Product.EntityState != System.Data.EntityState.Detached)
                UnitOfWork.Delete<base_Product>(base_Product);
        }

        /// <summary>
        /// Delete a sequence of existed base_Product.
        /// </summary>
        /// <param name="base_Product">Sequence of existed base_Product to delete.</param>
        public void Delete(IEnumerable<base_Product> base_Product)
        {
            int total = base_Product.Count();
            for (int i = total - 1; i >= 0; i--)
                Delete(base_Product.ElementAt(i));
        }

        /// <summary>
        /// Returns the first base_Product of a sequence that satisfies a specified condition or 
        /// a default value if no such base_Product is found.
        /// </summary>
        /// <param name="expression">A function to test each base_Product for a condition.</param>
        /// <returns>    
        /// Null if source is empty or if no base_Product passes the test specified by expression; 
        /// otherwise, the first base_Product in source that passes the test specified by expression.
        /// </returns>
        public base_Product Get(Expression<Func<base_Product, bool>> expression)
        {
            return UnitOfWork.Get<base_Product>(expression);
        }

        /// <summary>
        /// Get all base_Product.
        /// </summary>
        /// <returns>The new IList&lt;base_Product&gt; instance.</returns>
        public IList<base_Product> GetAll()
        {
            return UnitOfWork.GetAll<base_Product>().ToList();
        }

        /// <summary>
        /// Get all base_Product that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_Product for a condition.</param>
        /// <returns>The new IList&lt;base_Product&gt; instance.</returns>
        public IList<base_Product> GetAll(Expression<Func<base_Product, bool>> expression)
        {
            return UnitOfWork.GetAll<base_Product>(expression).ToList();
        }

        /// <summary>
        /// Get all base_Product.
        /// </summary>
        /// <returns>The new IEnumerable&lt;base_Product&gt; instance.</returns>
        public IEnumerable<base_Product> GetIEnumerable()
        {
            return UnitOfWork.GetIEnumerable<base_Product>();
        }

        /// <summary>
        /// Get all base_Product that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_Product for a condition.</param>
        /// <returns>The new IEnumerable&lt;base_Product&gt; instance.</returns>
        public IEnumerable<base_Product> GetIEnumerable(Expression<Func<base_Product, bool>> expression)
        {
            return UnitOfWork.GetIEnumerable<base_Product>(expression);
        }

        /// <summary>
        /// Get all base_Product.
        /// </summary>
        /// <returns>The new IQueryable&lt;base_Product&gt; instance.</returns>
        public IQueryable<base_Product> GetIQueryable()
        {
            return UnitOfWork.GetIQueryable<base_Product>();
        }

        /// <summary>
        /// Get all base_Product that satisfies a specified condition.
        /// </summary>
        /// <param name="expression">A function to test each base_Product for a condition.</param>
        /// <returns>The new IQueryable&lt;base_Product&gt; instance.</returns>
        public IQueryable<base_Product> GetIQueryable(Expression<Func<base_Product, bool>> expression)
        {
            return UnitOfWork.GetIQueryable<base_Product>(expression);
        }

        /// <summary>
        /// Take a few base_Product in a sequence was sorted on server.
        /// </summary>
        /// <param name="ignoreCount">Number of base_Product will ignore.</param>
        /// <param name="takeCount">Number of base_Product will take.</param>
        /// <param name="keys">The key columns by which to order the results.</param>
        /// <returns>The new IList&lt;base_Product&gt; instance.</returns>
        public IList<base_Product> GetRange(int ignoreCount, int takeCount, string keys)
        {
            return UnitOfWork.GetRange<base_Product>(ignoreCount, takeCount, keys);
        }

        /// <summary>
        /// Take a few base_Product in a sequence was sorted on server.
        /// </summary>
        /// <param name="ignoreCount">Number of base_Product will ignore.</param>
        /// <param name="takeCount">Number of base_Product will take.</param>
        /// <param name="keys">The key columns by which to order the results.</param>
        /// <param name="expression">A function to test each base_Product for a condition.</param>
        /// <returns>The new IList&lt;base_Product&gt; instance.</returns>
        public IList<base_Product> GetRange(int ignoreCount, int takeCount, string keys, Expression<Func<base_Product, bool>> expression)
        {
            return UnitOfWork.GetRange<base_Product>(ignoreCount, takeCount, keys, expression);
        }

        /// <summary>
        /// Updates an base_Product in the object context with data from the data source.
        /// </summary>
        /// <param name="base_Product">The base_Product to be refreshed.</param>
        public base_Product Refresh(base_Product base_Product)
        {
            UnitOfWork.Refresh<base_Product>(base_Product);
            if (base_Product.EntityState != System.Data.EntityState.Detached)
                return base_Product;
            return null;
        }

        /// <summary>
        /// Updates a sequence of base_Product in the object context with data from the data source.
        /// </summary>
        /// <typeparam name="base_Product">Type of object in a sequence to refresh.</typeparam>
        /// <param name="base_Product">Object collection to be refreshed.</param>
        public void Refresh(IEnumerable<base_Product> base_Product)
        {
            UnitOfWork.Refresh<base_Product>(base_Product);
        }

        /// <summary>
        /// Updates a sequence of base_Product in the object context with data from the data source.
        /// </summary>
        public void Refresh()
        {
            UnitOfWork.Refresh<base_Product>();
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
