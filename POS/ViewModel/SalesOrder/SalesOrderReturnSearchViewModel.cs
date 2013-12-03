using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CPC.Toolkit.Command;
using CPC.Toolkit.Base;
using CPC.POS.Model;
using System.Windows;
using System.ComponentModel;
using System.Linq.Expressions;
using CPC.POS.Database;
using CPC.POS.Repository;
using CPC.Helper;
using System.Collections.ObjectModel;

namespace CPC.POS.ViewModel
{
    public class SalesOrderReturnSearchViewModel : ViewModelBase
    {
        #region Define
        private base_SaleOrderRepository _saleOrderRepository = new base_SaleOrderRepository();
        private base_GuestRepository _guestRepository = new base_GuestRepository();
        private base_StoreRepository _storeRepository = new base_StoreRepository();
        private base_SaleTaxLocationRepository _saleTaxRepository = new base_SaleTaxLocationRepository();

        private string CUSTOMER_MARK = MarkType.Customer.ToDescription();
        #endregion

        #region Constructors
        public SalesOrderReturnSearchViewModel()
        {
            _ownerViewModel = App.Current.MainWindow.DataContext;

           

            InitialCommand();

            //LoadCustomer();

            LoadStores();



        }


        #endregion

        #region Properties
        //Sale Order
        #region SelectedSaleOrder

        private base_SaleOrderModel _selectedSaleOrder;
        /// <summary>
        /// Gets or sets the SelectedSaleOrder.
        /// </summary>
        public base_SaleOrderModel SelectedSaleOrder
        {
            get { return _selectedSaleOrder; }
            set
            {
                if (_selectedSaleOrder != value)
                {
                    _selectedSaleOrder = value;
                    OnPropertyChanged(() => SelectedSaleOrder);

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

        #region CustomerCollection
        private CollectionBase<base_GuestModel> _customerCollection;
        /// <summary>
        /// Gets or sets the CustomerCollection.
        /// </summary>
        public CollectionBase<base_GuestModel> CustomerCollection
        {
            get { return _customerCollection; }
            set
            {
                if (_customerCollection != value)
                {
                    _customerCollection = value;
                    OnPropertyChanged(() => CustomerCollection);
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

        //Search
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

        #region IsAdvanced
        private bool _isAdvanced;
        /// <summary>
        /// Gets or sets the IsAdvanced.
        /// </summary>
        public bool IsAdvanced
        {
            get { return _isAdvanced; }
            set
            {
                if (_isAdvanced != value)
                {
                    _isAdvanced = value;
                    OnPropertyChanged(() => IsAdvanced);
                }
            }
        }
        #endregion

        #region Keyword
        private string _keyword;
        /// <summary>
        /// Gets or sets the Keywork.
        /// </summary>
        public string Keyword
        {
            get { return _keyword; }
            set
            {
                if (_keyword != value)
                {
                    _keyword = value;
                    OnPropertyChanged(() => Keyword);
                }
            }
        }

        public string SearchText { get; set; }
        #endregion

        #region Code

        private string _code;
        /// <summary>
        /// Gets or sets Code.
        /// </summary>
        public string Code
        {
            get
            {
                return _code;
            }
            set
            {
                if (_code != value)
                {
                    _code = value;
                    OnPropertyChanged(() => Code);
                }
            }
        }

        #endregion

        #region ProductName

        private string _productName;
        /// <summary>
        /// Gets or sets ProductName.
        /// </summary>
        public string ProductName
        {
            get
            {
                return _productName;
            }
            set
            {
                if (_productName != value)
                {
                    _productName = value;
                    OnPropertyChanged(() => ProductName);
                }
            }
        }

        #endregion

        #region Category

        private string _category;
        /// <summary>
        /// Gets or sets Category.
        /// </summary>
        public string Category
        {
            get
            {
                return _category;
            }
            set
            {
                if (_category != value)
                {
                    _category = value;
                    OnPropertyChanged(() => Category);
                }
            }
        }

        #endregion

        #region AttributeSize

        private string _attributeSize;
        /// <summary>
        /// Gets or sets AttributeSize.
        /// </summary>
        public string AttributeSize
        {
            get
            {
                return _attributeSize;
            }
            set
            {
                if (_attributeSize != value)
                {
                    _attributeSize = value;
                    OnPropertyChanged(() => AttributeSize);
                }
            }
        }

        #endregion

        #region PartNumber

        private string _partNumber;
        /// <summary>
        /// Gets or sets PartNumber.
        /// </summary>
        public string PartNumber
        {
            get
            {
                return _partNumber;
            }
            set
            {
                if (_partNumber != value)
                {
                    _partNumber = value;
                    OnPropertyChanged(() => PartNumber);
                }
            }
        }

        #endregion

        #region Barcode

        private string _barcode;
        /// <summary>
        /// Gets or sets Barcode.
        /// </summary>
        public string Barcode
        {
            get
            {
                return _barcode;
            }
            set
            {
                if (_barcode != value)
                {
                    _barcode = value;
                    OnPropertyChanged(() => Barcode);
                }
            }
        }

        #endregion

        #endregion

        #region Commands Methods

        #region NewSaleOrderCommand
        /// <summary>
        /// Gets the NewSaleOrder Command.
        /// <summary>

        public RelayCommand<object> NewSaleOrderCommand { get; private set; }



        /// <summary>
        /// Method to check whether the NewSaleOrder command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnNewSaleOrderCommandCanExecute(object param)
        {
            return true;
        }


        /// <summary>
        /// Method to invoke when the NewSaleOrder command is executed.
        /// </summary>
        private void OnNewSaleOrderCommandExecute(object param)
        {
            ComboItem cmbValue = new ComboItem();
            cmbValue.Text = "SaleOrderReturn.New";
            cmbValue.Detail = 0;
            (_ownerViewModel as MainViewModel).OpenViewExecute("SalesOrder", cmbValue);
            CancelSaleOrderRetrunView();
        }
        #endregion

        #region GoToSaleOrderList
        /// <summary>
        /// Gets the GotoSaleOrderList Command.
        /// <summary>

        public RelayCommand<object> GotoSaleOrderListCommand { get; private set; }



        /// <summary>
        /// Method to check whether the GotoSaleOrderList command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnGotoSaleOrderListCommandCanExecute(object param)
        {
            return true;
        }


        /// <summary>
        /// Method to invoke when the GotoSaleOrderList command is executed.
        /// </summary>
        private void OnGotoSaleOrderListCommandExecute(object param)
        {
            ComboItem cmbValue = new ComboItem();
            cmbValue.Text = "SaleOrderReturn.SaleOrderList";
            cmbValue.Detail = 0;
            (_ownerViewModel as MainViewModel).OpenViewExecute("SalesOrder", cmbValue);
            CancelSaleOrderRetrunView();
        }
        #endregion

        #region SelectedSaleOrderCommand
        /// <summary>
        /// Gets the SelectedSaleOrder Command.
        /// <summary>

        public RelayCommand<object> SelectedSaleOrderCommand { get; private set; }


        /// <summary>
        /// Method to check whether the SelectedSaleOrder command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnSelectedSaleOrderCommandCanExecute(object param)
        {
            return SelectedSaleOrder != null;
        }


        /// <summary>
        /// Method to invoke when the SelectedSaleOrder command is executed.
        /// </summary>
        private void OnSelectedSaleOrderCommandExecute(object param)
        {
            ComboItem cmbValue = new ComboItem();
            cmbValue.Text = "SaleOrderReturn.SelectedItem";
            cmbValue.Detail = SelectedSaleOrder.Id;
            (_ownerViewModel as MainViewModel).OpenViewExecute("SalesOrder", cmbValue);
            CancelSaleOrderRetrunView();
        }
        #endregion

        #region CancelCommand
        /// <summary>
        /// Gets the Cancel Command.
        /// <summary>

        public RelayCommand<object> CancelCommand { get; private set; }



        /// <summary>
        /// Method to check whether the Cancel command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnCancelCommandCanExecute(object param)
        {
            return true;
        }


        /// <summary>
        /// Method to invoke when the Cancel command is executed.
        /// </summary>
        private void OnCancelCommandExecute(object param)
        {
            CancelSaleOrderRetrunView();
        }

        #endregion

        #region SearchCommand
        /// <summary>
        /// Gets the Search Command.
        /// <summary>

        public RelayCommand<object> SearchCommand { get; private set; }


        /// <summary>
        /// Method to check whether the Search command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnSearchCommandCanExecute(object param)
        {
            return true;
        }


        /// <summary>
        /// Method to invoke when the Search command is executed.
        /// </summary>
        private void OnSearchCommandExecute(object param)
        {
            if (param != null)
            {
                Search();
            }

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
            if (!string.IsNullOrWhiteSpace(SearchText))//Load Step Current With Search Current with Search
            {
                predicate = CreatePredicateWithConditionSearch(SearchText);
                if (IsAdvanced)
                    predicate = predicate.And(CreateAdvancedSearchCondition());
            }
            LoadDataByPredicate(predicate, false, SaleOrderCollection.Count);
        }
        #endregion

        #endregion

        #region Private Methods
        private void InitialCommand()
        {
            NewSaleOrderCommand = new RelayCommand<object>(OnNewSaleOrderCommandExecute, OnNewSaleOrderCommandCanExecute);
            GotoSaleOrderListCommand = new RelayCommand<object>(OnGotoSaleOrderListCommandExecute, OnGotoSaleOrderListCommandCanExecute);
            SelectedSaleOrderCommand = new RelayCommand<object>(OnSelectedSaleOrderCommandExecute, OnSelectedSaleOrderCommandCanExecute);
            CancelCommand = new RelayCommand<object>(OnCancelCommandExecute, OnCancelCommandCanExecute);
            SearchCommand = new RelayCommand<object>(OnSearchCommandExecute, OnSearchCommandCanExecute);
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
                predicate = predicate.And(x => x.OrderStatus != orderStatus && x.OrderStatus != layawayStatus && x.IsConverted && x.base_SaleOrderShip.Count(y => y.IsShipped) > 0);

                //Cout all SaleOrder in Data base show on grid
                lock (UnitOfWork.Locker)
                {
                    TotalSaleOrder = _saleOrderRepository.GetIQueryable(predicate).Count();

                    //Get data with range
                    IList<base_SaleOrder> saleOrders = _saleOrderRepository.GetRange<DateTime>(currentIndex, NumberOfDisplayItems, x => x.OrderDate.Value, predicate);

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
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                predicate = predicate.And(x => x.SONumber.ToLower().Contains(keyword.ToLower()));

                var customerList = _guestRepository.GetAll(x => x.Mark.Equals(CUSTOMER_MARK) && !x.IsPurged && x.IsActived && (x.LastName.ToLower().Contains(keyword.ToLower()) || x.FirstName.ToLower().Contains(keyword.ToLower()))).Select(x => x.Resource.ToString());
                //var customerList = CustomerCollection.Where(y => y.LastName.ToLower().Contains(keyword.ToLower()) || y.FirstName.ToLower().Contains(keyword.ToLower())).Select(x => x.Resource.ToString());
                predicate = predicate.Or(x => customerList.Contains(x.CustomerResource));

            }
            return predicate;
        }

        /// <summary>
        /// Execute Search
        /// </summary>
        private void Search()
        {
            Expression<Func<base_SaleOrder, bool>> predicate = CreatePredicateWithConditionSearch(Keyword);
            if (IsAdvanced)
            {
                predicate = predicate.And(CreateAdvancedSearchCondition());
            }
            LoadDataByPredicate(predicate, false, 0);
        }

        /// <summary>
        /// Create Conditionsearch with advanced
        /// </summary>
        /// <returns></returns>
        private Expression<Func<base_SaleOrder, bool>> CreateAdvancedSearchCondition()
        {
            base_ProductRepository productRepository = new base_ProductRepository();
            base_DepartmentRepository departmentRepository = new base_DepartmentRepository();
            Expression<Func<base_SaleOrder, bool>> advancedPredicate = PredicateBuilder.True<base_SaleOrder>();
            advancedPredicate = advancedPredicate.And(x => !x.IsPurge);

            if (!string.IsNullOrWhiteSpace(_code))
            {
                IEnumerable<string> GUIDList = productRepository.GetIEnumerable(x =>
                    x.Code != null && x.Code.ToLower().Contains(_code.ToLower())).Select(x => x.Resource.ToString());
                if (GUIDList.Any())
                {
                    advancedPredicate = advancedPredicate.And(x => x.base_SaleOrderDetail.Select(y => y.ProductResource).Intersect(GUIDList).Count() > 0);
                }
                else
                {
                    advancedPredicate = advancedPredicate.And(x => false);
                }
            }
            if (!string.IsNullOrWhiteSpace(_productName))
            {
                IEnumerable<string> GUIDList = productRepository.GetIEnumerable(x =>
                    x.ProductName != null && x.ProductName.ToLower().Contains(_productName.ToLower())).Select(x => x.Resource.ToString());
                if (GUIDList.Any())
                {
                    advancedPredicate = advancedPredicate.And(x => x.base_SaleOrderDetail.Select(y => y.ProductResource).Intersect(GUIDList).Count() > 0);
                }
                else
                {
                    advancedPredicate = advancedPredicate.And(x => false);
                }
            }
            if (!string.IsNullOrWhiteSpace(_category))
            {
                IEnumerable<int> depIdList = departmentRepository.GetAll(x =>
                        x.LevelId == (short)ProductDeparmentLevel.Category && x.Name != null && x.Name.ToLower().Contains(_category.ToLower())).Select(x => x.Id);
                IEnumerable<string> GUIDList = productRepository.GetIEnumerable(x =>
                    depIdList.Contains(x.ProductCategoryId)).Select(x => x.Resource.ToString());
                if (GUIDList.Any())
                {
                    advancedPredicate = advancedPredicate.And(x => x.base_SaleOrderDetail.Select(y => y.ProductResource).Intersect(GUIDList).Count() > 0);
                }
                else
                {
                    advancedPredicate = advancedPredicate.And(x => false);
                }
            }
            if (!string.IsNullOrWhiteSpace(_attributeSize))
            {
                IEnumerable<string> GUIDList = productRepository.GetIEnumerable(x =>
                    (x.Attribute != null && x.Attribute.ToLower().Contains(_attributeSize.ToLower())) ||
                    (x.Size != null && x.Size.ToLower().Contains(_attributeSize.ToLower()))).Select(x => x.Resource.ToString());
                if (GUIDList.Any())
                {
                    advancedPredicate = advancedPredicate.And(x => x.base_SaleOrderDetail.Select(y => y.ProductResource).Intersect(GUIDList).Count() > 0);
                }
                else
                {
                    advancedPredicate = advancedPredicate.And(x => false);
                }
            }
            if (!string.IsNullOrWhiteSpace(_partNumber))
            {
                IEnumerable<string> GUIDList = productRepository.GetIEnumerable(x =>
                    x.PartNumber != null && x.PartNumber.ToLower().Contains(_partNumber.ToLower())).Select(x => x.Resource.ToString());
                if (GUIDList.Any())
                {
                    advancedPredicate = advancedPredicate.And(x => x.base_SaleOrderDetail.Select(y => y.ProductResource).Intersect(GUIDList).Count() > 0);
                }
                else
                {
                    advancedPredicate = advancedPredicate.And(x => false);
                }
            }
            if (!string.IsNullOrWhiteSpace(_barcode))
            {
                IEnumerable<string> GUIDList = productRepository.GetIEnumerable(x =>
                    x.Barcode != null && x.Barcode.ToLower().Contains(_barcode.ToLower())).Select(x => x.Resource.ToString());
                if (GUIDList.Any())
                {
                    advancedPredicate = advancedPredicate.And(x => x.base_SaleOrderDetail.Select(y => y.ProductResource).Intersect(GUIDList).Count() > 0);
                }
                else
                {
                    advancedPredicate = advancedPredicate.And(x => false);
                }
            }
            return advancedPredicate;
        }

        /// <summary>
        /// Load All Customer From DB
        /// </summary>
        private void LoadCustomer()
        {
            IList<base_Guest> customerList = _guestRepository.GetAll(x => x.Mark.Equals(CUSTOMER_MARK) && !x.IsPurged && x.IsActived);

            if (CustomerCollection == null)
                CustomerCollection = new CollectionBase<base_GuestModel>(customerList.OrderBy(x => x.Id).Select(x => new base_GuestModel(x)));
            else
            {
                foreach (base_Guest customer in customerList)
                {
                    //Check Item is existed,update model for item
                    if (CustomerCollection.Any(x => x.Resource.Equals(customer.Resource)))
                    {
                        base_GuestModel customerModel = CustomerCollection.SingleOrDefault(x => x.Resource.Equals(customer.Resource));
                        customerModel.UpdateModel(customer);
                        customerModel.EndUpdate();
                    }
                    else //Add new item
                    {
                        CustomerCollection.Add(new base_GuestModel(customer));
                    }
                }
                //Remove Item From Local collection if in db collection is not existed
                IList<Guid?> itemReomoveList = CustomerCollection.Select(x => x.Resource).Except(customerList.Select(x => x.Resource)).ToList();
                if (itemReomoveList != null)
                {
                    foreach (Guid resource in itemReomoveList)
                    {
                        base_GuestModel itemRemoved = CustomerCollection.SingleOrDefault(x => x.Resource.Equals(resource));
                        CustomerCollection.Remove(itemRemoved);
                    }
                }
            }
        }

        /// <summary>
        /// Cancel ReturnSearch view
        /// </summary>
        private void CancelSaleOrderRetrunView()
        {
            Window window = this.FindOwnerWindow(this);
            window.DialogResult = false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="saleOrderModel"></param>
        private void SetSaleOrderToModel(base_SaleOrderModel saleOrderModel)
        {
            try
            {
                //Set SaleOrderStatus
                saleOrderModel.ItemStatus = Common.StatusSalesOrders.SingleOrDefault(x => Convert.ToInt16(x.ObjValue).Equals(saleOrderModel.OrderStatus));
                //Set Price Schema
                saleOrderModel.PriceLevelItem = Common.PriceSchemas.SingleOrDefault(x => Convert.ToInt16(x.ObjValue).Equals(saleOrderModel.PriceSchemaId));


                Guid customerGuid = Guid.NewGuid();
                if (Guid.TryParse(saleOrderModel.CustomerResource, out customerGuid))
                {
                    base_Guest customer = _guestRepository.Get(x => x.Resource == customerGuid);
                    if (customer != null)
                    {
                        saleOrderModel.GuestModel = new base_GuestModel(customer);
                    }
                }

                GetSaleTax(saleOrderModel);

                saleOrderModel.IsDirty = false;
            }
            catch (Exception ex)
            {
                _log4net.Error(ex);
            }
        }

        /// <summary>
        /// Load Tax for SaleOrder
        /// </summary>
        /// <param name="saleOrderModel"></param>
        private void GetSaleTax(base_SaleOrderModel saleOrderModel)
        {
            //Get Tax Location
            base_SaleTaxLocation saleTaxLocation = _saleTaxRepository.Get(x => x.Id == saleOrderModel.TaxLocation);
            if (saleTaxLocation != null)
            {
                saleOrderModel.TaxLocationModel = new base_SaleTaxLocationModel(saleTaxLocation);
            }
            //Get Tax Code
            base_SaleTaxLocation taxCode = _saleTaxRepository.Get(x => x.ParentId == saleOrderModel.TaxLocationModel.Id && x.TaxCode.Equals(saleOrderModel.TaxCode));
            if (taxCode != null)
            {
                saleOrderModel.TaxLocationModel.TaxCodeModel = new base_SaleTaxLocationModel(taxCode);
                saleOrderModel.TaxLocationModel.TaxCodeModel.SaleTaxLocationOptionCollection = new CollectionBase<base_SaleTaxLocationOptionModel>(saleOrderModel.TaxLocationModel.TaxCodeModel.base_SaleTaxLocation.base_SaleTaxLocationOption.Select(x => new base_SaleTaxLocationOptionModel(x)));
            }
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


        #endregion

        #region Public Methods
        #endregion
    }
}
