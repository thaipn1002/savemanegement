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
        public RelayCommand<object> DoubleClickViewCommand { get; private set; }
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

        #region StoreCollection

        private ObservableCollection<base_GuestModel> _storeCollection;
        /// <summary>
        /// Gets or sets the StoreCollection.
        /// </summary>
        public ObservableCollection<base_GuestModel> StoreCollection
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
                    SyncModel customer = this.ObjectData(filename) as SyncModel;
                    foreach (var item in customer.Content as ObservableCollection<object>)
                        this.InsertCustomer(item as base_GuestModel);
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

        private void InsertCustomer(base_GuestModel GuestModel)
        {
            //To set Customer
            GuestModel.CreateBase_Guest();
            GuestModel.Resource = new Guid();
            if (GuestModel.PhotoCollection != null && GuestModel.PhotoCollection.Count > 0)
            {
                GuestModel.PhotoCollection.FirstOrDefault().IsNew = false;
                GuestModel.PhotoCollection.FirstOrDefault().IsDirty = false;
                GuestModel.Picture = GuestModel.PhotoCollection.FirstOrDefault().ImageBinary;
            }
            else
                GuestModel.Picture = null;
            //To set Additional 
            if (GuestModel.AdditionalModel != null)
            {
                GuestModel.AdditionalModel.CreateAdditional();
                GuestModel.AdditionalModel.ToEntity();
                GuestModel.base_Guest.base_GuestAdditional.Add(GuestModel.AdditionalModel.base_GuestAdditional);
            }
            //To map Personal Info
            if (GuestModel.PersonalInfoModel != null)
            {
                GuestModel.PersonalInfoModel.CreateBase_GuestProfile();
                GuestModel.PersonalInfoModel.ToEntity();
                GuestModel.base_Guest.base_GuestProfile.Add(GuestModel.PersonalInfoModel.base_GuestProfile);
            }
            base_GuestAddressModel addressModel;
            bool firstAddress = true;
            //To Convert from AddressControlCollection To AddressModel 
            foreach (AddressControlModel addressControlModel in GuestModel.AddressControlCollection)
            {
                addressModel = new base_GuestAddressModel();
                addressModel.DateCreated = DateTime.Now;
                addressModel.DateUpdated = DateTime.Now;
                addressModel.UserCreated = Define.USER != null ? Define.USER.UserName : string.Empty;
                addressModel.ToModel(addressControlModel);
                addressModel.IsDefault = firstAddress;
                addressModel.EndUpdate();
                //To convert data from model to entity
                addressModel.ToEntity();
                GuestModel.base_Guest.base_GuestAddress.Add(addressModel.base_GuestAddress);
                firstAddress = false;
                addressModel.EndUpdate();
                addressControlModel.IsDirty = false;
                addressControlModel.IsNew = false;
            }
            GuestModel.ToEntity();
            _guestRepository.Add(GuestModel.base_Guest);
            _guestRepository.Commit();
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
           
        }
        #endregion

        #endregion
    }

    public static class SyncDataHelper
    {
        private static string ExportPath = Define.CONFIGURATION.BackupPath + @"\Export\";
        public static object ObjectData(string filename)
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
        public static void ExportData(object content, string tableName)
        {
            if (!Directory.Exists(ExportPath + tableName))
                Directory.CreateDirectory(ExportPath + tableName);
            string fileName = ExportPath + tableName + "\\" + string.Format("{0}_{1}.exp", tableName, DateTime.Now.ToString("yyMMddHHmmss")); ;
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, content);
            stream.Close();
        }
    }
}