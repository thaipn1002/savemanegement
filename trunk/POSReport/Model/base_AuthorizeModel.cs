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
using System.Collections.ObjectModel;
using System.ComponentModel;
using CPC.POSReport.Database;
using Toolkit.Base;

namespace CPC.POSReport.Model
{
    /// <summary>
    /// Model for table base_Authorize
    /// </summary>
    [Serializable]
    public partial class base_AuthorizeModel : ModelBase, IDataErrorInfo
    {
        #region Constructor

        // Default constructor
        public base_AuthorizeModel()
        {
            this.IsNew = true;
            this.base_Authorize = new base_Authorize();
        }

        // Default constructor that set entity to field
        public base_AuthorizeModel(base_Authorize base_authorize, bool isRaiseProperties = false)
        {
            this.base_Authorize = base_authorize;
            if (!isRaiseProperties)
                this.ToModel();
            else
                this.ToModelAndRaise();
            this.IsDirty = false;
        }

        #endregion

        #region Entity Properties

        public base_Authorize base_Authorize { get; private set; }

        #endregion

        #region Primitive Properties

        protected long _id;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the Id</para>
        /// </summary>
        public long Id
        {
            get { return this._id; }
            set
            {
                if (this._id != value)
                {
                    this.IsDirty = true;
                    this._id = value;
                    OnPropertyChanged(() => Id);
                    PropertyChangedCompleted(() => Id);
                }
            }
        }

        protected string _resource;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the Resource</para>
        /// </summary>
        public string Resource
        {
            get { return this._resource; }
            set
            {
                if (this._resource != value)
                {
                    this.IsDirty = true;
                    this._resource = value;
                    OnPropertyChanged(() => Resource);
                    PropertyChangedCompleted(() => Resource);
                }
            }
        }

        protected string _code;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the Code</para>
        /// </summary>
        public string Code
        {
            get { return this._code; }
            set
            {
                if (this._code != value)
                {
                    this.IsDirty = true;
                    this._code = value;
                    OnPropertyChanged(() => Code);
                    PropertyChangedCompleted(() => Code);
                }
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// <para>Public Method</para>
        /// Method for set IsNew & IsDirty = false;
        /// </summary>
        public void EndUpdate()
        {
            this.IsNew = false;
            this.IsDirty = false;
        }

        /// <summary>
        /// Public Method
        /// <para>Method for set PropertyModel to Entity</para>
        /// </summary>
        public void ToEntity()
        {
            if (IsNew)
                this.base_Authorize.Id = this.Id;
            this.base_Authorize.Resource = this.Resource;
            this.base_Authorize.Code = this.Code;
        }

        /// <summary>
        /// Public Method
        /// <para>Method for set Entity to PropertyModel</para>
        /// </summary
        public void ToModel()
        {
            this._id = this.base_Authorize.Id;
            this._resource = this.base_Authorize.Resource;
            this._code = this.base_Authorize.Code;
        }

        /// <summary>
        /// Public Method
        /// <para>Method for set Entity to PropertyModel</para>
        /// </summary
        public void ToModelAndRaise()
        {
            this.Id = this.base_Authorize.Id;
            this.Resource = this.base_Authorize.Resource;
            this.Code = this.base_Authorize.Code;
        }

        #endregion

        #region Custom Code


        #endregion

        #region IDataErrorInfo Members

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get
            {
                string message = string.Empty;

                switch (columnName)
                {
                    case "Id":
                        break;
                    case "Resource":
                        break;
                    case "Code":
                        break;
                }

                if (!string.IsNullOrWhiteSpace(message))
                    return message;
                return null;
            }
        }

        #endregion
    }
}
