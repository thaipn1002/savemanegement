﻿using System;
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
using System.Windows.Forms;

namespace CPC.POSReport.View
{
    /// <summary>
    /// Interaction logic for ViewReportWindow.xaml
    /// </summary>
    public partial class ViewReportWindow : Window
    {        
        /// <summary>
        /// Get total page number in report
        /// </summary>
        public int TotalPage { get; set; }
        public ViewReportWindow()
        {
            InitializeComponent();
            crystalReport.ViewerCore.EnableDrillDown = false;            
            this.Height = System.Windows.SystemParameters.PrimaryScreenHeight - 70;
            this.Width = System.Windows.SystemParameters.PrimaryScreenWidth - 100;
            txtGoToPage.TextAlignment = TextAlignment.Center;
        }

        public void ShowReport(byte[] image, string reportName, ViewReportWindow reportView)
        {
            this.Show();
            this.DataContext = new ViewModel.ReportViewViewModel(image, reportName, reportView);
        }

        #region -Report Toolbar-
        public void SetEnalbeButton()
        {            
            if (TotalPage > 1)
            {
                btnNext.IsEnabled = true;
                btnLast.IsEnabled = true;
                txtGoToPage.IsEnabled = true;
            }
            btnExport.IsEnabled = btnPrintDirect.IsEnabled = Function.Common.SHOW_PRINT_BUTTON;
            btnClose.IsEnabled = true;
        }
                
        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            crystalReport.ViewerCore.ExportReport();
        }

        private void btnPrintDirect_Click(object sender, RoutedEventArgs e)
        {
            crystalReport.ViewerCore.PrintReport();
        }

        private void BrdTopBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnLast_Click(object sender, RoutedEventArgs e)
        {
            if (crystalReport.ViewerCore.CurrentPageNumber !=  crystalReport.ViewerCore.TotalPageNumber && crystalReport.ViewerCore.TotalPageNumber > 1)
            {                
                crystalReport.ViewerCore.ShowLastPage();
                btnLast.IsEnabled = false;
                btnNext.IsEnabled = false;
                btnPrev.IsEnabled = true;
                btnFirst.IsEnabled = true;
            }
            txtGoToPage.Text = TotalPage.ToString();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            crystalReport.ViewerCore.ShowNextPage();
            btnPrev.IsEnabled = true;
            btnFirst.IsEnabled = true;
            if (crystalReport.ViewerCore.CurrentPageNumber == TotalPage)
            {
                btnNext.IsEnabled = false;
                btnLast.IsEnabled = false;
            }
            else
            {
                if (btnNext.IsEnabled == false)
                {
                    btnNext.IsEnabled = true;
                }
                if (btnLast.IsEnabled == false)
                {
                    btnLast.IsEnabled = true;
                }
            }
            SetCurrenPageNumber();
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            crystalReport.ViewerCore.ShowPreviousPage();
            btnNext.IsEnabled = true;
            btnLast.IsEnabled = true;
            if (crystalReport.ViewerCore.CurrentPageNumber == 1)
            {
                btnFirst.IsEnabled = false;
                btnPrev.IsEnabled = false;
            }
            else
            {
                if (btnPrev.IsEnabled == false)
                {
                    btnPrev.IsEnabled = true;
                }
                if (btnFirst.IsEnabled == false)
                {
                    btnFirst.IsEnabled = true;
                }
            }
            SetCurrenPageNumber();
        }

        private void btnFirst_Click(object sender, RoutedEventArgs e)
        {
            crystalReport.ViewerCore.ShowFirstPage();
            btnFirst.IsEnabled = false;
            btnPrev.IsEnabled = false;
            btnNext.IsEnabled = true;
            btnLast.IsEnabled = true;           
        }        

        #endregion

        public void GotoPage(int page)
        {
            crystalReport.ViewerCore.ShowNthPage(page);
        }

        private void SetCurrenPageNumber()
        {
            txtGoToPage.Text = crystalReport.ViewerCore.CurrentPageNumber.ToString();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState != System.Windows.WindowState.Minimized)
            {
                this.WindowState = System.Windows.WindowState.Minimized;
            }
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = (this.WindowState != System.Windows.WindowState.Maximized) ?
                    System.Windows.WindowState.Maximized : System.Windows.WindowState.Normal;
            //this.WindowStyle = System.Windows.WindowStyle.None;
        }

        private void txtGoToPage_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                int page = int.Parse(txtGoToPage.Text);
                if (page >= TotalPage)
                {
                    GotoPage(TotalPage);
                    txtGoToPage.Text = TotalPage.ToString();
                    btnFirst.IsEnabled = true;
                    btnPrev.IsEnabled = true;
                    btnLast.IsEnabled = false;
                    btnNext.IsEnabled = false;                    
                }
                else if (page <= 1)
                {
                    GotoPage(1);
                    txtGoToPage.Text = "1";
                    btnFirst.IsEnabled = false;
                    btnPrev.IsEnabled = false;
                    btnLast.IsEnabled = true;
                    btnNext.IsEnabled = true; 
                }
                else
                {
                    GotoPage(page);
                    txtGoToPage.Text = page.ToString();
                    btnFirst.IsEnabled = true;
                    btnPrev.IsEnabled = true;
                    btnLast.IsEnabled = true;
                    btnNext.IsEnabled = true; 
                }
            }
        }

        private void TextBoxNumberic_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            string tem = string.Empty;
            int mySelectionStart = 0;

            if (txtGoToPage.SelectionStart > 0)
                mySelectionStart = txtGoToPage.SelectionStart;

            bool textChanged = false;

            foreach (char item in txtGoToPage.Text)
            {
                //digit
                if (item == '0' || item == '1' || item == '2' || item == '3' || item == '4' || item == '5' || item == '6' || item == '7' || item == '8' || item == '9')
                {
                    tem += item;
                }
                else
                    textChanged = true;
            }

            txtGoToPage.Text = tem;

            if (textChanged.Equals(true) && mySelectionStart > 0)
                txtGoToPage.SelectionStart = mySelectionStart - 1;

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
