using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CPC.Toolkit.Base;
using System.Windows.Input;
using CPC.Toolkit.Command;
using CPC.POS.Model;
using CPC.Helper;
using CPC.POS.Repository;
using MessageBoxControl;

namespace CPC.POS.ViewModel
{
    class NewTaskViewModel : ViewModelBase
    {
        #region Fields

        #endregion

        #region Contructors

        public NewTaskViewModel(base_ReminderModel reminder)
        {
            _reminder = reminder;
            _reminder.OnDate = _reminder.Time.Date;
            _reminder.OnTime = _reminder.Time;
            Initialize();
        }

        #endregion

        #region Properties

        #region Reminder

        private base_ReminderModel _reminder;
        /// <summary>
        /// Gets or sets reminder.
        /// </summary>
        public base_ReminderModel Reminder
        {
            get
            {
                return _reminder;
            }
            set
            {
                if (_reminder != value)
                {
                    _reminder = value;
                    OnPropertyChanged(() => Reminder);
                }
            }
        }

        #endregion

        #region UserList

        private CollectionBase<base_GuestModel> _userList;
        /// <summary>
        /// Gets or sets user list.
        /// </summary>
        public CollectionBase<base_GuestModel> UserList
        {
            get
            {
                return _userList;
            }
            set
            {
                if (_userList != value)
                {
                    _userList = value;
                    OnPropertyChanged(() => UserList);
                }
            }
        }

        #endregion

        #endregion

        #region Command Properties

        #region SaveCommand

        private ICommand _saveCommand;
        /// <summary>
        /// Save reminder.
        /// </summary>
        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand(SaveExecute, CanSaveExecute);
                }
                return _saveCommand;
            }
        }

        #endregion

        #region CancelCommand

        private ICommand _cancelCommand;
        /// <summary>
        /// Cancel.
        /// </summary>
        public ICommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                {
                    _cancelCommand = new RelayCommand(CancelExecute);
                }
                return _cancelCommand;
            }
        }

        #endregion

        #endregion

        #region Command Methods

        #region SaveExecute

        /// <summary>
        /// Save reminder.
        /// </summary>
        private void SaveExecute()
        {
            Save();
        }

        #endregion

        #region CanSaveExecute

        private bool CanSaveExecute()
        {
            if (_reminder == null || _reminder.HasError || !_reminder.IsDirty)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region CancelExecute

        /// <summary>
        /// Cancel.
        /// </summary>
        private void CancelExecute()
        {
            Cancel();
        }

        #endregion

        #endregion

        #region Private Methods

        #region Save

        /// <summary>
        /// Save reminder.
        /// </summary>
        private void Save()
        {
            try
            {
                base_ReminderRepository reminderRepository = new base_ReminderRepository();

                if (_reminder.IsNew)
                {
                    //CalculateTime(_reminder);
                    _reminder.Time = new DateTime(_reminder.OnDate.Year, _reminder.OnDate.Month, _reminder.OnDate.Day, _reminder.OnTime.Hour, _reminder.OnTime.Minute, 0);
                    _reminder.UserCreated = Define.USER.LoginName;
                    _reminder.UserUpdated = Define.USER.LoginName;
                    _reminder.DateCreated = DateTime.Now;
                    _reminder.DateUpdated = DateTime.Now;
                    _reminder.ToEntity();
                    reminderRepository.Add(_reminder.base_Reminder);
                    reminderRepository.Commit();
                    _reminder.Id = _reminder.base_Reminder.Id;
                    _reminder.IsNew = false;
                    _reminder.IsDirty = false;
                }
                else
                {
                    //CalculateTime(_reminder);
                    _reminder.Time = new DateTime(_reminder.OnDate.Year, _reminder.OnDate.Month, _reminder.OnDate.Day, _reminder.OnTime.Hour, _reminder.OnTime.Minute, 0);
                    _reminder.UserUpdated = Define.USER.LoginName;
                    _reminder.DateUpdated = DateTime.Now;
                    _reminder.ToEntity();
                    reminderRepository.Commit();
                    _reminder.IsDirty = false;
                }

                Close(true);
            }
            catch (Exception exception)
            {
                WriteLog(exception);
                MsgControl.ShowWarning(exception.Message, Language.Warning, MessageBoxButtonCustom.OK);
            }
        }

        #endregion

        #region Cancel

        /// <summary>
        /// Cancel.
        /// </summary>
        private void Cancel()
        {
            if (_reminder.IsDirty)
            {
                _reminder.ToModelAndRaise();
            }

            Close(false);
        }

        #endregion

        #region Close

        /// <summary>
        /// Close popup.
        /// </summary>
        private void Close(bool result)
        {
            FindOwnerWindow(this).DialogResult = result;
        }

        #endregion

        #region Initialize

        /// <summary>
        /// Initialize data.
        /// </summary>
        private void Initialize()
        {
            try
            {
                base_GuestRepository guestRepository = new base_GuestRepository();
                string employeeMark = MarkType.Employee.ToDescription();
                UserList = new CollectionBase<base_GuestModel>(guestRepository.GetAll(x =>
                    !x.IsPurged && x.IsActived && x.Mark == employeeMark).Select(x => new base_GuestModel(x, false)));
                UserList.Insert(0, new base_GuestModel());
            }
            catch (Exception exception)
            {
                WriteLog(exception);
                MsgControl.ShowWarning(exception.Message, Language.Warning, MessageBoxButtonCustom.OK);
            }
        }

        #endregion

        #region CalculateTime

        /// <summary>
        /// Calculate time.
        /// </summary>
        private void CalculateTime(base_ReminderModel reminder)
        {
            DateTime current;
            DateTime reminderTime;
            DateTime oldTime;
            DateTime reminderOldTime;

            switch ((ReminderRepeat)reminder.Repeat)
            {
                case ReminderRepeat.Once:
                case ReminderRepeat.Daily:

                    // Lay thoi gian nhac nho dang chon.
                    current = DateTime.Now;
                    reminderTime = new DateTime(current.Year, current.Month, current.Day, reminder.Time.Hour, reminder.Time.Minute, 0);
                    // Neu da qua thi nhac o ngay tiep theo, nguoc lai thi chuan bi nhac.
                    if (DateTime.Compare(reminderTime, GetCurrentDateTime()) <= 0)
                    {
                        reminder.Time = GetNextDate(reminderTime);
                    }
                    else
                    {
                        reminder.Time = reminderTime;
                    }

                    break;

                case ReminderRepeat.Weekly:

                    // Lay thoi gian nhac nho dang chon.
                    current = DateTime.Now;
                    reminderTime = new DateTime(current.Year, current.Month, current.Day, reminder.Time.Hour, reminder.Time.Minute, 0);
                    oldTime = reminder.base_Reminder.Time.Date;
                    reminderOldTime = new DateTime(oldTime.Year, oldTime.Month, oldTime.Day, reminder.Time.Hour, reminder.Time.Minute, 0);
                    // Neu da qua thi nhac o tuan tiep theo, nguoc lai thi chuan bi nhac.
                    if (DateTime.Compare(reminderTime, GetCurrentDateTime()) <= 0)
                    {
                        // Chi tinh lai thoi gian nhac o tuan tiep theo khi thoi gian cu truoc ngay hien tai hoac hon ngay hien tai qua 1 tuan.
                        if (oldTime < current || oldTime.AddDays(-7) > current)
                        {
                            reminder.Time = GetDateInNextWeek(reminderTime);
                        }
                    }
                    else
                    {
                        // Chuan bi nhac khi thoi gian nhac truoc do da qua.
                        if (DateTime.Compare(reminderOldTime, GetCurrentDateTime()) <= 0)
                        {
                            reminder.Time = reminderTime;
                        }
                    }

                    break;

                case ReminderRepeat.Monthly:

                    // Lay thoi gian nhac nho dang chon.
                    current = DateTime.Now;
                    reminderTime = new DateTime(current.Year, current.Month, current.Day, reminder.Time.Hour, reminder.Time.Minute, 0);
                    oldTime = reminder.base_Reminder.Time.Date;
                    reminderOldTime = new DateTime(oldTime.Year, oldTime.Month, oldTime.Day, reminder.Time.Hour, reminder.Time.Minute, 0);
                    // Neu da qua thi nhac o thang tiep theo, nguoc lai thi chuan bi nhac.
                    if (DateTime.Compare(reminderTime, GetCurrentDateTime()) <= 0)
                    {
                        // Chi tinh lai thoi gian nhac o thang tiep theo khi thoi gian cu truoc ngay hien tai hoac hon ngay hien tai qua 1 thang.
                        if (oldTime < current || oldTime.AddDays(-38) > current)
                        {
                            reminder.Time = GetDateInNextMonth(reminderTime);
                        }
                    }
                    else
                    {
                        // Chuan bi nhac khi thoi gian nhac truoc do da qua.
                        if (DateTime.Compare(reminderOldTime, GetCurrentDateTime()) <= 0)
                        {
                            reminder.Time = reminderTime;
                        }
                    }

                    break;

                default:

                    current = DateTime.Now;
                    reminderTime = new DateTime(current.Year, current.Month, current.Day, reminder.Time.Hour, reminder.Time.Minute, 0);
                    if (DateTime.Compare(reminderTime, GetCurrentDateTime()) <= 0)
                    {
                        reminder.Time = GetNextDate(reminderTime);
                    }
                    else
                    {
                        reminder.Time = reminderTime;
                    }

                    break;
            }
        }

        #endregion

        #region GetDateInNextMonth

        private DateTime GetDateInNextMonth(DateTime date)
        {
            DateTime newDate = date.AddMonths(1);

            while (newDate.DayOfWeek != date.DayOfWeek)
            {
                newDate = newDate.AddDays(1);
            }

            return newDate;
        }

        #endregion

        #region GetDateInNextWeek

        private DateTime GetDateInNextWeek(DateTime now)
        {
            return now.AddDays(7);
        }

        #endregion

        #region GetNextDate

        private DateTime GetNextDate(DateTime now)
        {
            return now.AddDays(1);
        }

        #endregion

        #region GetCurrentDateTime

        /// <summary>
        /// Get current date time.
        /// </summary>
        private DateTime GetCurrentDateTime()
        {
            DateTime current = DateTime.Now;
            return new DateTime(current.Year, current.Month, current.Day, current.Hour, current.Minute, 0);
        }

        #endregion

        #region GetCurrentDate

        /// <summary>
        /// Get current date.
        /// </summary>
        private DateTime GetCurrentDate()
        {
            return DateTime.Now.Date;
        }

        #endregion

        #region GetDateTime

        /// <summary>
        /// Get date time.
        /// </summary>
        private DateTime GetDateTime(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, 0);
        }

        #endregion

        #endregion

        #region WriteLog

        private void WriteLog(Exception exception)
        {
            _log4net.Error(string.Format("Message: {0}. Source: {1}.", exception.Message, exception.Source));
            if (exception.InnerException != null)
            {
                _log4net.Error(exception.InnerException.ToString());
            }
        }

        #endregion
    }
}
