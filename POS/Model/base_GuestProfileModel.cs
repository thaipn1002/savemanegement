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
    /// Model for table base_GuestProfile
    /// </summary>
    [Serializable]
    public partial class base_GuestProfileModel : ModelBase, IDataErrorInfo
    {
        #region Constructor

        // Default constructor
        public base_GuestProfileModel()
        {
            this.IsNew = true;
            this.base_GuestProfile = new base_GuestProfile();
        }

        // Default constructor that set entity to field
        public base_GuestProfileModel(base_GuestProfile base_guestprofile, bool isRaiseProperties = false)
        {
            this.base_GuestProfile = base_guestprofile;
            if (!isRaiseProperties)
                this.ToModel();
            else
                this.ToModelAndRaise();
            this.IsDirty = false;
        }

        #endregion

        #region Entity Properties

        public base_GuestProfile base_GuestProfile { get; private set; }

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

        protected Nullable<short> _gender;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the Gender</param>
        /// </summary>
        public Nullable<short> Gender
        {
            get { return this._gender; }
            set
            {
                if (this._gender != value)
                {
                    this.IsDirty = true;
                    this._gender = value;
                    OnPropertyChanged(() => Gender);
                    PropertyChangedCompleted(() => Gender);
                }
            }
        }

        protected Nullable<short> _marital;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the Marital</param>
        /// </summary>
        public Nullable<short> Marital
        {
            get { return this._marital; }
            set
            {
                if (this._marital != value)
                {
                    this.IsDirty = true;
                    this._marital = value;
                    OnPropertyChanged(() => Marital);
                    PropertyChangedCompleted(() => Marital);
                }
            }
        }

        protected string _sSN;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the SSN</param>
        /// </summary>
        public string SSN
        {
            get { return this._sSN; }
            set
            {
                if (this._sSN != value)
                {
                    this.IsDirty = true;
                    this._sSN = value;
                    OnPropertyChanged(() => SSN);
                    PropertyChangedCompleted(() => SSN);
                }
            }
        }

        protected string _identification;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the Identification</param>
        /// </summary>
        public string Identification
        {
            get { return this._identification; }
            set
            {
                if (this._identification != value)
                {
                    this.IsDirty = true;
                    this._identification = value;
                    OnPropertyChanged(() => Identification);
                    PropertyChangedCompleted(() => Identification);
                }
            }
        }

        protected Nullable<System.DateTime> _dOB;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the DOB</param>
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

        protected Nullable<bool> _isSpouse;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the IsSpouse</param>
        /// </summary>
        public Nullable<bool> IsSpouse
        {
            get { return this._isSpouse; }
            set
            {
                if (this._isSpouse != value)
                {
                    this.IsDirty = true;
                    this._isSpouse = value;
                    OnPropertyChanged(() => IsSpouse);
                    PropertyChangedCompleted(() => IsSpouse);
                }
            }
        }

        protected string _firstName;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the FirstName</param>
        /// </summary>
        public string FirstName
        {
            get { return this._firstName; }
            set
            {
                if (this._firstName != value)
                {
                    this.IsDirty = true;
                    this._firstName = value;
                    OnPropertyChanged(() => FirstName);
                    PropertyChangedCompleted(() => FirstName);
                }
            }
        }

        protected string _lastName;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the LastName</param>
        /// </summary>
        public string LastName
        {
            get { return this._lastName; }
            set
            {
                if (this._lastName != value)
                {
                    this.IsDirty = true;
                    this._lastName = value;
                    OnPropertyChanged(() => LastName);
                    PropertyChangedCompleted(() => LastName);
                }
            }
        }

        protected string _middleName;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the MiddleName</param>
        /// </summary>
        public string MiddleName
        {
            get { return this._middleName; }
            set
            {
                if (this._middleName != value)
                {
                    this.IsDirty = true;
                    this._middleName = value;
                    OnPropertyChanged(() => MiddleName);
                    PropertyChangedCompleted(() => MiddleName);
                }
            }
        }

        protected string _state;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the State</param>
        /// </summary>
        public string State
        {
            get { return this._state; }
            set
            {
                if (this._state != value)
                {
                    this.IsDirty = true;
                    this._state = value;
                    OnPropertyChanged(() => State);
                    PropertyChangedCompleted(() => State);
                }
            }
        }

        protected Nullable<short> _sGender;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the SGender</param>
        /// </summary>
        public Nullable<short> SGender
        {
            get { return this._sGender; }
            set
            {
                if (this._sGender != value)
                {
                    this.IsDirty = true;
                    this._sGender = value;
                    OnPropertyChanged(() => SGender);
                    PropertyChangedCompleted(() => SGender);
                }
            }
        }

        protected string _sFirstName;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the SFirstName</param>
        /// </summary>
        public string SFirstName
        {
            get { return this._sFirstName; }
            set
            {
                if (this._sFirstName != value)
                {
                    this.IsDirty = true;
                    this._sFirstName = value;
                    OnPropertyChanged(() => SFirstName);
                    PropertyChangedCompleted(() => SFirstName);
                }
            }
        }

        protected string _sLastName;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the SLastName</param>
        /// </summary>
        public string SLastName
        {
            get { return this._sLastName; }
            set
            {
                if (this._sLastName != value)
                {
                    this.IsDirty = true;
                    this._sLastName = value;
                    OnPropertyChanged(() => SLastName);
                    PropertyChangedCompleted(() => SLastName);
                }
            }
        }

        protected string _sMiddleName;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the SMiddleName</param>
        /// </summary>
        public string SMiddleName
        {
            get { return this._sMiddleName; }
            set
            {
                if (this._sMiddleName != value)
                {
                    this.IsDirty = true;
                    this._sMiddleName = value;
                    OnPropertyChanged(() => SMiddleName);
                    PropertyChangedCompleted(() => SMiddleName);
                }
            }
        }

        protected string _sPhone;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the SPhone</param>
        /// </summary>
        public string SPhone
        {
            get { return this._sPhone; }
            set
            {
                if (this._sPhone != value)
                {
                    this.IsDirty = true;
                    this._sPhone = value;
                    OnPropertyChanged(() => SPhone);
                    PropertyChangedCompleted(() => SPhone);
                }
            }
        }

        protected string _sCellPhone;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the SCellPhone</param>
        /// </summary>
        public string SCellPhone
        {
            get { return this._sCellPhone; }
            set
            {
                if (this._sCellPhone != value)
                {
                    this.IsDirty = true;
                    this._sCellPhone = value;
                    OnPropertyChanged(() => SCellPhone);
                    PropertyChangedCompleted(() => SCellPhone);
                }
            }
        }

        protected string _sSSN;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the SSSN</param>
        /// </summary>
        public string SSSN
        {
            get { return this._sSSN; }
            set
            {
                if (this._sSSN != value)
                {
                    this.IsDirty = true;
                    this._sSSN = value;
                    OnPropertyChanged(() => SSSN);
                    PropertyChangedCompleted(() => SSSN);
                }
            }
        }

        protected string _sState;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the SState</param>
        /// </summary>
        public string SState
        {
            get { return this._sState; }
            set
            {
                if (this._sState != value)
                {
                    this.IsDirty = true;
                    this._sState = value;
                    OnPropertyChanged(() => SState);
                    PropertyChangedCompleted(() => SState);
                }
            }
        }

        protected string _sEmail;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the SEmail</param>
        /// </summary>
        public string SEmail
        {
            get { return this._sEmail; }
            set
            {
                if (this._sEmail != value)
                {
                    this.IsDirty = true;
                    this._sEmail = value;
                    OnPropertyChanged(() => SEmail);
                    PropertyChangedCompleted(() => SEmail);
                }
            }
        }

        protected Nullable<bool> _isEmergency;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the IsEmergency</param>
        /// </summary>
        public Nullable<bool> IsEmergency
        {
            get { return this._isEmergency; }
            set
            {
                if (this._isEmergency != value)
                {
                    this.IsDirty = true;
                    this._isEmergency = value;
                    OnPropertyChanged(() => IsEmergency);
                    PropertyChangedCompleted(() => IsEmergency);
                }
            }
        }

        protected string _eFirstName;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the EFirstName</param>
        /// </summary>
        public string EFirstName
        {
            get { return this._eFirstName; }
            set
            {
                if (this._eFirstName != value)
                {
                    this.IsDirty = true;
                    this._eFirstName = value;
                    OnPropertyChanged(() => EFirstName);
                    PropertyChangedCompleted(() => EFirstName);
                }
            }
        }

        protected string _eLastName;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the ELastName</param>
        /// </summary>
        public string ELastName
        {
            get { return this._eLastName; }
            set
            {
                if (this._eLastName != value)
                {
                    this.IsDirty = true;
                    this._eLastName = value;
                    OnPropertyChanged(() => ELastName);
                    PropertyChangedCompleted(() => ELastName);
                }
            }
        }

        protected string _eMiddleName;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the EMiddleName</param>
        /// </summary>
        public string EMiddleName
        {
            get { return this._eMiddleName; }
            set
            {
                if (this._eMiddleName != value)
                {
                    this.IsDirty = true;
                    this._eMiddleName = value;
                    OnPropertyChanged(() => EMiddleName);
                    PropertyChangedCompleted(() => EMiddleName);
                }
            }
        }

        protected string _ePhone;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the EPhone</param>
        /// </summary>
        public string EPhone
        {
            get { return this._ePhone; }
            set
            {
                if (this._ePhone != value)
                {
                    this.IsDirty = true;
                    this._ePhone = value;
                    OnPropertyChanged(() => EPhone);
                    PropertyChangedCompleted(() => EPhone);
                }
            }
        }

        protected string _eCellPhone;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the ECellPhone</param>
        /// </summary>
        public string ECellPhone
        {
            get { return this._eCellPhone; }
            set
            {
                if (this._eCellPhone != value)
                {
                    this.IsDirty = true;
                    this._eCellPhone = value;
                    OnPropertyChanged(() => ECellPhone);
                    PropertyChangedCompleted(() => ECellPhone);
                }
            }
        }

        protected string _eRelationship;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the ERelationship</param>
        /// </summary>
        public string ERelationship
        {
            get { return this._eRelationship; }
            set
            {
                if (this._eRelationship != value)
                {
                    this.IsDirty = true;
                    this._eRelationship = value;
                    OnPropertyChanged(() => ERelationship);
                    PropertyChangedCompleted(() => ERelationship);
                }
            }
        }

        protected Nullable<long> _guestId;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the GuestId</param>
        /// </summary>
        public Nullable<long> GuestId
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

        protected string _sRelationShip;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the SRelationShip</param>
        /// </summary>
        public string SRelationShip
        {
            get { return this._sRelationShip; }
            set
            {
                if (this._sRelationShip != value)
                {
                    this.IsDirty = true;
                    this._sRelationShip = value;
                    OnPropertyChanged(() => SRelationShip);
                    PropertyChangedCompleted(() => SRelationShip);
                }
            }
        }

        protected string _sIM;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the SIM</param>
        /// </summary>
        public string SIM
        {
            get { return this._sIM; }
            set
            {
                if (this._sIM != value)
                {
                    this.IsDirty = true;
                    this._sIM = value;
                    OnPropertyChanged(() => SIM);
                    PropertyChangedCompleted(() => SIM);
                }
            }
        }

        protected Nullable<System.DateTime> _sDOB;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the SDOB</param>
        /// </summary>
        public Nullable<System.DateTime> SDOB
        {
            get { return this._sDOB; }
            set
            {
                if (this._sDOB != value)
                {
                    this.IsDirty = true;
                    this._sDOB = value;
                    OnPropertyChanged(() => SDOB);
                    PropertyChangedCompleted(() => SDOB);
                }
            }
        }

        protected short _sTitle;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the STitle</param>
        /// </summary>
        public short STitle
        {
            get { return this._sTitle; }
            set
            {
                if (this._sTitle != value)
                {
                    this.IsDirty = true;
                    this._sTitle = value;
                    OnPropertyChanged(() => STitle);
                    PropertyChangedCompleted(() => STitle);
                }
            }
        }

        protected string _sIdentification;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the SIdentification</param>
        /// </summary>
        public string SIdentification
        {
            get { return this._sIdentification; }
            set
            {
                if (this._sIdentification != value)
                {
                    this.IsDirty = true;
                    this._sIdentification = value;
                    OnPropertyChanged(() => SIdentification);
                    PropertyChangedCompleted(() => SIdentification);
                }
            }
        }

        protected short _eTitle;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the ETitle</param>
        /// </summary>
        public short ETitle
        {
            get { return this._eTitle; }
            set
            {
                if (this._eTitle != value)
                {
                    this.IsDirty = true;
                    this._eTitle = value;
                    OnPropertyChanged(() => ETitle);
                    PropertyChangedCompleted(() => ETitle);
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
                this.base_GuestProfile.Id = this.Id;
            this.base_GuestProfile.Gender = this.Gender;
            this.base_GuestProfile.Marital = this.Marital;
            if (this.SSN != null)
                this.base_GuestProfile.SSN = this.SSN.Trim();
            if (this.Identification != null)
                this.base_GuestProfile.Identification = this.Identification.Trim();
            this.base_GuestProfile.DOB = this.DOB;
            this.base_GuestProfile.IsSpouse = this.IsSpouse;
            if (this.FirstName != null)
                this.base_GuestProfile.FirstName = this.FirstName.Trim();
            if (this.LastName != null)
                this.base_GuestProfile.LastName = this.LastName.Trim();
            if (this.MiddleName != null)
                this.base_GuestProfile.MiddleName = this.MiddleName.Trim();
            if (this.State != null)
                this.base_GuestProfile.State = this.State.Trim();
            this.base_GuestProfile.SGender = this.SGender;
            if (this.SFirstName != null)
                this.base_GuestProfile.SFirstName = this.SFirstName.Trim();
            if (this.SLastName != null)
                this.base_GuestProfile.SLastName = this.SLastName.Trim();
            if (this.SMiddleName != null)
                this.base_GuestProfile.SMiddleName = this.SMiddleName.Trim();
            if (this.SPhone != null)
                this.base_GuestProfile.SPhone = this.SPhone.Trim();
            if (this.SCellPhone != null)
                this.base_GuestProfile.SCellPhone = this.SCellPhone.Trim();
            if (this.SSSN != null)
                this.base_GuestProfile.SSSN = this.SSSN.Trim();
            if (this.SState != null)
                this.base_GuestProfile.SState = this.SState.Trim();
            if (this.SEmail != null)
                this.base_GuestProfile.SEmail = this.SEmail.Trim();
            this.base_GuestProfile.IsEmergency = this.IsEmergency;
            if (this.EFirstName != null)
                this.base_GuestProfile.EFirstName = this.EFirstName.Trim();
            if (this.ELastName != null)
                this.base_GuestProfile.ELastName = this.ELastName.Trim();
            if (this.EMiddleName != null)
                this.base_GuestProfile.EMiddleName = this.EMiddleName.Trim();
            if (this.EPhone != null)
                this.base_GuestProfile.EPhone = this.EPhone.Trim();
            if (this.ECellPhone != null)
                this.base_GuestProfile.ECellPhone = this.ECellPhone.Trim();
            if (this.ERelationship != null)
                this.base_GuestProfile.ERelationship = this.ERelationship.Trim();
            this.base_GuestProfile.GuestId = this.GuestId;
            if (this.SRelationShip != null)
                this.base_GuestProfile.SRelationShip = this.SRelationShip.Trim();
            if (this.SIM != null)
                this.base_GuestProfile.SIM = this.SIM.Trim();
            this.base_GuestProfile.SDOB = this.SDOB;
            this.base_GuestProfile.STitle = this.STitle;
            if (this.SIdentification != null)
                this.base_GuestProfile.SIdentification = this.SIdentification.Trim();
            this.base_GuestProfile.ETitle = this.ETitle;
        }

        /// <summary>
        /// Public Method
        /// <param>Method for set Entity to PropertyModel</param>
        /// </summary
        public void ToModel()
        {
            this._id = this.base_GuestProfile.Id;
            this._gender = this.base_GuestProfile.Gender;
            this._marital = this.base_GuestProfile.Marital;
            this._sSN = this.base_GuestProfile.SSN;
            this._identification = this.base_GuestProfile.Identification;
            this._dOB = this.base_GuestProfile.DOB;
            this._isSpouse = this.base_GuestProfile.IsSpouse;
            this._firstName = this.base_GuestProfile.FirstName;
            this._lastName = this.base_GuestProfile.LastName;
            this._middleName = this.base_GuestProfile.MiddleName;
            this._state = this.base_GuestProfile.State;
            this._sGender = this.base_GuestProfile.SGender;
            this._sFirstName = this.base_GuestProfile.SFirstName;
            this._sLastName = this.base_GuestProfile.SLastName;
            this._sMiddleName = this.base_GuestProfile.SMiddleName;
            this._sPhone = this.base_GuestProfile.SPhone;
            this._sCellPhone = this.base_GuestProfile.SCellPhone;
            this._sSSN = this.base_GuestProfile.SSSN;
            this._sState = this.base_GuestProfile.SState;
            this._sEmail = this.base_GuestProfile.SEmail;
            this._isEmergency = this.base_GuestProfile.IsEmergency;
            this._eFirstName = this.base_GuestProfile.EFirstName;
            this._eLastName = this.base_GuestProfile.ELastName;
            this._eMiddleName = this.base_GuestProfile.EMiddleName;
            this._ePhone = this.base_GuestProfile.EPhone;
            this._eCellPhone = this.base_GuestProfile.ECellPhone;
            this._eRelationship = this.base_GuestProfile.ERelationship;
            this._guestId = this.base_GuestProfile.GuestId;
            this._sRelationShip = this.base_GuestProfile.SRelationShip;
            this._sIM = this.base_GuestProfile.SIM;
            this._sDOB = this.base_GuestProfile.SDOB;
            this._sTitle = this.base_GuestProfile.STitle;
            this._sIdentification = this.base_GuestProfile.SIdentification;
            this._eTitle = this.base_GuestProfile.ETitle;
        }

        /// <summary>
        /// Public Method
        /// <param>Method for set Entity to PropertyModel</param>
        /// </summary
        public void ToModelAndRaise()
        {
            this.Id = this.base_GuestProfile.Id;
            this.Gender = this.base_GuestProfile.Gender;
            this.Marital = this.base_GuestProfile.Marital;
            this.SSN = this.base_GuestProfile.SSN;
            this.Identification = this.base_GuestProfile.Identification;
            this.DOB = this.base_GuestProfile.DOB;
            this.IsSpouse = this.base_GuestProfile.IsSpouse;
            this.FirstName = this.base_GuestProfile.FirstName;
            this.LastName = this.base_GuestProfile.LastName;
            this.MiddleName = this.base_GuestProfile.MiddleName;
            this.State = this.base_GuestProfile.State;
            this.SGender = this.base_GuestProfile.SGender;
            this.SFirstName = this.base_GuestProfile.SFirstName;
            this.SLastName = this.base_GuestProfile.SLastName;
            this.SMiddleName = this.base_GuestProfile.SMiddleName;
            this.SPhone = this.base_GuestProfile.SPhone;
            this.SCellPhone = this.base_GuestProfile.SCellPhone;
            this.SSSN = this.base_GuestProfile.SSSN;
            this.SState = this.base_GuestProfile.SState;
            this.SEmail = this.base_GuestProfile.SEmail;
            this.IsEmergency = this.base_GuestProfile.IsEmergency;
            this.EFirstName = this.base_GuestProfile.EFirstName;
            this.ELastName = this.base_GuestProfile.ELastName;
            this.EMiddleName = this.base_GuestProfile.EMiddleName;
            this.EPhone = this.base_GuestProfile.EPhone;
            this.ECellPhone = this.base_GuestProfile.ECellPhone;
            this.ERelationship = this.base_GuestProfile.ERelationship;
            this.GuestId = this.base_GuestProfile.GuestId;
            this.SRelationShip = this.base_GuestProfile.SRelationShip;
            this.SIM = this.base_GuestProfile.SIM;
            this.SDOB = this.base_GuestProfile.SDOB;
            this.STitle = this.base_GuestProfile.STitle;
            this.SIdentification = this.base_GuestProfile.SIdentification;
            this.ETitle = this.base_GuestProfile.ETitle;
        }

        #endregion

        #region Custom Code
        public void CreateBase_GuestProfile()
        {
            this.base_GuestProfile = new base_GuestProfile();
        }

       

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
                    case "Gender":
                        break;
                    case "Marital":
                        break;
                    case "SSN":
                        break;
                    case "Identification":
                        break;
                    case "DOB":
                        break;
                    case "IsSpouse":
                        break;
                    case "FirstName":
                        break;
                    case "LastName":
                        break;
                    case "MiddleName":
                        break;
                    case "State":
                        break;
                    case "SGender":
                        break;
                    case "SFirstName":
                        break;
                    case "SLastName":
                        break;
                    case "SMiddleName":
                        break;
                    case "SPhone":
                        break;
                    case "SCellPhone":
                        break;
                    case "SSSN":
                        break;
                    case "SState":
                        break;
                    case "SEmail":
                        if (!string.IsNullOrWhiteSpace(SEmail))
                        {
                            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$";
                            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(pattern);
                            if (!reg.IsMatch(SEmail))
                                message = "Email is not match format";
                        }
                        break;
                    case "IsEmergency":
                        break;
                    case "EFirstName":
                        break;
                    case "ELastName":
                        break;
                    case "EMiddleName":
                        break;
                    case "EPhone":
                        break;
                    case "ECellPhone":
                        break;
                    case "ERelationship":
                        break;
                    case "GuestId":
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
