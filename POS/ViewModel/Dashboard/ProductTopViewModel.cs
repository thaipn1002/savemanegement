﻿using System;
using System.Collections.Generic;
using System.Linq;
using CPC.Toolkit.Base;
using CPC.Helper;
using System.Windows.Input;
using CPC.Toolkit.Command;
using System.Windows;
using CPC.POS.Repository;
using CPC.POS.Model;
using CPC.POS.Database;
using De.TorstenMandelkow.MetroChart;
using System.Xml.Linq;
using CPC.POS.Interfaces;
using System.Windows.Controls;
using System.Windows.Data;
using MessageBoxControl;

namespace CPC.POS.ViewModel
{
    public class ProductTopViewModel : ViewModelBase, IDashboardItemFunction
    {
        #region Fields

        private Grid _gridView;

        private ChartBase _chartBase;

        private ChartSeries _chartSeries;

        private XElement _configuration;

        #endregion

        #region Constructors

        public ProductTopViewModel(Grid grid, XElement configuration)
        {
            _gridView = grid;
            _configuration = configuration;
            _topList = new short[] { 5, 10, 15, 20, 25, 30 };

            // Create ChartSeries Default used for all charts.
            _chartSeries = new ChartSeries();
            _chartSeries.DisplayMember = "ProductName";
            BindingOperations.SetBinding(_chartSeries, ChartSeries.ItemsSourceProperty, new Binding
            {
                Path = new PropertyPath("ProductCollection"),
                Mode = BindingMode.OneWay
            });

            if (_configuration != null)
            {
                if (configuration.Attribute("Total") != null)
                {
                    short.TryParse(configuration.Attribute("Total").Value, out _total);
                }
                if (configuration.Attribute("ProductOrderByID") != null)
                {
                    short.TryParse(configuration.Attribute("ProductOrderByID").Value, out _productOrderByID);
                }
                if (configuration.Attribute("OrderDirectionID") != null)
                {
                    short.TryParse(configuration.Attribute("OrderDirectionID").Value, out _orderDirectionID);
                }
                if (configuration.Attribute("ChartTypeID") != null)
                {
                    short.TryParse(configuration.Attribute("ChartTypeID").Value, out _chartTypeID);
                }
            }

            if (_total == 0)
            {
                _total = _topList.Min();
            }
            if (_productOrderByID == 0)
            {
                _productOrderByID = Common.ProductOrderBy.First().Value;
            }
            if (_orderDirectionID == 0)
            {
                _orderDirectionID = Common.OrderDirection.First().Value;
            }
            if (_chartTypeID == 0)
            {
                _chartTypeID = Common.ChartType.First().Value;
            }

            SelectChart();

            GetProducts();
        }

        #endregion

        #region Properties

        #region GridViewVisibility

        private Visibility _gridViewVisibility = Visibility.Visible;
        public Visibility GridViewVisibility
        {
            get
            {
                return _gridViewVisibility;
            }
            set
            {
                if (_gridViewVisibility != value)
                {
                    _gridViewVisibility = value;
                    OnPropertyChanged(() => GridViewVisibility);
                }
            }
        }

        #endregion

        #region GridEditVisibility

        private Visibility _gridEditVisibility = Visibility.Collapsed;
        public Visibility GridEditVisibility
        {
            get
            {
                return _gridEditVisibility;
            }
            set
            {
                if (_gridEditVisibility != value)
                {
                    _gridEditVisibility = value;
                    OnPropertyChanged(() => GridEditVisibility);
                }
            }
        }

        #endregion

        #region ToolTipFormat

        public string ToolTipFormat
        {
            get
            {
                return "{0} has value '{1}'";
            }
        }

        public string ToolTipFormatWithPercent
        {
            get
            {
                return "{0} has value '{1}' ({3:P2})";
            }
        }

        #endregion

        #region TopList

        private short[] _topList;
        public short[] TopList
        {
            get
            {
                return _topList;
            }
            set
            {
                if (_topList != value)
                {
                    _topList = value;
                    OnPropertyChanged(() => TopList);
                }
            }
        }

        #endregion

        #region Total

        private short _total;
        public short Total
        {
            get
            {
                return _total;
            }
            set
            {
                if (_total != value)
                {
                    _total = value;
                    OnPropertyChanged(() => Total);
                }
            }
        }

        #endregion

        #region ProductOrderByID

        private short _productOrderByID;
        public short ProductOrderByID
        {
            get
            {
                return _productOrderByID;
            }
            set
            {
                if (_productOrderByID != value)
                {
                    _productOrderByID = value;
                    OnPropertyChanged(() => ProductOrderByID);
                }
            }
        }

        #endregion

        #region OrderDirectionID

        private short _orderDirectionID;
        public short OrderDirectionID
        {
            get
            {
                return _orderDirectionID;
            }
            set
            {
                if (_orderDirectionID != value)
                {
                    _orderDirectionID = value;
                    OnPropertyChanged(() => OrderDirectionID);
                }
            }
        }

        #endregion

        #region ChartTypeID

        private short _chartTypeID;
        public short ChartTypeID
        {
            get
            {
                return _chartTypeID;
            }
            set
            {
                if (_chartTypeID != value)
                {
                    _chartTypeID = value;
                    OnPropertyChanged(() => ChartTypeID);
                }
            }
        }

        #endregion

        #region ProductCollection

        private CollectionBase<base_ProductStoreModel> _productCollection;
        public CollectionBase<base_ProductStoreModel> ProductCollection
        {
            get
            {
                return _productCollection;
            }
            set
            {
                if (_productCollection != value)
                {
                    _productCollection = value;
                    OnPropertyChanged(() => ProductCollection);
                }
            }
        }

        #endregion

        #region SelectedItem

        private base_ProductStoreModel _selectedItem;
        public base_ProductStoreModel SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    OnPropertyChanged(() => SelectedItem);
                    OnSelectedItemChanged();
                }
            }
        }

        #endregion

        #endregion

        #region Command Properties

        #region OKCommand

        private ICommand _OKCommand;
        /// <summary>
        /// When 'OK' button clicked, command will execute.
        /// </summary>
        public ICommand OKCommand
        {
            get
            {
                if (_OKCommand == null)
                {
                    _OKCommand = new RelayCommand(OKExecute);
                }
                return _OKCommand;
            }
        }

        #endregion

        #endregion

        #region Command Methods

        #region OKExecute

        private void OKExecute()
        {
            SelectChart();
            GetProducts();
            SaveConfiguration();
            Lock();
        }

        #endregion

        #endregion

        #region Property Changed Methods

        #region OnSelectedItemChanged

        private void OnSelectedItemChanged()
        {
            if (_selectedItem != null)
            {
                (App.Current.MainWindow.DataContext as MainViewModel).OpenViewExecute("Product", _selectedItem.ProductModel.Resource);
            }
        }

        #endregion

        #endregion

        #region Private Methods

        #region SelectChart

        /// <summary>
        /// Select Chart.
        /// </summary>
        private void SelectChart()
        {
            // Not change chart.
            if (_chartBase != null && ((short)_chartBase.Tag == _chartTypeID))
            {
                return;
            }

            double width = 0;
            double height = 0;
            if (_chartBase != null)
            {
                width = _chartBase.Width;
                height = _chartBase.Height;
            }

            // Clear old chart.
            if (_gridView.Children != null)
            {
                _gridView.Children.Clear();
            }

            // Create new chart.
            switch ((ChartType)_chartTypeID)
            {
                case ChartType.ColumnChart:

                    _chartBase = new ClusteredColumnChart();
                    _chartBase.Style = App.Current.FindResource("ColumnChartStyleVariableSize") as Style;

                    break;

                case ChartType.BarChart:

                    _chartBase = new ClusteredBarChart();
                    _chartBase.Style = App.Current.FindResource("BarChartStyleVariableSize") as Style;
                    break;

                case ChartType.StackedColumnChart:

                    _chartBase = new StackedColumnChart();
                    _chartBase.Style = App.Current.FindResource("StackedColumnChartStyleVariableSize") as Style;

                    break;

                case ChartType.StackedBarChart:

                    _chartBase = new StackedBarChart();
                    _chartBase.Style = App.Current.FindResource("StackedBarChartStyleVariableSize") as Style;

                    break;

                case ChartType.PieChart:

                    _chartBase = new PieChart();
                    _chartBase.Style = App.Current.FindResource("PieChartStyleVariableSize") as Style;

                    break;

                case ChartType.DoughnutChart:

                    _chartBase = new DoughnutChart();
                    _chartBase.Style = App.Current.FindResource("DoughnutChartStyleVariableSize") as Style;

                    break;

                default:

                    _chartBase = new ClusteredColumnChart();
                    _chartBase.Style = App.Current.FindResource("ColumnChartStyleVariableSize") as Style;

                    break;
            }

            _chartBase.ChartSubTitle = null;
            _chartBase.ToolTipFormat = ToolTipFormat;
            _chartBase.Tag = _chartTypeID;
            if (width > 0 && height > 0)
            {
                _chartBase.Width = width;
                _chartBase.Height = height;
            }
            BindingOperations.SetBinding(_chartBase, ChartBase.SelectedItemProperty, new Binding
            {
                Path = new PropertyPath("SelectedItem"),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });
            _chartBase.Series.Add(_chartSeries);
            _gridView.Children.Add(_chartBase);
        }

        #endregion

        #region GetProducts

        /// <summary>
        /// Gets products.
        /// </summary>
        private void GetProducts()
        {
            try
            {
                base_ProductStoreRepository productStoreRepository = new base_ProductStoreRepository();
                List<base_ProductStore> productStoreList = null;

                switch ((ProductOrderBy)_productOrderByID)
                {
                    case ProductOrderBy.TotalProfit:

                        if ((OrderDirection)_orderDirectionID == OrderDirection.Highest)
                        {
                            productStoreList = productStoreRepository.GetRangeDescending(0, _total, x => x.TotalProfit, x => x.StoreCode == Define.StoreCode).Where(x => x.TotalProfit > 0).ToList();
                        }
                        else
                        {
                            productStoreList = productStoreRepository.GetRange(0, _total, x => x.TotalProfit, x => x.StoreCode == Define.StoreCode).Where(x => x.TotalProfit > 0).ToList();
                        }

                        if ((ChartType)_chartTypeID == ChartType.PieChart || (ChartType)_chartTypeID == ChartType.DoughnutChart)
                        {
                            _chartBase.ToolTipFormat = ToolTipFormatWithPercent;
                            _chartBase.ChartSubTitle = "Total Profit";
                        }
                        _chartBase.ChartTitle = string.Format("Top {0} Products", _total);
                        _chartSeries.SeriesTitle = "Total Profit";
                        _chartSeries.ValueMember = "TotalProfit";

                        break;

                    case ProductOrderBy.OnHandQuantity:

                        if ((OrderDirection)_orderDirectionID == OrderDirection.Highest)
                        {
                            productStoreList = productStoreRepository.GetRangeDescending(0, _total, x => x.QuantityOnHand, x => x.StoreCode == Define.StoreCode).Where(x => x.QuantityOnHand > 0).ToList();
                        }
                        else
                        {
                            productStoreList = productStoreRepository.GetRange(0, _total, x => x.QuantityOnHand, x => x.StoreCode == Define.StoreCode).Where(x => x.QuantityOnHand > 0).ToList();
                        }

                        if ((ChartType)_chartTypeID == ChartType.PieChart || (ChartType)_chartTypeID == ChartType.DoughnutChart)
                        {
                            _chartBase.ToolTipFormat = ToolTipFormatWithPercent;
                            _chartBase.ChartSubTitle = "On-Hand Quantity";
                        }
                        _chartBase.ChartTitle = string.Format("Top {0} Products", _total);
                        _chartSeries.SeriesTitle = "On-Hand Quantity";
                        _chartSeries.ValueMember = "QuantityOnHand";

                        break;

                    case ProductOrderBy.SaleProfit:

                        if ((OrderDirection)_orderDirectionID == OrderDirection.Highest)
                        {
                            productStoreList = productStoreRepository.GetRangeDescending(0, _total, x => x.SaleProfit, x => x.StoreCode == Define.StoreCode).Where(x => x.SaleProfit > 0).ToList();
                        }
                        else
                        {
                            productStoreList = productStoreRepository.GetRange(0, _total, x => x.SaleProfit, x => x.StoreCode == Define.StoreCode).Where(x => x.SaleProfit > 0).ToList();
                        }

                        if ((ChartType)_chartTypeID == ChartType.PieChart || (ChartType)_chartTypeID == ChartType.DoughnutChart)
                        {
                            _chartBase.ToolTipFormat = ToolTipFormatWithPercent;
                            _chartBase.ChartSubTitle = "Sale Profit";
                        }
                        _chartBase.ChartTitle = string.Format("Top {0} Products", _total);
                        _chartSeries.SeriesTitle = "Sale Profit";
                        _chartSeries.ValueMember = "SaleProfit";

                        break;

                    case ProductOrderBy.SoldQuantity:

                        if ((OrderDirection)_orderDirectionID == OrderDirection.Highest)
                        {
                            productStoreList = productStoreRepository.GetRangeDescending(0, _total, x => x.SoldQuantity, x => x.StoreCode == Define.StoreCode).Where(x => x.SoldQuantity > 0).ToList();
                        }
                        else
                        {
                            productStoreList = productStoreRepository.GetRange(0, _total, x => x.SoldQuantity, x => x.StoreCode == Define.StoreCode).Where(x => x.SoldQuantity > 0).ToList();
                        }

                        if ((ChartType)_chartTypeID == ChartType.PieChart || (ChartType)_chartTypeID == ChartType.DoughnutChart)
                        {
                            _chartBase.ToolTipFormat = ToolTipFormatWithPercent;
                            _chartBase.ChartSubTitle = "Sold Quantity";
                        }
                        _chartBase.ChartTitle = string.Format("Top {0} Products", _total);
                        _chartSeries.SeriesTitle = "Sold Quantity";
                        _chartSeries.ValueMember = "SoldQuantity";

                        break;

                    case ProductOrderBy.QuantityAvailable:

                        if ((OrderDirection)_orderDirectionID == OrderDirection.Highest)
                        {
                            productStoreList = productStoreRepository.GetRangeDescending(0, _total, x => x.QuantityAvailable, x => x.StoreCode == Define.StoreCode).Where(x => x.QuantityAvailable > 0).ToList();
                        }
                        else
                        {
                            productStoreList = productStoreRepository.GetRange(0, _total, x => x.QuantityAvailable, x => x.StoreCode == Define.StoreCode).Where(x => x.QuantityAvailable > 0).ToList();
                        }

                        if ((ChartType)_chartTypeID == ChartType.PieChart || (ChartType)_chartTypeID == ChartType.DoughnutChart)
                        {
                            _chartBase.ToolTipFormat = ToolTipFormatWithPercent;
                            _chartBase.ChartSubTitle = "Quantity Available";
                        }
                        _chartBase.ChartTitle = string.Format("Top {0} Products", _total);
                        _chartSeries.SeriesTitle = "Quantity Available";
                        _chartSeries.ValueMember = "QuantityAvailable";

                        break;

                    case ProductOrderBy.PurchasedSubTotal:

                        if ((OrderDirection)_orderDirectionID == OrderDirection.Highest)
                        {
                            productStoreList = productStoreRepository.GetRangeDescending(0, _total, x => x.PurchasedSubTotal, x => x.StoreCode == Define.StoreCode).Where(x => x.PurchasedSubTotal > 0).ToList();
                        }
                        else
                        {
                            productStoreList = productStoreRepository.GetRange(0, _total, x => x.PurchasedSubTotal, x => x.StoreCode == Define.StoreCode).Where(x => x.PurchasedSubTotal > 0).ToList();
                        }

                        if ((ChartType)_chartTypeID == ChartType.PieChart || (ChartType)_chartTypeID == ChartType.DoughnutChart)
                        {
                            _chartBase.ToolTipFormat = ToolTipFormatWithPercent;
                            _chartBase.ChartSubTitle = "Purchased SubTotal";
                        }
                        _chartBase.ChartTitle = string.Format("Top {0} Products", _total);
                        _chartSeries.SeriesTitle = "Purchased SubTotal";
                        _chartSeries.ValueMember = "PurchasedSubTotal";

                        break;

                    case ProductOrderBy.PurchasedQuantity:

                        if ((OrderDirection)_orderDirectionID == OrderDirection.Highest)
                        {
                            productStoreList = productStoreRepository.GetRangeDescending(0, _total, x => x.PurchasedQuantity, x => x.StoreCode == Define.StoreCode).Where(x => x.PurchasedQuantity > 0).ToList();
                        }
                        else
                        {
                            productStoreList = productStoreRepository.GetRange(0, _total, x => x.PurchasedQuantity, x => x.StoreCode == Define.StoreCode).Where(x => x.PurchasedQuantity > 0).ToList();
                        }

                        if ((ChartType)_chartTypeID == ChartType.PieChart || (ChartType)_chartTypeID == ChartType.DoughnutChart)
                        {
                            _chartBase.ToolTipFormat = ToolTipFormatWithPercent;
                            _chartBase.ChartSubTitle = "Purchased Quantity";
                        }
                        _chartBase.ChartTitle = string.Format("Top {0} Products", _total);
                        _chartSeries.SeriesTitle = "Purchased Quantity";
                        _chartSeries.ValueMember = "PurchasedQuantity";

                        break;

                    //case ProductOrderBy.TotalSale:

                    //    if ((OrderDirection)_orderDirectionID == OrderDirection.Highest)
                    //    {
                    //        productStoreList = productStoreRepository.GetRangeDescending(0, _total, x => x.TotalSale, x => x.StoreCode == Define.StoreCode).Where(x => x.TotalSale > 0).ToList();
                    //    }
                    //    else
                    //    {
                    //        productStoreList = productStoreRepository.GetRange(0, _total, x => x.TotalSale, x => x.StoreCode == Define.StoreCode).Where(x => x.TotalSale > 0).ToList();
                    //    }

                    //    if ((ChartType)_chartTypeID == ChartType.PieChart || (ChartType)_chartTypeID == ChartType.DoughnutChart)
                    //    {
                    //        _chartBase.ToolTipFormat = ToolTipFormatWithPercent;
                    //        _chartBase.ChartSubTitle = "Total Sale";
                    //    }
                    //    _chartBase.ChartTitle = "Best Sale";
                    //    _chartSeries.SeriesTitle = "Total Sale";
                    //    _chartSeries.ValueMember = "TotalSale";

                    //    break;
                }

                if (productStoreList != null && productStoreList.Any())
                {
                    ProductCollection = new CollectionBase<base_ProductStoreModel>(productStoreList.Select(x => new base_ProductStoreModel(productStoreRepository.Refresh(x))
                    {
                        ProductModel = new base_ProductModel(x.base_Product)
                    }));
                }
            }
            catch (Exception exception)
            {
                WriteLog(exception);
                MsgControl.ShowWarning(exception.Message, Language.Warning, MessageBoxButtonCustom.OK);
            }
        }

        #endregion

        #region SaveConfiguration

        /// <summary>
        /// Save configuration.
        /// </summary>
        private void SaveConfiguration()
        {
            _configuration = new XElement("Configuration",
                new XAttribute("Total", _total),
                new XAttribute("ProductOrderByID", _productOrderByID),
                new XAttribute("OrderDirectionID", _orderDirectionID),
                new XAttribute("ChartTypeID", _chartTypeID));
        }

        #endregion

        #endregion

        #region Public Methods

        #endregion

        #region Events

        #endregion

        #region WriteLog

        private void WriteLog(Exception exception)
        {
            _log4net.Error(string.Format("Message: {0}. Source: {1}.", exception.Message, exception.Source));
            if (exception.InnerException != null)
            {
                _log4net.Error(exception.InnerException.ToString());
            }
        }

        #endregion

        #region IDashboardItemFunction Members

        public bool CanEdit
        {
            get
            {
                return true;
            }
        }

        public void Lock()
        {
            GridViewVisibility = Visibility.Visible;
            GridEditVisibility = Visibility.Collapsed;
        }

        public void Unlock()
        {
            GridViewVisibility = Visibility.Collapsed;
            GridEditVisibility = Visibility.Visible;
        }

        public XElement GetConfiguration()
        {
            if (_configuration == null)
            {
                SaveConfiguration();
            }

            return _configuration;
        }

        public void UpdateSize(Size newSize)
        {
            double width = (newSize.Width / 3);
            double height = (newSize.Height / 2);

            if (_chartBase != null)
            {
                if (double.IsNaN(_chartBase.Width) || _chartBase.Width < width)
                {
                    _chartBase.Width = width;
                }
                if (double.IsNaN(_chartBase.Height) || _chartBase.Height < height)
                {
                    _chartBase.Height = height;
                }
            }
        }

        #endregion
    }
}
