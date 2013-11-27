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
    /// Model for table pga_jobagent
    /// </summary>
    [Serializable]
    public partial class pga_jobagentModel : ModelBase, IDataErrorInfo
    {
        #region Constructor

        // Default constructor
        public pga_jobagentModel()
        {
            this.IsNew = true;
            this.pga_jobagent = new pga_jobagent();
        }

        // Default constructor that set entity to field
        public pga_jobagentModel(pga_jobagent pga_jobagent, bool isRaiseProperties = false)
        {
            this.pga_jobagent = pga_jobagent;
            if (!isRaiseProperties)
                this.ToModel();
            else
                this.ToModelAndRaise();
            this.IsDirty = false;
        }

        #endregion

        #region Entity Properties

        public pga_jobagent pga_jobagent { get; private set; }

        #endregion

        #region Primitive Properties

        protected int _jagpid;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the jagpid</para>
        /// </summary>
        public int jagpid
        {
            get { return this._jagpid; }
            set
            {
                if (this._jagpid != value)
                {
                    this.IsDirty = true;
                    this._jagpid = value;
                    OnPropertyChanged(() => jagpid);
                    PropertyChangedCompleted(() => jagpid);
                }
            }
        }

        protected System.DateTimeOffset _jaglogintime;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the jaglogintime</para>
        /// </summary>
        public System.DateTimeOffset jaglogintime
        {
            get { return this._jaglogintime; }
            set
            {
                if (this._jaglogintime != value)
                {
                    this.IsDirty = true;
                    this._jaglogintime = value;
                    OnPropertyChanged(() => jaglogintime);
                    PropertyChangedCompleted(() => jaglogintime);
                }
            }
        }

        protected string _jagstation;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the jagstation</para>
        /// </summary>
        public string jagstation
        {
            get { return this._jagstation; }
            set
            {
                if (this._jagstation != value)
                {
                    this.IsDirty = true;
                    this._jagstation = value;
                    OnPropertyChanged(() => jagstation);
                    PropertyChangedCompleted(() => jagstation);
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
                this.pga_jobagent.jagpid = this.jagpid;
            this.pga_jobagent.jaglogintime = this.jaglogintime;
            this.pga_jobagent.jagstation = this.jagstation;
        }

        /// <summary>
        /// Public Method
        /// <para>Method for set Entity to PropertyModel</para>
        /// </summary
        public void ToModel()
        {
            this._jagpid = this.pga_jobagent.jagpid;
            this._jaglogintime = this.pga_jobagent.jaglogintime;
            this._jagstation = this.pga_jobagent.jagstation;
        }

        /// <summary>
        /// Public Method
        /// <para>Method for set Entity to PropertyModel</para>
        /// </summary
        public void ToModelAndRaise()
        {
            this.jagpid = this.pga_jobagent.jagpid;
            this.jaglogintime = this.pga_jobagent.jaglogintime;
            this.jagstation = this.pga_jobagent.jagstation;
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
                    case "jagpid":
                        break;
                    case "jaglogintime":
                        break;
                    case "jagstation":
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
