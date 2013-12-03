﻿using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using CPC.POS.Model;
using CPC.Toolkit.Base;
using CPC.Toolkit.Command;
using System;
using CPC.POS.Repository;
using System.Collections;
using CPC.POS.Database;
using System.Collections.Generic;
using MessageBoxControl;

namespace CPC.POS.ViewModel
{
    class PopupAddNewGroupViewModel : ViewModelBase
    {
        #region Defines

        private base_GuestGroupRepository _guestGroupRepository = new base_GuestGroupRepository();

        #endregion

        #region Properties

        private base_GuestGroupModel _selectedGuestGroup;
        /// <summary>
        /// Gets or sets the SelectedGuestGroup.
        /// </summary>
        public base_GuestGroupModel SelectedGuestGroup
        {
            get { return _selectedGuestGroup; }
            set
            {
                if (_selectedGuestGroup != value)
                {
                    _selectedGuestGroup = value;
                    OnPropertyChanged(() => SelectedGuestGroup);
                }
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public PopupAddNewGroupViewModel()
            : base()
        {
            InitialCommand();

            // Create new guest group
            SelectedGuestGroup = new base_GuestGroupModel();
            SelectedGuestGroup.DateCreated = DateTimeExt.Now;
            SelectedGuestGroup.UserCreated = Define.USER.LoginName;
            Guid guid = Guid.NewGuid();
            SelectedGuestGroup.Resource = guid;
            SelectedGuestGroup.GuestGroupResource = guid.ToString();

            // Turn off IsDirty
            SelectedGuestGroup.IsDirty = false;
        }

        #endregion

        #region Command Methods

        #region OkCommand

        /// <summary>
        /// Gets the OkCommand command.
        /// </summary>
        public ICommand OkCommand { get; private set; }

        /// <summary>
        /// Method to check whether the OkCommand command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnOkCommandCanExecute()
        {
            if (SelectedGuestGroup == null)
                return false;
            return SelectedGuestGroup.IsDirty;
        }

        /// <summary>
        /// Method to invoke when the OkCommand command is executed.
        /// </summary>
        private void OnOkCommandExecute()
        {
            try
            {
                if (IsExistedName(SelectedGuestGroup))
                {
                    MessageBoxResultCustom msgResult = MsgControl.ShowWarning("Tên này đã tồn tại.", "Cảnh báo", MessageBoxButtonCustom.OK);
                }
                else
                {
                    // Map data from model to entity
                    SelectedGuestGroup.ToEntity();

                    // Add new guest group to database
                    _guestGroupRepository.Add(SelectedGuestGroup.base_GuestGroup);

                    // Accept changes
                    _guestGroupRepository.Commit();

                    SelectedGuestGroup.Id = SelectedGuestGroup.base_GuestGroup.Id;

                    Window window = FindOwnerWindow(this);
                    window.DialogResult = true;
                }
            }
            catch (Exception ex)
            {
                MsgControl.ShowWarning(ex.ToString(), "Lỗi", MessageBoxButtonCustom.OK);
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
            Window window = FindOwnerWindow(this);
            window.DialogResult = false;
        }

        #endregion

        #endregion

        #region Private Methods

        /// <summary>
        /// Initial commands for binding on form
        /// </summary>
        private void InitialCommand()
        {
            OkCommand = new RelayCommand(OnOkCommandExecute, OnOkCommandCanExecute);
            CancelCommand = new RelayCommand(OnCancelCommandExecute, OnCancelCommandCanExecute);
        }

        /// <summary>
        /// Check name is existed
        /// </summary>
        /// <param name="guestGroupModel"></param>
        /// <returns></returns>
        private bool IsExistedName(base_GuestGroupModel guestGroupModel)
        {
            IEnumerable<base_GuestGroup> guestGroups = _guestGroupRepository.GetAll(x => x.Name.Equals(guestGroupModel.Name));
            if (guestGroups == null)
                return false;
            return guestGroups.Count() > 0;
        }

        #endregion
    }
}