using System;
using System.Windows.Data;
using System.Windows.Media;
using System.Linq;
using System.Collections.ObjectModel;
using CPC.POS.Database;
namespace CPC.Converter
{
    public class IntToStringConverter : IValueConverter
    {
        #region IMultiValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && value is int)
                return int.Parse(value.ToString()) <= 0 ? 0 : 1;
            return 0;
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }

    public class BoolToToolTipConverter : IValueConverter
    {
        #region IMultiValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && value is int)
            {
                if (int.Parse(value.ToString()) < 0)
                    return string.Format("Trễ {0} ngày.", value.ToString().Replace("-", ""));
                else if (int.Parse(value.ToString()) > 0)
                    return string.Format("Còn {0} ngày.", value);
                else
                    return "Hôm nay hết hạn.";
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
