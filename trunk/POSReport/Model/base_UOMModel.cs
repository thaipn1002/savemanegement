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
    /// Model for table base_UOM
    /// </summary>
    [Serializable]
    public partial class base_UOMModel : ModelBase, IDataErrorInfo
    {
        #region Constructor

        // Default constructor
        public base_UOMModel()
        {
            this.IsNew = true;
            this.base_UOM = new base_UOM();
        }

        // Default constructor that set entity to field
        public base_UOMModel(base_UOM base_uom, bool isRaiseProperties = false)
        {
            this.base_UOM = base_uom;
            if (!isRaiseProperties)
                this.ToModel();
            else
                this.ToModelAndRaise();
            this.IsDirty = false;
        }

        #endregion

        #region Entity Properties

        public base_UOM base_UOM { get; private set; }

        #endregion

        #region Primitive Properties

        protected int _id;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the Id</para>
        /// </summary>
        public int Id
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

        protected string _name;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the Name</para>
        /// </summary>
        public string Name
        {
            get { return this._name; }
            set
            {
                if (this._name != value)
                {
                    this.IsDirty = true;
                    this._name = value;
                    OnPropertyChanged(() => Name);
                    PropertyChangedCompleted(() => Name);
                }
            }
        }

        protected Nullable<System.DateTime> _dateCreated;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the DateCreated</para>
        /// </summary>
        public Nullable<System.DateTime> DateCreated
        {
            get { return this._dateCreated; }
            set
            {
                if (this._dateCreated != value)
                {
                    this.IsDirty = true;
                    this._dateCreated = value;
                    OnPropertyChanged(() => DateCreated);
                    PropertyChangedCompleted(() => DateCreated);
                }
            }
        }

        protected string _userCreated;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the UserCreated</para>
        /// </summary>
        public string UserCreated
        {
            get { return this._userCreated; }
            set
            {
                if (this._userCreated != value)
                {
                    this.IsDirty = true;
                    this._userCreated = value;
                    OnPropertyChanged(() => UserCreated);
                    PropertyChangedCompleted(() => UserCreated);
                }
            }
        }

        protected Nullable<System.DateTime> _dateUpdated;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the DateUpdated</para>
        /// </summary>
        public Nullable<System.DateTime> DateUpdated
        {
            get { return this._dateUpdated; }
            set
            {
                if (this._dateUpdated != value)
                {
                    this.IsDirty = true;
                    this._dateUpdated = value;
                    OnPropertyChanged(() => DateUpdated);
                    PropertyChangedCompleted(() => DateUpdated);
                }
            }
        }

        protected string _userUpdated;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the UserUpdated</para>
        /// </summary>
        public string UserUpdated
        {
            get { return this._userUpdated; }
            set
            {
                if (this._userUpdated != value)
                {
                    this.IsDirty = true;
                    this._userUpdated = value;
                    OnPropertyChanged(() => UserUpdated);
                    PropertyChangedCompleted(() => UserUpdated);
                }
            }
        }

        protected bool _isActived;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the IsActived</para>
        /// </summary>
        public bool IsActived
        {
            get { return this._isActived; }
            set
            {
                if (this._isActived != value)
                {
                    this.IsDirty = true;
                    this._isActived = value;
                    OnPropertyChanged(() => IsActived);
                    PropertyChangedCompleted(() => IsActived);
                }
            }
        }

        protected Nullable<System.Guid> _resource;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the Resource</para>
        /// </summary>
        public Nullable<System.Guid> Resource
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
                this.base_UOM.Id = this.Id;
            this.base_UOM.Name = this.Name;
            this.base_UOM.DateCreated = this.DateCreated;
            this.base_UOM.UserCreated = this.UserCreated;
            this.base_UOM.DateUpdated = this.DateUpdated;
            this.base_UOM.UserUpdated = this.UserUpdated;
            this.base_UOM.IsActived = this.IsActived;
            this.base_UOM.Resource = this.Resource;
        }

        /// <summary>
        /// Public Method
        /// <para>Method for set Entity to PropertyModel</para>
        /// </summary
        public void ToModel()
        {
            this._id = this.base_UOM.Id;
            this._name = this.base_UOM.Name;
            this._dateCreated = this.base_UOM.DateCreated;
            this._userCreated = this.base_UOM.UserCreated;
            this._dateUpdated = this.base_UOM.DateUpdated;
            this._userUpdated = this.base_UOM.UserUpdated;
            this._isActived = this.base_UOM.IsActived;
            this._resource = this.base_UOM.Resource;
        }

        /// <summary>
        /// Public Method
        /// <para>Method for set Entity to PropertyModel</para>
        /// </summary
        public void ToModelAndRaise()
        {
            this.Id = this.base_UOM.Id;
            this.Name = this.base_UOM.Name;
            this.DateCreated = this.base_UOM.DateCreated;
            this.UserCreated = this.base_UOM.UserCreated;
            this.DateUpdated = this.base_UOM.DateUpdated;
            this.UserUpdated = this.base_UOM.UserUpdated;
            this.IsActived = this.base_UOM.IsActived;
            this.Resource = this.base_UOM.Resource;
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
                    case "Code":
                        break;
                    case "Name":
                        break;
                    case "DateCreated":
                        break;
                    case "UserCreated":
                        break;
                    case "DateUpdated":
                        break;
                    case "UserUpdated":
                        break;
                    case "IsActived":
                        break;
                    case "Resource":
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