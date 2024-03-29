﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Input;
using CPC.Helper;
using CPC.POS.Database;
using CPC.POS.Model;
using CPC.POS.Repository;
using CPC.Toolkit.Base;
using CPC.Toolkit.Command;
using CPCToolkitExt.DataGridControl;
using MessageBoxControl;

namespace CPC.POS.ViewModel
{
    public class PromotionViewModel : ViewModelBase, IDataErrorInfo
    {
        #region Defines

        private base_PromotionRepository _promotionRepository = new base_PromotionRepository();
        private base_PromotionScheduleRepository _promotionScheduleRepository = new base_PromotionScheduleRepository();
        private base_PromotionAffectRepository _promotionAffectRepository = new base_PromotionAffectRepository();
        private base_GuestRepository _guestRepository = new base_GuestRepository();
        private base_DepartmentRepository _departmentRepository = new base_DepartmentRepository();

        #endregion

        #region Properties

        #region IsSearchMode

        private bool isSearchMode = false;
        /// <summary>
        /// Gets or sets a value that indicates whether the grid-search is open.
        /// </summary>
        /// <returns>true if open grid-search; otherwise, false.</returns>
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

        private ObservableCollection<base_PromotionModel> _promotionCollection = new ObservableCollection<base_PromotionModel>();
        /// <summary>
        /// Gets or sets the PromotionCollection.
        /// </summary>
        public ObservableCollection<base_PromotionModel> PromotionCollection
        {
            get { return _promotionCollection; }
            set
            {
                if (_promotionCollection != value)
                {
                    _promotionCollection = value;
                    OnPropertyChanged(() => PromotionCollection);
                }
            }
        }

        private base_PromotionModel _selectedPromotion;
        /// <summary>
        /// Gets or sets the SelectedPromotion.
        /// </summary>
        public base_PromotionModel SelectedPromotion
        {
            get { return _selectedPromotion; }
            set
            {
                if (_selectedPromotion != value)
                {
                    _selectedPromotion = value;
                    OnPropertyChanged(() => SelectedPromotion);
                }
            }
        }

        private ObservableCollection<ComboItem> _categoryCollection;
        /// <summary>
        /// Gets or sets the CategoryCollection.
        /// </summary>
        public ObservableCollection<ComboItem> CategoryCollection
        {
            get { return _categoryCollection; }
            set
            {
                if (_categoryCollection != value)
                {
                    _categoryCollection = value;
                    OnPropertyChanged(() => CategoryCollection);
                }
            }
        }

        private ObservableCollection<ComboItem> _vendorCollection;
        /// <summary>
        /// Gets or sets the VendorCollection.
        /// </summary>
        public ObservableCollection<ComboItem> VendorCollection
        {
            get { return _vendorCollection; }
            set
            {
                if (_vendorCollection != value)
                {
                    _vendorCollection = value;
                    OnPropertyChanged(() => VendorCollection);
                }
            }
        }

        private short _status;
        /// <summary>
        /// Gets or sets the Status.
        /// </summary>
        public short Status
        {
            get { return _status; }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged(() => Status);
                    if (SelectedPromotion != null)
                    {
                        if (OnStatusChanged(SelectedPromotion))
                            SelectedPromotion.Status = Status;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the HiddenWarningConflict.
        /// </summary>
        public bool HiddenWarningConflict
        {
            get { return !PromotionCollection.Any(x => x.IsConflict); }
        }

        private ObservableCollection<ComboItem> _promotionTypes = new ObservableCollection<ComboItem>();
        /// <summary>
        /// Gets or sets the PromotionTypes.
        /// </summary>
        public ObservableCollection<ComboItem> PromotionTypes
        {
            get { return _promotionTypes; }
            set
            {
                if (_promotionTypes != value)
                {
                    _promotionTypes = value;
                    OnPropertyChanged(() => PromotionTypes);
                }
            }
        }

        private ObservableCollection<ComboItem> _takeOffOptions = new ObservableCollection<ComboItem>();
        /// <summary>
        /// Gets or sets the TakeOffOptions.
        /// </summary>
        public ObservableCollection<ComboItem> TakeOffOptions
        {
            get { return _takeOffOptions; }
            set
            {
                if (_takeOffOptions != value)
                {
                    _takeOffOptions = value;
                    OnPropertyChanged(() => TakeOffOptions);
                }
            }
        }

        #region ListCheckBox PriceSchema

        private ObservableCollection<CheckBoxItemModel> _priceSchemaCollection;
        /// <summary>
        /// Gets or sets the PriceSchemaCollection.
        /// </summary>
        public ObservableCollection<CheckBoxItemModel> PriceSchemaCollection
        {
            get { return _priceSchemaCollection; }
            set
            {
                if (_priceSchemaCollection != value)
                {
                    _priceSchemaCollection = value;
                    OnPropertyChanged(() => PriceSchemaCollection);
                }
            }
        }

        public bool IsDirtyPriceSchemaCollection { get; set; }

        public bool IsCheckedPriceSchemaCollection
        {
            get
            {
                if (PriceSchemaCollection == null)
                    return true;
                return PriceSchemaCollection.Count(x => x.IsChecked) > 0;
            }
        }

        #endregion

        #region Search And Filter

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

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the PromotionViewModel class.
        /// </summary>
        public PromotionViewModel()
            : base()
        {
            _ownerViewModel = App.Current.MainWindow.DataContext;

            // Load static data
            LoadStaticData();

            InitialCommand();
        }

        /// <summary>
        /// Initializes a new instance of the PromotionViewModel class with parameter.
        /// </summary>
        /// <param name="isList">true if show list, otherwise, false.</param>
        public PromotionViewModel(bool isList, object param = null)
            : this()
        {
            ChangeSearchMode(isList, param);

            // Get permission
            GetPermission();
        }

        #endregion

        #region Command Methods

        #region SearchCommand

        /// <summary>
        /// Gets the SearchCommand command.
        /// </summary>
        public ICommand SearchCommand { get; private set; }

        /// <summary>
        /// Method to check whether the SearchCommand command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnSearchCommandCanExecute(object param)
        {
            return true;
        }

        /// <summary>
        /// Method to invoke when the SearchCommand command is executed.
        /// </summary>
        private void OnSearchCommandExecute(object param)
        {
            try
            {
                SearchAlert = string.Empty;
                if ((param == null || string.IsNullOrWhiteSpace(param.ToString())) && SearchOption == 0)//Search All
                {
                    // Load data by predicate
                    LoadDataByPredicate();
                }
                else if (param != null)
                {
                    Keyword = param.ToString();
                    if (SearchOption == 0)
                    {
                        // Alert: Search option is required
                        SearchAlert = "Search Option is required";
                    }
                    else
                    {
                        // Load data by predicate
                        LoadDataByPredicate();
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

        #region NewCommand

        /// <summary>
        /// Gets the NewCommand command.
        /// </summary>
        public ICommand NewCommand { get; private set; }

        /// <summary>
        /// Method to check whether the NewCommand command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnNewCommandCanExecute()
        {
            return AllowAddPromotion;
        }

        /// <summary>
        /// Method to invoke when the NewCommand command is executed.
        /// </summary>
        private void OnNewCommandExecute()
        {
            if (ShowNotification(null))
                NewPromotion();
            this.IsSearchMode = false;
        }

        #endregion

        #region EditCommand

        /// <summary>
        /// Gets the EditCommand command.
        /// </summary>
        public ICommand EditCommand { get; private set; }

        /// <summary>
        /// Method to check whether the EditCommand command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnEditCommandCanExecute(object param)
        {
            // Convert param to DataGridControl
            DataGridControl dataGridControl = param as DataGridControl;

            if (dataGridControl == null)
                return false;

            return dataGridControl.SelectedItems.Count == 1;
        }

        /// <summary>
        /// Method to invoke when the EditCommand command is executed.
        /// </summary>
        private void OnEditCommandExecute(object param)
        {
            // Convert param to DataGridControl
            DataGridControl dataGridControl = param as DataGridControl;

            // Edit selected item
            OnDoubleClickViewCommandExecute(dataGridControl.SelectedItem);
        }

        #endregion

        #region SaveCommand

        /// <summary>
        /// Gets the SaveCommand command.
        /// </summary>
        public ICommand SaveCommand { get; private set; }

        /// <summary>
        /// Method to check whether the SaveCommand command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnSaveCommandCanExecute()
        {
            return IsValid && IsEdit() && IsCheckedPriceSchemaCollection;
        }

        /// <summary>
        /// Method to invoke when the SaveCommand command is executed.
        /// </summary>
        private void OnSaveCommandExecute()
        {
            SavePromotion();
        }

        #endregion

        #region DeleteCommand

        /// <summary>
        /// Gets the DeleteCommand command.
        /// </summary>
        public ICommand DeleteCommand { get; private set; }

        /// <summary>
        /// Method to check whether the DeleteCommand command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnDeleteCommandCanExecute()
        {
            if (SelectedPromotion == null)
                return false;
            return !IsEdit() && !SelectedPromotion.IsNew;
        }

        /// <summary>
        /// Method to invoke when the DeleteCommand command is executed.
        /// </summary>
        private void OnDeleteCommandExecute()
        {
            MessageBoxResultCustom msgResult = MsgControl.ShowWarning("Bạn có muốn xóa chương trình khuyến mãi này?", "Thông báo", MessageBoxButtonCustom.YesNo);
            if (msgResult.Is(MessageBoxResultCustom.Yes))
            {
                if (SelectedPromotion.IsNew)
                {
                    //DeleteNote();
                    SelectedPromotion = null;
                }
                else if (IsValid)
                {
                    // Check and remove conflict promotion
                    CheckAndRemoveConflictPromotion(SelectedPromotion);

                    // Delete all promotion affect
                    SelectedPromotion.AffectDiscount = 0;
                    OnSavePromotionAffect();

                    // Delete promotion schedule
                    _promotionScheduleRepository.Delete(SelectedPromotion.PromotionScheduleModel.base_PromotionSchedule);

                    // Delete promotion
                    _promotionRepository.Delete(SelectedPromotion.base_Promotion);

                    // Accept changes
                    _promotionRepository.Commit();

                    // Turn off IsDirty & IsNew
                    SelectedPromotion.EndUpdate();
                    SelectedPromotion.PromotionScheduleModel.EndUpdate();
                    //SelectedPromotion.PromotionAffectModel.EndUpdate();

                    // Remove promotion from collection
                    PromotionCollection.Remove(SelectedPromotion);
                }
                else
                    return;
                IsSearchMode = true;
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
            // Convert param to DataGridControl
            DataGridControl dataGridControl = param as DataGridControl;

            if (dataGridControl == null)
                return false;

            return dataGridControl.SelectedItems.Count > 0;
        }

        /// <summary>
        /// Method to invoke when the DeleteCommand command is executed.
        /// </summary>
        private void OnDeletesCommandExecute(object param)
        {
            DataGridControl dataGridControl = param as DataGridControl;

            MessageBoxResultCustom msgResult = MsgControl.ShowWarning("Bạn có muốn xóa chương trình khuyến mãi này?", "Thông báo", MessageBoxButtonCustom.YesNo);

            if (msgResult.Is(MessageBoxResultCustom.Yes))
            {
                foreach (base_PromotionModel promotionItem in dataGridControl.SelectedItems.Cast<base_PromotionModel>().ToList())
                {
                    // Check and remove conflict promotion
                    CheckAndRemoveConflictPromotion(promotionItem);

                    // Delete all promotion affect
                    promotionItem.AffectDiscount = 0;
                    OnSavePromotionAffect(promotionItem);

                    // Delete promotion schedule
                    _promotionScheduleRepository.Delete(promotionItem.PromotionScheduleModel.base_PromotionSchedule);

                    // Delete promotion
                    _promotionRepository.Delete(promotionItem.base_Promotion);

                    // Accept changes
                    _promotionRepository.Commit();

                    // Turn off IsDirty & IsNew
                    promotionItem.EndUpdate();
                    promotionItem.PromotionScheduleModel.EndUpdate();

                    // Remove promotion from collection
                    PromotionCollection.Remove(promotionItem);
                }
            }
        }

        #endregion

        #region DoubleClickViewCommand

        /// <summary>
        /// Gets the DoubleClickViewCommand command.
        /// </summary>
        public ICommand DoubleClickViewCommand { get; private set; }

        /// <summary>
        /// Method to check whether the DoubleClickViewCommand command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnDoubleClickViewCommandCanExecute(object param)
        {
            return true;
        }

        /// <summary>
        /// Method to invoke when the DoubleClickViewCommand command is executed.
        /// </summary>
        private void OnDoubleClickViewCommandExecute(object param)
        {
            if (param != null && IsSearchMode)
            {
                SelectedPromotion = param as base_PromotionModel;

                // Update Status
                if (SelectedPromotion.Status == (short)StatusBasic.Active)
                {
                    base_PromotionScheduleModel promotionScheduleModel = SelectedPromotion.PromotionScheduleModel;
                    if (promotionScheduleModel.ExpirationNoEndDate && promotionScheduleModel.EndDate.Value < DateTimeExt.Now)
                    {
                        Status = (short)StatusBasic.Deactive;
                        SavePromotion();
                    }
                }

                _status = SelectedPromotion.Status;
                OnPropertyChanged(() => Status);

                // Load PriceSchemaRange
                OnCheckPriceSchema();

                IsSearchMode = false;
            }
            else if (!IsSearchMode)
            {
                if (ShowNotification(null))
                    IsSearchMode = true;
            }
            else
                // Show detail form
                IsSearchMode = false;
        }

        #endregion

        #region LoadStepCommand

        /// <summary>
        /// Gets the LoadStepCommand command.
        /// </summary>
        public ICommand LoadStepCommand { get; private set; }

        /// <summary>
        /// Method to check whether the LoadStepCommand command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnLoadStepCommandCanExecute()
        {
            return true;
        }

        /// <summary>
        /// Method to invoke when the LoadStepCommand command is executed.
        /// </summary>
        private void OnLoadStepCommandExecute()
        {
            // Load data by predicate
            LoadDataByPredicate();
        }

        #endregion

        #region PopupCustomCommand

        /// <summary>
        /// Gets the PopupCustomCommand command.
        /// </summary>
        public ICommand PopupCustomCommand { get; private set; }

        /// <summary>
        /// Method to check whether the PopupCustomCommand command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnPopupCustomCommandCanExecute(object param)
        {
            return true;
        }

        /// <summary>
        /// Method to invoke when the PopupCustomCommand command is executed.
        /// </summary>
        private void OnPopupCustomCommandExecute(object param)
        {
            if (SelectedPromotion != null)
            {
                if (SelectedPromotion != null &&
                    (param == null && SelectedPromotion.PromotionAffectList.Count == 0) ||
                    (param != null && SelectedPromotion.AffectDiscount == 3))
                {
                    PromotionCustomViewModel viewModel = new PromotionCustomViewModel(CategoryCollection, SelectedPromotion.PromotionAffectList);
                    bool? result = _dialogService.ShowDialog<CPC.POS.View.CustomView>(_ownerViewModel, viewModel, "Select products apply to this promotion");
                    if (result.HasValue && result.Value)
                    {
                        // Update promotion affect list
                        SelectedPromotion.PromotionAffectList = viewModel.PromotionAffectList;
                    }

                    // Raise total selected products
                    SelectedPromotion.RaiseTotalSelectedProducts();
                }
            }
        }

        #endregion

        #region PopupAdvanceSearchCommand

        /// <summary>
        /// Gets the PopupAdvanceSearchCommand command.
        /// </summary>
        public ICommand PopupAdvanceSearchCommand { get; private set; }

        /// <summary>
        /// Method to check whether the PopupAdvanceSearchCommand command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnPopupAdvanceSearchCommandCanExecute(object param)
        {
            return true;
        }

        /// <summary>
        /// Method to invoke when the PopupAdvanceSearchCommand command is executed.
        /// </summary>
        private void OnPopupAdvanceSearchCommandExecute(object param)
        {
            PromotionAdvanceSearchViewModel viewModel = new PromotionAdvanceSearchViewModel(PromotionTypes);
            bool? msgResult = _dialogService.ShowDialog<CPC.POS.View.PromotionAdvanceSearchView>(_ownerViewModel, viewModel, "Advance Search");
            if (msgResult.HasValue)
            {
                if (msgResult.Value)
                {
                    if (param != null)
                        Keyword = param.ToString();

                    // Create basic predicate combine with advance predicate
                    Expression<Func<base_Promotion, bool>> predicate = CreateSearchPredicate(Keyword);
                    predicate = predicate.And(viewModel.AdvanceSearchPredicate);

                    // Load data by search predicate
                    LoadDataByPredicate(predicate, false, 0);
                }
            }
        }

        #endregion

        #region NewNoteCommand

        /// <summary>
        /// Gets the NewNoteCommand command.
        /// </summary>
        public ICommand NewNoteCommand { get; private set; }

        /// <summary>
        /// Method to check whether the NewNoteCommand command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnNewNoteCommandCanExecute()
        {
            return true;
        }

        /// <summary>
        /// Method to invoke when the NewNoteCommand command is executed.
        /// </summary>
        private void OnNewNoteCommandExecute()
        {
            // TODO: Handle command logic here
        }

        #endregion

        #region ShowOrHiddenNoteCommand

        /// <summary>
        /// Gets the ShowOrHiddenNoteCommand command.
        /// </summary>
        public ICommand ShowOrHiddenNoteCommand { get; private set; }

        /// <summary>
        /// Method to check whether the ShowOrHiddenNoteCommand command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnShowOrHiddenNoteCommandCanExecute()
        {
            return true;
        }

        /// <summary>
        /// Method to invoke when the ShowOrHiddenNoteCommand command is executed.
        /// </summary>
        private void OnShowOrHiddenNoteCommandExecute()
        {
            // TODO: Handle command logic here
        }

        #endregion

        #region ChangeStatusCommand

        /// <summary>
        /// Gets the ChangeStatusCommand command.
        /// </summary>
        public ICommand ChangeStatusCommand { get; private set; }

        /// <summary>
        /// Method to check whether the ChangeStatusCommand command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnChangeStatusCommandCanExecute(object param)
        {
            // Convert param to DataGridControl
            DataGridControl dataGridControl = param as DataGridControl;

            if (dataGridControl == null)
                return false;

            return dataGridControl.SelectedItems.Count == 1;
        }

        /// <summary>
        /// Method to invoke when the ChangeStatusCommand command is executed.
        /// </summary>
        private void OnChangeStatusCommandExecute(object param)
        {
            // Convert param to DataGridControl
            DataGridControl dataGridControl = param as DataGridControl;

            // Get promotion model
            base_PromotionModel promotionModel = dataGridControl.SelectedItem as base_PromotionModel;

            // Update status value in ViewModel
            if (promotionModel.Status.Equals((short)StatusBasic.Active))
                _status = (short)StatusBasic.Deactive;
            else
                _status = (short)StatusBasic.Active;
            OnPropertyChanged(() => Status);

            // Process when status changed
            if (OnStatusChanged(promotionModel))
            {
                promotionModel.DateUpdated = DateTimeExt.Now;
                if (Define.USER != null)
                    promotionModel.UserUpdated = Define.USER.LoginName;

                // Update status promotion
                promotionModel.Status = Status;

                // Check conflick promotion
                CheckConflictPromotion(promotionModel);

                // Map data from model to entity
                promotionModel.ToEntity();

                // Accept changes
                _promotionRepository.Commit();

                // Turn off IsDirty & IsNew
                promotionModel.EndUpdate();

                // Show warning conflict
                ShowWarningConflict(promotionModel);
                OnPropertyChanged(() => HiddenWarningConflict);
            }
        }

        #endregion

        #endregion

        #region Private Methods

        /// <summary>
        /// Load static data
        /// </summary>
        private void LoadStaticData()
        {
            // Load category collection
            if (CategoryCollection == null)
            {
                // Get all department that status is actived
                IQueryable<int> departmentIDList = _departmentRepository.
                    GetIQueryable(x => x.LevelId == 0 && x.IsActived == true).Select(x => x.Id);

                // Get all category that contain in department list
                CategoryCollection = new ObservableCollection<ComboItem>(_departmentRepository.
                    GetIQueryable(x => x.IsActived == true && x.LevelId == 1 && departmentIDList.Contains(x.ParentId.Value)).
                    OrderBy(x => x.Name).
                    Select(x => new ComboItem { IntValue = x.Id, Text = x.Name }));
            }

            // Load vendor collection
            if (VendorCollection == null)
            {
                string markType = MarkType.Vendor.ToDescription();
                VendorCollection = new ObservableCollection<ComboItem>(_guestRepository.
                    GetAll(x => x.Mark.Equals(markType) && !x.IsPurged && x.IsActived).
                    OrderBy(x => x.Company).
                    Select(x => new ComboItem { LongValue = x.Id, Text = x.Company }));
            }

            Common.Refresh();

            // Load PriceSchemaCollection
            PriceSchemaCollection = new ObservableCollection<CheckBoxItemModel>();
            foreach (ComboItem comboItem in Common.PriceSchemas)
            {
                CheckBoxItemModel checkBoxItemModel = new CheckBoxItemModel(comboItem);
                checkBoxItemModel.PropertyChanged += new PropertyChangedEventHandler(checkBoxItemModel_PropertyChanged);
                PriceSchemaCollection.Add(checkBoxItemModel);
            }

            // Load PromotionTypes
            PromotionTypes.Clear();
            foreach (ComboItem comboItem in Common.PromotionTypes)
            {
                if (comboItem.Text.Contains("$"))
                    comboItem.Text = comboItem.Text.Replace("$", Define.CONFIGURATION.CurrencySymbol);
                PromotionTypes.Add(comboItem);
            }

            // Load TakeOffOptions
            TakeOffOptions.Clear();
            foreach (ComboItem comboItem in Common.TakeOffOptions)
            {
                if (comboItem.Text.Contains("$"))
                    comboItem.Text = comboItem.Text.Replace("$", Define.CONFIGURATION.CurrencySymbol);
                TakeOffOptions.Add(comboItem);
            }
        }

        /// <summary>
        /// Initial commands for binding on form
        /// </summary>
        private void InitialCommand()
        {
            SearchCommand = new RelayCommand<object>(OnSearchCommandExecute, OnSearchCommandCanExecute);
            NewCommand = new RelayCommand(OnNewCommandExecute, OnNewCommandCanExecute);
            EditCommand = new RelayCommand<object>(OnEditCommandExecute, OnEditCommandCanExecute);
            SaveCommand = new RelayCommand(OnSaveCommandExecute, OnSaveCommandCanExecute);
            DeleteCommand = new RelayCommand(OnDeleteCommandExecute, OnDeleteCommandCanExecute);
            DoubleClickViewCommand = new RelayCommand<object>(OnDoubleClickViewCommandExecute, OnDoubleClickViewCommandCanExecute);
            NewNoteCommand = new RelayCommand(OnNewNoteCommandExecute, OnNewNoteCommandCanExecute);
            ShowOrHiddenNoteCommand = new RelayCommand(OnShowOrHiddenNoteCommandExecute, OnShowOrHiddenNoteCommandCanExecute);
            LoadStepCommand = new RelayCommand(OnLoadStepCommandExecute, OnLoadStepCommandCanExecute);
            PopupCustomCommand = new RelayCommand<object>(OnPopupCustomCommandExecute, OnPopupCustomCommandCanExecute);
            PopupAdvanceSearchCommand = new RelayCommand<object>(OnPopupAdvanceSearchCommandExecute, OnPopupAdvanceSearchCommandCanExecute);
            DeletesCommand = new RelayCommand<object>(OnDeletesCommandExecute, OnDeletesCommandCanExecute);
            ChangeStatusCommand = new RelayCommand<object>(OnChangeStatusCommandExecute, OnChangeStatusCommandCanExecute);
        }

        /// <summary>
        /// Gets a value that indicates whether the data is edit.
        /// </summary>
        /// <returns>true if the data is edit; otherwise, false.</returns>
        private bool IsEdit()
        {
            if (SelectedPromotion == null)
                return false;

            return SelectedPromotion.IsDirty ||
                SelectedPromotion.PromotionScheduleModel.IsDirty ||
                SelectedPromotion.PromotionAffectList.IsDirty ||
                IsDirtyPriceSchemaCollection;
        }

        /// <summary>
        /// Gets a value that indicates whether the data is edit.
        /// </summary>
        /// <param name="isClosing">
        /// true if form is closing;
        /// false if form is changing;
        /// null if switch change search mode
        /// </param>
        /// <returns>true if continue action; otherwise, false.</returns>
        private bool ShowNotification(bool? isClosing)
        {
            bool result = true;

            // Check data is edited
            if (IsEdit())
            {
                // Show notification when data has changed
                MessageBoxResultCustom msgResult = MsgControl.ShowWarning("Dữ liệu đã bị thay đổi.Bạn có muốn lu7 chúng lại không?", "Thông báo", MessageBoxButtonCustom.YesNoCancel);

                if (msgResult.Is(MessageBoxResultCustom.Cancel))
                {
                    return false;
                }
                else if (msgResult.Is(MessageBoxResultCustom.Yes))
                {
                    if (OnSaveCommandCanExecute())
                    {
                        // Call Save function
                        result = SavePromotion();
                    }
                    else
                    {
                        result = false;
                    }
                }
                else
                {
                    if (SelectedPromotion.IsNew)
                    {
                        //DeleteNote();

                        SelectedPromotion = null;
                        if (isClosing.HasValue && !isClosing.Value)
                            IsSearchMode = true;
                    }
                    else
                    {
                        // Refresh promotion datas
                        SelectedPromotion.PromotionScheduleModel = null;
                        SelectedPromotion.ToModelAndRaise();
                        SelectedPromotion.EndUpdate();
                        LoadRelationData(SelectedPromotion);

                        // Load PriceSchemaRange
                        OnCheckPriceSchema();
                    }
                }
            }

            if (result && isClosing == null && SelectedPromotion != null && !SelectedPromotion.IsNew)
            {
                // Refresh promotion datas
                SelectedPromotion.PromotionScheduleModel = null;
                SelectedPromotion.ToModelAndRaise();
                SelectedPromotion.EndUpdate();
                LoadRelationData(SelectedPromotion);

                // Load PriceSchemaRange
                OnCheckPriceSchema();

                // Clear selected item
                SelectedPromotion = null;
            }

            return result;
        }

        /// <summary>
        /// Create predicate with condition for search
        /// </summary>
        /// <param name="keyword">Keyword</param>
        /// <returns>Expression</returns>
        private Expression<Func<base_Promotion, bool>> CreateSearchPredicate(string keyword)
        {
            // Initial predicate
            Expression<Func<base_Promotion, bool>> predicate = PredicateBuilder.True<base_Promotion>();

            // Set conditions for predicate
            if (!string.IsNullOrWhiteSpace(keyword) && SearchOption > 0)
            {
                if (SearchOption.Has(SearchOptions.ItemName))
                {
                    predicate = predicate.And(x => x.Name.ToLower().Contains(keyword.ToLower()));
                }
                if (SearchOption.Has(SearchOptions.Type))
                {
                    // Get all promotion types that contain keyword
                    IEnumerable<ComboItem> promotionTypes = PromotionTypes.Where(x => x.Text.ToLower().Contains(keyword.ToLower()));
                    IEnumerable<short> promotionTypeIDList = promotionTypes.Select(x => x.Value);

                    // Get all promotion that contain in promotion type list
                    if (promotionTypeIDList.Count() > 0)
                        predicate = predicate.And(x => promotionTypeIDList.Contains(x.PromotionTypeId));
                    else
                        // If condition in predicate is false, GetRange function can not get data from database.
                        // Solution for this problem is create fake condition
                        predicate = predicate.And(x => x.Id < 0);
                }
                if (SearchOption.Has(SearchOptions.Code))
                {
                    predicate = predicate.And(x => x.CouponBarCode.Contains(keyword.ToLower()));
                }
            }
            return predicate;
        }

        /// <summary>
        /// Method get Data from database
        /// <para>Using load on the first time</para>
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="refreshData"></param>
        /// <param name="currentIndex">index to load if index = 0 , clear collection</param>
        private void LoadDataByPredicate(bool refreshData = false, int currentIndex = 0)
        {
            // Create predicate
            Expression<Func<base_Promotion, bool>> predicate = CreateSearchPredicate(Keyword);

            // Load data by predicate
            LoadDataByPredicate(predicate, refreshData, currentIndex);
        }

        /// <summary>
        /// Method get Data from database
        /// <para>Using load on the first time</para>
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="refreshData"></param>
        /// <param name="currentIndex">index to load if index = 0 , clear collection</param>
        private void LoadDataByPredicate(Expression<Func<base_Promotion, bool>> predicate, bool refreshData = false, int currentIndex = 0)
        {
            // Create background worker
            BackgroundWorker bgWorker = new BackgroundWorker { WorkerReportsProgress = true };

            if (currentIndex == 0)
                PromotionCollection.Clear();

            bgWorker.DoWork += (sender, e) =>
            {
                // Turn on BusyIndicator
                if (Define.DisplayLoading)
                    IsBusy = true;

                if (refreshData)
                {
                    _promotionRepository.Refresh();
                    _promotionScheduleRepository.Refresh();
                    _promotionAffectRepository.Refresh();
                    _departmentRepository.Refresh();
                    _guestRepository.Refresh();
                }

                // Get all promotions
                IList<base_Promotion> promotions = _promotionRepository.GetRangeDescending(currentIndex, NumberOfDisplayItems, x => x.DateCreated, predicate);
                foreach (base_Promotion promotion in promotions)
                {
                    bgWorker.ReportProgress(0, promotion);
                }
            };

            bgWorker.ProgressChanged += (sender, e) =>
            {
                // Create promotion model
                base_PromotionModel promotionModel = new base_PromotionModel((base_Promotion)e.UserState);

                // Load relation data
                LoadRelationData(promotionModel);

                // Add to collection
                PromotionCollection.Add(promotionModel);
            };

            bgWorker.RunWorkerCompleted += (sender, e) =>
            {
                OnPropertyChanged(() => HiddenWarningConflict);

                // Turn off BusyIndicator
                IsBusy = false;
            };

            // Run async background worker
            bgWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Load relation data for promotion
        /// </summary>
        /// <param name="promotionModel"></param>
        private void LoadRelationData(base_PromotionModel promotionModel)
        {
            // Get promotion type name
            if (string.IsNullOrWhiteSpace(promotionModel.PromotionTypeName))
            {
                ComboItem promotionType = PromotionTypes.FirstOrDefault(x => x.Value.Equals(promotionModel.PromotionTypeId));
                if (promotionType != null)
                    promotionModel.PromotionTypeName = promotionType.Text;
            }

            // Load  promotion schedule
            if (promotionModel.PromotionScheduleModel == null)
            {
                // Get promotion schedule
                base_PromotionSchedule promotionSchedule = promotionModel.base_Promotion.base_PromotionSchedule.SingleOrDefault();
                promotionModel.PromotionScheduleModel = new base_PromotionScheduleModel(promotionSchedule);

                // Check ExpirationNoEndDate
                promotionModel.PromotionScheduleModel.SetExpirationNoEndDate(promotionModel.PromotionScheduleModel.EndDate != null);
            }

            // Load promotion affect
            OnLoadPromotionAffect(promotionModel);

            // Update vendor id if vendor is deactived
            if (promotionModel.AffectDiscount == 2 && promotionModel.VendorId.HasValue)
            {
                if (!VendorCollection.Select(x => x.LongValue).Contains(promotionModel.VendorId.Value))
                    promotionModel.VendorId = 0;
            }
        }

        /// <summary>
        /// Load promotion affect by AffectDiscount
        /// </summary>
        /// <param name="promotionModel"></param>
        private void OnLoadPromotionAffect(base_PromotionModel promotionModel)
        {
            if (promotionModel.PromotionAffectList == null)
                promotionModel.PromotionAffectList = new CollectionBase<base_PromotionAffectModel>();

            if (promotionModel.AffectDiscount == 1 && promotionModel.CategoryId.HasValue)
            {
                if (!CategoryCollection.Select(x => x.IntValue).Contains(promotionModel.CategoryId.Value))
                    promotionModel.CategoryId = 0;
            }

            // Promotion Affect is Custom
            if (promotionModel.AffectDiscount == 3)
            {
                promotionModel.PromotionAffectList = new CollectionBase<base_PromotionAffectModel>(
                        promotionModel.base_Promotion.base_PromotionAffect.Select(x => new base_PromotionAffectModel(x)));
                promotionModel.RaiseTotalSelectedProducts();
            }
        }

        /// <summary>
        /// Create a new promotion
        /// </summary>
        private void NewPromotion()
        {
            // Create a new promotion with default values
            SelectedPromotion = new base_PromotionModel();
            _status = Define.CONFIGURATION.DefaultDiscountStatus;
            OnPropertyChanged(() => Status);
            SelectedPromotion.PromotionTypeId = Define.CONFIGURATION.DefaultDiscountType;
            SelectedPromotion.PriceSchemaRange = 0;
            SelectedPromotion.Description = string.Empty;
            SelectedPromotion.AffectDiscount = 0;
            SelectedPromotion.DateCreated = DateTimeExt.Now;
            SelectedPromotion.UserCreated = Define.USER.LoginName;
            SelectedPromotion.Resource = Guid.NewGuid();

            // Check PriceSchemaRange
            OnCheckPriceSchema();

            SelectedPromotion.PromotionScheduleModel = new base_PromotionScheduleModel();
            SelectedPromotion.PromotionAffectList = new CollectionBase<base_PromotionAffectModel>();

            // Turn off IsDirty
            SelectedPromotion.IsDirty = false;
            SelectedPromotion.PromotionScheduleModel.IsDirty = false;
        }

        /// <summary>
        /// Process save promotion function
        /// </summary>
        /// <returns></returns>
        private bool SavePromotion()
        {
            try
            {
                // Update Status
                //SelectedPromotion.Status = Status;

                // Update PriceSchemaRange value
                OnSavePriceSchema();

                // Check conflict promotion
                CheckConflictPromotion(SelectedPromotion);

                // Create new promotion
                if (SelectedPromotion.IsNew)
                {
                    // Call function create new promotion
                    SaveNew();
                }
                else
                {
                    // Vendor is edited
                    // Call function update promotion
                    SaveUpdate();
                }

                // Get promotion type name
                ComboItem promotionType = PromotionTypes.FirstOrDefault(x => x.Value.Equals(SelectedPromotion.PromotionTypeId));
                if (promotionType != null)
                    SelectedPromotion.PromotionTypeName = promotionType.Text;

                // Show warning conflict
                ShowWarningConflict(SelectedPromotion);
                OnPropertyChanged(() => HiddenWarningConflict);

                // Turn off IsDirty & IsNew
                SelectedPromotion.EndUpdate();
                SelectedPromotion.PromotionScheduleModel.EndUpdate();
                IsDirtyPriceSchemaCollection = false;
            }
            catch (Exception ex)
            {
                MsgControl.ShowWarning(ex.ToString(), "Thông báo", MessageBoxButtonCustom.OK);
            }

            return true;
        }

        /// <summary>
        /// Save when create new promotion
        /// </summary>
        private void SaveNew()
        {
            // Set shift
            SelectedPromotion.Shift = Define.ShiftCode;

            // Map data from model to entity
            SelectedPromotion.ToEntity();
            SelectedPromotion.PromotionScheduleModel.ToEntity();

            // Add new promotion schedule to repository
            SelectedPromotion.base_Promotion.base_PromotionSchedule.Add(SelectedPromotion.PromotionScheduleModel.base_PromotionSchedule);

            // Add new promotion affect to repository
            if (SelectedPromotion.AffectDiscount == 3)
            {
                foreach (base_PromotionAffectModel promotionAffectModel in SelectedPromotion.PromotionAffectList)
                {
                    // Map data from model to entity
                    promotionAffectModel.ToEntity();

                    SelectedPromotion.base_Promotion.base_PromotionAffect.Add(promotionAffectModel.base_PromotionAffect);
                }
            }
            else
                // Clear promotion affect list
                SelectedPromotion.PromotionAffectList = new CollectionBase<base_PromotionAffectModel>();

            // Add new promotion to repository
            _promotionRepository.Add(SelectedPromotion.base_Promotion);

            // Accept changes
            _promotionRepository.Commit();

            // Update ID from entity to model
            SelectedPromotion.Id = SelectedPromotion.base_Promotion.Id;
            SelectedPromotion.PromotionScheduleModel.PromotionId = SelectedPromotion.Id;
            SelectedPromotion.PromotionScheduleModel.Id = SelectedPromotion.PromotionScheduleModel.base_PromotionSchedule.Id;

            if (SelectedPromotion.AffectDiscount == 3)
            {
                foreach (base_PromotionAffectModel promotionAffectModel in SelectedPromotion.PromotionAffectList)
                {
                    promotionAffectModel.Id = promotionAffectModel.base_PromotionAffect.Id;
                    promotionAffectModel.PromotionId = promotionAffectModel.base_PromotionAffect.PromotionId;
                    promotionAffectModel.EndUpdate();
                }
            }

            // Push new promotion to collection
            PromotionCollection.Insert(0, SelectedPromotion);
        }

        /// <summary>
        /// Save when edit or update promotion
        /// </summary>
        private void SaveUpdate()
        {
            SelectedPromotion.DateUpdated = DateTimeExt.Now;
            if (Define.USER != null)
                SelectedPromotion.UserUpdated = Define.USER.LoginName;

            // Map data from model to entity
            SelectedPromotion.ToEntity();
            SelectedPromotion.PromotionScheduleModel.ToEntity();

            // Save promotion affect by AffectDiscount
            OnSavePromotionAffect();

            // Raise total selected products
            SelectedPromotion.RaiseTotalSelectedProducts();

            // Accept changes
            _promotionRepository.Commit();

            if (SelectedPromotion.PromotionAffectList.Count(x => x.IsNew) > 0)
            {
                foreach (base_PromotionAffectModel promotionAffectModel in SelectedPromotion.PromotionAffectList.Where(x => x.IsNew))
                {
                    promotionAffectModel.Id = promotionAffectModel.base_PromotionAffect.Id;

                    // Turn off IsDirty & IsNew
                    promotionAffectModel.EndUpdate();
                }
            }
        }

        /// <summary>
        /// Process promotion affect when save data
        /// </summary>
        private void OnSavePromotionAffect()
        {
            switch (SelectedPromotion.AffectDiscount)
            {
                case 0: // All items
                    // Delete all promotion affect in database
                    foreach (base_PromotionAffect promotionAffect in SelectedPromotion.base_Promotion.base_PromotionAffect.ToList())
                        _promotionAffectRepository.Delete(promotionAffect);

                    // Clear promotion affect in entity
                    SelectedPromotion.base_Promotion.base_PromotionAffect.Clear();

                    // Clear promotion affect list
                    SelectedPromotion.PromotionAffectList = new CollectionBase<base_PromotionAffectModel>();
                    break;
                case 1: // All items in department
                case 2: // All items in vendor
                    break;
                case 3: // Custom
                    // Remove promotion affect were deleted
                    if (SelectedPromotion.PromotionAffectList.DeletedItems != null)
                    {
                        foreach (base_PromotionAffectModel promotionAffectModel in SelectedPromotion.PromotionAffectList.DeletedItems)
                        {
                            // Remove promotion affect in entity
                            SelectedPromotion.base_Promotion.base_PromotionAffect.Remove(promotionAffectModel.base_PromotionAffect);

                            // Delete promotion affect in database
                            _promotionAffectRepository.Delete(promotionAffectModel.base_PromotionAffect);
                        }
                        SelectedPromotion.PromotionAffectList.DeletedItems.Clear();
                    }

                    // Create new promotion affect
                    foreach (base_PromotionAffectModel promotionAffectModel in SelectedPromotion.PromotionAffectList)
                    {
                        if (promotionAffectModel.IsNew)
                            promotionAffectModel.PromotionId = SelectedPromotion.Id;

                        // Map data from model to entity
                        promotionAffectModel.ToEntity();

                        // Add new to repository
                        if (promotionAffectModel.IsNew)
                            SelectedPromotion.base_Promotion.base_PromotionAffect.Add(promotionAffectModel.base_PromotionAffect);
                    }
                    break;
            }
        }

        /// <summary>
        /// Process price schema value when check box is checked
        /// </summary>
        private void OnSavePriceSchema()
        {
            int priceSchemaRange = 0;
            foreach (CheckBoxItemModel checkBoxItemModel in PriceSchemaCollection.Where(x => x.IsChecked))
            {
                priceSchemaRange ^= checkBoxItemModel.Value;
            }
            SelectedPromotion.PriceSchemaRange = priceSchemaRange;
        }

        /// <summary>
        /// Process check box when load price schema
        /// </summary>
        private void OnCheckPriceSchema()
        {
            if (SelectedPromotion != null)
            {
                foreach (CheckBoxItemModel checkBoxItemModel in PriceSchemaCollection)
                {
                    if ((SelectedPromotion.PriceSchemaRange & checkBoxItemModel.Value) == checkBoxItemModel.Value)
                        checkBoxItemModel.IsChecked = true;
                    else
                        checkBoxItemModel.IsChecked = false;
                }
                IsDirtyPriceSchemaCollection = false;
            }
        }

        /// <summary>
        /// Show popup reason to reactive when change status
        /// </summary>
        private bool OnStatusChanged(base_PromotionModel promotionModel)
        {
            bool result = true;

            // Check required discount reason when reactive promotion
            if (Status == (short)StatusBasic.Active && !promotionModel.IsNew &&
                Define.CONFIGURATION.IsRequireDiscountReason == true)
            {
                PromotionReasonViewModel viewModel = new PromotionReasonViewModel(promotionModel.ReasonReActive);
                bool? msgResult = _dialogService.ShowDialog<CPC.POS.View.PromotionReasonView>(_ownerViewModel, viewModel, "Entry for reason");
                if (msgResult.HasValue)
                {
                    if (msgResult.Value)
                        promotionModel.ReasonReActive = viewModel.ReasonReactive;
                    else
                    {
                        result = false;
                        App.Current.MainWindow.Dispatcher.BeginInvoke((Action)delegate
                        {
                            Status = (short)StatusBasic.Deactive;
                        });
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Process promotion affect when save data
        /// </summary>
        private void OnSavePromotionAffect(base_PromotionModel model)
        {
            switch (model.AffectDiscount)
            {
                case 0: // All items
                    // Delete all promotion affect in database
                    foreach (base_PromotionAffect promotionAffect in model.base_Promotion.base_PromotionAffect.ToList())
                        _promotionAffectRepository.Delete(promotionAffect);

                    // Clear promotion affect in entity
                    model.base_Promotion.base_PromotionAffect.Clear();

                    // Clear promotion affect list
                    model.PromotionAffectList = new CollectionBase<base_PromotionAffectModel>();
                    break;
                case 1: // All items in department
                case 2: // All items in vendor
                    break;
                case 3: // Custom
                    // Remove promotion affect were deleted
                    if (model.PromotionAffectList.DeletedItems != null)
                    {
                        foreach (base_PromotionAffectModel promotionAffectModel in model.PromotionAffectList.DeletedItems)
                        {
                            // Remove promotion affect in entity
                            model.base_Promotion.base_PromotionAffect.Remove(promotionAffectModel.base_PromotionAffect);

                            // Delete promotion affect in database
                            _promotionAffectRepository.Delete(promotionAffectModel.base_PromotionAffect);
                        }
                        model.PromotionAffectList.DeletedItems.Clear();
                    }

                    // Create new promotion affect
                    foreach (base_PromotionAffectModel promotionAffectModel in model.PromotionAffectList)
                    {
                        if (promotionAffectModel.IsNew)
                            promotionAffectModel.PromotionId = model.Id;

                        // Map data from model to entity
                        promotionAffectModel.ToEntity();

                        // Add new to repository
                        if (promotionAffectModel.IsNew)
                            model.base_Promotion.base_PromotionAffect.Add(promotionAffectModel.base_PromotionAffect);
                    }
                    break;
            }
        }

        /// <summary>
        /// Check and remove conflict promotion
        /// </summary>
        private void CheckAndRemoveConflictPromotion(base_PromotionModel promotionModel)
        {
            if (promotionModel.IsConflict)
            {
                // Remove conflict for this promotion
                promotionModel.IsConflict = false;

                // Get all promotions is conflict
                IEnumerable<base_PromotionModel> promotionList = PromotionCollection.Where(x => x.IsConflict);
                if (PromotionCollection.Count(x => x.IsConflict) == 1)
                {
                    // Get relative conflict promotion
                    base_PromotionModel promotionItem = PromotionCollection.SingleOrDefault(x => x.IsConflict);

                    // Remove conflict for relative promotion
                    promotionItem.IsConflict = false;

                    // Map data from model to entity
                    promotionItem.ToEntity();

                    // Turn off IsDirty & IsNew
                    promotionItem.EndUpdate();
                }
            }
        }

        /// <summary>
        /// Check conflict promotion
        /// </summary>
        private void CheckConflictPromotion(base_PromotionModel promotionModel)
        {
            // Check and update conflict for promotion
            if (promotionModel.Status.Equals((short)StatusBasic.Active))
            {
                // Get number of active promotion
                int numberOfActivePromotion = PromotionCollection.Where(x => !x.Resource.Equals(promotionModel.Resource)).
                    Count(x => x.base_Promotion.Status.Equals((short)StatusBasic.Active));

                if (numberOfActivePromotion > 0)
                {
                    if (numberOfActivePromotion == 1)
                    {
                        // Get active promotion
                        base_PromotionModel promotionItem = PromotionCollection.Where(x => !x.Resource.Equals(promotionModel.Resource)).
                            SingleOrDefault(x => x.base_Promotion.Status.Equals((short)StatusBasic.Active));

                        // Update conflict value for relative promotion
                        promotionItem.IsConflict = true;

                        // Map data from model to entity
                        promotionItem.ToEntity();

                        // Turn off IsDirty & IsNew
                        promotionItem.EndUpdate();
                    }

                    // Update conflict value for this promotion
                    promotionModel.IsConflict = true;
                }
            }
            else
            {
                // Check and remove conflict promotion
                CheckAndRemoveConflictPromotion(promotionModel);
            }
        }

        /// <summary>
        /// Show warning conflict
        /// </summary>
        /// <param name="promotionModel"></param>
        private void ShowWarningConflict(base_PromotionModel promotionModel)
        {
            if (promotionModel.IsConflict)
            {
                // Show notification when data has changed
                MsgControl.ShowWarning("Một hoặc nhiều mục chiết khấu được niêm yết hoặc giảm hoạt động khác. Giảm giá được xác định có thể không được áp dụng.", "Thông báo", MessageBoxButtonCustom.OK);
            }
        }

        #endregion

        #region Override Methods

        /// <summary>
        /// Process load data
        /// </summary>
        public override void LoadData()
        {
            if (SelectedPromotion != null && !SelectedPromotion.IsNew)
            {
                lock (UnitOfWork.Locker)
                {
                    // Refresh static data
                    CategoryCollection = null;
                    VendorCollection = null;

                    // Load static data
                    LoadStaticData();
                }

                // Refresh promotion datas
                SelectedPromotion.PromotionScheduleModel = null;
                SelectedPromotion.PromotionTypeId = 0;
                SelectedPromotion.TakeOffOption = -1;
                SelectedPromotion.ToModelAndRaise();
                SelectedPromotion.EndUpdate();
                LoadRelationData(SelectedPromotion);

                // Load PriceSchemaRange
                OnCheckPriceSchema();

                _status = SelectedPromotion.Status;
                OnPropertyChanged(() => Status);
            }

            // Load data by predicate
            LoadDataByPredicate();
        }

        /// <summary>
        /// Process when change display view
        /// </summary>
        /// <param name="isList"></param>
        public override void ChangeSearchMode(bool isList, object param = null)
        {
            if (param == null)
            {
                if (ShowNotification(null))
                {
                    // When user clicked create new button
                    if (!isList)
                    {
                        // Create new promotion
                        NewPromotion();

                        // Display promotion detail
                        IsSearchMode = false;
                    }
                    else
                    {
                        // When user click view list button
                        // Display promotion list
                        IsSearchMode = true;
                    }
                }
            }
        }

        /// <summary>
        /// Process when changed view
        /// </summary>
        /// <param name="isClosing">Form is closing or changing</param>
        /// <returns></returns>
        protected override bool OnViewChangingCommandCanExecute(bool isClosing)
        {
            return ShowNotification(isClosing);
        }

        #endregion

        #region Event Methods

        /// <summary>
        /// Set IsDirty for PriceSchemaCollection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxItemModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("IsChecked"))
            {
                if (!IsDirtyPriceSchemaCollection)
                    IsDirtyPriceSchemaCollection = true;
                OnPropertyChanged(() => IsCheckedPriceSchemaCollection);
            }
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
                    case "IsCheckedPriceSchemaCollection":
                        if (!IsCheckedPriceSchemaCollection)
                            message = "PriceSchemaRange must be selected";
                        break;
                }

                if (!string.IsNullOrWhiteSpace(message))
                    return message;
                return null;
            }
        }

        #endregion

        #region Permission

        #region Properties

        private bool _allowAddPromotion = true;
        /// <summary>
        /// Gets or sets the AllowAddPromotion.
        /// </summary>
        public bool AllowAddPromotion
        {
            get { return _allowAddPromotion; }
            set
            {
                if (_allowAddPromotion != value)
                {
                    _allowAddPromotion = value;
                    OnPropertyChanged(() => AllowAddPromotion);
                }
            }
        }

        #endregion

        /// <summary>
        /// Get permissions
        /// </summary>
        public override void GetPermission()
        {
            if (!IsAdminPermission)
            {
                if (IsFullPermission)
                {
                    // Set default permission
                    AllowAddPromotion = IsMainStore;
                }
                else
                {
                    // Get all user rights
                    IEnumerable<string> userRightCodes = Define.USER_AUTHORIZATION.Select(x => x.Code);

                    // Get edit quantity permission
                    AllowAddPromotion = userRightCodes.Contains("IV100-03-01") && IsMainStore;
                }
            }
        }

        #endregion
    }
}