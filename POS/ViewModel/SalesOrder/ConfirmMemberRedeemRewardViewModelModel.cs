using System.Linq;
using CPC.POS.Model;
using CPC.POS.Repository;
using CPC.Toolkit.Base;
using CPC.Toolkit.Command;
using System.Windows;
using CPC.POS.View;
using CPC.Control;
using System;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Threading;
using CPC.POS.Database;

namespace CPC.POS.ViewModel
{
    class ConfirmMemberRedeemRewardViewModel : ViewModelBase
    {
        #region Define
        public RelayCommand ApplyCommand { get; private set; }
        public RelayCommand RedeemCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }
        private base_RewardManagerRepository _rewardManagerRepository = new base_RewardManagerRepository();
        private DispatcherTimer _resetTimer = new DispatcherTimer();
        public enum ReeedemRewardType
        {
            Apply = 1,
            Redeemded = 2,//redeem later
            Cancel = 3
        }
        public enum ValidateType
        {
            None = 0,
            Success = 1,
            Fail = 2,
            NotAny = 3,
            Existed = 4
        }



        public ConfirmMemberRedeemRewardView ConfirmMemberRedeemRewardView { get; set; }
        #endregion

        #region Constructors
        public ConfirmMemberRedeemRewardViewModel(base_SaleOrderModel saleOrderModel)
            : base()
        {
            _ownerViewModel = this;
            InitialCommand();
            SaleOrderModel = saleOrderModel;
            
            //Load Member ship if null
            //For Form SaleOrder is Existed & Membership is just created;
            short memebershipActivedStatus = (short)MemberShipStatus.Actived;
            if (saleOrderModel.GuestModel.MembershipValidated == null)
            {
                base_MemberShip membership = saleOrderModel.GuestModel.base_Guest.base_MemberShip.FirstOrDefault(x => x.Status.Equals(memebershipActivedStatus));
                if (membership != null)
                    saleOrderModel.GuestModel.MembershipValidated = new base_MemberShipModel(membership);
            }

            //_resetTimer.Interval = new TimeSpan(0, 0, 0, 0, 50);
            //_resetTimer.Tick += new EventHandler(_resetTimer_Tick);
        }


        #endregion

        #region Properties

        #region SaleOrderModel

        private base_SaleOrderModel _saleOrderModel;
        /// <summary>
        /// Gets or sets the SaleOrderModel

        /// </summary>
        public base_SaleOrderModel SaleOrderModel
        {
            get { return _saleOrderModel; }
            set
            {
                if (_saleOrderModel != value)
                {
                    _saleOrderModel = value;
                    OnPropertyChanged(() => SaleOrderModel);
                }
            }
        }
        #endregion

        public ReeedemRewardType ViewActionType { get; set; }

        #region SelectedReward
        private base_GuestRewardModel _selectedReward;
        /// <summary>
        /// Gets or sets the SelectedReward.
        /// </summary>
        public base_GuestRewardModel SelectedReward
        {
            get { return _selectedReward; }
            set
            {
                if (_selectedReward != value)
                {
                    _selectedReward = value;
                    OnPropertyChanged(() => SelectedReward);
                }
            }
        }
        #endregion

        #region ValidMember
        private int _validMember;
        /// <summary>
        /// Gets or sets the ValidMember.
        /// None = 0;
        /// Success=1
        /// Fail = 2
        /// </summary>
        public int ValidMember
        {
            get { return _validMember; }
            set
            {
                if (_validMember != value)
                {
                    _validMember = value;
                    OnPropertyChanged(() => ValidMember);
                }
            }
        }
        #endregion

        #region IDCardNumber
        private string _idCardNumber;
        /// <summary>
        /// Gets or sets the IDCardNumber.
        /// </summary>
        public string IDCardNumber
        {
            get { return _idCardNumber; }
            set
            {
                if (_idCardNumber != value)
                {
                    _idCardNumber = value;
                    OnPropertyChanged(() => IDCardNumber);
                }
            }
        }
        #endregion

        #region Message
        private string _message = string.Empty;
        /// <summary>
        /// Gets or sets the Message.
        /// </summary>
        public string Message
        {
            get { return _message; }
            set
            {
                if (_message != value)
                {
                    _message = value;
                    OnPropertyChanged(() => Message);
                }
            }
        }
        #endregion

        #endregion

        #region Commands Methods

        #region ApplyCommand
        /// <summary>
        /// Method to check whether the NewCommand command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnApplyCommandCanExecute()
        {

            if (SaleOrderModel == null)
                return false;
            return
                 SaleOrderModel.GuestModel.MembershipValidated != null
                 && ValidMember.Is(ValidateType.Success)
                && SaleOrderModel.GuestModel.GuestRewardCollection != null
                && SaleOrderModel.GuestModel.GuestRewardCollection.Any(x => x.IsChecked);
        }

        /// <summary>
        /// Method to invoke when the NewCommand command is executed.
        /// </summary>
        private void OnApplyCommandExecute()
        {
            ViewActionType = ReeedemRewardType.Apply;
            FindOwnerWindow(_ownerViewModel).DialogResult = true;
        }
        #endregion

        #region Redeem Command
        /// <summary>
        /// Method to check whether the SaveCommand command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnRedeemCommandCanExecute()
        {
            //return false;
            return true;
        }
        /// <summary>
        /// Method to invoke when the SaveCommand command is executed.
        /// </summary>
        private void OnRedeemCommandExecute()
        {
            ViewActionType = ReeedemRewardType.Redeemded;
            SaleOrderModel.GuestModel.GuestRewardCollection.Clear();
            //foreach (base_GuestRewardModel guestRewardUpdated in SaleOrderModel.GuestModel.GuestRewardCollection.Where(x => x.IsChecked))
            //    guestRewardUpdated.IsChecked = false;
            FindOwnerWindow(_ownerViewModel).DialogResult = true;

        }
        #endregion

        #region CancelCommand
        /// <summary>
        /// Method to check whether the DeleteCommand command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnCancelCommandCanExecute()
        {
            return true;
        }

        /// <summary>
        /// Method to invoke when the DeleteCommand command is executed.
        /// </summary>
        private void OnCancelCommandExecute()
        {
            ViewActionType = ReeedemRewardType.Cancel;
            SaleOrderModel.GuestModel.GuestRewardCollection.Clear();
            //if (SaleOrderModel.GuestModel.GuestRewardCollection.Any(x => x.IsChecked))
            //{
            //    foreach (base_GuestRewardModel guestRewardUpdated in SaleOrderModel.GuestModel.GuestRewardCollection.Where(x => x.IsChecked))
            //        guestRewardUpdated.IsChecked = false;
            //}
            FindOwnerWindow(_ownerViewModel).DialogResult = false;
        }
        #endregion

        #region BarcodeInput
        /// <summary>
        /// Gets the BarcodeInput Command.
        /// <para>With Retun(Enter) key press</para> 
        /// <summary>

        public RelayCommand<object> BarcodeInputCommand { get; private set; }



        /// <summary>
        /// Method to check whether the BarcodeInput command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnBarcodeInputCommandCanExecute(object param)
        {
            return !string.IsNullOrWhiteSpace(IDCardNumber);
        }


        /// <summary>
        /// Method to invoke when the BarcodeInput command is executed.
        /// </summary>
        private void OnBarcodeInputCommandExecute(object param)
        {
            if (param != null )
            {
                if (param.ToString().IndexOf("****") >= 0 && ValidMember.Is(ValidateType.Success))
                {
                    if (OnApplyCommandCanExecute())
                    {
                        OnApplyCommandExecute();
                    }
                }
                else
                {
                    VerifyRewardCode(param.ToString());
                }
            }
        }


        #endregion

        #region ViewLoadedCommand
        /// <summary>
        /// Gets the ViewLoaded Command.
        /// <summary>

        public RelayCommand<object> ViewLoadedCommand { get; private set; }



        /// <summary>
        /// Method to check whether the ViewLoaded command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnViewLoadedCommandCanExecute(object param)
        {
            return true;
        }


        /// <summary>
        /// Method to invoke when the ViewLoaded command is executed.
        /// </summary>
        private void OnViewLoadedCommandExecute(object param)
        {
            GetView();
            ConfirmMemberRedeemRewardView.txtMemberBarcode.Focus();
        }
        #endregion

        #region BarcodeChangedCommand
        /// <summary>
        /// Gets the BarcodeChanged Command.
        /// <summary>

        public RelayCommand<object> BarcodeChangedCommand { get; private set; }


        /// <summary>
        /// Method to check whether the BarcodeChanged command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnBarcodeChangedCommandCanExecute(object param)
        {
            return true;
        }


        /// <summary>
        /// Method to invoke when the BarcodeChanged command is executed.
        /// </summary>
        private void OnBarcodeChangedCommandExecute(object param)
        {
            if (IDCardNumber.Length == 1 && ValidMember != (int)ValidateType.None)
            {
                ValidMember = (int)ValidateType.None;
                this.Message = string.Empty;
                ConfirmMemberRedeemRewardView.imgValid.Fill = SetImageSource();
            }
            else if (ValidMember == (int)ValidateType.Success && IDCardNumber.IndexOf("****") < 0)
            {
                ValidMember = (int)ValidateType.None;
                ConfirmMemberRedeemRewardView.imgValid.Fill = SetImageSource();
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(IDCardNumber)
                    && IDCardNumber.IndexOf("****") < 0
                    && IDCardNumber.Length == 13
                    && OnBarcodeInputCommandCanExecute(null))
                {
                    VerifyRewardCode(IDCardNumber);
                }
            }


        }
        #endregion

        #endregion

        #region Private Methods
        private void InitialCommand()
        {
            // Route the commands
            ApplyCommand = new RelayCommand(OnApplyCommandExecute, OnApplyCommandCanExecute);
            RedeemCommand = new RelayCommand(OnRedeemCommandExecute, OnRedeemCommandCanExecute);
            CancelCommand = new RelayCommand(OnCancelCommandExecute, OnCancelCommandCanExecute);
            //With Retun(Enter) key press
            BarcodeInputCommand = new RelayCommand<object>(OnBarcodeInputCommandExecute, OnBarcodeInputCommandCanExecute);
            ViewLoadedCommand = new RelayCommand<object>(OnViewLoadedCommandExecute, OnViewLoadedCommandCanExecute);
            BarcodeChangedCommand = new RelayCommand<object>(OnBarcodeChangedCommandExecute, OnBarcodeChangedCommandCanExecute);
        }

        /// <summary>
        /// Verify MemeberShip
        /// </summary>
        /// <param name="barcode"></param>
        private void VerifyRewardCode(string barcode)
        {
            GetView();

            ConfirmMemberRedeemRewardView.imgValid.Fill = null;
            if (barcode != null && !string.IsNullOrWhiteSpace(barcode) && SaleOrderModel.GuestModel.MembershipValidated != null)
            {
                SaleOrderModel.GuestModel.GuestRewardCollection.Clear();
                if (RewardValidation(barcode))
                {
                    ValidateType validateType = ValidateType.None;
                    if (CheckRewardAvailable(SaleOrderModel.GuestModel.MembershipValidated.BarcodeValidation, out validateType))
                    {
                        Message = string.Empty;
                        ValidMember = (int)ValidateType.Success;
                        ConfirmMemberRedeemRewardView.txtMemberBarcode.Text = "*************";
                        ConfirmMemberRedeemRewardView.txtMemberBarcode.SelectAll();
                        ConfirmMemberRedeemRewardView.btnApply.IsDefault = true;
                        //_resetTimer.Start();
                    }
                    else
                    {
                        if (validateType.Is(ValidateType.Existed))
                        {
                            //Reward Code is Using
                            FailValidation("Reward is using");
                        }
                        else
                        {
                            FailValidation("Not any reward with these code");
                        }
                    }
                }
                else
                {
                    FailValidation("Code not valid");
                }
            }
            else
            {
                ValidMember = (int)ValidateType.Fail;
                FailValidation("Customer is not membership");
                ConfirmMemberRedeemRewardView.txtMemberBarcode.Text = string.Empty;
                ConfirmMemberRedeemRewardView.txtMemberBarcode.Focus();
            }

            ConfirmMemberRedeemRewardView.imgValid.Fill = SetImageSource();
        }

        private bool RewardValidation(string barcode)
        {
            bool result = true;
            if (SaleOrderModel.GuestModel.MembershipValidated.CashRewardCode.Equals(barcode))
            {
                SaleOrderModel.GuestModel.MembershipValidated.BarcodeValidation = (int)base_MemberShipModel.BarcodeValidates.Cash;
            }
            else if (SaleOrderModel.GuestModel.MembershipValidated.PercentRewardCode.Equals(barcode))
            {
                SaleOrderModel.GuestModel.MembershipValidated.BarcodeValidation = (int)base_MemberShipModel.BarcodeValidates.Percent;
            }
            else if (SaleOrderModel.GuestModel.MembershipValidated.PointRewardCode.Equals(barcode))
            {
                SaleOrderModel.GuestModel.MembershipValidated.BarcodeValidation = (int)base_MemberShipModel.BarcodeValidates.Point;
            }
            else
            {
                SaleOrderModel.GuestModel.MembershipValidated.BarcodeValidation = (int)base_MemberShipModel.BarcodeValidates.None;
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Set Form Valid Fail
        /// </summary>
        /// <param name="msg"></param>
        private void FailValidation(string msg)
        {
            ValidMember = (int)ValidateType.Fail;
            Message = msg;
            if (SaleOrderModel.GuestModel.MembershipValidated != null)
                SaleOrderModel.GuestModel.MembershipValidated.BarcodeValidation = (int)base_MemberShipModel.BarcodeValidates.None;
            ConfirmMemberRedeemRewardView.txtMemberBarcode.Text = string.Empty;
            ConfirmMemberRedeemRewardView.txtMemberBarcode.Focus();
        }

        /// <summary>
        /// Check & get Reward From Reward Guest Reward Collection
        /// </summary>
        /// <param name="BarcodeValidation"></param>
        private bool CheckRewardAvailable(int barcodeValidation, out ValidateType infoValidate)
        {
            bool result = false;
            short rewardType = 0;
            switch (barcodeValidation)
            {
                case (int)base_MemberShipModel.BarcodeValidates.None:
                    break;
                case (int)base_MemberShipModel.BarcodeValidates.Cash:
                    rewardType = (short)RewardType.Money;
                    break;
                case (int)base_MemberShipModel.BarcodeValidates.Percent:
                    rewardType = (short)RewardType.Pecent;
                    break;
                case (int)base_MemberShipModel.BarcodeValidates.Point:
                    rewardType = (short)RewardType.Point;
                    break;

            }

            //Not existed guest reward with that type
            if (!SaleOrderModel.GuestModel.base_Guest.base_GuestReward.Any(x => x.Sign.Equals("+") && !x.IsApply && x.RewardSetupUnit.Equals(rewardType)))
            {
                result = false;
                infoValidate = ValidateType.NotAny;
            }
            else if (!SaleOrderModel.GuestModel.GuestRewardCollection.Any(x => x.IsChecked))// not reward apply
            {
                if (rewardType.Equals((short)RewardType.Money))
                {
                    if (Define.CONFIGURATION.IsSumCashReward)
                    {
                        var allCashRewards = SaleOrderModel.GuestModel.base_Guest.base_GuestReward.Where(x => x.Sign.Equals("+") && !x.IsApply && x.ActivedDate.Value <= DateTime.Today && (!x.ExpireDate.HasValue || x.ExpireDate.HasValue && DateTime.Today <= x.ExpireDate.Value) && x.RewardSetupUnit.Equals(rewardType));
                        var cashRedeemRewards = SaleOrderModel.GuestModel.base_Guest.base_GuestReward.Where(x => x.Sign.Equals("-") && x.RewardSetupUnit.Equals(rewardType));
                        if (allCashRewards.Any())
                        {
                            base_GuestReward cashReward = allCashRewards.FirstOrDefault();
                            base_GuestRewardModel cashRewardModel = new base_GuestRewardModel();
                            cashRewardModel.GuestId = cashReward.GuestId;
                            cashRewardModel.RewardId = cashReward.RewardId;
                            cashRewardModel.Amount = SaleOrderModel.Total;
                            cashRewardModel.EarnedDate = cashReward.EarnedDate;
                            cashRewardModel.AppliedDate = cashReward.AppliedDate;
                            cashRewardModel.RewardValue = cashReward.RewardValue;
                            cashRewardModel.ActivedDate = cashReward.ActivedDate;
                            cashRewardModel.ExpireDate = cashReward.ExpireDate;
                            cashRewardModel.Status = cashReward.Status;
                            cashRewardModel.Sign = cashReward.Sign;
                            cashRewardModel.Remark = cashReward.Remark;
                            cashRewardModel.IsNew = true;
                            //this is item sum of cash reward
                            cashRewardModel.IsTemporary = true;
                            cashRewardModel.IsChecked = true;
                            decimal SumCashRewards = allCashRewards.Sum(x => x.RewardSetupAmount);
                            decimal SumRedeem = cashRedeemRewards.Sum(x => x.RewardSetupAmount);
                            cashRewardModel.RewardSetupAmount = SumCashRewards - SumRedeem;
                            cashRewardModel.RewardSetupUnit = rewardType;
                            if (cashRewardModel.RewardSetupAmount > 0)
                            {
                                SaleOrderModel.GuestModel.GuestRewardCollection.Add(cashRewardModel);
                            }
                        }
                    }
                    else
                    {
                        base_GuestReward guestReward = SaleOrderModel.GuestModel.base_Guest.base_GuestReward.FirstOrDefault(x => x.Sign.Equals("+") && !x.IsApply && x.ActivedDate.Value <= DateTime.Today && (!x.ExpireDate.HasValue || x.ExpireDate.HasValue && DateTime.Today <= x.ExpireDate.Value) && x.RewardSetupUnit.Equals(rewardType));
                        if (guestReward != null)
                        {
                            //Item will be updated
                            base_GuestRewardModel guestRewardModel = new base_GuestRewardModel(guestReward);
                            guestRewardModel.Amount = SaleOrderModel.Total;
                            guestRewardModel.IsDirty = true;
                            guestRewardModel.IsChecked = true;
                            SaleOrderModel.GuestModel.GuestRewardCollection.Add(guestRewardModel);
                        }
                    }
                }
                else
                {
                    //For Point & Percent only update
                    base_GuestReward guestReward = SaleOrderModel.GuestModel.base_Guest.base_GuestReward.FirstOrDefault(x => x.Sign.Equals("+") && !x.IsApply && x.ActivedDate.Value <= DateTime.Today && (!x.ExpireDate.HasValue || x.ExpireDate.HasValue && DateTime.Today <= x.ExpireDate.Value) && x.RewardSetupUnit.Equals(rewardType));
                    if (guestReward != null)
                    {
                        //Item will be updated
                        base_GuestRewardModel guestRewardModel = new base_GuestRewardModel(guestReward);
                        guestRewardModel.Amount = SaleOrderModel.Total;
                        guestRewardModel.IsDirty = true;
                        guestRewardModel.IsChecked = true;
                        SaleOrderModel.GuestModel.GuestRewardCollection.Add(guestRewardModel);
                    }
                }
                if (SaleOrderModel.GuestModel.GuestRewardCollection.Any(x => x.IsChecked))
                {
                    result = true;
                    infoValidate = ValidateType.Success;
                }
                else
                {
                    result = false;
                    infoValidate = ValidateType.NotAny;
                }

            }
            else
            {
                infoValidate = ValidateType.Existed;
                ///These reward choice
                result = false;
            }
            return result;
        }

        private void GetView()
        {
            PopupContainer popupContainer = FindOwnerWindow(_ownerViewModel) as PopupContainer;
            if (popupContainer != null && ConfirmMemberRedeemRewardView == null)
                ConfirmMemberRedeemRewardView = popupContainer.grdContent.Children[0] as ConfirmMemberRedeemRewardView;
        }

        private DrawingBrush SetImageSource()
        {
            ValidateType validMember = (ValidateType)Enum.Parse(typeof(ValidateType), ValidMember.ToString());
            FrameworkElement fwElement = new FrameworkElement();
            DrawingBrush img = null;
            switch (validMember)
            {
                case ValidateType.None:
                    img = null;
                    break;
                case ValidateType.Success:
                    img = (fwElement.TryFindResource("OK") as DrawingBrush);
                    break;
                case ValidateType.Fail:
                    img = (fwElement.TryFindResource("Error") as DrawingBrush);
                    break;
            }
            return img;
        }

        private void Reset()
        {
            Message = string.Empty;
            ValidMember = (int)ValidateType.None;
            ConfirmMemberRedeemRewardView.imgValid.Fill = SetImageSource();
            ConfirmMemberRedeemRewardView.txtMemberBarcode.Text = string.Empty;
        }
        #endregion

        #region Events
        private void _resetTimer_Tick(object sender, EventArgs e)
        {
            if (_resetTimer.IsEnabled)
            {
                Reset();
                _resetTimer.Stop();
            }
        }


        #endregion
    }
}
