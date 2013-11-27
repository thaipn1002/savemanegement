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
    /// Model for table base_UserLog
    /// </summary>
    [Serializable]
    public partial class base_UserLogModel : ModelBase, IDataErrorInfo
    {
        #region Constructor

        // Default constructor
        public base_UserLogModel()
        {
            this.IsNew = true;
            this.base_UserLog = new base_UserLog();
        }

        // Default constructor that set entity to field
        public base_UserLogModel(base_UserLog base_userlog, bool isRaiseProperties = false)
        {
            this.base_UserLog = base_userlog;
            if (!isRaiseProperties)
                this.ToModel();
            else
                this.ToModelAndRaise();
            this.IsDirty = false;
        }

        #endregion

        #region Entity Properties

        public base_UserLog base_UserLog { get; private set; }

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

        protected string _ipSource;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the IpSource</para>
        /// </summary>
        public string IpSource
        {
            get { return this._ipSource; }
            set
            {
                if (this._ipSource != value)
                {
                    this.IsDirty = true;
                    this._ipSource = value;
                    OnPropertyChanged(() => IpSource);
                    PropertyChangedCompleted(() => IpSource);
                }
            }
        }

        protected System.DateTime _connectedOn;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the ConnectedOn</para>
        /// </summary>
        public System.DateTime ConnectedOn
        {
            get { return this._connectedOn; }
            set
            {
                if (this._connectedOn != value)
                {
                    this.IsDirty = true;
                    this._connectedOn = value;
                    OnPropertyChanged(() => ConnectedOn);
                    PropertyChangedCompleted(() => ConnectedOn);
                }
            }
        }

        protected Nullable<System.DateTime> _disConnectedOn;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the DisConnectedOn</para>
        /// </summary>
        public Nullable<System.DateTime> DisConnectedOn
        {
            get { return this._disConnectedOn; }
            set
            {
                if (this._disConnectedOn != value)
                {
                    this.IsDirty = true;
                    this._disConnectedOn = value;
                    OnPropertyChanged(() => DisConnectedOn);
                    PropertyChangedCompleted(() => DisConnectedOn);
                }
            }
        }

        protected string _resourceAccessed;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the ResourceAccessed</para>
        /// </summary>
        public string ResourceAccessed
        {
            get { return this._resourceAccessed; }
            set
            {
                if (this._resourceAccessed != value)
                {
                    this.IsDirty = true;
                    this._resourceAccessed = value;
                    OnPropertyChanged(() => ResourceAccessed);
                    PropertyChangedCompleted(() => ResourceAccessed);
                }
            }
        }

        protected Nullable<bool> _isDisconected;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the IsDisconected</para>
        /// </summary>
        public Nullable<bool> IsDisconected
        {
            get { return this._isDisconected; }
            set
            {
                if (this._isDisconected != value)
                {
                    this.IsDirty = true;
                    this._isDisconected = value;
                    OnPropertyChanged(() => IsDisconected);
                    PropertyChangedCompleted(() => IsDisconected);
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
                this.base_UserLog.Id = this.Id;
            this.base_UserLog.IpSource = this.IpSource;
            this.base_UserLog.ConnectedOn = this.ConnectedOn;
            this.base_UserLog.DisConnectedOn = this.DisConnectedOn;
            this.base_UserLog.ResourceAccessed = this.ResourceAccessed;
            this.base_UserLog.IsDisconected = this.IsDisconected;
        }

        /// <summary>
        /// Public Method
        /// <para>Method for set Entity to PropertyModel</para>
        /// </summary
        public void ToModel()
        {
            this._id = this.base_UserLog.Id;
            this._ipSource = this.base_UserLog.IpSource;
            this._connectedOn = this.base_UserLog.ConnectedOn;
            this._disConnectedOn = this.base_UserLog.DisConnectedOn;
            this._resourceAccessed = this.base_UserLog.ResourceAccessed;
            this._isDisconected = this.base_UserLog.IsDisconected;
        }

        /// <summary>
        /// Public Method
        /// <para>Method for set Entity to PropertyModel</para>
        /// </summary
        public void ToModelAndRaise()
        {
            this.Id = this.base_UserLog.Id;
            this.IpSource = this.base_UserLog.IpSource;
            this.ConnectedOn = this.base_UserLog.ConnectedOn;
            this.DisConnectedOn = this.base_UserLog.DisConnectedOn;
            this.ResourceAccessed = this.base_UserLog.ResourceAccessed;
            this.IsDisconected = this.base_UserLog.IsDisconected;
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
                    case "IpSource":
                        break;
                    case "ConnectedOn":
                        break;
                    case "DisConnectedOn":
                        break;
                    case "ResourceAccessed":
                        break;
                    case "IsDisconected":
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
