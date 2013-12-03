using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CPC.Toolkit.Base;
using CPC.POS.Model;
using System.Windows.Input;
using CPC.Toolkit.Command;
using CPC.POS.View;
using System.Windows.Data;

namespace CPC.POS.ViewModel
{
    class VendorSearchViewModel : ViewModelBase
    {
        #region Fields

        CollectionBase<base_GuestModel> _vendorCollectionRoot;

        #endregion

        #region Constructors

        public VendorSearchViewModel(CollectionBase<base_GuestModel> vendorCollection)
        {
            _ownerViewModel = App.Current.MainWindow.DataContext;
            _vendorCollectionRoot = vendorCollection;
            VendorCollection = new CollectionBase<base_GuestModel>(vendorCollection);
        }

        #endregion

        #region Properties

        #region VendorCollection

        private CollectionBase<base_GuestModel> _vendorCollection;
        /// <summary>
        /// Gets or sets VendorCollection.
        /// </summary>
        public CollectionBase<base_GuestModel> VendorCollection
        {
            get
            {
                return _vendorCollection;
            }
            set
            {
                if (_vendorCollection != value)
                {
                    _vendorCollection = value;
                    OnPropertyChanged(() => VendorCollection);
                }
            }
        }

        #endregion

        #region SelectedVendor

        private base_GuestModel _selectedVendor;
        public base_GuestModel SelectedVendor
        {
            get
            {
                return _selectedVendor;
            }
            set
            {
                if (_selectedVendor != value)
                {
                    _selectedVendor = value;
                    OnPropertyChanged(() => SelectedVendor);
                }
            }
        }

        #endregion

        #region HasGuestNo

        private bool _hasGuestNo;
        public bool HasGuestNo
        {
            get
            {
                return _hasGuestNo;
            }
            set
            {
                if (_hasGuestNo != value)
                {
                    _hasGuestNo = value;
                    OnPropertyChanged(() => HasGuestNo);
                    Search();
                }
            }
        }

        #endregion

        #region HasFirstName

        private bool _hasFirstName;
        public bool HasFirstName
        {
            get
            {
                return _hasFirstName;
            }
            set
            {
                if (_hasFirstName != value)
                {
                    _hasFirstName = value;
                    OnPropertyChanged(() => HasFirstName);
                    Search();
                }
            }
        }

        #endregion

        #region HasLastName

        private bool _hasLastName;
        public bool HasLastName
        {
            get
            {
                return _hasLastName;
            }
            set
            {
                if (_hasLastName != value)
                {
                    _hasLastName = value;
                    OnPropertyChanged(() => HasLastName);
                    Search();
                }
            }
        }

        #endregion

        #region HasCompany

        private bool _hasCompany;
        public bool HasCompany
        {
            get
            {
                return _hasCompany;
            }
            set
            {
                if (_hasCompany != value)
                {
                    _hasCompany = value;
                    OnPropertyChanged(() => HasCompany);
                    Search();
                }
            }
        }

        #endregion

        #region HasGroup

        private bool _hasGroup;
        public bool HasGroup
        {
            get
            {
                return _hasGroup;
            }
            set
            {
                if (_hasGroup != value)
                {
                    _hasGroup = value;
                    OnPropertyChanged(() => HasGroup);
                    Search();
                }
            }
        }

        #endregion

        #region HasEmail

        private bool _hasEmail;
        public bool HasEmail
        {
            get
            {
                return _hasEmail;
            }
            set
            {
                if (_hasEmail != value)
                {
                    _hasEmail = value;
                    OnPropertyChanged(() => HasEmail);
                    Search();
                }
            }
        }

        #endregion

        #region HasPhone

        private bool _hasPhone;
        public bool HasPhone
        {
            get
            {
                return _hasPhone;
            }
            set
            {
                if (_hasPhone != value)
                {
                    _hasPhone = value;
                    OnPropertyChanged(() => HasPhone);
                    Search();
                }
            }
        }

        #endregion

        #region Key

        private string _key;
        public string Key
        {
            get
            {
                return _key;
            }
            set
            {
                if (_key != value)
                {
                    _key = value;
                    OnPropertyChanged(() => Key);
                }
            }
        }

        #endregion

        #endregion

        #region Command Properties

        #region SearchCommand

        private ICommand _searchCommand;
        public ICommand SearchCommand
        {
            get
            {
                if (_searchCommand == null)
                {
                    _searchCommand = new RelayCommand(SearchExecute);
                }
                return _searchCommand;
            }
        }

        #endregion

        #region AddVendorCommand

        private ICommand _addVendorCommand;
        /// <summary>
        /// When 'Add New' Button in ComboBox clicked, AddVendorCommand will executes.
        /// </summary>
        public ICommand AddVendorCommand
        {
            get
            {
                if (_addVendorCommand == null)
                {
                    _addVendorCommand = new RelayCommand(AddVendorExecute);
                }
                return _addVendorCommand;
            }
        }

        #endregion

        #region OpenVendorViewCommand

        private ICommand _openVendorViewCommand;
        public ICommand OpenVendorViewCommand
        {
            get
            {
                if (_openVendorViewCommand == null)
                {
                    _openVendorViewCommand = new RelayCommand(OpenVendorViewExecute);
                }
                return _openVendorViewCommand;
            }
        }

        #endregion

        #region SelectCommand

        private ICommand _selectCommand;
        public ICommand SelectCommand
        {
            get
            {
                if (_selectCommand == null)
                {
                    _selectCommand = new RelayCommand(SelectExecute, CanSelectExecute);
                }
                return _selectCommand;
            }
        }

        #endregion

        #region CancelCommand

        private ICommand _cancelCommand;
        /// <summary>
        /// Cancel.
        /// </summary>
        public ICommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                {
                    _cancelCommand = new RelayCommand(CancelExecute);
                }
                return _cancelCommand;
            }
        }

        #endregion

        #endregion

        #region Command Methods

        #region SearchExecute

        private void SearchExecute()
        {
            Search();
        }

        #endregion

        #region AddVendorExecute

        /// <summary>
        /// Add new vendor.
        /// </summary>
        private void AddVendorExecute()
        {
            AddVendor();
        }

        #endregion

        #region OpenVendorViewExecute

        private void OpenVendorViewExecute()
        {
            OpenVendorView();
        }

        #endregion

        #region SelectExecute

        private void SelectExecute()
        {
            Select();
        }

        #endregion

        #region CanSelectExecute

        private bool CanSelectExecute()
        {
            return _selectedVendor != null;
        }

        #endregion

        #region CancelExecute

        /// <summary>
        /// Cancel.
        /// </summary>
        private void CancelExecute()
        {
            Cancel();
        }

        #endregion

        #endregion

        #region Property Changed Methods

        #endregion

        #region Private Methods

        #region Search

        /// <summary>
        /// Search.
        /// </summary>
        private void Search()
        {
            if (string.IsNullOrEmpty(_key))
            {
                ClearFilter();
                return;
            }

            bool flag = _hasGuestNo | _hasFirstName | _hasLastName | _hasCompany | _hasGroup | _hasEmail | _hasPhone;
            if (!flag)
            {
                return;
            }

            ListCollectionView vendorCollectionView = CollectionViewSource.GetDefaultView(VendorCollection) as ListCollectionView;
            if (vendorCollectionView != null)
            {
                vendorCollectionView.Filter = (item) =>
                {
                    base_GuestModel vendor = item as base_GuestModel;
                    flag = true;

                    if (_hasGuestNo)
                    {
                        flag = !string.IsNullOrWhiteSpace(vendor.GuestNo) && vendor.GuestNo.ToLower().Contains(_key.ToLower());
                        if (!flag)
                        {
                            return flag;
                        }
                    }

                    if (_hasFirstName)
                    {
                        flag = !string.IsNullOrWhiteSpace(vendor.FirstName) && vendor.FirstName.ToLower().Contains(_key.ToLower());
                        if (!flag)
                        {
                            return flag;
                        }
                    }

                    if (_hasLastName)
                    {
                        flag = !string.IsNullOrWhiteSpace(vendor.LastName) && vendor.LastName.ToLower().Contains(_key.ToLower());
                        if (!flag)
                        {
                            return flag;
                        }
                    }

                    if (_hasCompany)
                    {
                        flag = !string.IsNullOrWhiteSpace(vendor.Company) && vendor.Company.ToLower().Contains(_key.ToLower());
                        if (!flag)
                        {
                            return flag;
                        }
                    }

                    if (_hasGroup)
                    {
                        flag = !string.IsNullOrWhiteSpace(vendor.GroupName) && vendor.GroupName.ToLower().Contains(_key.ToLower());
                        if (!flag)
                        {
                            return flag;
                        }
                    }

                    if (_hasEmail)
                    {
                        flag = !string.IsNullOrWhiteSpace(vendor.Email) && vendor.Email.ToLower().Contains(_key.ToLower());
                        if (!flag)
                        {
                            return flag;
                        }
                    }

                    if (_hasPhone)
                    {
                        flag = !string.IsNullOrWhiteSpace(vendor.Phone1) && vendor.Phone1.ToLower().Contains(_key.ToLower());
                        if (!flag)
                        {
                            return flag;
                        }
                    }

                    return flag;
                };
            }
        }

        #endregion

        #region AddVendor

        /// <summary>
        /// Add new vendor.
        /// </summary>
        private void AddVendor()
        {
            PopupGuestViewModel popupGuestViewModel = new PopupGuestViewModel();
            _dialogService.ShowDialog<PopupGuestView>(_ownerViewModel, popupGuestViewModel, "Add Vendor");
            base_GuestModel newVendor = popupGuestViewModel.NewItem;
            // Add new vendor to VendorCollection.
            if (newVendor != null)
            {
                _vendorCollection.Add(newVendor);
                _vendorCollectionRoot.Add(newVendor);
            }
        }

        #endregion

        #region OpenVendorView

        /// <summary>
        /// Open vendor view.
        /// </summary>
        private void OpenVendorView()
        {
            Close(false);
            (_ownerViewModel as MainViewModel).OpenViewExecute("Vendor");
        }

        #endregion

        #region Select

        /// <summary>
        /// Select.
        /// </summary>
        private void Select()
        {
            Close(true);
        }

        #endregion

        #region Cancel

        /// <summary>
        /// Cancel.
        /// </summary>
        private void Cancel()
        {
            Close(false);
        }

        #endregion

        #region Close

        /// <summary>
        /// Close popup.
        /// </summary>
        private void Close(bool result)
        {
            FindOwnerWindow(this).DialogResult = result;
        }

        #endregion

        #region ClearFilter

        private void ClearFilter()
        {
            ListCollectionView vendorCollectionView = CollectionViewSource.GetDefaultView(VendorCollection) as ListCollectionView;
            if (vendorCollectionView != null)
            {
                vendorCollectionView.Filter = null;
            }
        }

        #endregion

        #endregion

        #region Override Methods

        #region LoadData

        public override void LoadData()
        {

        }

        #endregion

        #region OnViewChangingCommandCanExecute

        protected override bool OnViewChangingCommandCanExecute(bool isClosing)
        {
            return true;
        }

        #endregion

        #endregion
    }
}
