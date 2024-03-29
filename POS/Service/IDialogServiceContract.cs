﻿using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Windows;
using System.Windows.Forms;
using CPC.Service.FrameworkDialogs.FolderBrowse;
using CPC.Service.FrameworkDialogs.OpenFile;

namespace CPC.Service
{
    [ContractClassFor(typeof(IDialogService))]
    abstract class IDialogServiceContract : IDialogService
    {
        /// <summary>
        /// Gets the registered views.
        /// </summary>
        public ReadOnlyCollection<FrameworkElement> Views
        {
            get
            {
                return default(ReadOnlyCollection<FrameworkElement>);
            }
        }


        /// <summary>
        /// Registers a View.
        /// </summary>
        /// <param name="view">The registered View.</param>
        public void Register(FrameworkElement view)
        {
            Contract.Requires(view != null);
            Contract.Requires(!Views.Contains(view));
        }


        /// <summary>
        /// Unregisters a View.
        /// </summary>
        /// <param name="view">The unregistered View.</param>
        public void Unregister(FrameworkElement view)
        {
            Contract.Requires(Views.Contains(view));
        }


        /// <summary>
        /// Shows a dialog.
        /// </summary>
        /// <remarks>
        /// The dialog used to represent the ViewModel is retrieved from the registered mappings.
        /// </remarks>
        /// <param name="ownerViewModel">
        /// A ViewModel that represents the owner window of the dialog.
        /// </param>
        /// <param name="viewModel">The ViewModel of the new dialog.</param>
        /// <returns>
        /// A nullable value of type bool that signifies how a window was closed by the user.
        /// </returns>
        public bool? ShowDialog(object ownerViewModel, object viewModel)
        {
            Contract.Requires(ownerViewModel != null);
            Contract.Requires(viewModel != null);

            return default(bool?);
        }


        /// <summary>
        /// Shows a dialog.
        /// </summary>
        /// <param name="ownerViewModel">
        /// A ViewModel that represents the owner window of the dialog.
        /// </param>
        /// <param name="viewModel">The ViewModel of the new dialog.</param>
        /// <typeparam name="T">The type of the dialog to show.</typeparam>
        /// <returns>
        /// A nullable value of type bool that signifies how a window was closed by the user.
        /// </returns>
        public bool? ShowDialog<T>(object ownerViewModel, object viewModel) where T : FrameworkElement
        {
            Contract.Requires(ownerViewModel != null);
            Contract.Requires(viewModel != null);

            return default(bool?);
        }

        /// <summary>
        /// Shows a dialog with title.
        /// </summary>
        /// <param name="ownerViewModel">
        /// A ViewModel that represents the owner window of the dialog.
        /// </param>
        /// <param name="viewModel">The ViewModel of the new dialog.</param>
        /// <typeparam name="T">The type of the dialog to show.</typeparam>
        /// <returns>
        /// A nullable value of type bool that signifies how a window was closed by the user.
        /// </returns>
        public bool? ShowDialog<T>(object ownerViewModel, object viewModel, string title) where T : FrameworkElement
        {
            Contract.Requires(ownerViewModel != null);
            Contract.Requires(viewModel != null);
            Contract.Requires(title != null);

            return default(bool?);
        }

        /// <summary>
        /// Shows a dialog with title.
        /// </summary>
        /// <typeparam name="T">The type of the dialog to show.</typeparam>
        /// <param name="ownerViewModel">A ViewModel that represents the owner window of the dialog.</param>
        /// <param name="viewModel">The ViewModel of the new dialog.</param>
        /// <param name="title">Title of the new dialog.</param>
        /// <param name="isShowMaximize">True show Maximize button, False not show Maximize button.</param>
        /// <param name="isShowMinimize">True show Minimize button, False not show Minimize button.<</param>
        /// <param name="isShowClose">True show Close button, False not show Close button.<</param>
        /// <returns>A nullable value of type bool that signifies how a window was closed by the user.</returns>
        public bool? ShowDialog<T>(object ownerViewModel, object viewModel, string title, bool isShowMaximize, bool isShowMinimize, bool isShowClose) where T : FrameworkElement
        {
            Contract.Requires(ownerViewModel != null);
            Contract.Requires(viewModel != null);
            Contract.Requires(title != null);

            return default(bool?);
        }

        /// <summary>
        /// Shows a message box.
        /// </summary>
        /// <param name="ownerViewModel">
        /// A ViewModel that represents the owner window of the message box.
        /// </param>
        /// <param name="messageBoxText">A string that specifies the text to display.</param>
        /// <param name="caption">A string that specifies the title bar caption to display.</param>
        /// <param name="button">
        /// A MessageBoxButton value that specifies which button or buttons to display.
        /// </param>
        /// <param name="icon">A MessageBoxImage value that specifies the icon to display.</param>
        /// <returns>
        /// A MessageBoxResult value that specifies which message box button is clicked by the user.
        /// </returns>
        public MessageBoxResult ShowMessageBox(
            object ownerViewModel,
            string messageBoxText,
            string caption,
            MessageBoxButton button,
            MessageBoxImage icon)
        {
            Contract.Requires(ownerViewModel != null);
            Contract.Requires(!string.IsNullOrWhiteSpace(messageBoxText));
            Contract.Requires(!string.IsNullOrWhiteSpace(caption));

            return default(MessageBoxResult);
        }


        /// <summary>
        /// Shows the OpenFileDialog.
        /// </summary>
        /// <param name="ownerViewModel">
        /// A ViewModel that represents the owner window of the dialog.
        /// </param>
        /// <param name="openFileDialog">The interface of a open file dialog.</param>
        /// <returns>
        /// DialogResult.OK if successful; otherwise DialogResult.Cancel.
        /// </returns>
        public DialogResult ShowOpenFileDialog(object ownerViewModel, IOpenFileDialog openFileDialog)
        {
            Contract.Requires(ownerViewModel != null);
            Contract.Requires(openFileDialog != null);

            return default(DialogResult);
        }


        /// <summary>
        /// Shows the FolderBrowserDialog.
        /// </summary>
        /// <param name="ownerViewModel">
        /// A ViewModel that represents the owner window of the dialog.
        /// </param>
        /// <param name="folderBrowserDialog">The interface of a folder browser dialog.</param>
        /// <returns>
        /// The DialogResult.OK if successful; otherwise DialogResult.Cancel.
        /// </returns>
        public DialogResult ShowFolderBrowserDialog(object ownerViewModel, IFolderBrowserDialog folderBrowserDialog)
        {
            Contract.Requires(ownerViewModel != null);
            Contract.Requires(folderBrowserDialog != null);

            return default(DialogResult);
        }
    }
}
