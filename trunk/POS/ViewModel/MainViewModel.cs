using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.EntityClient;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System.Xml;
using CPC.Control;
using CPC.Helper;
using CPC.POS.Database;
using CPC.POS.Model;
using CPC.POS.Report;
using CPC.POS.Repository;
using CPC.POS.View;
using CPC.Toolkit.Base;
using CPC.Toolkit.Command;
using CPC.Toolkit.Layout;
using Npgsql;
using SecurityLib;
using MessageBoxControl;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CPC.POS.ViewModel
{
    partial class MainViewModel : ViewModelBase, IDataErrorInfo
    {
        #region Layout Defines

        // Number of rows
        private int _rowNumbers = 4;

        // Number of columns
        private int _columnNumbers = 2;

        // Host view list
        private List<BorderLayoutHost> _hostList = new List<BorderLayoutHost>();

        // Grid contain hosts
        private Grid _grdHost;

        // Grid contain targets
        private Grid _grdTarget;

        // Store expand status of view
        private bool _isPanelExpanded;

        // Store columns to expand
        private ColumnDefinition _colSubItem;
        private ColumnDefinition _colSubItemExpanded;

        #endregion

        #region Defines

        private base_UserLogRepository _userLogRepository = new base_UserLogRepository();
        private base_ResourceAccountRepository _accountRepository = new base_ResourceAccountRepository();
        private base_StoreRepository _storeRepository = new base_StoreRepository();
        private base_SaleTaxLocationRepository _saleTaxLocationRepository = new base_SaleTaxLocationRepository();

        private DispatcherTimer _idleTimer;
        private Regex _regexPassWord = new Regex(Define.CONFIGURATION.PasswordFormat);

        private LockScreenView _lockScreenView;
        private bool _isSwitchDatabase;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the IsAnimationCompletedAll
        /// </summary>
        public bool IsAnimationCompletedAll
        {
            get
            {
                return _hostList.Count(x => !x.IsAnimationCompleted) == 0;
                //return _hostList.Count(x => !x.IsAnimationCompleted || x.ViewModelBase.IsBusy) == 0;
            }
        }

        private ObservableCollection<ContainerModel> _hiddenHostList = new ObservableCollection<ContainerModel>();
        /// <summary>
        /// Gets or sets the HiddenHostList.
        /// </summary>
        public ObservableCollection<ContainerModel> HiddenHostList
        {
            get
            {
                return _hiddenHostList;
            }
            set
            {
                if (_hiddenHostList != value)
                {
                    _hiddenHostList = value;
                    OnPropertyChanged(() => HiddenHostList);
                }
            }
        }

        private ContainerModel _selectedHiddenHost;
        /// <summary>
        /// Gets or sets the SelectedHiddenHost.
        /// </summary>
        public ContainerModel SelectedHiddenHost
        {
            get
            {
                return _selectedHiddenHost;
            }
            set
            {
                if (_selectedHiddenHost != value)
                {
                    _selectedHiddenHost = value;
                    OnPropertyChanged(() => SelectedHiddenHost);

                    if (SelectedHiddenHost != null)
                    {
                        int index = HiddenHostList.IndexOf(SelectedHiddenHost);
                        HiddenHostList.RemoveAt(index);
                        var host = _hostList.ElementAt(_hostList.Count - 1 - index);
                        ChangeLayoutItem(host.Container.btnImageIcon);
                        Button btn = _hostList.ElementAt(_rowNumbers + 1).Container.btnTitle;
                        HiddenHostList.Add(new ContainerModel { Name = btn.Content.ToString(), Text = btn.Tag.ToString() });
                    }
                }
            }
        }

        /// <summary>
        /// Gets the HiddenHostVisibility
        /// </summary>
        public Visibility HiddenHostVisibility
        {
            get
            {
                return HiddenHostList.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private string _loginName;
        /// <summary>
        /// Gets or sets the LoginName.
        /// </summary>
        public string LoginName
        {
            get
            {
                return _loginName;
            }
            set
            {
                if (_loginName != value)
                {
                    _loginName = value;
                    OnPropertyChanged(() => LoginName);
                }
            }
        }

        private string _userName = string.Empty;
        /// <summary>
        /// Gets or sets the UserName.
        /// </summary>
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    OnPropertyChanged(() => UserName);
                }
            }
        }

        private string _userPassword = string.Empty;
        /// <summary>
        /// Gets or sets the UserPassword.
        /// </summary>
        public string UserPassword
        {
            get
            {
                return _userPassword;
            }
            set
            {
                if (_userPassword != value)
                {
                    _userPassword = value;
                    OnPropertyChanged(() => UserPassword);
                }
            }
        }

        /// <summary>
        /// Gets the Status
        /// </summary>
        public string Status
        {
            get
            {
                return "Connecting...";
            }
        }

        /// <summary>
        /// Gets the Server
        /// </summary>
        public string Server
        {
            get
            {
                if (Define.USER != null)
                    return Define.USER.IpAddress;
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets or sets the Database
        /// </summary>
        public string Database
        {
            get;
            private set;
        }

        private bool _isLockScreen;
        /// <summary>
        /// Gets or sets the IsLockScreen.
        /// </summary>
        public bool IsLockScreen
        {
            get
            {
                return _isLockScreen;
            }
            set
            {
                if (_isLockScreen != value)
                {
                    _isLockScreen = value;
                    OnPropertyChanged(() => IsLockScreen);
                }
            }
        }

        private string _storeName;
        /// <summary>
        /// Gets or sets the StoreName.
        /// </summary>
        public string StoreName
        {
            get
            {
                return _storeName;
            }
            set
            {
                if (_storeName != value)
                {
                    _storeName = value;
                    OnPropertyChanged(() => StoreName);
                }
            }
        }

        private string _taxLocation;
        /// <summary>
        /// Gets or sets the TaxLocation.
        /// </summary>
        public string TaxLocation
        {
            get
            {
                return _taxLocation;
            }
            set
            {
                if (_taxLocation != value)
                {
                    _taxLocation = value;
                    OnPropertyChanged(() => TaxLocation);
                }
            }
        }

        private string _taxCode;
        /// <summary>
        /// Gets or sets the TaxCode.
        /// </summary>
        public string TaxCode
        {
            get
            {
                return _taxCode;
            }
            set
            {
                if (_taxCode != value)
                {
                    _taxCode = value;
                    OnPropertyChanged(() => TaxCode);
                }
            }
        }

        /// <summary>
        /// Gets the DashboardVisibility.
        /// </summary>
        public Visibility DashboardVisibility
        {
            get
            {
                return _hostList.Count > 0 ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        /// <summary>
        /// Gets the IsPracticeMode.
        /// </summary>
        public bool IsPracticeMode
        {
            get
            {
                return Database.Contains("train");
                ;
            }
        }

        private Skins _selectedSkin;
        /// <summary>
        /// Gets or sets the SelectedSkin.
        /// </summary>
        public Skins SelectedSkin
        {
            get
            {
                return _selectedSkin;
            }
            set
            {
                if (_selectedSkin != value)
                {
                    _selectedSkin = value;
                    OnPropertyChanged(() => SelectedSkin);

                    try
                    {
                        // Change ribbon image folder by selected skin
                        RibbonImageFolder = string.Format("/Image/RibbonImages/{0}/", SelectedSkin);

                        // Get color resource
                        ResourceDictionary colorResource = Application.Current.Resources.MergedDictionaries[1];

                        // Get image resource
                        ResourceDictionary imageResource = Application.Current.Resources.MergedDictionaries[2];

                        // Change color and image source by selected skin
                        colorResource.Source = new Uri(string.Format(@"..\Dictionary\Brushes\{0}\{0}Resources.xaml", SelectedSkin), UriKind.Relative);
                        imageResource.Source = new Uri(string.Format(@"..\Dictionary\Brushes\{0}\{0}ImageResources.xaml", SelectedSkin), UriKind.Relative);

                        colorResource = Application.Current.Resources.MergedDictionaries[1];
                        imageResource = Application.Current.Resources.MergedDictionaries[2];
                    }
                    catch (Exception ex)
                    {
                        MsgControl.ShowWarning(ex.ToString(), "Thông báo", MessageBoxButtonCustom.OK);
                    }
                }
            }
        }

        #region SelectedLanguage

        private string _iconLanguagePath = @"/Image/RibbonImages/RibbonLanguage.png";
        /// <summary>
        /// Gets or sets the IconLanguagePath.
        /// </summary>
        public string IconLanguagePath
        {
            get { return _iconLanguagePath; }
            set
            {
                if (_iconLanguagePath != value)
                {
                    _iconLanguagePath = value;
                    OnPropertyChanged(() => IconLanguagePath);
                }
            }
        }

        private ComboItem _selectedLanguage;
        /// <summary>
        /// Gets or sets the SelectedLanguage.
        /// </summary>
        public ComboItem SelectedLanguage
        {
            get { return _selectedLanguage; }
            set
            {
                if (_selectedLanguage != value)
                {
                    _selectedLanguage = value;
                    OnPropertyChanged(() => SelectedLanguage);

                    Common.ChangeLanguage(SelectedLanguage.CultureInfo);

                    foreach (ComboItem comboItem in Common.Languages)
                    {
                        // Reset languages checked
                        comboItem.Flag = false;
                    }

                    // Checked new language is selected
                    SelectedLanguage.Flag = true;

                    string iconLanguagePath = @"/Image/RibbonImages/";
                    IconLanguagePath = string.Format("{0}RibbonLanguage{1}.png", iconLanguagePath, SelectedLanguage.Code);
                }
            }
        }

        #endregion

        private string _ribbonImageFolder;
        /// <summary>
        /// Gets or sets the RibbonImageFolder.
        /// </summary>
        public string RibbonImageFolder
        {
            get { return _ribbonImageFolder; }
            set
            {
                if (_ribbonImageFolder != value)
                {
                    _ribbonImageFolder = value;
                    OnPropertyChanged(() => RibbonImageFolder);
                }
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainViewModel()
            : base()
        {
            InitialCommand();
            this.CheckIdleTime();
            this.LoadLayout();
            this.LoadStatusInformation();
            this.SelectedSkin = Skins.Blue;
            this.GetPermission();
        }
        #endregion

        #region Command Methods

        #region OpenViewCommand

        /// <summary>
        /// Gets the OpenViewCommand command.
        /// </summary>
        public ICommand OpenViewCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Method to invoke when the OpenViewCommand command is executed.
        /// </summary>
        private void OnOpenViewCommandExecute(object param)
        {
            OpenViewExecute(param.ToString());
        }

        #endregion

        #region ChangeViewCommand

        /// <summary>
        /// Gets the ChangeViewCommand command.
        /// </summary>
        public ICommand ChangeViewCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Method to invoke when the ChangeViewCommand command is executed.
        /// </summary>
        private void OnChangeViewCommandExecute()
        {
            if (HiddenHostList.Count > 0)
            {
                HiddenHostList.RemoveAt(0);
                Button btn = _hostList.ElementAt(_rowNumbers + 1).Container.btnTitle;
                HiddenHostList.Add(new ContainerModel { Name = btn.Content.ToString(), Text = btn.Tag.ToString() });
            }
            var host = _hostList.LastOrDefault();
            if (host != null)
                ChangeLayoutItem(host);
        }

        #endregion

        #region CloseViewCommand

        /// <summary>
        /// Gets the CloseViewCommand command.
        /// </summary>
        public ICommand CloseViewCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Method to invoke when the CloseViewCommand command is executed.
        /// </summary>
        private void OnCloseViewCommandExecute(object param)
        {
            ContainerModel model = this.HiddenHostList.SingleOrDefault(x => x.Name.Equals(param.ToString()));
            int index = HiddenHostList.IndexOf(model);
            if (index >= 0 && index < HiddenHostList.Count)
            {
                HiddenHostList.RemoveAt(index);
                OnPropertyChanged(() => HiddenHostVisibility);
                _hostList.RemoveAt(_hostList.Count - 1 - index);
            }
        }

        #endregion

        #region ClearViewCommand

        /// <summary>
        /// Gets the ClearViewCommand command.
        /// </summary>
        public ICommand ClearViewCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Method to invoke when the ClearViewCommand command is executed.
        /// </summary>
        private void OnClearViewCommandExecute()
        {
            // Clear host list
            _hostList.Clear();
            HiddenHostList.Clear();
            OnPropertyChanged(() => HiddenHostVisibility);
            // Clear UIElement hosts
            _grdHost.Children.Clear();
        }

        #endregion

        #region CloseCommand

        /// <summary>
        /// Gets the CloseCommand command.
        /// </summary>
        public ICommand CloseCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Method to check whether the CloseCommand command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnCloseCommandCanExecute()
        {
            if (_hostList.Count == 0)
                return false;
            BorderLayoutHost currentHost = _hostList.ElementAt(0) as BorderLayoutHost;
            return !currentHost.ShowNotification(true);
        }

        /// <summary>
        /// Method to invoke when the CloseCommand command is executed.
        /// </summary>
        private void OnCloseCommandExecute()
        {
            // To write log into base_UserLogDetail table.
            App.WriteUserLog("Exit", "User closed the application.");
            // To Update status of user.
            this.UpdateUserLog();

            if (_isSwitchDatabase)
            {
                string connectionName = "POSDBEntities";
                string appConfigPath = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;

                // Get content app config file
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(appConfigPath);

                // Get connection string node
                XmlNode connectionStringsNode = xmlDoc.SelectSingleNode("configuration/connectionStrings");
                if (connectionStringsNode != null)
                {
                    foreach (XmlNode childNode in connectionStringsNode)
                    {
                        if (childNode.Attributes["name"].Value.Equals(connectionName))
                        {
                            // Get current connection string value
                            string connectionValue = childNode.Attributes["connectionString"].Value;

                            // Initial entity connection string builder
                            EntityConnectionStringBuilder entityConnectionBuilder = new EntityConnectionStringBuilder(connectionValue);

                            string databaseName = string.Format("Database={0}", Database);
                            string trainDBName = string.Format("Database=train_{0}", Database);
                            if (IsPracticeMode)
                                trainDBName = string.Format("Database={0}", Database.Split('_').LastOrDefault());

                            // Modify database name
                            entityConnectionBuilder.ProviderConnectionString = entityConnectionBuilder.ProviderConnectionString.Replace(databaseName, trainDBName);

                            // Update new connection string
                            childNode.Attributes["connectionString"].Value = entityConnectionBuilder.ConnectionString;

                            // Save app config file
                            xmlDoc.Save(appConfigPath);

                            // Update connection string in configuration manager
                            ConfigurationManager.RefreshSection("connectionStrings");

                            // Reload unit of work
                            UnitOfWork.Reload();
                            break;
                        }
                    }
                }
            }
        }

        #endregion

        #region ChangeStyleCommand

        /// <summary>
        /// Gets the ChangeStyleCommand command.
        /// </summary>
        public ICommand ChangeStyleCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Method to check whether the ChangeStyleCommand command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnChangeStyleCommandCanExecute(object param)
        {
            return true;
        }

        /// <summary>
        /// Method to invoke when the ChangeStyleCommand command is executed.
        /// </summary>
        private void OnChangeStyleCommandExecute(object param)
        {
            //int count = Application.Current.Resources.MergedDictionaries.Count;
            //ResourceDictionary skin = Application.Current.Resources.MergedDictionaries[count - 2];
            SelectedSkin = (Skins)param;

            //switch (SelectedSkin)
            //{
            //    case Skins.Blue:
            //        skin.Source = new Uri(string.Format(@"..\Dictionary\Brushes\Blue\{0}Resources.xaml", param.ToString()), UriKind.Relative);
            //        skin = Application.Current.Resources.MergedDictionaries[count - 1];
            //        break;
            //    case Skins.Grey:
            //        skin.Source = new Uri(string.Format(@"..\Dictionary\Brushes\Grey\{0}Resources.xaml", param.ToString()), UriKind.Relative);
            //        skin = Application.Current.Resources.MergedDictionaries[count - 1];
            //        break;
            //    case Skins.Red:
            //        skin.Source = new Uri(string.Format(@"..\Dictionary\Brushes\Red\{0}Resources.xaml", param.ToString()), UriKind.Relative);
            //        skin = Application.Current.Resources.MergedDictionaries[count - 1];
            //        break;
            //}

        }

        #endregion

        #region LoginCommand

        /// <summary>
        /// Gets the LoginCommand command.
        /// </summary>
        public ICommand LoginCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Method to check whether the LoginCommand command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnLoginCommandCanExecute()
        {
            return ExtensionErrors.Count == 0;
        }

        /// <summary>
        /// Method to invoke when the LoginCommand command is executed.
        /// </summary>
        private void OnLoginCommandExecute()
        {
            try
            {
                bool result = false;

                // Encrypt password
                string encryptPassword = AESSecurity.Encrypt(UserPassword);

                // Check default account
                if (UserName.Equals(Define.ADMIN_ACCOUNT) && encryptPassword.Equals(Define.ADMIN_PASSWORD))
                    result = true;
                else if (UserName.Equals(LoginName)) // Check login account
                {
                    // Get login account from database
                    base_ResourceAccount account = _accountRepository.Get(x => x.LoginName.Equals(UserName) && x.Password.Equals(encryptPassword));
                    result = account != null;
                }

                if (result)
                {
                    // Clear user password
                    UserPassword = string.Empty;

                    // Turn off lock screen view
                    _lockScreenView.DialogResult = true;
                }
                else
                {
                    // Show alert message
                    MsgControl.ShowWarning("Tên đăng nhập hoặc mật khẩu không hợp lệ, hãy thử lại!", "Thông báo", MessageBoxButtonCustom.OK);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("OnLoginCommand" + ex.ToString());
            }
        }

        #endregion

        #region LogOutCommand

        /// <summary>
        /// Gets the LogOutCommand command.
        /// </summary>
        public ICommand LogOutCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Enable the Login button if all required field are validated IsValid = true
        ///Created by Thaipn.
        /// </summary>
        /// <returns></returns>
        private bool CanOnLogOutExecute()
        {
            return true;
        }

        /// <summary>
        /// Check the user login
        /// </summary>
        private void OnLogOutExecuted()
        {
            object a = Application.Current.Windows;
            if (App.Current.MainWindow is MainWindow)
            {
                //To Update status of user.
                //this.UpdateUserLog();
                //To write log into base_UserLogDetail table.
                App.WriteUserLog("Logout", "User logged out the application.");
                // To LogOut Windows.
                App.Messenger.NotifyColleagues(Define.USER_LOGOUT_RESULT);
            }
        }

        #endregion

        #region OpenManagementUserCommand

        /// <summary>
        /// Gets the OpenManagementUserCommand command.
        /// </summary>
        public ICommand OpenManagementUserCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Method to invoke when the OpenViewCommand command is executed.
        /// </summary>
        private void OnOpenManagermentUserCommandExecute(object param)
        {
            ManagementUserLogView view = new ManagementUserLogView();
            view.DataContext = new ManagementUserLogViewModel();
            view.Show();
        }

        #endregion

        #region ChangePasswordCommand

        /// <summary>
        /// Gets the ChangePasswordCommand command.
        /// </summary>
        public ICommand ChangePasswordCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Method to check whether the ChangePasswordCommand command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnChangePasswordCommandCanExecute()
        {
            if (Define.USER == null)
                return false;
            return !Define.ADMIN_ACCOUNT.Equals(Define.USER.LoginName);
        }

        /// <summary>
        /// Method to invoke when the ChangePasswordCommand command is executed.
        /// </summary>
        private void OnChangePasswordCommandExecute()
        {
            ChangePasswordViewModel viewModel = new ChangePasswordViewModel();
            bool? result = _dialogService.ShowDialog<ChangePasswordView>(this, viewModel, "Change Password");
            if (result.HasValue && result.Value)
            {
                LoginName = viewModel.ResourceAccountModel.LoginName;
            }
        }

        #endregion

        #region LockScreenCommand

        /// <summary>
        /// Gets the LockScreenCommand command.
        /// </summary>
        public ICommand LockScreenCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Method to check whether the LockScreenCommand command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnLockScreenCommandCanExecute()
        {
            return true;
        }

        /// <summary>
        /// Method to invoke when the LockScreenCommand command is executed.
        /// </summary>
        public void OnLockScreenCommandExecute()
        {
            // Stop idle timer
            _idleTimer.Stop();

            // Set user name default
            UserName = LoginName;

            // Get main window
            Window mainWindow = App.Current.MainWindow;

            // Initial lock screen view
            _lockScreenView = new LockScreenView();
            _lockScreenView.DataContext = this;

            // Register closing event
            _lockScreenView.Closing += (senderLockScreen, eLockScreen) =>
            {
                // Prevent closing lock screen view when login have not success
                if (!_lockScreenView.DialogResult.HasValue)
                    eLockScreen.Cancel = true;
            };

            // Set default position over main window
            _lockScreenView.Width = mainWindow.ActualWidth;
            _lockScreenView.Height = mainWindow.ActualHeight;
            switch (mainWindow.WindowState)
            {
                case WindowState.Maximized:
                    _lockScreenView.Top = 0;
                    _lockScreenView.Left = 0;
                    break;
                case WindowState.Minimized:
                case WindowState.Normal:
                    _lockScreenView.Top = mainWindow.Top;
                    _lockScreenView.Left = mainWindow.Left;
                    break;
            }

            // Set login binding
            Binding loginBinding = new Binding("LoginCommand");
            BindingOperations.SetBinding(_lockScreenView.btnLogin, Button.CommandProperty, loginBinding);

            // Get active window if main show popup
            Window activeWindow = mainWindow.OwnedWindows.Cast<Window>().SingleOrDefault(x => x.IsActive);
            if (activeWindow != null)
            {
                // Set active window is owner lock screen view
                _lockScreenView.Owner = activeWindow;
            }
            else
            {
                // Set main window is owner lock screen view
                _lockScreenView.Owner = mainWindow;
            }

            // Show lock screen view
            if (_lockScreenView.ShowDialog().HasValue)
            {
                IdleTimeHelper.LostFocusTime = null;

                // Star idle timer
                _idleTimer.Start();
            }
        }

        #endregion

        #region DataCommand

        /// <summary>
        /// Gets the LockScreenCommand command.
        /// </summary>
        public RelayCommand<object> DataCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Method to check whether the LockScreenCommand command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnDataCommandCanExecute(object param)
        {
            return true;
        }

        /// <summary>
        /// Method to invoke when the LockScreenCommand command is executed.
        /// </summary>
        public void OnDataCommandExecute(object param)
        {
            try
            {
                switch (param.ToString())
                {
                    case "Backup":
                        MessageBoxResultCustom result = MsgControl.ShowQuestion(Language.Text38, Language.Information, MessageBoxButtonCustom.YesNo);
                        if (result == MessageBoxResultCustom.Yes)
                        {
                            //Backup data
                            BackgroundWorker bgWorker = new BackgroundWorker
                            {
                                WorkerReportsProgress = true
                            };
                            bgWorker.DoWork += (sender, e) =>
                            {
                                // Turn on BusyIndicator
                                if (Define.DisplayLoading)
                                    IsBusy = true;
                                BackupRestoreHelper.BackupDB();
                            };
                            bgWorker.RunWorkerCompleted += (sender, e) =>
                            {
                                // Turn off BusyIndicator
                                IsBusy = false;
                                if (BackupRestoreHelper.SuccessfulFlag == 1)
                                    MsgControl.ShowInfomation(Language.Text32, Language.Information, MessageBoxButtonCustom.OK);
                                else
                                    MsgControl.ShowWarning(Language.Text33, Language.Warning, MessageBoxButtonCustom.OK);
                            };
                            // Run async background worker
                            bgWorker.RunWorkerAsync();
                        }
                        break;

                    case "Restore":
                        MessageBoxResultCustom resultRestore = MsgControl.ShowQuestion(Language.Text39, Language.Information, MessageBoxButtonCustom.YesNo);
                        if (resultRestore == MessageBoxResultCustom.Yes)
                        {
                            System.Windows.Forms.OpenFileDialog openFile = new System.Windows.Forms.OpenFileDialog();
                            openFile.InitialDirectory = BackupRestoreHelper.BackupPath;
                            openFile.Filter = "File (*.backup)|*.backup";
                            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            {
                                //Backup data
                                BackgroundWorker bgWorkerBackup = new BackgroundWorker
                                {
                                    WorkerReportsProgress = true
                                };
                                bgWorkerBackup.DoWork += (sender, e) =>
                                {
                                    // Turn on BusyIndicator
                                    if (Define.DisplayLoading)
                                        IsBusy = true;
                                    BackupRestoreHelper.BackupDB();
                                };
                                // Run async background worker
                                bgWorkerBackup.RunWorkerAsync();

                                BackgroundWorker bgWorker = new BackgroundWorker
                                {
                                    WorkerReportsProgress = true
                                };
                                bgWorker.DoWork += (sender, e) =>
                                {
                                    // Turn on BusyIndicator
                                    if (Define.DisplayLoading)
                                        IsBusy = true;
                                    BackupModel model = new BackupModel();
                                    model.Path = openFile.FileName;
                                    BackupRestoreHelper.RestoreDB(model);
                                };
                                bgWorker.RunWorkerCompleted += (sender, e) =>
                                {
                                    // Turn off BusyIndicator
                                    IsBusy = false;
                                    if (BackupRestoreHelper.SuccessfulFlag == 1)
                                    {
                                        MsgControl.ShowInfomation(Language.Text35, Language.Information, MessageBoxButtonCustom.OK);
                                        //LogOut Windows.
                                        App.Messenger.NotifyColleagues(Define.USER_LOGOUT_RESULT);
                                    }
                                    else
                                        MsgControl.ShowWarning(Language.Text36, Language.Warning, MessageBoxButtonCustom.OK);
                                };
                                // Run async background worker
                                bgWorker.RunWorkerAsync();
                            }
                        }
                        break;

                    case "Clear":
                        MessageBoxResultCustom resultClearAll = MsgControl.ShowQuestion(Language.Text40, Language.Information, MessageBoxButtonCustom.YesNo);
                        if (resultClearAll == MessageBoxResultCustom.Yes)
                        {
                            BackupRestoreHelper.ClearAllData();
                            MsgControl.ShowWarning(Language.Text37, Language.Warning, MessageBoxButtonCustom.OK);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        #endregion

        #region ExitCommand

        /// <summary>
        /// Gets the ExitCommand command.
        /// </summary>
        public ICommand ExitCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Method to check whether the ExitCommand command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnExitCommandCanExecute()
        {
            return true;
        }

        /// <summary>
        /// Method to invoke when the ExitCommand command is executed.
        /// </summary>
        private void OnExitCommandExecute()
        {
            App.Current.MainWindow.Close();
        }

        #endregion

        #region SwitchDatabaseCommand

        /// <summary>
        /// Gets the SwitchDatabaseCommand command.
        /// </summary>
        public ICommand SwitchDatabaseCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Method to check whether the SwitchDatabaseCommand command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnSwitchDatabaseCommandCanExecute()
        {
            return true;
        }

        /// <summary>
        /// Method to invoke when the SwitchDatabaseCommand command is executed.
        /// </summary>
        private void OnSwitchDatabaseCommandExecute()
        {
            string mode = string.Format("Bạn có muốn chuyển ứng dụng sang {0}?", IsPracticeMode ? Language.RealMode : Language.PracticeMode);
            MessageBoxResultCustom msgResult = MsgControl.ShowWarning(mode, "Thông báo", MessageBoxButtonCustom.YesNo);
            if (msgResult.Is(MessageBoxResultCustom.Yes))
            {
                // Modify connection string when close main window
                _isSwitchDatabase = true;

                // Logout application
                App.Messenger.NotifyColleagues(Define.USER_LOGOUT_RESULT);
            }
        }

        #endregion

        #region ChangeLanguageCommand

        /// <summary>
        /// Gets the ChangeLanguageCommand command.
        /// </summary>
        public ICommand ChangeLanguageCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Method to check whether the ChangeLanguageCommand command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnChangeLanguageCommandCanExecute(object param)
        {
            return true;
        }

        /// <summary>
        /// Method to invoke when the ChangeLanguageCommand command is executed.
        /// </summary>
        private void OnChangeLanguageCommandExecute(object param)
        {
            //foreach (ComboItem comboItem in Common.Languages)
            //{
            //    // Reset languages checked
            //    comboItem.Flag = false;
            //}

            //// Checked new language is selected
            //(param as ComboItem).Flag = true;

            SelectedLanguage = param as ComboItem;
        }

        #endregion

        #region OpenLiabilityCommand

        /// <summary>
        /// Gets the OpenLiabilityCommand command.
        /// </summary>
        public ICommand OpenLiabilityCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Enable the Login button if all required field are validated IsValid = true
        ///Created by Thaipn.
        /// </summary>
        /// <returns></returns>
        private bool CanOnOpenLiabilityExecute()
        {
            return this.AllowAccessLiabilities;
        }

        /// <summary>
        /// Check the user login
        /// </summary>
        private void OnOnOpenLiabilityExecuted()
        {
            Define.IsOpenLiability = true;
            this.LoadLiabilities();
        }

        #endregion

        #region ImportCommand
        /// <summary>
        /// Gets the ImportCommand command.
        /// </summary>
        public RelayCommand<object> ImportCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Method to check whether the ExitCommand command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnImportCommandCanExecute(object param)
        {
            return true;
        }

        /// <summary>
        /// Method to invoke when the ExitCommand command is executed.
        /// </summary>
        private void OnImportCommandExecute(object param)
        {

        }

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
        private void ExportData(object content, string tableName)
        {
            string fileName = @"E:\Thai\Work\07-12\ExportData\tbl.osb";
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, content);
            stream.Close();
        }
        private bool CheckConnectionDB()
        {
            bool result = false;

            //POSEntities objectContext = new POSEntities(ConfigurationManager.ConnectionStrings["POSTEST"].ConnectionString);

            try
            {
                // Check connection
                //objectContext.Connection.Open();
                //var query = objectContext.base_UserRight.ToList();
                //objectContext.Connection.Dispose();
                //ExportData(query,string.Empty);
                //POSEntities objectContext1 = new POSEntities(ConfigurationManager.ConnectionStrings["POSDBEntities"].ConnectionString);
                //objectContext1.Connection.Open();
                //foreach (var item in query)
                //    objectContext1.AddTobase_UserRight(item);
                //objectContext1.SaveChanges();+		data	Count = 128	object {System.Collections.Generic.List<CPC.POS.Database.base_UserRight>}
                object data = this.ObjectData(@"E:\Thai\Work\07-12\ExportData\tbl.osb");
                POSEntities objectContext1 = new POSEntities(ConfigurationManager.ConnectionStrings["POSDBEntities"].ConnectionString);
                objectContext1.Connection.Open();
                foreach (var item in (data as System.Collections.Generic.List<CPC.POS.Database.base_UserRight>))
                    objectContext1.AddTobase_UserRight(item);
                objectContext1.SaveChanges();
            }
            catch
            {
                result = false;
            }
            finally
            {
                // Enforce close connnection
                //if (objectContext.Connection.State.Equals(ConnectionState.Open))
                //    objectContext.Connection.Close();
            }

            return result;
        }

        #endregion

        #endregion

        #region Layout Methods

        /// <summary>
        /// Open a UserControl as view by name
        /// </summary>
        /// <param name="viewName">Name of view</param>
        /// <param name="param">Opening with parameter. Default parameter is null</param>
        public void OpenViewExecute(string viewName, object param = null)
        {
            // Check all animation is completed
            if (IsAnimationCompletedAll)
            {
                // Create new target
                BorderLayoutTarget target = new BorderLayoutTarget();

                // Define position for target
                _grdTarget.Children.Add(target);
                Grid.SetRowSpan(target, _rowNumbers);
                if (_hostList.Count == 0) // The largest target
                    Grid.SetColumnSpan(target, _columnNumbers);

                // Create new host with defined target
                BorderLayoutHost host = new BorderLayoutHost(target);
                host.SetHostName(viewName);

                // Set parameter for view
                if (param != null)
                    host.Tag = param;

                // Check parameter to open view
                var hostClicked = _hostList.FirstOrDefault(x => x.KeyName.Equals(host.KeyName));
                if (hostClicked == null)
                {
                    // Show notification when open a new form
                    if (_hostList.Count > 0 && !_hostList.ElementAt(0).ShowNotification(false))
                        return;

                    if (!CreateContainerView(host))
                        return;

                    if (_hostList.Count > 0)
                    {
                        // Set focus to title bar to active keybinding
                        _hostList.ElementAt(0).Container.btnTitle.Focus();

                        // Turn on screenshot
                        _hostList.ElementAt(0).AllowScreenShot = true;
                    }

                    _hostList.Insert(0, host);
                    _grdHost.Children.Insert(0, host);
                    host.Loaded += new RoutedEventHandler(host_Loaded);
                    SetPositionTarget();

                    if (_hostList.Count > _rowNumbers + 1)
                    {
                        Button btn = _hostList.ElementAt(_rowNumbers + 1).Container.btnTitle;
                        HiddenHostList.Add(new ContainerModel { Name = btn.Content.ToString(), Text = btn.Tag.ToString() });
                        OnPropertyChanged(() => HiddenHostVisibility);
                    }
                }
                else
                {
                    ChangeLayoutItem(hostClicked);
                    hostClicked.ViewModelBase.ChangeSearchMode(host.IsOpenList, host.Tag);
                }

                // Hide dashboard when open one view
                OnPropertyChanged(() => DashboardVisibility);
            }
        }

        /// <summary>
        /// Create container to contain view and register events
        /// </summary>
        /// <param name="host">Host contain container</param>
        /// <returns>True is create view. False is show popup</returns>
        private bool CreateContainerView(BorderLayoutHost host)
        {
            bool showPopup = false;
            host.Child = new ContainerView();
            UserControl view = new UserControl();
            ViewModelBase viewModel = new ViewModelBase();
            //TestViewModel testViewModel = new TestViewModel { TitleView = name };
            //host.SetDataContext(testViewModel);
            //host.DataContext = testViewModel;
            //host.Container.grdContent.Children.Add(view);
            host.Container.btnTitle.Tag = string.Empty;
            host.Container.btnTitle.Content = host.DisplayName;
            host.Container.btnTitle.PreviewMouseDoubleClick += new MouseButtonEventHandler(btnTitle_MouseDoubleClick);
            host.Container.btnImageIcon.Click += new RoutedEventHandler(btnImageIcon_Click);
            host.Container.btnTitle.Click += new RoutedEventHandler(btnTitle_Click);
            host.Container.btnClose.Click += new RoutedEventHandler(btnClose_Click);
            host.Container.tgExpand.IsChecked = _isPanelExpanded;
            host.Container.tgExpand.Checked += new RoutedEventHandler(tgExpand_Checked);
            host.Container.tgExpand.Unchecked += new RoutedEventHandler(tgExpand_Unchecked);
            if (_hostList.Count == 0)
                host.Container.tgExpand.Visibility = Visibility.Collapsed;
            host.Container.btnImageIcon.Visibility = Visibility.Collapsed;

            switch (host.KeyName)
            {
                #region Application Menu

                case "CompanySetting":
                    view = new CompanySettingView();
                    viewModel = new CompanySettingViewModel();
                    host.Container.btnTitle.Tag = "Thông tin công ty";
                    break;
                case "Department":
                    view = new DepartmentView();
                    viewModel = new DepartmentViewModel();
                    host.Container.btnTitle.Tag = "Thông tin công ty";
                    break;

                #endregion

                #region Store Tab
                case "Employee":
                    view = new EmployeeInformationView();
                    viewModel = new EmployeeViewModel(host.IsOpenList, host.Tag);
                    host.Container.btnTitle.Tag = "Nhân viên";
                    break;
                case "UserPermission":
                    view = new UserListView();
                    viewModel = new UserListViewModel();
                    host.Container.btnTitle.Tag = "Phân Quyền";
                    break;
                #endregion

                #region Sales Tab

                case "Customer":
                    view = new CustomerView();
                    viewModel = new CustomerViewModel(host.IsOpenList);
                    host.Container.btnTitle.Tag = "Khách hàng";
                    break;
                case "SalesOrder":
                    view = new SalesOrderView();
                    viewModel = new SalesOrderViewModel(host.IsOpenList, host.Tag);
                    host.Container.btnTitle.Tag = "Bán hàng";
                    break;
                case "Liability":
                    view = new LiabilityView();
                    viewModel = new LiabilityViewModel();
                    host.Container.btnTitle.Tag = "Danh sách công nợ";
                    break;

                case "SalesOrderLocked":
                    view = new LockSalesOrderView();
                    viewModel = new LockSalesOrderViewModel(host.IsOpenList, host.Tag);
                    host.Container.btnTitle.Tag = "Đơn hàng bị khóa";
                    break;

                case "SalesOrderReturn":
                    _dialogService.ShowDialog<SalesOrderReturnSearchView>(this, new SalesOrderReturnSearchViewModel(), "Sale Order Return Search");
                    showPopup = true;
                    host.Container.btnTitle.Tag = "Đơn hàng bị trả lại";
                    break;

                #endregion

                #region Purchase Tab

                case "Vendor":
                    view = new VendorView();
                    viewModel = new VendorViewModel(host.IsOpenList, host.Tag);
                    host.Container.btnTitle.Tag = "Nhà cung cấp";
                    break;
                case "PurchaseOrder":
                    view = new PurchaseOrderView();
                    viewModel = new PurchaseOrderViewModel(host.IsOpenList, host.Tag);
                    host.Container.btnTitle.Tag = "Mua hàng";
                    break;
                case "PurchaseOrderLocked":
                    view = new LockPOListView();
                    viewModel = new LockPOListViewModel(host.IsOpenList, host.Tag);
                    host.Container.btnTitle.Tag = "Hóa đơn bị khóa";
                    break;
                case "PurchaseOrderReturn":
                    _dialogService.ShowDialog<POReturnSearchView>(this, new POReturnSearchViewModel(), null);
                    showPopup = true;
                    break;

                #endregion

                #region Inventory Tab

                case "Product":
                    view = new ProductView();
                    viewModel = new ProductViewModel(host.IsOpenList, host.Tag);
                    host.Container.btnTitle.Tag = "Sản phẩm";
                    break;
                case "ProductManual":
                    view = new ProductManualView();
                    viewModel = new ProductManualViewModel(host.IsOpenList, host.Tag);
                    break;
                case "Pricing":
                    view = new PricingView();
                    viewModel = new PricingViewModel(host.IsOpenList);
                    host.Container.btnTitle.Tag = "Thay đổi giá";
                    break;
                case "Discount":
                    view = new PromotionView();
                    viewModel = new PromotionViewModel(host.IsOpenList);
                    host.Container.btnTitle.Tag = "Giảm giá";
                    break;
                case "CountSheet":
                    view = new CountSheetView();
                    viewModel = new CountSheetViewModel(host.IsOpenList);
                    host.Container.btnTitle.Tag = "Kiểm kho";
                    break;
                case "CountSheetList":
                    view = new CountSheetView();
                    viewModel = new CountSheetViewModel();
                    host.Container.btnTitle.Tag = "Kiểm kho";
                    break;
                case "TransferStock":
                    view = new TransferStockView();
                    viewModel = new TransferStockViewModel(host.IsOpenList, host.Tag);
                    host.Container.btnTitle.Tag = "Chuyển kho";
                    break;
                case "ReOrderStock":
                    view = new ReOrderStockView();
                    viewModel = new ReorderStockViewModel();
                    host.Container.btnTitle.Tag = "Mua hàng lại";
                    break;

                case "CurrentStock":
                    view = new CurrentStockView();
                    viewModel = new CurrentStockViewModel();
                    host.Container.btnTitle.Tag = "Hàng tồn kho";
                    break;

                case "WorkOrder":
                    view = new WorkOrderView();
                    viewModel = new WorkOrderViewModel(host.IsOpenList);
                    break;
                case "CostAdjustment":
                    view = new CostAdjustmentHistoryView();
                    viewModel = new CostAdjustmentHistoryViewModel();
                    host.Container.btnTitle.Tag = "Điều chỉnh chi phí";
                    break;
                case "QuantityAdjustment":
                    view = new QuantityAdjustmentHistoryView();
                    viewModel = new QuantityAdjustmentHistoryViewModel();
                    host.Container.btnTitle.Tag = "Điều chỉnh số lượng";
                    break;

                #endregion

                #region Report Tab

                case "InventoryReport":
                    view = new InventoryReportView();
                    viewModel = new InventoryReportViewModel();
                    break;
                case "SalesReport":
                    break;
                case "PurchaseReport":
                    break;

                #endregion

                #region Configuration Tab

                case "Attachment":
                    view = new AttachmentView();
                    viewModel = new AttachmentViewModel();
                    host.Container.btnTitle.Tag = "Tập tin đính kèm";
                    break;
                case "SalesTax":
                    view = new SalesTaxView();
                    viewModel = new SalesTaxViewModel();
                    host.Container.btnTitle.Tag = "Thuế";
                    break;
                case "Style":
                    break;

                #endregion

                #region Maintenance
                case "BackUpData":
                    _dialogService.ShowDialog<BackupDataView>(this, new BackupRestoreViewModel(), null);
                    showPopup = true;
                    break;

                case "ExportData":
                    //CheckConnectionDB();
                    _dialogService.ShowDialog<ExportDataView>(this, new SyncDataViewModel(), null);
                    showPopup = true;
                    break;
                #endregion
            }

            host.DataContext = viewModel;
            host.Container.grdContent.Children.Add(view);

            return !showPopup;
        }

        /// <summary>
        /// Set shortcut key for usercontrol from main view
        /// </summary>
        /// <param name="host"></param>
        public void SetKeyBinding(InputBindingCollection inputBindingCollection)
        {
            SetKeyBinding(inputBindingCollection, App.Current.MainWindow);
        }

        /// <summary>
        /// Set shortcut key for usercontrol from main view
        /// </summary>
        /// <param name="host"></param>
        public void SetKeyBinding(InputBindingCollection inputBindingCollection, Window target)
        {
            // Get input binding collection from source window
            InputBindingCollection sourceInputBindingCollection = inputBindingCollection;

            if (sourceInputBindingCollection != null)
            {
                foreach (InputBinding sourceInputBindingItem in sourceInputBindingCollection)
                {
                    // Get key gesture of input binding
                    KeyGesture sourceKeyGesture = sourceInputBindingItem.Gesture as KeyGesture;

                    // Create key binding for main
                    KeyBinding targetKeyBinding = new KeyBinding(sourceInputBindingItem.Command, sourceKeyGesture);
                    //targetKeyBinding.CommandTarget = host;
                    targetKeyBinding.CommandParameter = sourceInputBindingItem.CommandParameter + "Main";

                    // Get key binding from main
                    InputBinding keyBinding = target.InputBindings.Cast<InputBinding>().FirstOrDefault(
                        x => ((KeyGesture)x.Gesture).Key.Equals(sourceKeyGesture.Key) &&
                            ((KeyGesture)x.Gesture).Modifiers.Equals(sourceKeyGesture.Modifiers));

                    // Check exist key binding
                    if (keyBinding != null)
                    {
                        // Remove key binding is existed from main
                        target.InputBindings.Remove(keyBinding);
                    }

                    // Add new key binding to main
                    target.InputBindings.Add(targetKeyBinding);
                }
            }
        }

        /// <summary>
        /// Expand current view when click expand button
        /// </summary>
        /// <param name="tgExpand"></param>
        private void ExpandItem(ToggleButton tgExpand)
        {
            if (IsAnimationCompletedAll)
            {
                _isPanelExpanded = tgExpand.IsChecked.Value;
                int colSubItemIndex = _grdTarget.ColumnDefinitions.IndexOf(_colSubItem);
                int colSubItemExpandedIndex = _grdTarget.ColumnDefinitions.IndexOf(_colSubItemExpanded);
                if (_isPanelExpanded)
                {
                    if (colSubItemIndex >= 0)
                        _grdTarget.ColumnDefinitions.Remove(_colSubItem);
                    if (colSubItemExpandedIndex < 0)
                        _grdTarget.ColumnDefinitions.Add(_colSubItemExpanded);
                }
                else
                {
                    if (colSubItemExpandedIndex >= 0)
                        _grdTarget.ColumnDefinitions.Remove(_colSubItemExpanded);
                    if (colSubItemIndex < 0)
                        _grdTarget.ColumnDefinitions.Add(_colSubItem);
                }
                for (int i = 1; i < _hostList.Count; i++)
                {
                    BorderLayoutHost host = _hostList.ElementAt(i);
                    host.RotateItem(_isPanelExpanded);
                }
            }
        }

        /// <summary>
        /// Call close view function when click close button
        /// </summary>
        /// <param name="btnClicked"></param>
        private void CloseItem(Button btnClicked)
        {
            BorderLayoutHost hostClicked = _hostList.SingleOrDefault(x => x.Container.btnClose.Equals(btnClicked));
            CloseItem(hostClicked);
        }

        /// <summary>
        /// Call close view function from other view
        /// </summary>
        /// <param name="viewName"></param>
        public void CloseItem(string viewName)
        {
            // Close hidden view
            OnCloseViewCommandExecute(viewName);

            // Close visible view
            BorderLayoutHost hostClicked = _hostList.FirstOrDefault(x => x.KeyName.Equals(viewName));
            CloseItem(hostClicked);
        }

        /// <summary>
        /// Process close view function
        /// </summary>
        /// <param name="hostClicked"></param>
        private void CloseItem(BorderLayoutHost hostClicked)
        {
            if (IsAnimationCompletedAll && hostClicked != null)
            {
                // Show notification when close a form
                int hostClickedPosition = _hostList.IndexOf(hostClicked);
                if (hostClickedPosition == 0 && !hostClicked.ShowNotification(true))
                    return;

                if (HiddenHostList.Count > 0)
                {
                    HiddenHostList.RemoveAt(HiddenHostList.Count - 1);
                    OnPropertyChanged(() => HiddenHostVisibility);
                }

                _hostList.Remove(hostClicked);
                _grdHost.Children.Remove(hostClicked);
                if (_hostList.Count > 0)
                {
                    BorderLayoutHost host = _hostList.ElementAt(0);
                    KeyboardNavigation.SetTabNavigation(hostClicked.View, KeyboardNavigationMode.Continue);
                    BorderLayoutTarget target = host.Target;
                    Grid.SetRow(target, 0);
                    Grid.SetRowSpan(target, _rowNumbers);
                    Grid.SetColumn(target, 0);
                    if (_hostList.Count > 1)
                    {
                        host.Container.tgExpand.IsChecked = _isPanelExpanded;
                        host.Container.tgExpand.Visibility = Visibility.Visible;
                    }
                    else // Only one view
                    {
                        Grid.SetColumnSpan(target, _columnNumbers);
                        //host.Container.tgExpand.IsChecked = false;
                        host.Container.tgExpand.Visibility = Visibility.Collapsed;
                    }
                    host.Container.btnImageIcon.Visibility = Visibility.Collapsed;
                    host.RotateItem(false);

                    // Refresh data
                    if (hostClickedPosition == 0)
                        host.IsRefreshData = true;
                    SetPositionTarget();
                    SetKeyBinding(host.View.InputBindings);
                }

                // Show dashboard when close all view
                OnPropertyChanged(() => DashboardVisibility);
            }
        }

        /// <summary>
        /// Call change view layout function
        /// </summary>
        /// <param name="btnClicked"></param>
        private void ChangeLayoutItem(Button btnClicked)
        {
            if (IsAnimationCompletedAll)
            {
                // Get clicked host
                var hostClicked = _hostList.SingleOrDefault(
                    x => x.Container.btnImageIcon.Equals(btnClicked) || x.Container.btnTitle.Equals(btnClicked));
                ChangeLayoutItem(hostClicked);
            }
        }

        /// <summary>
        /// Process change view layout
        /// </summary>
        /// <param name="hostClicked"></param>
        private void ChangeLayoutItem(BorderLayoutHost hostClicked)
        {
            if (IsAnimationCompletedAll)
            {
                // Turn on screenshot of main host
                if (_hostList.Count > 0)
                    _hostList.ElementAt(0).AllowScreenShot = true;

                // If clicked host is not the first host, swap position that host
                if (hostClicked != null && _hostList.IndexOf(hostClicked) > 0)
                {
                    // Show notification when change position form
                    if (!_hostList.ElementAt(0).ShowNotification(false))
                        return;

                    // Set position target of clicked host
                    BorderLayoutTarget target = hostClicked.Target;
                    Grid.SetRow(target, 0);
                    Grid.SetRowSpan(target, _rowNumbers);
                    Grid.SetColumn(target, 0);
                    if (_hostList.Count == 0)
                        Grid.SetColumnSpan(target, _columnNumbers);

                    _hostList.Remove(hostClicked);
                    _grdHost.Children.Remove(hostClicked);
                    _hostList.Insert(0, hostClicked);
                    _grdHost.Children.Insert(0, hostClicked);

                    KeyboardNavigation.SetTabNavigation(hostClicked.View, KeyboardNavigationMode.Continue);
                    hostClicked.Container.tgExpand.IsChecked = _isPanelExpanded;
                    hostClicked.Container.tgExpand.Visibility = Visibility.Visible;
                    hostClicked.Container.btnImageIcon.Visibility = Visibility.Collapsed;
                    hostClicked.RotateItem(false);

                    // Refresh data
                    hostClicked.IsRefreshData = true;
                    SetPositionTarget();
                    SetKeyBinding(hostClicked.View.InputBindings);
                }
            }
        }

        /// <summary>
        /// Set position of target from the second to last
        /// </summary>
        private void SetPositionTarget()
        {
            for (int i = 1; i < _hostList.Count; i++)
            {
                BorderLayoutHost host = _hostList.ElementAt(i);
                BorderLayoutTarget target = host.Target;
                Grid.SetRow(target, i - 1);
                Grid.SetRowSpan(target, 1);
                Grid.SetColumn(target, 1);
                Grid.SetColumnSpan(target, 1);

                // Update host is not main
                // Disable focusable of host's view
                KeyboardNavigation.SetTabNavigation(host.View, KeyboardNavigationMode.None);
                // Collapse expand button
                host.Container.tgExpand.Visibility = Visibility.Collapsed;
                // Visible image icon button
                host.Container.btnImageIcon.Visibility = Visibility.Visible;
                // If expand button is checked, rotate title bar of host
                host.RotateItem(_isPanelExpanded);
            }
        }

        /// <summary>
        /// Create default layout
        /// </summary>
        private void LoadLayout()
        {
            // Get all grid to layout
            Grid grdMainView = App.Current.MainWindow.FindName("grdMainView") as Grid;
            _grdTarget = new Grid
            {
                Name = "grdTarget"
            };
            _grdHost = new Grid
            {
                Name = "grdHost",
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top
            };
            grdMainView.Children.Add(_grdTarget);
            grdMainView.Children.Add(_grdHost);

            // Add column for target grid
            _grdTarget.ColumnDefinitions.Add(new ColumnDefinition());
            _colSubItem = new ColumnDefinition
            {
                Width = new GridLength(215)
            };
            _colSubItemExpanded = new ColumnDefinition
            {
                Width = new GridLength(36)
            };
            _grdTarget.ColumnDefinitions.Add(_colSubItem);

            // Add row for target grid
            for (int i = 0; i < _rowNumbers; i++)
                _grdTarget.RowDefinitions.Add(new RowDefinition());
            _grdTarget.RowDefinitions.Add(new RowDefinition
            {
                Height = new GridLength(0)
            });
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initial commands
        /// </summary>
        private void InitialCommand()
        {
            OpenViewCommand = new RelayCommand<object>(OnOpenViewCommandExecute);
            ChangeViewCommand = new RelayCommand(OnChangeViewCommandExecute);
            CloseViewCommand = new RelayCommand<object>(OnCloseViewCommandExecute);
            ClearViewCommand = new RelayCommand(OnClearViewCommandExecute);
            CloseCommand = new RelayCommand(OnCloseCommandExecute, OnCloseCommandCanExecute);
            ChangeStyleCommand = new RelayCommand<object>(OnChangeStyleCommandExecute, OnChangeStyleCommandCanExecute);
            LoginCommand = new RelayCommand(OnLoginCommandExecute, OnLoginCommandCanExecute);
            LogOutCommand = new RelayCommand(OnLogOutExecuted, CanOnLogOutExecute);
            OpenManagementUserCommand = new RelayCommand<object>(OnOpenManagermentUserCommandExecute);
            ChangePasswordCommand = new RelayCommand(OnChangePasswordCommandExecute, OnChangePasswordCommandCanExecute);
            LockScreenCommand = new RelayCommand(OnLockScreenCommandExecute, OnLockScreenCommandCanExecute);
            DataCommand = new RelayCommand<object>(OnDataCommandExecute, OnDataCommandCanExecute);
            ExitCommand = new RelayCommand(OnExitCommandExecute, OnExitCommandCanExecute);
            SwitchDatabaseCommand = new RelayCommand(OnSwitchDatabaseCommandExecute, OnSwitchDatabaseCommandCanExecute);
            ChangeLanguageCommand = new RelayCommand<object>(OnChangeLanguageCommandExecute, OnChangeLanguageCommandCanExecute);
            OpenLiabilityCommand = new RelayCommand(OnOnOpenLiabilityExecuted, CanOnOpenLiabilityExecute);
            this.ImportCommand = new RelayCommand<object>(OnImportCommandExecute, OnImportCommandCanExecute);
        }

        /// <summary>
        /// To update data on base_UserLog table.
        /// </summary>
        private void UpdateUserLog()
        {
            try
            {
                if (Define.USER != null)
                {
                    CPC.POS.Repository.base_UserLogRepository userLogRepository = new Repository.base_UserLogRepository();
                    CPC.POS.Database.base_UserLog userLog = userLogRepository.GetIEnumerable(x => x.ResourceAccessed == Define.USER.UserResource && x.IsDisconected.HasValue && !x.IsDisconected.Value).SingleOrDefault();
                    if (userLog != null)
                    {
                        userLog.DisConnectedOn = DateTimeExt.Now;
                        userLog.IsDisconected = true;
                        userLogRepository.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Logout Fail" + ex.ToString());
            }
        }

        /// <summary>
        /// Check idle time function
        /// </summary>
        private void CheckIdleTime()
        {
            _idleTimer = new DispatcherTimer(DispatcherPriority.SystemIdle);
            _idleTimer.Interval = TimeSpan.FromSeconds(1);
            _idleTimer.Tick += (sender, e) =>
            {
                if (IsIdle())
                {
                    if (App.Current.MainWindow.WindowState.Equals(WindowState.Minimized))
                    {
                        // Stop idle timer
                        _idleTimer.Stop();

                        // Turn on lock screen when window active
                        IsLockScreen = true;
                    }
                    else
                        OnLockScreenCommandExecute();
                }
            };

            // Star idle timer
            _idleTimer.Start();
        }

        /// <summary>
        /// Check system and application idle
        /// </summary>
        /// <returns></returns>
        private bool IsIdle()
        {
            // Check idle to LogOut application if TimeOutMinute is not null
            if (!Define.CONFIGURATION.TimeOutMinute.HasValue)
                return false;

            // Define time out minute value
            TimeSpan activityThreshold = TimeSpan.FromMinutes(Define.CONFIGURATION.TimeOutMinute.Value);
            //TimeSpan activityThreshold = TimeSpan.FromSeconds(5);

            // Get last input time to get system idle time
            TimeSpan machineIdle = IdleTimeHelper.GetIdleTime();

            // Get application idle time
            TimeSpan? appIdle = !IdleTimeHelper.LostFocusTime.HasValue ? null : (TimeSpan?)DateTime.Now.Subtract(IdleTimeHelper.LostFocusTime.Value);

            // Check is system idle
            bool isMachineIdle = machineIdle > activityThreshold;

            // Check is application idle
            bool isAppIdle = appIdle.HasValue && appIdle > activityThreshold;

            return isMachineIdle || isAppIdle;
        }

        /// <summary>
        /// Get connection string info
        /// </summary>
        /// <param name="connectionString">ConnectionString</param>
        /// <param name="server">Server</param>
        /// <param name="userID">UserName</param>
        /// <param name="password">Password</param>
        /// <param name="database">Database</param>
        private void GetConnectionStringInfo(string connectionString)
        {
            //connectionString = connectionString.Replace(";", "; ");
            Regex nameval = new Regex(@"(?<name>[^=]+)\s*=\s*(?<val>[^;]+?)\s*(;|$)",
                RegexOptions.Singleline);

            foreach (Match m in nameval.Matches(connectionString))
            {
                //Console.WriteLine("name=[{0}], val=[{1}]",
                //    m.Groups["name"].ToString(), m.Groups["val"].ToString());

                switch (m.Groups["name"].ToString())
                {
                    //case "Server":
                    //    Server = m.Groups["val"].ToString();
                    //    break;
                    //case "UserID":
                    //    userID = m.Groups["val"].ToString();
                    //    break;
                    //case "Password":
                    //    password = m.Groups["val"].ToString();
                    //    break;
                    case "Database":
                        Database = m.Groups["val"].ToString();
                        break;
                    //case "Port":
                    //    port = m.Groups["val"].ToString();
                    //    break;
                }
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Check view is opened
        /// </summary>
        /// <param name="viewName"></param>
        /// <returns>True is opened</returns>
        public bool IsOpenedView(string viewName)
        {
            return _hostList.Count(x => x.KeyName.Equals(viewName)) > 0;
        }

        /// <summary>
        /// Check is active opened view
        /// </summary>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public bool IsActiveView(string viewName)
        {
            if (!IsOpenedView(viewName))
                return false;
            return _hostList.FirstOrDefault().KeyName.Equals(viewName);
        }

        /// <summary>
        /// Load status information
        /// </summary>
        public void LoadStatusInformation()
        {
            LoadStoreName();
            LoadTaxLocationAndCode();

            // Get login name
            LoginName = Define.USER.LoginName;

            // Get database name
            GetConnectionStringInfo(ConfigurationManager.ConnectionStrings["POSDBEntities"].ConnectionString);
        }

        /// <summary>
        /// Load store name
        /// </summary>
        public void LoadStoreName()
        {
            // Get store name

            IOrderedEnumerable<base_Store> store = _storeRepository.GetAll().OrderBy(x => x.Id);
            if (store != null && store.Count() > 0)
                StoreName = store.ElementAt(Define.StoreCode).Name;
        }

        /// <summary>
        /// Load tax location and tax code
        /// </summary>
        public void LoadTaxLocationAndCode()
        {
            // Get tax location
            base_SaleTaxLocationModel taxLocationModel = null;
            if (Define.CONFIGURATION.DefaultSaleTaxLocation.HasValue && Define.CONFIGURATION.DefaultSaleTaxLocation.Value > 0)
            {
                taxLocationModel = new base_SaleTaxLocationModel(_saleTaxLocationRepository.Get(x => x.Id.Equals(Define.CONFIGURATION.DefaultSaleTaxLocation.Value)));
                // Get tax code
                TaxCode = Define.CONFIGURATION.DefaultTaxCodeNewDepartment;
            }
            if (taxLocationModel != null)
                TaxLocation = taxLocationModel.Name;
            else
            {
                taxLocationModel = _saleTaxLocationRepository.CreateDefaulSaleTaxLocation();
                //Set Default taxCode & TaxLocation
                TaxLocation = taxLocationModel.Name;
                TaxCode = taxLocationModel.TaxCodeModel.TaxCode;
            }


        }

        #endregion

        #region Event Methods

        /// <summary>
        /// Set keybinding after form loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void host_Loaded(object sender, RoutedEventArgs e)
        {
            BorderLayoutHost host = sender as BorderLayoutHost;
            SetKeyBinding(host.View.InputBindings);
        }

        /// <summary>
        /// Shrink current view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tgExpand_Unchecked(object sender, RoutedEventArgs e)
        {
            ExpandItem(sender as ToggleButton);
        }

        /// <summary>
        /// Expand current view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tgExpand_Checked(object sender, RoutedEventArgs e)
        {
            ExpandItem(sender as ToggleButton);
        }

        /// <summary>
        /// Call close view function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            CloseItem(sender as Button);
        }

        /// <summary>
        /// Call change view function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTitle_Click(object sender, RoutedEventArgs e)
        {
            ChangeLayoutItem(sender as Button);
        }

        /// <summary>
        /// Call change view function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImageIcon_Click(object sender, RoutedEventArgs e)
        {
            ChangeLayoutItem(sender as Button);
        }

        /// <summary>
        /// Call expand current view function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTitle_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (IsAnimationCompletedAll)
                _hostList.ElementAt(0).Container.tgExpand.IsChecked = true;
        }

        #endregion

        #region IDataErrorInfo Members

        protected HashSet<string> _extensionErrors = new HashSet<string>();
        /// <summary>
        /// <para> Gets or sets the ExtensionErrors </para>
        /// </summary>
        public HashSet<string> ExtensionErrors
        {
            get
            {
                return _extensionErrors;
            }
            set
            {
                if (_extensionErrors != value)
                {
                    _extensionErrors = value;
                    OnPropertyChanged(() => ExtensionErrors);
                }
            }
        }

        public string Error
        {
            get
            {
                return null;
            }
        }

        public string this[string columnName]
        {
            get
            {
                string message = null;
                this.ExtensionErrors.Clear();

                switch (columnName)
                {
                    case "UserPassword":
                        if (string.IsNullOrEmpty(UserPassword))
                            message = "Password is required.";
                        else if (!_regexPassWord.IsMatch(UserPassword))
                            message = "Password must a-z and length of 3-50 characters";
                        break;
                }

                if (!string.IsNullOrWhiteSpace(message))
                    this.ExtensionErrors.Add(columnName);

                return message;
            }
        }

        #endregion

        #region Permission

        #region Properties

        #region Sale Module

        private bool _allowAccessSaleModule = true;
        /// <summary>
        /// Gets or sets the AllowAccessSaleModule.
        /// </summary>
        public bool AllowAccessSaleModule
        {
            get
            {
                return _allowAccessSaleModule;
            }
            set
            {
                if (_allowAccessSaleModule != value)
                {
                    _allowAccessSaleModule = value;
                    OnPropertyChanged(() => AllowAccessSaleModule);
                }
            }
        }

        private bool _allowAccessCustomer = true;
        /// <summary>
        /// Gets or sets the AllowAccessCustomer.
        /// </summary>
        public bool AllowAccessCustomer
        {
            get
            {
                return _allowAccessCustomer;
            }
            set
            {
                if (_allowAccessCustomer != value)
                {
                    _allowAccessCustomer = value;
                    OnPropertyChanged(() => AllowAccessCustomer);
                }
            }
        }

        private bool _allowAddCustomer = true;
        /// <summary>
        /// Gets or sets the AllowAddCustomer.
        /// </summary>
        public bool AllowAddCustomer
        {
            get
            {
                return _allowAddCustomer;
            }
            set
            {
                if (_allowAddCustomer != value)
                {
                    _allowAddCustomer = value;
                    OnPropertyChanged(() => AllowAddCustomer);
                }
            }
        }

        private bool _allowAccessReward = true;
        /// <summary>
        /// Gets or sets the AllowAddReward.
        /// </summary>
        public bool AllowAccessReward
        {
            get
            {
                return _allowAccessReward;
            }
            set
            {
                if (_allowAccessReward != value)
                {
                    _allowAccessReward = value;
                    OnPropertyChanged(() => AllowAccessReward);
                }
            }
        }

        private bool _allowAccessSaleQuotation = true;
        /// <summary>
        /// Gets or sets the AllowAccessSaleQuotation.
        /// </summary>
        public bool AllowAccessSaleQuotation
        {
            get
            {
                return _allowAccessSaleQuotation;
            }
            set
            {
                if (_allowAccessSaleQuotation != value)
                {
                    _allowAccessSaleQuotation = value;
                    OnPropertyChanged(() => AllowAccessSaleQuotation);
                }
            }
        }

        private bool _allowAddSaleQuotation = true;
        /// <summary>
        /// Gets or sets the AllowAddSaleQuotation.
        /// </summary>
        public bool AllowAddSaleQuotation
        {
            get
            {
                return _allowAddSaleQuotation;
            }
            set
            {
                if (_allowAddSaleQuotation != value)
                {
                    _allowAddSaleQuotation = value;
                    OnPropertyChanged(() => AllowAddSaleQuotation);
                }
            }
        }

        private bool _allowAccessLayaway = true;
        /// <summary>
        /// Gets or sets the AllowAccessLayaway.
        /// </summary>
        public bool AllowAccessLayaway
        {
            get
            {
                return _allowAccessLayaway;
            }
            set
            {
                if (_allowAccessLayaway != value)
                {
                    _allowAccessLayaway = value;
                    OnPropertyChanged(() => AllowAccessLayaway);
                }
            }
        }

        private bool _allowAddLayaway = true;
        /// <summary>
        /// Gets or sets the AllowAddLayaway.
        /// </summary>
        public bool AllowAddLayaway
        {
            get
            {
                return _allowAddLayaway;
            }
            set
            {
                if (_allowAddLayaway != value)
                {
                    _allowAddLayaway = value;
                    OnPropertyChanged(() => AllowAddLayaway);
                }
            }
        }

        private bool _allowAccessWorkOrder = true;
        /// <summary>
        /// Gets or sets the AllowAccessWorkOrder.
        /// </summary>
        public bool AllowAccessWorkOrder
        {
            get
            {
                return _allowAccessWorkOrder;
            }
            set
            {
                if (_allowAccessWorkOrder != value)
                {
                    _allowAccessWorkOrder = value;
                    OnPropertyChanged(() => AllowAccessWorkOrder);
                }
            }
        }

        private bool _allowAddWorkOrder = true;
        /// <summary>
        /// Gets or sets the AllowAddWorkOrder.
        /// </summary>
        public bool AllowAddWorkOrder
        {
            get
            {
                return _allowAddWorkOrder;
            }
            set
            {
                if (_allowAddWorkOrder != value)
                {
                    _allowAddWorkOrder = value;
                    OnPropertyChanged(() => AllowAddWorkOrder);
                }
            }
        }

        private bool _allowAccessSaleOrder = true;
        /// <summary>
        /// Gets or sets the AllowAccessSaleOrder.
        /// </summary>
        public bool AllowAccessSaleOrder
        {
            get
            {
                return _allowAccessSaleOrder;
            }
            set
            {
                if (_allowAccessSaleOrder != value)
                {
                    _allowAccessSaleOrder = value;
                    OnPropertyChanged(() => AllowAccessSaleOrder);
                }
            }
        }

        private bool _allowAddSaleOrder = true;
        /// <summary>
        /// Gets or sets the AllowAddSaleOrder.
        /// </summary>
        public bool AllowAddSaleOrder
        {
            get
            {
                return _allowAddSaleOrder;
            }
            set
            {
                if (_allowAddSaleOrder != value)
                {
                    _allowAddSaleOrder = value;
                    OnPropertyChanged(() => AllowAddSaleOrder);
                }
            }
        }

        private bool _allowAccessLiabilities = true;
        /// <summary>
        /// Gets or sets the AllowAccessLiabilities.
        /// </summary>
        public bool AllowAccessLiabilities
        {
            get
            {
                return _allowAccessLiabilities;
            }
            set
            {
                if (_allowAccessLiabilities != value)
                {
                    _allowAccessLiabilities = value;
                    OnPropertyChanged(() => AllowAccessLiabilities);
                }
            }
        }

        #endregion

        #region Purchase Module

        private bool _allowAccessPurchaseModule = true;
        /// <summary>
        /// Gets or sets the AllowAccessPurchaseModule.
        /// </summary>
        public bool AllowAccessPurchaseModule
        {
            get
            {
                return _allowAccessPurchaseModule;
            }
            set
            {
                if (_allowAccessPurchaseModule != value)
                {
                    _allowAccessPurchaseModule = value;
                    OnPropertyChanged(() => AllowAccessPurchaseModule);
                }
            }
        }

        private bool _allowAccessVendor = true;
        /// <summary>
        /// Gets or sets the AllowAccessVendor.
        /// </summary>
        public bool AllowAccessVendor
        {
            get
            {
                return _allowAccessVendor;
            }
            set
            {
                if (_allowAccessVendor != value)
                {
                    _allowAccessVendor = value;
                    OnPropertyChanged(() => AllowAccessVendor);
                }
            }
        }

        private bool _allowAddVendor = true;
        /// <summary>
        /// Gets or sets the AllowAddVendor.
        /// </summary>
        public bool AllowAddVendor
        {
            get
            {
                return _allowAddVendor;
            }
            set
            {
                if (_allowAddVendor != value)
                {
                    _allowAddVendor = value;
                    OnPropertyChanged(() => AllowAddVendor);
                }
            }
        }

        private bool _allowAccessPurchaseOrder = true;
        /// <summary>
        /// Gets or sets the allowAccessPurchaseOrder.
        /// </summary>
        public bool AllowAccessPurchaseOrder
        {
            get
            {
                return _allowAccessPurchaseOrder;
            }
            set
            {
                if (_allowAccessPurchaseOrder != value)
                {
                    _allowAccessPurchaseOrder = value;
                    OnPropertyChanged(() => AllowAccessPurchaseOrder);
                }
            }
        }

        private bool _allowAddPurchaseOrder = true;
        /// <summary>
        /// Gets or sets the AllowAddPO.
        /// </summary>
        public bool AllowAddPurchaseOrder
        {
            get
            {
                return _allowAddPurchaseOrder;
            }
            set
            {
                if (_allowAddPurchaseOrder != value)
                {
                    _allowAddPurchaseOrder = value;
                    OnPropertyChanged(() => AllowAddPurchaseOrder);
                }
            }
        }

        #endregion

        #region Inventory Module

        private bool _allowAccessInventoryModule = true;
        /// <summary>
        /// Gets or sets the AllowAccessProductModule.
        /// </summary>
        public bool AllowAccessInventoryModule
        {
            get
            {
                return _allowAccessInventoryModule;
            }
            set
            {
                if (_allowAccessInventoryModule != value)
                {
                    _allowAccessInventoryModule = value;
                    OnPropertyChanged(() => AllowAccessInventoryModule);
                }
            }
        }

        private bool _allowAccessProduct = true;
        /// <summary>
        /// Gets or sets the AllowAccessProduct.
        /// </summary>
        public bool AllowAccessProduct
        {
            get
            {
                return _allowAccessProduct;
            }
            set
            {
                if (_allowAccessProduct != value)
                {
                    _allowAccessProduct = value;
                    OnPropertyChanged(() => AllowAccessProduct);
                }
            }
        }

        private bool _allowAddProduct = true;
        /// <summary>
        /// Gets or sets the AllowAddProduct.
        /// </summary>
        public bool AllowAddProduct
        {
            get
            {
                return _allowAddProduct;
            }
            set
            {
                if (_allowAddProduct != value)
                {
                    _allowAddProduct = value;
                    OnPropertyChanged(() => AllowAddProduct);
                }
            }
        }

        private bool _allowAddDepartment = true;
        /// <summary>
        /// Gets or sets the AllowAddDepartment.
        /// </summary>
        public bool AllowAddDepartment
        {
            get
            {
                return _allowAddDepartment;
            }
            set
            {
                if (_allowAddDepartment != value)
                {
                    _allowAddDepartment = value;
                    OnPropertyChanged(() => AllowAddDepartment);
                }
            }
        }

        private bool _allowAccessPricing = true;
        /// <summary>
        /// Gets or sets the AllowAccessPricing.
        /// </summary>
        public bool AllowAccessPricing
        {
            get
            {
                return _allowAccessPricing;
            }
            set
            {
                if (_allowAccessPricing != value)
                {
                    _allowAccessPricing = value;
                    OnPropertyChanged(() => AllowAccessPricing);
                }
            }
        }

        private bool _allowAddPricing = true;
        /// <summary>
        /// Gets or sets the AllowAddPricing.
        /// </summary>
        public bool AllowAddPricing
        {
            get
            {
                return _allowAddPricing;
            }
            set
            {
                if (_allowAddPricing != value)
                {
                    _allowAddPricing = value;
                    OnPropertyChanged(() => AllowAddPricing);
                }
            }
        }

        private bool _allowAccessDiscountProgram = true;
        /// <summary>
        /// Gets or sets the AllowAccessDiscountProgram.
        /// </summary>
        public bool AllowAccessDiscountProgram
        {
            get
            {
                return _allowAccessDiscountProgram;
            }
            set
            {
                if (_allowAccessDiscountProgram != value)
                {
                    _allowAccessDiscountProgram = value;
                    OnPropertyChanged(() => AllowAccessDiscountProgram);
                }
            }
        }

        private bool _allowAddPromotion = true;
        /// <summary>
        /// Gets or sets the AllowAddPromotion.
        /// </summary>
        public bool AllowAddPromotion
        {
            get
            {
                return _allowAddPromotion;
            }
            set
            {
                if (_allowAddPromotion != value)
                {
                    _allowAddPromotion = value;
                    OnPropertyChanged(() => AllowAddPromotion);
                }
            }
        }

        private bool _allowAccessStock = true;
        /// <summary>
        /// Gets or sets the AllowAccessCurrentStock.
        /// </summary>
        public bool AllowAccessStock
        {
            get
            {
                return _allowAccessStock;
            }
            set
            {
                if (_allowAccessStock != value)
                {
                    _allowAccessStock = value;
                    OnPropertyChanged(() => AllowAccessStock);
                }
            }
        }

        private bool _allowViewCurrentStock = true;
        /// <summary>
        /// Gets or sets the AllowViewCurrentStock.
        /// </summary>
        public bool AllowViewCurrentStock
        {
            get
            {
                return _allowViewCurrentStock;
            }
            set
            {
                if (_allowViewCurrentStock != value)
                {
                    _allowViewCurrentStock = value;
                    OnPropertyChanged(() => AllowViewCurrentStock);
                }
            }
        }

        private bool _allowAddCountSheet = true;
        /// <summary>
        /// Gets or sets the AllowAddCountSheet.
        /// </summary>
        public bool AllowAddCountSheet
        {
            get
            {
                return _allowAddCountSheet;
            }
            set
            {
                if (_allowAddCountSheet != value)
                {
                    _allowAddCountSheet = value;
                    OnPropertyChanged(() => AllowAddCountSheet);
                }
            }
        }

        private bool _allowAddTransferStock = true;
        /// <summary>
        /// Gets or sets the AllowAddTransferStock.
        /// </summary>
        public bool AllowAddTransferStock
        {
            get
            {
                return _allowAddTransferStock;
            }
            set
            {
                if (_allowAddTransferStock != value)
                {
                    _allowAddTransferStock = value;
                    OnPropertyChanged(() => AllowAddTransferStock);
                }
            }
        }

        private bool _allowAccessAdjustHistory = true;
        /// <summary>
        /// Gets or sets the AllowAccessAdjustHistory.
        /// </summary>
        public bool AllowAccessAdjustHistory
        {
            get
            {
                return _allowAccessAdjustHistory;
            }
            set
            {
                if (_allowAccessAdjustHistory != value)
                {
                    _allowAccessAdjustHistory = value;
                    OnPropertyChanged(() => AllowAccessAdjustHistory);
                }
            }
        }

        private bool _allowAccessCostAdjustment = true;
        /// <summary>
        /// Gets or sets the AllowAccessCostAdjustment.
        /// </summary>
        public bool AllowAccessCostAdjustment
        {
            get
            {
                return _allowAccessCostAdjustment;
            }
            set
            {
                if (_allowAccessCostAdjustment != value)
                {
                    _allowAccessCostAdjustment = value;
                    OnPropertyChanged(() => AllowAccessCostAdjustment);
                }
            }
        }

        private bool _allowAccessQuantityAdjustment = true;
        /// <summary>
        /// Gets or sets the AllowAccessQuantityAdjustment.
        /// </summary>
        public bool AllowAccessQuantityAdjustment
        {
            get
            {
                return _allowAccessQuantityAdjustment;
            }
            set
            {
                if (_allowAccessQuantityAdjustment != value)
                {
                    _allowAccessQuantityAdjustment = value;
                    OnPropertyChanged(() => AllowAccessQuantityAdjustment);
                }
            }
        }

        #endregion

        #region Configuration Module

        private bool _allowChangeConfiguration = true;
        /// <summary>
        /// Gets or sets the AllowChangeConfiguration.
        /// </summary>
        public bool AllowChangeConfiguration
        {
            get
            {
                return _allowChangeConfiguration;
            }
            set
            {
                if (_allowChangeConfiguration != value)
                {
                    _allowChangeConfiguration = value;
                    OnPropertyChanged(() => AllowChangeConfiguration);
                }
            }
        }

        #endregion

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
                    AllowAccessReward = IsMainStore;

                    AllowAddPurchaseOrder = IsMainStore;

                    AllowAddProduct = IsMainStore;
                    AllowAddPricing = IsMainStore;
                    AllowAddPromotion = IsMainStore;
                    AllowAccessCostAdjustment = IsMainStore;
                    AllowAccessQuantityAdjustment = IsMainStore;
                    AllowChangeConfiguration = IsMainStore;
                }
                else
                {
                    // Get all user rights
                    IEnumerable<string> userRightCodes = Define.USER_AUTHORIZATION.Select(x => x.Code);

                    #region Sale Module

                    // Get access sale module permission
                    this.AllowAccessSaleModule = userRightCodes.Contains("SO100");

                    // Get access customer permission
                    this.AllowAccessCustomer = userRightCodes.Contains("SO100-01") && AllowAccessSaleModule;

                    // Get add/copy customer permission
                    this.AllowAddCustomer = userRightCodes.Contains("SO100-01-01") && AllowAccessCustomer;

                    // Get access sale quotation permission
                    this.AllowAccessSaleQuotation = userRightCodes.Contains("SO100-03") && AllowAccessSaleModule;

                    // Get add/copy sale quotation permission
                    this.AllowAddSaleQuotation = userRightCodes.Contains("SO100-03-01") && AllowAccessSaleQuotation;

                    // Get access layaway permission
                    this.AllowAccessLayaway = userRightCodes.Contains("SO100-05") && AllowAccessSaleModule;

                    // Get add/copy layaway permission
                    this.AllowAddLayaway = userRightCodes.Contains("SO100-05-02") && AllowAccessLayaway;

                    // Get access work order permission
                    this.AllowAccessWorkOrder = userRightCodes.Contains("SO100-06") && AllowAccessSaleModule;

                    // Get add/copy work order permission
                    this.AllowAddWorkOrder = userRightCodes.Contains("SO100-06-02") && AllowAccessWorkOrder;

                    // Get access sale order permission
                    this.AllowAccessSaleOrder = userRightCodes.Contains("SO100-04") && AllowAccessSaleModule;

                    // Get add/copy sale order permission
                    this.AllowAddSaleOrder = userRightCodes.Contains("SO100-04-02") && AllowAccessSaleOrder;

                    this.AllowAccessLiabilities = userRightCodes.Contains("SO100-04-15") && AllowAccessSaleModule;

                    #endregion

                    #region Purchase Module

                    // Get access purchase module permission
                    AllowAccessPurchaseModule = userRightCodes.Contains("PO100");

                    // Get access vendor permission
                    AllowAccessVendor = userRightCodes.Contains("PO100-01") && AllowAccessPurchaseModule;

                    // Get add/copy vendor permission
                    AllowAddVendor = userRightCodes.Contains("PO100-01-01") && AllowAccessVendor;

                    // Get access purchase order permission
                    AllowAccessPurchaseOrder = userRightCodes.Contains("PO100-02") && AllowAccessPurchaseModule;

                    // Get add purchase order permission
                    AllowAddPurchaseOrder = userRightCodes.Contains("PO100-02-02") && AllowAccessPurchaseOrder && IsMainStore;

                    #endregion

                    #region Inventory Module

                    // Get access inventory module permission
                    AllowAccessInventoryModule = userRightCodes.Contains("IV100");

                    // Get access product permission
                    AllowAccessProduct = userRightCodes.Contains("IV100-01") && AllowAccessInventoryModule;

                    // Get add/copy product permission
                    AllowAddProduct = userRightCodes.Contains("IV100-01-01") && AllowAccessProduct && IsMainStore;

                    // Get add/copy department permission
                    AllowAddDepartment = userRightCodes.Contains("IV100-01-03") && AllowAccessProduct;

                    // Get access pricing permission
                    AllowAccessPricing = userRightCodes.Contains("IV100-02") && AllowAccessInventoryModule;

                    // Get add/copy pricing permission
                    AllowAddPricing = userRightCodes.Contains("IV100-02-01") && AllowAccessPricing && IsMainStore;

                    // Get access discount program permission
                    AllowAccessDiscountProgram = userRightCodes.Contains("IV100-03") && AllowAccessInventoryModule;

                    // Get add/copy promotion permission
                    AllowAddPromotion = userRightCodes.Contains("IV100-03-01") && AllowAccessDiscountProgram && IsMainStore;

                    // Get access stock permission
                    AllowAccessStock = userRightCodes.Contains("IV100-04") && AllowAccessInventoryModule;

                    // Get view current stock permission
                    AllowViewCurrentStock = userRightCodes.Contains("IV100-04-01") && AllowAccessStock;

                    // Get add count sheet permission
                    AllowAddCountSheet = userRightCodes.Contains("IV100-04-02") && AllowAccessStock;

                    // Get add transfer stock permission
                    AllowAddTransferStock = userRightCodes.Contains("IV100-04-05") && AllowAccessStock;

                    // Get access adjust history permission
                    AllowAccessAdjustHistory = userRightCodes.Contains("IV100-05") && AllowAccessInventoryModule;

                    // Get access cost adjustment permission
                    AllowAccessCostAdjustment = userRightCodes.Contains("IV100-05-01") && AllowAccessAdjustHistory && IsMainStore;

                    // Get access quantity adjustment permission
                    AllowAccessQuantityAdjustment = userRightCodes.Contains("IV100-05-02") && AllowAccessAdjustHistory && IsMainStore;

                    #endregion

                    #region Configuration Module

                    // Get change configuration permission
                    AllowChangeConfiguration = userRightCodes.Contains("CF100-05") && IsMainStore;

                    #endregion
                }
            }
        }

        #endregion

        #region LoadLiabilities
        private void LoadLiabilities()
        {
            LiabilityNotificationViewModel viewModel = new LiabilityNotificationViewModel();
            LiabilityView view = new LiabilityView();
            view.DataContext = viewModel;
            PopupContainer popup = new PopupContainer(view, true);
            popup.Title = "Thông báo công nợ.";
            //popup.systemButtons.Visibility = Visibility.Visible;
            viewModel.LoadData();
            popup.Closed += new EventHandler(popup_Closed);
            popup.ShowDialog();
            Define.IsOpenLiability = false;
        }
        private void popup_Closed(object sender, EventArgs e)
        {
            Define.IsOpenLiability = false;
        }
        #endregion
    }

    public class ContainerModel
    {
        public string Name { get; set; }
        public string Text { get; set; }
    }
}