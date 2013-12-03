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
using CPCToolkitExtLibraries;
using System.Text;

namespace CPC.POS.Model
{
    /// <summary>
    /// Model for table base_GuestAddress
    /// </summary>
    [Serializable]
    public partial class base_GuestAddressModel : ModelBase, IDataErrorInfo
    {
        #region Constructor

        // Default constructor
        public base_GuestAddressModel()
        {
            this.IsNew = true;
            this.base_GuestAddress = new base_GuestAddress();
        }

        // Default constructor that set entity to field
        public base_GuestAddressModel(base_GuestAddress base_guestaddress, bool isRaiseProperties = false)
        {
            this.base_GuestAddress = base_guestaddress;
            if (!isRaiseProperties)
                this.ToModel();
            else
                this.ToModelAndRaise();
            this.IsDirty = false;
        }

        #endregion

        #region Entity Properties

        public base_GuestAddress base_GuestAddress { get; private set; }

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

        protected long _guestId;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the GuestId</param>
        /// </summary>
        public long GuestId
        {
            get { return this._guestId; }
            set
            {
                if (this._guestId != value)
                {
                    this.IsDirty = true;
                    this._guestId = value;
                    OnPropertyChanged(() => GuestId);
                    PropertyChangedCompleted(() => GuestId);
                }
            }
        }

        protected int _addressTypeId;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the AddressTypeId</param>
        /// </summary>
        public int AddressTypeId
        {
            get { return this._addressTypeId; }
            set
            {
                if (this._addressTypeId != value)
                {
                    this.IsDirty = true;
                    this._addressTypeId = value;
                    OnPropertyChanged(() => AddressTypeId);
                    PropertyChangedCompleted(() => AddressTypeId);
                }
            }
        }

        protected string _addressLine1;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the AddressLine1</param>
        /// </summary>
        public string AddressLine1
        {
            get { return this._addressLine1; }
            set
            {
                if (this._addressLine1 != value)
                {
                    this.IsDirty = true;
                    this._addressLine1 = value;
                    OnPropertyChanged(() => AddressLine1);
                    PropertyChangedCompleted(() => AddressLine1);
                }
            }
        }

        protected string _addressLine2;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the AddressLine2</param>
        /// </summary>
        public string AddressLine2
        {
            get { return this._addressLine2; }
            set
            {
                if (this._addressLine2 != value)
                {
                    this.IsDirty = true;
                    this._addressLine2 = value;
                    OnPropertyChanged(() => AddressLine2);
                    PropertyChangedCompleted(() => AddressLine2);
                }
            }
        }

        protected string _city;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the City</param>
        /// </summary>
        public string City
        {
            get { return this._city; }
            set
            {
                if (this._city != value)
                {
                    this.IsDirty = true;
                    this._city = value;
                    OnPropertyChanged(() => City);
                    PropertyChangedCompleted(() => City);
                }
            }
        }

        protected int _stateProvinceId;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the StateProvinceId</param>
        /// </summary>
        public int StateProvinceId
        {
            get { return this._stateProvinceId; }
            set
            {
                if (this._stateProvinceId != value)
                {
                    this.IsDirty = true;
                    this._stateProvinceId = value;
                    OnPropertyChanged(() => StateProvinceId);
                    PropertyChangedCompleted(() => StateProvinceId);
                }
            }
        }

        protected string _postalCode;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the PostalCode</param>
        /// </summary>
        public string PostalCode
        {
            get { return this._postalCode; }
            set
            {
                if (this._postalCode != value)
                {
                    this.IsDirty = true;
                    this._postalCode = value;
                    OnPropertyChanged(() => PostalCode);
                    PropertyChangedCompleted(() => PostalCode);
                }
            }
        }

        protected int _countryId;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the CountryId</param>
        /// </summary>
        public int CountryId
        {
            get { return this._countryId; }
            set
            {
                if (this._countryId != value)
                {
                    this.IsDirty = true;
                    this._countryId = value;
                    OnPropertyChanged(() => CountryId);
                    PropertyChangedCompleted(() => CountryId);
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

        protected System.DateTime _dateUpdated;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the DateUpdated</param>
        /// </summary>
        public System.DateTime DateUpdated
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
        /// <param>Gets or sets the UserUpdated</param>
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

        protected bool _isDefault;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the IsDefault</param>
        /// </summary>
        public bool IsDefault
        {
            get { return this._isDefault; }
            set
            {
                if (this._isDefault != value)
                {
                    this.IsDirty = true;
                    this._isDefault = value;
                    OnPropertyChanged(() => IsDefault);
                    PropertyChangedCompleted(() => IsDefault);
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
                this.base_GuestAddress.Id = this.Id;
            this.base_GuestAddress.GuestId = this.GuestId;
            this.base_GuestAddress.AddressTypeId = this.AddressTypeId;
            if (this.AddressLine1 != null)
                this.base_GuestAddress.AddressLine1 = this.AddressLine1.Trim();
            if (this.AddressLine2 != null)
                this.base_GuestAddress.AddressLine2 = this.AddressLine2.Trim();
            if (this.City != null)
                this.base_GuestAddress.City = this.City.Trim();
            this.base_GuestAddress.StateProvinceId = this.StateProvinceId;
            if (this.PostalCode != null)
                this.base_GuestAddress.PostalCode = this.PostalCode.Trim();
            this.base_GuestAddress.CountryId = this.CountryId;
            this.base_GuestAddress.DateCreated = this.DateCreated;
            if (this.UserCreated != null)
                this.base_GuestAddress.UserCreated = this.UserCreated.Trim();
            this.base_GuestAddress.DateUpdated = this.DateUpdated;
            if (this.UserUpdated != null)
                this.base_GuestAddress.UserUpdated = this.UserUpdated.Trim();
            this.base_GuestAddress.IsDefault = this.IsDefault;
        }

        /// <summary>
        /// Public Method
        /// <param>Method for set Entity to PropertyModel</param>
        /// </summary
        public void ToModel()
        {
            this._id = this.base_GuestAddress.Id;
            this._guestId = this.base_GuestAddress.GuestId;
            this._addressTypeId = this.base_GuestAddress.AddressTypeId;
            this._addressLine1 = this.base_GuestAddress.AddressLine1;
            this._addressLine2 = this.base_GuestAddress.AddressLine2;
            this._city = this.base_GuestAddress.City;
            this._stateProvinceId = this.base_GuestAddress.StateProvinceId;
            this._postalCode = this.base_GuestAddress.PostalCode;
            this._countryId = this.base_GuestAddress.CountryId;
            this._dateCreated = this.base_GuestAddress.DateCreated;
            this._userCreated = this.base_GuestAddress.UserCreated;
            this._dateUpdated = this.base_GuestAddress.DateUpdated;
            this._userUpdated = this.base_GuestAddress.UserUpdated;
            this._isDefault = this.base_GuestAddress.IsDefault;
        }

        /// <summary>
        /// Public Method
        /// <param>Method for set Entity to PropertyModel</param>
        /// </summary
        public void ToModelAndRaise()
        {
            this.Id = this.base_GuestAddress.Id;
            this.GuestId = this.base_GuestAddress.GuestId;
            this.AddressTypeId = this.base_GuestAddress.AddressTypeId;
            this.AddressLine1 = this.base_GuestAddress.AddressLine1;
            this.AddressLine2 = this.base_GuestAddress.AddressLine2;
            this.City = this.base_GuestAddress.City;
            this.StateProvinceId = this.base_GuestAddress.StateProvinceId;
            this.PostalCode = this.base_GuestAddress.PostalCode;
            this.CountryId = this.base_GuestAddress.CountryId;
            this.DateCreated = this.base_GuestAddress.DateCreated;
            this.UserCreated = this.base_GuestAddress.UserCreated;
            this.DateUpdated = this.base_GuestAddress.DateUpdated;
            this.UserUpdated = this.base_GuestAddress.UserUpdated;
            this.IsDefault = this.base_GuestAddress.IsDefault;
        }

        #endregion

        #region Custom Code

        #region Properties
        #region Text
        //private string _text;
        /// <summary>
        /// Gets the Text.
        /// </summary>
        public string Text
        {
            get
            {
                return ToText();
            }
        }
        #endregion
        #endregion

        #region Methods

        public void ToModel(AddressControlModel addressControlModel)
        {
            this.AddressTypeId = addressControlModel.AddressTypeID;
            this.AddressLine1 = addressControlModel.AddressLine1;
            this.AddressLine2 = addressControlModel.AddressLine2;
            this.City = addressControlModel.City;
            this.StateProvinceId = addressControlModel.StateProvinceID;
            this.PostalCode = addressControlModel.PostalCode;
            this.CountryId = addressControlModel.CountryID;
            this.IsDefault = addressControlModel.IsDefault;
            if (addressControlModel.AddressLine1 != null && string.IsNullOrEmpty(addressControlModel.City))
            {
                addressControlModel.AddressLine1 = this.AddressLine1;
                addressControlModel.City = this.City;
            }
            addressControlModel.IsDirty = false;
        }

        public AddressControlModel ToAddressControlModel()
        {
            AddressControlModel addressControlModel = new AddressControlModel();
            addressControlModel.AddressTypeID = this.AddressTypeId;
            addressControlModel.AddressLine1 = this.AddressLine1;
            addressControlModel.AddressLine2 = this.AddressLine2;
            addressControlModel.City = this.City;
            addressControlModel.StateProvinceID = (short)this.StateProvinceId;
            addressControlModel.PostalCode = this.PostalCode;
            addressControlModel.CountryID = (short)this.CountryId;
            addressControlModel.IsDefault = this.IsDefault;
            return addressControlModel;
        }

        private string ToText()
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(AddressLine1))
                sb.Append(AddressLine1);

            if (!string.IsNullOrWhiteSpace(City))
            {
                sb.Append(Environment.NewLine);
                sb.Append(City);
            }
            if (StateProvinceId > 0 && Common.States.Any(x => x.Value == StateProvinceId))
            {
                sb.Append(", ");
                sb.Append(Common.States.SingleOrDefault(x => x.Value == StateProvinceId).Text);
            }
            if (!string.IsNullOrWhiteSpace(PostalCode))
            {
                sb.Append(" ");
                sb.Append(PostalCode);
            }
            if (!string.IsNullOrWhiteSpace(AddressLine2))
            {
                sb.Append(Environment.NewLine);
                sb.Append(AddressLine2);
            }
            if (CountryId > 0 && Common.Countries.Any(x => x.Value == CountryId))
            {
                sb.Append(Environment.NewLine);
                sb.Append(Common.Countries.SingleOrDefault(x => x.Value == CountryId).Text);
            }
            return sb.ToString();
        }

        public void CreateGuestAddress()
        {
            this.base_GuestAddress = new base_GuestAddress();
        }
        #endregion

        #region Override Methods
        protected override void PropertyChangedCompleted(string propertyName)
        {

            //switch (propertyName)
            //{
            //    case "CountryId":
            //        OnPropertyChanged(() => StateProvinceId);
            //        OnPropertyChanged(() => PostalCode);
            //        break;
            //}
        }
        #endregion


        #endregion

        #region IDataErrorInfo Members

        #region HasError

        public bool HasError
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Error);
            }
        }

        #endregion
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
                    case "GuestId":
                        break;
                    case "AddressTypeId":
                        break;
                    case "AddressLine1":
                        if (string.IsNullOrWhiteSpace(this.AddressLine1))
                            message = "AddressLine is required";
                        break;
                    case "AddressLine2":
                        break;
                    case "City":
                        if (string.IsNullOrWhiteSpace(this.City))
                            message = "City is required";
                        break;
                    case "StateProvinceId":
                        if (CountryId > 0)
                        {
                            ComboItem country = Common.Countries.SingleOrDefault(x => Convert.ToInt32(x.Value) == CountryId);
                            if (country != null && country.HasState && StateProvinceId == 0)
                            {
                                message = "State is required";
                            }
                        }
                        break;
                    case "PostalCode":
                        if (CountryId > 0)
                        {
                            ComboItem country = Common.Countries.SingleOrDefault(x => Convert.ToInt32(x.Value) == CountryId);
                            if (country != null && country.HasState && string.IsNullOrWhiteSpace(PostalCode))
                                message = "Zip is required";
                        }
                        break;
                    case "CountryId":
                        if (CountryId == 0)
                            message = "Country is required";
                        else
                        {
                            OnPropertyChanged(() => StateProvinceId);
                            OnPropertyChanged(() => PostalCode);
                        }
                        break;
                    case "DateCreated":
                        break;
                    case "UserCreated":
                        break;
                    case "DateUpdated":
                        break;
                    case "UserUpdated":
                        break;
                    case "IsDefault":
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
