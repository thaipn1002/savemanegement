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
using System.Linq;
using System.Linq.Expressions;
using CPC.Helper;
using CPC.POS.Database;
using CPC.Toolkit.Base;

namespace CPC.POS.Model
{
    /// <summary>
    /// Model for table base_CountStock
    /// </summary>
    [Serializable]
    public partial class base_CountStockModel : ModelBase, IDataErrorInfo
    {
        #region Constructor

        // Default constructor
        public base_CountStockModel()
        {
            this.IsNew = true;
            this.base_CountStock = new base_CountStock();
        }

        // Default constructor that set entity to field
        public base_CountStockModel(base_CountStock base_countstock, bool isRaiseProperties = false)
        {
            this.base_CountStock = base_countstock;
            if (!isRaiseProperties)
                this.ToModel();
            else
                this.ToModelAndRaise();
            this.IsDirty = false;
        }

        #endregion

        #region Entity Properties

        public base_CountStock base_CountStock { get; private set; }

        #endregion

        #region Primitive Properties

        protected long _id;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the Id</param>
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

        protected string _documentNo;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the DocumentNo</param>
        /// </summary>
        public string DocumentNo
        {
            get { return this._documentNo; }
            set
            {
                if (this._documentNo != value)
                {
                    this.IsDirty = true;
                    this._documentNo = value;
                    OnPropertyChanged(() => DocumentNo);
                    PropertyChangedCompleted(() => DocumentNo);
                }
            }
        }

        protected System.DateTime _dateCreated;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the DateCreated</param>
        /// </summary>
        public System.DateTime DateCreated
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
        /// <param>Gets or sets the UserCreated</param>
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

        protected Nullable<System.DateTime> _completedDate;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the CompletedDate</param>
        /// </summary>
        public Nullable<System.DateTime> CompletedDate
        {
            get { return this._completedDate; }
            set
            {
                if (this._completedDate != value)
                {
                    this.IsDirty = true;
                    this._completedDate = value;
                    OnPropertyChanged(() => CompletedDate);
                    PropertyChangedCompleted(() => CompletedDate);
                }
            }
        }

        protected string _userCounted;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the UserCounted</param>
        /// </summary>
        public string UserCounted
        {
            get { return this._userCounted; }
            set
            {
                if (this._userCounted != value)
                {
                    this.IsDirty = true;
                    this._userCounted = value;
                    OnPropertyChanged(() => UserCounted);
                    PropertyChangedCompleted(() => UserCounted);
                }
            }
        }

        protected short _status;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the Status</param>
        /// </summary>
        public short Status
        {
            get { return this._status; }
            set
            {
                if (this._status != value)
                {
                    this.IsDirty = true;
                    this._status = value;
                    OnPropertyChanged(() => Status);
                    PropertyChangedCompleted(() => Status);
                }
            }
        }

        protected System.Guid _resource;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the Resource</param>
        /// </summary>
        public System.Guid Resource
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

        protected string _shift;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the Shift</param>
        /// </summary>
        public string Shift
        {
            get { return this._shift; }
            set
            {
                if (this._shift != value)
                {
                    this.IsDirty = true;
                    this._shift = value;
                    OnPropertyChanged(() => Shift);
                    PropertyChangedCompleted(() => Shift);
                }
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// <param>Public Method</param>
        /// Method for set IsNew & IsDirty = false;
        /// </summary>
        public void EndUpdate()
        {
            this.IsNew = false;
            this.IsDirty = false;
        }

        /// <summary>
        /// Public Method
        /// <param>Method for set PropertyModel to Entity</param>
        /// </summary>
        public void ToEntity()
        {
            if (IsNew)
                this.base_CountStock.Id = this.Id;
            if (this.DocumentNo != null)
                this.base_CountStock.DocumentNo = this.DocumentNo.Trim();
            this.base_CountStock.DateCreated = this.DateCreated;
            if (this.UserCreated != null)
                this.base_CountStock.UserCreated = this.UserCreated.Trim();
            this.base_CountStock.CompletedDate = this.CompletedDate;
            if (this.UserCounted != null)
                this.base_CountStock.UserCounted = this.UserCounted.Trim();
            this.base_CountStock.Status = this.Status;
            this.base_CountStock.Resource = this.Resource;
            if (this.Shift != null)
                this.base_CountStock.Shift = this.Shift.Trim();
        }

        /// <summary>
        /// Public Method
        /// <param>Method for set Entity to PropertyModel</param>
        /// </summary
        public void ToModel()
        {
            this._id = this.base_CountStock.Id;
            this._documentNo = this.base_CountStock.DocumentNo;
            this._dateCreated = this.base_CountStock.DateCreated;
            this._userCreated = this.base_CountStock.UserCreated;
            this._completedDate = this.base_CountStock.CompletedDate;
            this._userCounted = this.base_CountStock.UserCounted;
            this._status = this.base_CountStock.Status;
            this._resource = this.base_CountStock.Resource;
            this._shift = this.base_CountStock.Shift;
        }

        /// <summary>
        /// Public Method
        /// <param>Method for set Entity to PropertyModel</param>
        /// </summary
        public void ToModelAndRaise()
        {
            this.Id = this.base_CountStock.Id;
            this.DocumentNo = this.base_CountStock.DocumentNo;
            this.DateCreated = this.base_CountStock.DateCreated;
            this.UserCreated = this.base_CountStock.UserCreated;
            this.CompletedDate = this.base_CountStock.CompletedDate;
            this.UserCounted = this.base_CountStock.UserCounted;
            this.Status = this.base_CountStock.Status;
            this.Resource = this.base_CountStock.Resource;
            this.Shift = this.base_CountStock.Shift;
        }

        #endregion

        #region Custom Code

        #region CountStockDetailCollection
        private ObservableCollection<base_CountStockDetailModel> _countStockDetailCollection;
        /// <summary>
        /// Gets or sets the CountStockDetailCollection.
        /// </summary>
        public ObservableCollection<base_CountStockDetailModel> CountStockDetailCollection
        {
            get { return _countStockDetailCollection; }
            set
            {
                if (_countStockDetailCollection != value)
                {
                    _countStockDetailCollection = value;
                    OnPropertyChanged(() => CountStockDetailCollection);
                }
            }
        }
        #endregion

        #region IsLoad
        protected bool _isLoad;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the Id</para>
        /// </summary>
        public bool IsLoad
        {
            get { return this._isLoad; }
            set
            {
                if (this._isLoad != value)
                {
                    this._isLoad = value;
                    OnPropertyChanged(() => IsLoad);
                    PropertyChangedCompleted(() => IsLoad);
                }
            }
        }
        #endregion

        #region IsChangeProductCollection
        protected bool _isChangeProductCollection;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the IsChangeProductCollection</para>
        /// </summary>
        public bool IsChangeProductCollection
        {
            get { return this._isChangeProductCollection; }
            set
            {
                if (this._isChangeProductCollection != value)
                {
                    this._isChangeProductCollection = value;
                    OnPropertyChanged(() => IsChangeProductCollection);
                }
            }
        }
        #endregion

        #region IsEnable
        protected bool _isEnable = true;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the Id</para>
        /// </summary>
        public bool IsEnable
        {
            get { return this._isEnable; }
            set
            {
                if (this._isEnable != value)
                {
                    this._isEnable = value;
                    OnPropertyChanged(() => IsEnable);
                    PropertyChangedCompleted(() => IsEnable);
                }
            }
        }
        #endregion

        #region HasValue
        protected bool _hasValue = true;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the HasValue</para>
        /// </summary>
        public bool HasValue
        {
            get { return this._hasValue; }
            set
            {
                if (this._hasValue != value)
                {
                    this._hasValue = value;
                    OnPropertyChanged(() => HasValue);
                    PropertyChangedCompleted(() => HasValue);
                }
            }
        }
        #endregion

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
                    case "DocumentNo":
                        break;
                    case "DateCreated":
                        break;
                    case "UserCreated":
                        break;
                    case "CountedDate":
                        break;
                    case "UserCounted":
                        break;
                    case "Status":
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