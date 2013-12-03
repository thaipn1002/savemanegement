﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Input;
using CPC.Control;
using CPC.Helper;
using CPC.POS.Database;
using CPC.POS.Model;
using CPC.POS.Repository;
using CPC.POS.View;
using CPC.Toolkit.Base;
using CPC.Toolkit.Command;
using CPCToolkitExtLibraries;
using Microsoft.Win32;
using MessageBoxControl;

namespace CPC.POS.ViewModel
{
    class EmployeeViewModel : ViewModelBase
    {
        #region Define
        public RelayCommand NewCommand { get; private set; }
        public RelayCommand<object> SaveCommand { get; private set; }
        public RelayCommand DeleteCommand { get; private set; }
        public RelayCommand<object> SearchCommand { get; private set; }
        public RelayCommand<object> DoubleClickViewCommand { get; private set; }
        public RelayCommand NoteCommand { get; private set; }
        public RelayCommand<object> DuplicateCommand { get; private set; }
        public RelayCommand<object> EditCommand { get; private set; }
        private base_GuestRepository _guestRepository = new base_GuestRepository();
        private base_GuestAdditionalRepository _guestAdditionalRepository = new base_GuestAdditionalRepository();
        private base_GuestAddressRepository _guestAddressRepository = new base_GuestAddressRepository();
        private base_ResourcePhotoRepository _photoRepository = new base_ResourcePhotoRepository();
        private base_SaleCommissionRepository _saleCommissionRepository = new base_SaleCommissionRepository();

        private BackgroundWorker _bgWorker = new BackgroundWorker { WorkerReportsProgress = true };

        private string _employeeMark = MarkType.Employee.ToDescription();
        #endregion

        #region Constructors

        public EmployeeViewModel()
        {
            _ownerViewModel = App.Current.MainWindow.DataContext;
            StickyManagementViewModel = new PopupStickyViewModel();
            this.InitialCommand();
            Parameter = new Common();
        }

        public EmployeeViewModel(bool isList, object param = null)
            : this()
        {
            this.ChangeSearchMode(isList, param);
        }

        #endregion

        #region Properties

        #region IsDirty
        /// <summary>
        /// Gets or sets the IsDirty.
        /// </summary>
        public bool IsDirty
        {
            get
            {
                if (SelectedItemEmployee == null)
                    return false;
                return (SelectedItemEmployee.IsDirty
                    || (this.SelectedItemEmployee.EmployeeFingerprintCollection != null && this.SelectedItemEmployee.EmployeeFingerprintCollection.Any(x => x.IsDirty))
                    || (this.SelectedItemEmployee.AddressControlCollection != null && this.SelectedItemEmployee.AddressControlCollection.IsEditingData)
                    || (this.SelectedItemEmployee.PhotoCollection != null && this.SelectedItemEmployee.PhotoCollection.IsDirty)
                    || this.SelectedItemEmployee.PersonalInfoModel.IsDirty);
            }
        }
        #endregion

        #region EmployeeCollection
        /// <summary>
        /// Gets or sets the WorkScheduleCollection.
        /// </summary>
        public ObservableCollection<base_GuestModel> _employeeCollection = new ObservableCollection<base_GuestModel>();
        public ObservableCollection<base_GuestModel> EmployeeCollection
        {
            get
            {
                return _employeeCollection;
            }
            set
            {
                if (_employeeCollection != value)
                {
                    _employeeCollection = value;
                    OnPropertyChanged(() => EmployeeCollection);
                }
            }

        }
        #endregion

        #region SelectedItemEmployee
        /// <summary>
        /// Gets or sets the SelectedItemEmployee.
        /// </summary>
        private base_GuestModel _selectedItemEmployee;
        public base_GuestModel SelectedItemEmployee
        {
            get
            {
                return _selectedItemEmployee;
            }
            set
            {
                if (_selectedItemEmployee != value)
                {
                    _selectedItemEmployee = value;
                    OnPropertyChanged(() => SelectedItemEmployee);
                }
            }
        }
        #endregion

        #region Parameter
        /// <summary>
        /// Gets or sets the SelectedItemEmployee.
        /// </summary>
        private Common _parameter;
        public Common Parameter
        {
            get
            {
                return _parameter;
            }
            set
            {
                _parameter = value;
                OnPropertyChanged(() => Parameter);
            }
        }

        #endregion

        #region IsSearchMode
        private bool isSearchMode = false;
        /// <summary>
        /// Search Mode: 
        /// true open the Search grid.
        /// false close the search grid and open data entry.
        /// </summary>
        public bool IsSearchMode
        {
            get { return isSearchMode; }
            set
            {
                if (value != isSearchMode)
                {
                    isSearchMode = value;
                    OnPropertyChanged(() => IsSearchMode);
                }
            }
        }
        #endregion

        #region IsAdvanceMode
        private bool _isAdvanceMode;
        /// <summary>
        /// Gets or sets the IsAdvanceMode.
        /// Using for Search. False is a simple Search
        /// </summary>
        public bool IsAdvanceMode
        {
            get { return _isAdvanceMode; }
            set
            {
                if (_isAdvanceMode != value)
                {
                    _isAdvanceMode = value;
                    OnPropertyChanged(() => IsAdvanceMode);
                }
            }
        }
        #endregion

        #region TotalItem
        /// <summary>
        /// Gets or sets the CountFilter For Search Control.
        /// </summary>
        public int TotalItem
        {
            get
            {
                return 0;
            }
        }
        #endregion

        #region AddressTypeCollection
        private AddressTypeCollection _addressTypeCollection;
        /// <summary>
        /// Gets or sets the AddressTypeCollection.
        /// </summary>
        public AddressTypeCollection AddressTypeCollection
        {
            get { return _addressTypeCollection; }
            set
            {
                if (_addressTypeCollection != value)
                {
                    _addressTypeCollection = value;
                    OnPropertyChanged(() => AddressTypeCollection);
                }
            }
        }
        #endregion

        #region NumberOfItems
        /// <summary>
        /// Gets or sets the NumberOfItems.
        /// </summary>
        private int _numberOfItems;
        public int NumberOfItems
        {
            get
            {
                return _numberOfItems;
            }
            set
            {
                _numberOfItems = value;
                OnPropertyChanged(() => NumberOfItems);
            }
        }

        #endregion

        #region DisplayItems
        /// <summary>
        /// Gets or sets the DisplayItems.
        /// </summary>
        private int _displayItems = 10;
        public int DisplayItems
        {
            get
            {
                return _displayItems;
            }
            set
            {
                _displayItems = value;
                OnPropertyChanged(() => DisplayItems);
            }
        }

        #endregion

        #region CurrentPageIndex
        /// <summary>
        /// Gets or sets the CurrentPageIndex.
        /// </summary>
        private int _currentPageIndex;
        public int CurrentPageIndex
        {
            get
            {
                return _currentPageIndex;
            }
            set
            {
                _currentPageIndex = value;
                OnPropertyChanged(() => CurrentPageIndex);
            }
        }

        #endregion

        #region SearchOption
        private int _searchOption;
        /// <summary>
        /// Gets or sets the SearchOption.
        /// </summary>
        public int SearchOption
        {
            get { return _searchOption; }
            set
            {
                if (_searchOption != value)
                {
                    _searchOption = value;
                    OnPropertyChanged(() => SearchOption);
                    if (!string.IsNullOrWhiteSpace(FilterText))
                        OnSearchCommandExecute(FilterText);
                }
            }
        }
        #endregion

        #region FilterText & Keyword
        private string _filterText;
        /// <summary>
        /// Gets or sets the FilterText.
        /// <para>Keyword user input but not press enter</para>
        /// <remarks>Binding in textbox keyword</remarks>
        /// </summary>
        public string FilterText
        {
            get { return _filterText; }
            set
            {
                if (_filterText != value)
                {
                    _filterText = value;
                    OnPropertyChanged(() => FilterText);
                }
            }
        }

        public string Keyword { get; set; }
        #endregion

        #region SearchAlert
        private string _searchAlert;
        /// <summary>
        /// Gets or sets the SearchAlert.
        /// </summary>
        public string SearchAlert
        {
            get { return _searchAlert; }
            set
            {
                if (_searchAlert != value)
                {
                    _searchAlert = value;
                    OnPropertyChanged(() => SearchAlert);
                }
            }
        }
        #endregion

        #region NotePopupCollection

        /// <summary>
        /// Gets or sets the NotePopupCollection.
        /// </summary>
        public ObservableCollection<PopupContainer> NotePopupCollection { get; set; }

        /// <summary>
        /// Gets the ShowOrHiddenNote
        /// </summary>
        public string ShowOrHiddenNote
        {
            get
            {
                if (NotePopupCollection.Count == 0)
                    return "Show Stickies";
                else if (NotePopupCollection.Count == SelectedItemEmployee.ResourceNoteCollection.Count && NotePopupCollection.Any(x => x.IsVisible))
                    return "Hide Stickies";
                else
                    return "Show Stickies";
            }
        }
        #endregion

        #region IsSpouse
        private bool? _isSpouse = false;
        public bool? IsSpouse
        {
            get { return _isSpouse; }
            set
            {
                if (value != _isSpouse)
                {
                    _isSpouse = value;
                    OnPropertyChanged(() => IsSpouse);
                }
            }
        }
        #endregion

        #region IsEmergency
        private bool? _isEmergency = false;
        public bool? IsEmergency
        {
            get { return _isEmergency; }
            set
            {
                if (value != _isEmergency)
                {
                    _isEmergency = value;
                    OnPropertyChanged(() => IsEmergency);
                }
            }
        }
        #endregion

        #region StateCollection
        private ObservableCollection<ComboItem> _stateCollection;
        /// <summary>
        /// Gets or sets the StateCollection.
        /// </summary>
        public ObservableCollection<ComboItem> StateCollection
        {
            get { return _stateCollection; }
            set
            {
                if (_stateCollection != value)
                {
                    _stateCollection = value;
                    OnPropertyChanged(() => StateCollection);
                }
            }
        }
        #endregion

        #region JobTitleCollection
        private ObservableCollection<ComboItem> _jobTitleCollection;
        /// <summary>
        /// Gets or sets the JobTitleCollection.
        /// </summary>
        public ObservableCollection<ComboItem> JobTitleCollection
        {
            get { return _jobTitleCollection; }
            set
            {
                if (_jobTitleCollection != value)
                {
                    _jobTitleCollection = value;
                    OnPropertyChanged(() => JobTitleCollection);
                }
            }
        }
        #endregion

        #region IsForceFocused
        private bool _isForceFocused;
        /// <summary>
        /// Gets or sets the IsForceFocused.
        /// </summary>
        public bool IsForceFocused
        {
            get { return _isForceFocused; }
            set
            {
                if (_isForceFocused != value)
                {
                    _isForceFocused = value;
                    OnPropertyChanged(() => IsForceFocused);
                }
            }
        }
        #endregion

        #region IsManualGenerate
        /// <summary>
        /// Gets the IsManualGenerate.
        /// </summary>
        public bool IsManualGenerate
        {
            get
            {
                if (Define.CONFIGURATION == null)
                    return false;
                return Define.CONFIGURATION.IsManualGenerate;
            }
        }
        #endregion

        #region ResourceCollection
        private ObservableCollection<ItemModel> _resourceCollection;
        /// <summary>
        /// Gets or sets the IsForceFocused.
        /// </summary>
        public ObservableCollection<ItemModel> ResourceCollection
        {
            get { return _resourceCollection; }
            set
            {
                if (_resourceCollection != value)
                {
                    _resourceCollection = value;
                    OnPropertyChanged(() => ResourceCollection);
                }
            }
        }
        #endregion

        #endregion

        #region Commands Methods

        #region NewCommand
        /// <summary>
        /// Method to check whether the NewCommand command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnNewCommandCanExecute()
        {
            return true;
        }

        /// <summary>
        /// Method to invoke when the NewCommand command is executed.
        /// </summary>
        private void OnNewCommandExecute()
        {
            if (this.ChangeViewExecute(null))
            {
                this.CreateEmployee();
                //To set enable of detail grid.
                this.IsSearchMode = false;
            }
        }
        #endregion

        #region Save Command
        /// <summary>
        /// Method to check whether the SaveCommand command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnSaveCommandCanExecute(object param)
        {
            if (SelectedItemEmployee == null)
                return false;
            return IsValid && this.IsDirty &&
                (this.SelectedItemEmployee.AddressControlCollection != null && !this.SelectedItemEmployee.AddressControlCollection.IsErrorData);
            //if (!this.IsError() && this.IsEdit())
            //    return true;
            //return false;
        }
        /// <summary>
        /// Method to invoke when the SaveCommand command is executed.
        /// </summary>
        private void OnSaveCommandExecute(object param)
        {
            // TODO: Handle command logic here
            this.SaveEmployee();
        }
        #endregion

        #region DeleteCommand
        /// <summary>
        /// Method to check whether the DeleteCommand command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnDeleteCommandCanExecute()
        {
            if (SelectedItemEmployee == null)
                return false;
            return !SelectedItemEmployee.IsNew && !IsDirty;
        }

        /// <summary>
        /// Method to invoke when the DeleteCommand command is executed.
        /// </summary>
        private void OnDeleteCommandExecute()
        {
            MessageBoxResultCustom msgResult = MsgControl.ShowWarning(Language.Text4, Language.DeleteItems, MessageBoxButtonCustom.YesNo, MessageBoxImage.Warning);
            if (msgResult.Is(MessageBoxResultCustom.Yes))
            {
                if (SelectedItemEmployee.IsNew)
                {
                    // Remove all popup sticky
                    StickyManagementViewModel.DeleteAllResourceNote();

                    SelectedItemEmployee = null;
                    IsSearchMode = true;
                }
                else
                {
                    List<ItemModel> ItemModel = new List<ItemModel>();
                    string resource = SelectedItemEmployee.Resource.Value.ToString();
                    if (!_saleCommissionRepository.GetAll().Select(x => x.GuestResource).Contains(resource))
                    {
                        SelectedItemEmployee.IsPurged = true;
                        SaveEmployee();
                        EmployeeCollection.Remove(SelectedItemEmployee);
                        SelectedItemEmployee = EmployeeCollection.First();
                        NumberOfItems = NumberOfItems - 1;

                        // Remove all popup sticky
                        StickyManagementViewModel.DeleteAllResourceNote();

                        IsSearchMode = true;
                        App.WriteUserLog("Employee", "User deleted an employee.");
                    }
                    else
                    {
                        ItemModel.Add(new ItemModel { Id = SelectedItemEmployee.Id, Text = SelectedItemEmployee.GuestNo, Resource = resource });
                        _dialogService.ShowDialog<ProblemDetectionView>(_ownerViewModel, new ProblemDetectionViewModel(ItemModel, "Employee"), "Problem Detection");
                    }
                }
                IsSearchMode = true;
            }
        }
        #endregion

        #region SearchCommand
        /// <summary>
        /// Method to check whether the SearchCommand command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnSearchCommandCanExecute(object param)
        {
            return true;
        }

        private void OnSearchCommandExecute(object param)
        {
            try
            {
                SearchAlert = string.Empty;
                if ((param == null || string.IsNullOrWhiteSpace(param.ToString())) && SearchOption == 0)//Search All
                {
                    Expression<Func<base_Guest, bool>> predicate = CreatePredicateWithConditionSearch(Keyword);
                    LoadDataByPredicate(predicate, false, 0);


                }
                else if (param != null)
                {
                    Keyword = param.ToString();
                    if (SearchOption == 0)
                    {
                        //Notification when search condition is empty.
                        SearchAlert = Language.Text20;
                    }
                    else
                    {
                        Expression<Func<base_Guest, bool>> predicate = CreatePredicateWithConditionSearch(Keyword);
                        LoadDataByPredicate(predicate, false, 0);
                    }
                }
            }
            catch (Exception ex)
            {
                _log4net.Error(ex);
                throw;
            }
        }

        #endregion

        #region DoubleClickViewCommand
        /// <summary>
        /// Method to check whether the DoubleClickViewCommand command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnDoubleClickViewCommandCanExecute(object param)
        {
            return (param == null) ? false : true;
        }

        private void OnDoubleClickViewCommandExecute(object param)
        {
            if (param != null && IsSearchMode)
            {
                this.LoadManagerResource((param as base_GuestModel).Resource.Value);
                this.SelectedItemEmployee = param as base_GuestModel;
                this.LoadDataWhenSelected();
                IsSearchMode = false;
            }
            else if (!IsSearchMode)//Change from Edit form to Search Gird check view has dirty
            {
                if (this.ChangeViewExecute(null))
                    this.IsSearchMode = true;
            }
            else
                this.IsSearchMode = !this.IsSearchMode;//Change View To
        }

        #endregion

        #region LoadDataByStepCommand

        public RelayCommand<object> LoadStepCommand { get; private set; }
        /// <summary>
        /// Method to check whether the LoadStep command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnLoadStepCommandCanExecute(object param)
        {
            return true;
        }

        /// <summary>
        /// Method to invoke when the LoadStep command is executed.
        /// </summary>
        private void OnLoadStepCommandExecute(object param)
        {
            BackgroundWorker bgWorker = new BackgroundWorker { WorkerReportsProgress = true };
            Expression<Func<base_Guest, bool>> predicate = PredicateBuilder.True<base_Guest>();
            predicate = predicate.And(x => !x.IsPurged && x.Mark.Equals(_employeeMark));
            if (!string.IsNullOrWhiteSpace(FilterText))//Load Step Current With Search Current with Search
                predicate = CreatePredicateWithConditionSearch(Keyword);
            LoadDataByPredicate(predicate, false, EmployeeCollection.Count);
        }
        #endregion

        #region RecordFingerprintCommand
        /// <summary>
        /// Gets the RecordFingerprint Command.
        /// <summary>

        public RelayCommand<object> RecordFingerprintCommand { get; private set; }

        /// <summary>
        /// Method to check whether the RecordFingerprint command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnRecordFingerprintCommandCanExecute(object param)
        {
            if (SelectedItemEmployee == null || param == null)
                return false;
            return !FingerPrintNotSupport();
        }

        /// <summary>
        /// Method to invoke when the RecordFingerprint command is executed.
        /// </summary>
        private void OnRecordFingerprintCommandExecute(object param)
        {
            bool rightHand = bool.Parse(param.ToString());
            RecordFingerprintViewModel viewModel = new RecordFingerprintViewModel();
            viewModel.IsLeft = !rightHand;
            bool? result = _dialogService.ShowDialog<RecordFingerprintView>(_ownerViewModel, viewModel, "Register right fingerprint");
            if (result.HasValue && result.Value)
            {
                base_GuestFingerPrintModel employeeFingerPrintModel = this.SelectedItemEmployee.EmployeeFingerprintCollection.SingleOrDefault(x => x.FingerIndex == viewModel.FingerID && x.HandFlag == rightHand);
                if (employeeFingerPrintModel != null)
                {
                    employeeFingerPrintModel.DateUpdated = DateTime.Now;
                    employeeFingerPrintModel.FingerIndex = viewModel.FingerID;
                    employeeFingerPrintModel.FingerPrintImage = viewModel.Temp;
                }
                else
                {
                    employeeFingerPrintModel = new base_GuestFingerPrintModel();
                    employeeFingerPrintModel.HandFlag = rightHand;
                    employeeFingerPrintModel.FingerIndex = viewModel.FingerID;
                    employeeFingerPrintModel.FingerPrintImage = viewModel.Temp;
                    employeeFingerPrintModel.DateUpdated = DateTime.Now;
                    this.SelectedItemEmployee.EmployeeFingerprintCollection.Add(employeeFingerPrintModel);
                }

                //Set has FingerPrint
                this.SelectedItemEmployee.HasFingerPrintRight = this.SelectedItemEmployee.EmployeeFingerprintCollection.Any(x => x.HandFlag);

                this.SelectedItemEmployee.HasFingerPrintLeft = this.SelectedItemEmployee.EmployeeFingerprintCollection.Any(x => !x.HandFlag);
            }
        }
        #endregion

        #region DeletesCommand

        /// <summary>
        /// Gets the DeleteCommand command.
        /// </summary>
        public ICommand DeletesCommand { get; private set; }

        /// <summary>
        /// Method to check whether the DeleteCommand command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnDeletesCommandCanExecute(object param)
        {
            return (param == null || (param is ObservableCollection<object> && (param as ObservableCollection<object>).Count == 0)) ? false : true;
        }

        /// <summary>
        /// Method to invoke when the DeleteCommand command is executed.
        /// </summary>
        private void OnDeletesCommandExecute(object param)
        {
            MessageBoxResultCustom msgResult = MsgControl.ShowWarning(Language.Text4, Language.DeleteItems, MessageBoxButtonCustom.YesNo, MessageBoxImage.Warning);
            if (msgResult.Is(MessageBoxResultCustom.Yes))
            {
                bool flag = false;
                string employeeID = string.Empty;
                List<ItemModel> ItemModel = new List<ItemModel>();
                for (int i = 0; i < (param as ObservableCollection<object>).Count; i++)
                {
                    base_GuestModel model = (param as ObservableCollection<object>)[i] as base_GuestModel;
                    string resource = model.Resource.Value.ToString();
                    if (!_saleCommissionRepository.GetAll().Select(x => x.GuestResource).Contains(resource))
                    {
                        model.IsPurged = true;
                        model.ToEntity();
                        _guestRepository.Commit();
                        model.EndUpdate();
                        this.EmployeeCollection.Remove(model);
                        NumberOfItems = NumberOfItems - 1;

                        // Remove all popup sticky
                        StickyManagementViewModel.DeleteAllResourceNote(model.ResourceNoteCollection);

                        i--;
                        employeeID += employeeID + model.GuestNo;
                    }
                    else
                    {
                        ItemModel.Add(new ItemModel { Id = model.Id, Text = model.GuestNo, Resource = resource });
                        flag = true;
                    }
                }
                if (flag)
                    _dialogService.ShowDialog<ProblemDetectionView>(_ownerViewModel, new ProblemDetectionViewModel(ItemModel, "Employee"), "Problem Detection");
                if (ItemModel.Count < (param as ObservableCollection<object>).Count)
                    App.WriteUserLog("Employee", "User deleted employee(s)." + employeeID);
            }
        }
        #endregion

        #region DuplicateCommand
        /// <summary>
        /// Method to check whether the DuplicateCommand command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnDuplicateCommandCanExecute(object param)
        {
            return (param == null || (param is ObservableCollection<object> && ((param as ObservableCollection<object>).Count == 0 || (param as ObservableCollection<object>).Count > 1))) ? false : true;
        }

        /// <summary>
        /// Method to invoke when the DuplicateCommand command is executed.
        /// </summary>
        private void OnDuplicateCommandExecute(object param)
        {
            try
            {
                //To set enable of detail grid.
                this.IsSearchMode = false;
                this.CreateDuplicate((param as ObservableCollection<object>)[0] as base_GuestModel);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("OnDuplicateCommandExecute" + ex.ToString());
            }

        }

        private void CreateDuplicate(base_GuestModel GuestModel)
        {
            this.SelectedItemEmployee = new base_GuestModel();
            this.SelectedItemEmployee.MiddleName = GuestModel.MiddleName;
            this.SelectedItemEmployee.LastName = GuestModel.LastName;
            this.SelectedItemEmployee.Company = GuestModel.Company;
            this.SelectedItemEmployee.Phone1 = GuestModel.Phone1;
            this.SelectedItemEmployee.Ext1 = GuestModel.Ext1;
            this.SelectedItemEmployee.Phone2 = GuestModel.Phone2;
            this.SelectedItemEmployee.Ext2 = GuestModel.Ext2;
            this.SelectedItemEmployee.Fax = GuestModel.Fax;
            this.SelectedItemEmployee.CellPhone = GuestModel.CellPhone;
            this.SelectedItemEmployee.Email = string.Empty;
            this.SelectedItemEmployee.Website = GuestModel.Website;
            this.SelectedItemEmployee.UserCreated = GuestModel.UserCreated;
            this.SelectedItemEmployee.UserUpdated = GuestModel.UserUpdated;
            this.SelectedItemEmployee.DateCreated = GuestModel.DateCreated;
            this.SelectedItemEmployee.DateUpdated = GuestModel.DateUpdated;
            this.SelectedItemEmployee.IsPurged = GuestModel.IsPurged;
            this.SelectedItemEmployee.GuestTypeId = GuestModel.GuestTypeId;
            this.SelectedItemEmployee.IsActived = GuestModel.IsActived;
            this.SelectedItemEmployee.GuestNo = DateTime.Now.ToString(Define.GuestNoFormat);
            this.SelectedItemEmployee.PositionId = GuestModel.PositionId;
            this.SelectedItemEmployee.Department = GuestModel.Department;
            this.SelectedItemEmployee.Mark = GuestModel.Mark;
            this.SelectedItemEmployee.AccountNumber = GuestModel.AccountNumber;
            this.SelectedItemEmployee.ParentId = GuestModel.ParentId;
            this.SelectedItemEmployee.IsRewardMember = GuestModel.IsRewardMember;
            this.SelectedItemEmployee.CheckLimit = GuestModel.CheckLimit;
            this.SelectedItemEmployee.CreditLimit = GuestModel.CreditLimit;
            this.SelectedItemEmployee.BalanceDue = GuestModel.BalanceDue;
            this.SelectedItemEmployee.AvailCredit = GuestModel.AvailCredit;
            this.SelectedItemEmployee.PastDue = GuestModel.PastDue;
            this.SelectedItemEmployee.IsPrimary = GuestModel.IsPrimary;
            this.SelectedItemEmployee.CommissionPercent = GuestModel.CommissionPercent;
            this.SelectedItemEmployee.TotalRewardRedeemed = GuestModel.TotalRewardRedeemed;
            this.SelectedItemEmployee.PurchaseDuringTrackingPeriod = GuestModel.PurchaseDuringTrackingPeriod;
            this.SelectedItemEmployee.RequirePurchaseNextReward = GuestModel.RequirePurchaseNextReward;
            this.SelectedItemEmployee.HireDate = GuestModel.HireDate;
            this.SelectedItemEmployee.IsBlockArriveLate = GuestModel.IsBlockArriveLate;
            this.SelectedItemEmployee.IsDeductLunchTime = GuestModel.IsDeductLunchTime;
            this.SelectedItemEmployee.IsBalanceOvertime = GuestModel.IsBalanceOvertime;
            this.SelectedItemEmployee.LateMinutes = GuestModel.LateMinutes;
            this.SelectedItemEmployee.OvertimeOption = GuestModel.OvertimeOption;
            this.SelectedItemEmployee.OTLeastMinute = GuestModel.OTLeastMinute;
            this.SelectedItemEmployee.IsTrackingHour = GuestModel.IsTrackingHour;
            this.SelectedItemEmployee.TermDiscount = GuestModel.TermDiscount;
            this.SelectedItemEmployee.TermNetDue = GuestModel.TermNetDue;
            this.SelectedItemEmployee.TermPaidWithinDay = GuestModel.TermPaidWithinDay;
            this.SelectedItemEmployee.PaymentTermDescription = GuestModel.PaymentTermDescription;
            this.SelectedItemEmployee.SaleRepId = GuestModel.SaleRepId;
            this.SelectedItemEmployee.Shift = GuestModel.Shift;
            this.SelectedItemEmployee.IdCard = GuestModel.IdCard;
            this.SelectedItemEmployee.IdCardImg = GuestModel.IdCardImg;
            this.SelectedItemEmployee.Remark = GuestModel.Remark;
            this.SelectedItemEmployee.GroupResource = GuestModel.GroupResource;
            //Load Address Model
            this.SelectedItemEmployee.AddressCollection = new ObservableCollection<base_GuestAddressModel>();
            foreach (var item in GuestModel.base_Guest.base_GuestAddress)
            {
                base_GuestAddressModel addressModel = new base_GuestAddressModel(item);
                addressModel.CreateGuestAddress();
                this.SelectedItemEmployee.AddressCollection.Add(addressModel);
            }
            //AddressCollection For "Address Control"
            this.SelectedItemEmployee.AddressControlCollection = new AddressControlCollection();
            //Set AddresssModel to address for "Address Control"
            foreach (base_GuestAddressModel guestAddressModel in this.SelectedItemEmployee.AddressCollection)
            {
                AddressControlModel addressControlModel = guestAddressModel.ToAddressControlModel();
                addressControlModel.IsDirty = true;
                addressControlModel.IsNew = true;
                this.SelectedItemEmployee.AddressControlCollection.Add(addressControlModel);
            }
            //Load PhotoCollection
            string resource = GuestModel.Resource.ToString();
            this.SelectedItemEmployee.PhotoCollection = new CollectionBase<base_ResourcePhotoModel>(
                   _photoRepository.GetAll(x => x.Resource.Equals(resource)).
                   Select(x => new base_ResourcePhotoModel(x)
                   {
                       IsDirty = true,
                       IsNew = true,
                       ImagePath = System.IO.Path.Combine(IMG_EMPLOYEE_DIRECTORY, GuestModel.GuestNo, x.LargePhotoFilename)
                   }));
            foreach (var itemImage in this.SelectedItemEmployee.PhotoCollection)
                itemImage.CreateResourcePhoto();

            if (this.SelectedItemEmployee.PhotoCollection.Count > 0)
                this.SelectedItemEmployee.PhotoDefault = this.SelectedItemEmployee.PhotoCollection.FirstOrDefault();
            else
                this.SelectedItemEmployee.PhotoDefault = new base_ResourcePhotoModel();

            this.SelectedItemEmployee.Resource = Guid.NewGuid();
            this.SelectedItemEmployee.FirstName = GuestModel.FirstName + "(Copy)";
            this.SelectedItemEmployee.ResourceNoteCollection = new CollectionBase<base_ResourceNoteModel>();
            this.StickyManagementViewModel.SetParentResource(this.SelectedItemEmployee.Resource.ToString(), this.SelectedItemEmployee.ResourceNoteCollection);
            this.SelectedItemEmployee.EmployeeFingerprintCollection = new CollectionBase<base_GuestFingerPrintModel>();
            if (GuestModel.PersonalInfoModel == null)
            {
                this.SelectedItemEmployee.PersonalInfoModel = new base_GuestProfileModel();
                this.IsSpouse = false;
                this.IsEmergency = false;
            }
            else
            {
                this.SelectedItemEmployee.PersonalInfoModel = GuestModel.PersonalInfoModel;
                this.IsSpouse = this.SelectedItemEmployee.PersonalInfoModel.IsSpouse;
                this.IsEmergency = this.SelectedItemEmployee.PersonalInfoModel.IsEmergency;
            }
            this.SelectedItemEmployee.PersonalInfoModel.CreateBase_GuestProfile();
            this.SelectedItemEmployee.PersonalInfoModel.IsNew = true;
            this.SelectedItemEmployee.PersonalInfoModel.IsDirty = true;
        }
        #endregion

        #region EditCommand
        /// <summary>
        /// Method to check whether the EditCommand command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnEditCommandCanExecute(object param)
        {
            return (param == null || (param is ObservableCollection<object> && ((param as ObservableCollection<object>).Count == 0 || (param as ObservableCollection<object>).Count > 1))) ? false : true;
        }

        /// <summary>
        /// Method to invoke when the EditCommand command is executed.
        /// </summary>
        private void OnEditCommandExecute(object param)
        {
            try
            {
                this.SelectedItemEmployee = (param as ObservableCollection<object>)[0] as base_GuestModel;
                this.LoadDataWhenSelected();
                //To set enable of detail grid.
                this.IsSearchMode = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("OnDuplicateCommandExecute" + ex.ToString());
            }

        }

        #endregion

        #region InsertDateStampCommand
        /// <summary>
        /// Gets the InsertDateStamp Command.
        /// <summary>

        public RelayCommand<object> InsertDateStampCommand { get; private set; }


        /// <summary>
        /// Method to check whether the InsertDateStamp command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnInsertDateStampCommandCanExecute(object param)
        {
            if (param == null)
                return false;
            return true;
        }


        /// <summary>
        /// Method to invoke when the InsertDateStamp command is executed.
        /// </summary>
        private void OnInsertDateStampCommandExecute(object param)
        {
            CPCToolkitExt.TextBoxControl.TextBox remarkTextBox = param as CPCToolkitExt.TextBoxControl.TextBox;
            SetValueControlHelper.InsertTimeStamp(remarkTextBox);
        }
        #endregion

        #region AddNewStateCommand
        /// <summary>
        /// Gets the AddNewState Command.
        /// <summary>

        public RelayCommand<object> AddNewStateCommand { get; private set; }


        /// <summary>
        /// Method to check whether the AddNewState command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnAddNewStateCommandCanExecute(object param)
        {
            return true;
        }

        /// <summary>
        /// Method to invoke when the AddNewState command is executed.
        /// </summary>
        private void OnAddNewStateCommandExecute(object param)
        {
            AddNewStateViewModel viewModel = new AddNewStateViewModel();
            bool? result = _dialogService.ShowDialog<AddNewStateView>(_ownerViewModel, viewModel, "Add New State");
            if (result == true)
            {
                StateCollection.Insert(StateCollection.Count, viewModel.ItemState);
                if (param != null)
                {
                    if (param.ToString().Equals("Personal"))
                    {
                        this.SelectedItemEmployee.PersonalInfoModel.State = viewModel.ItemState.ObjValue.ToString();
                    }
                    else
                    {
                        this.SelectedItemEmployee.PersonalInfoModel.SState = viewModel.ItemState.ObjValue.ToString();
                    }
                }
            }

        }
        #endregion

        #region AddNewJobTitleCommand
        /// <summary>
        /// Gets the AddNewJobTitleCommand Command.
        /// <summary>

        public RelayCommand<object> AddNewJobTitleCommand { get; private set; }


        /// <summary>
        /// Method to check whether the AddNewJobTitle command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnAddNewJobTitleCommandCanExecute(object param)
        {
            return true;
        }

        /// <summary>
        /// Method to invoke when the AddNewState command is executed.
        /// </summary>
        private void OnAddNewJobTitleCommandExecute(object param)
        {
            AddNewJobTitleViewModel viewModel = new AddNewJobTitleViewModel();
            bool? result = _dialogService.ShowDialog<AddNewJobTitleView>(_ownerViewModel, viewModel, "Add Job Title");
            if (result == true)
            {
                this.JobTitleCollection.Insert(JobTitleCollection.Count, viewModel.ItemJobTitle);
                this.SelectedItemEmployee.PositionId = viewModel.ItemJobTitle.Value;
            }

        }
        #endregion

        #endregion

        #region Private Methods

        #region InitialCommand
        /// <summary>
        /// To create commands when View is opened.
        /// </summary>
        private void InitialCommand()
        {
            // Route the commands
            this.NewCommand = new RelayCommand(OnNewCommandExecute, OnNewCommandCanExecute);
            this.SaveCommand = new RelayCommand<object>(OnSaveCommandExecute, OnSaveCommandCanExecute);
            this.DeleteCommand = new RelayCommand(OnDeleteCommandExecute, OnDeleteCommandCanExecute);
            this.SearchCommand = new RelayCommand<object>(OnSearchCommandExecute, OnSearchCommandCanExecute);
            this.DoubleClickViewCommand = new RelayCommand<object>(OnDoubleClickViewCommandExecute, OnDoubleClickViewCommandCanExecute);
            this.LoadStepCommand = new RelayCommand<object>(OnLoadStepCommandExecute, OnLoadStepCommandCanExecute);
            this.RecordFingerprintCommand = new RelayCommand<object>(OnRecordFingerprintCommandExecute, OnRecordFingerprintCommandCanExecute);
            this.DeletesCommand = new RelayCommand<object>(OnDeletesCommandExecute, OnDeletesCommandCanExecute);
            this.DuplicateCommand = new RelayCommand<object>(OnDuplicateCommandExecute, OnDuplicateCommandCanExecute);
            this.EditCommand = new RelayCommand<object>(OnEditCommandExecute, OnEditCommandCanExecute);
            ///To load AddressTypeCollection
            this.AddressTypeCollection = new CPCToolkitExtLibraries.AddressTypeCollection();
            this.AddressTypeCollection.Add(new AddressTypeModel { ID = 0, Name = Language.GetMsg("Address_Home") });
            this.AddressTypeCollection.Add(new AddressTypeModel { ID = 1, Name = Language.GetMsg("Address_Business") });
            this.AddressTypeCollection.Add(new AddressTypeModel { ID = 2, Name = Language.GetMsg("Address_Billing") });
            this.AddressTypeCollection.Add(new AddressTypeModel { ID = 3, Name = Language.GetMsg("Address_Shipping") });
            this.NotePopupCollection = new ObservableCollection<PopupContainer>();
            this.StateCollection = new ObservableCollection<ComboItem>(Common.States);
            this.JobTitleCollection = new ObservableCollection<ComboItem>(Common.JobTitles);
            this.NotePopupCollection.CollectionChanged += (sender, e) => { OnPropertyChanged(() => ShowOrHiddenNote); };
            this.InsertDateStampCommand = new RelayCommand<object>(OnInsertDateStampCommandExecute, OnInsertDateStampCommandCanExecute);
            this.AddNewStateCommand = new RelayCommand<object>(OnAddNewStateCommandExecute, OnAddNewStateCommandCanExecute);
            this.AddNewJobTitleCommand = new RelayCommand<object>(this.OnAddNewJobTitleCommandExecute, this.OnAddNewJobTitleCommandCanExecute);
        }
        #endregion

        #region CreateEmployee
        /// <summary>
        ///To create new an rmployeeModel and defaults value.
        /// </summary>
        private void CreateEmployee()
        {
            this.SelectedItemEmployee = new base_GuestModel();
            this.SelectedItemEmployee.Resource = Guid.NewGuid();
            this.SelectedItemEmployee.IsActived = true;
            this.SelectedItemEmployee.GuestTypeId = 1;
            this.SelectedItemEmployee.IsPrimary = false;
            this.SelectedItemEmployee.Company = string.Empty;
            this.SelectedItemEmployee.Department = string.Empty;
            this.SelectedItemEmployee.DateCreated = DateTime.Now;
            this.SelectedItemEmployee.UserCreated = Define.USER != null ? Define.USER.LoginName : string.Empty;
            this.SelectedItemEmployee.GuestNo = DateTime.Now.ToString(Define.GuestNoFormat);
            this.SelectedItemEmployee.Mark = MarkType.Employee.ToDescription();
            this.SelectedItemEmployee.PositionId = 0;
            this.SelectedItemEmployee.OvertimeOption = 0;
            this.SelectedItemEmployee.ResourceNoteCollection = new CollectionBase<base_ResourceNoteModel>();
            this.StickyManagementViewModel.SetParentResource(SelectedItemEmployee.Resource.ToString(), SelectedItemEmployee.ResourceNoteCollection);
            //Personal Info
            this.SelectedItemEmployee.PersonalInfoModel = new base_GuestProfileModel();
            this.IsSpouse = false;
            this.SelectedItemEmployee.PersonalInfoModel.IsSpouse = false;
            this.SelectedItemEmployee.PersonalInfoModel.SEmail = string.Empty;
            this.SelectedItemEmployee.PersonalInfoModel.DOB = DateTime.Today.AddYears(-10);
            this.IsEmergency = false;
            this.SelectedItemEmployee.PersonalInfoModel.IsEmergency = false;
            this.SelectedItemEmployee.PersonalInfoModel.Gender = Common.Gender.First().Value;
            this.SelectedItemEmployee.PersonalInfoModel.Marital = Common.MaritalStatus.First().Value;
            this.SelectedItemEmployee.PersonalInfoModel.SGender = Common.Gender.First().Value;
            this.SelectedItemEmployee.PersonalInfoModel.IsDirty = false;
            //Collection relation
            this.SelectedItemEmployee.AddressControlCollection = new AddressControlCollection { new AddressControlModel { IsDefault = true, AddressTypeID = 0, IsNew = true, IsDirty = false } };
            this.SelectedItemEmployee.PhotoCollection = new CollectionBase<base_ResourcePhotoModel>();
            this.SelectedItemEmployee.AddressCollection = new ObservableCollection<base_GuestAddressModel>();
            this.SelectedItemEmployee.AddressCollection.Add(new base_GuestAddressModel { AddressLine1 = string.Empty, City = string.Empty });
            this.SelectedItemEmployee.EmployeeFingerprintCollection = new CollectionBase<base_GuestFingerPrintModel>();
            this.SelectedItemEmployee.PropertyChanged += new PropertyChangedEventHandler(SelectedItemEmployee_PropertyChanged);
            //To load ManagerResource
            this.LoadManagerResource(this.SelectedItemEmployee.Resource.Value);
            this.SelectedItemEmployee.IsDirty = false;
        }


        #endregion

        #region SaveEmployee
        /// <summary>
        /// Function save Employee
        /// </summary>
        /// <param name="param"></param>
        private bool SaveEmployee()
        {
            bool result = true;
            try
            {
                //To close detail grid of Employee after saving data.
                if (this.SelectedItemEmployee.IsNew)
                    this.Insert();
                //To update item when it is edited.
                else
                    this.Update();
                this.SelectedItemEmployee.PersonalInfoModel.ToModelAndRaise();
                this.SelectedItemEmployee.PersonalInfoModel.EndUpdate();
                this.SelectedItemEmployee.ToModelAndRaise();
                this.SelectedItemEmployee.EndUpdate();
                this.SelectedItemEmployee.GuestCollection = this.EmployeeCollection;
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
                _log4net.Error(ex);
                MsgControl.ShowWarning(ex.ToString(), "Error", MessageBoxControl.MessageBoxButtonCustom.OK, MessageBoxImage.Error);
            }
            return result;
        }
        #endregion

        #region Insert
        /// <summary>
        /// To save when create new Employee
        /// </summary>
        private void Insert()
        {
            // To convert data from model to entity
            this.SelectedItemEmployee.ToEntity();
            //Mapping Personal Info
            if (SelectedItemEmployee.PersonalInfoModel != null)
            {
                //Mapping Personal Info
                SelectedItemEmployee.PersonalInfoModel.ToEntity();
                if (SelectedItemEmployee.PersonalInfoModel.IsNew)
                    SelectedItemEmployee.base_Guest.base_GuestProfile.Add(SelectedItemEmployee.PersonalInfoModel.base_GuestProfile);
                SelectedItemEmployee.PersonalInfoModel.EndUpdate();
            }

            ///Created by Thaipn.
            base_GuestAddressModel addressModel;
            bool firstAddress = true;
            //To insert an address. 
            //Convert from AddressControlCollection To AddressModel 
            foreach (AddressControlModel addressControlModel in this.SelectedItemEmployee.AddressControlCollection)
            {
                addressModel = new base_GuestAddressModel();
                addressModel.UserCreated = string.Empty;
                addressModel.ToModel(addressControlModel);
                addressModel.IsDefault = firstAddress;
                addressModel.DateCreated = DateTimeExt.Now;
                addressModel.EndUpdate();
                // To convert data from model to entity
                addressModel.ToEntity();
                this.SelectedItemEmployee.base_Guest.base_GuestAddress.Add(addressModel.base_GuestAddress);
                firstAddress = false;
                addressModel.EndUpdate();
                addressControlModel.IsDirty = false;
                addressControlModel.IsNew = false;
            }
            //Save FingerPrint
            if (this.SelectedItemEmployee.EmployeeFingerprintCollection != null)
            {
                foreach (base_GuestFingerPrintModel fingerPrintModel in this.SelectedItemEmployee.EmployeeFingerprintCollection)
                {
                    fingerPrintModel.ToEntity();
                    if (fingerPrintModel.IsNew)
                        SelectedItemEmployee.base_Guest.base_GuestFingerPrint.Add(fingerPrintModel.base_GuestFingerPrint);
                    fingerPrintModel.EndUpdate();

                }
            }
            this.SavePhotoResource(this.SelectedItemEmployee);
            //To commit image.
            _guestRepository.Add(this.SelectedItemEmployee.base_Guest);
            _guestRepository.Commit();
            // To update ID from entity to model
            this.SelectedItemEmployee.Id = this.SelectedItemEmployee.base_Guest.Id;
            // To turn off IsDirty & IsNew
            this.SelectedItemEmployee.EndUpdate();
            this.EmployeeCollection.Add(this.SelectedItemEmployee);
            App.WriteUserLog("Employee", "User inserted new employee.");
        }
        #endregion

        #region Update
        /// <summary>
        /// To update item when it was edited.
        /// </summary>
        private void Update()
        {
            this.SelectedItemEmployee.DateUpdated = DateTime.Now;
            this.SelectedItemEmployee.UserUpdated = Define.USER != null ? Define.USER.LoginName : string.Empty;
            // To map data from model to entity
            this.SelectedItemEmployee.ToEntity();
            //Mapping Personal Info
            if (SelectedItemEmployee.PersonalInfoModel != null && SelectedItemEmployee.PersonalInfoModel.IsDirty)
            {
                //Mapping Personal Info
                SelectedItemEmployee.PersonalInfoModel.ToEntity();
                if (SelectedItemEmployee.PersonalInfoModel.IsNew)
                    SelectedItemEmployee.base_Guest.base_GuestProfile.Add(SelectedItemEmployee.PersonalInfoModel.base_GuestProfile);
                SelectedItemEmployee.PersonalInfoModel.EndUpdate();
            }
            // Insert or update address
            // Created by Thaipn
            foreach (AddressControlModel addressControlModel in this.SelectedItemEmployee.AddressControlCollection.Where(x => x.IsDirty))
            {
                base_GuestAddressModel addressModel = new base_GuestAddressModel();
                // Insert new address
                if (addressControlModel.IsNew)
                {
                    addressModel.DateCreated = DateTimeExt.Now;
                    addressModel.UserCreated = string.Empty;
                    // Map date from AddressControlModel to AddressModel
                    addressModel.ToModel(addressControlModel);
                    // Map data from model to entity
                    addressModel.ToEntity();
                    SelectedItemEmployee.base_Guest.base_GuestAddress.Add(addressModel.base_GuestAddress);
                    addressModel.EndUpdate();
                }
                // Update address
                else
                {
                    base_GuestAddress address = SelectedItemEmployee.base_Guest.base_GuestAddress.SingleOrDefault(x => x.AddressTypeId == addressControlModel.AddressTypeID);
                    addressModel = new base_GuestAddressModel(address);
                    addressModel.DateUpdated = DateTimeExt.Now;
                    //addressModel.UserUpdated = string.Empty;
                    // Map date from AddressControlModel to AddressModel
                    addressModel.ToModel(addressControlModel);
                    addressModel.ToEntity();
                }

                // Update default address
                if (addressModel.IsDefault)
                    SelectedItemEmployee.AddressModel = addressModel;

                // Turn off IsDirty & IsNew
                addressModel.EndUpdate();

                addressControlModel.IsNew = false;
                addressControlModel.IsDirty = false;
            }

            //Save FingerPrint
            foreach (base_GuestFingerPrintModel fingerPrintModel in SelectedItemEmployee.EmployeeFingerprintCollection.Where(x => x.IsDirty))
            {
                fingerPrintModel.ToEntity();
                if (fingerPrintModel.IsNew)
                    SelectedItemEmployee.base_Guest.base_GuestFingerPrint.Add(fingerPrintModel.base_GuestFingerPrint);
                fingerPrintModel.EndUpdate();

            }

            //Save,Update or delete PhotoResource
            SavePhotoResource(this.SelectedItemEmployee);

            _guestRepository.Commit();
            // To turn off IsDirty & IsNew
            this.SelectedItemEmployee.EndUpdate();
            App.WriteUserLog("Employee", "User updated an employee.");
        }
        #endregion

        #region ChangeViewExecute
        /// <summary>
        /// To execute BarListExecute when user click Bar Button.
        /// </summary>
        /// <returns></returns>
        private bool ChangeViewExecute(bool? isClosing)
        {
            bool result = true;
            if (this.IsDirty)
            {
                MessageBoxResultCustom msgResult = MessageBoxResultCustom.None;
                msgResult = MsgControl.ShowWarning(Language.Text13, Language.Save, MessageBoxControl.MessageBoxButtonCustom.YesNoCancel, MessageBoxImage.Warning);
                if (msgResult.Is(MessageBoxResultCustom.Cancel))
                    return false;
                if (msgResult.Is(MessageBoxResultCustom.Yes))
                {
                    if (OnSaveCommandCanExecute(null))
                        //if (SaveCustomer())
                        result = SaveEmployee();
                    else //Has Error
                        result = false;
                    // Close all popup sticky
                    StickyManagementViewModel.CloseAllPopupSticky();
                }
                else if (msgResult.Is(MessageBoxResultCustom.No))
                {
                    if (SelectedItemEmployee.IsNew)
                    {
                        // Remove all popup sticky
                        this.StickyManagementViewModel.DeleteAllResourceNote();
                        if (isClosing.HasValue && !isClosing.Value)
                            this.IsSearchMode = true;
                    }
                    else //Old Item Rollback data
                    {
                        // Close all popup sticky
                        this.StickyManagementViewModel.CloseAllPopupSticky();
                        this.SelectedItemEmployee.ToModelAndRaise();
                        this.SetDataDefaultToModel(SelectedItemEmployee);
                        this.SetDataRelationToModel(SelectedItemEmployee);
                    }
                }
            }
            else
            {
                if (SelectedItemEmployee != null && SelectedItemEmployee.IsNew)
                {
                    // Remove all popup sticky
                    StickyManagementViewModel.DeleteAllResourceNote();
                }
                else
                    // Close all popup sticky
                    StickyManagementViewModel.CloseAllPopupSticky();

            }
            return result;
        }
        #endregion

        #region IsEdit
        /// <summary>
        ///To check has edit on form
        /// </summary>
        /// <returns></returns>
        private bool IsEdit()
        {
            if (this.SelectedItemEmployee == null)
                return false;

            return this.SelectedItemEmployee.IsDirty
                || SelectedItemEmployee.PersonalInfoModel.IsDirty
                || (this.SelectedItemEmployee.AddressControlCollection != null && this.SelectedItemEmployee.AddressControlCollection.IsEditingData)
                || (this.SelectedItemEmployee.PhotoCollection != null && this.SelectedItemEmployee.PhotoCollection.IsDirty);
        }

        // <summary>
        ///To check has error on form
        /// </summary>
        /// <returns></returns>
        private bool IsError()
        {
            if (this.SelectedItemEmployee == null)
                return false;
            if (this.IsValid || this.HasError())
                return true;
            return false;
            //return this.IsValid ;//&& (this.SelectedItemEmployee.AddressCollection == null || (this.SelectedItemEmployee.AddressCollection.Where(x => x.HasError).Count() > 0)));
        }

        private bool HasError()
        {
            if (this.SelectedItemEmployee.AddressCollection == null)
                return false;
            return (this.SelectedItemEmployee.AddressCollection.Where(x => x.HasError).Count() > 0);
        }
        #endregion

        #region LoadDataWhenSelected
        /// <summary>
        /// To load data when an item is selected.
        /// </summary>
        private void LoadDataWhenSelected()
        {
            this.SetDataRelationToModel(this.SelectedItemEmployee);
            this.SelectedItemEmployee.PropertyChanged += new PropertyChangedEventHandler(SelectedItemEmployee_PropertyChanged);
            this.SelectedItemEmployee.IsDirty = false;
        }
        #endregion

        #region Save Image
        /// <summary>
        /// Save images into folder if this ImageCollection have data.
        /// </summary>
        /// 
        string IMG_EMPLOYEE_DIRECTORY = System.IO.Path.Combine(Define.CONFIGURATION.DefautlImagePath, "Employee\\");
        private void SaveImage(base_ResourcePhotoModel model)
        {
            try
            {
                string imgGuestDirectory = IMG_EMPLOYEE_DIRECTORY + this.SelectedItemEmployee.GuestNo + "\\";
                if (!System.IO.Directory.Exists(imgGuestDirectory))
                    System.IO.Directory.CreateDirectory(imgGuestDirectory);
                ///To check file on client and copy file to server
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(model.ImagePath);
                if (fileInfo.Exists)
                {
                    ///To copy image to server
                    string filename = System.IO.Path.Combine(imgGuestDirectory, model.LargePhotoFilename);
                    System.IO.FileInfo file = new System.IO.FileInfo(filename);
                    if (!file.Exists)
                        fileInfo.CopyTo(filename, true);
                    model.ImagePath = filename;
                }
                else
                    model.ImagePath = string.Empty;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Save Image" + ex.ToString());
            }
        }
        #endregion

        #region InitialData
        /// <summary>
        /// To load data of Employee from DB.
        /// </summary>
        private void InitialData()
        {
            Expression<Func<base_Guest, bool>> predicate = PredicateBuilder.True<base_Guest>();
            if (!string.IsNullOrWhiteSpace(Keyword) && SearchOption > 0)//Load with Search Condition
                predicate = CreatePredicateWithConditionSearch(Keyword);
            else
                predicate = predicate.And(x => !x.IsPurged && x.Mark.Equals(_employeeMark));
            LoadDataByPredicate(predicate, true);
        }
        #endregion

        #region CreatePredicateWithConditionSearch
        /// <summary>
        /// Create predicate
        /// </summary>
        /// <returns></returns>
        private Expression<Func<base_Guest, bool>> CreatePredicateWithConditionSearch(string keyword)
        {
            Expression<Func<base_Guest, bool>> predicate = PredicateBuilder.True<base_Guest>();
            if (!string.IsNullOrWhiteSpace(keyword) && SearchOption > 0)
            {
                if (SearchOption.Has(SearchOptions.AccountNum))
                {
                    predicate = predicate.And(x => x.GuestNo.Contains(keyword.ToLower()));
                }
                if (SearchOption.Has(SearchOptions.FirstName))
                {
                    predicate = predicate.And(x => x.FirstName.ToLower().Contains(keyword.ToLower()));
                }
                if (SearchOption.Has(SearchOptions.LastName))
                {
                    predicate = predicate.And(x => x.LastName.ToLower().Contains(keyword.ToLower()));
                }
                if (SearchOption.Has(SearchOptions.Company))
                {
                    predicate = predicate.And(x => x.Company.ToLower().Contains(keyword.ToLower()));
                }
                if (SearchOption.Has(SearchOptions.Phone))
                {
                    predicate = predicate.And(x => x.Phone1.ToLower().Contains(keyword.ToLower()) || x.Phone2.ToLower().Contains(keyword.ToLower()));
                }
            }
            predicate = predicate.And(x => !x.IsPurged && x.Mark.Equals(_employeeMark));
            return predicate;
        }

        #endregion

        #region LoadDataByPredicate
        /// <summary>
        /// Method get Data from database
        /// <para>Using load on the first time</para>
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="refreshData"></param>
        /// <param name="currentIndex">index to load if index =0 , clear collection</param>
        private void LoadDataByPredicate(Expression<Func<base_Guest, bool>> predicate, bool refreshData = false, int currentIndex = 0)
        {
            BackgroundWorker bgWorker = new BackgroundWorker { WorkerReportsProgress = true };
            if (currentIndex == 0)
                EmployeeCollection.Clear();
            bgWorker.DoWork += (sender, e) =>
            {
                IsBusy = true;
                //Cout all Customer in Data base show on grid
                NumberOfItems = _guestRepository.GetIQueryable(predicate).Count();

                //Get data with range
                IList<base_Guest> employees = _guestRepository.GetRange(currentIndex, NumberOfDisplayItems, "It.Id", predicate);
                foreach (base_Guest employee in employees)
                {
                    bgWorker.ReportProgress(0, employee);
                }
            };

            bgWorker.ProgressChanged += (sender, e) =>
            {
                base_GuestModel employeeModel = new base_GuestModel((base_Guest)e.UserState);
                SetDataDefaultToModel(employeeModel);
                EmployeeCollection.Add(employeeModel);
            };

            bgWorker.RunWorkerCompleted += (sender, e) =>
            {
                IsBusy = false;
            };
            bgWorker.RunWorkerAsync();
        }

        #endregion

        #region SetDataDefaultToModel
        /// <summary>
        /// Set data default for
        /// </summary>
        /// <param name="employeeModel"></param>
        private void SetDataDefaultToModel(base_GuestModel employeeModel)
        {
            //Load PersonalInfoModel
            if (employeeModel.base_Guest.base_GuestProfile.Count > 0)
            {
                employeeModel.PersonalInfoModel = new base_GuestProfileModel(employeeModel.base_Guest.base_GuestProfile.First());
                this.IsSpouse = employeeModel.PersonalInfoModel.IsSpouse;
                this.IsEmergency = employeeModel.PersonalInfoModel.IsEmergency;
            }
            else
            {
                employeeModel.PersonalInfoModel = new base_GuestProfileModel();
                employeeModel.PersonalInfoModel.IsSpouse = false;
                employeeModel.PersonalInfoModel.IsEmergency = false;
                this.IsSpouse = false;
                this.IsEmergency = false;
                employeeModel.PersonalInfoModel.IsDirty = false;
            }
            //Load DefaultAdress Address
            if (employeeModel.base_Guest.base_GuestAddress.Count > 0)
                employeeModel.AddressModel = new base_GuestAddressModel(employeeModel.base_Guest.base_GuestAddress.SingleOrDefault(x => x.IsDefault));

            //Load Schedule
            if (employeeModel.base_Guest.base_GuestSchedule != null
                      && employeeModel.base_Guest.base_GuestSchedule.Count > 0
                      && employeeModel.base_Guest.base_GuestSchedule.Where(x => x.Status > 0).Count() > 0)
            {
                if (employeeModel.base_Guest.base_GuestSchedule.Count == 1)
                {
                    employeeModel.EmployeeWorkScheduleName = employeeModel.base_Guest.base_GuestSchedule.First().tims_WorkSchedule.WorkScheduleName;
                }
                var query = employeeModel.base_Guest.base_GuestSchedule.Where(x => x.Status > 0).LastOrDefault();
                if (query != null)
                    employeeModel.EmployeeWorkScheduleName = query.tims_WorkSchedule.WorkScheduleName;
            }
            else
                employeeModel.EmployeeWorkScheduleName = "Employee not work schedule";

            //Load FingerPrint
            employeeModel.EmployeeFingerprintCollection = new CollectionBase<base_GuestFingerPrintModel>(
                            employeeModel.base_Guest.base_GuestFingerPrint.Select(x => new base_GuestFingerPrintModel(x)));

            if (employeeModel.EmployeeFingerprintCollection.Count > 0)
            {
                employeeModel.HasFingerPrintLeft = employeeModel.EmployeeFingerprintCollection.Any(x => !x.HandFlag);
                employeeModel.HasFingerPrintRight = employeeModel.EmployeeFingerprintCollection.Any(x => x.HandFlag);
            }

            //Load Resource Photo
            LoadResourcePhoto(employeeModel);

            //Load resource note collection
            LoadResourceNoteCollection(employeeModel);

            employeeModel.IsDirty = false;
        }

        #endregion

        #region SetDataRelationToModel
        /// <summary>
        /// Load data relation in form.
        /// Using for initial data or rollback data
        /// <para>Item will be set : AddressCollection,AddressControlCollection,PhotoCollection</para>
        /// </summary>
        /// <param name="employeeModel"></param>
        private void SetDataRelationToModel(base_GuestModel employeeModel)
        {
            ///To load data from DB.
            if (employeeModel != null)
            {
                //Load Address Model
                employeeModel.AddressCollection = new ObservableCollection<base_GuestAddressModel>(employeeModel.base_Guest.base_GuestAddress.Select(x => new base_GuestAddressModel(x)));
                //AddressCollection For "Address Control"
                employeeModel.AddressControlCollection = new AddressControlCollection();
                //Set AddresssModel to address for "Address Control"
                foreach (base_GuestAddressModel guestAddressModel in employeeModel.AddressCollection)
                {
                    AddressControlModel addressControlModel = guestAddressModel.ToAddressControlModel();
                    addressControlModel.IsDirty = false;
                    employeeModel.AddressControlCollection.Add(addressControlModel);
                }

                //Load PhotoCollection
                LoadResourcePhoto(SelectedItemEmployee);

                StickyManagementViewModel.SetParentResource(SelectedItemEmployee.Resource.ToString(), SelectedItemEmployee.ResourceNoteCollection);

                employeeModel.IsDirty = false;
            }
        }

        #endregion

        #region LoadResourcePhoto
        /// <summary>
        /// Load Resource Photo Collection & DefaultPhoto for GuestModel
        /// </summary>
        /// <param name="guestModel"></param>
        private void LoadResourcePhoto(base_GuestModel guestModel)
        {
            if (guestModel.PhotoCollection == null)
            {
                string resource = guestModel.Resource.ToString();
                guestModel.PhotoCollection = new CollectionBase<base_ResourcePhotoModel>(
                    _photoRepository.GetAll(x => x.Resource.Equals(resource)).
                    Select(x => new base_ResourcePhotoModel(x)
                    {
                        ImagePath = System.IO.Path.Combine(IMG_EMPLOYEE_DIRECTORY, guestModel.GuestNo, x.LargePhotoFilename),
                        IsDirty = false
                    }));

                if (guestModel.PhotoCollection.Count > 0)
                    guestModel.PhotoDefault = guestModel.PhotoCollection.FirstOrDefault();
                else
                    guestModel.PhotoDefault = new base_ResourcePhotoModel();
            }
        }

        #endregion

        #region SavePhotoResource
        /// <summary>
        /// SaveNew,Update or detete photo to resource & set photo default for guestModel
        /// </summary>
        private void SavePhotoResource(base_GuestModel guestModel)
        {
            //To remove image deleted.
            if (guestModel.PhotoCollection.DeletedItems != null
                && guestModel.PhotoCollection.DeletedItems.Count > 0)
            {
                foreach (base_ResourcePhotoModel item in guestModel.PhotoCollection.DeletedItems)
                {
                    _photoRepository.Delete(item.base_ResourcePhoto);
                }
                _photoRepository.Commit();
                guestModel.PhotoCollection.DeletedItems.Clear();
            }

            //To update image.
            if (guestModel.PhotoCollection != null && guestModel.PhotoCollection.Count > 0)
            {
                foreach (base_ResourcePhotoModel photoModel in guestModel.PhotoCollection.Where(x => x.IsDirty))
                {
                    photoModel.Resource = guestModel.Resource.ToString();
                    photoModel.LargePhotoFilename = this.SelectedItemEmployee.GuestNo + Guid.NewGuid().ToString().Substring(0, 8) + new System.IO.FileInfo(photoModel.ImagePath).Extension;
                    //To map data from model to entity
                    photoModel.ToEntity();
                    if (photoModel.IsNew)
                        _photoRepository.Add(photoModel.base_ResourcePhoto);
                    //To save image to store.
                    this.SaveImage(photoModel);
                    _photoRepository.Commit();
                    //set Id
                    photoModel.Id = photoModel.base_ResourcePhoto.Id;
                    photoModel.EndUpdate();
                }

                if (guestModel.PhotoCollection.Count > 0)
                    guestModel.PhotoDefault = guestModel.PhotoCollection.FirstOrDefault();
                else
                    guestModel.PhotoDefault = new base_ResourcePhotoModel();
            }
        }
        #endregion

        #region FingerFrint
        /// <summary>
        /// Check Finger Print driver had setup 
        /// </summary>
        /// <returns>
        /// True : finger print setup
        /// False : Support
        /// </returns>
        private bool FingerPrintNotSupport()
        {
            try
            {
                RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\DigitalPersona");
                if (registryKey == null)
                    return true;
            }
            catch
            {
                return true;
            }
            return false;
        }
        #endregion

        #region CheckDuplicateGuestNo
        void SelectedItemEmployee_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base_GuestModel employeeModel = sender as base_GuestModel;

            switch (e.PropertyName)
            {
                case "GuestNo":
                    this.CheckDuplicateGuestNo(employeeModel);

                    break;
                default:
                    break;
            }
        }

        private bool CheckDuplicateGuestNo(base_GuestModel employeeModel)
        {
            bool result = false;
            try
            {
                lock (UnitOfWork.Locker)
                {
                    IQueryable<base_Guest> query = _guestRepository.GetIQueryable(x => x.Mark == this._employeeMark && x.Resource != employeeModel.Resource && x.GuestNo.Equals(employeeModel.GuestNo));
                    if (query.Any())
                        result = true;
                }
            }
            catch (Exception ex)
            {
                result = false;
                _log4net.Error(ex);
            }
            employeeModel.IsDuplicateGuestNo = result;
            return result;
        }
        #endregion

        #region LoadManagerResource
        private void LoadManagerResource(Guid resource)
        {
            this.ResourceCollection = new ObservableCollection<ItemModel>();
            var list = _guestRepository.GetIQueryable(x => x.Resource != resource && x.Mark.Equals(_employeeMark));
            ItemModel model;
            if (list != null)
            {
                this.ResourceCollection.Add(new ItemModel { Id = -1, Resource = string.Empty, Text = string.Empty });
                foreach (var item in list)
                {
                    model = new ItemModel();
                    model.Id = item.Id;
                    model.Resource = item.Resource.ToString();
                    model.Text = string.Format("{0} {1}", item.FirstName, item.LastName);
                    this.ResourceCollection.Add(model);
                }
            }
        }
        #endregion

        #endregion

        #region Override Methods

        #region LoadData

        public override void LoadData()
        {
            InitialData();
            //if (!this.IsSearchMode && this.SelectedItemEmployee == null)
            //    this.CreateEmployee();
        }
        #endregion

        public override void ChangeSearchMode(bool isList, object param = null)
        {
            if (param == null)
            {
                if (this.ChangeViewExecute(null))
                {
                    if (!isList)
                    {
                        this.CreateEmployee();
                        IsSearchMode = false;
                    }
                    else
                        IsSearchMode = true;
                }
            }
        }

        protected override bool OnViewChangingCommandCanExecute(bool isClosing)
        {
            return ChangeViewExecute(isClosing);
        }

        #endregion

        #region Note Module

        /// <summary>
        /// Initial resource note repository
        /// </summary>
        private base_ResourceNoteRepository _resourceNoteRepository = new base_ResourceNoteRepository();

        /// <summary>
        /// Get or sets the StickyManagementViewModel
        /// </summary>
        public PopupStickyViewModel StickyManagementViewModel { get; set; }

        /// <summary>
        /// Load resource note collection
        /// </summary>
        /// <param name="guestModel"></param>
        private void LoadResourceNoteCollection(base_GuestModel guestModel)
        {
            // Load resource note collection
            if (guestModel.ResourceNoteCollection == null)
            {
                string resource = guestModel.Resource.ToString();
                guestModel.ResourceNoteCollection = new CollectionBase<base_ResourceNoteModel>(
                    _resourceNoteRepository.GetAll(x => x.Resource.Equals(resource)).
                    Select(x => new base_ResourceNoteModel(x)));
            }
        }

        #endregion
    }
}