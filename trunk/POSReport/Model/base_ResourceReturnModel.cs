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
    /// Model for table base_ResourceReturn
    /// </summary>
    [Serializable]
    public partial class base_ResourceReturnModel : ModelBase, IDataErrorInfo
    {
        #region Constructor

        // Default constructor
        public base_ResourceReturnModel()
        {
            this.IsNew = true;
            this.base_ResourceReturn = new base_ResourceReturn();
        }

        // Default constructor that set entity to field
        public base_ResourceReturnModel(base_ResourceReturn base_resourcereturn, bool isRaiseProperties = false)
        {
            this.base_ResourceReturn = base_resourcereturn;
            if (!isRaiseProperties)
                this.ToModel();
            else
                this.ToModelAndRaise();
            this.IsDirty = false;
        }

        #endregion

        #region Entity Properties

        public base_ResourceReturn base_ResourceReturn { get; private set; }

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

        protected string _documentResource;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the DocumentResource</para>
        /// </summary>
        public string DocumentResource
        {
            get { return this._documentResource; }
            set
            {
                if (this._documentResource != value)
                {
                    this.IsDirty = true;
                    this._documentResource = value;
                    OnPropertyChanged(() => DocumentResource);
                    PropertyChangedCompleted(() => DocumentResource);
                }
            }
        }

        protected string _documentNo;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the DocumentNo</para>
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

        protected decimal _totalAmount;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the TotalAmount</para>
        /// </summary>
        public decimal TotalAmount
        {
            get { return this._totalAmount; }
            set
            {
                if (this._totalAmount != value)
                {
                    this.IsDirty = true;
                    this._totalAmount = value;
                    OnPropertyChanged(() => TotalAmount);
                    PropertyChangedCompleted(() => TotalAmount);
                }
            }
        }

        protected decimal _totalRefund;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the TotalRefund</para>
        /// </summary>
        public decimal TotalRefund
        {
            get { return this._totalRefund; }
            set
            {
                if (this._totalRefund != value)
                {
                    this.IsDirty = true;
                    this._totalRefund = value;
                    OnPropertyChanged(() => TotalRefund);
                    PropertyChangedCompleted(() => TotalRefund);
                }
            }
        }

        protected decimal _balance;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the Balance</para>
        /// </summary>
        public decimal Balance
        {
            get { return this._balance; }
            set
            {
                if (this._balance != value)
                {
                    this.IsDirty = true;
                    this._balance = value;
                    OnPropertyChanged(() => Balance);
                    PropertyChangedCompleted(() => Balance);
                }
            }
        }

        protected System.DateTime _dateCreated;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the DateCreated</para>
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

        protected System.Guid _resource;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the Resource</para>
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

        protected string _mark;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the Mark</para>
        /// </summary>
        public string Mark
        {
            get { return this._mark; }
            set
            {
                if (this._mark != value)
                {
                    this.IsDirty = true;
                    this._mark = value;
                    OnPropertyChanged(() => Mark);
                    PropertyChangedCompleted(() => Mark);
                }
            }
        }

        protected decimal _discountPercent;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the DiscountPercent</para>
        /// </summary>
        public decimal DiscountPercent
        {
            get { return this._discountPercent; }
            set
            {
                if (this._discountPercent != value)
                {
                    this.IsDirty = true;
                    this._discountPercent = value;
                    OnPropertyChanged(() => DiscountPercent);
                    PropertyChangedCompleted(() => DiscountPercent);
                }
            }
        }

        protected decimal _discountAmount;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the DiscountAmount</para>
        /// </summary>
        public decimal DiscountAmount
        {
            get { return this._discountAmount; }
            set
            {
                if (this._discountAmount != value)
                {
                    this.IsDirty = true;
                    this._discountAmount = value;
                    OnPropertyChanged(() => DiscountAmount);
                    PropertyChangedCompleted(() => DiscountAmount);
                }
            }
        }

        protected decimal _freight;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the Freight</para>
        /// </summary>
        public decimal Freight
        {
            get { return this._freight; }
            set
            {
                if (this._freight != value)
                {
                    this.IsDirty = true;
                    this._freight = value;
                    OnPropertyChanged(() => Freight);
                    PropertyChangedCompleted(() => Freight);
                }
            }
        }

        protected decimal _subTotal;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the SubTotal</para>
        /// </summary>
        public decimal SubTotal
        {
            get { return this._subTotal; }
            set
            {
                if (this._subTotal != value)
                {
                    this.IsDirty = true;
                    this._subTotal = value;
                    OnPropertyChanged(() => SubTotal);
                    PropertyChangedCompleted(() => SubTotal);
                }
            }
        }

        protected decimal _returnFee;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the ReturnFee</para>
        /// </summary>
        public decimal ReturnFee
        {
            get { return this._returnFee; }
            set
            {
                if (this._returnFee != value)
                {
                    this.IsDirty = true;
                    this._returnFee = value;
                    OnPropertyChanged(() => ReturnFee);
                    PropertyChangedCompleted(() => ReturnFee);
                }
            }
        }

        protected decimal _returnFeePercent;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the ReturnFeePercent</para>
        /// </summary>
        public decimal ReturnFeePercent
        {
            get { return this._returnFeePercent; }
            set
            {
                if (this._returnFeePercent != value)
                {
                    this.IsDirty = true;
                    this._returnFeePercent = value;
                    OnPropertyChanged(() => ReturnFeePercent);
                    PropertyChangedCompleted(() => ReturnFeePercent);
                }
            }
        }

        protected decimal _redeemed;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the Redeemed</para>
        /// </summary>
        public decimal Redeemed
        {
            get { return this._redeemed; }
            set
            {
                if (this._redeemed != value)
                {
                    this.IsDirty = true;
                    this._redeemed = value;
                    OnPropertyChanged(() => Redeemed);
                    PropertyChangedCompleted(() => Redeemed);
                }
            }
        }

        protected string _shift;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the Shift</para>
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
                this.base_ResourceReturn.Id = this.Id;
            this.base_ResourceReturn.DocumentResource = this.DocumentResource;
            this.base_ResourceReturn.DocumentNo = this.DocumentNo;
            this.base_ResourceReturn.TotalAmount = this.TotalAmount;
            this.base_ResourceReturn.TotalRefund = this.TotalRefund;
            this.base_ResourceReturn.Balance = this.Balance;
            this.base_ResourceReturn.DateCreated = this.DateCreated;
            this.base_ResourceReturn.UserCreated = this.UserCreated;
            this.base_ResourceReturn.Resource = this.Resource;
            this.base_ResourceReturn.Mark = this.Mark;
            this.base_ResourceReturn.DiscountPercent = this.DiscountPercent;
            this.base_ResourceReturn.DiscountAmount = this.DiscountAmount;
            this.base_ResourceReturn.Freight = this.Freight;
            this.base_ResourceReturn.SubTotal = this.SubTotal;
            this.base_ResourceReturn.ReturnFee = this.ReturnFee;
            this.base_ResourceReturn.ReturnFeePercent = this.ReturnFeePercent;
            this.base_ResourceReturn.Redeemed = this.Redeemed;
            this.base_ResourceReturn.Shift = this.Shift;
        }

        /// <summary>
        /// Public Method
        /// <para>Method for set Entity to PropertyModel</para>
        /// </summary
        public void ToModel()
        {
            this._id = this.base_ResourceReturn.Id;
            this._documentResource = this.base_ResourceReturn.DocumentResource;
            this._documentNo = this.base_ResourceReturn.DocumentNo;
            this._totalAmount = this.base_ResourceReturn.TotalAmount;
            this._totalRefund = this.base_ResourceReturn.TotalRefund;
            this._balance = this.base_ResourceReturn.Balance;
            this._dateCreated = this.base_ResourceReturn.DateCreated;
            this._userCreated = this.base_ResourceReturn.UserCreated;
            this._resource = this.base_ResourceReturn.Resource;
            this._mark = this.base_ResourceReturn.Mark;
            this._discountPercent = this.base_ResourceReturn.DiscountPercent;
            this._discountAmount = this.base_ResourceReturn.DiscountAmount;
            this._freight = this.base_ResourceReturn.Freight;
            this._subTotal = this.base_ResourceReturn.SubTotal;
            this._returnFee = this.base_ResourceReturn.ReturnFee;
            this._returnFeePercent = this.base_ResourceReturn.ReturnFeePercent;
            this._redeemed = this.base_ResourceReturn.Redeemed;
            this._shift = this.base_ResourceReturn.Shift;
        }

        /// <summary>
        /// Public Method
        /// <para>Method for set Entity to PropertyModel</para>
        /// </summary
        public void ToModelAndRaise()
        {
            this.Id = this.base_ResourceReturn.Id;
            this.DocumentResource = this.base_ResourceReturn.DocumentResource;
            this.DocumentNo = this.base_ResourceReturn.DocumentNo;
            this.TotalAmount = this.base_ResourceReturn.TotalAmount;
            this.TotalRefund = this.base_ResourceReturn.TotalRefund;
            this.Balance = this.base_ResourceReturn.Balance;
            this.DateCreated = this.base_ResourceReturn.DateCreated;
            this.UserCreated = this.base_ResourceReturn.UserCreated;
            this.Resource = this.base_ResourceReturn.Resource;
            this.Mark = this.base_ResourceReturn.Mark;
            this.DiscountPercent = this.base_ResourceReturn.DiscountPercent;
            this.DiscountAmount = this.base_ResourceReturn.DiscountAmount;
            this.Freight = this.base_ResourceReturn.Freight;
            this.SubTotal = this.base_ResourceReturn.SubTotal;
            this.ReturnFee = this.base_ResourceReturn.ReturnFee;
            this.ReturnFeePercent = this.base_ResourceReturn.ReturnFeePercent;
            this.Redeemed = this.base_ResourceReturn.Redeemed;
            this.Shift = this.base_ResourceReturn.Shift;
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
                    case "DocumentResource":
                        break;
                    case "DocumentNo":
                        break;
                    case "TotalAmount":
                        break;
                    case "TotalRefund":
                        break;
                    case "Balance":
                        break;
                    case "DateCreated":
                        break;
                    case "UserCreated":
                        break;
                    case "Resource":
                        break;
                    case "Mark":
                        break;
                    case "DiscountPercent":
                        break;
                    case "DiscountAmount":
                        break;
                    case "Freight":
                        break;
                    case "SubTotal":
                        break;
                    case "ReturnFee":
                        break;
                    case "ReturnFeePercent":
                        break;
                    case "Redeemed":
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
