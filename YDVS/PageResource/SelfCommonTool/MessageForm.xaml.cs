using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SelfCommonTool
{
    /// <summary>
    /// MessageForm.xaml 的交互逻辑
    /// </summary>
    public partial class MessageForm : Window
    {
        public Timer Time { get; set; }

        public MessageBoxResult Mbr { get; set; }

        public MessageForm(int Button)
        {

            InitializeComponent();
            if (Button == 0)
            {
                Time = new Timer(1500);
                Time.Elapsed += Time_Elapsed;
                this.EnButton.Visibility = Visibility.Collapsed;
                this.EsButton.Visibility = Visibility.Collapsed;
                this.CloseBtn.Visibility = Visibility.Collapsed;
                Time.Start();
            }
            else if (Button == 1)
            {
                this.EsButton.Click += EsButton_Click;
                this.EsButton.Visibility = Visibility.Visible;
                this.EnButton.Visibility = Visibility.Collapsed;
                this.CloseBtn.Visibility = Visibility.Collapsed;
            }
            else
            {
                this.EnButton.Visibility = Visibility.Visible;
                this.EsButton.Visibility = Visibility.Visible;
                this.CloseBtn.Visibility = Visibility.Visible;
                this.EnButton.Click += EnButton_Click;
                this.EsButton.Click += EsButton_Click;
                this.CloseBtn.Click += CloseBtn_Click;
            }



        }

        void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Mbr = MessageBoxResult.Cancel;
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                Close();
            }));
        }

        void EsButton_Click(object sender, RoutedEventArgs e)
        {
            Mbr = MessageBoxResult.No;
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                Close();
            }));
        }

        void EnButton_Click(object sender, RoutedEventArgs e)
        {

            Mbr = MessageBoxResult.Yes;
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                Close();
            }));
        }

        void Time_Elapsed(object sender, ElapsedEventArgs e)
        {
            Mbr = MessageBoxResult.None;
            this.Time.Dispose();
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                this.Close();
            }));
        }
        public static MessageBoxResult Show(string MessageHead, string Message, int Button)
        {
            var MessageBox = new MessageForm(Button);
            MessageBox.MessageHead.Text = MessageHead;
            MessageBox.Message.Text = Message;
            var s = MessageBox.ShowDialog();
            return MessageBox.Mbr;

        }
        public static MessageBoxResult Show(string MessageHead, string Message, int Button, string strEnBtnName, string strEsBtnName)
        {
            var MessageBox = new MessageForm(Button);
            MessageBox.MessageHead.Text = MessageHead;
            MessageBox.Message.Text = Message;
            MessageBox.EnButton.Content = strEnBtnName;
            MessageBox.EsButton.Content = strEsBtnName;
            var s = MessageBox.ShowDialog();
            return MessageBox.Mbr;

        }
    }
}
