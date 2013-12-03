using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using CPC.Helper;
using CPC.POS.Database;
using CPC.POS.Model;
using CPC.POS.Repository;
using CPC.POS.View;
using CPC.Toolkit.Base;
using CPC.Toolkit.Command;
using CPCToolkitExt.DataGridControl;
using CPCToolkitExtLibraries;
using System.Collections.Specialized;
using MessageBoxControl;

namespace CPC.POS.ViewModel
{
    public class QuotationViewModel : OrderViewModel
    {
        #region Define
        //Respository
        private base_ResourcePaymentRepository _paymentRepository = new base_ResourcePaymentRepository();
        private base_ProductStoreRepository _productStoreRespository = new base_ProductStoreRepository();
        #endregion

        #region Constructors

        public QuotationViewModel()
            : base()
        {

        }

        public QuotationViewModel(bool isList)
            : base()
        {
            LoadDynamicData();
            ChangeSearchMode(isList, null);
            // Get permission
            GetPermission();
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
                    || (SelectedSaleOrder.PaymentCollection != null && SelectedSaleOrder.PaymentCollection.Any(x => x.IsDirty))
                    || (SelectedSaleOrder.BillAddressModel != null && SelectedSaleOrder.BillAddressModel.IsDirty)
                    || (SelectedSaleOrder.ShipAddressModel != null && SelectedSaleOrder.ShipAddressModel.IsDirty);
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

        //Static Property

        #region CustomerFieldCollection
        ///// <summary>
        ///// Gets or sets the CustomerFieldCollection for Autocomplete Control
        ///// </summary>
        public DataSearchCollection CustomerFieldCollection { get; set; }
        #endregion

        #region ProductFieldCollection
        private DataSearchCollection _productFieldCollection;
        /// <summary>
        /// Gets or sets the ProductFieldCollection.
        /// </summary>
        public DataSearchCollection ProductFieldCollection
        {
            get { return _productFieldCollection; }
            set
            {
                if (_productFieldCollection != value)
                {
                    _productFieldCollection = value;
                    OnPropertyChanged(() => ProductFieldCollection);
                }
            }
        }
        #endregion

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

        #endregion

        #region Commands Methods

        #region NewCommand

        protected override bool OnNewCommandCanExecute(object param)
        {
            return base.OnNewCommandCanExecute(param);
        }
        protected override void OnNewCommandExecute(object param)
        {
            base.OnNewCommandExecute(param);

            if (ChangeViewExecute(null))
            {
                _isForceFocused = false;
                CreateNewSaleOrder();
                IsSearchMode = false;
                IsForceFocused = true;
            }
        }
        #endregion

        #region Save Command

        protected override bool OnSaveCommandCanExecute(object param)
        {
            return IsDirty && IsValid && IsOrderValid;
        }

        protected override void OnSaveCommandExecute(object param)
        {
            SaveQuotation(false);
        }

        #endregion

        #region DeleteCommand

        protected override bool OnDeleteCommandCanExecute(object param)
        {
            if (SelectedSaleOrder == null)
                return false;
            return !SelectedSaleOrder.IsNew;
        }

        protected override void OnDeleteCommandExecute(object param)
        {
            if (SelectedSaleOrder != null)
            {
                MessageBoxResultCustom result = MsgControl.ShowWarning(Language.GetMsg("M103"), Language.GetMsg("DeleteCaption"), MessageBoxButtonCustom.YesNo);
                if (result.Is(MessageBoxResultCustom.Yes))
                {
                    SelectedSaleOrder.IsPurge = true;
                    SaveQuotation(false);
                    this.SaleOrderCollection.Remove(SelectedSaleOrder);
                    TotalSaleOrder -= 1;
                    _selectedSaleOrder = null;
                    IsSearchMode = true;
                }
            }
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

        public RelayCommand<object> DoubleClickViewCommand { get; private set; }

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
                SelectedSaleOrder = param as base_SaleOrderModel;

                SetSaleOrderRelation(SelectedSaleOrder);
                SelectedSaleOrder.RaiseTotalPaid();

                //Check customer is a Active or Deactive 
                //if Deactive => need to get customer deactive store in CustomerCollection.DeletedItems && add to collection using for choice customer
                if (!CustomerCollection.Any(x => x.Resource.Equals(SelectedSaleOrder.GuestModel.Resource)))
                {
                    base_GuestModel customerDeActived = CustomerCollection.DeletedItems.SingleOrDefault(x => x.Resource.Equals(SelectedSaleOrder.GuestModel.Resource));
                    CustomerCollection.Add(customerDeActived);
                    CustomerCollection.DeletedItems.Remove(customerDeActived);
                }

                //Set for selectedCustomer
                _selectedCustomer = SelectedSaleOrder.GuestModel;
                OnPropertyChanged(() => SelectedCustomer);

                SetAllowChangeOrder(SelectedSaleOrder);
                SelectedSaleOrder.IsDirty = false;

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

        #region LoadDatByStepCommand

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
            Expression<Func<base_SaleOrder, bool>> predicate = PredicateBuilder.True<base_SaleOrder>();
            if (!string.IsNullOrWhiteSpace(FilterText))//Load Step Current With Search Current with Search
                predicate = CreatePredicateWithConditionSearch(Keyword);
            LoadDataByPredicate(predicate, false, SaleOrderCollection.Count);
        }
        #endregion

        #region PaymentCommand
        /// <summary>
        /// Gets the Payment Command.
        /// <summary>

        public RelayCommand<object> PaymentCommand { get; private set; }


        /// Method to check whether the Payment command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnPaymentCommandCanExecute(object param)
        {
            if (SelectedSaleOrder == null)
                return false;

            return !string.IsNullOrWhiteSpace(SelectedSaleOrder.CustomerResource) && SelectedSaleOrder.Paid == 0 && SelectedSaleOrder.SubTotal > SelectedSaleOrder.Deposit && (SelectedSaleOrder.OrderStatus.Equals((short)SaleOrderStatus.Quote));

        }

        /// <summary>
        /// Method to invoke when the Payment command is executed.
        /// </summary>
        private void OnPaymentCommandExecute(object param)
        {
            DepositProcess();

        }
        #endregion

        //Main Grid
        #region DeleteItemsCommand
        /// <summary>
        /// Gets the DeleteItems Command.
        /// <summary>

        public RelayCommand<object> DeleteItemsCommand { get; private set; }

        /// <summary>
        /// Method to check whether the DeleteItems command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnDeleteItemsCommandCanExecute(object param)
        {
            if (param == null)
                return false;
            DataGridControl datagrid = param as DataGridControl;
            object collectionDelete = datagrid.SelectedItems.Cast<object>();

            return datagrid.SelectedItems.Count > 0
                && !(collectionDelete as ObservableCollection<object>).Cast<base_SaleOrderModel>().Any(x => !x.OrderStatus.Equals((short)SaleOrderStatus.Quote));

        }


        /// <summary>
        /// Method to invoke when the DeleteItems command is executed.
        /// </summary>
        private void OnDeleteItemsCommandExecute(object param)
        {
            DataGridControl datagrid = param as DataGridControl;
            object collectionDelete = datagrid.SelectedItems.Cast<object>();
            DeleteItemsSaleOrder(collectionDelete);
        }


        /// <summary>
        /// Gets the DeleteItems Command.
        /// <summary>

        public RelayCommand<object> DeleteItemsWithKeyCommand { get; private set; }

        /// <summary>
        /// Method to check whether the DeleteItems command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnDeleteItemsWithKeyCommandCanExecute(object param)
        {
            return true;
        }


        /// <summary>
        /// Method to invoke when the DeleteItems command is executed.
        /// </summary>
        private void OnDeleteItemsWithKeyCommandExecute(object param)
        {
            if (param == null || (param as ObservableCollection<object>).Cast<base_SaleOrderModel>().Any(x => !x.OrderStatus.Equals((short)SaleOrderStatus.Quote)))
            {
                MsgControl.ShowWarning(Language.GetMsg("SO_Message_CouldDeleteItemIsQuote"), Language.Information, MessageBoxButtonCustom.OK);
                return;
            }

            DeleteItemsSaleOrder(param);
        }

        /// <summary>
        /// Confirm & delete saleorders
        /// </summary>
        /// <param name="param"></param>
        private void DeleteItemsSaleOrder(object param)
        {
            MessageBoxResultCustom msgResult = MsgControl.ShowWarning(Language.GetMsg("M103"), Language.Warning, MessageBoxButtonCustom.YesNo);
            if (msgResult.Is(MessageBoxResultCustom.No))
                return;

            foreach (base_SaleOrderModel saleOrderModel in (param as ObservableCollection<object>).Cast<base_SaleOrderModel>().ToList())
            {
                saleOrderModel.IsPurge = true;
                TotalSaleOrder -= 1;
                saleOrderModel.ToEntity();
                _saleOrderRepository.Commit();
                SaleOrderCollection.Remove(saleOrderModel);
            }

            if (SelectedSaleOrder != null)
                _selectedSaleOrder = null;
        }
        #endregion

        #region DuplicateItemCommand
        /// <summary>
        /// Gets the DuplicateItem Command.
        /// <summary>

        public RelayCommand<object> DuplicateItemCommand { get; private set; }

        /// <summary>
        /// Method to check whether the DuplicateItem command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnDuplicateItemCommandCanExecute(object param)
        {
            if (param == null)
                return false;
            DataGridControl datagrid = param as DataGridControl;
            return datagrid.SelectedItems.Count == 1;
        }


        /// <summary>
        /// Method to invoke when the DuplicateItem command is executed.
        /// </summary>
        private void OnDuplicateItemCommandExecute(object param)
        {
            DataGridControl datagrid = param as DataGridControl;
            base_SaleOrderModel saleOrderSource = datagrid.SelectedItem as base_SaleOrderModel;
            CreateNewSaleOrder();
            SelectedSaleOrder.CopyFrom(saleOrderSource);
            SelectedSaleOrder.CalcBalance();
            SetSaleOrderToModel(SelectedSaleOrder);
            //Check not set to collection
            if (saleOrderSource.SaleOrderDetailCollection == null && saleOrderSource.base_SaleOrder.base_SaleOrderDetail.Any())
                SetSaleOrderRelation(saleOrderSource);

            if (saleOrderSource.SaleOrderDetailCollection != null)
            {
                foreach (base_SaleOrderDetailModel saleOrderDetailModel in saleOrderSource.SaleOrderDetailCollection.Where(x => string.IsNullOrWhiteSpace(x.ParentResource)))
                {
                    string parentResource = CloneSaleOrderDetailModel(saleOrderDetailModel);
                    //Get Child item follow Resource parent
                    var childInGroup = saleOrderSource.SaleOrderDetailCollection.Where(x => x.ParentResource.Equals(saleOrderDetailModel.Resource.ToString()));
                    if (childInGroup.Any())//Is a group 
                    {
                        foreach (base_SaleOrderDetailModel saleOrderDetaiInGrouplModel in childInGroup)
                        {
                            CloneSaleOrderDetailModel(saleOrderDetaiInGrouplModel, parentResource);
                        }
                    }
                }
            }

            SaveQuotation(false);

            _selectedCustomer = null;
            //Set for selectedCustomer
            _selectedCustomer = CustomerCollection.SingleOrDefault(x => x.Resource.ToString().Equals(SelectedSaleOrder.CustomerResource));
            OnPropertyChanged(() => SelectedCustomer);
            SetAllowChangeOrder(SelectedSaleOrder);
            SelectedSaleOrder.IsDirty = false;
            IsSearchMode = false;

        }

        /// <summary>
        /// Clone & AddNew SaleOrderDetail
        /// </summary>
        /// <param name="saleOrderDetailModel"></param>
        /// <returns></returns>
        private string CloneSaleOrderDetailModel(base_SaleOrderDetailModel saleOrderDetailModel, string parentResource = "")
        {
            base_SaleOrderDetailModel newSaleOrderDetailModel = new base_SaleOrderDetailModel();
            newSaleOrderDetailModel.Resource = Guid.NewGuid();
            newSaleOrderDetailModel.ParentResource = parentResource;
            newSaleOrderDetailModel.CopyFrom(saleOrderDetailModel);
            newSaleOrderDetailModel.CalcDueQty();

            //Set Item type Sale Order to know item is group/child or none
            if (newSaleOrderDetailModel.ProductModel.ItemTypeId.Equals((short)ItemTypes.Group))//Parent Of Group
                newSaleOrderDetailModel.ItemType = 1;
            else if (!string.IsNullOrWhiteSpace(newSaleOrderDetailModel.ParentResource))//Child item of group
                newSaleOrderDetailModel.ItemType = 2;
            else
                newSaleOrderDetailModel.ItemType = 0;

            SelectedSaleOrder.SaleOrderDetailCollection.Add(newSaleOrderDetailModel);
            _saleOrderRepository.CalcOnHandStore(SelectedSaleOrder, newSaleOrderDetailModel);
            return newSaleOrderDetailModel.Resource.ToString();
        }
        #endregion

        #region EditItemCommand
        /// <summary>
        /// Gets the EditItem Command.
        /// <summary>

        public RelayCommand<object> EditItemCommand { get; private set; }


        /// <summary>
        /// Method to check whether the EditItem command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnEditItemCommandCanExecute(object param)
        {
            if (param == null)
                return false;
            DataGridControl datagrid = param as DataGridControl;
            return datagrid.SelectedItems.Count == 1;
        }

        /// <summary>
        /// Method to invoke when the EditItem command is executed.
        /// </summary>
        private void OnEditItemCommandExecute(object param)
        {
            DataGridControl datagrid = param as DataGridControl;
            OnDoubleClickViewCommandExecute(datagrid.SelectedItem);
        }
        #endregion

        #region SaleOrderAdvanceSearchCommand
        /// <summary>
        /// Gets the SaleOrderAdvanceSearch Command.
        /// <summary>

        public RelayCommand<object> SaleOrderAdvanceSearchCommand { get; private set; }


        /// <summary>
        /// Method to check whether the SaleOrderAdvanceSearch command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnSaleOrderAdvanceSearchCommandCanExecute(object param)
        {
            return true;
        }


        /// <summary>
        /// Method to invoke when the SaleOrderAdvanceSearch command is executed.
        /// </summary>
        private void OnSaleOrderAdvanceSearchCommandExecute(object param)
        {
            OpenSOAdvanceSearch();
        }


        #endregion

        #region ConvertToSaleOrderCommand

        /// <summary>
        /// Gets the ConvertToSaleOrder Command.
        /// <summary>
        public RelayCommand<object> ConvertToSaleOrderCommand { get; private set; }

        /// <summary>
        /// Method to check whether the ConvertToSaleOrder command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnConvertToSaleOrderCommandCanExecute(object param)
        {
            if (SelectedSaleOrder == null)
                return false;

            return !SelectedSaleOrder.IsNew && IsValid && IsOrderValid && AllowAddSaleOrder &&
                (SelectedSaleOrder.OrderStatus.Equals((short)SaleOrderStatus.Quote));
        }

        /// <summary>
        /// Method to invoke when the ConvertToSaleOrder command is executed.
        /// </summary>
        private void OnConvertToSaleOrderCommandExecute(object param)
        {
            App.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                SelectedSaleOrder.OrderStatus = (short)SaleOrderStatus.Open;
                SelectedSaleOrder.IsConverted = true;

                SaveQuotation(true/*Update Product Quantity*/);
                IsSearchMode = true;
                ComboItem cmbValue = new ComboItem();
                cmbValue.Text = "Quotation";
                cmbValue.Detail = SelectedSaleOrder.Id;
                (_ownerViewModel as MainViewModel).OpenViewExecute("SalesOrder", cmbValue);
            }), System.Windows.Threading.DispatcherPriority.Background);

        }

        #endregion

        #region ConvertItemToSaleOrderCommand

        /// <summary>
        /// Gets the ConvertItemToSaleOrder Command.
        /// Using on selected in datagrid
        /// <summary>
        public RelayCommand<object> ConvertItemToSaleOrderCommand { get; private set; }

        /// <summary>
        /// Method to check whether the ConvertItemToSaleOrder command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnConvertItemToSaleOrderCommandCanExecute(object param)
        {
            if (param == null)
                return false;
            DataGridControl datagrid = param as DataGridControl;
            return datagrid.SelectedItems.Count == 1 && AllowAddSaleOrder &&
                ((datagrid.SelectedItem as base_SaleOrderModel).OrderStatus == (short)SaleOrderStatus.Quote);
        }

        /// <summary>
        /// Method to invoke when the ConvertItemToSaleOrder command is executed.
        /// </summary>
        private void OnConvertItemToSaleOrderCommandExecute(object param)
        {
            App.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                DataGridControl datagrid = param as DataGridControl;
                _selectedSaleOrder = datagrid.SelectedItem as base_SaleOrderModel;
                _selectedSaleOrder.IsConverted = true;
                _selectedSaleOrder.OrderStatus = (short)SaleOrderStatus.Open;
                SetSaleOrderRelation(SelectedSaleOrder);
                SaveQuotation(true);
                IsSearchMode = true;
                ComboItem cmbValue = new ComboItem();
                cmbValue.Text = "Quotation";
                cmbValue.Detail = SelectedSaleOrder.Id;
                (_ownerViewModel as MainViewModel).OpenViewExecute("SalesOrder", cmbValue);
            }), System.Windows.Threading.DispatcherPriority.Background);

        }

        #endregion

        #region DepositHistory Command
        /// <summary>
        /// Gets the DepositHistory Command.
        /// <summary>

        public RelayCommand<object> DepositHistoryCommand { get; private set; }


        /// <summary>
        /// Method to check whether the DepositHistory command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnDepositHistoryCommandCanExecute(object param)
        {
            return true;
        }


        /// <summary>
        /// Method to invoke when the DepositHistory command is executed.
        /// </summary>
        private void OnDepositHistoryCommandExecute(object param)
        {
            DepositHistoryProcess();
        }

        #endregion

        #region Refund Command
        /// <summary>
        /// Gets the Refund Command.
        /// <summary>

        public RelayCommand<object> RefundCommand { get; private set; }

        /// <summary>
        /// Method to check whether the Refund command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnRefundCommandCanExecute(object param)
        {
            return SelectedSaleOrder != null && SelectedSaleOrder.Deposit > 0 && (SelectedSaleOrder.OrderStatus.Equals((short)SaleOrderStatus.Quote));
        }


        /// <summary>
        /// Method to invoke when the Refund command is executed.
        /// </summary>
        private void OnRefundCommandExecute(object param)
        {
            string msg = string.Format(Language.GetMsg("SO_Message_ConfirmRefundAllDeposit").Replace("\\n", "\n"), string.Format(Define.ConverterCulture, Define.CurrencyFormat, SelectedSaleOrder.Deposit));
            MessageBoxResultCustom result = MsgControl.ShowWarning(msg, "Refund", MessageBoxButtonCustom.YesNo);
            if (result.Equals(MessageBoxResultCustom.Yes))
            {
                if (SelectedSaleOrder.PaymentCollection == null)
                    SelectedSaleOrder.PaymentCollection = new ObservableCollection<base_ResourcePaymentModel>();
                base_ResourcePaymentModel refundPaymentModel = new base_ResourcePaymentModel()
                {
                    IsDeposit = true,
                    DocumentResource = SelectedSaleOrder.Resource.ToString(),
                    DocumentNo = SelectedSaleOrder.SONumber,
                    DateCreated = DateTime.Now,
                    UserCreated = Define.USER != null ? Define.USER.LoginName : string.Empty,
                    Resource = Guid.NewGuid(),
                    TotalAmount = SelectedSaleOrder.SubTotal,
                    TotalPaid = -SelectedSaleOrder.Deposit.Value

                };
                if (Define.CONFIGURATION.DefaultCashiedUserName.HasValue && Define.CONFIGURATION.DefaultCashiedUserName.Value)
                    refundPaymentModel.Cashier = Define.USER.LoginName;
                SelectedSaleOrder.PaymentCollection.Add(refundPaymentModel);
                SelectedSaleOrder.Deposit = SelectedSaleOrder.PaymentCollection.Where(x => x.IsDeposit.Value).Sum(x => x.TotalPaid);
            }
        }
        #endregion

        #endregion

        #region Private Methods


        /// <summary>
        /// LoadDataByPredicate
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
            Console.WriteLine("Searching");
            BackgroundWorker bgWorker = new BackgroundWorker { WorkerReportsProgress = true };
            if (currentIndex == 0)
                SaleOrderCollection.Clear();
            bgWorker.DoWork += (sender, e) =>
            {
                IsBusy = true;
                predicate = predicate.And(x => !x.IsPurge && !x.IsLocked);



                string quotationMark = MarkType.Quotation.ToDescription();
                predicate = predicate.And(x => x.Mark == quotationMark);

                //Cout all SaleOrder in Data base show on grid
                lock (UnitOfWork.Locker)
                {
                    TotalSaleOrder = _saleOrderRepository.GetIQueryable(predicate).Count();

                    //Get data with range
                    IList<base_SaleOrder> saleOrders = _saleOrderRepository.GetRange<DateTime>(currentIndex -_numberNewItem, NumberOfDisplayItems, x => x.OrderDate.Value, predicate);
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
                if (_viewExisted && !IsSearchMode && SelectedSaleOrder != null && SaleOrderCollection.Any() && !SelectedSaleOrder.IsNew) //Item is selected
                {
                    SelectedSaleOrder = SaleOrderCollection.SingleOrDefault(x => x.Id.Equals(SelectedSaleOrder.Id));
                    SetSaleOrderRelation(SelectedSaleOrder, true);
                }
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
            if (!string.IsNullOrWhiteSpace(keyword) && SearchOption > 0)
            {
                if (SearchOption.Has(SearchOptions.SoNum))
                {
                    predicate = predicate.And(x => x.SONumber.ToLower().Contains(keyword.ToLower()));
                }
                if (SearchOption.Has(SearchOptions.Customer))
                {
                    var customerList = CustomerCollection.Where(y => y.LastName.ToLower().Contains(keyword.ToLower()) || y.FirstName.ToLower().Contains(keyword.ToLower())).Select(x => x.Resource.ToString());
                    predicate = predicate.And(x => customerList.Contains(x.CustomerResource));
                }
            }
            return predicate;
        }

        /// <summary>
        /// Set CustomerRewardCollection for RewardMember
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
                    foreach (base_GuestReward guestReward in saleOrderModel.GuestModel.base_Guest.base_GuestReward.Where(x => x.GuestId.Equals(saleOrderModel.GuestModel.Id) && !x.IsApply && x.ActivedDate.Value <= DateTime.Today && (!x.ExpireDate.HasValue || x.ExpireDate.HasValue && DateTime.Today <= x.ExpireDate.Value)))
                    {
                        saleOrderModel.GuestModel.GuestRewardCollection.Add(new base_GuestRewardModel(guestReward));
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="saleOrderModel"></param>
        /// <param name="isForce"></param>
        protected override void SetSaleOrderRelation(base_SaleOrderModel saleOrderModel, bool isForce = false)
        {
            base.SetSaleOrderRelation(saleOrderModel, isForce);

            LoadPaymentCollection(saleOrderModel);

            saleOrderModel.RaiseAnyShipped();
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
        /// Insert New sale order
        /// </summary>
        private void InsertQuotation()
        {
            if (SelectedSaleOrder.IsNew)
            {
                UpdateCustomerAddress(SelectedSaleOrder.BillAddressModel);
                SelectedSaleOrder.BillAddressId = SelectedSaleOrder.BillAddressModel.Id;
                UpdateCustomerAddress(SelectedSaleOrder.ShipAddressModel);
                SelectedSaleOrder.ShipAddressId = SelectedSaleOrder.ShipAddressModel.Id;
                //Sale Order Detail Model
                foreach (base_SaleOrderDetailModel saleOrderDetailModel in SelectedSaleOrder.SaleOrderDetailCollection)
                {
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
                SaleOrderCollection.Add(SelectedSaleOrder);
                _numberNewItem++;
            }
        }

        /// <summary>
        /// UpdateQuote
        /// </summary>
        /// <param name="UpdateQtyCustomer"></param>
        private void UpdateQuotation(bool UpdateQtyCustomer = false)
        {
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

                    _saleOrderDetailRepository.Delete(saleOrderDetailModel.base_SaleOrderDetail);
                }
                _saleOrderDetailRepository.Commit();
                SelectedSaleOrder.SaleOrderDetailCollection.DeletedItems.Clear();
            }

            //Sale Order Detail Model
            foreach (base_SaleOrderDetailModel saleOrderDetailModel in SelectedSaleOrder.SaleOrderDetailCollection)
            {
                if ((saleOrderDetailModel.Quantity != saleOrderDetailModel.base_SaleOrderDetail.Quantity || saleOrderDetailModel.UOMId != saleOrderDetailModel.base_SaleOrderDetail.UOMId) && UpdateQtyCustomer)// Convert To SO => Update Qty Onhand on Customer, because quoation is update qty customer in convert to sale order proccess
                {
                    _saleOrderRepository.UpdateCustomerQuantity(saleOrderDetailModel, SelectedSaleOrder.StoreCode, saleOrderDetailModel.Quantity);
                }

                saleOrderDetailModel.ToEntity();
                if (saleOrderDetailModel.IsNew)
                    SelectedSaleOrder.base_SaleOrder.base_SaleOrderDetail.Add(saleOrderDetailModel.base_SaleOrderDetail);
            }


            _productRepository.Commit();
            #endregion

            #region Payment
            SavePaymentCollection(SelectedSaleOrder);
            #endregion

            SelectedSaleOrder.UserUpdated = Define.USER != null ? Define.USER.LoginName : string.Empty;
            SelectedSaleOrder.ToEntity();
            _saleOrderRepository.Commit();

            //Set ID
            SelectedSaleOrder.ToModel();
            SelectedSaleOrder.EndUpdate();
            foreach (base_SaleOrderDetailModel saleOrderDetailModel in SelectedSaleOrder.SaleOrderDetailCollection)
            {
                saleOrderDetailModel.ToModel();
                saleOrderDetailModel.EndUpdate();
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
        }

        /// <summary>
        /// SalveQuote
        /// </summary>
        /// <returns></returns>
        private bool SaveQuotation(bool UpdateQtyCustomer = false)
        {
            bool result = false;
            try
            {
                UnitOfWork.BeginTransaction();
                if (SelectedSaleOrder.IsNew)
                    InsertQuotation();
                else
                    UpdateQuotation(UpdateQtyCustomer);

                UpdateCustomer(SelectedSaleOrder);
                UnitOfWork.CommitTransaction();
                result = true;
            }
            catch (Exception ex)
            {
                UnitOfWork.RollbackTransaction();
                result = false;
                _log4net.Error(ex);
                MsgControl.ShowWarning(ex.ToString(), "Thông báo", MessageBoxButtonCustom.OK);
            }
            return result;
        }


        //View Util
        /// <summary>
        /// Method check Item has edit & show message
        /// </summary>
        /// <returns></returns>
        private bool ChangeViewExecute(bool? isClosing)
        {
            bool result = true;
            if (this.IsDirty)
            {
                MessageBoxResultCustom msgResult = MessageBoxResultCustom.None;
                msgResult = MsgControl.ShowWarning(Language.GetMsg("M106"), Language.Information, MessageBoxButtonCustom.YesNoCancel);
                if (msgResult.Is(MessageBoxResultCustom.Yes))
                {
                    if (OnSaveCommandCanExecute(null))
                    {
                        result = SaveQuotation(true);
                    }
                    else //Has Error
                        result = false;
                }
                else if (msgResult.Is(MessageBoxResultCustom.No))
                {
                    if (SelectedSaleOrder.IsNew)
                    {
                        SelectedSaleOrder = null;
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


        //Get Set Value
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
            else if (saleOrderModel.Paid > 0)/*has paid*/
                this.IsAllowChangeOrder = false;
            else if (saleOrderModel.OrderStatus.Equals((short)SaleOrderStatus.PaidInFull)
                || saleOrderModel.OrderStatus.Equals((short)SaleOrderStatus.Close)
                || saleOrderModel.OrderStatus.Equals((short)SaleOrderStatus.Open)
                || saleOrderModel.OrderStatus.Equals((short)SaleOrderStatus.FullyShipped))
                this.IsAllowChangeOrder = false;
            else
                this.IsAllowChangeOrder = saleOrderModel.OrderStatus == (short)SaleOrderStatus.FullyShipped && Define.CONFIGURATION.IsAllowChangeOrder.Value;

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
        /// Deposite for Quotation
        /// </summary>
        private void DepositProcess()
        {
            if (SelectedSaleOrder.OrderStatus.Equals((short)SaleOrderStatus.Quote))
                SelectedSaleOrder.RewardAmount = SelectedSaleOrder.Total;

            decimal balance = SelectedSaleOrder.RewardAmount - SelectedSaleOrder.Deposit.Value;
            decimal depositTaken = SelectedSaleOrder.PaymentCollection.Where(x => x.IsDeposit.Value).Sum(x => x.TotalPaid);

            //Show Payment
            SalesOrderPaymenViewModel paymentViewModel = new SalesOrderPaymenViewModel(SelectedSaleOrder, balance, depositTaken);
            bool? dialogResult = _dialogService.ShowDialog<DepositPaymentView>(_ownerViewModel, paymentViewModel, Language.GetMsg("SO_Title_Deposit"));
            if (dialogResult == true)
            {
                if (Define.CONFIGURATION.DefaultCashiedUserName.HasValue && Define.CONFIGURATION.DefaultCashiedUserName.Value)
                    paymentViewModel.PaymentModel.Cashier = Define.USER.LoginName;

                if (SelectedSaleOrder.PaymentCollection == null)
                    SelectedSaleOrder.PaymentCollection = new ObservableCollection<base_ResourcePaymentModel>();

                SelectedSaleOrder.PaymentCollection.Add(paymentViewModel.PaymentModel);
                SelectedSaleOrder.Deposit = SelectedSaleOrder.PaymentCollection.Where(x => x.IsDeposit.Value).Sum(x => x.TotalPaid);
                SelectedSaleOrder.CalcSubTotal();
            }

            SelectedSaleOrder.PaymentProcess = SelectedSaleOrder.PaymentCollection.Any();
        }

        /// <summary>
        /// Open Sale Order or Quotation Advance Search
        /// </summary>
        private void OpenSOAdvanceSearch()
        {
            POSOAdvanceSearchViewModel viewModel = new POSOAdvanceSearchViewModel(false);
            bool? dialogResult = _dialogService.ShowDialog<POSOAdvanceSearchView>(_ownerViewModel, viewModel, Language.GetMsg("SO_Title_QuotationAdvanceSearch"));

            if (dialogResult == true)
            {
                Expression<Func<base_SaleOrder, bool>> predicate = viewModel.SOPredicate;
                LoadDataByPredicate(predicate, false, 0);
            }
        }

        /// <summary>
        /// {Quotation View}
        /// Show Popup Quoation Payment History
        /// </summary>
        private void DepositHistoryProcess()
        {
            SelectedSaleOrder.RewardAmount = SelectedSaleOrder.Total;
            decimal balance = SelectedSaleOrder.RewardAmount - (SelectedSaleOrder.Deposit.Value + SelectedSaleOrder.Paid);
            decimal depositTaken = SelectedSaleOrder.PaymentCollection.Where(x => x.IsDeposit.Value).Sum(x => x.TotalPaid);

            QuotationPaymentHistoryViewModel viewModel = new QuotationPaymentHistoryViewModel(SelectedSaleOrder, balance, depositTaken);

            string title = Language.GetMsg("SO_Tite_PaymentHistory");

            bool? dialogResult = _dialogService.ShowDialog<QuotationPaymentHistoryView>(_ownerViewModel, viewModel, title);

            if (dialogResult == true)
            {
                switch (viewModel.ViewActionType)
                {
                    case QuotationPaymentHistoryViewModel.PopupType.Deposit:
                        DepositProcess();
                        break;
                    case QuotationPaymentHistoryViewModel.PopupType.Refund:
                        break;
                }
            }
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
                decimal parentPickQty = totalOfPick * parentSaleOrderDetailModel.Quantity / totalQty;
                parentSaleOrderDetailModel.PickQty = Math.Round(parentPickQty, 2);
            }
        }


        #endregion

        #region PropertyChanged

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
                    saleOrderModel.RewardAmount = saleOrderModel.Total;
                    saleOrderModel.CalcBalance();
                    break;
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
                    break;
                case "OrderStatus":
                    SetAllowChangeOrder(saleOrderModel);
                    break;
                case "StoreCode":
                    StoreChanged();
                    break;
                case "TotalPaid":

                    break;


            }
        }

        protected override void SaleOrderDetailCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            base.SaleOrderDetailCollection_CollectionChanged(sender, e);


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

        protected override void SaleOrderDetailModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (BreakSODetailChange)
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

        #endregion

        #region Override Methods

        protected override void InitialCommand()
        {
            base.InitialCommand();

            ConvertToSaleOrderCommand = new RelayCommand<object>(OnConvertToSaleOrderCommandExecute, OnConvertToSaleOrderCommandCanExecute);
            ConvertItemToSaleOrderCommand = new RelayCommand<object>(OnConvertItemToSaleOrderCommandExecute, OnConvertItemToSaleOrderCommandCanExecute);
            DepositHistoryCommand = new RelayCommand<object>(OnDepositHistoryCommandExecute, OnDepositHistoryCommandCanExecute);
            RefundCommand = new RelayCommand<object>(OnRefundCommandExecute, OnRefundCommandCanExecute);
            DoubleClickViewCommand = new RelayCommand<object>(OnDoubleClickViewCommandExecute, OnDoubleClickViewCommandCanExecute);
            DeleteItemsCommand = new RelayCommand<object>(OnDeleteItemsCommandExecute, OnDeleteItemsCommandCanExecute);
            DeleteItemsWithKeyCommand = new RelayCommand<object>(OnDeleteItemsWithKeyCommandExecute, OnDeleteItemsWithKeyCommandCanExecute);
            DuplicateItemCommand = new RelayCommand<object>(OnDuplicateItemCommandExecute, OnDuplicateItemCommandCanExecute);
            EditItemCommand = new RelayCommand<object>(OnEditItemCommandExecute, OnEditItemCommandCanExecute);
            SaleOrderAdvanceSearchCommand = new RelayCommand<object>(OnSaleOrderAdvanceSearchCommandExecute, OnSaleOrderAdvanceSearchCommandCanExecute);
            PaymentCommand = new RelayCommand<object>(OnPaymentCommandExecute, OnPaymentCommandCanExecute);
        }

        public override void LoadData()
        {
            //IsBusy = false;
            App.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                //Flag When Existed view Call LoadStatic Data
                if (_viewExisted)
                    LoadDynamicData();

                Expression<Func<base_SaleOrder, bool>> predicate = PredicateBuilder.True<base_SaleOrder>();
                LoadDataByPredicate(predicate);
                _viewExisted = true;

            }), System.Windows.Threading.DispatcherPriority.Background);
        }

        protected override void LoadProducts()
        {
            IList<base_Product> products;

            if (Define.StoreCode == 0)
                products = _productRepository.GetAll(x => !x.IsPurge.Value);
            else
                products = _productRepository.GetAll(x => !x.IsPurge.Value && x.base_ProductStore.Any(y => y.StoreCode.Equals(Define.StoreCode)));

            if (ProductCollection == null)
                ProductCollection = new ObservableCollection<base_ProductModel>(products.Select(x => new base_ProductModel(x)).OrderBy(x => x.Id));
            else
            {
                foreach (base_Product product in products.OrderBy(x => x.Id))
                {
                    if (ProductCollection.Any(x => x.Resource.Equals(product.Resource)))
                    {
                        base_ProductModel productModel = ProductCollection.SingleOrDefault(x => x.Resource.Equals(product.Resource));
                        productModel.UpdateModel(product, true);
                    }
                    else
                    {
                        ProductCollection.Add(new base_ProductModel(product));
                    }
                }

                //Remove Item From Local collection if in db collection is not existed
                IList<Guid> itemReomoveList = ProductCollection.Where(x => !x.IsCoupon).Select(x => x.Resource).Except(products.Select(x => x.Resource)).ToList();
                if (itemReomoveList != null)
                {
                    foreach (Guid resource in itemReomoveList)
                    {
                        base_ProductModel itemRemoved = ProductCollection.SingleOrDefault(x => x.Resource.Equals(resource));
                        ProductCollection.Remove(itemRemoved);
                    }
                }
            }
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
                    if (!isList)
                    {
                        CreateNewSaleOrder();
                        IsSearchMode = false;
                    }
                    else
                        IsSearchMode = true;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void CreateExtentSaleOrder()
        {
            base.CreateExtentSaleOrder();
            _selectedCustomer = null;
            OnPropertyChanged(() => SelectedCustomer);
            _selectedSaleOrder.Mark = MarkType.Quotation.ToDescription();
            _selectedSaleOrder.OrderStatus = (short)SaleOrderStatus.Quote;
            _selectedSaleOrder.ItemStatus = Common.StatusSalesOrders.SingleOrDefault(x => Convert.ToInt16(x.ObjValue).Equals(_selectedSaleOrder.OrderStatus));
            SetAllowChangeOrder(_selectedSaleOrder);
            _selectedSaleOrder.SaleOrderDetailCollection.CollectionChanged += new NotifyCollectionChangedEventHandler(SaleOrderDetailCollection_CollectionChanged);
            _selectedSaleOrder.PropertyChanged += new PropertyChangedEventHandler(SelectedSaleOrder_PropertyChanged);
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void SelectedCustomerChanged()
        {
            base.SelectedCustomerChanged();

            if (SelectedSaleOrder != null)
            {
                SelectedSaleOrder.PropertyChanged -= new PropertyChangedEventHandler(SelectedSaleOrder_PropertyChanged);
                SelectedSaleOrder.PropertyChanged += new PropertyChangedEventHandler(SelectedSaleOrder_PropertyChanged);
            }
        }


        /// <summary>
        /// Open Popup Search Product Advanced
        /// </summary>
        protected override void SearchProductAdvance()
        {
            ProductSearchViewModel productSearchViewModel = new ProductSearchViewModel(false);
            bool? dialogResult = _dialogService.ShowDialog<ProductSearchView>(_ownerViewModel, productSearchViewModel, Language.GetMsg("SO_Title_SearchProduct"));
            if (dialogResult == true)
            {
                CreateSaleOrderDetailWithProducts(productSearchViewModel.SelectedProducts);
            }
        }
        #endregion

        #region Permission

        #region Properties

        private bool _allowConvertToSO = true;
        /// <summary>
        /// Gets or sets the AllowConvertToSO.
        /// </summary>
        public bool AllowConvertToSO
        {
            get { return _allowConvertToSO; }
            set
            {
                if (_allowConvertToSO != value)
                {
                    _allowConvertToSO = value;
                    OnPropertyChanged(() => AllowConvertToSO);
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

                // Get sale quotation permission
                AllowConvertToSO = userRightCodes.Contains("SO100-03-08");

                // Union add/copy sale order and allow convert to sale order permission
                AllowAddSaleOrder = userRightCodes.Contains("SO100-04-02") && AllowConvertToSO;

                // Get add/copy customer permission
                AllowAddCustomer = userRightCodes.Contains("SO100-01-01");


                // Get delete product in quotation permission
                AllowDeleteProduct = userRightCodes.Contains("SO100-03-07");
            }
        }

        #endregion
    }
}