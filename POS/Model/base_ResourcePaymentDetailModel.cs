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
using CPC.POS;

namespace CPC.POS.Model
{
    /// <summary>
    /// Model for table base_ResourcePaymentDetail
    /// </summary>
    [Serializable]
    public partial class base_ResourcePaymentDetailModel : ModelBase, IDataErrorInfo
    {
        #region Constructor

        // Default constructor
        public base_ResourcePaymentDetailModel()
        {
            this.IsNew = true;
            this.base_ResourcePaymentDetail = new base_ResourcePaymentDetail();
        }

        // Default constructor that set entity to field
        public base_ResourcePaymentDetailModel(base_ResourcePaymentDetail base_resourcepaymentdetail, bool isRaiseProperties = false)
        {
            this.base_ResourcePaymentDetail = base_resourcepaymentdetail;
            if (!isRaiseProperties)
                this.ToModel();
            else
                this.ToModelAndRaise();
            this.IsDirty = false;
        }

        #endregion

        #region Entity Properties

        public base_ResourcePaymentDetail base_ResourcePaymentDetail { get; private set; }

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

        protected string _paymentType;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the PaymentType</param>
        /// </summary>
        public string PaymentType
        {
            get { return this._paymentType; }
            set
            {
                if (this._paymentType != value)
                {
                    this.IsDirty = true;
                    this._paymentType = value;
                    OnPropertyChanged(() => PaymentType);
                    PropertyChangedCompleted(() => PaymentType);
                }
            }
        }

        protected long _resourcePaymentId;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the ResourcePaymentId</param>
        /// </summary>
        public long ResourcePaymentId
        {
            get { return this._resourcePaymentId; }
            set
            {
                if (this._resourcePaymentId != value)
                {
                    this.IsDirty = true;
                    this._resourcePaymentId = value;
                    OnPropertyChanged(() => ResourcePaymentId);
                    PropertyChangedCompleted(() => ResourcePaymentId);
                }
            }
        }

        protected short _paymentMethodId;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the PaymentMethodId</param>
        /// </summary>
        public short PaymentMethodId
        {
            get { return this._paymentMethodId; }
            set
            {
                if (this._paymentMethodId != value)
                {
                    this.IsDirty = true;
                    this._paymentMethodId = value;
                    OnPropertyChanged(() => PaymentMethodId);
                    PropertyChangedCompleted(() => PaymentMethodId);
                }
            }
        }

        protected string _paymentMethod;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the PaymentMethod</param>
        /// </summary>
        public string PaymentMethod
        {
            get { return this._paymentMethod; }
            set
            {
                if (this._paymentMethod != value)
                {
                    this.IsDirty = true;
                    this._paymentMethod = value;
                    OnPropertyChanged(() => PaymentMethod);
                    PropertyChangedCompleted(() => PaymentMethod);
                }
            }
        }

        protected short _cardType;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the CardType</param>
        /// </summary>
        public short CardType
        {
            get { return this._cardType; }
            set
            {
                if (this._cardType != value)
                {
                    this.IsDirty = true;
                    this._cardType = value;
                    OnPropertyChanged(() => CardType);
                    PropertyChangedCompleted(() => CardType);
                }
            }
        }

        protected decimal _paid;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the Paid</param>
        /// </summary>
        public decimal Paid
        {
            get { return this._paid; }
            set
            {
                if (this._paid != value)
                {
                    this.IsDirty = true;
                    this._paid = value;
                    OnPropertyChanged(() => Paid);
                    PropertyChangedCompleted(() => Paid);
                }
            }
        }

        protected decimal _change;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the Change</param>
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

        protected decimal _tip;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the Tip</param>
        /// </summary>
        public decimal Tip
        {
            get { return this._tip; }
            set
            {
                if (this._tip != value)
                {
                    this.IsDirty = true;
                    this._tip = value;
                    OnPropertyChanged(() => Tip);
                    PropertyChangedCompleted(() => Tip);
                }
            }
        }

        protected string _giftCardNo;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the GiftCardNo</param>
        /// </summary>
        public string GiftCardNo
        {
            get { return this._giftCardNo; }
            set
            {
                if (this._giftCardNo != value)
                {
                    this.IsDirty = true;
                    this._giftCardNo = value;
                    OnPropertyChanged(() => GiftCardNo);
                    PropertyChangedCompleted(() => GiftCardNo);
                }
            }
        }

        protected string _reason;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the Reason</param>
        /// </summary>
        public string Reason
        {
            get { return this._reason; }
            set
            {
                if (this._reason != value)
                {
                    this.IsDirty = true;
                    this._reason = value;
                    OnPropertyChanged(() => Reason);
                    PropertyChangedCompleted(() => Reason);
                }
            }
        }

        protected string _reference;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the Reference</param>
        /// </summary>
        public string Reference
        {
            get { return this._reference; }
            set
            {
                if (this._reference != value)
                {
                    this.IsDirty = true;
                    this._reference = value;
                    OnPropertyChanged(() => Reference);
                    PropertyChangedCompleted(() => Reference);
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
                this.base_ResourcePaymentDetail.Id = this.Id;
            if (this.PaymentType != null)
                this.base_ResourcePaymentDetail.PaymentType = this.PaymentType.Trim();
            this.base_ResourcePaymentDetail.ResourcePaymentId = this.ResourcePaymentId;
            this.base_ResourcePaymentDetail.PaymentMethodId = this.PaymentMethodId;
            if (this.PaymentMethod != null)
                this.base_ResourcePaymentDetail.PaymentMethod = this.PaymentMethod.Trim();
            this.base_ResourcePaymentDetail.CardType = this.CardType;
            this.base_ResourcePaymentDetail.Paid = this.Paid;
            this.base_ResourcePaymentDetail.Change = this.Change;
            this.base_ResourcePaymentDetail.Tip = this.Tip;
            if (this.GiftCardNo != null)
                this.base_ResourcePaymentDetail.GiftCardNo = this.GiftCardNo.Trim();
            if (this.Reason != null)
                this.base_ResourcePaymentDetail.Reason = this.Reason.Trim();
            if (this.Reference != null)
                this.base_ResourcePaymentDetail.Reference = this.Reference.Trim();
        }

        /// <summary>
        /// Public Method
        /// <param>Method for set Entity to PropertyModel</param>
        /// </summary
        public void ToModel()
        {
            this._id = this.base_ResourcePaymentDetail.Id;
            this._paymentType = this.base_ResourcePaymentDetail.PaymentType;
            this._resourcePaymentId = this.base_ResourcePaymentDetail.ResourcePaymentId;
            this._paymentMethodId = this.base_ResourcePaymentDetail.PaymentMethodId;
            this._paymentMethod = this.base_ResourcePaymentDetail.PaymentMethod;
            this._cardType = this.base_ResourcePaymentDetail.CardType;
            this._paid = this.base_ResourcePaymentDetail.Paid;
            this._change = this.base_ResourcePaymentDetail.Change;
            this._tip = this.base_ResourcePaymentDetail.Tip;
            this._giftCardNo = this.base_ResourcePaymentDetail.GiftCardNo;
            this._reason = this.base_ResourcePaymentDetail.Reason;
            this._reference = this.base_ResourcePaymentDetail.Reference;
        }

        /// <summary>
        /// Public Method
        /// <param>Method for set Entity to PropertyModel</param>
        /// </summary
        public void ToModelAndRaise()
        {
            this.Id = this.base_ResourcePaymentDetail.Id;
            this.PaymentType = this.base_ResourcePaymentDetail.PaymentType;
            this.ResourcePaymentId = this.base_ResourcePaymentDetail.ResourcePaymentId;
            this.PaymentMethodId = this.base_ResourcePaymentDetail.PaymentMethodId;
            this.PaymentMethod = this.base_ResourcePaymentDetail.PaymentMethod;
            this.CardType = this.base_ResourcePaymentDetail.CardType;
            this.Paid = this.base_ResourcePaymentDetail.Paid;
            this.Change = this.base_ResourcePaymentDetail.Change;
            this.Tip = this.base_ResourcePaymentDetail.Tip;
            this.GiftCardNo = this.base_ResourcePaymentDetail.GiftCardNo;
            this.Reason = this.base_ResourcePaymentDetail.Reason;
            this.Reference = this.base_ResourcePaymentDetail.Reference;
        }

        #endregion

        #region Custom Code

        #region Properties
        #region PaymentCardCollection
        private CollectionBase<base_ResourcePaymentDetailModel> _paymentCardCollection;
        /// <summary>
        /// Gets or sets the PaymentCardCollection.
        /// </summary>
        public CollectionBase<base_ResourcePaymentDetailModel> PaymentCardCollection
        {
            get { return _paymentCardCollection; }
            set
            {
                if (_paymentCardCollection != value)
                {
                    _paymentCardCollection = value;
                    OnPropertyChanged(() => PaymentCardCollection);
                }
            }
        }
        #endregion

        #region CardName
        private string _cardName;
        /// <summary>
        /// Gets or sets the CardName.
        /// </summary>
        public string CardName
        {
            get { return _cardName; }
            set
            {
                if (_cardName != value)
                {
                    _cardName = value;
                    OnPropertyChanged(() => CardName);
                }
            }
        }
        #endregion

        #region PaymentCardId
        private int _paymentCardId;
        /// <summary>
        /// Gets or sets the PaymentCardId.
        /// </summary>
        public int PaymentCardId
        {
            get { return _paymentCardId; }
            set
            {
                if (_paymentCardId != value)
                {
                    _paymentCardId = value;
                    OnPropertyChanged(() => PaymentCardId);
                }
            }
        }
        #endregion

        #region EnableRow
        private bool _enableRow = true;
        /// <summary>
        /// Gets or sets the EnableRow.
        /// </summary>
        public bool EnableRow
        {
            get { return _enableRow; }
            set
            {
                if (_enableRow != value)
                {
                    _enableRow = value;
                    OnPropertyChanged(() => EnableRow);
                }
            }
        }
        #endregion

        #region IsCreditCard
        private bool _isCreditCard;
        /// <summary>
        /// Gets or sets the IsCreditCard.
        /// <para>Show Tip when creditcard</para>
        /// </summary>
        public bool IsCreditCard
        {
            get { return _isCreditCard; }
            set
            {
                if (_isCreditCard != value)
                {
                    _isCreditCard = value;
                    OnPropertyChanged(() => IsCreditCard);
                }
            }
        }
        #endregion

        /// <summary>
        /// Flag using for Calc TotalPaid 
        /// <para>not calc Total Paid when isCard = true</para>
        /// </summary>
        public bool IsCard { get; set; }

        #region IsPaymentCardMethod
        private bool _isPaymentCardMethod;
        /// <summary>
        /// Gets or sets the IsPaymentCardMethod.
        /// <para>Flag to set amount pay is not show text.when payment card or Gift card</para>
        /// </summary>
        public bool IsPaymentCardMethod
        {
            get { return _isPaymentCardMethod; }
            set
            {
                if (_isPaymentCardMethod != value)
                {
                    _isPaymentCardMethod = value;
                    OnPropertyChanged(() => IsPaymentCardMethod);
                }
            }
        }
        #endregion

        #region IsValid
        private bool _isValid = true;
        /// <summary>
        /// Gets or sets the IsValid.
        /// </summary>
        public bool IsValid
        {
            get { return _isValid; }
            set
            {
                if (_isValid != value)
                {
                    _isValid = value;
                    OnPropertyChanged(() => IsValid);
                }
            }
        }
        #endregion

        #region CouponCardModel
        private base_CardManagementModel _couponCardModel;
        /// <summary>
        /// Gets or sets the CouponCardModel.
        /// </summary>
        public base_CardManagementModel CouponCardModel
        {
            get { return _couponCardModel; }
            set
            {
                if (_couponCardModel != value)
                {
                    _couponCardModel = value;
                    OnPropertyChanged(() => CouponCardModel);
                }
            }
        }
        #endregion

        #region IsCardValid
        private bool _isCardValid = true;
        /// <summary>
        /// Gets or sets the IsCardValid.
        /// </summary>
        public bool IsCardValid
        {
            get { return _isCardValid; }
            set
            {
                if (_isCardValid != value)
                {
                    _isCardValid = value;
                    OnPropertyChanged(() => IsCardValid);
                }
            }
        }
        #endregion

        #region Count

        private long _count;
        public long Count
        {
            get
            {
                return _count;
            }
            set
            {
                if (_count != value)
                {
                    _isDirty = true;
                    _count = value;
                    OnPropertyChanged(() => Count);
                }
            }
        }

        #endregion

        #region ErrorInfo
        private string _errorInfo;
        /// <summary>
        /// Gets or sets the ErrorInfo.
        /// </summary>
        public string ErrorInfo
        {
            get { return _errorInfo; }
            set
            {
                if (_errorInfo != value)
                {
                    _errorInfo = value;
                    OnPropertyChanged(() => ErrorInfo);
                }
            }
        }
        #endregion

        #region DateCreated

        public DateTime? DateCreated
        {
            get
            {
                if (base_ResourcePaymentDetail != null && base_ResourcePaymentDetail.base_ResourcePayment != null)
                {
                    return base_ResourcePaymentDetail.base_ResourcePayment.DateCreated;
                }

                return null;
            }
        }

        #endregion

        #endregion

        #region Methods

        public void NewResourcePaymentDetailEntity()
        {
            base_ResourcePaymentDetail = new base_ResourcePaymentDetail();
        }

        #endregion

        #endregion

        #region IDataErrorInfo Members
        /// <summary>
        /// Gets or sets the IsError.
        /// </summary>
        public bool IsError
        {
            get { return !string.IsNullOrWhiteSpace(Error); }

        }

        public string Error
        {
            get
            {
                List<string> errors = new List<string>();
                PropertyDescriptorCollection props = TypeDescriptor.GetProperties(this);
                foreach (PropertyDescriptor prop in props)
                {
                    string msg = this[prop.Name];
                    if (!string.IsNullOrWhiteSpace(msg))
                    {
                        errors.Add(msg);
                    }
                }

                return string.Join(Environment.NewLine, errors);
            }
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
                    case "PayType":
                        break;
                    //case "Reference":
                    //    if (CardType > 0 && (PaymentMethodId.Equals((short)CPC.POS.PaymentMethod.CreditCard) || PaymentMethodId.Equals((short)CPC.POS.PaymentMethod.GiftCard)))//Credit Card or Gift Card
                    //        if (Paid>0 && string.IsNullOrWhiteSpace(Reference))
                    //            message = "Card Number is required !";

                    //    break;
                    //case "IsCardValid":
                    //    if (!this.IsCardValid && !string.IsNullOrWhiteSpace(Reference) && (PaymentMethodId.Equals((short)CPC.POS.PaymentMethod.GiftCard) || PaymentMethodId.Equals((short)CPC.POS.PaymentMethod.GiftCertificate)))//Using Only for GiftCard or Certification Card
                    //        message = "Card is not valid";
                    //    break;
                    //case "IsValid":
                    //    if (!this.IsValid && (PaymentMethodId.Equals((short)CPC.POS.PaymentMethod.GiftCard) || PaymentMethodId.Equals((short)CPC.POS.PaymentMethod.GiftCertificate)))
                    //        message = "Paid is not greater than remaining total";
                    //    break;

                    case "ErrorInfo":
                        if (!string.IsNullOrWhiteSpace(ErrorInfo) && Paid > 0)
                            message = ErrorInfo;
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
