using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using CPC.POS.Database;
using CPC.POS.Model;
using CPC.POS.Repository;
using CPC.Toolkit.Base;
using CPC.Toolkit.Command;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Input;
using System.Windows.Forms;

namespace CPC.POS.ViewModel
{
    class SyncDataViewModel : ViewModelBase
    {
        #region Define
        //To define repositories to use them in class.
        private base_StoreRepository _storeRepository = new base_StoreRepository();
        private base_ProductRepository _productRepository = new base_ProductRepository();
        private base_GuestRepository _guestRepository = new base_GuestRepository();
        private base_PurchaseOrderDetailRepository _purchaseOrderDetailRepository = new base_PurchaseOrderDetailRepository();
        private base_SaleOrderDetailRepository _saleOrderDetailRepository = new base_SaleOrderDetailRepository();
        private base_ProductStoreRepository _productStoreRepository = new base_ProductStoreRepository();
        //To define commands to use them in class.
        public RelayCommand NewCommand { get; private set; }
        public RelayCommand<object> SaveCommand { get; private set; }
        public RelayCommand SearchCommand { get; private set; }
        public RelayCommand<object> DoubleClickViewCommand { get; private set; }
        public RelayCommand<object> ExportCommand { get; private set; }
        public RelayCommand<object> ImportCommand { get; private set; }
        //To define VendorType to use it in class.
        private string _vendorType = MarkType.Vendor.ToDescription();
        private string ExportPath = Define.CONFIGURATION.BackupPath + @"\Backup\";
        #endregion

        #region Constructors
        public SyncDataViewModel()
        {
            base._ownerViewModel = App.Current.MainWindow.DataContext;
            this.InitialCommand();
            this.GetTables();
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

        #region SelectedStore
        /// <summary>
        /// Gets or sets the SelectedStore.
        /// </summary>
        private base_Store _selectedStore;
        public base_Store SelectedStore
        {
            get
            {
                return _selectedStore;
            }
            set
            {
                _selectedStore = value;
                this.OnPropertyChanged(() => SelectedStore);
                if (this.SelectedStore != null)
                {
                    if (this.SelectedStore.Id == -1)
                        this.LoadAllProduct();
                    else
                    {
                        Expression<Func<base_ProductStore, bool>> predicate = PredicateBuilder.True<base_ProductStore>();
                        predicate = this.CreateProductPredicate(this.StoreCollection.IndexOf(this.SelectedStore) - 1);
                        this.LoadProductWithStore(predicate, true);
                    }
                }
            }
        }
        #endregion

        #region ProductCollection
        /// <summary>
        /// Gets or sets the ProductCollection.
        /// </summary>
        private ObservableCollection<base_ProductModel> _productCollection = new ObservableCollection<base_ProductModel>();
        public ObservableCollection<base_ProductModel> ProductCollection
        {
            get
            {
                return _productCollection;
            }
            set
            {
                if (_productCollection != value)
                {
                    _productCollection = value;
                    OnPropertyChanged(() => ProductCollection);
                }
            }
        }
        #endregion

        #region TotalProducts
        private int _totalProducts;
        /// <summary>
        /// Gets or sets the CountFilter For Search Control.
        /// </summary>
        public int TotalProducts
        {

            get
            {
                return _totalProducts;
            }
            set
            {
                if (_totalProducts != value)
                {
                    _totalProducts = value;
                    OnPropertyChanged(() => TotalProducts);
                }
            }
        }
        #endregion

        #region TableCollection

        private ObservableCollection<BackupModel> _tableCollection;
        /// <summary>
        /// Gets or sets the TableCollection.
        /// </summary>
        public ObservableCollection<BackupModel> TableCollection
        {
            get { return _tableCollection; }
            set
            {
                if (_tableCollection != value)
                {
                    _tableCollection = value;
                    OnPropertyChanged(() => TableCollection);
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
            // TODO: Handle command logic here
        }
        #endregion

        #region Save Command
        /// <summary>
        /// Method to check whether the SaveCommand command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnSaveCommandCanExecute(object param)
        {
            if (param != null && (param as ObservableCollection<object>).Count > 0)
                return true;
            return false;
        }
        /// <summary>
        /// Method to invoke when the SaveCommand command is executed.
        /// </summary>
        private void OnSaveCommandExecute(object param)
        {
            try
            {
                // TODO: Handle command logic here
                if (param is ObservableCollection<object>)
                {
                    ObservableCollection<base_ProductModel> ProductSelectedCollection = new ObservableCollection<base_ProductModel>();
                    foreach (var item in (param as ObservableCollection<object>))
                    {
                        ProductSelectedCollection.Add(item as base_ProductModel);
                    }
                    if (ProductSelectedCollection.Count > 0)
                        (_ownerViewModel as MainViewModel).OpenViewExecute("PurchaseOrder", ProductSelectedCollection);
                    App.WriteUserLog("ReOrderStock", "Re-Order stock.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

        }
        #endregion

        #region SearchCommand
        /// <summary>
        /// Method to check whether the SearchCommand command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnSearchCommandCanExecute()
        {
            return true;
        }

        private void OnSearchCommandExecute()
        {
            // TODO: Handle command logic here
        }

        #endregion

        #region DoubleClickCommand
        /// <summary>
        /// Method to check whether the DoubleClick command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnDoubleClickViewCommandCanExecute(object param)
        {
            return param != null ? true : false;
        }

        /// <summary>
        /// Method to invoke when the DoubleClick command is executed.
        /// </summary>
        private void OnDoubleClickViewCommandExecute(object param)
        {
            if (param != null && param is base_ProductModel)
            {
                (_ownerViewModel as MainViewModel).OpenViewExecute("Product", (param as base_ProductModel).Resource);
            }

        }
        #endregion

        #region ExportCommand
        /// <summary>
        /// Method to check whether the ExportCommand command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnExportCommandCanExecute(object param)
        {
            return param != null;
        }

        /// <summary>
        /// Method to invoke when the ExportCommand command is executed.
        /// </summary>
        private void OnExportCommandExecute(object param)
        {
            // TODO: Handle command logic here
        }
        #endregion

        #region ImportCommand
        /// <summary>
        /// Method to check whether the ImportCommand command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnImportCommandCanExecute(object param)
        {
            return param != null;
        }

        /// <summary>
        /// Method to invoke when the ImportCommand command is executed.
        /// </summary>
        private void OnImportCommandExecute(object param)
        {
            // TODO: Handle command logic here

            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.FileOk += delegate
                {
                    this.ImportData(param.ToString(), openFileDialog.FileName);
                };
                openFileDialog.ShowDialog();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        #region CancelCommand

        /// <summary>
        /// Gets the CancelCommand command.
        /// </summary>
        public ICommand CancelCommand { get; private set; }

        /// <summary>
        /// Method to check whether the CancelCommand command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnCancelCommandCanExecute()
        {
            return true;
        }

        /// <summary>
        /// Method to invoke when the CancelCommand command is executed.
        /// </summary>
        private void OnCancelCommandExecute()
        {
            Window window = this.FindOwnerWindow(this);
            window.DialogResult = false;
        }

        #endregion

        #endregion

        #region Private Methods

        #region InitialCommand
        /// <summary>
        /// To register commmand.
        /// </summary>
        private void InitialCommand()
        {
            // Route the commands
            this.ExportCommand = new RelayCommand<object>(OnExportCommandExecute, OnExportCommandCanExecute);
            this.ImportCommand = new RelayCommand<object>(OnImportCommandExecute, OnImportCommandCanExecute);
            this.CancelCommand = new RelayCommand(OnCancelCommandExecute, OnCancelCommandCanExecute);
            //To create a directory to contain export file.
            if (!Directory.Exists(this.ExportPath))
                Directory.CreateDirectory(this.ExportPath);
        }
        #endregion

        #region CreateProductPredicate
        /// <summary>
        /// To create search condition.
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        private Expression<Func<base_ProductStore, bool>> CreateProductPredicate(int storeId)
        {
            // Initial predicate
            Expression<Func<base_ProductStore, bool>> predicate = PredicateBuilder.True<base_ProductStore>();
            // Set conditions for predicate
            predicate = predicate.And(x => x.StoreCode == storeId);
            return predicate;
        }
        #endregion

        #region ChangeViewExecute
        /// <summary>
        /// Method check Item has edit & show message
        /// </summary>
        /// <returns></returns>
        private bool ChangeViewExecute(bool? isClosing)
        {
            return true;
        }
        #endregion

        #region LoadStore
        /// <summary>
        /// To load all of stores from DB.
        /// </summary>
        private void LoadStore()
        {
            //To get data from store table.
            this.StoreCollection = new ObservableCollection<base_Store>(_storeRepository.GetAll().OrderBy(x => x.Id));
            this.StoreCollection.Insert(0, new base_Store { Id = -1, Name = "All Store" });
        }
        #endregion

        #region LoadProductWithStore
        /// <summary>
        /// To load products with store from DB.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="refreshData"></param>
        /// <param name="currentIndex"></param>
        private void LoadProductWithStore(Expression<Func<base_ProductStore, bool>> predicate, bool refreshData = false, int currentIndex = 0)
        {
            try
            {
                //To add item.
                BackgroundWorker bgWorker = new BackgroundWorker { WorkerReportsProgress = true };
                this.ProductCollection = new ObservableCollection<base_ProductModel>();
                bgWorker.DoWork += (sender, e) =>
                {
                    base.IsBusy = true;
                    short itemTypeGroup = (short)ItemTypes.Group;
                    short itemTypeServices = (short)ItemTypes.Services;
                    short itemInsurance = (short)ItemTypes.Insurance;
                    IEnumerable<base_ProductStore> productStores = _productStoreRepository.GetAll(predicate).Where(x => x.base_Product.ItemTypeId != itemTypeGroup && x.base_Product.ItemTypeId != itemTypeServices && x.base_Product.ItemTypeId != itemInsurance).OrderBy(x => x.StoreCode);
                    if (productStores.Count() == 0)
                        base.IsBusy = false;
                    foreach (var productStore in productStores)
                        bgWorker.ReportProgress(0, productStore);
                };
                bgWorker.ProgressChanged += (sender, e) =>
                {
                    //To add item.
                    base_ProductStore productStore = e.UserState as base_ProductStore;
                    if (productStore.ReorderPoint > 0)
                    {
                        base_ProductModel productModel = new base_ProductModel(productStore.base_Product);
                        base_Guest vendor = _guestRepository.GetAll(x => x.Mark.Equals(this._vendorType) && x.IsActived && !x.IsPurged && x.Id.Equals(productModel.VendorId)).SingleOrDefault();
                        if (vendor != null)
                            productModel.VendorName = vendor.Company;
                        //To get purchase order detail
                        string productResource = productModel.Resource.ToString();
                        //To get product with all of stores
                        productModel.QuantityOnHand = productStore.QuantityOnHand;
                        productModel.QuantityOnSO = productStore.QuantityOnCustomer;
                        //QuantityOnHand is QuantityAvailable..Repairing..
                        productModel.QuantityAvailable = productStore.QuantityAvailable;
                        productModel.QuantityOnPO = productStore.QuantityOnOrder;
                        productModel.CompanyReOrderPoint = productStore.ReorderPoint;
                        productModel.EndUpdate();
                        if (productModel.QuantityAvailable + productModel.QuantityOnPO < productModel.CompanyReOrderPoint)
                        {
                            productModel.ReOrderQuatity = productModel.CompanyReOrderPoint - (productModel.QuantityAvailable + productModel.QuantityOnPO);
                            this.ProductCollection.Add(productModel);
                        }
                    }
                };
                bgWorker.RunWorkerCompleted += (sender, e) =>
                {
                    //To count all User in Data base show on grid
                    this.TotalProducts = this.ProductCollection.Count;
                    base.IsBusy = false;
                };
                bgWorker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

        }
        #endregion

        #region LoadAllProduct
        /// <summary>
        /// To load all product from DB.
        /// </summary>
        private void LoadAllProduct()
        {
            try
            {
                //To add item.
                BackgroundWorker bgWorker = new BackgroundWorker { WorkerReportsProgress = true };
                this.ProductCollection = new ObservableCollection<base_ProductModel>();
                bgWorker.DoWork += (sender, e) =>
                {
                    base.IsBusy = true;
                    short itemTypeGroup = (short)ItemTypes.Group;
                    short itemTypeServices = (short)ItemTypes.Services;
                    short itemInsurance = (short)ItemTypes.Insurance;
                    IEnumerable<base_Product> products = _productRepository.GetAll().Where(x => x.ItemTypeId != itemTypeGroup && x.ItemTypeId != itemTypeServices && x.ItemTypeId != itemInsurance);
                    if (products.Count() == 0)
                        base.IsBusy = false;
                    foreach (var productItem in products)
                        bgWorker.ReportProgress(0, productItem);
                };
                bgWorker.ProgressChanged += (sender, e) =>
                {
                    //To add item.
                    base_Product product = e.UserState as base_Product;
                    if (product.CompanyReOrderPoint > 0)
                    {
                        base_ProductModel productModel = new base_ProductModel(product);
                        base_Guest vendor = _guestRepository.GetAll(x => x.Mark.Equals(this._vendorType) && x.IsActived && !x.IsPurged && x.Id.Equals(productModel.VendorId)).SingleOrDefault();
                        if (vendor != null)
                            productModel.VendorName = vendor.Company;
                        //To get purchase order detail
                        string productResource = productModel.Resource.ToString();
                        //To get product with all of stores
                        //productModel.OnHandStore = productModel.QuantityOnHand;
                        productModel.QuantityOnSO = productModel.QuantityOnCustomer;
                        //QuantityOnHand is QuantityAvailable..Repairing..
                        //productModel.QuantityOnHand = productModel.QuantityOnHand - productModel.QuantityOnSO;
                        productModel.QuantityOnPO = productModel.QuantityOnOrder;
                        productModel.EndUpdate();
                        if (productModel.QuantityAvailable + productModel.QuantityOnPO < productModel.CompanyReOrderPoint)
                        {
                            productModel.ReOrderQuatity = productModel.CompanyReOrderPoint - (productModel.QuantityAvailable + productModel.QuantityOnPO);
                            this.ProductCollection.Add(productModel);
                        }
                    }
                };
                bgWorker.RunWorkerCompleted += (sender, e) =>
                {
                    //To count all User in Data base show on grid
                    this.TotalProducts = this.ProductCollection.Count;
                    base.IsBusy = false;
                };
                bgWorker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        #endregion

        #region GetTables
        private void GetTables()
        {
            this.TableCollection = new ObservableCollection<BackupModel>();
            this.TableCollection.Add(new BackupModel { Id = 1, Name = "Customer", Text = "base_Guest", Detail = MarkType.Customer.ToString() });
            this.TableCollection.Add(new BackupModel { Id = 1, Name = "Employee", Text = "base_Guest", Detail = MarkType.Employee.ToString() });
        }
        #endregion

        #region Data
        private void ImportData(string tableName, string filename)
        {
            switch (tableName)
            {
                case "Customer":
                    this.ObjectData(filename);
                    break;
                case "Employee":
                    break;
            }

        }

        /// <summary>
        /// To convert object data to its real data.
        /// </summary>
        /// <param name="content"></param>
        private object ObjectData(string filename)
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
                Object obj = (Object)formatter.Deserialize(stream);
                stream.Close();
                return obj;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// To convert data to object data.
        /// </summary>
        /// <param name="content"></param>
        private void ExportData(object content, string tableName)
        {
            string fileName = this.ExportPath;
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, content);
            stream.Close();
        }
        #endregion

        #endregion

        #region Public Methods

        #region OnViewChangingCommandCanExecute
        /// Check save data when changing view
        /// </summary>
        /// <param name="isClosing"></param>
        /// <returns></returns>
        protected override bool OnViewChangingCommandCanExecute(bool isClosing)
        {
            return ChangeViewExecute(isClosing);
        }
        #endregion

        #region ChangeSearchMode
        /// <summary>
        /// ChangeSearchMode
        /// </summary>
        /// <param name="isList"></param>
        /// <param name="param"></param>
        public override void ChangeSearchMode(bool isList, object param = null)
        {
            if (this.ChangeViewExecute(null))
            {
                this.IsSearchMode = true;
            }
        }

        #endregion

        #region LoadData
        /// <summary>
        /// Loading data in Change view or Inititial
        /// </summary>
        public override void LoadData()
        {
            //To set default store.
            this.SelectedStore = this.StoreCollection[0];
        }
        #endregion

        #endregion
    }
}