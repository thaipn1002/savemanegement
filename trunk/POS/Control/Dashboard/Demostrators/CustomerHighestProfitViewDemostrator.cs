﻿using CPC.POS.Interfaces;
using System.Windows.Controls;
using System.Xml.Linq;
using CPC.POS.View;

namespace CPC.Control
{
    class CustomerHighestProfitViewDemostrator : IDemonstrator
    {
        #region IDemonstrator Members

        public string Name
        {
            get
            {
                return "CustomerHighestProfitView";
            }
        }

        public string Title
        {
            get
            {
                return "Customer Graph";
            }
        }

        public string Description
        {
            get
            {
                return "Customer Highest Profit";
            }
        }

        public UserControl Create(XElement configuration = null)
        {
            return new CustomerHighestProfitView(configuration);
        }

        #endregion
    }
}
