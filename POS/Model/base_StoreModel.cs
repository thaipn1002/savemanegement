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
    /// Model for table base_Store
    /// </summary>
    [Serializable]
    public partial class base_StoreModel : ModelBase, IDataErrorInfo
    {
        #region Constructor

        // Default constructor
        public base_StoreModel()
        {
            this.IsNew = true;
            this.base_Store = new base_Store();
        }

        // Default constructor that set entity to field
        public base_StoreModel(base_Store base_store, bool isRaiseProperties = false)
        {
            this.base_Store = base_store;
            if (!isRaiseProperties)
                this.ToModel();
            else
                this.ToModelAndRaise();
            this.IsDirty = false;
        }

        #endregion

        #region Entity Properties

        public base_Store base_Store { get; private set; }

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

        protected string _code;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the Code</param>
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

        protected string _street;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the Street</param>
        /// </summary>
        public string Street
        {
            get { return this._street; }
            set
            {
                if (this._street != value)
                {
                    this.IsDirty = true;
                    this._street = value;
                    OnPropertyChanged(() => Street);
                    PropertyChangedCompleted(() => Street);
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

        protected string _password;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the Password</param>
        /// </summary>
        public string Password
        {
            get { return this._password; }
            set
            {
                if (this._password != value)
                {
                    this.IsDirty = true;
                    this._password = value;
                    OnPropertyChanged(() => Password);
                    PropertyChangedCompleted(() => Password);
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
                this.base_Store.Id = this.Id;
            if (this.Code != null)
                this.base_Store.Code = this.Code.Trim();
            if (this.Name != null)
                this.base_Store.Name = this.Name.Trim();
            if (this.Street != null)
                this.base_Store.Street = this.Street.Trim();
            if (this.City != null)
                this.base_Store.City = this.City.Trim();
            if (this.Password != null)
                this.base_Store.Password = this.Password.Trim();
        }

        /// <summary>
        /// Public Method
        /// <param>Method for set Entity to PropertyModel</param>
        /// </summary
        public void ToModel()
        {
            this._id = this.base_Store.Id;
            this._code = this.base_Store.Code;
            this._name = this.base_Store.Name;
            this._street = this.base_Store.Street;
            this._city = this.base_Store.City;
            this._password = this.base_Store.Password;
        }

        /// <summary>
        /// Public Method
        /// <param>Method for set Entity to PropertyModel</param>
        /// </summary
        public void ToModelAndRaise()
        {
            this.Id = this.base_Store.Id;
            this.Code = this.base_Store.Code;
            this.Name = this.base_Store.Name;
            this.Street = this.base_Store.Street;
            this.City = this.base_Store.City;
            this.Password = this.base_Store.Password;
        }

        #endregion

        #region Custom Code

        #region Fields

        /// <summary>
        /// Holds backup of this object.
        /// </summary>
        private base_StoreModel _backup;

        #endregion

        #region Properties

        #endregion

        #region Methods

        #region ShallowClone

        /// <summary>
        /// Creates a shallow copy of this object.
        /// </summary>
        /// <returns>A shallow copy of this object.</returns>
        public base_StoreModel ShallowClone()
        {
            return (base_StoreModel)this.MemberwiseClone();
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
                string message = null;

                switch (columnName)
                {
                    case "Code":

                        if (string.IsNullOrWhiteSpace(_code))
                        {
                            message = "Code is required.";
                        }

                        break;
                }

                return message;
            }
        }

        #endregion

        #region IEditableObject Members

        public void BeginEdit()
        {
            if (_backup == null)
            {
                _backup = ShallowClone();
            }
        }

        public void CancelEdit()
        {
            if (_backup != null)
            {
                Id = _backup.Id;
                Code = _backup.Code;
                Name = _backup.Name;
                Street = _backup.Street;
                City = _backup.City;
                IsTemporary = _backup.IsTemporary;
                IsChecked = _backup.IsChecked;
                IsDeleted = _backup.IsDeleted;
                IsNew = _backup.IsNew;
                IsDirty = _backup.IsDirty;
                _backup = null;
            }
        }

        public void EndEdit()
        {
            if (_backup != null)
            {
                _backup = null;
            }
        }

        #endregion

        #endregion
    }
}
