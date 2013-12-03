using Microsoft.Windows.Controls.Ribbon;
using System.Windows;
using CPC.POS.ViewModel;
using CPC.POS.View;
using CPC.Control;
using System.Windows.Threading;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
namespace CPC.POS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {

        //Visibility="{Binding DashboardVisibility, Mode=OneWay}" 
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        { // Get all user rights
            IEnumerable<string> userRightCodes = Define.USER_AUTHORIZATION.Select(x => x.Code);
            if (userRightCodes.Contains("SO100-04-15"))
                this.Dispatcher.BeginInvoke(
                                   DispatcherPriority.Loaded,
                                   (ThreadStart)delegate
                                   {
                                       Define.IsOpenLiability = true;
                                       this.LoadLiabilities();
                                   });
        }
        private void LoadLiabilities()
        {
            LiabilityNotificationViewModel viewModel = new LiabilityNotificationViewModel();
            LiabilityView view = new LiabilityView();
            view.DataContext = viewModel;
            PopupContainer popup = new PopupContainer(view, true);
            popup.ShowClose = true;
            popup.Title = "Thông báo công nợ";
            viewModel.LoadData();
            popup.ShowDialog();
            Define.IsOpenLiability = false;
        }
    }
}
