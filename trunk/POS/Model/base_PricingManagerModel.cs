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
using System.Windows;

namespace CPC.POS.Model
{
    /// <summary>
    /// Model for table base_PricingManager
    /// </summary>
    [Serializable]
    public partial class base_PricingManagerModel : ModelBase, IDataErrorInfo
    {
        #region Constructor

        // Default constructor
        public base_PricingManagerModel()
        {
            this.IsNew = true;
            this.base_PricingManager = new base_PricingManager();
        }

        // Default constructor that set entity to field
        public base_PricingManagerModel(base_PricingManager base_pricingmanager, bool isRaiseProperties = false)
        {
            this.base_PricingManager = base_pricingmanager;
            if (!isRaiseProperties)
                this.ToModel();
            else
                this.ToModelAndRaise();
            this.IsDirty = false;
        }

        #endregion

        #region Entity Properties

        public base_PricingManager base_PricingManager { get; private set; }

        #endregion

        #region Primitive Properties

        protected int _id;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the Id</param>
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
        /// <param>Gets or sets the Name</param>
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

        protected Nullable<decimal> _description;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the Description</param>
        /// </summary>
        public Nullable<decimal> Description
        {
            get { return this._description; }
            set
            {
                if (this._description != value)
                {
                    this.IsDirty = true;
                    this._description = value;
                    OnPropertyChanged(() => Description);
                    PropertyChangedCompleted(() => Description);
                }
            }
        }

        protected Nullable<System.DateTime> _dateCreated;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the DateCreated</param>
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

        protected Nullable<System.DateTime> _dateApplied;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the DateApplied</param>
        /// </summary>
        public Nullable<System.DateTime> DateApplied
        {
            get { return this._dateApplied; }
            set
            {
                if (this._dateApplied != value)
                {
                    this.IsDirty = true;
                    this._dateApplied = value;
                    OnPropertyChanged(() => DateApplied);
                    PropertyChangedCompleted(() => DateApplied);
                }
            }
        }

        protected string _userApplied;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the UserApplied</param>
        /// </summary>
        public string UserApplied
        {
            get { return this._userApplied; }
            set
            {
                if (this._userApplied != value)
                {
                    this.IsDirty = true;
                    this._userApplied = value;
                    OnPropertyChanged(() => UserApplied);
                    PropertyChangedCompleted(() => UserApplied);
                }
            }
        }

        protected Nullable<System.DateTime> _dateRestored;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the DateRestored</param>
        /// </summary>
        public Nullable<System.DateTime> DateRestored
        {
            get { return this._dateRestored; }
            set
            {
                if (this._dateRestored != value)
                {
                    this.IsDirty = true;
                    this._dateRestored = value;
                    OnPropertyChanged(() => DateRestored);
                    PropertyChangedCompleted(() => DateRestored);
                }
            }
        }

        protected string _userRestored;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the UserRestored</param>
        /// </summary>
        public string UserRestored
        {
            get { return this._userRestored; }
            set
            {
                if (this._userRestored != value)
                {
                    this.IsDirty = true;
                    this._userRestored = value;
                    OnPropertyChanged(() => UserRestored);
                    PropertyChangedCompleted(() => UserRestored);
                }
            }
        }

        protected short _affectPricing;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the AffectPricing</param>
        /// </summary>
        public short AffectPricing
        {
            get { return this._affectPricing; }
            set
            {
                if (this._affectPricing != value)
                {
                    this.IsDirty = true;
                    this._affectPricing = value;
                    OnPropertyChanged(() => AffectPricing);
                    PropertyChangedCompleted(() => AffectPricing);
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

        protected string _priceLevel;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the PriceLevel</param>
        /// </summary>
        public string PriceLevel
        {
            get { return this._priceLevel; }
            set
            {
                if (this._priceLevel != value)
                {
                    this.IsDirty = true;
                    this._priceLevel = value;
                    OnPropertyChanged(() => PriceLevel);
                    PropertyChangedCompleted(() => PriceLevel);
                }
            }
        }

        protected string _status;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the Status</param>
        /// </summary>
        public string Status
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

        protected short _basePrice;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the BasePrice</param>
        /// </summary>
        public short BasePrice
        {
            get { return this._basePrice; }
            set
            {
                if (this._basePrice != value)
                {
                    this.IsDirty = true;
                    this._basePrice = value;
                    OnPropertyChanged(() => BasePrice);
                    PropertyChangedCompleted(() => BasePrice);
                }
            }
        }

        protected Nullable<short> _calculateMethod;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the CalculateMethod</param>
        /// </summary>
        public Nullable<short> CalculateMethod
        {
            get { return this._calculateMethod; }
            set
            {
                if (this._calculateMethod != value)
                {
                    this.IsDirty = true;
                    this._calculateMethod = value;
                    OnPropertyChanged(() => CalculateMethod);
                    PropertyChangedCompleted(() => CalculateMethod);
                }
            }
        }

        protected Nullable<decimal> _amountChange;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the AmountChange</param>
        /// </summary>
        public Nullable<decimal> AmountChange
        {
            get { return this._amountChange; }
            set
            {
                if (this._amountChange != value)
                {
                    this.IsDirty = true;
                    this._amountChange = value;
                    OnPropertyChanged(() => AmountChange);
                    PropertyChangedCompleted(() => AmountChange);
                }
            }
        }

        protected Nullable<short> _amountUnit;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the AmountUnit</param>
        /// </summary>
        public Nullable<short> AmountUnit
        {
            get { return this._amountUnit; }
            set
            {
                if (this._amountUnit != value)
                {
                    this.IsDirty = true;
                    this._amountUnit = value;
                    OnPropertyChanged(() => AmountUnit);
                    PropertyChangedCompleted(() => AmountUnit);
                }
            }
        }

        protected Nullable<int> _itemCount;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the ItemCount</param>
        /// </summary>
        public Nullable<int> ItemCount
        {
            get { return this._itemCount; }
            set
            {
                if (this._itemCount != value)
                {
                    this.IsDirty = true;
                    this._itemCount = value;
                    OnPropertyChanged(() => ItemCount);
                    PropertyChangedCompleted(() => ItemCount);
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
                this.base_PricingManager.Id = this.Id;
            if (this.Name != null)
                this.base_PricingManager.Name = this.Name.Trim();
            this.base_PricingManager.Description = this.Description;
            this.base_PricingManager.DateCreated = this.DateCreated;
            if (this.UserCreated != null)
                this.base_PricingManager.UserCreated = this.UserCreated.Trim();
            this.base_PricingManager.DateApplied = this.DateApplied;
            if (this.UserApplied != null)
                this.base_PricingManager.UserApplied = this.UserApplied.Trim();
            this.base_PricingManager.DateRestored = this.DateRestored;
            if (this.UserRestored != null)
                this.base_PricingManager.UserRestored = this.UserRestored.Trim();
            this.base_PricingManager.AffectPricing = this.AffectPricing;
            this.base_PricingManager.Resource = this.Resource;
            if (this.PriceLevel != null)
                this.base_PricingManager.PriceLevel = this.PriceLevel.Trim();
            if (this.Status != null)
                this.base_PricingManager.Status = this.Status.Trim();
            this.base_PricingManager.BasePrice = this.BasePrice;
            this.base_PricingManager.CalculateMethod = this.CalculateMethod;
            this.base_PricingManager.AmountChange = this.AmountChange;
            this.base_PricingManager.AmountUnit = this.AmountUnit;
            this.base_PricingManager.ItemCount = this.ItemCount;
            if (this.Reason != null)
                this.base_PricingManager.Reason = this.Reason.Trim();
            if (this.Shift != null)
                this.base_PricingManager.Shift = this.Shift.Trim();
        }

        /// <summary>
        /// Public Method
        /// <param>Method for set Entity to PropertyModel</param>
        /// </summary
        public void ToModel()
        {
            this._id = this.base_PricingManager.Id;
            this._name = this.base_PricingManager.Name;
            this._description = this.base_PricingManager.Description;
            this._dateCreated = this.base_PricingManager.DateCreated;
            this._userCreated = this.base_PricingManager.UserCreated;
            this._dateApplied = this.base_PricingManager.DateApplied;
            this._userApplied = this.base_PricingManager.UserApplied;
            this._dateRestored = this.base_PricingManager.DateRestored;
            this._userRestored = this.base_PricingManager.UserRestored;
            this._affectPricing = this.base_PricingManager.AffectPricing;
            this._resource = this.base_PricingManager.Resource;
            this._priceLevel = this.base_PricingManager.PriceLevel;
            this._status = this.base_PricingManager.Status;
            this._basePrice = this.base_PricingManager.BasePrice;
            this._calculateMethod = this.base_PricingManager.CalculateMethod;
            this._amountChange = this.base_PricingManager.AmountChange;
            this._amountUnit = this.base_PricingManager.AmountUnit;
            this._itemCount = this.base_PricingManager.ItemCount;
            this._reason = this.base_PricingManager.Reason;
            this._shift = this.base_PricingManager.Shift;
        }

        /// <summary>
        /// Public Method
        /// <param>Method for set Entity to PropertyModel</param>
        /// </summary
        public void ToModelAndRaise()
        {
            this.Id = this.base_PricingManager.Id;
            this.Name = this.base_PricingManager.Name;
            this.Description = this.base_PricingManager.Description;
            this.DateCreated = this.base_PricingManager.DateCreated;
            this.UserCreated = this.base_PricingManager.UserCreated;
            this.DateApplied = this.base_PricingManager.DateApplied;
            this.UserApplied = this.base_PricingManager.UserApplied;
            this.DateRestored = this.base_PricingManager.DateRestored;
            this.UserRestored = this.base_PricingManager.UserRestored;
            this.AffectPricing = this.base_PricingManager.AffectPricing;
            this.Resource = this.base_PricingManager.Resource;
            this.PriceLevel = this.base_PricingManager.PriceLevel;
            this.Status = this.base_PricingManager.Status;
            this.BasePrice = this.base_PricingManager.BasePrice;
            this.CalculateMethod = this.base_PricingManager.CalculateMethod;
            this.AmountChange = this.base_PricingManager.AmountChange;
            this.AmountUnit = this.base_PricingManager.AmountUnit;
            this.ItemCount = this.base_PricingManager.ItemCount;
            this.Reason = this.base_PricingManager.Reason;
            this.Shift = this.base_PricingManager.Shift;
        }

        #endregion

        #region Custom Code

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

        #region CtorExt
        public base_PricingManagerModel(base_PricingManager base_pricingmanager)
        {
            this.IsLoad = true;
            this.base_PricingManager = base_pricingmanager;
            this.ToModel();
            this.IsDirty = false;
            this.IsLoad = false;
        }
        #endregion

        #region ProductCollection
        /// <summary>
        /// Gets or sets the ProductCollection.
        /// </summary>
        private ObservableCollection<base_ProductModel> _productCollection = new ObservableCollection<base_ProductModel>();
        public ObservableCollection<base_ProductModel> ProductCollection
        {
            get
            {
                return _productCollection;
            }
            set
            {
                if (_productCollection != value)
                {
                    _productCollection = value;
                    OnPropertyChanged(() => ProductCollection);
                }
            }
        }

        #endregion

        #region ResourceNoteCollection
        private CollectionBase<base_ResourceNoteModel> _resourceNoteCollection;
        /// <summary>
        /// Gets or sets the ResourceNoteCollection.
        /// </summary>
        public CollectionBase<base_ResourceNoteModel> ResourceNoteCollection
        {
            get
            {
                return _resourceNoteCollection;
            }
            set
            {
                if (_resourceNoteCollection != value)
                {
                    _resourceNoteCollection = value;
                    OnPropertyChanged(() => ResourceNoteCollection);
                }
            }
        }
        #endregion

        #region IsErrorProductCollection
        protected bool _isErrorProductCollection;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the Id</para>
        /// </summary>
        public bool IsErrorProductCollection
        {
            get { return this._isErrorProductCollection; }
            set
            {
                this._isErrorProductCollection = value;
                OnPropertyChanged(() => IsErrorProductCollection);
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

        #region VisibilityApplied
        protected Visibility _visibilityApplied = Visibility.Visible;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the Id</para>
        /// </summary>
        public Visibility VisibilityApplied
        {
            get { return this._visibilityApplied; }
            set
            {
                if (this._visibilityApplied != value)
                {
                    this._visibilityApplied = value;
                    OnPropertyChanged(() => VisibilityApplied);
                }
            }
        }
        #endregion

        #region VisibilityRestore
        protected Visibility _visibilityRestore = Visibility.Collapsed;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the Id</para>
        /// </summary>
        public Visibility VisibilityRestore
        {
            get { return this._visibilityRestore; }
            set
            {
                if (this._visibilityRestore != value)
                {
                    this._visibilityRestore = value;
                    OnPropertyChanged(() => VisibilityRestore);
                }
            }
        }
        #endregion

        #endregion

        #region IDataErrorInfo Members

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
                    case "Name":
                        if (string.IsNullOrWhiteSpace(this.Name))
                            message = "Pricing Name is required";
                        break;
                    case "IsErrorProductCollection":
                        if (this.IsErrorProductCollection && (this.ProductCollection == null || this.ProductCollection.Count == 0))
                            message = "ProductCollection is required";
                        break;
                    case "PriceLevel":
                        break;
                    case "DateCreated":
                        break;
                    case "UserCreated":
                        break;
                    case "DateApplied":
                        break;
                    case "UserApplied":
                        break;
                    case "DateRestored":
                        break;
                    case "UserRestored":
                        break;
                    case "Status":
                        break;
                    case "AffectPricing":
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