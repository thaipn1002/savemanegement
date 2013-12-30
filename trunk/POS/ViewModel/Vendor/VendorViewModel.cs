using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
using CPCToolkitExt.DataGridControl;
using CPCToolkitExtLibraries;
using MessageBoxControl;

namespace CPC.POS.ViewModel
{
    class VendorViewModel : ViewModelBase
    {
        #region Defines

        private base_GuestRepository _guestRepository = new base_GuestRepository();
        private base_GuestAddressRepository _addressRepository = new base_GuestAddressRepository();
        private base_ResourcePhotoRepository _photoRepository = new base_ResourcePhotoRepository();
        private base_GuestAdditionalRepository _additionalRepository = new base_GuestAdditionalRepository();
        private base_ResourceNoteRepository _noteRepository = new base_ResourceNoteRepository();
        private base_SaleTaxLocationRepository _saleTaxLocationRepository = new base_SaleTaxLocationRepository();
        private base_ProductRepository _productRepository = new base_ProductRepository();
        private base_DepartmentRepository _departmentRepository = new base_DepartmentRepository();
        private base_UOMRepository _uomRepository = new base_UOMRepository();
        private base_PurchaseOrderRepository _purchaseOrderRepository = new base_PurchaseOrderRepository();
        private base_VendorProductRepository _vendorProductRepository = new base_VendorProductRepository();
        private base_PromotionRepository _promotionRepository = new base_PromotionRepository();
        private base_GuestRewardRepository _guestRewardRepository = new base_GuestRewardRepository();
        private base_GuestGroupRepository _guestGroupRepository = new base_GuestGroupRepository();
        private base_CustomFieldRepository _customFieldRepository = new base_CustomFieldRepository();

        private string _vendorMark = MarkType.Vendor.ToDescription();

        public List<base_SaleTaxLocation> AllSaleTax { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public VendorViewModel()
            : base()
        {
            _ownerViewModel = App.Current.MainWindow.DataContext;

            StickyManagementViewModel = new PopupStickyViewModel();

            LoadStaticData();

            InitialCommand();
        }

        public VendorViewModel(bool isList, object param = null)
            : this()
        {
            ChangeSearchMode(isList, param);

            // Get permission
            GetPermission();
        }

        #endregion

        #region Properties

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

        private ObservableCollection<base_GuestModel> _vendorCollection = new ObservableCollection<base_GuestModel>();
        /// <summary>
        /// Gets or sets the VendorCollection.
        /// </summary>
        public ObservableCollection<base_GuestModel> VendorCollection
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

        private base_GuestModel _selectedVendor;
        /// <summary>
        /// Gets or sets the SelectedVendor.
        /// </summary>
        public base_GuestModel SelectedVendor
        {
            get { return _selectedVendor; }
            set
            {
                if (_selectedVendor != value)
                {
                    _selectedVendor = value;
                    OnPropertyChanged(() => SelectedVendor);
                }
            }
        }

        private int _totalVendors;
        /// <summary>
        /// Gets or sets the TotalVendors.
        /// </summary>
        public int TotalVendors
        {
            get { return _totalVendors; }
            set
            {
                if (_totalVendors != value)
                {
                    _totalVendors = value;
                    OnPropertyChanged(() => TotalVendors);
                }
            }
        }

        private base_GuestModel _selectedContact;
        /// <summary>
        /// Gets or sets the SelectedContact.
        /// </summary>
        public base_GuestModel SelectedContact
        {
            get { return _selectedContact; }
            set
            {
                if (_selectedContact != value)
                {
                    _selectedContact = value;
                    OnPropertyChanged(() => SelectedContact);
                }
            }
        }

        /// <summary>
        /// Gets the IsDirtyContactCollection
        /// </summary>
        public bool IsDirtyContactCollection { get; set; }

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
                    return "Show stickies";
                else if (NotePopupCollection.Count == SelectedVendor.ResourceNoteCollection.Count && NotePopupCollection.Any(x => x.IsVisible))
                    return "Hide stickies";
                else
                    return "Show stickies";
            }
        }

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

        #region SaleTaxCollection
        private ObservableCollection<base_SaleTaxLocationModel> _saleTaxCollection;
        /// <summary>
        /// Gets or sets the SaleTaxCollection.
        /// </summary>
        public ObservableCollection<base_SaleTaxLocationModel> SaleTaxCollection
        {
            get { return _saleTaxCollection; }
            set
            {
                if (_saleTaxCollection != value)
                {
                    _saleTaxCollection = value;
                    OnPropertyChanged(() => SaleTaxCollection);
                }
            }
        }
        #endregion

        private int _totalProducts;
        /// <summary>
        /// Gets or sets the TotalProducts.
        /// </summary>
        public int TotalProducts
        {
            get { return _totalProducts; }
            set
            {
                if (_totalProducts != value)
                {
                    _totalProducts = value;
                    OnPropertyChanged(() => TotalProducts);
                }
            }
        }

        /// <summary>
        /// Gets or sets the CategoryList
        /// </summary>
        public List<base_DepartmentModel> CategoryList { get; set; }

        /// <summary>
        /// Gets or sets the UOMList
        /// </summary>
        public ObservableCollection<CheckBoxItemModel> UOMList { get; set; }

        private base_PurchaseOrderModel _totalPurchaseOrder;
        /// <summary>
        /// Gets or sets the TotalPurchaseOrder.
        /// </summary>
        public base_PurchaseOrderModel TotalPurchaseOrder
        {
            get { return _totalPurchaseOrder; }
            set
            {
                if (_totalPurchaseOrder != value)
                {
                    _totalPurchaseOrder = value;
                    OnPropertyChanged(() => TotalPurchaseOrder);
                }
            }
        }

        /// <summary>
        /// Gets or sets the GuestGroupCollection.
        /// </summary>
        public ObservableCollection<base_GuestGroupModel> GuestGroupCollection { get; set; }

        private int _selectedTabIndex;
        /// <summary>
        /// Gets or sets the SelectedTabIndex.
        /// </summary>
        public int SelectedTabIndex
        {
            get { return _selectedTabIndex; }
            set
            {
                if (_selectedTabIndex != value)
                {
                    _selectedTabIndex = value;
                    OnPropertyChanged(() => SelectedTabIndex);
                    if (SelectedVendor != null)
                        OnSelectedTabIndexChanged();
                }
            }
        }

        private bool _focusDefault;
        /// <summary>
        /// Gets or sets the FocusDefault.
        /// </summary>
        public bool FocusDefault
        {
            get { return _focusDefault; }
            set
            {
                if (_focusDefault != value)
                {
                    _focusDefault = value;
                    OnPropertyChanged(() => FocusDefault);
                }
            }
        }

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

        private ObservableCollection<base_CustomFieldModel> _customFieldCollection;
        /// <summary>
        /// Gets or sets the CustomFieldCollection.
        /// </summary>
        public ObservableCollection<base_CustomFieldModel> CustomFieldCollection
        {
            get { return _customFieldCollection; }
            set
            {
                if (_customFieldCollection != value)
                {
                    _customFieldCollection = value;
                    OnPropertyChanged(() => CustomFieldCollection);
                }
            }
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

                // Search all
                if ((param == null || string.IsNullOrWhiteSpace(param.ToString())) && SearchOption == 0)
                {
                    // Load data by predicate
                    LoadVendorDataByPredicate(false);

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
                        LoadVendorDataByPredicate(false);
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
            return AllowAddVendor;
        }

        /// <summary>
        /// Method to invoke when the NewCommand command is executed.
        /// </summary>
        private void OnNewCommandExecute()
        {
            if (IsSearchMode)
            {
                IsSearchMode = false;
                NewVendor();
            }
            else if (ShowNotification(null))
                NewVendor();
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
            return IsValid && IsEdit();
        }

        /// <summary>
        /// Method to invoke when the SaveCommand command is executed.
        /// </summary>
        private void OnSaveCommandExecute()
        {
            SaveVendor(SelectedVendor);
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
            if (SelectedVendor == null)
                return false;
            return !IsEdit() && !SelectedVendor.IsNew;
        }

        /// <summary>
        /// Method to invoke when the DeleteCommand command is executed.
        /// </summary>
        private void OnDeleteCommandExecute()
        {
            MessageBoxResultCustom msgResult = MsgControl.ShowWarning("Bạn có muốn xóa nhà cung cấp này ?", "Cảnh báo", MessageBoxButtonCustom.YesNo);
            if (msgResult.Is(MessageBoxResultCustom.Yes))
            {

                if (SelectedVendor.IsNew)
                {
                    // Remove all popup sticky
                    StickyManagementViewModel.DeleteAllResourceNote();

                    SelectedVendor = null;
                    IsSearchMode = true;
                }
                else if (IsValid)
                {
                    List<ItemModel> ItemModel = new List<ItemModel>();
                    string resource = SelectedVendor.Resource.Value.ToString();
                    if (!_purchaseOrderRepository.GetAll().Select(x => x.VendorResource).Contains(resource))
                    {
                        // Remove all popup sticky
                        StickyManagementViewModel.DeleteAllResourceNote();

                        SelectedVendor.IsPurged = true;
                        SelectedVendor.ToEntity();
                        foreach (base_GuestModel contactModel in SelectedVendor.ContactCollection.Where(x => !x.IsAcceptedRow))
                            contactModel.base_Guest.IsPurged = true;
                        _guestRepository.Commit();
                        SelectedVendor.EndUpdate();
                        VendorCollection.Remove(SelectedVendor);
                        IsSearchMode = true;
                    }
                    else
                    {
                        ItemModel.Add(new ItemModel { Id = SelectedVendor.Id, Text = SelectedVendor.GuestNo, Resource = SelectedVendor.Resource.ToString() });
                        _dialogService.ShowDialog<ProblemDetectionView>(_ownerViewModel, new ProblemDetectionViewModel(ItemModel, "PurchaseOrder"), "Problem Detection");
                    }
                }
                else
                    return;


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

            MessageBoxResultCustom msgResult = MsgControl.ShowWarning("Bạn có muốn xóa nhà cung cấp này ?", "Cảnh báo", MessageBoxButtonCustom.YesNo);
            if (msgResult.Is(MessageBoxResultCustom.Yes))
            {
                bool flag = false;
                List<ItemModel> ItemModel = new List<ItemModel>();
                for (int i = 0; i < (dataGridControl.SelectedItems as ObservableCollection<object>).Count; i++)
                {
                    base_GuestModel model = (dataGridControl.SelectedItems as ObservableCollection<object>)[i] as base_GuestModel;
                    string resource = model.Resource.Value.ToString();
                    if (!_purchaseOrderRepository.GetAll().Select(x => x.VendorResource).Contains(resource))
                    {
                        model.IsPurged = true;
                        model.ToEntity();
                        foreach (base_GuestModel contactModel in model.ContactCollection.Where(x => !x.IsAcceptedRow))
                            contactModel.base_Guest.IsPurged = true;
                        _guestRepository.Commit();
                        model.EndUpdate();
                        VendorCollection.Remove(model);

                        // Remove all popup sticky
                        StickyManagementViewModel.DeleteAllResourceNote(model.ResourceNoteCollection);

                        i--;
                    }
                    else
                    {
                        ItemModel.Add(new ItemModel { Id = model.Id, Text = model.GuestNo, Resource = resource });
                        flag = true;
                    }
                }
                if (flag)
                    _dialogService.ShowDialog<ProblemDetectionView>(_ownerViewModel, new ProblemDetectionViewModel(ItemModel, "PurchaseOrder"), "Problem Detection");
            }
        }

        #endregion

        #region DuplicateCommand

        /// <summary>
        /// Gets the DuplicateCommand command.
        /// </summary>
        public ICommand DuplicateCommand { get; private set; }

        /// <summary>
        /// Method to check whether the DuplicateCommand command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnDuplicateCommandCanExecute(object param)
        {
            // Convert param to DataGridControl
            DataGridControl dataGridControl = param as DataGridControl;

            if (dataGridControl == null)
                return false;

            return dataGridControl.SelectedItems.Count == 1 && AllowAddVendor;
        }

        /// <summary>
        /// Method to invoke when the DuplicateCommand command is executed.
        /// </summary>
        private void OnDuplicateCommandExecute(object param)
        {
            // Convert param to DataGridControl
            DataGridControl dataGridControl = param as DataGridControl;
            base_GuestModel selectedItem = dataGridControl.SelectedItem as base_GuestModel;
            IsSearchMode = false;

            // Create new vendor model
            SelectedVendor = new base_GuestModel { Mark = selectedItem.Mark };

            // Duplicate vendor
            SelectedVendor.IsActived = true;
            SelectedVendor.FirstName = selectedItem.FirstName;
            SelectedVendor.MiddleName = selectedItem.MiddleName;
            SelectedVendor.LastName = selectedItem.LastName;
            SelectedVendor.Company = string.Format("{0} (Copy)", selectedItem.Company);
            SelectedVendor.GroupResource = selectedItem.GroupResource;
            SelectedVendor.GroupName = selectedItem.GroupName;
            SelectedVendor.DateCreated = DateTimeExt.Now;
            SelectedVendor.GuestNo = DateTimeExt.Now.ToString(Define.GuestNoFormat);
            SelectedVendor.Resource = Guid.NewGuid();
            SelectedVendor.UserCreated = Define.USER.LoginName;
            SelectedVendor.Shift = Define.ShiftCode;

            // Initial guest address collection
            SelectedVendor.AddressCollection = new ObservableCollection<base_GuestAddressModel>();

            // Initial address control collection
            SelectedVendor.AddressControlCollection = new AddressControlCollection();

            foreach (AddressControlModel addressControlItem in selectedItem.AddressControlCollection)
            {
                // Create new guest address model
                base_GuestAddressModel guestAddressModel = new base_GuestAddressModel();

                // Duplicate guest address model
                guestAddressModel.ToModel(addressControlItem);

                // Get default guest address model
                if (guestAddressModel.IsDefault)
                    SelectedVendor.AddressModel = guestAddressModel;

                // Add new guest address model to collection
                SelectedVendor.AddressCollection.Add(guestAddressModel);

                // Create new address control model
                AddressControlModel addressControlModel = guestAddressModel.ToAddressControlModel();

                // Add new address control model to collection
                SelectedVendor.AddressControlCollection.Add(addressControlModel);

                // Turn off IsDirty & IsNew
                guestAddressModel.EndUpdate();
                addressControlModel.IsDirty = false;
            }

            // Duplicate additional model
            SelectedVendor.AdditionalModel = new base_GuestAdditionalModel();
            SelectedVendor.PaymentTermDescription = selectedItem.PaymentTermDescription;
            SelectedVendor.TermDiscount = selectedItem.TermDiscount;
            SelectedVendor.TermNetDue = selectedItem.TermNetDue;
            SelectedVendor.TermPaidWithinDay = selectedItem.TermPaidWithinDay;
            SelectedVendor.AdditionalModel.FedTaxId = selectedItem.AdditionalModel.FedTaxId;
 
            SelectedVendor.PhotoCollection = new CollectionBase<base_ResourcePhotoModel>();
            SelectedVendor.ContactCollection = new CollectionBase<base_GuestModel>();
            SelectedVendor.ResourceNoteCollection = new CollectionBase<base_ResourceNoteModel>();
            StickyManagementViewModel.SetParentResource(SelectedVendor.Resource.ToString(), SelectedVendor.ResourceNoteCollection);

            // Turn off IsDirty
            SelectedVendor.IsDirty = false;
            SelectedVendor.AdditionalModel.IsDirty = false;
        }

        #endregion

        #region MergeItemCommand

        /// <summary>
        /// Gets the MergeItemCommand command.
        /// </summary>
        public ICommand MergeItemCommand { get; private set; }

        /// <summary>
        /// Method to check whether the MergeItemCommand command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnMergeItemCommandCanExecute(object param)
        {
            // Convert param to DataGridControl
            DataGridControl dataGridControl = param as DataGridControl;

            if (dataGridControl == null)
                return false;

            return dataGridControl.SelectedItems.Count == 1 && VendorCollection.Count > 1;
        }

        /// <summary>
        /// Method to invoke when the MergeItemCommand command is executed.
        /// </summary>
        private void OnMergeItemCommandExecute(object param)
        {
            // Convert param to DataGridControl
            DataGridControl dataGridControl = param as DataGridControl;

            // Get vendor model
            base_GuestModel vendorModel = dataGridControl.SelectedItem as base_GuestModel;

            PopupMergeVendorViewModel viewModel = new PopupMergeVendorViewModel(vendorModel, VendorCollection);
            bool? result = _dialogService.ShowDialog<PopupMergeVendorView>(_ownerViewModel, viewModel, "Merge Vendor");
            if (result.HasValue && result.Value)
            {
                // Get source vendor model
                base_GuestModel sourceVendorModel = VendorCollection.SingleOrDefault(x => x.Id.Equals(viewModel.SourceVendor.Id));

                // Get target vendor model
                base_GuestModel targetVendorModel = VendorCollection.SingleOrDefault(x => x.Id.Equals(viewModel.TargetVendor.Id));

                string sourceVendorResource = sourceVendorModel.Resource.ToString();
                string targetVendorResource = targetVendorModel.Resource.ToString();

                // Remove all popup sticky
                StickyManagementViewModel.DeleteAllResourceNote(sourceVendorModel.ResourceNoteCollection);

                // Get all guest reward that contain vendor id
                IList<base_GuestReward> guestRewards = _guestRewardRepository.GetAll(x => x.GuestId.Equals(sourceVendorModel.Id));
                foreach (base_GuestReward guestReward in guestRewards)
                {
                    // Update vendor id in GuestReward
                    guestReward.GuestId = targetVendorModel.Id;
                }

                // Get all product that contain vendor id
                IList<base_Product> products = _productRepository.GetAll(x => x.VendorId.Equals(sourceVendorModel.Id));
                foreach (base_Product product in products)
                {
                    // Update vendor id in Product
                    product.VendorId = targetVendorModel.Id;

                    // Get vendor product
                    base_VendorProduct vendorProduct = product.base_VendorProduct.SingleOrDefault(x => x.VendorId.Equals(targetVendorModel.Id));

                    // Delete vendor product from database
                    _vendorProductRepository.Delete(vendorProduct);
                }

                // Get all promotion that contain vendor id
                IList<base_Promotion> promotions = _promotionRepository.
                    GetAll(x => x.VendorId.HasValue && x.VendorId.Value.Equals(sourceVendorModel.Id));
                foreach (base_Promotion promotion in promotions)
                {
                    // Update vendor id in Promotion
                    promotion.VendorId = targetVendorModel.Id;
                }

                // Get all purchase order that contain vendor id
                IList<base_PurchaseOrder> purchaseOrders = _purchaseOrderRepository.GetAll(x => x.VendorResource.Equals(sourceVendorResource));
                foreach (base_PurchaseOrder purchaseOrder in purchaseOrders)
                {
                    // Update vendor code and vendor resource in PurchaseOrder
                    purchaseOrder.VendorCode = targetVendorModel.GuestNo;
                    purchaseOrder.VendorResource = targetVendorResource;
                }

                // Get all vendor product that contain vendor id
                //IList<base_VendorProduct> vendorProducts = _vendorProductRepository.GetAll(x => x.VendorId.Equals(sourceVendorModel.Id));
                if (targetVendorModel.base_Guest.base_VendorProduct.Count == 0)
                {
                    foreach (base_VendorProduct vendorProduct in sourceVendorModel.base_Guest.base_VendorProduct.ToList())
                    {
                        if (vendorProduct.base_Product.VendorId.Equals(targetVendorModel.Id))
                        {
                            // Delete vendor product from database
                            _vendorProductRepository.Delete(vendorProduct);
                        }
                        else
                        {
                            // Update vendor id in VendorProduct
                            vendorProduct.VendorId = targetVendorModel.Id;
                        }
                    }
                }

                // Remove source vendor from database
                _guestRepository.Delete(sourceVendorModel.base_Guest);

                // Remove source vendor from collection
                if (sourceVendorModel != null)
                    VendorCollection.Remove(sourceVendorModel);

                // Accept changes
                _guestRepository.Commit();
            }
        }

        #endregion

        #region PurchaseOrderCommand

        /// <summary>
        /// Gets the PurchaseOrderCommand command.
        /// </summary>
        public ICommand PurchaseOrderCommand { get; private set; }

        /// <summary>
        /// Method to check whether the PurchaseOrderCommand command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnPurchaseOrderCommandCanExecute(object param)
        {
            return param != null;
        }

        /// <summary>
        /// Method to invoke when the PurchaseOrderCommand command is executed.
        /// </summary>
        private void OnPurchaseOrderCommandExecute(object param)
        {
            // Convert param to vendor model
            base_GuestModel vendorModel = param as base_GuestModel;

            // Open purchase order detail
            (_ownerViewModel as MainViewModel).OpenViewExecute("PurchaseOrder", vendorModel.Resource.Value);
        }

        #endregion

        #region DoubleClickViewCommand

        /// <summary>
        /// Gets the DoubleClickViewCommand command.
        /// </summary>
        public ICommand DoubleClickViewCommand { get; private set; }

        /// <summary>
        /// Method to check whether the DoubleClick command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnDoubleClickViewCommandCanExecute(object param)
        {
            return true;
        }

        /// <summary>
        /// Method to invoke when the DoubleClick command is executed.
        /// </summary>
        private void OnDoubleClickViewCommandExecute(object param)
        {
            if (param != null && IsSearchMode)
            {
                // Update selected vendor
                SelectedVendor = param as base_GuestModel;

                //if (SelectedVendor.AdditionalModel != null)
                //{
                //    SelectedVendor.AdditionalModel.PropertyChanged -= new PropertyChangedEventHandler(AdditionalModel_PropertyChanged);
                //    SelectedVendor.AdditionalModel.PropertyChanged += new PropertyChangedEventHandler(AdditionalModel_PropertyChanged);
                //}

                // Load Additional
                LoadAdditionalModel(SelectedVendor);

                // Set parent resource
                StickyManagementViewModel.SetParentResource(SelectedVendor.Resource.ToString(), SelectedVendor.ResourceNoteCollection);

                // Show detail form
                IsSearchMode = false;
            }
            else if (IsSearchMode)
            {
                // Show detail form
                IsSearchMode = false;
            }
            else if (ShowNotification(null))
            {
                // Show list form
                IsSearchMode = true;
            }
        }

        #endregion

        #region NewContactCommand

        /// <summary>
        /// Gets the NewContactCommand command.
        /// </summary>
        public ICommand NewContactCommand { get; private set; }

        /// <summary>
        /// Method to check whether the NewContactCommand command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnNewContactCommandCanExecute(object param)
        {
            //return SelectedVendor.ContactCollection.Count(x => x.IsNew && !x.IsAcceptedRow) == 0;
            return true;
        }

        /// <summary>
        /// Method to invoke when the NewContactCommand command is executed.
        /// </summary>
        private void OnNewContactCommandExecute(object param)
        {
            ContactViewModel contactViewModel = new ContactViewModel();
            contactViewModel.ContactModel = SelectedContact;
            contactViewModel.ContactCollection = SelectedVendor.ContactCollection;
            bool? result = _dialogService.ShowDialog<CPC.POS.View.ContactView>(_ownerViewModel, contactViewModel, "Create Contact");
            if (result.HasValue && result.Value)
            {
                SelectedContact.IsAcceptedRow = false;
                IsDirtyContactCollection = true;
                SelectedVendor.ContactCollection.Add(CreateNewContact());
            }
        }

        #endregion

        #region SaveContactCommand

        /// <summary>
        /// Gets the SaveContactCommand command.
        /// </summary>
        public ICommand SaveContactCommand { get; private set; }

        /// <summary>
        /// Method to check whether the SaveContactCommand command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnSaveContactCommandCanExecute()
        {
            return true;
        }

        /// <summary>
        /// Method to invoke when the SaveContactCommand command is executed.
        /// </summary>
        private void OnSaveContactCommandExecute()
        {
            // TODO: Handle command logic here
        }

        #endregion

        #region DeleteContactCommand

        /// <summary>
        /// Gets the DeleteContactCommand command.
        /// </summary>
        public ICommand DeleteContactCommand { get; private set; }

        /// <summary>
        /// Method to check whether the DeleteContactCommand command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnDeleteContactCommandCanExecute(object param)
        {
            return param != null;
        }

        /// <summary>
        /// Method to invoke when the DeleteContactCommand command is executed.
        /// </summary>
        private void OnDeleteContactCommandExecute(object param)
        {
            base_GuestModel contactModel = param as base_GuestModel;
            if (!contactModel.IsAcceptedRow && SelectedVendor.ContactCollection.Count(x => !x.IsAcceptedRow) > 1)
            {
                MessageBoxResultCustom msgResult = MsgControl.ShowWarning("Bạn có muốn xóa liên lạc này ?", "Cãnh báo", MessageBoxButtonCustom.YesNo);
                if (msgResult.Is(MessageBoxResultCustom.Yes))
                {
                    SelectedVendor.ContactCollection.Remove(contactModel);
                    if (contactModel.IsPrimary)
                        SelectedVendor.ContactCollection.FirstOrDefault().IsPrimary = true;
                    IsDirtyContactCollection = true;
                }
            }
        }

        #endregion

        #region PopupContactCommand

        /// <summary>
        /// Gets the PopupContactCommand command.
        /// </summary>
        public ICommand PopupContactCommand { get; private set; }

        /// <summary>
        /// Method to check whether the PopupContactCommand command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnPopupContactCommandCanExecute(object param)
        {
            return true;
        }

        /// <summary>
        /// Method to invoke when the PopupContactCommand command is executed.
        /// </summary>
        private void OnPopupContactCommandExecute(object param)
        {
            if (param != null && !(param as base_GuestModel).IsAcceptedRow)
            {
                ContactViewModel contactViewModel = new ContactViewModel();
                contactViewModel.ContactModel = SelectedContact;
                contactViewModel.ContactCollection = SelectedVendor.ContactCollection;
                bool? result = _dialogService.ShowDialog<CPC.POS.View.ContactView>(_ownerViewModel, contactViewModel, "Update Contact");
                if (result.HasValue && result.Value)
                {
                    IsDirtyContactCollection = true;
                }
            }
        }

        #endregion

        #region LoadStepCommand

        /// <summary>
        /// Gets the LoadStepCommand command.
        /// </summary>
        public ICommand LoadStepCommand { get; private set; }

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
            // Load data by predicate
            LoadVendorDataByPredicate(false, VendorCollection.Count);
        }

        #endregion

        #region AddTermCommand

        /// <summary>
        /// Gets the AddTerm Command.
        /// <summary>
        public ICommand AddTermCommand { get; private set; }

        /// <summary>
        /// Method to check whether the AddTerm command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnAddTermCommandCanExecute()
        {
            return true;
        }

        /// <summary>
        /// Method to invoke when the AddTerm command is executed.
        /// </summary>
        private void OnAddTermCommandExecute()
        {
            short dueDays = SelectedVendor.TermNetDue;
            decimal discount = SelectedVendor.TermDiscount;
            short discountDays = SelectedVendor.TermPaidWithinDay;
            PaymentTermViewModel paymentTermViewModel = new PaymentTermViewModel(dueDays, discount, discountDays);
            bool? dialogResult = _dialogService.ShowDialog<PaymentTermView>(_ownerViewModel, paymentTermViewModel, "Add Term");
            if (dialogResult == true)
            {
                SelectedVendor.TermNetDue = paymentTermViewModel.DueDays;
                SelectedVendor.TermDiscount = paymentTermViewModel.Discount;
                SelectedVendor.TermPaidWithinDay = paymentTermViewModel.DiscountDays;
                SelectedVendor.PaymentTermDescription = paymentTermViewModel.Description;
            }
        }

        #endregion

        #region LoadStepProductCommand

        /// <summary>
        /// Gets the LoadStepProductCommand command.
        /// </summary>
        public ICommand LoadStepProductCommand { get; private set; }

        /// <summary>
        /// Method to check whether the LoadStepProductCommand command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnLoadStepProductCommandCanExecute()
        {
            return true;
        }

        /// <summary>
        /// Method to invoke when the LoadStepProductCommand command is executed.
        /// </summary>
        private void OnLoadStepProductCommandExecute()
        {
            // TODO: Handle command logic here
        }

        #endregion

        #region PopupGuestGroupCommand

        /// <summary>
        /// Gets the PopupGuestGroupCommand command.
        /// </summary>
        public ICommand PopupGuestGroupCommand { get; private set; }

        /// <summary>
        /// Method to check whether the PopupGuestGroupCommand command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnPopupGuestGroupCommandCanExecute()
        {
            return true;
        }

        /// <summary>
        /// Method to invoke when the PopupGuestGroupCommand command is executed.
        /// </summary>
        private void OnPopupGuestGroupCommandExecute()
        {
            PopupAddNewGroupViewModel viewModel = new PopupAddNewGroupViewModel();
            bool? result = _dialogService.ShowDialog<PopupAddNewGroupView>(_ownerViewModel, viewModel, "Add new group");
            if (result.HasValue && result.Value)
            {
                // Add new guest group to collection
                GuestGroupCollection.Add(viewModel.SelectedGuestGroup);

                SelectedVendor.GroupResource = viewModel.SelectedGuestGroup.Resource.ToString();
            }
        }

        #endregion

        #region InsertDateStampCommand

        /// <summary>
        /// Gets the InsertDateStampCommand command.
        /// </summary>
        public ICommand InsertDateStampCommand { get; private set; }

        /// <summary>
        /// Method to check whether the InsertDateStampCommand command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnInsertDateStampCommandCanExecute(object param)
        {
            return param != null;
        }

        /// <summary>
        /// Method to invoke when the InsertDateStampCommand command is executed.
        /// </summary>
        private void OnInsertDateStampCommandExecute(object param)
        {
            CPCToolkitExt.TextBoxControl.TextBox remarkTextBox = param as CPCToolkitExt.TextBoxControl.TextBox;
            SetValueControlHelper.InsertTimeStamp(remarkTextBox);
        }

        #endregion

        #endregion

        #region Private Methods

        /// <summary>
        /// Load static data
        /// </summary>
        private void LoadStaticData()
        {
            // Created by Thaipn
            Parameter = new Common();

            // Get address type collection
            // Created by Thaipn
            AddressTypeCollection = new AddressTypeCollection();
            this.AddressTypeCollection.Add(new AddressTypeModel { ID = 0, Name = Language.GetMsg("Address_Home") });
            this.AddressTypeCollection.Add(new AddressTypeModel { ID = 1, Name = Language.GetMsg("Address_Business") });
            this.AddressTypeCollection.Add(new AddressTypeModel { ID = 2, Name = Language.GetMsg("Address_Billing") });
            this.AddressTypeCollection.Add(new AddressTypeModel { ID = 3, Name = Language.GetMsg("Address_Shipping") });

            if (SaleTaxCollection == null)
            {
                AllSaleTax = _saleTaxLocationRepository.GetAll().ToList();

                // Initial sale tax collection
                SaleTaxCollection = new ObservableCollection<base_SaleTaxLocationModel>(AllSaleTax.Where(x => x.ParentId == 0).
                    Select(x => new base_SaleTaxLocationModel(x)));

                // Add Item null for sale tax using radio button set none sale tax
                base_SaleTaxLocationModel saleTaxNone = new base_SaleTaxLocationModel()
                {
                    Id = 0,
                    ParentId = 0,
                    Name = string.Empty
                };
                SaleTaxCollection.Insert(0, saleTaxNone);
            }

            // Load category list
            if (CategoryList == null)
            {
                CategoryList = new List<base_DepartmentModel>(_departmentRepository.
                        GetAll(x => x.IsActived == true && x.LevelId == 1).
                        Select(x => new base_DepartmentModel(x)));
            }

            // Load UOM list
            if (UOMList == null)
            {
                UOMList = new ObservableCollection<CheckBoxItemModel>(_uomRepository.GetIQueryable(x => x.IsActived).
                        OrderBy(x => x.Name).Select(x => new CheckBoxItemModel { Value = x.Id, Text = x.Name }));
            }

            // Load guest group collection
            if (GuestGroupCollection == null)
            {
                GuestGroupCollection = new ObservableCollection<base_GuestGroupModel>(_guestGroupRepository.GetAll().
                        Select(x => new base_GuestGroupModel(x) { GuestGroupResource = x.Resource.ToString() }));
            }
        }

        /// <summary>
        /// Initial commands
        /// </summary>
        private void InitialCommand()
        {
            SearchCommand = new RelayCommand<object>(OnSearchCommandExecute, OnSearchCommandCanExecute);
            NewCommand = new RelayCommand(OnNewCommandExecute, OnNewCommandCanExecute);
            EditCommand = new RelayCommand<object>(OnEditCommandExecute, OnEditCommandCanExecute);
            SaveCommand = new RelayCommand(OnSaveCommandExecute, OnSaveCommandCanExecute);
            DeleteCommand = new RelayCommand(OnDeleteCommandExecute, OnDeleteCommandCanExecute);
            DoubleClickViewCommand = new RelayCommand<object>(OnDoubleClickViewCommandExecute, OnDoubleClickViewCommandCanExecute);
            NewContactCommand = new RelayCommand<object>(OnNewContactCommandExecute, OnNewContactCommandCanExecute);
            SaveContactCommand = new RelayCommand(OnSaveContactCommandExecute, OnSaveContactCommandCanExecute);
            DeleteContactCommand = new RelayCommand<object>(OnDeleteContactCommandExecute, OnDeleteContactCommandCanExecute);
            PopupContactCommand = new RelayCommand<object>(OnPopupContactCommandExecute, OnPopupContactCommandCanExecute);
            LoadStepCommand = new RelayCommand<object>(OnLoadStepCommandExecute, OnLoadStepCommandCanExecute);
            AddTermCommand = new RelayCommand(OnAddTermCommandExecute, OnAddTermCommandCanExecute);
            LoadStepProductCommand = new RelayCommand(OnLoadStepProductCommandExecute, OnLoadStepProductCommandCanExecute);
            DeletesCommand = new RelayCommand<object>(OnDeletesCommandExecute, OnDeletesCommandCanExecute);
            MergeItemCommand = new RelayCommand<object>(OnMergeItemCommandExecute, OnMergeItemCommandCanExecute);
            PurchaseOrderCommand = new RelayCommand<object>(OnPurchaseOrderCommandExecute, OnPurchaseOrderCommandCanExecute);
            PopupGuestGroupCommand = new RelayCommand(OnPopupGuestGroupCommandExecute, OnPopupGuestGroupCommandCanExecute);
            DuplicateCommand = new RelayCommand<object>(OnDuplicateCommandExecute, OnDuplicateCommandCanExecute);
            InsertDateStampCommand = new RelayCommand<object>(OnInsertDateStampCommandExecute, OnInsertDateStampCommandCanExecute);
        }

        /// <summary>
        /// Check has edit on form
        /// </summary>
        /// <returns></returns>
        private bool IsEdit()
        {
            if (SelectedVendor == null)
                return false;

            //Repaired by Thaipn.
            return (SelectedVendor.IsDirty || SelectedVendor.AdditionalModel.IsDirty ||
                (SelectedVendor.AddressControlCollection != null && SelectedVendor.AddressControlCollection.IsEditingData) ||
                (SelectedVendor.PhotoCollection != null && SelectedVendor.PhotoCollection.IsDirty) ||
                IsDirtyContactCollection);
        }

        /// <summary>
        /// Notify when exit or change form
        /// </summary>
        /// <returns>True is continue action</returns>
        private bool ShowNotification(bool? isClosing)
        {
            bool result = true;

            // Check data is edited
            if (IsEdit())
            {
                // Show notification when data has changed
                MessageBoxResultCustom msgResult = MsgControl.ShowWarning("Dữ liệu đã bị thay đổi,bạn có muốn lưu chúng không ?", "Cảnh báo", MessageBoxButtonCustom.YesNoCancel);

                if (msgResult.Is(MessageBoxResultCustom.Cancel))
                {
                    return false;
                }
                if (msgResult.Is(MessageBoxResultCustom.Yes))
                {
                    if (OnSaveCommandCanExecute())
                    {
                        // Call Save function
                        result = SaveVendor(SelectedVendor);
                    }
                    else
                    {
                        result = false;
                    }

                    // Close all popup sticky
                    StickyManagementViewModel.CloseAllPopupSticky();
                }
                else
                {
                    if (SelectedVendor.IsNew)
                    {
                        // Remove all popup sticky
                        StickyManagementViewModel.DeleteAllResourceNote();

                        SelectedVendor = null;
                        if (isClosing.HasValue && !isClosing.Value)
                        {
                            IsSearchMode = true;
                        }
                    }
                    else
                    {
                        // Rollback vendor
                        SelectedVendor.AddressCollection = null;
                        SelectedVendor.PhotoCollection = null;
                        SelectedVendor.AdditionalModel = null;
                        SelectedVendor.ProductCollection = null;
                        SelectedVendor.PurchaseOrderCollection = null;
                        SelectedVendor.ContactCollection = null;
                        SelectedVendor.ToModelAndRaise();
                        SelectedVendor.EndUpdate();
                        IsDirtyContactCollection = false;
                        LoadRelationVendorData(SelectedVendor);

                        // Close all popup sticky
                        StickyManagementViewModel.CloseAllPopupSticky();
                    }
                }
            }
            else
            {
                if (SelectedVendor != null && SelectedVendor.IsNew)
                {
                    // Remove all popup sticky
                    StickyManagementViewModel.DeleteAllResourceNote();
                }
                else
                {
                    // Close all popup sticky
                    StickyManagementViewModel.CloseAllPopupSticky();
                }
            }

            // Clear selected item
            if (result && isClosing == null && SelectedVendor != null)
                SelectedVendor = null;

            return result;
        }

        /// <summary>
        /// Create a new contact
        /// </summary>
        /// <param name="isPrimary">Default is false</param>
        /// <returns>New contact</returns>
        private base_GuestModel CreateNewContact(bool isPrimary = false)
        {
            return new base_GuestModel
            {
                IsPrimary = isPrimary,
                DateCreated = DateTimeExt.Now,
                GuestNo = DateTimeExt.Now.ToString(Define.GuestNoFormat),
                Mark = MarkType.Contact.ToDescription(),
                PositionId = 0,
                Resource = Guid.NewGuid(),
                IsAcceptedRow = true
            };
        }

        /// <summary>
        /// Create new a VendorModel and some default value
        /// </summary>
        private void NewVendor()
        {
            SelectedVendor = new base_GuestModel { Mark = MarkType.Vendor.ToDescription() };
            SelectedVendor.IsActived = true;
            SelectedVendor.DateCreated = DateTimeExt.Now;
            SelectedVendor.GuestNo = DateTimeExt.Now.ToString(Define.GuestNoFormat);
            SelectedVendor.PositionId = 0;
            SelectedVendor.Resource = Guid.NewGuid();
            SelectedVendor.UserCreated = Define.USER.LoginName;
            SelectedVendor.Shift = Define.ShiftCode;

            // Initial address control collection
            // Created by Thaipn.
            SelectedVendor.AddressControlCollection = new AddressControlCollection();
            SelectedVendor.AddressControlCollection.Add(new AddressControlModel
            {
                IsDefault = true,
                IsNew = true
            });

            SelectedVendor.PhotoCollection = new CollectionBase<base_ResourcePhotoModel>();
            SelectedVendor.AdditionalModel = new base_GuestAdditionalModel();
            SelectedVendor.ContactCollection = new CollectionBase<base_GuestModel>();
            SelectedVendor.ResourceNoteCollection = new CollectionBase<base_ResourceNoteModel>();
            StickyManagementViewModel.SetParentResource(SelectedVendor.Resource.ToString(), SelectedVendor.ResourceNoteCollection);

            // Turn off IsDirty
            SelectedVendor.IsDirty = false;

            FocusDefault = false;
            FocusDefault = true;
        }

        /// <summary>
        /// Function save Vendor
        /// </summary>
        /// <param name="param"></param>
        private bool SaveVendor(base_GuestModel vendorModel)
        {
            // Check duplicate vendor
            if (CheckDuplicateVendor(vendorModel))
                return false;
            //To save picture.
            if (this.SelectedVendor.PhotoCollection != null && this.SelectedVendor.PhotoCollection.Count > 0)
            {
                this.SelectedVendor.PhotoCollection.FirstOrDefault().IsNew = false;
                this.SelectedVendor.PhotoCollection.FirstOrDefault().IsDirty = false;
                this.SelectedVendor.Picture = this.SelectedVendor.PhotoCollection.FirstOrDefault().ImageBinary;
            }
            else
                this.SelectedVendor.Picture = null;
            if (this.SelectedVendor.PhotoCollection.DeletedItems != null &&
                 this.SelectedVendor.PhotoCollection.DeletedItems.Count > 0)
                this.SelectedVendor.PhotoCollection.DeletedItems.Clear();
            //// Handle clear data if user not choose "Tax Information" in Additional with FexTaxId & TaxLocation
            //if (vendorModel.AdditionalModel.TaxInfoType.Is(TaxInfoType.FedTaxID))
            //    vendorModel.AdditionalModel.SaleTaxLocation = 0;
            //else
            //    vendorModel.AdditionalModel.FedTaxId = string.Empty;

            // Get group name for vendor
            if (vendorModel.base_Guest.GroupResource != vendorModel.GroupResource)
            {
                base_GuestGroupModel guestGroupItem = GuestGroupCollection.FirstOrDefault(x => x.Resource.ToString().Equals(vendorModel.GroupResource));
                if (guestGroupItem != null)
                    vendorModel.GroupName = guestGroupItem.Name;
            }

            // Vendor is create new
            if (vendorModel.IsNew)
            {
                // Insert a new vendor
                SaveNew();

                //if (vendorModel.ContactCollection.Count == 0)
                //{
                //    vendorModel.ContactCollection.Add(CreateNewContact(true));
                //}
            }
            else // Vendor is edited
            {
                // Update vendor
                SaveUpdate();
            }

            // Turn off IsDirty & IsNew
            vendorModel.EndUpdate();
            vendorModel.AdditionalModel.EndUpdate();
            IsDirtyContactCollection = false;

            return true;
        }

        /// <summary>
        /// Save when create new Vendor
        /// </summary>
        private void SaveNew()
        {
            // Map data from model to entity
            SelectedVendor.ToEntity();

            // Insert address
            // Created by Thaipn
            foreach (AddressControlModel addressControlModel in this.SelectedVendor.AddressControlCollection)
            {
                base_GuestAddressModel addressModel = new base_GuestAddressModel();
                addressModel.DateCreated = DateTimeExt.Now;
                //addressModel.UserCreated = string.Empty;
                // Map date from AddressControlModel to AddressModel
                addressModel.ToModel(addressControlModel);

                // Update default address
                if (addressModel.IsDefault)
                    SelectedVendor.AddressModel = addressModel;

                // Map data from model to entity
                addressModel.ToEntity();
                SelectedVendor.base_Guest.base_GuestAddress.Add(addressModel.base_GuestAddress);

                // Turn off IsDirty & IsNew
                addressModel.EndUpdate();

                addressControlModel.IsNew = false;
                addressControlModel.IsDirty = false;
            }

            // Save image
            if (SelectedVendor.PhotoCollection != null && SelectedVendor.PhotoCollection.Count > 0)
            {
                foreach (base_ResourcePhotoModel photoModel in SelectedVendor.PhotoCollection.Where(x => x.IsNew))
                {
                    //photoModel.LargePhotoFilename = new System.IO.FileInfo(photoModel.ImagePath).Name;
                    photoModel.LargePhotoFilename = DateTimeExt.Now.ToString(Define.GuestNoFormat) + Guid.NewGuid().ToString().Substring(0, 8) + new System.IO.FileInfo(photoModel.ImagePath).Extension;

                    // Update resource photo
                    photoModel.Resource = SelectedVendor.Resource.ToString();

                    // Map data from model to entity
                    photoModel.ToEntity();
                    _photoRepository.Add(photoModel.base_ResourcePhoto);

                    // Copy image from client to server
                    SaveImage(photoModel);

                    // Turn off IsDirty & IsNew
                    photoModel.EndUpdate();
                }
            }

            // Update default photo if it is deleted
            SelectedVendor.PhotoDefault = SelectedVendor.PhotoCollection.FirstOrDefault();

            foreach (base_GuestModel contactModel in SelectedVendor.ContactCollection.Where(x => x.IsDirty))
            {
                // Create new personal info for contact
                if (contactModel.PersonalInfoModel != null)
                {
                    // Map data from model to entity
                    contactModel.PersonalInfoModel.ToEntity();

                    // Create new personal info
                    contactModel.base_Guest.base_GuestProfile.Add(contactModel.PersonalInfoModel.base_GuestProfile);

                    // Turn off IsDirty & IsNew
                    contactModel.PersonalInfoModel.EndUpdate();
                }

                // Map data from model to entity
                contactModel.ToEntity();
                SelectedVendor.base_Guest.base_Guest1.Add(contactModel.base_Guest);
            }

            SelectedVendor.AdditionalModel.ToEntity();
            SelectedVendor.base_Guest.base_GuestAdditional.Add(SelectedVendor.AdditionalModel.base_GuestAdditional);

            _guestRepository.Add(SelectedVendor.base_Guest);
            _guestRepository.Commit();

            // Update ID from entity to model
            SelectedVendor.Id = SelectedVendor.base_Guest.Id;
            SelectedVendor.AdditionalModel.GuestId = SelectedVendor.base_Guest.Id;
            SelectedVendor.AdditionalModel.Id = SelectedVendor.AdditionalModel.base_GuestAdditional.Id;
            foreach (base_ResourcePhotoModel photoModel in SelectedVendor.PhotoCollection)
            {
                photoModel.Id = photoModel.base_ResourcePhoto.Id;

                // Turn off IsDirty & IsNew
                photoModel.EndUpdate();
            }

            foreach (base_GuestModel contactModel in SelectedVendor.ContactCollection.Where(x => !x.IsAcceptedRow))
            {
                contactModel.Id = contactModel.base_Guest.Id;
                contactModel.ParentId = SelectedVendor.base_Guest.Id;

                // Turn off IsDirty & IsNew
                contactModel.EndUpdate();
            }

            // Push new vendor to collection
            VendorCollection.Insert(0, SelectedVendor);
        }

        /// <summary>
        /// Update a vendor
        /// </summary>
        private void SaveUpdate()
        {
            SelectedVendor.DateUpdated = DateTimeExt.Now;
            if (Define.USER != null)
                SelectedVendor.UserUpdated = Define.USER.LoginName;

            // Map data from model to entity
            SelectedVendor.ToEntity();

            #region Save address

            // Insert or update address
            // Created by Thaipn
            foreach (AddressControlModel addressControlModel in this.SelectedVendor.AddressControlCollection.Where(x => x.IsDirty))
            {
                base_GuestAddressModel addressModel = new base_GuestAddressModel();

                // Insert new address
                if (addressControlModel.IsNew)
                {
                    addressModel.DateCreated = DateTimeExt.Now;
                    //addressModel.UserCreated = string.Empty;
                    // Map date from AddressControlModel to AddressModel
                    addressModel.ToModel(addressControlModel);

                    // Map data from model to entity
                    addressModel.ToEntity();
                    SelectedVendor.base_Guest.base_GuestAddress.Add(addressModel.base_GuestAddress);
                }
                // Update address
                else
                {
                    base_GuestAddress address = SelectedVendor.base_Guest.base_GuestAddress.SingleOrDefault(x => x.AddressTypeId == addressControlModel.AddressTypeID);
                    addressModel = new base_GuestAddressModel(address);

                    addressModel.DateUpdated = DateTimeExt.Now;
                    //addressModel.UserUpdated = string.Empty;
                    // Map date from AddressControlModel to AddressModel
                    addressModel.ToModel(addressControlModel);
                    addressModel.ToEntity();
                }

                // Update default address
                if (addressModel.IsDefault)
                    SelectedVendor.AddressModel = addressModel;

                // Turn off IsDirty & IsNew
                addressModel.EndUpdate();

                addressControlModel.IsNew = false;
                addressControlModel.IsDirty = false;
            }

            #endregion

            #region Save photo

            // Remove photo were deleted
            if (SelectedVendor.PhotoCollection != null &&
                SelectedVendor.PhotoCollection.DeletedItems != null && SelectedVendor.PhotoCollection.DeletedItems.Count > 0)
            {
                foreach (base_ResourcePhotoModel photoModel in SelectedVendor.PhotoCollection.DeletedItems)
                {
                    //System.IO.FileInfo fileInfo = new System.IO.FileInfo(photoModel.ImagePath);
                    //fileInfo.MoveTo(photoModel.ImagePath + "temp");
                    //System.IO.FileInfo fileInfoTemp = new System.IO.FileInfo(photoModel.ImagePath + "temp");
                    //fileInfoTemp.Delete();

                    _photoRepository.Delete(photoModel.base_ResourcePhoto);
                }
                SelectedVendor.PhotoCollection.DeletedItems.Clear();
            }

            // Update photo
            if (SelectedVendor.PhotoCollection != null && SelectedVendor.PhotoCollection.Count > 0)
            {
                foreach (base_ResourcePhotoModel photoModel in SelectedVendor.PhotoCollection.Where(x => x.IsDirty))
                {
                    //photoModel.LargePhotoFilename = new System.IO.FileInfo(photoModel.ImagePath).Name;
                    photoModel.LargePhotoFilename = DateTimeExt.Now.ToString(Define.GuestNoFormat) + Guid.NewGuid().ToString().Substring(0, 8) + new System.IO.FileInfo(photoModel.ImagePath).Extension;

                    // Update resource photo
                    if (string.IsNullOrWhiteSpace(photoModel.Resource))
                        photoModel.Resource = SelectedVendor.Resource.ToString();

                    // Map data from model to entity
                    photoModel.ToEntity();

                    if (photoModel.IsNew)
                        _photoRepository.Add(photoModel.base_ResourcePhoto);

                    // Copy image from client to server
                    SaveImage(photoModel);

                    // Turn off IsDirty & IsNew
                    photoModel.EndUpdate();
                }
            }

            // Update default photo if it is deleted
            SelectedVendor.PhotoDefault = SelectedVendor.PhotoCollection.FirstOrDefault();

            #endregion

            #region Save contact

            if (SelectedVendor.ContactCollection != null)
            {
                // Remove contact were deleted
                if (SelectedVendor.ContactCollection.DeletedItems != null)
                {
                    foreach (base_GuestModel contactModel in SelectedVendor.ContactCollection.DeletedItems)
                    {
                        if (!contactModel.IsNew)
                            contactModel.base_Guest.IsPurged = true;

                        //if (contactModel.IsNew)
                        //    SelectedVendor.ContactCollection.Remove(contactModel);
                        //else
                        //{
                        //    SelectedVendor.base_Guest.base_Guest1.Remove(contactModel.base_Guest);
                        //    _guestRepository.Delete(contactModel.base_Guest);
                        //}
                    }
                    SelectedVendor.ContactCollection.DeletedItems.Clear();
                }

                // Update contact
                foreach (base_GuestModel contactModel in SelectedVendor.ContactCollection.
                    Where(x => !x.IsAcceptedRow && (x.IsDirty || (x.PersonalInfoModel != null && x.PersonalInfoModel.IsDirty))))
                {
                    // Update personal info for contact
                    if (contactModel.PersonalInfoModel != null)
                    {
                        // Map data from model to entity
                        contactModel.PersonalInfoModel.ToEntity();

                        // Create new personal info
                        if (contactModel.PersonalInfoModel.IsNew)
                            contactModel.base_Guest.base_GuestProfile.Add(contactModel.PersonalInfoModel.base_GuestProfile);

                        // Turn off IsDirty & IsNew
                        contactModel.PersonalInfoModel.EndUpdate();
                    }

                    // Create new contact
                    if (contactModel.IsNew)
                    {
                        //if (contactModel.IsPrimary)
                        //    contactModel.IsPrimary = false;

                        // Map data from model to entity
                        contactModel.ToEntity();
                        SelectedVendor.base_Guest.base_Guest1.Add(contactModel.base_Guest);
                    }
                    else // Update contact
                        // Map data from model to entity
                        contactModel.ToEntity();

                    // Turn off IsDirty & IsNew
                    contactModel.EndUpdate();
                }
            }

            #endregion

            SelectedVendor.AdditionalModel.GuestId = SelectedVendor.Id;

            // Map data from model to entity
            SelectedVendor.AdditionalModel.ToEntity();

            if (SelectedVendor.base_Guest.base_GuestAdditional.Count == 0)
                SelectedVendor.base_Guest.base_GuestAdditional.Add(SelectedVendor.AdditionalModel.base_GuestAdditional);

            _guestRepository.Commit();

            foreach (base_GuestModel contactModel in SelectedVendor.ContactCollection.Where(x => !x.IsAcceptedRow && !x.ParentId.HasValue))
            {
                contactModel.Id = contactModel.base_Guest.Id;
                contactModel.ParentId = SelectedVendor.base_Guest.Id;

                // Turn off IsDirty & IsNew
                contactModel.EndUpdate();
            }
            if (SelectedVendor.AdditionalModel.IsNew)
                SelectedVendor.AdditionalModel.Id = SelectedVendor.AdditionalModel.base_GuestAdditional.Id;
        }

        /// <summary>
        /// Create predicate
        /// </summary>
        /// <returns></returns>
        private Expression<Func<base_Guest, bool>> CreateSearchVendorPredicate(string keyword)
        {
            // Initial predicate
            Expression<Func<base_Guest, bool>> predicate = PredicateBuilder.True<base_Guest>();

            // Set conditions for predicate
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
                if (SearchOption.Has(SearchOptions.Email))
                {
                    predicate = predicate.And(x => x.Email.ToLower().Contains(keyword.ToLower()));
                }
                if (SearchOption.Has(SearchOptions.Phone))
                {
                    predicate = predicate.And(x => x.Phone1.ToLower().Contains(keyword.ToLower()) || x.Phone2.ToLower().Contains(keyword.ToLower()));
                }
                if (SearchOption.Has(SearchOptions.Group))
                {
                    // Get all guest group that contain keyword
                    IEnumerable<base_GuestGroupModel> guestGroups = GuestGroupCollection.Where(x => x.Name.ToLower().Contains(keyword.ToLower()));
                    IEnumerable<string> guestGroupResourceList = guestGroups.Select(x => x.Resource.ToString());

                    // Get all product that contain in guest group resource list
                    if (guestGroupResourceList.Count() > 0)
                        predicate = predicate.And(x => guestGroupResourceList.Contains(x.GroupResource));
                    else
                        // If condition in predicate is false, GetRange function can not get data from database.
                        // Solution for this problem is create fake condition
                        predicate = predicate.And(x => x.Id < 0);
                }
            }

            // Default condition
            predicate = predicate.And(x => !x.IsPurged && x.Mark.Equals(_vendorMark));

            return predicate;
        }

        /// <summary>
        /// Method get Data from database
        /// <para>Using load on the first time</para>
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="refreshData"></param>
        /// <param name="currentIndex">index to load if index =0 , clear collection</param>
        private void LoadVendorDataByPredicate(bool refreshData = false, int currentIndex = 0)
        {
            // Create predicate
            Expression<Func<base_Guest, bool>> predicate = CreateSearchVendorPredicate(Keyword);

            // Create background worker
            BackgroundWorker bgWorker = new BackgroundWorker { WorkerReportsProgress = true };

            if (currentIndex == 0)
                VendorCollection.Clear();
            bgWorker.DoWork += (sender, e) =>
            {
                // Turn on BusyIndicator
                if (Define.DisplayLoading)
                    IsBusy = true;

                if (refreshData)
                {
                    //_guestRepository.Refresh();
                    //_addressRepository.Refresh();
                    //_photoRepository.Refresh();
                    //_additionalRepository.Refresh();
                }

                // Get total vendors with condition in predicate
                TotalVendors = _guestRepository.GetIQueryable(predicate).Count();

                // Get data with range
                IList<base_Guest> vendors = _guestRepository.GetRangeDescending(currentIndex, NumberOfDisplayItems, x => x.DateCreated, predicate);
                foreach (base_Guest vendor in vendors)
                {
                    bgWorker.ReportProgress(0, vendor);
                }
            };

            bgWorker.ProgressChanged += (sender, e) =>
            {
                // Create vendor model
                base_GuestModel vendorModel = new base_GuestModel((base_Guest)e.UserState);

                // Load relation data
                LoadRelationVendorData(vendorModel);

                // Add to collection
                VendorCollection.Add(vendorModel);
            };

            bgWorker.RunWorkerCompleted += (sender, e) =>
            {
                // Turn off BusyIndicator
                IsBusy = false;
            };

            // Run async background worker
            bgWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Load relation data for vendor
        /// </summary>
        /// <param name="vendorModel"></param>
        private void LoadRelationVendorData(base_GuestModel vendorModel)
        {
            // Get group name for vendor
            if (string.IsNullOrWhiteSpace(vendorModel.GroupName))
            {
                base_GuestGroupModel guestGroupItem = GuestGroupCollection.FirstOrDefault(x => x.Resource.ToString().Equals(vendorModel.GroupResource));
                if (guestGroupItem != null)
                    vendorModel.GroupName = guestGroupItem.Name;
            }

            // Load Address
            LoadAddressCollection(vendorModel);

            // Load Photo
            LoadPhotoCollection(vendorModel);

            // Load Additional
            LoadAdditionalModel(vendorModel);

            // Load Contact
            LoadContactCollection(vendorModel);

            // Load resource note collection
            LoadResourceNoteCollection(vendorModel);
        }

        /// <summary>
        /// Load address collection
        /// </summary>
        /// <param name="vendorModel"></param>
        private void LoadAddressCollection(base_GuestModel vendorModel)
        {
            if (vendorModel.AddressCollection == null)
            {
                vendorModel.AddressCollection = new ObservableCollection<base_GuestAddressModel>(
                    vendorModel.base_Guest.base_GuestAddress.Select(x => new base_GuestAddressModel(x)));
                vendorModel.AddressModel = vendorModel.AddressCollection.SingleOrDefault(x => x.IsDefault);
                vendorModel.AddressControlCollection = new AddressControlCollection();
                foreach (base_GuestAddressModel addressModel in vendorModel.AddressCollection)
                {
                    AddressControlModel addressControlModel = addressModel.ToAddressControlModel();
                    addressControlModel.IsDirty = false;
                    vendorModel.AddressControlCollection.Add(addressControlModel);
                }
            }
        }

        /// <summary>
        /// Load photo collection
        /// </summary>
        /// <param name="vendorModel"></param>
        private void LoadPhotoCollection(base_GuestModel vendorModel)
        {
            if (vendorModel.PhotoCollection == null)
            {
                vendorModel.PhotoCollection = new CollectionBase<base_ResourcePhotoModel>();
                if (vendorModel.Picture != null && vendorModel.Picture.Length > 0)
                {
                    base_ResourcePhotoModel ResourcePhotoModel = new base_ResourcePhotoModel();
                    ResourcePhotoModel.ImageBinary = vendorModel.Picture;
                    ResourcePhotoModel.IsDirty = false;
                    ResourcePhotoModel.IsNew = false;
                    vendorModel.PhotoCollection.Add(ResourcePhotoModel);
                    //// Set default photo
                    vendorModel.PhotoDefault = vendorModel.PhotoCollection.FirstOrDefault();
                }
            }
        }

        /// <summary>
        /// Load additional model
        /// </summary>
        /// <param name="vendorModel"></param>
        private void LoadAdditionalModel(base_GuestModel vendorModel)
        {
            if (vendorModel.AdditionalModel == null)
            {
                if (vendorModel.base_Guest.base_GuestAdditional.Count > 0)
                {
                    vendorModel.AdditionalModel = new base_GuestAdditionalModel(
                        vendorModel.base_Guest.base_GuestAdditional.FirstOrDefault());
                    ////Set Tax Infomation with FeedTaxId & TaxLocation
                    //if (vendorModel.AdditionalModel.SaleTaxLocation == 0)
                    //    vendorModel.AdditionalModel.TaxInfoType = (int)TaxInfoType.FedTaxID;
                    //else
                    //    vendorModel.AdditionalModel.TaxInfoType = (int)TaxInfoType.TaxLocation;
                    vendorModel.AdditionalModel.EndUpdate();
                }
                else
                    vendorModel.AdditionalModel = new base_GuestAdditionalModel();
            }
        }

        /// <summary>
        /// Load contact collection
        /// </summary>
        /// <param name="vendorModel"></param>
        private void LoadContactCollection(base_GuestModel vendorModel)
        {
            if (vendorModel.ContactCollection == null)
            {
                vendorModel.ContactCollection = new CollectionBase<base_GuestModel>(
                    vendorModel.base_Guest.base_Guest1.Where(y => !y.IsPurged).Select(y => new base_GuestModel(y)));

                vendorModel.ContactCollection.Add(
                    CreateNewContact(!vendorModel.ContactCollection.Any(x => x.IsPrimary)));
            }

            // Add new temporary contact
            vendorModel.ContactModel = new base_GuestModel();
        }

        /// <summary>
        /// Load purchase order collection
        /// </summary>
        /// <param name="vendorModel"></param>
        private void LoadPurchaseOrderCollection(base_GuestModel vendorModel)
        {
            if (vendorModel.PurchaseOrderCollection == null)
            {
                // Get vendor resource
                string vendorResource = vendorModel.Resource.ToString();

                // Initial purchase order collection
                vendorModel.PurchaseOrderCollection = new ObservableCollection<base_PurchaseOrderModel>(_purchaseOrderRepository.
                    GetAll(x => x.VendorResource.Equals(vendorResource)).Select(x => new base_PurchaseOrderModel(x)));

                TotalPurchaseOrder = new base_PurchaseOrderModel
                {
                    Total = vendorModel.PurchaseOrderCollection.Sum(x => x.Total),
                    Paid = vendorModel.PurchaseOrderCollection.Sum(x => x.Paid),
                    Balance = vendorModel.PurchaseOrderCollection.Sum(x => x.Balance)
                };
            }
        }

        /// <summary>
        /// Load product vendor collection
        /// </summary>
        /// <param name="vendorModel"></param>
        private void LoadProductVendorCollection(base_GuestModel vendorModel)
        {
            if (vendorModel.ProductCollection == null)
            {
                // Initial purchase order collection
                vendorModel.ProductCollection = new ObservableCollection<base_ProductModel>();

                // Get all vendor product of selected vendor
                IEnumerable<base_VendorProduct> vendorProducts = _vendorProductRepository.GetIEnumerable(x => x.VendorId.Equals(vendorModel.Id));
                IEnumerable<long> productIDList = vendorProducts.Select(x => x.ProductId);

                // Get all product of selected vendor
                IList<base_Product> products = _productRepository.GetAll(x => x.IsPurge == false && (x.VendorId.Equals(vendorModel.Id) || productIDList.Count(y => y.Equals(x.Id)) > 0));

                foreach (base_Product product in products)
                {
                    base_ProductModel productModel = new base_ProductModel(product);
                    LoadRelationProductData(productModel);
                    vendorModel.ProductCollection.Add(productModel);

                    // Turn off IsDirty & IsNew
                    productModel.EndUpdate();
                }
            }
        }

        /// <summary>
        /// Load Custom Field from db
        /// </summary>
        /// <param name="customerModel"></param>
        private void LoadCustomFieldCollection()
        {
            IEnumerable<base_CustomField> customerFields = _customFieldRepository.GetAll().Where(x => x.Mark.Equals(_vendorMark));
            _customFieldRepository.Refresh(customerFields);
            if (customerFields != null && customerFields.Any())
            {
                CustomFieldCollection = new ObservableCollection<base_CustomFieldModel>(customerFields.OrderBy(x => x.Id).Select(x => new base_CustomFieldModel(x)));
            }
            else
            {
                CustomFieldCollection = new ObservableCollection<base_CustomFieldModel>();
                for (int i = 1; i < 9; i++)
                {
                    base_CustomFieldModel customFieldModel = new base_CustomFieldModel();
                    customFieldModel.Mark = _vendorMark;
                    customFieldModel.FieldName = "Custom " + i;
                    customFieldModel.IsShow = true;
                    customFieldModel.Label = customFieldModel.FieldName;
                    customFieldModel.ToEntity();
                    //insert to db
                    _customFieldRepository.Add(customFieldModel.base_CustomField);
                    customFieldModel.EndUpdate();
                    //Add To Collection
                    CustomFieldCollection.Add(customFieldModel);
                }
                _customFieldRepository.Commit();
            }
        }

        /// <summary>
        /// Check Customer is duplicate
        /// <para>Value Compare : Email,Phone1</para>
        /// </summary>
        /// 
        /// <param name="vendorModel"></param>
        /// 
        /// <returns></returns>
        private bool CheckDuplicateVendor(base_GuestModel vendorModel)
        {
            bool result = false;
            IQueryable<base_Guest> query = _guestRepository.
                GetIQueryable(x => !x.GuestNo.Equals(vendorModel.GuestNo) && x.Mark.Equals(_vendorMark) && !x.IsPurged &&
                    (x.Phone1.Equals(vendorModel.Phone1) ||
                    x.Email.ToLower().Equals(vendorModel.Email.ToLower()) ||
                    x.AccountNumber.ToLower().Equals(vendorModel.AccountNumber.ToLower())));
            if (query.Count() > 0)
            {
                result = true;
                MessageBoxResultCustom resultMsg = MsgControl.ShowInfomation("Nhà cung cấp đã tồn tại. Vui lòng kiểm tra lại Tài khoản,địa chỉ mail hoặc số điện thoại. Bạn có muốn xem thông tin đó ?", "Thông tin", MessageBoxButtonCustom.YesNo);
                if (MessageBoxResultCustom.Yes.Is(resultMsg))
                {
                    base_GuestModel guestModel = new base_GuestModel(query.FirstOrDefault());
                    if (guestModel.base_Guest.base_GuestProfile.Count > 0)
                        guestModel.PersonalInfoModel = new base_GuestProfileModel(guestModel.base_Guest.base_GuestProfile.FirstOrDefault());
                    else
                        guestModel.PersonalInfoModel = new base_GuestProfileModel();
                    ViewProfileViewModel viewProfileViewModel = new ViewProfileViewModel();
                    viewProfileViewModel.GuestModel = guestModel;
                    _dialogService.ShowDialog<ViewProfile>(_ownerViewModel, viewProfileViewModel, "View Profile");
                }
            }
            return result;
        }

        private void OnSelectedTabIndexChanged()
        {
            switch (SelectedTabIndex)
            {
                case 1: // Additional Tab
                    break;
                case 2: // Product Vendor Tab
                    // Load product by selected vendor id
                    LoadProductVendorCollection(SelectedVendor);
                    break;
                case 3: // Purchase Order History Tab
                    // Load purchase order collection
                    LoadPurchaseOrderCollection(SelectedVendor);
                    break;
            }
        }

        #region VendorProductTab

        /// <summary>
        /// Create predicate with condition for search
        /// </summary>
        /// <param name="keyword">Keyword</param>
        /// <returns>Expression</returns>
        private Expression<Func<base_Product, bool>> CreateSearchProductPredicate(string keyword)
        {
            // Initial predicate
            Expression<Func<base_Product, bool>> predicate = PredicateBuilder.True<base_Product>();

            // Default condition
            predicate = predicate.And(x => x.IsPurge == false && x.VendorId.Equals(SelectedVendor.Id));

            return predicate;
        }

        /// <summary>
        /// Method get Data from database
        /// <para>Using load on the first time</para>
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="refreshData"></param>
        /// <param name="currentIndex">index to load if index =0 , clear collection</param>
        private void LoadProductDataByPredicate(bool refreshData = false, int currentIndex = 0)
        {
            // Create predicate
            Expression<Func<base_Product, bool>> predicate = CreateSearchProductPredicate(Keyword);

            // Create background worker
            BackgroundWorker bgWorker = new BackgroundWorker { WorkerReportsProgress = true };

            if (currentIndex == 0)
                SelectedVendor.ProductCollection = new ObservableCollection<base_ProductModel>();

            bgWorker.DoWork += (sender, e) =>
            {
                // Turn on BusyIndicator
                if (Define.DisplayLoading)
                    IsBusy = true;

                if (refreshData)
                {

                }

                // Get total products with condition in predicate
                TotalProducts = _productRepository.GetIQueryable(predicate).Count();

                // Get data with range
                IList<base_Product> products = _productRepository.GetRange(currentIndex, NumberOfDisplayItems, "It.Id", predicate);
                foreach (base_Product product in products)
                {
                    bgWorker.ReportProgress(0, product);
                }
            };

            bgWorker.ProgressChanged += (sender, e) =>
            {
                // Create product model
                base_ProductModel productModel = new base_ProductModel((base_Product)e.UserState);

                // Load relation data
                LoadRelationProductData(productModel);

                // Add to collection
                SelectedVendor.ProductCollection.Add(productModel);
            };

            bgWorker.RunWorkerCompleted += (sender, e) =>
            {
                // Turn off BusyIndicator
                IsBusy = false;
            };

            // Run async background worker
            bgWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Load relation data for product
        /// </summary>
        /// <param name="productModel"></param>
        private void LoadRelationProductData(base_ProductModel productModel)
        {
            // Load Photo
            if (productModel.PhotoCollection == null)
            {
                string resource = productModel.Resource.ToString();
                productModel.PhotoCollection = new CollectionBase<base_ResourcePhotoModel>(
                    _photoRepository.GetAll(x => x.Resource.Equals(resource)).
                    Select(x => new base_ResourcePhotoModel(x)
                    {
                        ImagePath = Path.Combine(IMG_PRODUCT_DIRECTORY, productModel.Code, x.LargePhotoFilename),
                        IsDirty = false
                    }));

                // Set default photo
                productModel.PhotoDefault = productModel.PhotoCollection.FirstOrDefault();
            }

            // Get category name for product
            if (string.IsNullOrWhiteSpace(productModel.CategoryName))
            {
                base_DepartmentModel categoryItem = CategoryList.FirstOrDefault(x => x.Id.Equals(productModel.ProductCategoryId));
                if (categoryItem != null)
                    productModel.CategoryName = categoryItem.Name;
            }

            // Get uom name for product
            if (string.IsNullOrWhiteSpace(productModel.UOMName))
            {
                CheckBoxItemModel uomItem = UOMList.FirstOrDefault(x => x.Value.Equals(productModel.BaseUOMId));
                if (uomItem != null)
                    productModel.UOMName = uomItem.Text;
            }
        }

        #endregion

        #endregion

        #region Override Methods

        /// <summary>
        /// Load data when open form
        /// </summary>
        public override void LoadData()
        {
            if (SelectedVendor != null && !SelectedVendor.IsNew)
            {
                lock (UnitOfWork.Locker)
                {
                    // Refresh static data
                    SaleTaxCollection = null;
                    CategoryList = null;
                    UOMList = null;
                    GuestGroupCollection = null;

                    // Load static data
                    LoadStaticData();
                }

                SelectedVendor.AddressCollection = null;
                SelectedVendor.PhotoCollection = null;
                SelectedVendor.AdditionalModel = null;
                SelectedVendor.ProductCollection = null;
                SelectedVendor.PurchaseOrderCollection = null;
                SelectedVendor.ContactCollection = null;
                SelectedVendor.ToModelAndRaise();
                SelectedVendor.EndUpdate();
                IsDirtyContactCollection = false;
                LoadRelationVendorData(SelectedVendor);

                // Load data at selected tab
                OnSelectedTabIndexChanged();

                // Reload IsManualGenerate value from configuration
                OnPropertyChanged(() => IsManualGenerate);
            }

            // Load custom field collection
            LoadCustomFieldCollection();

            // Load data by predicate
            LoadVendorDataByPredicate(true);
        }

        /// <summary>
        /// Switch to search mode
        /// </summary>
        /// <param name="isList"></param>
        public override void ChangeSearchMode(bool isList, object param = null)
        {
            if (param == null)
            {
                if (ShowNotification(null))
                {
                    if (!isList)
                    {
                        NewVendor();
                        IsSearchMode = false;
                    }
                    else
                        IsSearchMode = true;
                }
            }
            else
            {
                Guid vendorGuid = new Guid();
                if (Guid.TryParse(param.ToString(), out vendorGuid))
                {
                    // Get vendor from product collection if product view is opened
                    base_GuestModel vendorModel = VendorCollection.SingleOrDefault(x => x.Resource.Equals(vendorGuid));

                    if (vendorModel == null)
                    {
                        // Get product from database if product is not loaded
                        vendorModel = new base_GuestModel(_guestRepository.Get(x => x.Resource.HasValue && x.Resource.Value.Equals(vendorGuid)));
                    }

                    if (vendorModel != null)
                    {
                        // Display detail grid
                        IsSearchMode = true;

                        // Load relation collection
                        OnDoubleClickViewCommandExecute(vendorModel);
                    }
                }
            }
        }

        /// <summary>
        /// Show notification when exit or change form
        /// </summary>
        /// <param name="isClosing"></param>
        /// <returns></returns>
        protected override bool OnViewChangingCommandCanExecute(bool isClosing)
        {
            return ShowNotification(isClosing);
        }

        #endregion

        #region Event Methods

        private void AdditionalModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base_GuestAdditionalModel additionalModel = sender as base_GuestAdditionalModel;
            switch (e.PropertyName)
            {
                case "SaleTaxLocation":
                    if (additionalModel.SaleTaxLocation != 0)
                    {
                        base_SaleTaxLocation taxCode = AllSaleTax.FirstOrDefault(x => x.ParentId == additionalModel.SaleTaxLocation &&
                            x.TaxCode.Equals(Define.CONFIGURATION.DefaultTaxCodeNewDepartment));
                        //This TaxLocation has only one TaxCode &  this sale Code is Single or Price
                        if (AllSaleTax.Count(x => x.ParentId == additionalModel.SaleTaxLocation) == 1 && taxCode != null &&
                            taxCode.TaxOption != (int)SalesTaxOption.Multi && taxCode.base_SaleTaxLocationOption.Any())
                            additionalModel.TaxRate = taxCode.base_SaleTaxLocationOption.FirstOrDefault().TaxRate;
                        else
                            additionalModel.TaxRate = 0;
                    }
                    else
                        additionalModel.TaxRate = 0;
                    break;
                case "IsTaxExemption":
                    additionalModel.SaleTaxLocation = 0;
                    break;
            }
        }

        #endregion

        #region Save Image

        /// <summary>
        /// Get product image folder
        /// </summary>
        private string IMG_PRODUCT_DIRECTORY = Path.Combine(Define.CONFIGURATION.DefautlImagePath, "Product");

        /// <summary>
        /// Get vendor image folder
        /// </summary>
        private string IMG_VENDOR_DIRECTORY = Path.Combine(Define.CONFIGURATION.DefautlImagePath, "Vendor");

        /// <summary>
        /// Copy image to server folder
        /// </summary>
        /// <param name="model"></param>
        private void SaveImage(base_ResourcePhotoModel model)
        {
            try
            {
                // Server image path
                string imgGuestDirectory = Path.Combine(IMG_VENDOR_DIRECTORY, SelectedVendor.GuestNo);

                // Create folder image on server if is not exist
                if (!Directory.Exists(imgGuestDirectory))
                    Directory.CreateDirectory(imgGuestDirectory);

                // Check client image to copy to server
                FileInfo clientFileInfo = new FileInfo(model.ImagePath);
                if (clientFileInfo.Exists)
                {
                    // Get file name image
                    string serverFileName = Path.Combine(imgGuestDirectory, model.LargePhotoFilename);
                    FileInfo serverFileInfo = new FileInfo(serverFileName);
                    if (!serverFileInfo.Exists)
                        clientFileInfo.CopyTo(serverFileName, true);
                    model.ImagePath = serverFileName;
                }
                else
                    model.ImagePath = string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Save Image" + ex.ToString());
            }
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
                    _noteRepository.GetAll(x => x.Resource.Equals(resource)).
                    Select(x => new base_ResourceNoteModel(x)));
            }
        }

        #endregion

        #region Permission

        #region Properties

        private bool _allowAddVendor = true;
        /// <summary>
        /// Gets or sets the AllowAddVendor.
        /// </summary>
        public bool AllowAddVendor
        {
            get { return _allowAddVendor; }
            set
            {
                if (_allowAddVendor != value)
                {
                    _allowAddVendor = value;
                    OnPropertyChanged(() => AllowAddVendor);
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
                    AllowAddVendor = IsMainStore;
                }
                else
                {
                    // Get all user rights
                    IEnumerable<string> userRightCodes = Define.USER_AUTHORIZATION.Select(x => x.Code);

                    // Get add/copy vendor permission
                    AllowAddVendor = userRightCodes.Contains("PO100-01-01") && IsMainStore;
                }
            }
        }

        #endregion
    }
}