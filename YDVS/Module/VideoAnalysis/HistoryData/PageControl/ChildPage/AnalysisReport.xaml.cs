using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;
using VideoAnalysis.HistoryData.ViewModel;

namespace VideoAnalysis.HistoryData.PageControl.ChildPage
{
    /// <summary>
    /// AnalysisReport.xaml 的交互逻辑
    /// </summary>
    public partial class AnalysisReport : Window
    {
        private AnalysisReportViewModel ViewModel { get; set; }
        public AnalysisReport()
        {
            InitializeComponent();
            this.aReport_wait.Visibility = Visibility.Visible;
        }

        private void WinClose_Btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch { }
        }
        public async void InitPageAsync(AnalysisReportViewModel _viewModel)
        {
            try
            {
                await this.Dispatcher.InvokeAsync(() =>
                {
                    this.image_wp.Children.Clear();
                    if (_viewModel == null) return;
                    this.ViewModel = _viewModel;
                    //设置标题
                    StringBuilder sbTitle = new StringBuilder();
                    sbTitle.Append(this.ViewModel.TrainShortName);
                    sbTitle.Append(string.IsNullOrWhiteSpace(this.ViewModel.MDataViewModel.TrainNum) ? "" : ("-" + this.ViewModel.MDataViewModel.TrainNum));
                    sbTitle.Append("（" + this.ViewModel.EventTimeStr + "）");
                    sbTitle.Append("视频分析报告");
                    this.aReport_title.Text = sbTitle.ToString();
                    //设置图片显示
                    foreach (CapVideoImage CapImage in this.ViewModel.CapImages)
                    {
                        byte[] buf = CapImage.CapImageBuf;
                        this.image_wp.Children.Add(this.GetImageRadioButtonByBytes(buf));
                    }
                    //设置LKJ监控数据
                    this.aReport_MonitorData.RefreshDataByViewModel(this.ViewModel.MDataViewModel);
                    //初始化分析数据
                    this.ViewModel.AnalysisTimeStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    this.aReport_time.Text = this.ViewModel.AnalysisTimeStr;
                    this.aReport_person.Text = "";
                    this.aReport_content.Text = "";
                    this.aReport_wait.Visibility = Visibility.Collapsed;
                });
            }
            catch (Exception ex)
            {
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "加载视频分析报告分析页面", ex);
            }
        }
        private RadioButton GetImageRadioButtonByBytes(byte[] iBytes)
        {
            try
            {
                BitmapImage bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.StreamSource = new MemoryStream(iBytes);
                bmp.EndInit();
                Image image = new Image();
                image.Width = 290;
                //image.Height = 290;
                image.Source = bmp;
                RadioButton rb = new RadioButton();
                rb.Margin = new Thickness(3);
                rb.BorderThickness = new Thickness(1);
                rb.SetResourceReference(RadioButton.BorderBrushProperty, "BorderColor");
                rb.SetResourceReference(RadioButton.StyleProperty, "NavRadioButton");
                rb.Content = image;
                return rb;
            }
            catch (Exception ex)
            {
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "二进制转换为图片控件赋值错误", ex);
                return null;
            }
        }

        private void SaveReport_Btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                

            }
            catch (Exception ex)
            {
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "保存报告错误", ex);
            }
        }
    }
}
