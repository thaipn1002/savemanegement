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
    /// Model for table rpt_Group
    /// </summary>
    [Serializable]
    public partial class rpt_GroupModel : ModelBase, IDataErrorInfo
    {
        #region Constructor

        // Default constructor
        public rpt_GroupModel()
        {
            this.IsNew = true;
            this.rpt_Group = new rpt_Group();
        }

        // Default constructor that set entity to field
        public rpt_GroupModel(rpt_Group rpt_group, bool isRaiseProperties = false)
        {
            this.rpt_Group = rpt_group;
            if (!isRaiseProperties)
                this.ToModel();
            else
                this.ToModelAndRaise();
            this.IsDirty = false;
        }

        #endregion

        #region Entity Properties

        public rpt_Group rpt_Group { get; private set; }

        #endregion

        #region Primitive Properties

        protected int _id;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the Id</para>
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
        /// <para>Gets or sets the Code</para>
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

        protected Nullable<System.DateTime> _dateCreated;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the DateCreated</para>
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
                this.rpt_Group.Id = this.Id;
            this.rpt_Group.Code = this.Code;
            this.rpt_Group.Name = this.Name;
            this.rpt_Group.DateCreated = this.DateCreated;
            this.rpt_Group.UserCreated = this.UserCreated;
        }

        /// <summary>
        /// Public Method
        /// <para>Method for set Entity to PropertyModel</para>
        /// </summary
        public void ToModel()
        {
            this._id = this.rpt_Group.Id;
            this._code = this.rpt_Group.Code;
            this._name = this.rpt_Group.Name;
            this._dateCreated = this.rpt_Group.DateCreated;
            this._userCreated = this.rpt_Group.UserCreated;
        }

        /// <summary>
        /// Public Method
        /// <para>Method for set Entity to PropertyModel</para>
        /// </summary
        public void ToModelAndRaise()
        {
            this.Id = this.rpt_Group.Id;
            this.Code = this.rpt_Group.Code;
            this.Name = this.rpt_Group.Name;
            this.DateCreated = this.rpt_Group.DateCreated;
            this.UserCreated = this.rpt_Group.UserCreated;
        }

        #endregion

        #region Custom Code
        // Default constructor that set entity to field
        public rpt_GroupModel(rpt_Group rpt_group)
        {
            this.rpt_Group = rpt_group;
            this.ToModel();
            this.Right = false;
            this.IsDirty = false;
        }

        protected bool _right;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the Right</para>
        /// </summary>
        public bool Right
        {
            get { return this._right; }
            set
            {
                if (this._right != value)
                {
                    this.IsDirty = true;
                    this._right = value;
                    OnPropertyChanged(() => Right);
                    PropertyChangedCompleted(() => Right);
                }
            }
        }

        #region -RootReportColection-
        /// <summary>
        /// Set or get RptReportColection
        /// </summary>
        private ObservableCollection<rpt_ReportModel> _rootReportColection;
        public ObservableCollection<rpt_ReportModel> RootReportColection
        {
            get { return _rootReportColection; }
            set
            {
                if (_rootReportColection != value)
                {
                    _rootReportColection = value;
                    OnPropertyChanged(() => RootReportColection);
                }
            }
        }
        #endregion

        #endregion

        #region IDataErrorInfo Members

        public string Error
        {
            get { return null; }
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
                    case "Code":
                        break;
                    case "Name":
                        break;
                    case "DateCreated":
                        break;
                    case "UserCreated":
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
