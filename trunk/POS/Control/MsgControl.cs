using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MessageBoxControl;
using System.Windows;

namespace CPC.POS
{
    public static class MsgControl
    {
        public static MessageBoxResultCustom ShowWarning(string content, string caption, MessageBoxButtonCustom messageBoxButton)
        {
            return MessageBoxCustom.Show(content, "Thông báo", messageBoxButton, MessageBoxImageCustom.Warning);
        }

        public static MessageBoxResultCustom ShowWarning(string content, string caption, MessageBoxButtonCustom messageBoxButton, MessageBoxImage messageBoxImage)
        {
            return MessageBoxCustom.Show(content, "Thông báo", messageBoxButton, MessageBoxImageCustom.Warning);
        }

        public static MessageBoxResultCustom ShowQuestion(string content, string caption, MessageBoxButtonCustom messageBoxButton)
        {
            return MessageBoxCustom.Show(content, "Thông báo", messageBoxButton, MessageBoxImageCustom.Question);
        }
        public static MessageBoxResultCustom ShowQuestion(string content, string caption, MessageBoxButtonCustom messageBoxButton, MessageBoxImage messageBoxImage)
        {
            return MessageBoxCustom.Show(content, "Thông báo", messageBoxButton, MessageBoxImageCustom.Question);
        }

        public static MessageBoxResultCustom ShowInfomation(string content, string caption, MessageBoxButtonCustom messageBoxButton)
        {
            return MessageBoxCustom.Show(content, "Thông báo", messageBoxButton, MessageBoxImageCustom.Information);
        }
        public static MessageBoxButtonCustom GetMessageBoxButton(MessageBoxButton messageBoxButton)
        {
            if (messageBoxButton == MessageBoxButton.OK)
                return MessageBoxButtonCustom.OK;
            else if (messageBoxButton == MessageBoxButton.OKCancel)
                return MessageBoxButtonCustom.OKCancel;
            else if (messageBoxButton == MessageBoxButton.YesNo)
                return MessageBoxButtonCustom.YesNo;
            else
                return MessageBoxButtonCustom.YesNoCancel;
        }
    }
}
