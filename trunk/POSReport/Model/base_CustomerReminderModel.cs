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
    /// Model for table base_CustomerReminder
    /// </summary>
    [Serializable]
    public partial class base_CustomerReminderModel : ModelBase, IDataErrorInfo
    {
        #region Constructor

        // Default constructor
        public base_CustomerReminderModel()
        {
            this.IsNew = true;
            this.base_CustomerReminder = new base_CustomerReminder();
        }

        // Default constructor that set entity to field
        public base_CustomerReminderModel(base_CustomerReminder base_customerreminder, bool isRaiseProperties = false)
        {
            this.base_CustomerReminder = base_customerreminder;
            if (!isRaiseProperties)
                this.ToModel();
            else
                this.ToModelAndRaise();
            this.IsDirty = false;
        }

        #endregion

        #region Entity Properties

        public base_CustomerReminder base_CustomerReminder { get; private set; }

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

        protected string _guestResource;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the GuestResource</para>
        /// </summary>
        public string GuestResource
        {
            get { return this._guestResource; }
            set
            {
                if (this._guestResource != value)
                {
                    this.IsDirty = true;
                    this._guestResource = value;
                    OnPropertyChanged(() => GuestResource);
                    PropertyChangedCompleted(() => GuestResource);
                }
            }
        }

        protected Nullable<short> _reminderTypeId;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the ReminderTypeId</para>
        /// </summary>
        public Nullable<short> ReminderTypeId
        {
            get { return this._reminderTypeId; }
            set
            {
                if (this._reminderTypeId != value)
                {
                    this.IsDirty = true;
                    this._reminderTypeId = value;
                    OnPropertyChanged(() => ReminderTypeId);
                    PropertyChangedCompleted(() => ReminderTypeId);
                }
            }
        }

        protected bool _isSend;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the IsSend</para>
        /// </summary>
        public bool IsSend
        {
            get { return this._isSend; }
            set
            {
                if (this._isSend != value)
                {
                    this.IsDirty = true;
                    this._isSend = value;
                    OnPropertyChanged(() => IsSend);
                    PropertyChangedCompleted(() => IsSend);
                }
            }
        }

        protected Nullable<System.DateTime> _dateSend;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the DateSend</para>
        /// </summary>
        public Nullable<System.DateTime> DateSend
        {
            get { return this._dateSend; }
            set
            {
                if (this._dateSend != value)
                {
                    this.IsDirty = true;
                    this._dateSend = value;
                    OnPropertyChanged(() => DateSend);
                    PropertyChangedCompleted(() => DateSend);
                }
            }
        }

        protected string _cardName;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the CardName</para>
        /// </summary>
        public string CardName
        {
            get { return this._cardName; }
            set
            {
                if (this._cardName != value)
                {
                    this.IsDirty = true;
                    this._cardName = value;
                    OnPropertyChanged(() => CardName);
                    PropertyChangedCompleted(() => CardName);
                }
            }
        }

        protected Nullable<System.DateTime> _dOB;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the DOB</para>
        /// </summary>
        public Nullable<System.DateTime> DOB
        {
            get { return this._dOB; }
            set
            {
                if (this._dOB != value)
                {
                    this.IsDirty = true;
                    this._dOB = value;
                    OnPropertyChanged(() => DOB);
                    PropertyChangedCompleted(() => DOB);
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

        protected string _company;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the Company</para>
        /// </summary>
        public string Company
        {
            get { return this._company; }
            set
            {
                if (this._company != value)
                {
                    this.IsDirty = true;
                    this._company = value;
                    OnPropertyChanged(() => Company);
                    PropertyChangedCompleted(() => Company);
                }
            }
        }

        protected string _phone;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the Phone</para>
        /// </summary>
        public string Phone
        {
            get { return this._phone; }
            set
            {
                if (this._phone != value)
                {
                    this.IsDirty = true;
                    this._phone = value;
                    OnPropertyChanged(() => Phone);
                    PropertyChangedCompleted(() => Phone);
                }
            }
        }

        protected string _email;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the Email</para>
        /// </summary>
        public string Email
        {
            get { return this._email; }
            set
            {
                if (this._email != value)
                {
                    this.IsDirty = true;
                    this._email = value;
                    OnPropertyChanged(() => Email);
                    PropertyChangedCompleted(() => Email);
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
                this.base_CustomerReminder.Id = this.Id;
            this.base_CustomerReminder.GuestResource = this.GuestResource;
            this.base_CustomerReminder.ReminderTypeId = this.ReminderTypeId;
            this.base_CustomerReminder.IsSend = this.IsSend;
            this.base_CustomerReminder.DateSend = this.DateSend;
            this.base_CustomerReminder.CardName = this.CardName;
            this.base_CustomerReminder.DOB = this.DOB;
            this.base_CustomerReminder.Name = this.Name;
            this.base_CustomerReminder.Company = this.Company;
            this.base_CustomerReminder.Phone = this.Phone;
            this.base_CustomerReminder.Email = this.Email;
        }

        /// <summary>
        /// Public Method
        /// <para>Method for set Entity to PropertyModel</para>
        /// </summary
        public void ToModel()
        {
            this._id = this.base_CustomerReminder.Id;
            this._guestResource = this.base_CustomerReminder.GuestResource;
            this._reminderTypeId = this.base_CustomerReminder.ReminderTypeId;
            this._isSend = this.base_CustomerReminder.IsSend;
            this._dateSend = this.base_CustomerReminder.DateSend;
            this._cardName = this.base_CustomerReminder.CardName;
            this._dOB = this.base_CustomerReminder.DOB;
            this._name = this.base_CustomerReminder.Name;
            this._company = this.base_CustomerReminder.Company;
            this._phone = this.base_CustomerReminder.Phone;
            this._email = this.base_CustomerReminder.Email;
        }

        /// <summary>
        /// Public Method
        /// <para>Method for set Entity to PropertyModel</para>
        /// </summary
        public void ToModelAndRaise()
        {
            this.Id = this.base_CustomerReminder.Id;
            this.GuestResource = this.base_CustomerReminder.GuestResource;
            this.ReminderTypeId = this.base_CustomerReminder.ReminderTypeId;
            this.IsSend = this.base_CustomerReminder.IsSend;
            this.DateSend = this.base_CustomerReminder.DateSend;
            this.CardName = this.base_CustomerReminder.CardName;
            this.DOB = this.base_CustomerReminder.DOB;
            this.Name = this.base_CustomerReminder.Name;
            this.Company = this.base_CustomerReminder.Company;
            this.Phone = this.base_CustomerReminder.Phone;
            this.Email = this.base_CustomerReminder.Email;
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
                    case "GuestResource":
                        break;
                    case "ReminderTypeId":
                        break;
                    case "IsSend":
                        break;
                    case "DateSend":
                        break;
                    case "CardName":
                        break;
                    case "DOB":
                        break;
                    case "Name":
                        break;
                    case "Company":
                        break;
                    case "Phone":
                        break;
                    case "Email":
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
