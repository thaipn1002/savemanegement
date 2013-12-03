using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using CPC.POS;

namespace CPC.Converter
{
    class DiscountRateToIntegerConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            if (value == null)
                return Define.CONFIGURATION.CurrencySymbol;
            
            if (value.ToString().Equals("0"))
                return Define.CONFIGURATION.CurrencySymbol;
            else
                return "%";
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return 0;
            if (Define.CONFIGURATION.CurrencySymbol.Equals(value.ToString()))
                return 0;
            else
                return 1;
        }
    }
}
