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
    /// Model for table tims_TimeLog
    /// </summary>
    [Serializable]
    public partial class tims_TimeLogModel : ModelBase, IDataErrorInfo
    {
        #region Constructor

        // Default constructor
        public tims_TimeLogModel()
        {
            this.IsNew = true;
            this.tims_TimeLog = new tims_TimeLog();
        }

        // Default constructor that set entity to field
        public tims_TimeLogModel(tims_TimeLog tims_timelog, bool isRaiseProperties = false)
        {
            this.tims_TimeLog = tims_timelog;
            if (!isRaiseProperties)
                this.ToModel();
            else
                this.ToModelAndRaise();
            this.IsDirty = false;
        }

        #endregion

        #region Entity Properties

        public tims_TimeLog tims_TimeLog { get; private set; }

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

        protected Nullable<long> _employeeId;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the EmployeeId</para>
        /// </summary>
        public Nullable<long> EmployeeId
        {
            get { return this._employeeId; }
            set
            {
                if (this._employeeId != value)
                {
                    this.IsDirty = true;
                    this._employeeId = value;
                    OnPropertyChanged(() => EmployeeId);
                    PropertyChangedCompleted(() => EmployeeId);
                }
            }
        }

        protected Nullable<int> _workScheduleId;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the WorkScheduleId</para>
        /// </summary>
        public Nullable<int> WorkScheduleId
        {
            get { return this._workScheduleId; }
            set
            {
                if (this._workScheduleId != value)
                {
                    this.IsDirty = true;
                    this._workScheduleId = value;
                    OnPropertyChanged(() => WorkScheduleId);
                    PropertyChangedCompleted(() => WorkScheduleId);
                }
            }
        }

        protected Nullable<int> _payrollId;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the PayrollId</para>
        /// </summary>
        public Nullable<int> PayrollId
        {
            get { return this._payrollId; }
            set
            {
                if (this._payrollId != value)
                {
                    this.IsDirty = true;
                    this._payrollId = value;
                    OnPropertyChanged(() => PayrollId);
                    PropertyChangedCompleted(() => PayrollId);
                }
            }
        }

        protected System.DateTime _clockIn;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the ClockIn</para>
        /// </summary>
        public System.DateTime ClockIn
        {
            get { return this._clockIn; }
            set
            {
                if (this._clockIn != value)
                {
                    this.IsDirty = true;
                    this._clockIn = value;
                    OnPropertyChanged(() => ClockIn);
                    PropertyChangedCompleted(() => ClockIn);
                }
            }
        }

        protected Nullable<System.DateTime> _clockOut;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the ClockOut</para>
        /// </summary>
        public Nullable<System.DateTime> ClockOut
        {
            get { return this._clockOut; }
            set
            {
                if (this._clockOut != value)
                {
                    this.IsDirty = true;
                    this._clockOut = value;
                    OnPropertyChanged(() => ClockOut);
                    PropertyChangedCompleted(() => ClockOut);
                }
            }
        }

        protected bool _manualClockInFlag;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the ManualClockInFlag</para>
        /// </summary>
        public bool ManualClockInFlag
        {
            get { return this._manualClockInFlag; }
            set
            {
                if (this._manualClockInFlag != value)
                {
                    this.IsDirty = true;
                    this._manualClockInFlag = value;
                    OnPropertyChanged(() => ManualClockInFlag);
                    PropertyChangedCompleted(() => ManualClockInFlag);
                }
            }
        }

        protected bool _manualClockOutFlag;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the ManualClockOutFlag</para>
        /// </summary>
        public bool ManualClockOutFlag
        {
            get { return this._manualClockOutFlag; }
            set
            {
                if (this._manualClockOutFlag != value)
                {
                    this.IsDirty = true;
                    this._manualClockOutFlag = value;
                    OnPropertyChanged(() => ManualClockOutFlag);
                    PropertyChangedCompleted(() => ManualClockOutFlag);
                }
            }
        }

        protected float _workTime;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the WorkTime</para>
        /// </summary>
        public float WorkTime
        {
            get { return this._workTime; }
            set
            {
                if (this._workTime != value)
                {
                    this.IsDirty = true;
                    this._workTime = value;
                    OnPropertyChanged(() => WorkTime);
                    PropertyChangedCompleted(() => WorkTime);
                }
            }
        }

        protected float _lunchTime;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the LunchTime</para>
        /// </summary>
        public float LunchTime
        {
            get { return this._lunchTime; }
            set
            {
                if (this._lunchTime != value)
                {
                    this.IsDirty = true;
                    this._lunchTime = value;
                    OnPropertyChanged(() => LunchTime);
                    PropertyChangedCompleted(() => LunchTime);
                }
            }
        }

        protected float _overtimeBefore;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the OvertimeBefore</para>
        /// </summary>
        public float OvertimeBefore
        {
            get { return this._overtimeBefore; }
            set
            {
                if (this._overtimeBefore != value)
                {
                    this.IsDirty = true;
                    this._overtimeBefore = value;
                    OnPropertyChanged(() => OvertimeBefore);
                    PropertyChangedCompleted(() => OvertimeBefore);
                }
            }
        }

        protected string _reason;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the Reason</para>
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

        protected bool _deductLunchTimeFlag;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the DeductLunchTimeFlag</para>
        /// </summary>
        public bool DeductLunchTimeFlag
        {
            get { return this._deductLunchTimeFlag; }
            set
            {
                if (this._deductLunchTimeFlag != value)
                {
                    this.IsDirty = true;
                    this._deductLunchTimeFlag = value;
                    OnPropertyChanged(() => DeductLunchTimeFlag);
                    PropertyChangedCompleted(() => DeductLunchTimeFlag);
                }
            }
        }

        protected Nullable<float> _lateTime;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the LateTime</para>
        /// </summary>
        public Nullable<float> LateTime
        {
            get { return this._lateTime; }
            set
            {
                if (this._lateTime != value)
                {
                    this.IsDirty = true;
                    this._lateTime = value;
                    OnPropertyChanged(() => LateTime);
                    PropertyChangedCompleted(() => LateTime);
                }
            }
        }

        protected Nullable<float> _leaveEarlyTime;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the LeaveEarlyTime</para>
        /// </summary>
        public Nullable<float> LeaveEarlyTime
        {
            get { return this._leaveEarlyTime; }
            set
            {
                if (this._leaveEarlyTime != value)
                {
                    this.IsDirty = true;
                    this._leaveEarlyTime = value;
                    OnPropertyChanged(() => LeaveEarlyTime);
                    PropertyChangedCompleted(() => LeaveEarlyTime);
                }
            }
        }

        protected bool _activeFlag;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the ActiveFlag</para>
        /// </summary>
        public bool ActiveFlag
        {
            get { return this._activeFlag; }
            set
            {
                if (this._activeFlag != value)
                {
                    this.IsDirty = true;
                    this._activeFlag = value;
                    OnPropertyChanged(() => ActiveFlag);
                    PropertyChangedCompleted(() => ActiveFlag);
                }
            }
        }

        protected Nullable<System.DateTime> _dateUpdated;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the DateUpdated</para>
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

        protected float _overtimeAfter;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the OvertimeAfter</para>
        /// </summary>
        public float OvertimeAfter
        {
            get { return this._overtimeAfter; }
            set
            {
                if (this._overtimeAfter != value)
                {
                    this.IsDirty = true;
                    this._overtimeAfter = value;
                    OnPropertyChanged(() => OvertimeAfter);
                    PropertyChangedCompleted(() => OvertimeAfter);
                }
            }
        }

        protected float _overtimeLunch;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the OvertimeLunch</para>
        /// </summary>
        public float OvertimeLunch
        {
            get { return this._overtimeLunch; }
            set
            {
                if (this._overtimeLunch != value)
                {
                    this.IsDirty = true;
                    this._overtimeLunch = value;
                    OnPropertyChanged(() => OvertimeLunch);
                    PropertyChangedCompleted(() => OvertimeLunch);
                }
            }
        }

        protected float _overtimeDayOff;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the OvertimeDayOff</para>
        /// </summary>
        public float OvertimeDayOff
        {
            get { return this._overtimeDayOff; }
            set
            {
                if (this._overtimeDayOff != value)
                {
                    this.IsDirty = true;
                    this._overtimeDayOff = value;
                    OnPropertyChanged(() => OvertimeDayOff);
                    PropertyChangedCompleted(() => OvertimeDayOff);
                }
            }
        }

        protected int _overtimeOptions;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the OvertimeOptions</para>
        /// </summary>
        public int OvertimeOptions
        {
            get { return this._overtimeOptions; }
            set
            {
                if (this._overtimeOptions != value)
                {
                    this.IsDirty = true;
                    this._overtimeOptions = value;
                    OnPropertyChanged(() => OvertimeOptions);
                    PropertyChangedCompleted(() => OvertimeOptions);
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

        protected bool _isHoliday;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the IsHoliday</para>
        /// </summary>
        public bool IsHoliday
        {
            get { return this._isHoliday; }
            set
            {
                if (this._isHoliday != value)
                {
                    this.IsDirty = true;
                    this._isHoliday = value;
                    OnPropertyChanged(() => IsHoliday);
                    PropertyChangedCompleted(() => IsHoliday);
                }
            }
        }

        protected string _holidayDescription;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the HolidayDescription</para>
        /// </summary>
        public string HolidayDescription
        {
            get { return this._holidayDescription; }
            set
            {
                if (this._holidayDescription != value)
                {
                    this.IsDirty = true;
                    this._holidayDescription = value;
                    OnPropertyChanged(() => HolidayDescription);
                    PropertyChangedCompleted(() => HolidayDescription);
                }
            }
        }

        protected string _userUpdated;
        /// <summary>
        /// Property Model
        /// <para>Gets or sets the UserUpdated</para>
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
                this.tims_TimeLog.Id = this.Id;
            this.tims_TimeLog.EmployeeId = this.EmployeeId;
            this.tims_TimeLog.WorkScheduleId = this.WorkScheduleId;
            this.tims_TimeLog.PayrollId = this.PayrollId;
            this.tims_TimeLog.ClockIn = this.ClockIn;
            this.tims_TimeLog.ClockOut = this.ClockOut;
            this.tims_TimeLog.ManualClockInFlag = this.ManualClockInFlag;
            this.tims_TimeLog.ManualClockOutFlag = this.ManualClockOutFlag;
            this.tims_TimeLog.WorkTime = this.WorkTime;
            this.tims_TimeLog.LunchTime = this.LunchTime;
            this.tims_TimeLog.OvertimeBefore = this.OvertimeBefore;
            this.tims_TimeLog.Reason = this.Reason;
            this.tims_TimeLog.DeductLunchTimeFlag = this.DeductLunchTimeFlag;
            this.tims_TimeLog.LateTime = this.LateTime;
            this.tims_TimeLog.LeaveEarlyTime = this.LeaveEarlyTime;
            this.tims_TimeLog.ActiveFlag = this.ActiveFlag;
            this.tims_TimeLog.DateUpdated = this.DateUpdated;
            this.tims_TimeLog.OvertimeAfter = this.OvertimeAfter;
            this.tims_TimeLog.OvertimeLunch = this.OvertimeLunch;
            this.tims_TimeLog.OvertimeDayOff = this.OvertimeDayOff;
            this.tims_TimeLog.OvertimeOptions = this.OvertimeOptions;
            this.tims_TimeLog.GuestResource = this.GuestResource;
            this.tims_TimeLog.IsHoliday = this.IsHoliday;
            this.tims_TimeLog.HolidayDescription = this.HolidayDescription;
            this.tims_TimeLog.UserUpdated = this.UserUpdated;
        }

        /// <summary>
        /// Public Method
        /// <para>Method for set Entity to PropertyModel</para>
        /// </summary
        public void ToModel()
        {
            this._id = this.tims_TimeLog.Id;
            this._employeeId = this.tims_TimeLog.EmployeeId;
            this._workScheduleId = this.tims_TimeLog.WorkScheduleId;
            this._payrollId = this.tims_TimeLog.PayrollId;
            this._clockIn = this.tims_TimeLog.ClockIn;
            this._clockOut = this.tims_TimeLog.ClockOut;
            this._manualClockInFlag = this.tims_TimeLog.ManualClockInFlag;
            this._manualClockOutFlag = this.tims_TimeLog.ManualClockOutFlag;
            this._workTime = this.tims_TimeLog.WorkTime;
            this._lunchTime = this.tims_TimeLog.LunchTime;
            this._overtimeBefore = this.tims_TimeLog.OvertimeBefore;
            this._reason = this.tims_TimeLog.Reason;
            this._deductLunchTimeFlag = this.tims_TimeLog.DeductLunchTimeFlag;
            this._lateTime = this.tims_TimeLog.LateTime;
            this._leaveEarlyTime = this.tims_TimeLog.LeaveEarlyTime;
            this._activeFlag = this.tims_TimeLog.ActiveFlag;
            this._dateUpdated = this.tims_TimeLog.DateUpdated;
            this._overtimeAfter = this.tims_TimeLog.OvertimeAfter;
            this._overtimeLunch = this.tims_TimeLog.OvertimeLunch;
            this._overtimeDayOff = this.tims_TimeLog.OvertimeDayOff;
            this._overtimeOptions = this.tims_TimeLog.OvertimeOptions;
            this._guestResource = this.tims_TimeLog.GuestResource;
            this._isHoliday = this.tims_TimeLog.IsHoliday;
            this._holidayDescription = this.tims_TimeLog.HolidayDescription;
            this._userUpdated = this.tims_TimeLog.UserUpdated;
        }

        /// <summary>
        /// Public Method
        /// <para>Method for set Entity to PropertyModel</para>
        /// </summary
        public void ToModelAndRaise()
        {
            this.Id = this.tims_TimeLog.Id;
            this.EmployeeId = this.tims_TimeLog.EmployeeId;
            this.WorkScheduleId = this.tims_TimeLog.WorkScheduleId;
            this.PayrollId = this.tims_TimeLog.PayrollId;
            this.ClockIn = this.tims_TimeLog.ClockIn;
            this.ClockOut = this.tims_TimeLog.ClockOut;
            this.ManualClockInFlag = this.tims_TimeLog.ManualClockInFlag;
            this.ManualClockOutFlag = this.tims_TimeLog.ManualClockOutFlag;
            this.WorkTime = this.tims_TimeLog.WorkTime;
            this.LunchTime = this.tims_TimeLog.LunchTime;
            this.OvertimeBefore = this.tims_TimeLog.OvertimeBefore;
            this.Reason = this.tims_TimeLog.Reason;
            this.DeductLunchTimeFlag = this.tims_TimeLog.DeductLunchTimeFlag;
            this.LateTime = this.tims_TimeLog.LateTime;
            this.LeaveEarlyTime = this.tims_TimeLog.LeaveEarlyTime;
            this.ActiveFlag = this.tims_TimeLog.ActiveFlag;
            this.DateUpdated = this.tims_TimeLog.DateUpdated;
            this.OvertimeAfter = this.tims_TimeLog.OvertimeAfter;
            this.OvertimeLunch = this.tims_TimeLog.OvertimeLunch;
            this.OvertimeDayOff = this.tims_TimeLog.OvertimeDayOff;
            this.OvertimeOptions = this.tims_TimeLog.OvertimeOptions;
            this.GuestResource = this.tims_TimeLog.GuestResource;
            this.IsHoliday = this.tims_TimeLog.IsHoliday;
            this.HolidayDescription = this.tims_TimeLog.HolidayDescription;
            this.UserUpdated = this.tims_TimeLog.UserUpdated;
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
                    case "EmployeeId":
                        break;
                    case "WorkScheduleId":
                        break;
                    case "PayrollId":
                        break;
                    case "ClockIn":
                        break;
                    case "ClockOut":
                        break;
                    case "ManualClockInFlag":
                        break;
                    case "ManualClockOutFlag":
                        break;
                    case "WorkTime":
                        break;
                    case "LunchTime":
                        break;
                    case "OvertimeBefore":
                        break;
                    case "Reason":
                        break;
                    case "DeductLunchTimeFlag":
                        break;
                    case "LateTime":
                        break;
                    case "LeaveEarlyTime":
                        break;
                    case "ActiveFlag":
                        break;
                    case "ModifiedDate":
                        break;
                    case "ModifiedById":
                        break;
                    case "OvertimeAfter":
                        break;
                    case "OvertimeLunch":
                        break;
                    case "OvertimeDayOff":
                        break;
                    case "OvertimeOptions":
                        break;
                    case "GuestResource":
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
