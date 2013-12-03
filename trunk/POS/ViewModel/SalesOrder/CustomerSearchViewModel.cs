using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CPC.Toolkit.Base;
using CPC.Toolkit.Command;
using System.Collections.ObjectModel;
using CPC.POS.Model;
using System.ComponentModel;
using System.Windows.Data;
using CPC.POS.Repository;
using CPC.POS.View;
using CPC.Helper;

namespace CPC.POS.ViewModel
{
    class CustomerSearchViewModel : ViewModelBase
    {
        #region Define
        private ICollectionView _customerCollectionView;
        private base_GuestGroupRepository _guestGroupRepository = new base_GuestGroupRepository();
        private BackgroundWorker bgWorker = new BackgroundWorker { WorkerReportsProgress = true };
        public enum ActionView
        {
            Cancel = 0,
            SelectedItem = 1,
            NewCustomer = 2,
            GoToList = 3
        }

        public ActionView CurrentViewAction { get; set; }
        public object ParentViewModel { get; set; }
        #endregion

        #region Constructors
        public CustomerSearchViewModel(ObservableCollection<base_GuestModel> customerSource, object parent)
            : base()
        {
            _ownerViewModel = this;
            this.ParentViewModel = parent;
            InitialCommand();
            _customerCollectionSource = new ObservableCollection<base_GuestModel>(customerSource.ToList());
            CustomerCollection = customerSource;
            TotalCustomer = CustomerCollection.Count();
            bgWorker.DoWork += new DoWorkEventHandler(bgWorker_DoWork);
            bgWorker.ProgressChanged += new ProgressChangedEventHandler(bgWorker_ProgressChanged);
            bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgWorker_RunWorkerCompleted);
        }


        #endregion

        #region Properties

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
                    OnSearchCommandExecute(null);
                }
            }
        }
        #endregion

        #region Keyword
        private string _keyword;
        /// <summary>
        /// Gets or sets the Keyword.
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
        #endregion

        #region SearchAlert
        private string _searchAlert;
        /// <summary>
        /// Gets or sets the SearchAler.
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

        #region CustomerCollectionSource
        private ObservableCollection<base_GuestModel> _customerCollectionSource;
        /// <summary>
        /// Gets or sets the CustomerCollectionSource.
        /// </summary>
        public ObservableCollection<base_GuestModel> CustomerCollectionSource
        {
            get { return _customerCollectionSource; }
            set
            {
                if (_customerCollectionSource != value)
                {
                    _customerCollectionSource = value;
                    OnPropertyChanged(() => CustomerCollectionSource);
                }
            }
        }
        #endregion

        #region GuestGroupCollection
        private ObservableCollection<base_GuestGroupModel> _guestGroupCollection;
        /// <summary>
        /// Gets or sets the GuestGroupCollection.
        /// </summary>
        public ObservableCollection<base_GuestGroupModel> GuestGroupCollection
        {
            get { return _guestGroupCollection; }
            set
            {
                if (_guestGroupCollection != value)
                {
                    _guestGroupCollection = value;
                    OnPropertyChanged(() => GuestGroupCollection);
                }
            }
        }
        #endregion

        #region CustomerCollection
        private ObservableCollection<base_GuestModel> _customerCollection = new ObservableCollection<base_GuestModel>();
        /// <summary>
        /// Gets or sets the CustomerCollection.
        /// </summary>
        public ObservableCollection<base_GuestModel> CustomerCollection
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

        #region SelectedCustomer
        private base_GuestModel _selectedCustomer;
        /// <summary>
        /// Gets or sets the SelectedCustomer.
        /// </summary>
        public base_GuestModel SelectedCustomer
        {
            get { return _selectedCustomer; }
            set
            {
                if (_selectedCustomer != value)
                {
                    _selectedCustomer = value;
                    OnPropertyChanged(() => SelectedCustomer);
                }
            }
        }
        #endregion

        #region TotalCustomer
        private int _totalCustomer;
        /// <summary>
        /// Gets or sets the TotalCustomer.
        /// </summary>
        public int TotalCustomer
        {
            get { return _totalCustomer; }
            set
            {
                if (_totalCustomer != value)
                {
                    _totalCustomer = value;
                    OnPropertyChanged(() => TotalCustomer);
                }
            }
        }
        #endregion
        #endregion

        #region Commands Methods

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
            if (CustomerCollectionSource != null)
            {
                SearchWithCollectionSource();
            }
        }

        #endregion

        #region NewCustomerCommand

        /// <summary>
        /// Gets the CreatedNewCustomer Command.
        /// <summary>

        public RelayCommand<object> NewCustomerCommand { get; private set; }



        /// <summary>
        /// Method to check whether the CreatedNewCustomer command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnNewCustomerCommandCanExecute(object param)
        {
            return true;
        }


        /// <summary>
        /// Method to invoke when the CreatedNewCustomer command is executed.
        /// </summary>
        private void OnNewCustomerCommandExecute(object param)
        {
            CurrentViewAction = ActionView.NewCustomer;
            FindOwnerWindow(_ownerViewModel).DialogResult = true;
        }
        #endregion

        #region GoToCustomerListCommand
        /// <summary>
        /// Gets the GotoCustomerList Command.
        /// <summary>

        public RelayCommand<object> GotoCustomerListCommand { get; private set; }



        /// <summary>
        /// Method to check whether the GotoCustomerList command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnGotoCustomerListCommandCanExecute(object param)
        {
            return true;
        }


        /// <summary>
        /// Method to invoke when the GotoCustomerList command is executed.
        /// </summary>
        private void OnGotoCustomerListCommandExecute(object param)
        {
            if ((ParentViewModel as SalesOrderViewModel).ChangeViewExecute(false))
            {
                CurrentViewAction = ActionView.GoToList;
                FindOwnerWindow(_ownerViewModel).DialogResult = true;
            }
        }
        #endregion

        #region SelectedItemCommand
        /// <summary>
        /// Gets the SelectedItem Command.
        /// <summary>

        public RelayCommand<object> SelectedItemCommand { get; private set; }



        /// <summary>
        /// Method to check whether the SelectedItem command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnSelectedItemCommandCanExecute(object param)
        {
            if (SelectedCustomer == null)
                return false;
            return true;
        }


        /// <summary>
        /// Method to invoke when the SelectedItem command is executed.
        /// </summary>
        private void OnSelectedItemCommandExecute(object param)
        {
            CurrentViewAction = ActionView.SelectedItem;
            FindOwnerWindow(_ownerViewModel).DialogResult = true;
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
            CurrentViewAction = ActionView.Cancel;
            FindOwnerWindow(_ownerViewModel).DialogResult = false;
        }
        #endregion

        #endregion

        #region Private Methods
        private void InitialCommand()
        {
            SearchCommand = new RelayCommand<object>(OnSearchCommandExecute, OnSearchCommandCanExecute);
            NewCustomerCommand = new RelayCommand<object>(OnNewCustomerCommandExecute, OnNewCustomerCommandCanExecute);
            GotoCustomerListCommand = new RelayCommand<object>(OnGotoCustomerListCommandExecute, OnGotoCustomerListCommandCanExecute);
            SelectedItemCommand = new RelayCommand<object>(OnSelectedItemCommandExecute, OnSelectedItemCommandCanExecute);
            CancelCommand = new RelayCommand<object>(OnCancelCommandExecute, OnCancelCommandCanExecute);
        }

        private void LoadDynamicData()
        {
            // Load guest group collection
            GuestGroupCollection = new ObservableCollection<base_GuestGroupModel>(_guestGroupRepository.GetAll().
                Select(x => new base_GuestGroupModel(x) { GuestGroupResource = x.Resource.ToString() }));
        }

        private void SearchWithCollectionSource()
        {
            if (!string.IsNullOrWhiteSpace(Keyword) && SearchOption == 0)
            {
                SearchAlert = Language.GetMsg("SO_Message_SearchOptionRequired");
                return;
            }

            if (!bgWorker.IsBusy)
            {
                SearchAlert = string.Empty;
                CustomerCollection.Clear();
                bgWorker.RunWorkerAsync();
            }

        }
        #endregion

        #region Event Methods
        void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            TotalCustomer = CustomerCollection.Count();
            IsBusy = false;
        }

        void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            CustomerCollection.Add((base_GuestModel)e.UserState);
        }

        void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (Define.DisplayLoading)
                IsBusy = true;

            if (!string.IsNullOrWhiteSpace(Keyword) && SearchOption == 0)
            {
                SearchAlert = Language.GetMsg("SO_Message_SearchOptionRequired");
                return;
            }

            foreach (base_GuestModel guestModel in CustomerCollectionSource)
            {
                bool result = true;
                if (guestModel == null)
                {
                    result = false;
                    continue;
                }
                if (string.IsNullOrWhiteSpace(Keyword))
                {
                    result = true;
                }
                else
                {
                    if (SearchOption.Has(SearchOptions.AccountNum))
                    {
                        result &= guestModel.GuestNo.Contains(Keyword.ToLower());
                    }

                    if (SearchOption.Has(SearchOptions.FirstName))
                    {
                        result &= string.IsNullOrWhiteSpace(guestModel.FirstName) && guestModel.FirstName.ToLower().Contains(Keyword.ToLower());
                    }

                    if (SearchOption.Has(SearchOptions.LastName))
                    {
                        result &= string.IsNullOrWhiteSpace(guestModel.LastName) && guestModel.LastName.ToLower().Contains(Keyword.ToLower());
                    }

                    if (SearchOption.Has(SearchOptions.Company))
                    {
                        result &= !string.IsNullOrWhiteSpace(guestModel.Company) && guestModel.Company.ToLower().Contains(Keyword.ToLower());
                    }

                    if (SearchOption.Has(SearchOptions.Phone))
                    {
                        result &= guestModel.Phone1.Contains(Keyword.ToLower()) || guestModel.Phone2.Contains(Keyword.ToLower());
                    }

                    if (SearchOption.Has(SearchOptions.Group))
                    {
                        // Get all guest group that contain keyword
                        IEnumerable<base_GuestGroupModel> guestGroups = GuestGroupCollection.Where(x => x.Name != string.Empty && x.Name.ToLower().Contains(Keyword.ToLower()));
                        IEnumerable<string> guestGroupResourceList = guestGroups.Select(x => x.Resource.ToString());

                        // Get all product that contain in guest group resource list
                        if (guestGroupResourceList.Count() > 0)
                        {
                            result &= guestGroupResourceList.Contains(guestModel.GroupResource);
                        }
                        else
                            // If condition in predicate is false, GetRange function can not get data from database.
                            // Solution for this problem is create fake condition
                            result &= false;
                    }

                    //Search Member Ship
                    //if (true)
                    //{ 
                    //    short memebershipActivedStatus = (short)MemberShipStatus.Actived;
                    //    bool value= guestModel.base_Guest.base_MemberShip.Any(x => x.Status.Equals(memebershipActivedStatus) && x.IdCard != string.Empty && x.IdCard.ToLower().Equals(Keyword.ToLower()));
                    //    result &= value;
                    //}
                }

                if (result)
                {
                    bgWorker.ReportProgress(0, guestModel);
                }
            }
        }
        #endregion
    }


}

