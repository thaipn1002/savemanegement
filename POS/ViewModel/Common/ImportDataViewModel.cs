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
using CPCToolkitExtLibraries;

namespace CPC.POS.ViewModel
{
    class ImportDataViewModel : ViewModelBase
    {
        #region Define
        //To define repositories to use them in class.
        private base_GuestRepository _guestRepository = new base_GuestRepository();
        //To define commands to use them in class.
        public RelayCommand NewCommand { get; private set; }
        public RelayCommand<object> SaveCommand { get; private set; }
        public RelayCommand SearchCommand { get; private set; }
        public RelayCommand<object> OpenFileCommand { get; private set; }
        public RelayCommand<object> ExportCommand { get; private set; }
        public RelayCommand<object> ImportCommand { get; private set; }
        //To define VendorType to use it in class.
        private string _vendorType = MarkType.Vendor.ToDescription();
        private string ExportPath = Define.CONFIGURATION.BackupPath + @"\Backup\";
        #endregion

        #region Constructors
        public ImportDataViewModel()
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

        #region CustomerCollection

        private ObservableCollection<ImportCustomerModel> _customerCollection;
        /// <summary>
        /// Gets or sets the CustomerCollection.
        /// </summary>
        public ObservableCollection<ImportCustomerModel> CustomerCollection
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

        #region FilePath
        private string _filePath = string.Empty;
        public string FilePath
        {
            get { return _filePath; }
            set
            {
                if (value != _filePath)
                {
                    _filePath = value;
                    OnPropertyChanged(() => FilePath);
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
            return false;
        }
        /// <summary>
        /// Method to invoke when the SaveCommand command is executed.
        /// </summary>
        private void OnSaveCommandExecute(object param)
        {
            try
            {

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

        #region OpenFileCommand
        /// <summary>
        /// Method to check whether the DoubleClick command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnOpenFileCommandCanExecute(object param)
        {
            return true;
        }

        /// <summary>
        /// Method to invoke when the DoubleClick command is executed.
        /// </summary>
        private void OnOpenFileCommandExecute(object param)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.FileOk += delegate
                {
                    this.FilePath=openFileDialog.FileName;
                    this.LoadData();
                };
                openFileDialog.ShowDialog();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
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
                foreach (var item in this.CustomerCollection)
                    this.InsertCustomer(item);
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
            this.OpenFileCommand = new RelayCommand<object>(OnOpenFileCommandExecute , OnOpenFileCommandCanExecute);
            this.ImportCommand = new RelayCommand<object>(OnImportCommandExecute , OnImportCommandCanExecute);
            this.CancelCommand = new RelayCommand(OnCancelCommandExecute , OnCancelCommandCanExecute);
            //To create a directory to contain export file.
            if (!Directory.Exists(this.ExportPath))
                Directory.CreateDirectory(this.ExportPath);
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
        }
        #endregion


        #region GetTables
        private void GetTables()
        {
            this.TableCollection = new ObservableCollection<BackupModel>();
            this.TableCollection.Add(new BackupModel { Id = 1 , Name = "Customer" , Text = "base_Guest" , Detail = MarkType.Customer.ToString() });
            this.TableCollection.Add(new BackupModel { Id = 1 , Name = "Employee" , Text = "base_Guest" , Detail = MarkType.Employee.ToString() });
        }
        #endregion

        #region Data
        private void ImportData(string tableName , string filename)
        {
            switch (tableName)
            {
                case "Customer":
                   
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
                Stream stream = new FileStream(filename , FileMode.Open , FileAccess.Read , FileShare.Read);
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
        private void ExportData(object content , string tableName)
        {
            string fileName = this.ExportPath;
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(fileName , FileMode.Create , FileAccess.Write , FileShare.None);
            formatter.Serialize(stream , content);
            stream.Close();
        }

        private void InsertCustomer(ImportCustomerModel ImportCustomerModel)
        {
            //To set customer.
            base_Guest base_Guest=new Database.base_Guest();
            base_Guest.Resource = new Guid();
            base_Guest.Picture = null;
            //To get profile.
            base_GuestProfile base_GuestProfile=new Database.base_GuestProfile();
            //GuestModel.base_Guest.base_GuestProfile.Add(GuestModel.PersonalInfoModel.base_GuestProfile);
            bool firstAddress = true;
            //To Convert from AddressControlCollection To AddressModel. 
            base_GuestAddress base_GuestAddress = new base_GuestAddress();
            base_GuestAddress.DateCreated = DateTime.Now;
            base_GuestAddress.DateUpdated = DateTime.Now;
            base_GuestAddress.UserCreated = Define.USER != null ? Define.USER.UserName : string.Empty;
            base_GuestAddress.IsDefault = firstAddress;
            //To convert data from model to entity
            base_GuestAddress.AddressTypeId = 0;
            base_GuestAddress.AddressLine1 = ImportCustomerModel.Street.Trim();
            if (ImportCustomerModel.City != null)
                base_GuestAddress.City = ImportCustomerModel.City.Trim();
            base_GuestAddress.StateProvinceId = this.GetStateID(ImportCustomerModel.City);
            base_GuestAddress.CountryId = 1;
            base_GuestAddress.DateCreated = DateTime.Now;
            base_GuestAddress.UserCreated = Define.USER.UserName;
            base_GuestAddress.IsDefault = true;
            base_Guest.base_GuestAddress.Add(base_GuestAddress);
             _guestRepository.Add(base_Guest);
            _guestRepository.Commit();
        }
        #endregion

        private int GetStateID(string name)
        {
            return CPC.Helper.Common.States.SingleOrDefault(x=>x.Text.Contains(name)).IntValue;
        }

        private void LoadingData()
        {
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(this.FilePath);
            Microsoft.Office.Interop.Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange;
            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;
            CustomerCollection =new ObservableCollection<ImportCustomerModel>();
            ImportCustomerModel model;
            string tp=string.Empty;
            for (int i = 2 ; i <= rowCount ; i++)
            {
                model=new ImportCustomerModel();
                for (int j = 1 ; j <= colCount ; j++)
                {
                    if (xlRange.Cells[i , j].Value2!=null)
                    {
                        if (j==1)
                            model.CustomerID=i;
                        else if (j==3)
                            model.CustomerName=xlRange.Cells[i , j].Value2.ToString();
                        else if (j==4)
                            model.Address=xlRange.Cells[i , j].Value2.ToString();
                        else if (j==5)
                            model.Phone=xlRange.Cells[i , j].Value2.ToString();
                        else if (j==6)
                            model.CellPhone=xlRange.Cells[i , j].Value2.ToString();
                    }
                }
                if (model.CustomerID>0)
                    tp=model.CustomerName=xlRange.Cells[i , 1].Value2.ToString();
                else
                    model.City=tp;
                if (model.CustomerID==0 && !string.IsNullOrEmpty(model.CustomerName))
                    CustomerCollection.Add(model);
            }
        }

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
        public override void ChangeSearchMode(bool isList , object param = null)
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

        }
        #endregion

        #endregion
    }
}