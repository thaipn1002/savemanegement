using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using CPC.POS.Database;
using CPC.POS.View;
using CPC.Toolkit.Base;
using MessageBoxControl;

namespace CPC.POS.ViewModel
{
    class SplashScreenViewModel : ViewModelBase
    {
        #region Defines

        private BackgroundWorker _bgWorkerSplashScreen = new BackgroundWorker();
        private DispatcherTimer _timerSplashScreen = new DispatcherTimer();
        private SplashScreenView _splashScreenView;

        private string _messageCheckingConnection = ".";
        private string _messageConnectionFailed = "Connect failed.";
        private int _numberOfContinueChar = 15;
        private char _continueChar = '.';

        #endregion

        #region Properties

        private bool _canConnectDB;
        /// <summary>
        /// Gets or sets the CanConnectDB.
        /// </summary>
        public bool CanConnectDB
        {
            get { return _canConnectDB; }
            set
            {
                if (_canConnectDB != value)
                {
                    _canConnectDB = value;
                    OnPropertyChanged(() => CanConnectDB);
                }
            }
        }

        private string _statusMessage;
        /// <summary>
        /// Gets or sets the StatusMessage.
        /// </summary>
        public string StatusMessage
        {
            get { return _statusMessage; }
            set
            {
                if (_statusMessage != value)
                {
                    _statusMessage = value;
                    OnPropertyChanged(() => StatusMessage);
                }
            }
        }

        #endregion

        #region Constructors

        public SplashScreenViewModel(SplashScreenView splashScreenView)
        {
            // Set default status message
            this.StatusMessage = _messageCheckingConnection;

            _timerSplashScreen.Tick += (sender, e) =>
            {
                // Display splash screen specified period time
                if (this.StatusMessage.Count(x => x.Equals(_continueChar)) == this._numberOfContinueChar)
                {
                    if (!_bgWorkerSplashScreen.IsBusy)
                        _bgWorkerSplashScreen.RunWorkerAsync();
                    // Reset status message
                    this.StatusMessage = _messageCheckingConnection;
                }
                else
                    // Animate status message
                    this.StatusMessage += this._messageCheckingConnection;
                
            };

            // Set interval for timer
            _timerSplashScreen.Interval = TimeSpan.FromSeconds(0.5);

            // Star timer
            _timerSplashScreen.Start();

            _bgWorkerSplashScreen.DoWork += (sender, e) =>
            {
                _splashScreenView = splashScreenView;

                // Check connection to database
                CanConnectDB = CheckConnectionDB();
            };
            _bgWorkerSplashScreen.RunWorkerCompleted += (sender, e) =>
            {
                // Stop timer
                _timerSplashScreen.Stop();

                // Show alert message if connection failed
                if (!CanConnectDB)
                {
                    StatusMessage = _messageConnectionFailed;
                    MessageBoxResultCustom msgResult = MsgControl.ShowWarning(StatusMessage, "POS", MessageBoxButtonCustom.OK);
                }

                // Close splash screen view
                _splashScreenView.DialogResult = CanConnectDB;
            };
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Check connection to database
        /// </summary>
        /// <returns></returns>
        private static bool CheckConnectionDB()
        {
            bool result = false;

            POSEntities objectContext = new POSEntities(ConfigurationManager.ConnectionStrings["POSDBEntities"].ConnectionString);

            try
            {
                // Check connection
                objectContext.Connection.Open();
                // Connection completed
                result = true;
            }
            catch
            {
                result = false;
            }
            finally
            {
                // Enforce close connnection
                if (objectContext.Connection.State.Equals(ConnectionState.Open))
                    objectContext.Connection.Close();
            }

            return result;
        }

        #endregion
    }
}