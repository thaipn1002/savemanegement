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
    /// Model for table base_ResourcePayment
    /// </summary>
    [Serializable]
    public partial class base_ResourcePaymentModel : ModelBase, IDataErrorInfo
    {
        #region Constructor

        // Default constructor
        public base_ResourcePaymentModel()
        {
            this.IsNew = true;
            this.base_ResourcePayment = new base_ResourcePayment();
        }

        // Default constructor that set entity to field
        public base_ResourcePaymentModel(base_ResourcePayment base_resourcepayment, bool isRaiseProperties = false)
        {
            this.base_ResourcePayment = base_resourcepayment;
            if (!isRaiseProperties)
                this.ToModel();
            else
                this.ToModelAndRaise();
            this.IsDirty = false;
        }

        #endregion

        #region Entity Properties

        public base_ResourcePayment base_ResourcePayment { get; private set; }

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

        protected decimal _totalPaid;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the TotalPaid</para>
        /// </summary>
        public decimal TotalPaid
        {
            get { return this._totalPaid; }
            set
            {
                if (this._totalPaid != value)
                {
                    this.IsDirty = true;
                    this._totalPaid = value;
                    OnPropertyChanged(() => TotalPaid);
                    PropertyChangedCompleted(() => TotalPaid);
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

        protected decimal _change;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the Change</para>
        /// </summary>
        public decimal Change
        {
            get { return this._change; }
            set
            {
                if (this._change != value)
                {
                    this.IsDirty = true;
                    this._change = value;
                    OnPropertyChanged(() => Change);
                    PropertyChangedCompleted(() => Change);
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

        protected string _remark;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the Remark</para>
        /// </summary>
        public string Remark
        {
            get { return this._remark; }
            set
            {
                if (this._remark != value)
                {
                    this.IsDirty = true;
                    this._remark = value;
                    OnPropertyChanged(() => Remark);
                    PropertyChangedCompleted(() => Remark);
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

        protected Nullable<bool> _isDeposit;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the IsDeposit</para>
        /// </summary>
        public Nullable<bool> IsDeposit
        {
            get { return this._isDeposit; }
            set
            {
                if (this._isDeposit != value)
                {
                    this.IsDirty = true;
                    this._isDeposit = value;
                    OnPropertyChanged(() => IsDeposit);
                    PropertyChangedCompleted(() => IsDeposit);
                }
            }
        }

        protected string _taxCode;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the TaxCode</para>
        /// </summary>
        public string TaxCode
        {
            get { return this._taxCode; }
            set
            {
                if (this._taxCode != value)
                {
                    this.IsDirty = true;
                    this._taxCode = value;
                    OnPropertyChanged(() => TaxCode);
                    PropertyChangedCompleted(() => TaxCode);
                }
            }
        }

        protected decimal _taxAmount;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the TaxAmount</para>
        /// </summary>
        public decimal TaxAmount
        {
            get { return this._taxAmount; }
            set
            {
                if (this._taxAmount != value)
                {
                    this.IsDirty = true;
                    this._taxAmount = value;
                    OnPropertyChanged(() => TaxAmount);
                    PropertyChangedCompleted(() => TaxAmount);
                }
            }
        }

        protected decimal _lastRewardAmount;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the LastRewardAmount</para>
        /// </summary>
        public decimal LastRewardAmount
        {
            get { return this._lastRewardAmount; }
            set
            {
                if (this._lastRewardAmount != value)
                {
                    this.IsDirty = true;
                    this._lastRewardAmount = value;
                    OnPropertyChanged(() => LastRewardAmount);
                    PropertyChangedCompleted(() => LastRewardAmount);
                }
            }
        }

        protected string _cashier;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the Cashier</para>
        /// </summary>
        public string Cashier
        {
            get { return this._cashier; }
            set
            {
                if (this._cashier != value)
                {
                    this.IsDirty = true;
                    this._cashier = value;
                    OnPropertyChanged(() => Cashier);
                    PropertyChangedCompleted(() => Cashier);
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
                this.base_ResourcePayment.Id = this.Id;
            this.base_ResourcePayment.DocumentResource = this.DocumentResource;
            this.base_ResourcePayment.DocumentNo = this.DocumentNo;
            this.base_ResourcePayment.TotalAmount = this.TotalAmount;
            this.base_ResourcePayment.TotalPaid = this.TotalPaid;
            this.base_ResourcePayment.Balance = this.Balance;
            this.base_ResourcePayment.Change = this.Change;
            this.base_ResourcePayment.DateCreated = this.DateCreated;
            this.base_ResourcePayment.UserCreated = this.UserCreated;
            this.base_ResourcePayment.Remark = this.Remark;
            this.base_ResourcePayment.Resource = this.Resource;
            this.base_ResourcePayment.SubTotal = this.SubTotal;
            this.base_ResourcePayment.DiscountPercent = this.DiscountPercent;
            this.base_ResourcePayment.DiscountAmount = this.DiscountAmount;
            this.base_ResourcePayment.Mark = this.Mark;
            this.base_ResourcePayment.IsDeposit = this.IsDeposit;
            this.base_ResourcePayment.TaxCode = this.TaxCode;
            this.base_ResourcePayment.TaxAmount = this.TaxAmount;
            this.base_ResourcePayment.LastRewardAmount = this.LastRewardAmount;
            this.base_ResourcePayment.Cashier = this.Cashier;
            this.base_ResourcePayment.Shift = this.Shift;
        }

        /// <summary>
        /// Public Method
        /// <para>Method for set Entity to PropertyModel</para>
        /// </summary
        public void ToModel()
        {
            this._id = this.base_ResourcePayment.Id;
            this._documentResource = this.base_ResourcePayment.DocumentResource;
            this._documentNo = this.base_ResourcePayment.DocumentNo;
            this._totalAmount = this.base_ResourcePayment.TotalAmount;
            this._totalPaid = this.base_ResourcePayment.TotalPaid;
            this._balance = this.base_ResourcePayment.Balance;
            this._change = this.base_ResourcePayment.Change;
            this._dateCreated = this.base_ResourcePayment.DateCreated;
            this._userCreated = this.base_ResourcePayment.UserCreated;
            this._remark = this.base_ResourcePayment.Remark;
            this._resource = this.base_ResourcePayment.Resource;
            this._subTotal = this.base_ResourcePayment.SubTotal;
            this._discountPercent = this.base_ResourcePayment.DiscountPercent;
            this._discountAmount = this.base_ResourcePayment.DiscountAmount;
            this._mark = this.base_ResourcePayment.Mark;
            this._isDeposit = this.base_ResourcePayment.IsDeposit;
            this._taxCode = this.base_ResourcePayment.TaxCode;
            this._taxAmount = this.base_ResourcePayment.TaxAmount;
            this._lastRewardAmount = this.base_ResourcePayment.LastRewardAmount;
            this._cashier = this.base_ResourcePayment.Cashier;
            this._shift = this.base_ResourcePayment.Shift;
        }

        /// <summary>
        /// Public Method
        /// <para>Method for set Entity to PropertyModel</para>
        /// </summary
        public void ToModelAndRaise()
        {
            this.Id = this.base_ResourcePayment.Id;
            this.DocumentResource = this.base_ResourcePayment.DocumentResource;
            this.DocumentNo = this.base_ResourcePayment.DocumentNo;
            this.TotalAmount = this.base_ResourcePayment.TotalAmount;
            this.TotalPaid = this.base_ResourcePayment.TotalPaid;
            this.Balance = this.base_ResourcePayment.Balance;
            this.Change = this.base_ResourcePayment.Change;
            this.DateCreated = this.base_ResourcePayment.DateCreated;
            this.UserCreated = this.base_ResourcePayment.UserCreated;
            this.Remark = this.base_ResourcePayment.Remark;
            this.Resource = this.base_ResourcePayment.Resource;
            this.SubTotal = this.base_ResourcePayment.SubTotal;
            this.DiscountPercent = this.base_ResourcePayment.DiscountPercent;
            this.DiscountAmount = this.base_ResourcePayment.DiscountAmount;
            this.Mark = this.base_ResourcePayment.Mark;
            this.IsDeposit = this.base_ResourcePayment.IsDeposit;
            this.TaxCode = this.base_ResourcePayment.TaxCode;
            this.TaxAmount = this.base_ResourcePayment.TaxAmount;
            this.LastRewardAmount = this.base_ResourcePayment.LastRewardAmount;
            this.Cashier = this.base_ResourcePayment.Cashier;
            this.Shift = this.base_ResourcePayment.Shift;
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
                    case "TotalPaid":
                        break;
                    case "Balance":
                        break;
                    case "Change":
                        break;
                    case "DateCreated":
                        break;
                    case "UserCreated":
                        break;
                    case "Remark":
                        break;
                    case "Resource":
                        break;
                    case "SubTotal":
                        break;
                    case "DiscountPercent":
                        break;
                    case "DiscountAmount":
                        break;
                    case "Mark":
                        break;
                    case "IsDeposit":
                        break;
                    case "TaxCode":
                        break;
                    case "TaxAmount":
                        break;
                    case "LastRewardAmount":
                        break;
                    case "Cashier":
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
