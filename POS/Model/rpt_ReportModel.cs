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
    /// Model for table rpt_Report
    /// </summary>
    [Serializable]
    public partial class rpt_ReportModel : ModelBase, IDataErrorInfo
    {
        #region Constructor

        // Default constructor
        public rpt_ReportModel()
        {
            this.IsNew = true;
            this.rpt_Report = new rpt_Report();
        }

        // Default constructor that set entity to field
        public rpt_ReportModel(rpt_Report rpt_report, bool isRaiseProperties = false)
        {
            this.rpt_Report = rpt_report;
            if (!isRaiseProperties)
                this.ToModel();
            else
                this.ToModelAndRaise();
            this.IsDirty = false;
        }

        #endregion

        #region Entity Properties

        public rpt_Report rpt_Report { get; private set; }

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

        protected int _groupId;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the GroupId</param>
        /// </summary>
        public int GroupId
        {
            get { return this._groupId; }
            set
            {
                if (this._groupId != value)
                {
                    this.IsDirty = true;
                    this._groupId = value;
                    OnPropertyChanged(() => GroupId);
                    PropertyChangedCompleted(() => GroupId);
                }
            }
        }

        protected int _parentId;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the ParentId</param>
        /// </summary>
        public int ParentId
        {
            get { return this._parentId; }
            set
            {
                if (this._parentId != value)
                {
                    this.IsDirty = true;
                    this._parentId = value;
                    OnPropertyChanged(() => ParentId);
                    PropertyChangedCompleted(() => ParentId);
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

        protected string _formatFile;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the FormatFile</param>
        /// </summary>
        public string FormatFile
        {
            get { return this._formatFile; }
            set
            {
                if (this._formatFile != value)
                {
                    this.IsDirty = true;
                    this._formatFile = value;
                    OnPropertyChanged(() => FormatFile);
                    PropertyChangedCompleted(() => FormatFile);
                }
            }
        }

        protected bool _isShow;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the IsShow</param>
        /// </summary>
        public bool IsShow
        {
            get { return this._isShow; }
            set
            {
                if (this._isShow != value)
                {
                    this.IsDirty = true;
                    this._isShow = value;
                    OnPropertyChanged(() => IsShow);
                    PropertyChangedCompleted(() => IsShow);
                }
            }
        }

        protected string _preProcessName;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the PreProcessName</param>
        /// </summary>
        public string PreProcessName
        {
            get { return this._preProcessName; }
            set
            {
                if (this._preProcessName != value)
                {
                    this.IsDirty = true;
                    this._preProcessName = value;
                    OnPropertyChanged(() => PreProcessName);
                    PropertyChangedCompleted(() => PreProcessName);
                }
            }
        }

        protected byte[] _samplePicture;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the SamplePicture</param>
        /// </summary>
        public byte[] SamplePicture
        {
            get { return this._samplePicture; }
            set
            {
                if (this._samplePicture != value)
                {
                    this.IsDirty = true;
                    this._samplePicture = value;
                    OnPropertyChanged(() => SamplePicture);
                    PropertyChangedCompleted(() => SamplePicture);
                }
            }
        }

        protected int _printTimes;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the PrintTimes</param>
        /// </summary>
        public int PrintTimes
        {
            get { return this._printTimes; }
            set
            {
                if (this._printTimes != value)
                {
                    this.IsDirty = true;
                    this._printTimes = value;
                    OnPropertyChanged(() => PrintTimes);
                    PropertyChangedCompleted(() => PrintTimes);
                }
            }
        }

        protected Nullable<System.DateTime> _lastPrintDate;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the LastPrintDate</param>
        /// </summary>
        public Nullable<System.DateTime> LastPrintDate
        {
            get { return this._lastPrintDate; }
            set
            {
                if (this._lastPrintDate != value)
                {
                    this.IsDirty = true;
                    this._lastPrintDate = value;
                    OnPropertyChanged(() => LastPrintDate);
                    PropertyChangedCompleted(() => LastPrintDate);
                }
            }
        }

        protected string _lastPrintUser;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the LastPrintUser</param>
        /// </summary>
        public string LastPrintUser
        {
            get { return this._lastPrintUser; }
            set
            {
                if (this._lastPrintUser != value)
                {
                    this.IsDirty = true;
                    this._lastPrintUser = value;
                    OnPropertyChanged(() => LastPrintUser);
                    PropertyChangedCompleted(() => LastPrintUser);
                }
            }
        }

        protected string _excelFile;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the ExcelFile</param>
        /// </summary>
        public string ExcelFile
        {
            get { return this._excelFile; }
            set
            {
                if (this._excelFile != value)
                {
                    this.IsDirty = true;
                    this._excelFile = value;
                    OnPropertyChanged(() => ExcelFile);
                    PropertyChangedCompleted(() => ExcelFile);
                }
            }
        }

        protected string _printerName;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the PrinterName</param>
        /// </summary>
        public string PrinterName
        {
            get { return this._printerName; }
            set
            {
                if (this._printerName != value)
                {
                    this.IsDirty = true;
                    this._printerName = value;
                    OnPropertyChanged(() => PrinterName);
                    PropertyChangedCompleted(() => PrinterName);
                }
            }
        }

        protected short _printCopy;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the PrintCopy</param>
        /// </summary>
        public short PrintCopy
        {
            get { return this._printCopy; }
            set
            {
                if (this._printCopy != value)
                {
                    this.IsDirty = true;
                    this._printCopy = value;
                    OnPropertyChanged(() => PrintCopy);
                    PropertyChangedCompleted(() => PrintCopy);
                }
            }
        }

        protected string _remark;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the Remark</param>
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

        protected Nullable<System.DateTime> _dateUpdated;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the DateUpdated</param>
        /// </summary>
        public Nullable<System.DateTime> DateUpdated
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

        protected short _paperSize;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the PaperSize</param>
        /// </summary>
        public short PaperSize
        {
            get { return this._paperSize; }
            set
            {
                if (this._paperSize != value)
                {
                    this.IsDirty = true;
                    this._paperSize = value;
                    OnPropertyChanged(() => PaperSize);
                    PropertyChangedCompleted(() => PaperSize);
                }
            }
        }

        protected int _screenTimes;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the ScreenTimes</param>
        /// </summary>
        public int ScreenTimes
        {
            get { return this._screenTimes; }
            set
            {
                if (this._screenTimes != value)
                {
                    this.IsDirty = true;
                    this._screenTimes = value;
                    OnPropertyChanged(() => ScreenTimes);
                    PropertyChangedCompleted(() => ScreenTimes);
                }
            }
        }

        protected string _prepProcessDescription;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the PrepProcessDescription</param>
        /// </summary>
        public string PrepProcessDescription
        {
            get { return this._prepProcessDescription; }
            set
            {
                if (this._prepProcessDescription != value)
                {
                    this.IsDirty = true;
                    this._prepProcessDescription = value;
                    OnPropertyChanged(() => PrepProcessDescription);
                    PropertyChangedCompleted(() => PrepProcessDescription);
                }
            }
        }

        protected string _cCReport;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the CCReport</param>
        /// </summary>
        public string CCReport
        {
            get { return this._cCReport; }
            set
            {
                if (this._cCReport != value)
                {
                    this.IsDirty = true;
                    this._cCReport = value;
                    OnPropertyChanged(() => CCReport);
                    PropertyChangedCompleted(() => CCReport);
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
                this.rpt_Report.Id = this.Id;
            this.rpt_Report.GroupId = this.GroupId;
            this.rpt_Report.ParentId = this.ParentId;
            if (this.Code != null)
                this.rpt_Report.Code = this.Code.Trim();
            if (this.Name != null)
                this.rpt_Report.Name = this.Name.Trim();
            if (this.FormatFile != null)
                this.rpt_Report.FormatFile = this.FormatFile.Trim();
            this.rpt_Report.IsShow = this.IsShow;
            if (this.PreProcessName != null)
                this.rpt_Report.PreProcessName = this.PreProcessName.Trim();
            this.rpt_Report.SamplePicture = this.SamplePicture;
            this.rpt_Report.PrintTimes = this.PrintTimes;
            this.rpt_Report.LastPrintDate = this.LastPrintDate;
            if (this.LastPrintUser != null)
                this.rpt_Report.LastPrintUser = this.LastPrintUser.Trim();
            if (this.ExcelFile != null)
                this.rpt_Report.ExcelFile = this.ExcelFile.Trim();
            if (this.PrinterName != null)
                this.rpt_Report.PrinterName = this.PrinterName.Trim();
            this.rpt_Report.PrintCopy = this.PrintCopy;
            if (this.Remark != null)
                this.rpt_Report.Remark = this.Remark.Trim();
            this.rpt_Report.DateCreated = this.DateCreated;
            if (this.UserCreated != null)
                this.rpt_Report.UserCreated = this.UserCreated.Trim();
            this.rpt_Report.DateUpdated = this.DateUpdated;
            if (this.UserUpdated != null)
                this.rpt_Report.UserUpdated = this.UserUpdated.Trim();
            this.rpt_Report.PaperSize = this.PaperSize;
            this.rpt_Report.ScreenTimes = this.ScreenTimes;
            if (this.PrepProcessDescription != null)
                this.rpt_Report.PrepProcessDescription = this.PrepProcessDescription.Trim();
            if (this.CCReport != null)
                this.rpt_Report.CCReport = this.CCReport.Trim();
        }

        /// <summary>
        /// Public Method
        /// <param>Method for set Entity to PropertyModel</param>
        /// </summary
        public void ToModel()
        {
            this._id = this.rpt_Report.Id;
            this._groupId = this.rpt_Report.GroupId;
            this._parentId = this.rpt_Report.ParentId;
            this._code = this.rpt_Report.Code;
            this._name = this.rpt_Report.Name;
            this._formatFile = this.rpt_Report.FormatFile;
            this._isShow = this.rpt_Report.IsShow;
            this._preProcessName = this.rpt_Report.PreProcessName;
            this._samplePicture = this.rpt_Report.SamplePicture;
            this._printTimes = this.rpt_Report.PrintTimes;
            this._lastPrintDate = this.rpt_Report.LastPrintDate;
            this._lastPrintUser = this.rpt_Report.LastPrintUser;
            this._excelFile = this.rpt_Report.ExcelFile;
            this._printerName = this.rpt_Report.PrinterName;
            this._printCopy = this.rpt_Report.PrintCopy;
            this._remark = this.rpt_Report.Remark;
            this._dateCreated = this.rpt_Report.DateCreated;
            this._userCreated = this.rpt_Report.UserCreated;
            this._dateUpdated = this.rpt_Report.DateUpdated;
            this._userUpdated = this.rpt_Report.UserUpdated;
            this._paperSize = this.rpt_Report.PaperSize;
            this._screenTimes = this.rpt_Report.ScreenTimes;
            this._prepProcessDescription = this.rpt_Report.PrepProcessDescription;
            this._cCReport = this.rpt_Report.CCReport;
        }

        /// <summary>
        /// Public Method
        /// <param>Method for set Entity to PropertyModel</param>
        /// </summary
        public void ToModelAndRaise()
        {
            this.Id = this.rpt_Report.Id;
            this.GroupId = this.rpt_Report.GroupId;
            this.ParentId = this.rpt_Report.ParentId;
            this.Code = this.rpt_Report.Code;
            this.Name = this.rpt_Report.Name;
            this.FormatFile = this.rpt_Report.FormatFile;
            this.IsShow = this.rpt_Report.IsShow;
            this.PreProcessName = this.rpt_Report.PreProcessName;
            this.SamplePicture = this.rpt_Report.SamplePicture;
            this.PrintTimes = this.rpt_Report.PrintTimes;
            this.LastPrintDate = this.rpt_Report.LastPrintDate;
            this.LastPrintUser = this.rpt_Report.LastPrintUser;
            this.ExcelFile = this.rpt_Report.ExcelFile;
            this.PrinterName = this.rpt_Report.PrinterName;
            this.PrintCopy = this.rpt_Report.PrintCopy;
            this.Remark = this.rpt_Report.Remark;
            this.DateCreated = this.rpt_Report.DateCreated;
            this.UserCreated = this.rpt_Report.UserCreated;
            this.DateUpdated = this.rpt_Report.DateUpdated;
            this.UserUpdated = this.rpt_Report.UserUpdated;
            this.PaperSize = this.rpt_Report.PaperSize;
            this.ScreenTimes = this.rpt_Report.ScreenTimes;
            this.PrepProcessDescription = this.rpt_Report.PrepProcessDescription;
            this.CCReport = this.rpt_Report.CCReport;
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
                    case "GroupId":
                        break;
                    case "ParentId":
                        break;
                    case "Code":
                        break;
                    case "Name":
                        break;
                    case "FormatFile":
                        break;
                    case "DateCreated":
                        break;
                    case "UserCreated":
                        break;
                    case "IsShow":
                        break;
                    case "ProcessName":
                        break;
                    case "SamplePicture":
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
