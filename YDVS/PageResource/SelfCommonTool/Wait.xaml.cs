using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SelfCommonTool
{
    /// <summary>
    /// Wait.xaml 的交互逻辑
    /// </summary>
    public partial class Wait : UserControl
    {
        public static DependencyProperty IsTextInfoStr;
        public string TextInfoStr
        {
            get { return (string)GetValue(IsTextInfoStr); }
            set { SetValue(IsTextInfoStr, value); }
        }

        static Wait()
        {
            PropertyMetadata pm = new PropertyMetadata("请稍等，正在取得数据......", new PropertyChangedCallback(SetTextInfo));
            IsTextInfoStr = DependencyProperty.Register("IsTextInfoStr", typeof(string), typeof(Wait), pm);
        }

        private static void SetTextInfo(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Wait w = d as Wait;
            if (w == null) return;
            w.showText.Text = e.NewValue.ToString();
        }
        public Wait()
        {
            this.InitializeComponent();
        }
    }
}