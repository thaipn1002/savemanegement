using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Data;
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
    public class LiabilityViewModel : OrderViewModel
    {
        #region Define

        public RelayCommand<object> DoubleClickViewCommand { get; private set; }

        //Respository
        private base_StoreRepository _storeRepository = new base_StoreRepository();
        private base_SaleCommissionRepository _saleCommissionRepository = new base_SaleCommissionRepository();
        private base_GuestRewardRepository _guestRewardRepository = new base_GuestRewardRepository();
        private base_SaleOrderShipRepository _saleOrderShipRepository = new base_SaleOrderShipRepository();
        private base_SaleOrderShipDetailRepository _saleOrderShipDetailRepository = new base_SaleOrderShipDetailRepository();
        private base_ResourcePaymentRepository _paymentRepository = new base_ResourcePaymentRepository();
        private base_ResourceReturnRepository _resourceReturnRepository = new base_ResourceReturnRepository();
        private base_ResourceReturnDetailRepository _resourceReturnDetailRepository = new base_ResourceReturnDetailRepository();
        private base_ProductGroupRepository _productGroupRepository = new base_ProductGroupRepository();

        private base_ProductStoreRepository _productStoreRespository = new base_ProductStoreRepository();
        private base_ProductUOMRepository _productUOMRepository = new base_ProductUOMRepository();




        private enum SaleOrderTab
        {
            Order = 0,
            Ship = 1,
            Payment = 2,
            Return = 3
        }

        #endregion

        #region Constructors

        public LiabilityViewModel()
            : base()
        {
            _ownerViewModel = App.Current.MainWindow.DataContext;
            LoadDynamicData();
            //Get value from config
            IsIncludeReturnFee = Define.CONFIGURATION.IsIncludeReturnFee;
            // Get permission
            GetPermission();
        }

        public LiabilityViewModel(bool isList, object param)
            : this()
        {
            ChangeSearchMode(isList, param);
        }

        #endregion

        #region Properties

        #region IsForceFocused
        private bool _isForceFocused;
        /// <summary>
        /// Gets or sets the IsForceFocus.
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

        #region BreakAllChange
        private bool _breakAllChange = false;
        /// <summary>
        /// Gets or sets the BreakAllChange.
        /// </summary>
        public bool BreakAllChange
        {
            get { return _breakAllChange; }
            set
            {
                if (_breakAllChange != value)
                {
                    _breakAllChange = value;
                }
            }
        }
        #endregion

        #region BreakSODetailChange
        private bool _breakSODetailChange = false;
        /// <summary>
        /// Gets or sets the BreakSODetailChange.
        /// </summary>
        public bool BreakSODetailChange
        {
            get { return _breakSODetailChange; }
            set
            {
                if (_breakSODetailChange != value)
                {
                    _breakSODetailChange = value;
                }
            }
        }
        #endregion

        #region IsIncludeReturnFee
        private bool _isIncludeReturnFee;
        /// <summary>
        /// Gets or sets the IsIncludeReturnFee.
        /// </summary>
        public bool IsIncludeReturnFee
        {
            get { return _isIncludeReturnFee; }
            set
            {
                if (_isIncludeReturnFee != value)
                {
                    _isIncludeReturnFee = value;
                    OnPropertyChanged(() => IsIncludeReturnFee);
                }
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

        #region IsDirty
        /// <summary>
        /// Gets the IsDirty.
        /// </summary>
        public bool IsDirty
        {
            get
            {
                if (SelectedSaleOrder == null)
                    return false;
                return SelectedSaleOrder.IsDirty
                    || (SelectedSaleOrder.SaleOrderDetailCollection != null
                            && (SelectedSaleOrder.SaleOrderDetailCollection.Any(x => x.IsDirty)
                            || SelectedSaleOrder.SaleOrderDetailCollection.DeletedItems.Any()))
                    || (SelectedSaleOrder.SaleOrderShipCollection != null
                            && (SelectedSaleOrder.SaleOrderShipCollection.Any(x => x.IsDirty)
                            || SelectedSaleOrder.SaleOrderShipCollection.DeletedItems.Any()))
                    || (SelectedSaleOrder.ReturnModel != null && (SelectedSaleOrder.ReturnModel.IsDirty || SelectedSaleOrder.ReturnModel.ReturnDetailCollection.Any(x => x.IsDirty)))
                    || (SelectedSaleOrder.PaymentCollection != null && SelectedSaleOrder.PaymentCollection.Any(x => x.IsDirty))
                    || (SelectedSaleOrder.BillAddressModel != null && SelectedSaleOrder.BillAddressModel.IsDirty)
                    || (SelectedSaleOrder.ShipAddressModel != null && SelectedSaleOrder.ShipAddressModel.IsDirty);
            }

        }
        #endregion

        #region IsShipValid
        /// <summary>
        /// Gets the IsShipValid.
        /// Check Ship Has Error or is null set return true
        /// </summary>
        public bool IsShipValid
        {
            get
            {
                if (SelectedSaleOrder == null)
                    return false;
                if (SelectedSaleOrder.SaleOrderShipCollection == null || (SelectedSaleOrder.SaleOrderShipCollection != null && !SelectedSaleOrder.SaleOrderShipCollection.Any()))
                    return true;

                return (SelectedSaleOrder.SaleOrderShipCollection != null && !SelectedSaleOrder.SaleOrderShipCollection.Any(x => x.IsError));
            }

        }
        #endregion

        #region IsOrderValid
        /// <summary>
        /// Gets the IsShipValid.
        /// Check Ship Has Error or is null set return true
        /// </summary>
        public bool IsOrderValid
        {
            get
            {
                if (SelectedSaleOrder == null)
                    return false;
                if (SelectedSaleOrder.SaleOrderDetailCollection == null || (SelectedSaleOrder.SaleOrderDetailCollection != null && !SelectedSaleOrder.SaleOrderDetailCollection.Any()))
                    return true;

                return (SelectedSaleOrder.SaleOrderDetailCollection != null && !SelectedSaleOrder.SaleOrderDetailCollection.Any(x => x.IsError))
                    && !SelectedSaleOrder.SaleOrderDetailCollection.Any(x => !x.IsQuantityAccepted);
            }

        }
        #endregion

        #region IsReturnValid
        /// <summary>
        /// Gets the IsShipValid.
        /// Check Ship Has Error or is null set return true
        /// </summary>
        public bool IsReturnValid
        {
            get
            {
                if (SelectedSaleOrder == null)
                    return false;
                return (SelectedSaleOrder.ReturnModel != null && !SelectedSaleOrder.ReturnModel.ReturnDetailCollection.Any(x => x.HasError));
            }

        }
        #endregion

        #region IsValidTab
        private bool _isValidTab;
        /// <summary>
        /// Gets or sets the IsValidTab.
        /// </summary>
        public bool IsValidTab
        {
            get { return _isValidTab; }
            set
            {
                if (_isValidTab != value)
                {
                    _isValidTab = value;
                    OnPropertyChanged(() => IsValidTab);
                }
            }
        }
        #endregion

        #region StoreCollection
        private ObservableCollection<base_Store> _storeCollection;
        /// <summary>
        /// Gets or sets the StoreCollection.
        /// </summary>
        public ObservableCollection<base_Store> StoreCollection
        {
            get { return _storeCollection; }
            set
            {
                if (_storeCollection != value)
                {
                    _storeCollection = value;
                    OnPropertyChanged(() => StoreCollection);
                }
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

        //Sale Order

        #region SaleOrderCollection
        private CollectionBase<base_SaleOrderModel> _saleOrderCollection = new CollectionBase<base_SaleOrderModel>();
        /// <summary>
        /// Gets or sets the SaleOrderCollection.
        /// </summary>
        public CollectionBase<base_SaleOrderModel> SaleOrderCollection
        {
            get { return _saleOrderCollection; }
            set
            {
                if (_saleOrderCollection != value)
                {
                    _saleOrderCollection = value;
                    OnPropertyChanged(() => SaleOrderCollection);
                }
            }
        }
        #endregion

        #region TotalSaleOrder
        private int _totalSaleOrder;
        /// <summary>
        /// Gets or sets the TotalSaleOrder.
        /// </summary>
        public int TotalSaleOrder
        {
            get { return _totalSaleOrder; }
            set
            {
                if (_totalSaleOrder != value)
                {
                    _totalSaleOrder = value;
                    OnPropertyChanged(() => TotalSaleOrder);
                }
            }
        }
        #endregion

        #region SelectedSaleOrderShip
        private base_SaleOrderShipModel _selectedSaleOrderShip;
        /// <summary>
        /// Gets or sets the SelectedSaleOrderShip.
        /// </summary>
        public base_SaleOrderShipModel SelectedSaleOrderShip
        {
            get { return _selectedSaleOrderShip; }
            set
            {
                if (_selectedSaleOrderShip != value)
                {
                    _selectedSaleOrderShip = value;
                    OnPropertyChanged(() => SelectedSaleOrderShip);
                    if (SelectedSaleOrderShip != null)
                    {
                        SelectedSaleOrderShip.PropertyChanged -= new PropertyChangedEventHandler(SelectedSaleOrderShip_PropertyChanged);
                        SelectedSaleOrderShip.PropertyChanged += new PropertyChangedEventHandler(SelectedSaleOrderShip_PropertyChanged);
                    }
                }
            }
        }


        #endregion

        #region SaleOrderId
        private long _saleOrderId = 0;
        /// <summary>
        /// Gets or sets the QuotationId.
        /// </summary>
        public long SaleOrderId
        {
            get { return _saleOrderId; }
            set
            {
                if (_saleOrderId != value)
                {
                    _saleOrderId = value;
                    OnPropertyChanged(() => SaleOrderId);
                }
            }
        }
        /// <summary>
        /// Flag using for call from another from & set what tab user want
        /// </summary>
        private SaleOrderTab SaleOrderSelectedTab { get; set; }
        #endregion

        //Products
        #region SelectedTabIndex
        private int _previousTabIndex;
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
                    _previousTabIndex = _selectedTabIndex;
                    _selectedTabIndex = value;
                    TabChanged(value);
                    OnPropertyChanged(() => SelectedTabIndex);
                }
            }
        }
        #endregion

        //Return
        #region SaleOrderShipDetailFieldCollection
        private DataSearchCollection _saleOrderShipDetailFieldCollection;
        /// <summary>
        /// Gets or sets the SaleOrderShipDetailFieldCollection.
        /// </summary>
        public DataSearchCollection SaleOrderShipDetailFieldCollection
        {
            get { return _saleOrderShipDetailFieldCollection; }
            set
            {
                if (_saleOrderShipDetailFieldCollection != value)
                {
                    _saleOrderShipDetailFieldCollection = value;
                    OnPropertyChanged(() => SaleOrderShipDetailFieldCollection);
                }
            }
        }
        #endregion

        #region SelectedReturnDetail
        private object _selectedReturnDetail;
        /// <summary>
        /// Gets or sets the SelectedReturnDetail.
        /// </summary>
        public object SelectedReturnDetail
        {
            get { return _selectedReturnDetail; }
            set
            {
                if (_selectedReturnDetail != value)
                {
                    _selectedReturnDetail = value;
                    OnPropertyChanged(() => SelectedReturnDetail);
                    SelectedReturnDetailChanged();
                }
            }
        }


        #endregion

        #endregion

        #region Commands Methods

        #region PrintCommand
        /// <summary>
        /// Gets the Print Command.
        /// <summary>

        public RelayCommand<object> PrintCommand { get; private set; }



        /// <summary>
        /// Method to check whether the Print command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnPrintCommandCanExecute(object param)
        {
            if (SelectedSaleOrder == null)
                return false;
            return !SelectedSaleOrder.IsNew && SelectedSaleOrder.PaymentCollection != null && SelectedSaleOrder.PaymentCollection.Any(x => !x.IsNew && x.IsDeposit.HasValue && !x.IsDeposit.Value);
        }


        /// <summary>
        /// Method to invoke when the Print command is executed.
        /// </summary>
        private void OnPrintCommandExecute(object param)
        {
            
        }
        #endregion

        #region SearchCommand
        /// <summary>
        /// Method to check whether the SearchCommand command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        protected override bool OnSearchCommandCanExecute(object param)
        {
            return true;
        }

        protected override void OnSearchCommandExecute(object param)
        {
            SearchAlert = string.Empty;
            if ((param == null || string.IsNullOrWhiteSpace(param.ToString())) && SearchOption == 0)//Search All
            {
                Expression<Func<base_SaleOrder, bool>> predicate = CreatePredicateWithConditionSearch(Keyword);
                LoadDataByPredicate(predicate, false, 0);

            }
            else if (param != null)
            {
                Keyword = param.ToString();
                if (SearchOption == 0)
                {
                    //Thong bao Can co dk
                    SearchAlert = "Search Option is required";
                }
                else
                {
                    Expression<Func<base_SaleOrder, bool>> predicate = CreatePredicateWithConditionSearch(Keyword);

                    LoadDataByPredicate(predicate, false, 0);
                }
            }
        }

        #endregion

        #region DoubleClickCommand

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
            ComboItem cmbValue = new ComboItem();
            cmbValue.Text = "SaleOrderReturn.SelectedItem";
            cmbValue.Detail = SelectedSaleOrder.Id;
            (_ownerViewModel as MainViewModel).OpenViewExecute("SalesOrder", cmbValue);
        }
        #endregion

        #endregion "\Commands Methods"

        #region Private Methods

        /// <summary>
        /// 
        /// </summary>
        protected override void InitialCommand()
        {
            base.InitialCommand();
            PrintCommand = new RelayCommand<object>(OnPrintCommandExecute, OnPrintCommandCanExecute);
            DoubleClickViewCommand = new RelayCommand<object>(OnDoubleClickViewCommandExecute, OnDoubleClickViewCommandCanExecute);
        }

        /// <summary>
        /// Method check Item has edit & show message
        /// </summary>
        /// <returns></returns>
        public bool ChangeViewExecute(bool? isClosing)
        {
            bool result = true;
            if (this.IsDirty)
            {
                MessageBoxResultCustom msgResult = MessageBoxResultCustom.None;
                //Some data has changed. Do you want to save?
                msgResult = MsgControl.ShowWarning(Language.GetMsg("M106"), Language.GetMsg("POSCaption"), MessageBoxButtonCustom.YesNoCancel);
                if (msgResult.Is(MessageBoxResultCustom.Yes))
                {
                    if (OnSaveCommandCanExecute(null))
                        result = SaveSalesOrder();
                    else //Has Error
                        result = false;
                }
                else if (msgResult.Is(MessageBoxResultCustom.No))
                {
                    if (SelectedSaleOrder.IsNew)
                    {
                        _selectedSaleOrder = null;
                        if (isClosing.HasValue && !isClosing.Value)
                            IsSearchMode = true;
                    }
                    else //Old Item Rollback data
                    {
                        SelectedSaleOrder.ToModelAndRaise();
                        SetSaleOrderToModel(SelectedSaleOrder);
                        SetSaleOrderRelation(SelectedSaleOrder, true);
                    }
                }
                else
                {
                    result = false;
                }

            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void LoadStaticData()
        {
            base.LoadStaticData();

            SaleOrderShipDetailFieldCollection = new DataSearchCollection
            {
                new DataSearchModel { ID = 1, Level = 0, DisplayName = "Code", KeyName = "ItemCode" },
                new DataSearchModel { ID = 2, Level = 0, DisplayName = "Product Name", KeyName = "ItemName" },
                new DataSearchModel { ID = 3, Level = 0, DisplayName = "Attribute", KeyName = "ItemAtribute" },
                new DataSearchModel { ID = 4, Level = 0, DisplayName = "Size", KeyName = "ItemSize" },
            };

        }

        /// <summary>
        /// Load relate data with form from database
        /// </summary>
        protected override void LoadDynamicData()
        {
            //Load
            base.LoadDynamicData();

            //Load Extention
            //Get Store
            LoadStores();
        }

        /// <summary>
        /// Load Product From Database
        /// </summary>
        protected override void LoadProducts()
        {
            base.LoadProducts();
        }

        /// <summary>
        /// Load Store from db
        /// </summary>
        private void LoadStores()
        {
            IList<base_Store> stores = _storeRepository.GetAll();
            if (StoreCollection == null)
                StoreCollection = new ObservableCollection<base_Store>(stores.OrderBy(x => x.Id));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="saleOrderModel"></param>
        /// <param name="isForce"></param>
        protected override void SetSaleOrderRelation(base_SaleOrderModel saleOrderModel, bool isForce = false)
        {
            base.SetSaleOrderRelation(saleOrderModel, isForce);

            SetForSaleOrderShip(saleOrderModel, isForce);

            //Get SaleOrderShipDetail for return
            SetForShippedCollection(saleOrderModel, isForce);

            SetToSaleOrderReturn(saleOrderModel, isForce);

            LoadPaymentCollection(saleOrderModel);

            saleOrderModel.RaiseAnyShipped();
        }

        /// <summary>
        /// Load Sale Order Detail Collection with SaleOrderDetailCollection
        /// </summary>
        /// <param name="saleOrderModel"></param>
        /// <param name="isForce">Can set SaleOrderDetailCollection when difference null</param>
        protected override void SetSaleOrderDetail(base_SaleOrderModel saleOrderModel, bool isForce = false)
        {
            base.SetSaleOrderDetail(saleOrderModel, isForce);

            ShowShipTab(saleOrderModel);
        }

        /// <summary>
        /// Load Sale Order Shippeds Collection with SaleOrderShipDetailCollection
        /// </summary>
        /// <param name="saleOrderModel"></param>
        /// <param name="isForce">Can set SaleOrderShipDetailCollection when difference null</param>
        private void SetForShippedCollection(base_SaleOrderModel saleOrderModel, bool isForce = false)
        {
            if ((saleOrderModel.SaleOrderShipDetailCollection == null && saleOrderModel.SaleOrderShipCollection != null) || isForce)
            {
                saleOrderModel.SaleOrderShipDetailCollection = new CollectionBase<base_SaleOrderShipDetailModel>();
                foreach (base_SaleOrderShipModel saleOrderShipModel in saleOrderModel.SaleOrderShipCollection.Where(x => x.IsShipped == true))
                {
                    foreach (base_SaleOrderShipDetailModel saleOrderShipDetailModel in saleOrderShipModel.SaleOrderShipDetailCollection)
                    {
                        saleOrderModel.SaleOrderShipDetailCollection.Add(saleOrderShipDetailModel);
                    }
                }

                saleOrderModel.SaleOrderShippedCollection = new CollectionBase<base_SaleOrderDetailModel>();
                foreach (base_SaleOrderDetailModel saleOrderDetailModel in saleOrderModel.SaleOrderDetailCollection)
                {
                    //Item is shipped
                    if (saleOrderModel.SaleOrderShipDetailCollection.Any(x => x.SaleOrderDetailResource.Equals(saleOrderDetailModel.Resource.ToString())))
                    {
                        //Item is shipped => lock Uom this item
                        if (!saleOrderDetailModel.IsReadOnlyUOM)
                            saleOrderDetailModel.IsReadOnlyUOM = true;

                        base_SaleOrderDetailModel saleOrderShipModel = saleOrderDetailModel.Clone();
                        saleOrderShipModel.IsNew = false;
                        saleOrderShipModel.PickQty = saleOrderModel.SaleOrderShipDetailCollection.Where(x => x.SaleOrderDetailResource.Equals(saleOrderDetailModel.Resource.ToString())).Sum(x => x.PackedQty);
                        saleOrderShipModel.SubTotal = saleOrderShipModel.PickQty * saleOrderShipModel.SalePrice;
                        saleOrderModel.SaleOrderShippedCollection.Add(saleOrderShipModel);
                    }
                }
            }
        }

        /// <summary>
        /// Load Sale Order Return
        /// </summary>
        /// <param name="saleOrderModel"></param>
        /// <param name="isForce"></param>
        private void SetToSaleOrderReturn(base_SaleOrderModel saleOrderModel, bool isForce = false)
        {
            //Get Return Resource
            string saleOrderResource = saleOrderModel.Resource.ToString();
            base_ResourceReturn resourceReturn = _resourceReturnRepository.Get(x => x.DocumentResource.Equals(saleOrderResource));

            if (resourceReturn != null)
                saleOrderModel.ReturnModel = new base_ResourceReturnModel(resourceReturn);
            else
            {
                saleOrderModel.ReturnModel = new base_ResourceReturnModel();
                saleOrderModel.ReturnModel.DocumentNo = saleOrderModel.SONumber;
                saleOrderModel.ReturnModel.TotalAmount = saleOrderModel.Total;
                saleOrderModel.ReturnModel.DocumentResource = saleOrderModel.Resource.ToString();
                saleOrderModel.ReturnModel.Resource = Guid.NewGuid();
                saleOrderModel.ReturnModel.TotalRefund = 0;
                saleOrderModel.ReturnModel.Mark = "SO";
                saleOrderModel.ReturnModel.UserCreated = Define.USER != null ? Define.USER.LoginName : string.Empty;
                saleOrderModel.ReturnModel.DateCreated = DateTime.Today;
                saleOrderModel.ReturnModel.IsDirty = false;
            }
            if (isForce || saleOrderModel.ReturnModel.ReturnDetailCollection == null || !saleOrderModel.ReturnModel.ReturnDetailCollection.Any())
            {
                saleOrderModel.ReturnModel.ReturnDetailCollection = new CollectionBase<base_ResourceReturnDetailModel>();
                saleOrderModel.ReturnModel.ReturnDetailCollection.CollectionChanged += ReturnDetailCollection_CollectionChanged;
                foreach (base_ResourceReturnDetail resourceReturnDetail in saleOrderModel.ReturnModel.base_ResourceReturn.base_ResourceReturnDetail)
                {
                    base_ResourceReturnDetailModel returnDetailModel = new base_ResourceReturnDetailModel(resourceReturnDetail);
                    returnDetailModel.SaleOrderModel = saleOrderModel;
                    returnDetailModel.SaleOrderDetailModel = saleOrderModel.SaleOrderDetailCollection.SingleOrDefault(x => x.Resource.ToString().Equals(returnDetailModel.OrderDetailResource));
                    returnDetailModel.UnitName = returnDetailModel.SaleOrderDetailModel.UnitName;
                    CalcReturnDetailSubTotal(saleOrderModel, returnDetailModel);
                    saleOrderModel.ReturnModel.ReturnDetailCollection.Add(returnDetailModel);
                    returnDetailModel.IsDirty = false;
                    returnDetailModel.IsTemporary = false;
                }
            }
            saleOrderModel.ReturnModel.PropertyChanged += new PropertyChangedEventHandler(ReturnModel_PropertyChanged);
        }

        /// <summary>
        /// Load Sale Order Ship Collection
        /// </summary>
        /// <param name="saleOrderModel"></param>
        /// <param name="isForce"></param>
        private void SetForSaleOrderShip(base_SaleOrderModel saleOrderModel, bool isForce = false)
        {
            //Collection Sale Order Ship
            if (isForce || saleOrderModel.SaleOrderShipCollection == null || !saleOrderModel.SaleOrderShipCollection.Any())
            {
                saleOrderModel.SaleOrderShipCollection = new CollectionBase<base_SaleOrderShipModel>();

                foreach (base_SaleOrderShip saleOrderShip in saleOrderModel.base_SaleOrder.base_SaleOrderShip)
                {
                    base_SaleOrderShipModel saleOrderShipModel = new base_SaleOrderShipModel(saleOrderShip);
                    saleOrderShipModel.IsChecked = saleOrderShipModel.IsShipped;
                    saleOrderShipModel.IsDirty = false;
                    //SaleOrderShipDetail
                    saleOrderShipModel.SaleOrderShipDetailCollection = new CollectionBase<base_SaleOrderShipDetailModel>();
                    foreach (base_SaleOrderShipDetail saleOrderShipDetail in saleOrderShip.base_SaleOrderShipDetail)
                    {
                        base_SaleOrderShipDetailModel saleOrderShipDetailModel = new base_SaleOrderShipDetailModel(saleOrderShipDetail);
                        saleOrderShipDetailModel.SaleOrderDetailModel = saleOrderModel.SaleOrderDetailCollection.SingleOrDefault(x => x.Resource.ToString().Equals(saleOrderShipDetail.SaleOrderDetailResource));
                        saleOrderShipDetailModel.IsDirty = false;
                        saleOrderShipModel.SaleOrderShipDetailCollection.Add(saleOrderShipDetailModel);
                    }
                    saleOrderModel.SaleOrderShipCollection.Add(saleOrderShipModel);
                }

                saleOrderModel.PaymentProcess = saleOrderModel.SaleOrderShipCollection.Any();
            }
        }

        /// <summary>
        /// Set CustomerRewardCollection for RewardMember
        /// <remarks>Require validation memebership</remarks>
        /// </summary>
        /// <param name="saleOrderModel"></param>
        private void SetCustomerRewardCollection(base_SaleOrderModel saleOrderModel, bool isForce = false)
        {
            //Get GuestReward collection
            if (isForce || saleOrderModel.GuestModel.GuestRewardCollection == null || !saleOrderModel.GuestModel.GuestRewardCollection.Any())
            {
                saleOrderModel.GuestModel.GuestRewardCollection = new CollectionBase<base_GuestRewardModel>();
                if (saleOrderModel.GuestModel.IsRewardMember)
                {
                    if (Define.CONFIGURATION.IsSumCashReward)
                    {
                        short rewardTypePercent = (short)RewardType.Pecent;
                        short rewardTypePoint = (short)RewardType.Point;
                        //Get Reward Available with type is percent or Point

                        var rewards = saleOrderModel.GuestModel.base_Guest.base_GuestReward.Where(x => x.GuestId.Equals(saleOrderModel.GuestModel.Id) && x.Sign.Equals("+") && !x.IsApply && x.ActivedDate.Value <= DateTime.Today && (!x.ExpireDate.HasValue || x.ExpireDate.HasValue && DateTime.Today <= x.ExpireDate.Value) && (x.RewardSetupUnit.Equals(rewardTypePoint) || x.RewardSetupUnit.Equals(rewardTypePercent)));
                        foreach (base_GuestReward guestReward in rewards)
                        {
                            saleOrderModel.GuestModel.GuestRewardCollection.Add(new base_GuestRewardModel(guestReward));
                        }
                        short cashRewardType = (short)RewardType.Money;
                        var cashRewards = saleOrderModel.GuestModel.base_Guest.base_GuestReward.Where(x => x.GuestId.Equals(saleOrderModel.GuestModel.Id) && x.Sign.Equals("+") && !x.IsApply && x.ActivedDate.Value <= DateTime.Today && (!x.ExpireDate.HasValue || x.ExpireDate.HasValue && DateTime.Today <= x.ExpireDate.Value) && x.RewardSetupUnit.Equals(cashRewardType));
                        var cashRedeemRewards = saleOrderModel.GuestModel.base_Guest.base_GuestReward.Where(x => x.GuestId.Equals(saleOrderModel.GuestModel.Id) && x.Sign.Equals("-") && x.RewardSetupUnit.Equals(cashRewardType));

                        if (cashRewards.Any())
                        {
                            base_GuestReward cashReward = cashRewards.FirstOrDefault();
                            base_GuestRewardModel cashRewardModel = new base_GuestRewardModel(cashReward);
                            cashRewardModel.IsNew = false;
                            //this is item sum of cash reward
                            cashRewardModel.IsTemporary = true;
                            cashRewardModel.RewardSetupAmount = cashRewards.Sum(x => x.RewardSetupAmount) - cashRedeemRewards.Sum(x => x.RewardSetupAmount);
                            //saleOrderModel.GuestModel.MembershipValidated.TotalCashReward; 
                            //[not using this code cause with item is cash & config is allow sum cash then reward after redeem not set apply]cashRewards.Sum(x => x.RewardSetupAmount);
                            cashRewardModel.RewardSetupUnit = cashRewardType;
                            saleOrderModel.GuestModel.GuestRewardCollection.Add(cashRewardModel);
                        }
                    }
                    else
                    {
                        foreach (base_GuestReward guestReward in saleOrderModel.GuestModel.base_Guest.base_GuestReward.Where(x => x.GuestId.Equals(saleOrderModel.GuestModel.Id) && !x.IsApply && x.ActivedDate.Value <= DateTime.Today && x.Sign.Equals("+") && (!x.ExpireDate.HasValue || x.ExpireDate.HasValue && DateTime.Today <= x.ExpireDate.Value)))
                        {
                            saleOrderModel.GuestModel.GuestRewardCollection.Add(new base_GuestRewardModel(guestReward));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Load payment collection 
        /// </summary>
        /// <param name="saleOrderModel"></param>
        private void LoadPaymentCollection(base_SaleOrderModel saleOrderModel)
        {
            // Get document resource
            string docResource = saleOrderModel.Resource.ToString();

            // Get all payment by document resource
            IEnumerable<base_ResourcePayment> payments = _paymentRepository.GetAll(x => x.DocumentResource.Equals(docResource));

            // Load payment collection
            saleOrderModel.PaymentCollection = new ObservableCollection<base_ResourcePaymentModel>(payments.Select(x => new base_ResourcePaymentModel(x)));

            // Check show PaymentTab
            saleOrderModel.PaymentProcess = saleOrderModel.PaymentCollection.Any();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="refreshData"></param>
        /// <param name="currentIndex"></param>
        private void LoadDataByPredicate(Expression<Func<base_SaleOrder, bool>> predicate, bool refreshData = false, int currentIndex = 0)
        {
            if (IsBusy)//Break multi call to server
            {
                Console.WriteLine("IsBusy");
                return;
            }

            BackgroundWorker bgWorker = new BackgroundWorker { WorkerReportsProgress = true };
            if (currentIndex == 0)
                SaleOrderCollection.Clear();
            bgWorker.DoWork += (sender, e) =>
            {
                if (Define.DisplayLoading)
                    IsBusy = true;
                predicate = predicate.And(x => !x.IsPurge && !x.IsLocked);

                short orderStatus = (short)SaleOrderStatus.Quote;
                short layawayStatus = (short)SaleOrderStatus.Layaway;
                //Show item is created by SaleOrder or isConverted from Quote,Layaway,WorkOrder
                predicate = predicate.And(x => x.OrderStatus != orderStatus && x.OrderStatus != layawayStatus && x.IsConverted);

                //Cout all SaleOrder in Data base show on grid
                lock (UnitOfWork.Locker)
                {
                    TotalSaleOrder = _saleOrderRepository.GetIQueryable(predicate).Count();

                    //Get data with range
                    IList<base_SaleOrder> saleOrders = _saleOrderRepository.GetRange<DateTime>(currentIndex, TotalSaleOrder, x => x.OrderDate.Value, predicate);

                    if (refreshData)
                        _saleOrderRepository.Refresh(saleOrders);

                    foreach (base_SaleOrder saleOrder in saleOrders)
                    {
                        bgWorker.ReportProgress(0, saleOrder);
                    }
                }
            };

            bgWorker.ProgressChanged += (sender, e) =>
            {
                base_SaleOrderModel saleOrderModel = new base_SaleOrderModel((base_SaleOrder)e.UserState);
                SetSaleOrderToModel(saleOrderModel);
                SaleOrderCollection.Add(saleOrderModel);
            };

            bgWorker.RunWorkerCompleted += (sender, e) =>
            {
                if (SaleOrderId > 0)
                {
                    SetSelectedSaleOrderFromAnother();
                }
                else
                {
                    if (_viewExisted && !IsSearchMode && SelectedSaleOrder != null && SaleOrderCollection.Any() && !SelectedSaleOrder.IsNew) //Item is selected
                    {
                        SetSelectedSaleOrderFromDbOrCollection();
                    }
                }
                this.Total = SaleOrderCollection.Sum(x => x.Total);
                this.Paid = SaleOrderCollection.Sum(x => x.Paid);
                this.Balance = SaleOrderCollection.Sum(x => x.Balance);
                IsBusy = false;
            };
            bgWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Create predicate
        /// </summary>
        /// <returns></returns>
        private Expression<Func<base_SaleOrder, bool>> CreatePredicateWithConditionSearch(string keyword)
        {
            Expression<Func<base_SaleOrder, bool>> predicate = PredicateBuilder.True<base_SaleOrder>();
            predicate = predicate.And(x => ((x.RefundedAmount != null && x.Balance > 0 && x.Paid + x.Balance - (x.RefundedAmount) != x.Total)) || (x.Balance > 0 && x.RefundedAmount == 0));
            if (!string.IsNullOrWhiteSpace(keyword) && SearchOption > 0)
            {
                if (SearchOption.Has(SearchOptions.SoNum))
                    predicate = predicate.And(x => x.SONumber.ToLower().Contains(keyword.ToLower()));
                if (SearchOption.Has(SearchOptions.Customer))
                {
                    var customerList = CustomerCollection.Where(y => y.LastName.ToLower().Contains(keyword.ToLower()) || y.FirstName.ToLower().Contains(keyword.ToLower())).Select(x => x.Resource.ToString());
                    predicate = predicate.And(x => customerList.Contains(x.CustomerResource));
                }
            }
            return predicate;
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        protected override base_SaleOrderModel CreateNewSaleOrder()
        {
            _selectedSaleOrder = new base_SaleOrderModel();
            _selectedSaleOrder.Shift = Define.ShiftCode;
            _selectedSaleOrder.IsTaxExemption = false;
            _selectedSaleOrder.IsConverted = true;
            _selectedSaleOrder.IsLocked = false;
            _selectedSaleOrder.SONumber = DateTime.Now.ToString(Define.GuestNoFormat);
            _saleOrderRepository.SOCardGenerate(_selectedSaleOrder, _selectedSaleOrder.SONumber);
            _selectedSaleOrder.DateCreated = DateTime.Now;
            _selectedSaleOrder.DueDate = DateTime.Now;
            _selectedSaleOrder.BookingChanel = Convert.ToInt16(Common.BookingChannel.First().ObjValue);
            _selectedSaleOrder.StoreCode = Define.StoreCode;//Default StoreCode
            _selectedSaleOrder.OrderDate = DateTime.Now;
            _selectedSaleOrder.RequestShipDate = DateTime.Now;
            _selectedSaleOrder.UserCreated = Define.USER != null ? Define.USER.LoginName : string.Empty;
            _selectedSaleOrder.TaxPercent = 0;
            _selectedSaleOrder.TaxAmount = 0;
            _selectedSaleOrder.Deposit = 0;
            _selectedSaleOrder.OrderStatus = (short)SaleOrderStatus.Open;
            _selectedSaleOrder.ItemStatus = Common.StatusSalesOrders.SingleOrDefault(x => Convert.ToInt16(x.ObjValue).Equals(_selectedSaleOrder.OrderStatus));
            _selectedSaleOrder.Mark = MarkType.SaleOrder.ToDescription();
            _selectedSaleOrder.TermNetDue = 0;
            _selectedSaleOrder.TermDiscountPercent = 0;
            _selectedSaleOrder.TermPaidWithinDay = 0;
            _selectedSaleOrder.PaymentTermDescription = string.Empty;
            //Set Price Schema
            _selectedSaleOrder.PriceSchemaId = 1;
            _selectedSaleOrder.PriceLevelItem = Common.PriceSchemas.SingleOrDefault(x => Convert.ToInt16(x.ObjValue).Equals(_selectedSaleOrder.PriceSchemaId));

            _selectedSaleOrder.TaxExemption = string.Empty;
            _selectedSaleOrder.SaleRep = EmployeeCollection.FirstOrDefault().GuestNo;
            _selectedSaleOrder.Resource = Guid.NewGuid();
            _selectedSaleOrder.WeightUnit = Common.ShipUnits.First().Value;
            _selectedSaleOrder.IsDeposit = Define.CONFIGURATION.AcceptedPaymentMethod.HasValue ? Define.CONFIGURATION.AcceptedPaymentMethod.Value.Has(16) : false;//Accept Payment with deposit
            _selectedSaleOrder.WeightUnit = Define.CONFIGURATION.DefaultShipUnit.HasValue ? Define.CONFIGURATION.DefaultShipUnit.Value : Convert.ToInt16(Common.ShipUnits.First().ObjValue);
            _selectedSaleOrder.IsHiddenErrorColumn = true;

            _selectedSaleOrder.TaxLocation = Convert.ToInt32(Define.CONFIGURATION.DefaultSaleTaxLocation);
            _selectedSaleOrder.TaxCode = Define.CONFIGURATION.DefaultTaxCodeNewDepartment;
            //Get TaxLocation
            _selectedSaleOrder.TaxLocationModel = SaleTaxLocationCollection.SingleOrDefault(x => x.Id == _selectedSaleOrder.TaxLocation);

            //Create a sale order detail collection
            _selectedSaleOrder.SaleOrderDetailCollection = new CollectionBase<base_SaleOrderDetailModel>();
            _selectedSaleOrder.SaleOrderDetailCollection.CollectionChanged += new NotifyCollectionChangedEventHandler(SaleOrderDetailCollection_CollectionChanged);

            //create a sale order Ship Collection
            _selectedSaleOrder.SaleOrderShipCollection = new CollectionBase<base_SaleOrderShipModel>();
            _selectedSaleOrder.SaleOrderShippedCollection = new CollectionBase<base_SaleOrderDetailModel>();

            // Create new payment collection
            _selectedSaleOrder.PaymentCollection = new ObservableCollection<base_ResourcePaymentModel>();

            //ReturnModel & ReturnDetailCollection
            _selectedSaleOrder.ReturnModel = new base_ResourceReturnModel();
            _selectedSaleOrder.ReturnModel.DocumentNo = SelectedSaleOrder.SONumber;
            _selectedSaleOrder.ReturnModel.DocumentResource = SelectedSaleOrder.Resource.ToString();
            _selectedSaleOrder.ReturnModel.TotalAmount = SelectedSaleOrder.Total;
            _selectedSaleOrder.ReturnModel.Resource = Guid.NewGuid();
            _selectedSaleOrder.ReturnModel.TotalRefund = 0;
            _selectedSaleOrder.ReturnModel.TotalAmount = 0;
            _selectedSaleOrder.ReturnModel.Mark = "SO";
            _selectedSaleOrder.ReturnModel.UserCreated = Define.USER != null ? Define.USER.LoginName : string.Empty;
            _selectedSaleOrder.ReturnModel.DateCreated = DateTime.Today;
            _selectedSaleOrder.ReturnModel.IsDirty = false;
            _selectedSaleOrder.ReturnModel.ReturnDetailCollection = new CollectionBase<base_ResourceReturnDetailModel>();
            _selectedSaleOrder.ReturnModel.ReturnDetailCollection.CollectionChanged += ReturnDetailCollection_CollectionChanged;
            _selectedSaleOrder.SaleOrderShipDetailCollection = new CollectionBase<base_SaleOrderShipDetailModel>();
            //Additional
            _selectedSaleOrder.BillAddressModel = new base_GuestAddressModel() { AddressTypeId = (int)AddressType.Billing, IsDirty = false };
            _selectedSaleOrder.ShipAddressModel = new base_GuestAddressModel() { AddressTypeId = (int)AddressType.Shipping, IsDirty = false };

            _selectedCustomer = null;
            OnPropertyChanged(() => SelectedCustomer);

            //Set to fist tab & skip TabChanged Methods in SelectedTabIndex property
            _selectedTabIndex = 0;
            OnPropertyChanged(() => SelectedTabIndex);
            SetAllowChangeOrder(_selectedSaleOrder);
            _selectedSaleOrder.IsDirty = false;
            _selectedSaleOrder.PropertyChanged += new PropertyChangedEventHandler(SelectedSaleOrder_PropertyChanged);
            _selectedSaleOrder.ReturnModel.PropertyChanged += new PropertyChangedEventHandler(ReturnModel_PropertyChanged);
            OnPropertyChanged(() => SelectedSaleOrder);
            OnPropertyChanged(() => AllowSOShipping);
            OnPropertyChanged(() => AllowSOReturn);
            return _selectedSaleOrder;
        }

        /// <summary>
        /// Get SelectedSaleOrder From collection when Convert from quotation
        /// </summary>
        private void SetSelectedSaleOrderFromAnother()
        {
            if (SaleOrderId > 0)
            {

                App.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    SetSelectedSaleOrderFromDbOrCollection();

                    //Calc Onhand
                    foreach (base_SaleOrderDetailModel saleOrderDetailModel in SelectedSaleOrder.SaleOrderDetailCollection)
                    {
                        if (!saleOrderDetailModel.IsQuantityAccepted)
                        {
                            saleOrderDetailModel.IsQuantityAccepted = true;
                            _saleOrderRepository.CalcOnHandStore(SelectedSaleOrder, saleOrderDetailModel);
                        }
                    }

                    //Set for selectedCustomer

                    _selectedCustomer = CustomerCollection.SingleOrDefault(x => x.Resource.ToString().Equals(SelectedSaleOrder.CustomerResource));
                    OnPropertyChanged(() => SelectedCustomer);

                    SetAllowChangeOrder(SelectedSaleOrder);
                    ShowShipTab(SelectedSaleOrder);
                    SelectedSaleOrder.IsDirty = false;

                    //Changed tab
                    _selectedTabIndex = (int)SaleOrderSelectedTab;

                    OnPropertyChanged(() => SelectedTabIndex);
                    _saleOrderId = 0;
                    IsSearchMode = false;
                    IsForceFocused = true;
                }), System.Windows.Threading.DispatcherPriority.Background);


            }
        }

        /// <summary>
        /// Load Selected SaleOrder when item is selected with get from db or collection
        /// </summary>
        private void SetSelectedSaleOrderFromDbOrCollection()
        {
            if (SaleOrderCollection.Any(x => x.Id.Equals(SaleOrderId)))
            {
                SelectedSaleOrder = SaleOrderCollection.SingleOrDefault(x => x.Id.Equals(SaleOrderId));
            }
            else
            {
                lock (UnitOfWork.Locker)
                {
                    //If Current SaleOrder loading not yet
                    base_SaleOrder saleOrder = _saleOrderRepository.Get(x => x.Id.Equals(SaleOrderId));
                    if (saleOrder != null)
                    {
                        SelectedSaleOrder = new base_SaleOrderModel(saleOrder);
                        SetSaleOrderToModel(SelectedSaleOrder);
                    }
                }
            }
            if (SelectedSaleOrder != null)
                SetSaleOrderRelation(SelectedSaleOrder, true);
        }

        /// <summary>
        /// ShowShip Tab
        /// </summary>
        private void SetShippingStatus(base_SaleOrderModel saleOrderModel)
        {
            if (saleOrderModel.OrderStatus < (short)SaleOrderStatus.Shipping && saleOrderModel.SaleOrderShipCollection != null && saleOrderModel.SaleOrderShipCollection.Any())
                saleOrderModel.OrderStatus = (short)SaleOrderStatus.Shipping;
        }

        /// <summary>
        /// Set for SaleOrderStatus when order is Ship full
        /// </summary>
        private void SetShipStatus()
        {
            bool ShipAll = true;
            foreach (var item in SelectedSaleOrder.SaleOrderDetailCollection)
            {
                if (item.ProductModel == null || item.ProductModel.ItemTypeId.Equals((short)ItemTypes.Group))
                    continue;

                decimal shipTotal = SelectedSaleOrder.SaleOrderShipCollection.Where(x => x.IsShipped == true).Sum(x => x.SaleOrderShipDetailCollection.Where(y => y.SaleOrderDetailResource == item.Resource.ToString() && y.ProductResource == item.ProductResource).Sum(z => z.PackedQty));
                ShipAll &= item.Qty > 0 && shipTotal == item.Qty;
            }

            if (!SelectedSaleOrder.Mark.Equals(MarkType.SaleOrder.ToDescription()))//Set Close for layaway
            {
                if (ShipAll)
                {
                    SelectedSaleOrder.OrderStatus = (short)SaleOrderStatus.Close;
                }
            }
            else
            {
                if (SelectedSaleOrder.OrderStatus.Equals((short)SaleOrderStatus.PaidInFull))//Not change status when PaidInFull
                    return;

                if (ShipAll)
                    SelectedSaleOrder.OrderStatus = (short)SaleOrderStatus.FullyShipped;
                else if (SelectedSaleOrder.SaleOrderShipCollection.Any())
                    SelectedSaleOrder.OrderStatus = (short)SaleOrderStatus.Shipping;
            }
        }

        /// <summary>
        /// Check to Show ship tab when has saleorder detail
        /// </summary>
        /// <param name="saleOrderModel"></param>
        private void ShowShipTab(base_SaleOrderModel saleOrderModel)
        {
            if (saleOrderModel != null)
            {
                if (saleOrderModel.Mark.Equals(MarkType.Layaway.ToDescription()))
                    saleOrderModel.ShipProcess = saleOrderModel.Balance == 0;
                else
                    saleOrderModel.ShipProcess = (saleOrderModel.SaleOrderDetailCollection != null ? saleOrderModel.SaleOrderDetailCollection.Any() : false) && !saleOrderModel.IsNew;

                OnPropertyChanged(() => AllowSOShipping);
                OnPropertyChanged(() => AllowSOReturn);
            }

        }

        /// <summary>
        /// set user change order follow config & order status
        /// </summary>
        /// <param name="saleOrderModel"></param>
        private void SetAllowChangeOrder(base_SaleOrderModel saleOrderModel)
        {
            if (BreakAllChange)
                return;
            if (saleOrderModel.IsLocked)
                this.IsAllowChangeOrder = false;
            else if (saleOrderModel.OrderStatus.Equals((short)SaleOrderStatus.Quote))
                this.IsAllowChangeOrder = true;
            else if (saleOrderModel.PaymentCollection != null && saleOrderModel.PaymentCollection.Any(x => !x.IsDeposit.Value))/*has paid*/
                this.IsAllowChangeOrder = false;
            else if (saleOrderModel.OrderStatus < (short)SaleOrderStatus.FullyShipped)//Open or Shipping
                this.IsAllowChangeOrder = true;
            else if (saleOrderModel.OrderStatus.Equals((short)SaleOrderStatus.PaidInFull))
                this.IsAllowChangeOrder = false;
            else
                this.IsAllowChangeOrder = saleOrderModel.OrderStatus == (short)SaleOrderStatus.FullyShipped && Define.CONFIGURATION.IsAllowChangeOrder.Value;

        }

        #region SelectedItem



        /// <summary>
        /// Selected Return Detail Changed
        /// when item is selected,is check collection reference  exited with item choice (compare saleOrderResource)?
        /// unless get item from DeletedItems(used for store item) add to collection shipped(collection autocompelete choice item)
        /// </summary>
        private void SelectedReturnDetailChanged()
        {
            base_ResourceReturnDetailModel selectedReturnDetail = SelectedReturnDetail as base_ResourceReturnDetailModel;
            if (selectedReturnDetail == null || selectedReturnDetail.SaleOrderDetailModel == null)
                return;

            CheckReturned(selectedReturnDetail);
        }


        #endregion


        //CRUD region
        /// <summary>
        /// Insert New sale order
        /// </summary>
        private void InsertSaleOrder()
        {
            if (SelectedSaleOrder.IsNew)
            {
                SelectedSaleOrder.Shift = Define.ShiftCode;
                UpdateCustomerAddress(SelectedSaleOrder.BillAddressModel);
                SelectedSaleOrder.BillAddressId = SelectedSaleOrder.BillAddressModel.Id;
                UpdateCustomerAddress(SelectedSaleOrder.ShipAddressModel);
                SelectedSaleOrder.ShipAddressId = SelectedSaleOrder.ShipAddressModel.Id;
                //Sale Order Detail Model
                foreach (base_SaleOrderDetailModel saleOrderDetailModel in SelectedSaleOrder.SaleOrderDetailCollection)
                {
                    _saleOrderRepository.UpdateCustomerQuantity(saleOrderDetailModel, SelectedSaleOrder.StoreCode, saleOrderDetailModel.Quantity);

                    saleOrderDetailModel.ToEntity();
                    SelectedSaleOrder.base_SaleOrder.base_SaleOrderDetail.Add(saleOrderDetailModel.base_SaleOrderDetail);
                }
                _productRepository.Commit();

                SavePaymentCollection(SelectedSaleOrder);

                SelectedSaleOrder.ToEntity();
                _saleOrderRepository.Add(SelectedSaleOrder.base_SaleOrder);

                _saleOrderRepository.Commit();
                SelectedSaleOrder.EndUpdate();
                //Set ID
                SelectedSaleOrder.ToModel();
                SelectedSaleOrder.EndUpdate();
                foreach (base_SaleOrderDetailModel saleOrderDetailModel in SelectedSaleOrder.SaleOrderDetailCollection)
                {
                    saleOrderDetailModel.ToModel();
                    saleOrderDetailModel.EndUpdate();
                }

                if (SelectedSaleOrder.PaymentCollection != null)
                {
                    foreach (base_ResourcePaymentModel paymentModel in SelectedSaleOrder.PaymentCollection.Where(x => x.IsNew))
                    {
                        paymentModel.ToModel();
                        //Update or Add New PaymentDetail
                        if (paymentModel.PaymentDetailCollection != null)
                        {
                            foreach (base_ResourcePaymentDetailModel paymentDetailModel in paymentModel.PaymentDetailCollection.Where(x => x.IsNew))
                            {
                                paymentDetailModel.ToModel();
                                paymentDetailModel.EndUpdate();
                            }
                        }
                        paymentModel.EndUpdate();
                    }
                }
                SaleOrderCollection.Insert(0, SelectedSaleOrder);
                TotalSaleOrder++;
                ShowShipTab(SelectedSaleOrder);
            }
        }

        /// <summary>
        /// Insert New sale order
        /// </summary>
        private void UpdateSaleOrder()
        {
            //Usefull for situation : Order 5 unit after ship 2 unit , change order qty to 2 unit 
            //=> that order is full shipped but not set that current quantity because conflit with condition "Allow Change Order" when full shipping, may be make order locked
            SetShipStatus();
            SelectedSaleOrder.Shift = Define.ShiftCode;
            //Insert or update address for customer
            UpdateCustomerAddress(SelectedSaleOrder.BillAddressModel);
            UpdateCustomerAddress(SelectedSaleOrder.ShipAddressModel);
            //set dateUpdate
            SelectedSaleOrder.DateUpdated = DateTime.Now;
            #region SaleOrderDetail
            //Delete SaleOrderDetail
            if (SelectedSaleOrder.SaleOrderDetailCollection.DeletedItems.Any())
            {
                foreach (base_SaleOrderDetailModel saleOrderDetailModel in SelectedSaleOrder.SaleOrderDetailCollection.DeletedItems)
                {
                    //Get quantity from entity to substract store(avoid quantity in model is changed)
                    _saleOrderRepository.UpdateCustomerQuantity(saleOrderDetailModel, SelectedSaleOrder.base_SaleOrder.StoreCode, saleOrderDetailModel.base_SaleOrderDetail.Quantity, false/*descrease quantity*/);
                    _saleOrderDetailRepository.Delete(saleOrderDetailModel.base_SaleOrderDetail);
                }
                _saleOrderDetailRepository.Commit();
                SelectedSaleOrder.SaleOrderDetailCollection.DeletedItems.Clear();
            }

            if (SelectedSaleOrder.IsPurge)
            {
                foreach (base_SaleOrderDetailModel saleOrderDetailModel in SelectedSaleOrder.SaleOrderDetailCollection)
                {
                    _saleOrderRepository.UpdateCustomerQuantity(saleOrderDetailModel, SelectedSaleOrder.base_SaleOrder.StoreCode, saleOrderDetailModel.base_SaleOrderDetail.Quantity, false/*descrease quantity*/);
                }
            }
            else
            {
                //Sale Order Detail Model
                foreach (base_SaleOrderDetailModel saleOrderDetailModel in SelectedSaleOrder.SaleOrderDetailCollection.Where(x => x.IsDirty))
                {
                    //Need to check difference store code (user change to another store)
                    if (SelectedSaleOrder.StoreCode.Equals(SelectedSaleOrder.base_SaleOrder.StoreCode))
                    {
                        if (saleOrderDetailModel.Quantity != saleOrderDetailModel.base_SaleOrderDetail.Quantity || saleOrderDetailModel.UOMId != saleOrderDetailModel.base_SaleOrderDetail.UOMId) //addition quantity
                        {
                            _saleOrderRepository.UpdateCustomerQuantityChanged(saleOrderDetailModel, SelectedSaleOrder.StoreCode);
                        }
                    }
                    else
                    {
                        //Subtract quantity from "old store"(user change to another store)
                        _saleOrderRepository.UpdateCustomerQuantity(saleOrderDetailModel, SelectedSaleOrder.base_SaleOrder.StoreCode, saleOrderDetailModel.base_SaleOrderDetail.Quantity, false/*descrease quantity*/);
                        //Add quantity to new store
                        _saleOrderRepository.UpdateCustomerQuantity(saleOrderDetailModel, SelectedSaleOrder.StoreCode, saleOrderDetailModel.Quantity);
                    }

                    saleOrderDetailModel.ToEntity();
                    if (saleOrderDetailModel.IsNew)
                        SelectedSaleOrder.base_SaleOrder.base_SaleOrderDetail.Add(saleOrderDetailModel.base_SaleOrderDetail);
                    saleOrderDetailModel.EndUpdate();
                }
            }
            _productRepository.Commit();
            #endregion

            #region SaleOrderShip
            if (SelectedSaleOrder.SaleOrderShipCollection.DeletedItems.Any())
            {
                //Delete Sale Order Ship Model
                foreach (base_SaleOrderShipModel saleOrderShipModel in SelectedSaleOrder.SaleOrderShipCollection.DeletedItems)
                    _saleOrderShipRepository.Delete(saleOrderShipModel.base_SaleOrderShip);
                _saleOrderShipRepository.Commit();
                SelectedSaleOrder.SaleOrderShipCollection.DeletedItems.Clear();
            }

            //Sale Order Ship Model
            foreach (base_SaleOrderShipModel saleOrderShipModel in SelectedSaleOrder.SaleOrderShipCollection.Where(x => x.IsDirty || x.IsNew))
            {
                saleOrderShipModel.IsShipped = saleOrderShipModel.IsChecked;

                // Delete SaleOrderShipDetail
                if (saleOrderShipModel.SaleOrderShipDetailCollection != null && saleOrderShipModel.SaleOrderShipDetailCollection.DeletedItems.Any())
                {
                    foreach (base_SaleOrderShipDetailModel saleOrderShipDetailModelDel in saleOrderShipModel.SaleOrderShipDetailCollection.DeletedItems)
                        _saleOrderShipDetailRepository.Delete(saleOrderShipDetailModelDel.base_SaleOrderShipDetail);
                    _saleOrderShipDetailRepository.Commit();
                    saleOrderShipModel.SaleOrderShipDetailCollection.DeletedItems.Clear();
                }

                //Update SaleOrderShipDetail & Upd
                if (saleOrderShipModel.SaleOrderShipDetailCollection != null && saleOrderShipModel.SaleOrderShipDetailCollection.Any())
                {
                    foreach (base_SaleOrderShipDetailModel saleOrderShipDetailModel in saleOrderShipModel.SaleOrderShipDetailCollection)
                    {
                        //Package is shipped & is a new shipped =>update Onhand,CustomerQty
                        if (saleOrderShipModel.IsShipped && !saleOrderShipModel.base_SaleOrderShip.IsShipped && !saleOrderShipDetailModel.SaleOrderDetailModel.ProductModel.IsCoupon)
                        {
                            //Descrease OnHand product Store which product in SaleOrderShipDetail
                            //Descrease store with Product On Hand in group with parent product
                            //Cause : Item Product Group not stockable 
                            if (saleOrderShipDetailModel.SaleOrderDetailModel.ProductModel.ItemTypeId.Equals((short)ItemTypes.Group))
                            {
                                foreach (base_ProductGroup productGroup in saleOrderShipDetailModel.SaleOrderDetailModel.ProductModel.base_Product.base_ProductGroup1)
                                {
                                    //Get Product From ProductCollection (child product)
                                    base_ProductModel productInGroupModel = this.ProductCollection.SingleOrDefault(x => x.Id.Equals(productGroup.base_Product.Id));
                                    //Get Unit Of Product

                                    base_ProductUOM productGroupUOM = _saleOrderRepository.GetProductUomOfProductInGroup(SelectedSaleOrder.StoreCode, productGroup);
                                    if (productGroupUOM != null)
                                    {
                                        decimal baseUnitNumber = productGroupUOM.BaseUnitNumber;
                                        //productGroup.Quantity : quantity default of group
                                        decimal packQty = productGroup.Quantity * saleOrderShipDetailModel.PackedQty;
                                        _productRepository.UpdateOnHandQuantity(productInGroupModel.Resource.ToString(), SelectedSaleOrder.StoreCode, packQty, true, baseUnitNumber);
                                    }
                                }
                            }
                            else
                            {
                                decimal baseUnitNumber = saleOrderShipDetailModel.SaleOrderDetailModel.ProductUOMCollection.Single(x => x.UOMId.Equals(saleOrderShipDetailModel.SaleOrderDetailModel.UOMId)).BaseUnitNumber;
                                _productRepository.UpdateOnHandQuantity(saleOrderShipDetailModel.ProductResource, SelectedSaleOrder.StoreCode, saleOrderShipDetailModel.PackedQty, true, baseUnitNumber);
                            }

                            //Descrease Quantity OnCustomer which product in SaleOrderDetail
                            _saleOrderRepository.UpdateCustomerQuantity(saleOrderShipDetailModel.SaleOrderDetailModel, SelectedSaleOrder.StoreCode, saleOrderShipDetailModel.PackedQty, false);
                        }

                        saleOrderShipDetailModel.ToEntity();
                        if (saleOrderShipDetailModel.IsNew)
                            saleOrderShipModel.base_SaleOrderShip.base_SaleOrderShipDetail.Add(saleOrderShipDetailModel.base_SaleOrderShipDetail);
                    }

                    //Calulate Profit For product Package is shipped & is a new shipped
                    if (!saleOrderShipModel.base_SaleOrderShip.IsShipped && saleOrderShipModel.IsShipped)
                    {
                        /// Calulate Profit For product
                        var gShip = saleOrderShipModel.SaleOrderShipDetailCollection.GroupBy(x => x.ProductResource);
                        foreach (var item in gShip)//Foreach collection group with Product
                        {
                            if (item.Any(x => x.SaleOrderDetailModel.ProductModel.IsCoupon))//Not Calculate OnHand with Coupon
                                continue;
                            decimal totalQuantityBaseUom = 0;

                            if (item.Any(x => x.SaleOrderDetailModel.ProductModel.ItemTypeId.Equals((short)ItemTypes.Group)))
                            {
                                foreach (var saleOrderShipDetail in item)
                                {
                                    //Get Product In Group
                                    foreach (base_ProductGroup productGroup in saleOrderShipDetail.SaleOrderDetailModel.ProductModel.base_Product.base_ProductGroup1)
                                    {
                                        base_ProductUOM productGroupUOM = _saleOrderRepository.GetProductUomOfProductInGroup(SelectedSaleOrder.StoreCode, productGroup);
                                        decimal quantityBaseUnit = productGroupUOM.BaseUnitNumber;
                                        totalQuantityBaseUom += quantityBaseUnit * saleOrderShipDetail.PackedQty;
                                        decimal total = 0;
                                        //Get SaleOrderDetail to know is item change price ?
                                        base_SaleOrderDetailModel saleOrderDetailModel = SelectedSaleOrder.SaleOrderDetailCollection.SingleOrDefault(x => x.Resource.ToString().Equals(saleOrderShipDetail.SaleOrderDetailResource));
                                        if (saleOrderDetailModel != null)
                                        {
                                            int decimalPlace = Define.CONFIGURATION.DecimalPlaces.HasValue ? Define.CONFIGURATION.DecimalPlaces.Value : 0;
                                            decimal totalPriceProductGroup = saleOrderShipDetail.SaleOrderDetailModel.ProductModel.base_Product.base_ProductGroup1.Sum(x => x.Amount);
                                            decimal unitPrice = productGroup.RegularPrice + (productGroup.RegularPrice * (saleOrderDetailModel.SalePrice - totalPriceProductGroup) / totalPriceProductGroup);
                                            total = Math.Round(productGroup.Quantity * unitPrice, 2);
                                            _productRepository.UpdateProductStore(productGroup.ProductResource, SelectedSaleOrder.StoreCode, totalQuantityBaseUom, total, 0, 0, true);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                foreach (var saleOrderShipDetail in item)
                                {
                                    decimal quantityBaseUnit = saleOrderShipDetail.SaleOrderDetailModel.ProductUOMCollection.Single(x => x.UOMId.Equals(saleOrderShipDetail.SaleOrderDetailModel.UOMId)).BaseUnitNumber;
                                    totalQuantityBaseUom += quantityBaseUnit * saleOrderShipDetail.PackedQty;
                                }
                                _productRepository.UpdateProductStore(item.Key, SelectedSaleOrder.StoreCode, totalQuantityBaseUom, item.Sum(x => x.SaleOrderDetailModel.SalePrice * x.PackedQty), 0, 0, true);
                            }
                        }
                    }

                }
                //Map value Of Model To Entity
                saleOrderShipModel.ToEntity();
                if (saleOrderShipModel.IsNew)
                    SelectedSaleOrder.base_SaleOrder.base_SaleOrderShip.Add(saleOrderShipModel.base_SaleOrderShip);

            }
            #endregion

            #region SaleOrderReturn
            if (SelectedSaleOrder.ReturnModel != null)
            {
                bool calcGuestReward = false;
                if (SelectedSaleOrder.ReturnModel.SubTotal != SelectedSaleOrder.ReturnModel.base_ResourceReturn.SubTotal
                    && SelectedSaleOrder.Paid + SelectedSaleOrder.Deposit.Value >= SelectedSaleOrder.RewardAmount
                    && SelectedSaleOrder.ReturnModel.ReturnDetailCollection.Any(x => x.IsReturned))
                {
                    calcGuestReward = true;
                    //Value subtract
                    decimal valueSubtract = SelectedSaleOrder.ReturnModel.SubTotal - SelectedSaleOrder.ReturnModel.base_ResourceReturn.SubTotal;
                    SelectedSaleOrder.GuestModel.PurchaseDuringTrackingPeriod -= valueSubtract;
                }

                SelectedSaleOrder.ReturnModel.ToEntity();
                //Update Refund for SaleOrder
                SelectedSaleOrder.RefundedAmount = SelectedSaleOrder.ReturnModel.TotalRefund;

                if (SelectedSaleOrder.ReturnModel.IsNew && SelectedSaleOrder.ReturnModel.ReturnDetailCollection.DeletedItems.Any())
                {
                    foreach (base_ResourceReturnDetailModel returnDetailModel in SelectedSaleOrder.ReturnModel.ReturnDetailCollection.DeletedItems.Where(x => !x.IsTemporary))
                        _resourceReturnDetailRepository.Delete(returnDetailModel.base_ResourceReturnDetail);
                }
                SelectedSaleOrder.SaleOrderDetailCollection.DeletedItems.Clear();

                foreach (base_ResourceReturnDetailModel returnDetailModel in SelectedSaleOrder.ReturnModel.ReturnDetailCollection.Where(x => x.IsDirty))
                {
                    if (!returnDetailModel.SaleOrderDetailModel.ProductModel.IsCoupon)
                    {
                        decimal totalQuantityBaseUom = 0;
                        if (!returnDetailModel.base_ResourceReturnDetail.IsReturned && returnDetailModel.IsReturned)//New Item Return
                        {
                            decimal quantityBaseUnit = returnDetailModel.SaleOrderDetailModel.ProductUOMCollection.Single(x => x.UOMId.Equals(returnDetailModel.SaleOrderDetailModel.UOMId)).BaseUnitNumber;

                            totalQuantityBaseUom = quantityBaseUnit * returnDetailModel.ReturnQty;
                            //Update Product Profit
                            _productRepository.UpdateProductStore(returnDetailModel.ProductResource, SelectedSaleOrder.StoreCode, 0, 0, totalQuantityBaseUom, returnDetailModel.Price * returnDetailModel.ReturnQty, true);

                            //Increase Store for return product
                            _productRepository.UpdateOnHandQuantity(returnDetailModel.ProductResource, SelectedSaleOrder.StoreCode, totalQuantityBaseUom);

                            //Calculate return commission for Employee & Manager
                            CommissionReturn(SelectedSaleOrder, returnDetailModel);
                        }
                    }

                    returnDetailModel.ToEntity();
                    if (returnDetailModel.IsNew)
                        SelectedSaleOrder.ReturnModel.base_ResourceReturn.base_ResourceReturnDetail.Add(returnDetailModel.base_ResourceReturnDetail);
                }

                //Handle Return Reward For reward Member
                if (SelectedSaleOrder.GuestModel.IsRewardMember)
                {
                    var reward = GetReward(SelectedSaleOrder.OrderDate.Value.Date);

                    if (reward != null && calcGuestReward && SelectedSaleOrder.Balance == 0)
                    {
                        int totalOfReward = Convert.ToInt32(Math.Truncate(SelectedSaleOrder.GuestModel.PurchaseDuringTrackingPeriod / reward.PurchaseThreshold));
                        short rewardAvaliable = (short)GuestRewardStatus.Available;
                        short rewardPending = (short)GuestRewardStatus.Pending;

                        SelectedSaleOrder.GuestModel.RequirePurchaseNextReward = reward.PurchaseThreshold - ((SelectedSaleOrder.GuestModel.PurchaseDuringTrackingPeriod / reward.PurchaseThreshold) % 1) * reward.PurchaseThreshold;

                        IEnumerable<base_GuestReward> guestReward = SelectedSaleOrder.GuestModel.base_Guest.base_GuestReward.Where(x => (x.Status == rewardAvaliable || x.Status == rewardPending) && x.Reason != "Manual");
                        if (guestReward.Any() && totalOfReward > 0 && totalOfReward < guestReward.Count())
                        {
                            int rewardRemoved = guestReward.Count() - totalOfReward;
                            for (int i = rewardRemoved; i > 0; i--)
                                _guestRewardRepository.Delete(guestReward.ElementAt(i - 1));

                            _guestRewardRepository.Commit();
                        }

                        //Get GuestReward collection
                        SelectedSaleOrder.GuestModel.GuestRewardCollection = new CollectionBase<base_GuestRewardModel>();
                        foreach (base_GuestReward guestRewardItem in SelectedSaleOrder.GuestModel.base_Guest.base_GuestReward.Where(x => x.GuestId.Equals(SelectedSaleOrder.GuestModel.Id) && !x.IsApply && x.ActivedDate.Value <= DateTime.Today && (!x.ExpireDate.HasValue || (x.ExpireDate.HasValue && DateTime.Today <= x.ExpireDate.Value.Date))))
                        {
                            SelectedSaleOrder.GuestModel.GuestRewardCollection.Add(new base_GuestRewardModel(guestRewardItem));
                        }
                    }
                }

                if (SelectedSaleOrder.ReturnModel.IsNew)
                    _resourceReturnRepository.Add(SelectedSaleOrder.ReturnModel.base_ResourceReturn);
                _resourceReturnRepository.Commit();


                calcGuestReward = false;
                //Update ID
                SelectedSaleOrder.ReturnModel.Id = SelectedSaleOrder.ReturnModel.base_ResourceReturn.Id;
                SelectedSaleOrder.ReturnModel.EndUpdate();

                foreach (base_ResourceReturnDetailModel returnDetailModel in SelectedSaleOrder.ReturnModel.ReturnDetailCollection.Where(x => x.IsDirty))
                {
                    returnDetailModel.Id = returnDetailModel.base_ResourceReturnDetail.Id;
                    returnDetailModel.ResourceReturnId = returnDetailModel.base_ResourceReturnDetail.ResourceReturnId;
                    returnDetailModel.EndUpdate();
                }
            }
            #endregion

            #region Payment
            SavePaymentCollection(SelectedSaleOrder);
            #endregion

            #region Commission
            if (SelectedSaleOrder.CommissionCollection != null && SelectedSaleOrder.CommissionCollection.Any())
            {
                foreach (base_SaleCommissionModel saleCommissionModel in SelectedSaleOrder.CommissionCollection)
                {
                    saleCommissionModel.ToEntity();
                    if (saleCommissionModel.IsNew)
                        _saleCommissionRepository.Add(saleCommissionModel.base_SaleCommission);
                }
                _saleCommissionRepository.Commit();
                SelectedSaleOrder.CommissionCollection.Clear();
            }
            #endregion

            SelectedSaleOrder.UserUpdated = Define.USER != null ? Define.USER.LoginName : string.Empty;
            SelectedSaleOrder.ToEntity();
            _saleOrderRepository.Commit();
            _productRepository.Commit();

            //Set ID
            #region Update Id & Set End Update
            SelectedSaleOrder.ToModel();
            SelectedSaleOrder.EndUpdate();
            foreach (base_SaleOrderDetailModel saleOrderDetailModel in SelectedSaleOrder.SaleOrderDetailCollection)
            {
                saleOrderDetailModel.ToModel();
                saleOrderDetailModel.EndUpdate();
            }

            foreach (base_SaleOrderShipModel saleOrderShipModel in SelectedSaleOrder.SaleOrderShipCollection)
            {
                saleOrderShipModel.ToModel();
                foreach (base_SaleOrderShipDetailModel saleOrderShipDetailModel in saleOrderShipModel.SaleOrderShipDetailCollection)
                {
                    saleOrderShipDetailModel.ToModel();
                    saleOrderShipDetailModel.EndUpdate();
                }
                saleOrderShipModel.EndUpdate();
            }

            //Update ID For Payment
            if (SelectedSaleOrder.PaymentCollection != null)
            {
                foreach (base_ResourcePaymentModel paymentModel in SelectedSaleOrder.PaymentCollection.Where(x => x.IsNew))
                {
                    paymentModel.ToModel();
                    //Update or Add New PaymentDetail
                    if (paymentModel.PaymentDetailCollection != null)
                    {
                        foreach (base_ResourcePaymentDetailModel paymentDetailModel in paymentModel.PaymentDetailCollection.Where(x => x.IsNew))
                        {
                            paymentDetailModel.ToModel();
                            paymentDetailModel.EndUpdate();
                        }
                    }
                    paymentModel.EndUpdate();
                }
            }
            #endregion
            ShowShipTab(SelectedSaleOrder);
        }

        /// <summary>
        /// Save Sale Order
        /// </summary>
        /// <returns></returns>
        private bool SaveSalesOrder()
        {
            bool result = false;
            try
            {
                UnitOfWork.BeginTransaction();
                if (SelectedSaleOrder.IsNew)
                    InsertSaleOrder();
                else
                    UpdateSaleOrder();

                UpdateCustomer(SelectedSaleOrder);
                UnitOfWork.CommitTransaction();
                result = true;
            }
            catch (Exception ex)
            {
                UnitOfWork.RollbackTransaction();
                result = false;
                _log4net.Error(ex);
                MsgControl.ShowWarning(ex.ToString(), Language.GetMsg("ErrorCaption"), MessageBoxButtonCustom.OK);
            }
            return result;
        }

        /// <summary>
        /// calculate commission for employee
        /// <para>Need Payment Full</para>
        /// </summary>
        /// <param name="saleOrderModel"></param>
        private void SaveSaleCommission(base_SaleOrderModel saleOrderModel)
        {
            if (saleOrderModel.CommissionCollection == null)
                saleOrderModel.CommissionCollection = new CollectionBase<base_SaleCommissionModel>();
            if (!Define.CONFIGURATION.IsAllwayCommision)
            {
                ComboItem item = Common.BookingChannel.SingleOrDefault(x => x.Value == SelectedSaleOrder.BookingChanel);
                if (item.Flag)//True : this booking channel dont use commission
                    return;
            }

            foreach (base_SaleOrderDetailModel saleOrderDetailModel in saleOrderModel.SaleOrderDetailCollection.Where(x => x.ProductModel != null && x.ProductModel.IsAllowCommission))
            {
                decimal qtyReturn = 0;
                if (saleOrderDetailModel.ProductModel != null && saleOrderDetailModel.ProductModel.IsAllowCommission && saleOrderDetailModel.ProductModel.ComissionPercent > 0)
                {
                    //Get Quantity Item is Return
                    if (saleOrderModel.ReturnModel != null && saleOrderModel.ReturnModel.ReturnDetailCollection.Any(x => x.IsReturned))
                        qtyReturn = saleOrderModel.ReturnModel.ReturnDetailCollection.Where(x => x.IsReturned && x.OrderDetailResource.Equals(saleOrderDetailModel.Resource.ToString())).Sum(x => x.ReturnQty);

                    //Calculate Commision for Employee
                    Guid customerGuid = Guid.Parse(saleOrderModel.CustomerResource);
                    //Get Customer with CustomerResourceC
                    base_GuestModel customerModel = CustomerCollection.SingleOrDefault(x => x.Resource == customerGuid);
                    if (customerModel != null && customerModel.SaleRepId.HasValue)
                    {
                        base_GuestModel employeeModel = EmployeeCollection.SingleOrDefault(x => x.Id == customerModel.SaleRepId);
                        if (employeeModel != null)
                        {
                            base_SaleCommissionModel employeeCommission = new base_SaleCommissionModel();
                            employeeCommission.Remark = MarkType.SaleOrder.ToDescription();
                            employeeCommission.GuestResource = employeeModel.Resource.ToString();
                            employeeCommission.Sign = "+";
                            employeeCommission.Mark = "E";
                            employeeCommission.SOResource = saleOrderModel.Resource.ToString();
                            employeeCommission.SONumber = saleOrderModel.SONumber;
                            employeeCommission.SOTotal = saleOrderModel.RewardAmount;
                            employeeCommission.SODate = saleOrderModel.OrderDate;
                            employeeCommission.SaleOrderDetailResource = saleOrderDetailModel.Resource.ToString();
                            employeeCommission.ProductResource = saleOrderDetailModel.ProductModel.Resource.ToString();
                            employeeCommission.Attribute = saleOrderDetailModel.ProductModel.Attribute;
                            employeeCommission.RegularPrice = saleOrderDetailModel.RegularPrice;
                            employeeCommission.Price = saleOrderDetailModel.SalePrice;
                            employeeCommission.Size = saleOrderDetailModel.ProductModel.Size;
                            employeeCommission.Quanity = saleOrderDetailModel.Quantity - qtyReturn;
                            employeeCommission.Amount = employeeCommission.Price * employeeCommission.Quanity; //saleOrderDetailModel.SubTotal;
                            employeeCommission.ComissionPercent = employeeModel.CommissionPercent;


                            if (saleOrderDetailModel.ProductModel.CommissionUnit == 1) //$
                            {
                                employeeCommission.CommissionAmount = saleOrderDetailModel.ProductModel.ComissionPercent * employeeCommission.Quanity;
                            }
                            else
                            {
                                decimal comissionOfProduct = (saleOrderDetailModel.ProductModel.ComissionPercent * employeeCommission.Amount.Value) / 100;
                                employeeCommission.CommissionAmount = (comissionOfProduct * employeeCommission.ComissionPercent) / 100;
                            }

                            saleOrderModel.CommissionCollection.Add(employeeCommission);

                            //Has Manager
                            if (!string.IsNullOrWhiteSpace(employeeModel.ManagerResource))
                            {
                                //Calculate Commission for Manager
                                base_GuestModel managerModel = EmployeeCollection.SingleOrDefault(x => x.Resource.ToString().Equals(employeeModel.ManagerResource));
                                if (managerModel != null)
                                {
                                    base_SaleCommissionModel managerCommission = new base_SaleCommissionModel();
                                    managerCommission.Remark = MarkType.SaleOrder.ToDescription();
                                    managerCommission.GuestResource = managerModel.Resource.ToString();
                                    managerCommission.SOResource = saleOrderModel.Resource.ToString();
                                    managerCommission.SONumber = saleOrderModel.SONumber;
                                    managerCommission.SOTotal = saleOrderModel.RewardAmount;
                                    managerCommission.SODate = saleOrderModel.OrderDate;
                                    managerCommission.SaleOrderDetailResource = saleOrderDetailModel.Resource.ToString();
                                    managerCommission.ProductResource = saleOrderDetailModel.ProductModel.Resource.ToString();
                                    managerCommission.Attribute = saleOrderDetailModel.ProductModel.Attribute;
                                    managerCommission.Size = saleOrderDetailModel.ProductModel.Size;
                                    managerCommission.Attribute = saleOrderDetailModel.ProductModel.Attribute;
                                    managerCommission.RegularPrice = saleOrderDetailModel.RegularPrice;
                                    managerCommission.Price = saleOrderDetailModel.SalePrice;
                                    managerCommission.Quanity = saleOrderDetailModel.Quantity - qtyReturn;//subtract item is return in saleOrderDetail
                                    managerCommission.Amount = managerCommission.Quanity * managerCommission.Price; //saleOrderDetailModel.SubTotal;
                                    managerCommission.ComissionPercent = managerModel.CommissionPercent;
                                    managerCommission.Sign = "+";
                                    managerCommission.Mark = "M";

                                    //calculate commission manager from percent of commission employee
                                    managerCommission.CommissionAmount = (employeeCommission.CommissionAmount * managerCommission.ComissionPercent) / 100;

                                    saleOrderModel.CommissionCollection.Add(managerCommission);
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="saleOrderModel"></param>
        /// <param name="returnDetailModel"></param>
        private void CommissionReturn(base_SaleOrderModel saleOrderModel, base_ResourceReturnDetailModel returnDetailModel)
        {
            if (saleOrderModel.CommissionCollection == null)
                saleOrderModel.CommissionCollection = new CollectionBase<base_SaleCommissionModel>();

            //get SaleRep of this Customer
            Guid customerGuid = Guid.Parse(saleOrderModel.CustomerResource);
            //Get Customer with CustomerResource
            base_GuestModel customerModel = CustomerCollection.SingleOrDefault(x => x.Resource == customerGuid);
            if (customerModel == null || !customerModel.SaleRepId.HasValue)
                return;

            base_GuestModel employeeModel = EmployeeCollection.SingleOrDefault(x => x.Id == customerModel.SaleRepId);
            if (employeeModel == null)
                return;

            string employeeResource = employeeModel.Resource.ToString();
            IQueryable<base_SaleCommission> saleCommissions = _saleCommissionRepository.GetIQueryable(x => x.GuestResource.Equals(employeeResource) && x.SaleOrderDetailResource.Equals(returnDetailModel.OrderDetailResource) && x.ProductResource.Equals(returnDetailModel.ProductResource));
            if (saleCommissions.Any())
            {
                base_SaleCommissionModel employeeCommission = new base_SaleCommissionModel();
                employeeCommission.Remark = MarkType.SaleOrderReturn.ToDescription();
                employeeCommission.GuestResource = employeeModel.Resource.ToString();
                employeeCommission.Sign = "-";
                employeeCommission.Mark = "E";
                employeeCommission.SOResource = saleOrderModel.Resource.ToString();
                employeeCommission.SONumber = saleOrderModel.SONumber;
                employeeCommission.SOTotal = saleOrderModel.RewardAmount;
                employeeCommission.SODate = saleOrderModel.OrderDate;
                employeeCommission.SaleOrderDetailResource = returnDetailModel.OrderDetailResource;
                employeeCommission.ProductResource = returnDetailModel.ProductResource;
                employeeCommission.Attribute = returnDetailModel.SaleOrderDetailModel.ProductModel.Attribute;
                employeeCommission.Size = returnDetailModel.SaleOrderDetailModel.ProductModel.Size;
                employeeCommission.Quanity = returnDetailModel.ReturnQty;
                employeeCommission.RegularPrice = returnDetailModel.SaleOrderDetailModel.RegularPrice;
                employeeCommission.Price = returnDetailModel.SaleOrderDetailModel.SalePrice;
                employeeCommission.Amount = returnDetailModel.Amount;
                employeeCommission.ComissionPercent = employeeModel.CommissionPercent;


                if (returnDetailModel.SaleOrderDetailModel.ProductModel.CommissionUnit == 1) //$
                    employeeCommission.CommissionAmount = returnDetailModel.SaleOrderDetailModel.ProductModel.ComissionPercent;
                else
                {
                    decimal comissionOfProduct = (returnDetailModel.SaleOrderDetailModel.ProductModel.ComissionPercent * employeeCommission.Amount.Value) / 100;
                    employeeCommission.CommissionAmount = (comissionOfProduct * employeeCommission.ComissionPercent) / 100;
                }

                saleOrderModel.CommissionCollection.Add(employeeCommission);

                ///when get manager not get with Employee.ManagerResource, because manager may by change to another one, that manager is not received after
                //Get Manager get commission from this SaleOrder to subtract product return
                string saleOrderResource = saleOrderModel.Resource.ToString();
                //Manger(mark=M) get commssion (Sign : '+') of product (ProductResource) from SaleOrderDetail (SaleOrderDetailResource) of SaleOrder (saleOrderResource)
                base_SaleCommission mangerCommission = _saleCommissionRepository.Get(x => x.Sign.Equals("+") && x.Mark.Equals("M") && x.SOResource.Equals(saleOrderResource) && x.SaleOrderDetailResource.Equals(returnDetailModel.OrderDetailResource) && x.ProductResource.Equals(returnDetailModel.ProductResource));
                if (mangerCommission != null)//manger get Commission
                {
                    base_GuestModel managerModel = EmployeeCollection.SingleOrDefault(x => x.Resource.ToString().Equals(mangerCommission.GuestResource));
                    if (managerModel != null)
                    {
                        base_SaleCommissionModel managerCommssionReturn = new base_SaleCommissionModel();
                        managerCommssionReturn.Remark = MarkType.SaleOrderReturn.ToDescription();
                        managerCommssionReturn.GuestResource = managerModel.Resource.ToString();
                        managerCommssionReturn.Sign = "-";
                        managerCommssionReturn.Mark = "M";
                        managerCommssionReturn.SOResource = saleOrderModel.Resource.ToString();
                        managerCommssionReturn.SONumber = saleOrderModel.SONumber;
                        managerCommssionReturn.SOTotal = saleOrderModel.RewardAmount;
                        managerCommssionReturn.SODate = saleOrderModel.OrderDate;
                        managerCommssionReturn.SaleOrderDetailResource = returnDetailModel.OrderDetailResource;
                        managerCommssionReturn.ProductResource = returnDetailModel.ProductResource;
                        managerCommssionReturn.Attribute = returnDetailModel.SaleOrderDetailModel.ProductModel.Attribute;
                        managerCommssionReturn.Size = returnDetailModel.SaleOrderDetailModel.ProductModel.Size;
                        managerCommssionReturn.Quanity = returnDetailModel.ReturnQty;
                        managerCommssionReturn.RegularPrice = returnDetailModel.SaleOrderDetailModel.RegularPrice;
                        managerCommssionReturn.Price = returnDetailModel.SaleOrderDetailModel.SalePrice;
                        managerCommssionReturn.Amount = returnDetailModel.Amount;
                        managerCommssionReturn.ComissionPercent = employeeModel.CommissionPercent;

                        managerCommssionReturn.CommissionAmount = (employeeCommission.CommissionAmount * managerCommssionReturn.ComissionPercent) / 100;

                        saleOrderModel.CommissionCollection.Add(managerCommssionReturn);
                    }
                }
            }
        }

        /// <summary>
        /// Calculate Commission for refunded 
        /// </summary>
        /// <param name="saleOrderModel"></param>
        private void CalcCommissionForReturn(base_SaleOrderModel saleOrderModel)
        {
            ComboItem item = Common.BookingChannel.SingleOrDefault(x => x.Value == SelectedSaleOrder.BookingChanel);
            if (item.Flag)//True : this booking channel dont use commission
                return;

            if (saleOrderModel.CommissionCollection == null)
                saleOrderModel.CommissionCollection = new CollectionBase<base_SaleCommissionModel>();

            Guid customerGuid = Guid.Parse(saleOrderModel.CustomerResource);
            //Get Customer with CustomerResource
            base_GuestModel customerModel = CustomerCollection.Where(x => x.Resource == customerGuid).SingleOrDefault();
            if (customerModel != null && customerModel.SaleRepId.HasValue)
            {
                base_GuestModel employeeModel = EmployeeCollection.Where(x => x.Id == customerModel.SaleRepId).SingleOrDefault();
                string remarkReturn = MarkType.SaleOrderReturn.ToDescription();
                base_SaleCommission saleCommission = _saleCommissionRepository.Get(x => x.Sign.Equals("-") && x.Remark.Equals(remarkReturn) && x.GuestResource == employeeModel.ResourceString && x.SOResource.Equals(saleOrderModel.ResourceString));
                if (saleCommission == null)
                {
                    base_SaleCommissionModel newSaleCommission = new base_SaleCommissionModel();
                    newSaleCommission.ComissionPercent = employeeModel.CommissionPercent;
                    newSaleCommission.GuestResource = employeeModel.Resource.ToString();
                    newSaleCommission.Remark = MarkType.SaleOrderReturn.ToDescription();
                    newSaleCommission.Sign = "-";
                    newSaleCommission.SODate = saleOrderModel.OrderDate;
                    newSaleCommission.SONumber = saleOrderModel.SONumber;
                    newSaleCommission.SOResource = saleOrderModel.Resource.ToString();
                    newSaleCommission.SOTotal = saleOrderModel.Paid + saleOrderModel.Deposit;
                    newSaleCommission.CommissionAmount = saleOrderModel.ReturnModel.TotalRefund * newSaleCommission.ComissionPercent / 100;
                    saleOrderModel.CommissionCollection.Add(newSaleCommission);
                }
                else
                {
                    base_SaleCommissionModel UpdateSaleCommission = new base_SaleCommissionModel(saleCommission);
                    UpdateSaleCommission.SOTotal = saleOrderModel.Paid + saleOrderModel.Deposit;
                    UpdateSaleCommission.CommissionAmount = saleOrderModel.ReturnModel.TotalRefund * UpdateSaleCommission.ComissionPercent / 100;
                    if (saleOrderModel.CommissionCollection.Any(x => x.Sign.Equals("-")))
                    {
                        base_SaleCommissionModel updateCommisionModel = saleOrderModel.CommissionCollection.SingleOrDefault(x => x.Sign.Equals("-"));
                        updateCommisionModel = UpdateSaleCommission;
                    }
                    else
                    {
                        saleOrderModel.CommissionCollection.Add(UpdateSaleCommission);
                    }
                }
            }
        }

        /// <summary>
        /// Delete Sale Commision of SaleOrder
        /// </summary>
        /// <param name="saleOrderModel"></param>
        private void DeleteSaleCommission(base_SaleOrderModel saleOrderModel)
        {
            base_SaleCommission saleCommission = _saleCommissionRepository.GetAll().ToList().SingleOrDefault(x => x.SOResource.Equals(saleOrderModel.Resource.ToString()));
            if (saleCommission != null)
            {
                _saleCommissionRepository.Delete(saleCommission);
                _saleCommissionRepository.Commit();
            }
        }

        /// <summary>
        /// Execute payment
        /// </summary>
        private void SaleOrderPayment()
        {
            if (SelectedSaleOrder.IsNew)
                return;

            bool? resultReward;//True : go to payment process, False : Break process
            bool isPayFull = false;
            bool isRewardOnDiscount = Define.CONFIGURATION.IsRewardOnDiscount.HasValue ? Define.CONFIGURATION.IsRewardOnDiscount.Value : false;
            bool isApplyRewardDiscount = isRewardOnDiscount || (!isRewardOnDiscount && SelectedSaleOrder.DiscountPercent == 0);
            //Show Reward Form
            //Need check has any Guest Reward
            //Show Reward only SaleOrder Payment
            #region Check & Apply Reward

            if (SelectedSaleOrder.GuestModel.IsRewardMember)
            {
                if (isApplyRewardDiscount && SelectedSaleOrder.PaymentCollection != null
                           && !SelectedSaleOrder.PaymentCollection.Any(x => !x.IsDeposit.Value) /* This order is paid with multi pay*/)
                {
                    //Confirm User want to Payment Full
                    //msg: You have some rewards, you need to pay fully and use these rewards. Do you?
                    MessageBoxResultCustom confirmPayFull = MsgControl.ShowWarning(Language.GetMsg("SO_Message_NotifyPayfullUseReward"), Language.GetMsg("POSCaption"), MessageBoxButtonCustom.YesNo);
                    if (confirmPayFull.Equals(MessageBoxResultCustom.Yes))//User Payment full
                    {
                        isPayFull = true;
                        int ViewActionType;
                        if (Define.CONFIGURATION.IsRequirePromotionCode.HasValue && Define.CONFIGURATION.IsRequirePromotionCode.Value)//Open Enter Barcode 
                        {
                            ConfirmMemberRedeemRewardViewModel confirmMemberRedeemRewardViewModel = new ConfirmMemberRedeemRewardViewModel(SelectedSaleOrder);
                            resultReward = _dialogService.ShowDialog<ConfirmMemberRedeemRewardView>(_ownerViewModel, confirmMemberRedeemRewardViewModel, Language.GetMsg("SO_Message_RedeemReward") + "Validate reward Code");
                            ViewActionType = (int)confirmMemberRedeemRewardViewModel.ViewActionType;
                        }
                        else
                        {
                            RedeemRewardViewModel redeemRewardViewModel = new RedeemRewardViewModel(SelectedSaleOrder);
                            resultReward = _dialogService.ShowDialog<RedeemRewardView>(_ownerViewModel, redeemRewardViewModel, Language.GetMsg("SO_Message_RedeemReward"));
                            ViewActionType = (int)redeemRewardViewModel.ViewActionType;
                        }

                        if (resultReward == true)
                        {
                            if (ViewActionType == (int)ConfirmMemberRedeemRewardViewModel.ReeedemRewardType.Redeemded)
                                SelectedSaleOrder.RewardValueApply = 0;
                            else
                                SelectedSaleOrder.IsRedeeem = true;//Customer used reward
                        }
                    }
                    else
                    {
                        isPayFull = false;
                        resultReward = true;
                    }
                }
                else
                    resultReward = true;
            }
            else
                resultReward = true;

            #endregion

            if (resultReward == true)
            {
                SelectedSaleOrder.RewardValueApply = 0;

                //Calc Subtotal user apply reward
                if (SelectedSaleOrder.GuestModel.GuestRewardCollection != null && SelectedSaleOrder.GuestModel.GuestRewardCollection.Any(x => x.IsChecked))
                {
                    base_GuestRewardModel guestRewardModel = SelectedSaleOrder.GuestModel.GuestRewardCollection.SingleOrDefault(x => x.IsChecked);
                    if (guestRewardModel != null)
                    {
                        decimal subTotal = 0;
                        if (Define.CONFIGURATION.IsRewardOnTax)//Check reward include tax ?
                            subTotal = SelectedSaleOrder.SubTotal - SelectedSaleOrder.DiscountAmount + SelectedSaleOrder.TaxAmount + SelectedSaleOrder.Shipping;
                        else
                            subTotal = SelectedSaleOrder.SubTotal - SelectedSaleOrder.DiscountAmount + SelectedSaleOrder.Shipping;

                        //Update Subtoal After apply reward
                        if (guestRewardModel.RewardSetupUnit.Equals((short)RewardType.Pecent))
                        {
                            SelectedSaleOrder.RewardValueApply = Math.Round(Math.Round(subTotal * guestRewardModel.RewardSetupAmount / 100) - 0.01M, MidpointRounding.AwayFromZero);
                        }
                        else
                        {
                            //Check user using cash reward(sum) to paid
                            decimal depositeTotal = SelectedSaleOrder.PaymentCollection != null ? SelectedSaleOrder.PaymentCollection.Where(x => x.IsDeposit.Value).Sum(x => x.TotalPaid) : 0;
                            if (guestRewardModel.RewardSetupAmount > subTotal - depositeTotal)
                                SelectedSaleOrder.RewardValueApply = subTotal - depositeTotal;
                            else
                                SelectedSaleOrder.RewardValueApply = guestRewardModel.RewardSetupAmount;
                        }

                        //Update Reward Value
                        guestRewardModel.RewardValue = SelectedSaleOrder.RewardValueApply;

                        //Update Total Reward in MemberShip
                        if (guestRewardModel.RewardSetupUnit.Equals((short)RewardType.Money))
                        {
                            SelectedSaleOrder.GuestModel.MembershipValidated.TotalCashReward -= SelectedSaleOrder.RewardValueApply;
                        }
                        else if (guestRewardModel.RewardSetupUnit.Equals((short)RewardType.Point))
                        {
                            SelectedSaleOrder.GuestModel.MembershipValidated.TotalPointReward--;
                        }
                        else if (guestRewardModel.RewardSetupUnit.Equals((short)RewardType.Pecent))
                        {
                            SelectedSaleOrder.GuestModel.MembershipValidated.TotalPercentReward--;
                        }

                    }
                }
                //Update total have to paid
                if (SelectedSaleOrder.RewardValueApply != 0)
                    SelectedSaleOrder.RewardAmount = SelectedSaleOrder.Total - SelectedSaleOrder.RewardValueApply;

                //Return Product Proccess
                //Subtract total of refunded in Return process
                decimal refunded = SelectedSaleOrder.ReturnModel != null ? SelectedSaleOrder.ReturnModel.TotalRefund : 0;

                //Handle subtract money when has some product is return
                decimal returnValue = 0;
                if (SelectedSaleOrder.ReturnModel.ReturnDetailCollection.Any())
                {
                    //decimal returnTax = Math.Round(Math.Round(CalculateReturnTax(SelectedSaleOrder.ReturnModel, SelectedSaleOrder), Define.CONFIGURATION.DecimalPlaces.Value) - 0.01M, MidpointRounding.AwayFromZero);
                    returnValue = SelectedSaleOrder.ReturnModel.ReturnDetailCollection.Where(x => x.IsReturned).Sum(x => x.Amount + x.VAT - x.RewardRedeem - ((x.Amount * SelectedSaleOrder.DiscountPercent) / 100));
                }

                //End Return Product Proccess
                decimal paidValue = SelectedSaleOrder.PaymentCollection.Sum(x => x.TotalPaid - x.Change);
                decimal balance = SelectedSaleOrder.RewardAmount - returnValue - paidValue - (SelectedSaleOrder.Deposit.HasValue ? SelectedSaleOrder.Deposit.Value : 0);

                decimal totalDeposit = 0;
                decimal lastPayment = 0;
                if (SelectedSaleOrder.PaymentCollection != null)
                {
                    totalDeposit = SelectedSaleOrder.PaymentCollection.Where(x => x.IsDeposit.Value).Sum(x => x.TotalPaid);
                    base_ResourcePaymentModel lastPaymentModel = SelectedSaleOrder.PaymentCollection.Where(x => !x.IsDeposit.Value && x.TotalPaid > 0).OrderBy(x => x.DateCreated).LastOrDefault();
                    if (lastPaymentModel != null)
                        lastPayment = lastPaymentModel.TotalPaid;
                }

                //Show Payment
                SalesOrderPaymenViewModel paymentViewModel = new SalesOrderPaymenViewModel(SelectedSaleOrder, balance, totalDeposit, lastPayment, isPayFull);
                bool? dialogResult = _dialogService.ShowDialog<SalesOrderPaymentView>(_ownerViewModel, paymentViewModel, "Payment");
                if (dialogResult == true)
                {
                    //Calc Reward , redeem & update subtotal
                    CalcRedeemReward(SelectedSaleOrder);

                    if (Define.CONFIGURATION.DefaultCashiedUserName.HasValue && Define.CONFIGURATION.DefaultCashiedUserName.Value)
                        paymentViewModel.PaymentModel.Cashier = Define.USER.LoginName;
                    // Add new payment to collection
                    SelectedSaleOrder.PaymentCollection.Add(paymentViewModel.PaymentModel);

                    SelectedSaleOrder.Paid = SelectedSaleOrder.PaymentCollection.Where(x => !x.IsDeposit.Value).Sum(x => x.TotalPaid - x.Change);
                    SelectedSaleOrder.CalcBalance();

                    //Set Status
                    if (SelectedSaleOrder.Paid + SelectedSaleOrder.Deposit.Value >= SelectedSaleOrder.RewardAmount - returnValue)
                    {
                        if (SelectedSaleOrder.GuestModel.IsRewardMember && SelectedSaleOrder.GuestModel.MembershipValidated != null)//Only for Reward Member & validated is a membership
                        {
                            //Check IsCalRewardAfterRedeem Config
                            SelectedSaleOrder.GuestModel.PurchaseDuringTrackingPeriod += SelectedSaleOrder.RewardAmount;

                            bool isRewardApplied = SelectedSaleOrder.IsRedeeem;
                            if (Define.CONFIGURATION.IsCalRewardAfterRedeem //Calc reward anyway
                                || (!Define.CONFIGURATION.IsCalRewardAfterRedeem && !isRewardApplied))//calc new reward when so not apply redeem
                            {
                                CreateNewReward(SelectedSaleOrder);
                            }
                        }

                        if (SelectedSaleOrder.Mark.Equals(MarkType.SaleOrder.ToDescription()))
                        {
                            SelectedSaleOrder.OrderStatus = (short)SaleOrderStatus.PaidInFull;
                        }
                        else
                        {
                            SelectedSaleOrder.OrderStatus = (short)SaleOrderStatus.Close;//Set status to close when SO convert from Layaway/workorder/Quote
                        }

                        //Calculate & create commission for Employee
                        SaveSaleCommission(SelectedSaleOrder);

                        SaveSalesOrder();

                        //Clear Guest Collection After Save
                        SelectedSaleOrder.GuestModel.GuestRewardCollection.Clear();

                        SendEmailToCustomer();

                        //Not change to search when layaway cause after paid user may be execute shipping process
                        if (!SelectedSaleOrder.Mark.Equals(MarkType.Layaway.ToDescription()))
                            this.IsSearchMode = true;
                    }
                }
                else
                {
                    if (SelectedSaleOrder.GuestModel != null //Need for Quotation
                        && SelectedSaleOrder.GuestModel.GuestRewardCollection != null)
                    {
                        //Reset check
                        IEnumerable<base_GuestRewardModel> guestRewards = SelectedSaleOrder.GuestModel.GuestRewardCollection.Where(x => x.IsChecked);
                        foreach (base_GuestRewardModel guestReward in guestRewards)
                            guestReward.IsChecked = false;
                        SelectedSaleOrder.IsRedeeem = false;
                    }
                    SelectedSaleOrder.RewardAmount = SelectedSaleOrder.Total;
                }
                // Reset reward apply after use
                SelectedSaleOrder.RewardValueApply = 0;
            }

            SetAllowChangeOrder(SelectedSaleOrder);
        }

        /// <summary>
        /// Save payment collection, payment detail and payment product
        /// </summary>
        /// <param name="saleOrderModel"></param>
        private void SavePaymentCollection(base_SaleOrderModel saleOrderModel)
        {
            if (saleOrderModel.PaymentCollection != null)
            {
                foreach (base_ResourcePaymentModel paymentItem in saleOrderModel.PaymentCollection.Where(x => x.IsDirty))
                {
                    // Map data from model to entity
                    paymentItem.ToEntity();

                    if (paymentItem.PaymentDetailCollection != null)
                    {
                        foreach (base_ResourcePaymentDetailModel paymentDetailModel in paymentItem.PaymentDetailCollection.Where(x => x.IsDirty))
                        {
                            // Map data from model to entity
                            paymentDetailModel.ToEntity();

                            // Add new payment detail to database
                            if (paymentDetailModel.Id == 0)
                                paymentItem.base_ResourcePayment.base_ResourcePaymentDetail.Add(paymentDetailModel.base_ResourcePaymentDetail);
                        }
                    }

                    if (paymentItem.IsNew)
                        _paymentRepository.Add(paymentItem.base_ResourcePayment);
                    _paymentRepository.Commit();
                }
            }
        }

        /// <summary>
        /// Calculate Reward apply for guest
        /// </summary>
        /// <param name="customerModel"></param>
        /// <param name="saleOrderModel"></param>
        private void CalcRedeemReward(base_SaleOrderModel saleOrderModel)
        {
            if (!saleOrderModel.GuestModel.IsRewardMember)
                return;
            //Calc Subtotal user apply reward
            if (saleOrderModel.GuestModel.GuestRewardCollection.Any(x => x.IsChecked))
            {
                base_GuestRewardModel guestRewardModel = saleOrderModel.GuestModel.GuestRewardCollection.SingleOrDefault(x => x.IsChecked);

                guestRewardModel.Sign = "-";
                guestRewardModel.AppliedDate = DateTime.Today;
                guestRewardModel.Status = (short)GuestRewardStatus.Redeemed;
                guestRewardModel.SaleOrderNo = saleOrderModel.SONumber;
                guestRewardModel.SaleOrderResource = saleOrderModel.Resource.ToString();
                guestRewardModel.IsApply = true;

                //is Temp reward new turn on is new & off IsTemporary to insert new to db
                if (guestRewardModel.IsTemporary)
                {
                    guestRewardModel.IsNew = true;
                    guestRewardModel.IsTemporary = false;
                }

                //Set Total Reward Redeemed
                saleOrderModel.GuestModel.TotalRewardRedeemed += guestRewardModel.RewardValue;

                //Update Reward Redeemed to Reward manager
                base_RewardManager rewardManager = _rewardManagerRepository.Get(x => x.Id.Equals(guestRewardModel.RewardId));
                if (rewardManager != null)
                    rewardManager.TotalRewardRedeemed += guestRewardModel.RewardValue;
            }
        }

        /// <summary>
        /// Create New Reward for customer
        /// </summary>
        /// <param name="saleOrderModel"></param>
        private void CreateNewReward(base_SaleOrderModel saleOrderModel)
        {
            if (!saleOrderModel.GuestModel.IsRewardMember)
                return;
            //saleOrderModel.SubTotal >= x.PurchaseThreshold
            var reward = GetReward(saleOrderModel.OrderDate.Value.Date);

            if (reward != null && saleOrderModel.GuestModel.MembershipValidated != null)
            {
                int totalOfReward = 0;
                string msgExpireDate = string.Empty;
                //Check if not set Purchase Threshold
                if (saleOrderModel.GuestModel.RequirePurchaseNextReward == 0)
                    saleOrderModel.GuestModel.RequirePurchaseNextReward = reward.PurchaseThreshold;

                decimal totalOfPurchase = saleOrderModel.RewardAmount + (reward.PurchaseThreshold - saleOrderModel.GuestModel.RequirePurchaseNextReward);

                if (totalOfPurchase > reward.PurchaseThreshold)
                {
                    totalOfReward = Convert.ToInt32(Math.Truncate(totalOfPurchase / reward.PurchaseThreshold));
                    //Update Memeber ShipReward
                    if (reward.RewardAmtType.Is(RewardType.Money) || reward.RewardAmtType.Is(RewardType.Point))
                    {
                        saleOrderModel.GuestModel.MembershipValidated.TotalCashReward += totalOfReward * reward.RewardAmount;
                    }
                    else if (reward.RewardAmtType.Is(RewardType.Pecent))
                    {
                        saleOrderModel.GuestModel.MembershipValidated.TotalPercentReward += totalOfReward;
                    }

                    for (int i = 0; i < totalOfReward; i++)
                    {
                        base_GuestRewardModel guestRewardModel = new base_GuestRewardModel();
                        guestRewardModel.EarnedDate = DateTime.Today;
                        guestRewardModel.IsApply = false;
                        guestRewardModel.RewardId = reward.Id;
                        guestRewardModel.GuestId = saleOrderModel.GuestModel.Id;
                        guestRewardModel.SaleOrderNo = string.Empty;
                        guestRewardModel.SaleOrderResource = string.Empty;
                        guestRewardModel.Amount = 0;
                        guestRewardModel.RewardValue = 0;
                        guestRewardModel.Remark = string.Empty;
                        guestRewardModel.Sign = "+";
                        guestRewardModel.RewardSetupAmount = reward.RewardAmount;
                        guestRewardModel.RewardSetupUnit = (short)reward.RewardAmtType;

                        //Set Block reward redeemption for ??? days after earned
                        if (reward.IsBlockRedemption && reward.RedemptionAfterDays > 0)
                        {
                            guestRewardModel.Status = (short)GuestRewardStatus.Pending;
                            guestRewardModel.ActivedDate = guestRewardModel.EarnedDate.Value.AddDays(reward.RedemptionAfterDays);
                        }
                        else
                        {
                            guestRewardModel.Status = (int)GuestRewardStatus.Available;
                            guestRewardModel.ActivedDate = guestRewardModel.EarnedDate.Value;
                        }
                        //Set Expired Date For Reward
                        if (reward.RewardExpiration != 0)//RewardExpiration =0 (Never Expired)
                        {
                            int expireDay = Convert.ToInt32(Common.RewardExpirationTypes.Single(x => Convert.ToInt32(x.ObjValue) == reward.RewardExpiration).Detail);
                            guestRewardModel.ExpireDate = guestRewardModel.ActivedDate.Value.AddDays(expireDay);
                        }
                        else
                        {
                            guestRewardModel.ExpireDate = null;
                        }

                        msgExpireDate = guestRewardModel.ExpireDate.HasValue ? guestRewardModel.ExpireDate.Value.ToString(Define.DateFormat) : Language.GetMsg("SO_Message_RewardNeverExpired");

                        //Add to Temp Collection to Insert to db
                        saleOrderModel.GuestModel.GuestRewardCollection.DeletedItems.Add(guestRewardModel);
                    }

                }

                //Calculate Require Purchase Next Reward
                //A is PurchaseDuringTrackingPeriod
                //P is PurchaseThreshold
                //R is RequirePurchaseNextReward
                //R = P - (A/P % 2 * P)

                saleOrderModel.GuestModel.RequirePurchaseNextReward = reward.PurchaseThreshold - ((saleOrderModel.GuestModel.PurchaseDuringTrackingPeriod / reward.PurchaseThreshold) % 1) * reward.PurchaseThreshold;

                //Notify to Cashier about reward customer earned ? 
                if (reward.IsInformCashier)
                {
                    if (totalOfReward > 0)
                    {
                        string rewardProgram = string.Empty;
                        if (reward.RewardAmtType.Equals((int)RewardType.Money))
                            rewardProgram = string.Format(Language.GetMsg("SO_Message_RewardAmount"), string.Format(Define.ConverterCulture, Define.CurrencyFormat, reward.RewardAmount));
                        else
                            rewardProgram = string.Format(Language.GetMsg("SO_Message_RewardAmount") + " %", reward.RewardAmount);

                        //Msg : You are received : {0} reward(s) {1}  \nExpire Date : {2}

                        MsgControl.ShowWarning(string.Format(Language.GetMsg("SO_Message_ReceivedReward").ToString().Replace("\\n", "\n"), totalOfReward, rewardProgram, msgExpireDate), Language.GetMsg("POSCaption"), MessageBoxButtonCustom.OK);
                    }
                }
            }
        }

        /// <summary>
        /// Shipped Proccess 
        /// User click to shipped
        /// </summary>
        /// <param name="param"></param>
        private void ShippedProcess(object param)
        {
            //msg : "Do you want to ship?"
            MessageBoxResultCustom result = MsgControl.ShowWarning(Language.GetMsg("SO_Message_ConfirmShipItem"), Language.Warning, MessageBoxButtonCustom.YesNo);
            base_SaleOrderShipModel saleOrderShipModel = param as base_SaleOrderShipModel;
            if (result.Is(MessageBoxResultCustom.Yes))
            {

                saleOrderShipModel.IsShipped = saleOrderShipModel.IsChecked;

                SelectedSaleOrder.ShippedBox = Convert.ToInt16(SelectedSaleOrder.SaleOrderShipCollection.Count(x => x.IsShipped));

                SelectedSaleOrder.RaiseAnyShipped();

                SetShipStatus();

                if (SelectedSaleOrder.PaymentCollection == null)
                    SelectedSaleOrder.PaymentCollection = new ObservableCollection<base_ResourcePaymentModel>();

                //Set Referrence value Refund fee from config
                if (Define.CONFIGURATION.IsIncludeReturnFee || (SelectedSaleOrder.ReturnModel.ReturnFeePercent == 0 && SelectedSaleOrder.ReturnModel.ReturnFee == 0))
                    SelectedSaleOrder.ReturnModel.ReturnFeePercent = Define.CONFIGURATION.ReturnFeePercent;

                foreach (base_SaleOrderShipDetailModel saleOrderShipDetailModel in saleOrderShipModel.SaleOrderShipDetailCollection)
                {
                    saleOrderShipDetailModel.SaleOrderDetailModel = SelectedSaleOrder.SaleOrderDetailCollection.SingleOrDefault(x => x.Resource.ToString().Equals(saleOrderShipDetailModel.SaleOrderDetailResource));
                    base_SaleOrderShipDetailModel saleOrderShipClone = saleOrderShipDetailModel.Clone();
                    saleOrderShipClone.SaleOrderDetailModel = saleOrderShipDetailModel.SaleOrderDetailModel;

                    SelectedSaleOrder.SaleOrderShipDetailCollection.Add(saleOrderShipClone);

                    //Set for return Collection
                    //Existed item SaleOrderShippedDetail in Shipped Collection
                    if (SelectedSaleOrder.SaleOrderShippedCollection.Any(x => x.Resource.ToString().Equals(saleOrderShipDetailModel.SaleOrderDetailResource))
                        || SelectedSaleOrder.SaleOrderShippedCollection.DeletedItems.Any(x => x.Resource.ToString().Equals(saleOrderShipDetailModel.SaleOrderDetailResource)))
                    {
                        base_SaleOrderDetailModel saleOrderDetailModel = SelectedSaleOrder.SaleOrderShippedCollection.SingleOrDefault(x => x.Resource.ToString().Equals(saleOrderShipDetailModel.SaleOrderDetailResource));
                        if (saleOrderDetailModel != null)
                        {
                            saleOrderDetailModel.PickQty = SelectedSaleOrder.SaleOrderShipDetailCollection.Where(x => x.SaleOrderDetailResource.Equals(saleOrderDetailModel.Resource.ToString())).Sum(x => x.PackedQty);
                            saleOrderDetailModel.SubTotal = saleOrderDetailModel.PickQty * saleOrderDetailModel.SalePrice;
                        }
                        else
                        {
                            base_SaleOrderDetailModel saleOrderShippedRemoved = SelectedSaleOrder.SaleOrderShippedCollection.DeletedItems.SingleOrDefault(x => x.Resource.ToString().Equals(saleOrderShipDetailModel.SaleOrderDetailResource));
                            if (saleOrderShippedRemoved != null)
                            {
                                SelectedSaleOrder.SaleOrderShippedCollection.Add(saleOrderShippedRemoved);
                                SelectedSaleOrder.SaleOrderShippedCollection.DeletedItems.Remove(saleOrderShippedRemoved);
                            }
                        }
                    }
                    else
                    {
                        base_SaleOrderDetailModel saleOrderDetailModel = SelectedSaleOrder.SaleOrderDetailCollection.SingleOrDefault(x => x.Resource.ToString().Equals(saleOrderShipDetailModel.SaleOrderDetailResource)).Clone();
                        saleOrderDetailModel.PickQty = saleOrderShipDetailModel.PackedQty;
                        saleOrderDetailModel.SubTotal = saleOrderDetailModel.PickQty * saleOrderDetailModel.SalePrice;
                        SelectedSaleOrder.SaleOrderShippedCollection.Add(saleOrderDetailModel);
                    }

                    //lock quantity Combobox when item is shipped
                    Guid saleOrderShipDetailResource = Guid.Parse(saleOrderShipDetailModel.SaleOrderDetailResource);
                    base_SaleOrderDetailModel lockUOM = SelectedSaleOrder.SaleOrderDetailCollection.SingleOrDefault(x => x.Resource.Equals(saleOrderShipDetailResource));
                    if (lockUOM != null && !lockUOM.IsReadOnlyUOM)
                    {
                        lockUOM.IsReadOnlyUOM = true;
                    }

                }
                _saleOrderRepository.UpdateQtyOrderNRelate(SelectedSaleOrder);
                SelectedSaleOrder.SetFullPayment();
                //Save SaleOrder After Shipped
                UpdateSaleOrder();
                _productRepository.Commit();
                //Lock Order if any item is shipped
                SetAllowChangeOrder(SelectedSaleOrder);
            }
            else
            {

                saleOrderShipModel.IsChecked = false;
                saleOrderShipModel.IsShipped = false;
            }
        }

        //Calculation region
        #region "Calculate Tax"

        /// <summary>
        /// Apply Tax
        /// </summary>
        private decimal CalculateReturnTax(base_ResourceReturnModel returnModel, base_SaleOrderModel saleOrderModel)
        {
            decimal result = 0;
            if (saleOrderModel.TaxLocationModel.TaxCodeModel != null)
            {
                if (saleOrderModel.IsTaxExemption)
                {
                    result = 0;
                }
                else if (Convert.ToInt32(saleOrderModel.TaxLocationModel.TaxCodeModel.TaxOption).Is((int)SalesTaxOption.Multi))
                {
                    saleOrderModel.TaxPercent = 0;

                    foreach (base_ResourceReturnDetailModel returnDetailModel in returnModel.ReturnDetailCollection.Where(x => x.IsReturned))
                    {
                        if (!returnDetailModel.SaleOrderDetailModel.ProductModel.IsCoupon)//18/06/2013: not calculate tax for coupon
                            result += _saleOrderRepository.CalcMultiTaxForItem(saleOrderModel.TaxLocationModel.SaleTaxLocationOptionCollection, returnDetailModel.Amount, returnDetailModel.SaleOrderDetailModel.SalePrice);
                    }
                }
                else if (Convert.ToInt32(saleOrderModel.TaxLocationModel.TaxCodeModel.TaxOption).Is((int)SalesTaxOption.Price))
                {
                    saleOrderModel.TaxPercent = 0;
                    base_SaleTaxLocationOptionModel saleTaxLocationOptionModel = saleOrderModel.TaxLocationModel.TaxCodeModel.SaleTaxLocationOptionCollection.FirstOrDefault();
                    foreach (base_ResourceReturnDetailModel returnDetailModel in returnModel.ReturnDetailCollection.Where(x => x.IsReturned))
                    {
                        if (!returnDetailModel.SaleOrderDetailModel.ProductModel.IsCoupon)
                            result += _saleOrderRepository.CalcPriceDependentItem(returnDetailModel.Amount, returnDetailModel.SaleOrderDetailModel.SalePrice, saleTaxLocationOptionModel);
                    }
                }
                else
                {


                    base_SaleTaxLocationOptionModel taxOptionModel = saleOrderModel.TaxLocationModel.TaxCodeModel.SaleTaxLocationOptionCollection.FirstOrDefault();
                    if (taxOptionModel != null)
                    {
                        foreach (base_ResourceReturnDetailModel returnDetailModel in returnModel.ReturnDetailCollection.Where(x => x.IsReturned))
                        {
                            result += returnDetailModel.Amount * taxOptionModel.TaxRate / 100;
                        }
                    }
                }

            }
            return result;
        }
        #endregion

        /// <summary>
        /// Calculate Remain Return Quantity
        /// </summary>
        /// <param name="returnDetailModel"></param>
        private void CalculateRemainReturnQty(base_ResourceReturnDetailModel returnDetailModel, bool IsCalcAll = false)
        {
            decimal TotalItemReturn = 0;

            if (IsCalcAll)
                TotalItemReturn = SelectedSaleOrder.ReturnModel.ReturnDetailCollection.Where(x => x.OrderDetailResource.Equals(returnDetailModel.OrderDetailResource)).Sum(x => x.ReturnQty);
            else
                TotalItemReturn = SelectedSaleOrder.ReturnModel.ReturnDetailCollection.Where(x => !x.Resource.Equals(returnDetailModel.Resource) && x.OrderDetailResource.Equals(returnDetailModel.OrderDetailResource)).Sum(x => x.ReturnQty);
            decimal remainQuantity = SelectedSaleOrder.SaleOrderShippedCollection.Where(x => x.Resource.ToString().Equals(returnDetailModel.OrderDetailResource)).Sum(x => Convert.ToDecimal(x.PickQty)) - TotalItemReturn;
            returnDetailModel.ReturnQty = remainQuantity;
        }

        /// <summary>
        /// Calculate Subtotal of Return
        /// </summary>
        /// <param name="saleOrderModel"></param>
        private void CalculateReturnSubtotal(base_SaleOrderModel saleOrderModel)
        {
            if (saleOrderModel.ReturnModel != null && saleOrderModel.ReturnModel.ReturnDetailCollection.Any())
            {
                //saleOrderModel.ReturnModel.SubTotal = saleOrderModel.ReturnModel.ReturnDetailCollection.Sum(x => x.Amount);
                decimal subtotal = saleOrderModel.ReturnModel.ReturnDetailCollection.Sum(x => x.Amount + x.VAT - x.RewardRedeem - ((x.Amount * saleOrderModel.DiscountPercent) / 100));
                int decimalPlace = Define.CONFIGURATION.DecimalPlaces.HasValue ? Define.CONFIGURATION.DecimalPlaces.Value : 0;
                saleOrderModel.ReturnModel.SubTotal = subtotal;// Math.Round(Math.Round(subtotal, decimalPlace) - 0.01M, MidpointRounding.AwayFromZero);
            }
            else
                saleOrderModel.ReturnModel.SubTotal = 0;
        }

        /// <summary>
        /// Return Detail Subtotal = Amount + VAT - rewardReedem - Discount(Order)
        /// </summary>
        /// <param name="returnDetailModel"></param>
        private void CalcReturnDetailSubTotal(base_SaleOrderModel saleOrderModel, base_ResourceReturnDetailModel returnDetailModel)
        {
            decimal subtotal = returnDetailModel.Amount + returnDetailModel.VAT - returnDetailModel.RewardRedeem - ((returnDetailModel.Amount * saleOrderModel.DiscountPercent) / 100);
            int decimalPlace = Define.CONFIGURATION.DecimalPlaces.HasValue ? Define.CONFIGURATION.DecimalPlaces.Value : 0;
            returnDetailModel.SubTotalDetail = subtotal;//Math.Round(Math.Round(subtotal, decimalPlace) - 0.01M, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Calc Return Reward for item returned
        /// </summary>
        /// <param name="saleOrderModel"></param>
        /// <param name="returnDetailModel"></param>
        private void CalcReturnDetailRewardRedeem(base_SaleOrderModel saleOrderModel, base_ResourceReturnDetailModel returnDetailModel)
        {
            if (saleOrderModel.Total != saleOrderModel.RewardAmount && saleOrderModel.RewardAmount > 0)//Has Apply Reward
            {
                decimal rewardApply = saleOrderModel.Total - saleOrderModel.RewardAmount;
                //Calculate reward redeem with amount include tax
                decimal rewardRedeem = Math.Round(Math.Round(((returnDetailModel.Amount + returnDetailModel.VAT) * rewardApply) / saleOrderModel.Total, Define.CONFIGURATION.DecimalPlaces.Value) - 0.01M, MidpointRounding.AwayFromZero);
                returnDetailModel.RewardRedeem = rewardRedeem;
            }
        }

        //Update value

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        private void TabChanged(int saleTab)
        {
            //if (!IsDirty)
            //    return;
            bool allowChangeTab = true;
            switch (saleTab)
            {
                case (int)SaleOrderTab.Order:
                    if (!IsOrderValid || !IsReturnValid || !IsShipValid)
                        allowChangeTab = false;
                    break;
                case (int)SaleOrderTab.Ship:
                    if (!IsOrderValid || !IsReturnValid || !IsShipValid)
                    {
                        allowChangeTab = false;
                    }
                    else
                        if (SelectedSaleOrder.SaleOrderDetailCollection.Any(x => x.IsDirty) && _previousTabIndex.Is(SaleOrderTab.Order))//Change from SaleOrderTab
                        {
                            if (IsValid & IsOrderValid)
                                SaveSalesOrder();
                        }
                    break;
                case (int)SaleOrderTab.Payment:
                    if (!IsOrderValid || !IsReturnValid || !IsShipValid)
                    {
                        allowChangeTab = false;
                    }
                    break;
                case (int)SaleOrderTab.Return:
                    if (!IsOrderValid || !IsReturnValid || !IsShipValid)
                    {
                        allowChangeTab = false;
                    }
                    break;
            }

            if (!allowChangeTab)
            {
                App.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    //msg: notify fix error
                    MsgControl.ShowWarning(Language.GetMsg("M107"), Language.GetMsg("POSCaption"), MessageBoxButtonCustom.OK);
                    _selectedTabIndex = _previousTabIndex;
                    OnPropertyChanged(() => SelectedTabIndex);
                }), System.Windows.Threading.DispatcherPriority.Background);
            }


        }

        /// <summary>
        /// Return All 
        /// Set all item is shipped to return collection
        /// If exited item in return collection and item is not set returned, it will be added quantity. Otherwise create new item to return collection 
        /// </summary>
        private void ReturnAll()
        {
            if (SelectedSaleOrder.SaleOrderShipDetailCollection != null)
            {
                foreach (base_SaleOrderDetailModel saleOrderDetailModel in SelectedSaleOrder.SaleOrderDetailCollection)
                {
                    if (SelectedSaleOrder.SaleOrderShipDetailCollection.Any(x => x.SaleOrderDetailResource.Equals(saleOrderDetailModel.Resource.ToString())))
                    {
                        base_ResourceReturnDetailModel returnDetailModel = new base_ResourceReturnDetailModel();

                        returnDetailModel.SaleOrderDetailModel = saleOrderDetailModel;
                        returnDetailModel.OrderDetailResource = saleOrderDetailModel.Resource.ToString();
                        returnDetailModel.SaleOrderModel = SelectedSaleOrder;
                        returnDetailModel.IsParent = (returnDetailModel.SaleOrderDetailModel.ProductModel != null && returnDetailModel.SaleOrderDetailModel.ProductModel.ItemTypeId.Equals((short)ItemTypes.Group));
                        CalculateRemainReturnQty(returnDetailModel, true);
                        if (returnDetailModel.ReturnQty > 0)
                        {
                            returnDetailModel.ProductResource = saleOrderDetailModel.ProductResource;
                            returnDetailModel.ItemCode = saleOrderDetailModel.ItemCode;
                            returnDetailModel.ItemName = saleOrderDetailModel.ItemName;
                            returnDetailModel.ItemAtribute = saleOrderDetailModel.ItemAtribute;
                            returnDetailModel.ItemSize = saleOrderDetailModel.ItemSize;
                            returnDetailModel.UnitName = saleOrderDetailModel.UnitName;
                            returnDetailModel.Price = saleOrderDetailModel.SalePrice;
                            returnDetailModel.Amount = returnDetailModel.Price * returnDetailModel.ReturnQty;
                            CalcReturnQtyBaseUnit(returnDetailModel, returnDetailModel.SaleOrderDetailModel);
                            returnDetailModel.IsTemporary = false;
                            //Existed item not return & the same of SaleOrderDetailResource=>update Return Qty
                            if (SelectedSaleOrder.ReturnModel.ReturnDetailCollection.Where(x => !x.IsReturned && x.OrderDetailResource.Equals(returnDetailModel.OrderDetailResource)).Any())
                            {
                                base_ResourceReturnDetailModel returnDetailModelUpdate = SelectedSaleOrder.ReturnModel.ReturnDetailCollection.SingleOrDefault(x => !x.IsReturned && x.OrderDetailResource.Equals(returnDetailModel.OrderDetailResource));
                                returnDetailModelUpdate.ReturnQty += returnDetailModel.ReturnQty;
                            }
                            else
                            {
                                SelectedSaleOrder.ReturnModel.ReturnDetailCollection.Add(returnDetailModel);
                                returnDetailModel.IsTemporary = false;
                            }

                            returnDetailModel.VAT = _saleOrderRepository.CalculateReturnDetailTax(returnDetailModel, SelectedSaleOrder);
                            CalcReturnDetailRewardRedeem(SelectedSaleOrder, returnDetailModel);
                            CalcReturnDetailSubTotal(SelectedSaleOrder, returnDetailModel);
                        }
                    }
                }
                CalculateReturnSubtotal(SelectedSaleOrder);
            }
        }

        /// <summary>
        /// Check item is return all. 
        /// if item is return all, remove collection shipped to not show in autocomplete choice Product
        /// </summary>
        private void CheckReturned()
        {
            if (SelectedSaleOrder == null)
                return;
            var allReturn = SelectedSaleOrder.ReturnModel.ReturnDetailCollection.Where(x => !x.IsTemporary && x.SaleOrderDetailModel != null);


            foreach (var item in allReturn)
            {
                decimal totalReturn = allReturn.Where(x => x.OrderDetailResource.Equals(item.OrderDetailResource)).Sum(x => x.ReturnQty);
                decimal totalShipped = SelectedSaleOrder.SaleOrderShippedCollection.Where(x => x.Resource.ToString().Equals(item.OrderDetailResource)).Sum(x => x.PickQty);
                totalShipped += SelectedSaleOrder.SaleOrderShippedCollection.DeletedItems.Where(x => x.Resource.ToString().Equals(item.OrderDetailResource)).Sum(x => x.PickQty);
                if (totalShipped <= totalReturn)
                {
                    base_SaleOrderDetailModel saleOrderShippedModel = SelectedSaleOrder.SaleOrderShippedCollection.SingleOrDefault(x => x.Resource.ToString().Equals(item.OrderDetailResource));
                    if (saleOrderShippedModel != null)
                        SelectedSaleOrder.SaleOrderShippedCollection.Remove(saleOrderShippedModel);
                }
                else
                {
                    base_SaleOrderDetailModel saleOrderShippedRemoved = SelectedSaleOrder.SaleOrderShippedCollection.DeletedItems.SingleOrDefault(x => x.Resource.ToString().Equals(item.OrderDetailResource));
                    if (saleOrderShippedRemoved != null)
                    {
                        //add To CollectionShipped
                        SelectedSaleOrder.SaleOrderShippedCollection.Add(saleOrderShippedRemoved);
                        //Remove In Collection DeletedItems
                        SelectedSaleOrder.SaleOrderShippedCollection.DeletedItems.Remove(saleOrderShippedRemoved);
                    }
                }
            }
        }

        /// <summary>
        /// Check Item Is Return All
        /// </summary>
        /// <param name="selectedReturnDetail"></param>
        private void CheckReturned(base_ResourceReturnDetailModel selectedReturnDetail)
        {
            if (SelectedSaleOrder.ReturnModel.ReturnDetailCollection.Any(x => x.OrderDetailResource.Equals(selectedReturnDetail.OrderDetailResource)))
            {
                base_SaleOrderDetailModel saleOrderShippedRemoved = SelectedSaleOrder.SaleOrderShippedCollection.DeletedItems.SingleOrDefault(x => x.Resource.ToString().Equals(selectedReturnDetail.OrderDetailResource));
                if (saleOrderShippedRemoved != null)
                {
                    SelectedSaleOrder.SaleOrderShippedCollection.Add(saleOrderShippedRemoved);
                    SelectedSaleOrder.SaleOrderShippedCollection.DeletedItems.Remove(saleOrderShippedRemoved);
                }
                //Remove Item Returned All
                //Get Item Diffrent with Current Item Selected
                var saleOrderShipped = SelectedSaleOrder.SaleOrderShippedCollection.Where(x => !x.Resource.ToString().Equals(selectedReturnDetail.OrderDetailResource));
                foreach (base_SaleOrderDetailModel saleOrderShippedModel in saleOrderShipped.ToList())
                {
                    decimal totalReturn = SelectedSaleOrder.ReturnModel.ReturnDetailCollection.Where(x => !x.IsTemporary && x.SaleOrderDetailModel != null && x.SaleOrderDetailModel.Resource.Equals(saleOrderShippedModel.Resource)).Sum(x => x.ReturnQty);
                    decimal totalShipped = saleOrderShippedModel.PickQty;
                    totalShipped += SelectedSaleOrder.SaleOrderShippedCollection.DeletedItems.Where(x => x.Resource.Equals(saleOrderShippedModel.Resource)).Sum(x => x.PickQty);
                    if (totalShipped <= totalReturn)
                    {
                        SelectedSaleOrder.SaleOrderShippedCollection.Remove(saleOrderShippedModel);
                    }
                }

            }
        }

        /// <summary>
        /// Store Changed
        /// </summary>
        private void StoreChanged()
        {
            foreach (base_SaleOrderDetailModel saleOrderDetailModel in this.SelectedSaleOrder.SaleOrderDetailCollection)
            {
                SetPriceUOM(saleOrderDetailModel);

                CalculateDiscount(saleOrderDetailModel);

                _saleOrderRepository.CalcOnHandStore(SelectedSaleOrder, saleOrderDetailModel);

                saleOrderDetailModel.CalcSubTotal();

                saleOrderDetailModel.CalcDueQty();

                saleOrderDetailModel.CalUnfill();
            }
            SelectedSaleOrder.CalcSubTotal();
        }

        /// <summary>
        /// Update Pick quatity for parent when Child of Product Group Changed qty of pick pack
        /// </summary>
        /// <param name="saleOrderDetailModel"></param>
        private void UpdatePickQtyForParent(base_SaleOrderDetailModel saleOrderDetailModel)
        {
            if (!string.IsNullOrWhiteSpace(saleOrderDetailModel.ParentResource))//ChildOf ProductGroup
            {
                //Get Parent Item for update
                base_SaleOrderDetailModel parentSaleOrderDetailModel = SelectedSaleOrder.SaleOrderDetailCollection.SingleOrDefault(x => x.Resource.ToString().Equals(saleOrderDetailModel.ParentResource));
                var childGroupList = SelectedSaleOrder.SaleOrderDetailCollection.Where(x => x.ParentResource.Equals(saleOrderDetailModel.ParentResource));
                decimal totalQty = childGroupList.Sum(x => x.Quantity);
                decimal totalOfPick = childGroupList.Sum(x => x.PickQty);
                decimal parentPickQty = totalQty == 0 ? 0 : totalOfPick * parentSaleOrderDetailModel.Quantity / totalQty;
                parentSaleOrderDetailModel.PickQty = Math.Round(parentPickQty, 2);
            }
        }

        //Handle from another form
        /// <summary>
        /// Open Sale Order or Quotation Advance Search
        /// </summary>
        private void OpenSOAdvanceSearch()
        {
            POSOAdvanceSearchViewModel viewModel = new POSOAdvanceSearchViewModel(false);
            bool? dialogResult = _dialogService.ShowDialog<POSOAdvanceSearchView>(_ownerViewModel, viewModel, "Sale Order Advance Search");

            if (dialogResult == true)
            {
                Expression<Func<base_SaleOrder, bool>> predicate = viewModel.SOPredicate;
                LoadDataByPredicate(predicate, false, 0);
            }
        }

        /// <summary>
        /// Method Check config accept send email to customer
        /// </summary>
        private void SendEmailToCustomer()
        {
            //Send Email To Customer
            if (Define.CONFIGURATION.IsSendEmailCustomer)
            {
                //MsgControl.ShowWarning("Send Email");
            }
        }
        #endregion

        #region Propertychanged

        private void SelectedSaleOrder_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base_SaleOrderModel saleOrderModel = sender as base_SaleOrderModel;
            switch (e.PropertyName)
            {
                case "SONumber":
                    CheckDuplicateSoNum(saleOrderModel);
                    break;
                case "SubTotal":
                    CalculateAllTax(saleOrderModel);
                    saleOrderModel.CalcDiscountAmount();
                    break;
                case "Total":
                    saleOrderModel.CalcBalance();
                    if (!saleOrderModel.IsRedeeem)
                        saleOrderModel.RewardAmount = saleOrderModel.Total;
                    break;
                case "RefundedAmount":
                case "RewardAmount":
                case "Deposit":
                    saleOrderModel.CalcBalance();
                    break;
                case "Paid":
                    saleOrderModel.CalcBalance();
                    break;
                case "Shipping":
                    saleOrderModel.ShipTaxAmount = CalcShipTaxAmount(saleOrderModel);
                    saleOrderModel.CalcTotal();
                    break;
                case "ProductTaxAmount":
                case "ShipTaxAmount":
                    if (saleOrderModel.TaxLocationModel.TaxCodeModel.IsTaxAfterDiscount)
                        saleOrderModel.TaxAmount = saleOrderModel.ProductTaxAmount + saleOrderModel.ShipTaxAmount - saleOrderModel.DiscountAmount;
                    else
                        saleOrderModel.TaxAmount = saleOrderModel.ShipTaxAmount + saleOrderModel.ProductTaxAmount;

                    break;
                case "TaxAmount":
                    saleOrderModel.CalcTotal();
                    break;
                case "DiscountAmount":
                    saleOrderModel.CalcDiscountPercent();
                    saleOrderModel.SkipDisc = false;
                    if (saleOrderModel.TaxLocationModel.TaxCodeModel != null)
                    {
                        if (saleOrderModel.TaxLocationModel.TaxCodeModel.IsTaxAfterDiscount)
                            saleOrderModel.TaxAmount = saleOrderModel.ProductTaxAmount + saleOrderModel.ShipTaxAmount - saleOrderModel.DiscountAmount;
                        else
                            saleOrderModel.TaxAmount = saleOrderModel.ShipTaxAmount + saleOrderModel.ProductTaxAmount;
                    }

                    saleOrderModel.CalcTotal();
                    break;
                case "DiscountPercent":
                    saleOrderModel.CalcDiscountAmount();
                    saleOrderModel.SkipDisc = false;
                    break;
                case "PriceSchemaId"://Update Price When Price Schema Changed
                    PriceSchemaChanged();

                    saleOrderModel.PriceLevelItem = Common.PriceSchemas.SingleOrDefault(x => Convert.ToInt16(x.ObjValue).Equals(saleOrderModel.PriceSchemaId));
                    break;
                case "OrderStatus":
                    SetAllowChangeOrder(saleOrderModel);
                    saleOrderModel.SetFullPayment();

                    //Set Text Status
                    saleOrderModel.ItemStatus = Common.StatusSalesOrders.SingleOrDefault(x => Convert.ToInt16(x.ObjValue).Equals(saleOrderModel.OrderStatus));
                    break;
                case "StoreCode":
                    StoreChanged();
                    break;
                case "TotalPaid":
                    saleOrderModel.ReturnModel.CalcBalance(saleOrderModel.TotalPaid);
                    break;
                //case "BookingChanel":
                //    SetSaleTaxLocationForSaleOrder(saleOrderModel);
                //    break;




            }
        }

        protected override void SaleOrderDetailModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (BreakAllChange || BreakSODetailChange)
                return;
            base_SaleOrderDetailModel saleOrderDetailModel = sender as base_SaleOrderDetailModel;
            switch (e.PropertyName)
            {
                case "SalePrice":
                    saleOrderDetailModel.SalePriceChanged();
                    saleOrderDetailModel.CalcSubTotal();
                    CalculateMultiNPriceTax();
                    _saleOrderRepository.CheckToShowDatagridRowDetail(saleOrderDetailModel);
                    break;
                case "Quantity":
                    //Update child quantity when parent change (apply only for Product Group)

                    if (saleOrderDetailModel.ProductModel.ItemTypeId.Equals((short)ItemTypes.Group))
                    {
                        var childInGroup = SelectedSaleOrder.SaleOrderDetailCollection.Where(x => x.ParentResource.Equals(saleOrderDetailModel.Resource.ToString()));
                        if (childInGroup.Any())//Is a group 
                        {
                            foreach (base_SaleOrderDetailModel saleOrderDetaiInGrouplModel in childInGroup)
                            {
                                saleOrderDetaiInGrouplModel.Quantity = saleOrderDetaiInGrouplModel.ProductGroupItem.Quantity * saleOrderDetailModel.Quantity;
                                //Update Parent Pick Qty
                                UpdatePickQtyForParent(saleOrderDetaiInGrouplModel);
                                if (saleOrderDetaiInGrouplModel.ProductModel.IsSerialTracking)
                                    OpenTrackingSerialNumber(saleOrderDetaiInGrouplModel, true, true);
                            }
                        }
                    }
                    else//Child of Product Group Change Quanity
                        UpdatePickQtyForParent(saleOrderDetailModel);
                    saleOrderDetailModel.CalcDueQty();
                    saleOrderDetailModel.CalcSubTotal();
                    if (!saleOrderDetailModel.ProductModel.IsSerialTracking)
                    {
                        BreakSODetailChange = true;
                        _saleOrderRepository.CalcProductDiscount(SelectedSaleOrder, saleOrderDetailModel);
                        BreakSODetailChange = false;
                    }

                    CalculateMultiNPriceTax();
                    SelectedSaleOrder.CalcSubTotal();
                    _saleOrderRepository.CalcOnHandStore(SelectedSaleOrder, saleOrderDetailModel);
                    //SetShipStatus();
                    _saleOrderRepository.UpdateQtyOrderNRelate(SelectedSaleOrder);
                    break;
                case "DueQty":
                    saleOrderDetailModel.CalUnfill();
                    break;
                case "PickQty":
                    //Calc PickQty for parent if pickqty change is a child of ProductGroup
                    UpdatePickQtyForParent(saleOrderDetailModel);

                    saleOrderDetailModel.CalcDueQty();
                    break;
                case "UOMId":
                    SetPriceUOM(saleOrderDetailModel);

                    BreakSODetailChange = true;
                    _saleOrderRepository.CalcProductDiscount(SelectedSaleOrder, saleOrderDetailModel);
                    BreakSODetailChange = false;

                    _saleOrderRepository.CalcOnHandStore(SelectedSaleOrder, saleOrderDetailModel);

                    _saleOrderRepository.UpdateQtyOrderNRelate(SelectedSaleOrder);
                    break;
                case "SubTotal":
                    SelectedSaleOrder.CalcSubTotal();
                    break;
                case "IsQuantityAccepted":
                    if (SelectedSaleOrder.SaleOrderDetailCollection != null)
                        SelectedSaleOrder.IsHiddenErrorColumn = !SelectedSaleOrder.SaleOrderDetailCollection.Any(x => !x.IsQuantityAccepted);
                    break;
            }
        }

        protected override void SaleOrderDetailCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            base_SaleOrderDetailModel saleOrderDetailModel;
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var item in e.NewItems)
                {
                    saleOrderDetailModel = item as base_SaleOrderDetailModel;
                    saleOrderDetailModel.PropertyChanged += new PropertyChangedEventHandler(SaleOrderDetailModel_PropertyChanged);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var item in e.OldItems)
                {
                    saleOrderDetailModel = item as base_SaleOrderDetailModel;
                    saleOrderDetailModel.PropertyChanged -= new PropertyChangedEventHandler(SaleOrderDetailModel_PropertyChanged);
                }
            }
        }

        private void SelectedSaleOrderShip_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "BoxNo":
                    SelectedSaleOrder.RaiseTotalShipBox();
                    break;
                case "Weight":
                    SelectedSaleOrder.RaiseTotalWeight();
                    break;
            }

        }

        private void ResourceReturnDetailModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base_ResourceReturnDetailModel returnDetailModel = sender as base_ResourceReturnDetailModel;
            switch (e.PropertyName)
            {
                case "SaleOrderDetailModel":
                    if (returnDetailModel.SaleOrderDetailModel != null)
                    {
                        if (string.IsNullOrWhiteSpace(returnDetailModel.OrderDetailResource) || !returnDetailModel.SaleOrderDetailModel.ProductResource.Equals(returnDetailModel.ProductResource))
                        {
                            returnDetailModel.OrderDetailResource = returnDetailModel.SaleOrderDetailModel.Resource.ToString();
                            returnDetailModel.SaleOrderModel = SelectedSaleOrder;
                            returnDetailModel.ProductResource = returnDetailModel.SaleOrderDetailModel.ProductResource;
                            returnDetailModel.ItemCode = returnDetailModel.SaleOrderDetailModel.ItemCode;
                            returnDetailModel.ItemName = returnDetailModel.SaleOrderDetailModel.ItemName;
                            returnDetailModel.ItemAtribute = returnDetailModel.SaleOrderDetailModel.ItemAtribute;
                            returnDetailModel.ItemSize = returnDetailModel.SaleOrderDetailModel.ItemSize;
                            returnDetailModel.UnitName = returnDetailModel.SaleOrderDetailModel.UnitName;
                            returnDetailModel.Price = returnDetailModel.SaleOrderDetailModel.SalePrice;
                            //Product is Parent of goup not change quantity when return
                            returnDetailModel.IsParent = (returnDetailModel.SaleOrderDetailModel.ProductModel != null && returnDetailModel.SaleOrderDetailModel.ProductModel.ItemTypeId.Equals((short)ItemTypes.Group));
                            CalculateRemainReturnQty(returnDetailModel);
                        }
                    }
                    else
                    {
                        returnDetailModel.OrderDetailResource = null;
                        returnDetailModel.ProductResource = null;
                        returnDetailModel.ItemCode = null;
                        returnDetailModel.ItemName = null;
                        returnDetailModel.ItemAtribute = null;
                        returnDetailModel.ItemSize = null;
                        returnDetailModel.Price = 0;
                        returnDetailModel.ReturnQty = 0;
                    }
                    break;
                case "Price":
                    returnDetailModel.Amount = returnDetailModel.Price * returnDetailModel.ReturnQty;
                    break;
                case "ReturnQty":
                    //resourceReturnDetailModel.SaleOrderDetailModel.ProductModel.
                    returnDetailModel.Amount = returnDetailModel.Price * returnDetailModel.ReturnQty;
                    base_SaleOrderDetailModel saleOrderDetail = SelectedSaleOrder.SaleOrderShippedCollection.SingleOrDefault(x => x.Resource.ToString().Equals(returnDetailModel.OrderDetailResource));
                    if (saleOrderDetail != null && SelectedSaleOrder != null)
                    {
                        decimal TotalItemReturn = SelectedSaleOrder.ReturnModel.ReturnDetailCollection.Where(x => !x.IsTemporary && x.OrderDetailResource.Equals(returnDetailModel.OrderDetailResource)).Sum(x => x.ReturnQty);
                        var remainQuantity = SelectedSaleOrder.SaleOrderShippedCollection.Where(x => x.Resource.ToString().Equals(returnDetailModel.OrderDetailResource)).Sum(x => x.PickQty) - TotalItemReturn;
                        saleOrderDetail.QtyAfterRerturn = remainQuantity;

                        CalcReturnQtyBaseUnit(returnDetailModel, saleOrderDetail);
                        CheckReturned(returnDetailModel);
                    }
                    break;

                case "Amount":
                    returnDetailModel.VAT = _saleOrderRepository.CalculateReturnDetailTax(returnDetailModel, SelectedSaleOrder);
                    CalcReturnDetailRewardRedeem(SelectedSaleOrder, returnDetailModel);
                    CalcReturnDetailSubTotal(SelectedSaleOrder, returnDetailModel);
                    CalculateReturnSubtotal(SelectedSaleOrder);
                    break;

                case "IsReturned":
                    if (returnDetailModel.IsReturned)
                    {
                        if (!returnDetailModel.HasError)
                        {
                            //"Are you sure you return this item ?"
                            MessageBoxResultCustom result = MsgControl.ShowWarning(Language.GetMsg("M110"), Language.Warning, MessageBoxButtonCustom.OKCancel);
                            if (result == MessageBoxResultCustom.Cancel)
                            {
                                App.Current.Dispatcher.BeginInvoke(new Action(() =>
                                {
                                    returnDetailModel.IsReturned = false;
                                }), System.Windows.Threading.DispatcherPriority.Background);
                            }
                        }
                        else
                        {
                            App.Current.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                returnDetailModel.IsReturned = false;

                                MsgControl.ShowWarning(Language.GetMsg("M111"), Language.GetMsg("POSCaption"), MessageBoxButtonCustom.OK);
                            }), System.Windows.Threading.DispatcherPriority.Background);
                        }
                        SelectedSaleOrder.ReturnModel.RaiseRefundAccepted();
                    }
                    break;

            }
        }

        private void CalcReturnQtyBaseUnit(base_ResourceReturnDetailModel resourceReturnDetailModel, base_SaleOrderDetailModel saleOrderDetail)
        {
            //Get BaseUnit & convert value to Qty to BaseUnit for ReturnQtyUOM

            base_ProductUOMModel productUomModel = null;
            if (saleOrderDetail.ProductUOMCollection != null)
                productUomModel = saleOrderDetail.ProductUOMCollection.Single(x => !saleOrderDetail.ProductModel.IsCoupon && x.UOMId.Equals(saleOrderDetail.UOMId));
            if (productUomModel != null)
            {
                decimal quantityBaseUnit = productUomModel.BaseUnitNumber * resourceReturnDetailModel.ReturnQty;
                //Update To ReturnQtyUOM
                resourceReturnDetailModel.ReturnQtyUOM = quantityBaseUnit;
            }
            else
                resourceReturnDetailModel.ReturnQtyUOM = resourceReturnDetailModel.ReturnQty;
        }

        private void ReturnModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base_ResourceReturnModel returnModel = sender as base_ResourceReturnModel;
            switch (e.PropertyName)
            {
                case "TotalRefund":
                    returnModel.CalcBalance(SelectedSaleOrder.TotalPaid);
                    break;
                case "SubTotal":
                    returnModel.CalcReturnFee();
                    returnModel.CalcBalance(SelectedSaleOrder.TotalPaid);
                    break;
                case "ReturnFee":
                    //returnModel.SetRefundedFeePercent();
                    returnModel.CalcBalance(SelectedSaleOrder.TotalPaid);
                    break;
                case "ReturnFeePercent":
                    returnModel.CalcBalance(SelectedSaleOrder.TotalPaid);
                    break;
            }
        }

        private void ReturnDetailCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            base_ResourceReturnDetailModel resourceReturnDetail;
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var item in e.NewItems)
                {
                    resourceReturnDetail = item as base_ResourceReturnDetailModel;
                    resourceReturnDetail.ReturnedDate = DateTime.Now;
                    resourceReturnDetail.IsTemporary = true;
                    resourceReturnDetail.IsDirty = false;
                    resourceReturnDetail.PropertyChanged += ResourceReturnDetailModel_PropertyChanged;
                }
                CheckReturned();
                SelectedSaleOrder.ReturnModel.RaiseRefundAccepted();
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var item in e.OldItems)
                {
                    resourceReturnDetail = item as base_ResourceReturnDetailModel;
                    resourceReturnDetail.PropertyChanged -= ResourceReturnDetailModel_PropertyChanged;
                }
                //CheckReturned();
            }
        }

        #endregion

        #region Total
        protected decimal _total;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the Total</param>
        /// </summary>
        public decimal Total
        {
            get
            {
                return this._total;
            }
            set
            {
                if (this._total != value)
                {
                    this._total = value;
                    OnPropertyChanged(() => Total);
                }
            }
        }
        #endregion

        #region Paid
        protected decimal _paid;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the Paid</param>
        /// </summary>
        public decimal Paid
        {
            get
            {
                return this._paid;
            }
            set
            {
                if (this._paid != value)
                {
                    this._paid = value;
                    OnPropertyChanged(() => Paid);
                }
            }
        }
        #endregion

        #region Balance
        protected decimal _balance;
        /// <summary>
        /// Property Model
        /// <param>Gets or sets the Balance</param>
        /// </summary>
        public decimal Balance
        {
            get
            {
                return this._balance;
            }
            set
            {
                if (this._balance != value)
                {
                    this._balance = value;
                    OnPropertyChanged(() => Balance);

                }
            }
        }
        #endregion

        #region Override Methods

        public override void LoadData()
        {
            //Flag When Existed view Call LoadDynamicData Data
            if (_viewExisted)
                LoadDynamicData();
            Expression<Func<base_SaleOrder, bool>> predicate = PredicateBuilder.True<base_SaleOrder>();
            predicate = predicate.And(x => ((x.RefundedAmount != null && x.Balance > 0 && x.Paid + x.Balance - (x.RefundedAmount) != x.Total)) || (x.Balance > 0 && x.RefundedAmount == 0));
            LoadDataByPredicate(predicate);
            _viewExisted = true;
        }

        protected override bool OnViewChangingCommandCanExecute(bool isClosing)
        {
            if (IsBusy)
                return false;
            return ChangeViewExecute(isClosing);
        }

        /// <summary>
        /// Change view from Ribbon
        /// </summary>
        /// <param name="isList"></param>
        public override void ChangeSearchMode(bool isList, object param = null)
        {
            if (param == null)
            {
                if (ChangeViewExecute(null))
                {
                    if (isList)
                    {
                        IsSearchMode = true;
                    }
                    else
                    {
                        CreateNewSaleOrder();
                        IsSearchMode = false;
                        IsForceFocused = true;
                    }
                }
            }
            else
            {
                if (param is ComboItem)
                {
                    SaleOrderSelectedTab = SaleOrderTab.Order;
                    ComboItem cmbValue = param as ComboItem;
                    if (cmbValue.Text.Equals("Quotation") || cmbValue.Text.Equals(MarkType.WorkOrder.ToDescription()))
                    {
                        IsSearchMode = false;
                        SaleOrderId = Convert.ToInt32(cmbValue.Detail);
                        _selectedSaleOrder = null;
                    }
                    else if (cmbValue.Text.Equals("UnLock"))
                    {
                        SaleOrderId = Convert.ToInt32(cmbValue.Detail);
                        OnPropertyChanged(() => SelectedSaleOrder);
                        IsSearchMode = false;
                    }
                    else if (cmbValue.Text.Equals("Customer"))//Create SaleOrder With Customer
                    {
                        CreateNewSaleOrder();
                        long customerId = Convert.ToInt64(cmbValue.Detail);
                        SelectedCustomer = CustomerCollection.SingleOrDefault(x => x.Id.Equals(customerId));
                        this.IsSearchMode = false;
                    }
                    else if (cmbValue.Text.Equals("SaleOrderReturn.New"))
                    {
                        CreateNewSaleOrder();
                        this.IsSearchMode = false;
                        SaleOrderSelectedTab = SaleOrderTab.Return;
                    }
                    else if (cmbValue.Text.Equals("SaleOrderReturn.SaleOrderList"))
                    {
                        this.IsSearchMode = true;
                    }
                    else if (cmbValue.Text.Equals("SaleOrderReturn.SelectedItem"))
                    {
                        SaleOrderId = Convert.ToInt32(cmbValue.Detail);
                        _selectedSaleOrder = null;
                        this.IsSearchMode = false;
                        SaleOrderSelectedTab = SaleOrderTab.Return;
                    }
                    ////when View Active =>  LoadData methods will be loaded again
                    //bool saleOrderActived = (_ownerViewModel as MainViewModel).IsActiveView("SalesOrder");
                    //if (saleOrderActived)
                    //{
                    //    LoadData();
                    //}

                }
                else //Create saleOrder with ProductCollection
                {
                    CreateNewSaleOrder();
                    IEnumerable<base_ProductModel> productCollection = param as IEnumerable<base_ProductModel>;
                    CreateSaleOrderDetailWithProducts(productCollection);
                    this.IsSearchMode = false;
                }
            }
        }

        protected override void SelectedSaleOrderChanged()
        {
            base.SelectedSaleOrderChanged();
            if (SelectedSaleOrder != null)
            {
                SelectedSaleOrder.PropertyChanged -= new PropertyChangedEventHandler(SelectedSaleOrder_PropertyChanged);
                SelectedSaleOrder.PropertyChanged += new PropertyChangedEventHandler(SelectedSaleOrder_PropertyChanged);
            }
        }

        #endregion

        #region Permission

        #region Properties

        private bool _allowSOShipping = true;
        /// <summary>
        /// Gets or sets the AllowSOShipping.
        /// </summary>
        public bool AllowSOShipping
        {
            get
            {
                if (SelectedSaleOrder == null)
                    return _allowSOShipping;
                return _allowSOShipping && SelectedSaleOrder.ShipProcess;
            }
            set
            {
                if (_allowSOShipping != value)
                {
                    _allowSOShipping = value;
                    OnPropertyChanged(() => AllowSOShipping);
                }
            }
        }

        private bool _allowSOReturn = true;
        /// <summary>
        /// Gets or sets the AllowSOReturn.
        /// </summary>
        public bool AllowSOReturn
        {
            get
            {
                if (SelectedSaleOrder == null)
                    return _allowSOReturn;
                return _allowSOReturn && SelectedSaleOrder.ShipProcess;
            }
            set
            {
                if (_allowSOReturn != value)
                {
                    _allowSOReturn = value;
                    OnPropertyChanged(() => AllowSOReturn);
                }
            }
        }

        #endregion

        /// <summary>
        /// Get permissions
        /// </summary>
        public override void GetPermission()
        {
            if (!IsAdminPermission && !IsFullPermission)
            {
                // Get all user rights
                IEnumerable<string> userRightCodes = Define.USER_AUTHORIZATION.Select(x => x.Code);

                // Get sale order shipping permission
                AllowSOShipping = userRightCodes.Contains("SO100-04-11");

                // Get sale order return permission
                AllowSOReturn = userRightCodes.Contains("SO100-04-05");

                // Get add/copy customer permission
                AllowAddCustomer = userRightCodes.Contains("SO100-01-01");

                // Get delete product in sale order permission
                AllowDeleteProduct = userRightCodes.Contains("SO100-04-13");
            }
        }

        #endregion
    }
}