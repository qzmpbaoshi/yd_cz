using System;
using System.Windows;
using System.Windows.Controls;
using VideoAnalysis.TrainProprietorship.ViewModel;

namespace VideoAnalysis.TrainProprietorship.PageControl
{
    /// <summary>
    /// TableHead.xaml 的交互逻辑
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
        public TrainProprietorshipViewModel ViewModel { get; set; }
        static TableItem()
        {
            PropertyMetadata pm = new PropertyMetadata(false, IsSelectCallBack);
            IsSelected = DependencyProperty.Register("IsSelected", typeof(bool), typeof(TableItem), pm);
        }
        public TableItem()
        {
            InitializeComponent();
            this.ViewModel = new TrainProprietorshipViewModel();
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

        private void UserControl_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
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

        private void UserControl_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
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
    }
}
