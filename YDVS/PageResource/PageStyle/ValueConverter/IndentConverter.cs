using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace PageStyle
{
    public class IndentConverter : IValueConverter
    {
        public double Indent { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var item = value as TreeViewItem;
            if (null == item)
                return new Thickness(0);
            return new Thickness(Indent * item.GetDepth(), 0, 0, 0);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public static class TreeViewItemExtensions
    {
        public static int GetDepth(this TreeViewItem item)
        {
            FrameworkElement elem = item;
            while (elem.Parent != null)
            {
                var tvi = elem.Parent as TreeViewItem;
                if (null != tvi)
                    return tvi.GetDepth() + 1;
                elem = elem.Parent as FrameworkElement;
            }
            return 0;
        }
    } 
}
