using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Windows.Controls.Primitives;
using System.Threading;
using System.Windows.Threading;

namespace CPC.POS.View
{
    /// <summary>
    /// Interaction logic for LogOnScreenView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
            this.txtblUsername.Focus();
            this.Loaded += new RoutedEventHandler(LoginView_Loaded);
        }

        void LoginView_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.IsVisible)
                this.Dispatcher.BeginInvoke(
                                    DispatcherPriority.Background,
                                    (ThreadStart)delegate
                                       {
                                           Keyboard.Focus(this.txtUsername);
                                       });
        }
    }
}
