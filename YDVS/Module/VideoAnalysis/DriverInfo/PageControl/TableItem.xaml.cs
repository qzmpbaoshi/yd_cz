using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VideoAnalysis.DriverInfo.ViewModel;

namespace VideoAnalysis.DriverInfo.PageControl
{
    /// <summary>
    /// TableItem.xaml 的交互逻辑
    /// </summary>
    public partial class TableItem : UserControl
    {
        private static DependencyProperty IsSelected;
        public bool Selected
        {
            get
            {
                return (bool)GetValue(IsSelected);
            }
            set
            {
                SetValue(IsSelected, value);
            }
        }

        public DriverInfoViewModel ViewModel { get; set; }
        static TableItem()
        {
            PropertyMetadata pm = new PropertyMetadata(false, IsSelectCallBack);
            IsSelected = DependencyProperty.Register("IsSelected", typeof(bool), typeof(TableItem), pm);
        }
        public TableItem()
        {
            InitializeComponent();
            this.ViewModel = new DriverInfoViewModel();
            this.DataContext = this.ViewModel;
        }
        private static void IsSelectCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TableItem item = d as TableItem;
            if (item == null) return;
            if (e.NewValue is bool)
            {
                bool isSelected = false;
                bool.TryParse(e.NewValue.ToString(), out isSelected);
                item.SetResourceReference(BackgroundProperty, isSelected ? "CheckedColor" : "TableBodyColor");
            }
        }
        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            try
            {
                TableItem item = sender as TableItem;
                if (item == null) return;
                if (this.Selected) return;
                item.SetResourceReference(BackgroundProperty, "TableBodyColor");
            }
            catch { }
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                TableItem item = sender as TableItem;
                if (item == null) return;
                if (this.Selected) return;
                item.SetResourceReference(BackgroundProperty, "MouseOverColor");
            }
            catch { }
        }
    }
}
