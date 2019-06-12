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
using VideoAnalysis.HistoryData.EventHandler;
using VideoAnalysis.HistoryData.ViewModel;
using Visifire.Charts;

namespace VideoAnalysis.HistoryData.PageControl
{
    /// <summary>
    /// LKJChart.xaml 的交互逻辑
    /// </summary>
    public partial class LKJChart : UserControl
    {
        public event EventHandler<ChangeVideoEventArgs> ChangeVideoEvent;
        public LKJChart()
        {
            InitializeComponent();
        }
        public async void SetLKJChartTitleAsync(string titleStr)
        {
            try
            {
                await this.Dispatcher.InvokeAsync(() =>
                 {
                     this.lkj_chart_title.Text = "";
                     if (string.IsNullOrWhiteSpace(titleStr)) return;
                     this.lkj_chart_title.Text = titleStr;
                 });
            }
            catch { }
        }
        public async void MoveTrendLineAsync(object _xValue)
        {
            try
            {
                if (_xValue == null) return;
                await this.Dispatcher.InvokeAsync(() =>
                 {
                     this.lkj_chart_trendLine.Value = _xValue;
                 });
            }
            catch { }
        }
        public void ClearVideoSeries(object sender, EventArgs eventArgs)
        {
            try
            {
                this.lkj_chart_video_series.DataPoints.Clear();
            }
            catch { }
        }
        public void SetVideoSeries(object sender, DrawVideoLineEventArgs eventArgs)
        {
            try
            {
                if (eventArgs == null || eventArgs.EndPoint.XValue == null) return;
                VideoSource vs = sender as VideoSource;
                this.Dispatcher.Invoke(() =>
                 {
                     this.lkj_chart_video_series.DataPoints.Add(eventArgs.StartPoint);
                     this.lkj_chart_video_series.DataPoints.Add(eventArgs.EndPoint);
                     this.lkj_chart_video_series.DataPoints.Add(new DataPoint
                     {
                         XValue = eventArgs.EndPoint.XValue,
                         //设置Y轴点                   
                         YValue = double.NaN,
                         MarkerSize = 0,
                     });
                     if (eventArgs.IsDrawFinish)
                         this.chart_wait.Visibility = Visibility.Collapsed;
                     //Console.WriteLine("通道" + vs.VideoChannel + ":YValue=" + eventArgs.EndPoint.YValue + "|总时长-" + vs.DurationSecond + "秒(开始时间-" + vs.StartTime.ToString("yyyy-MM-dd HH:mm:ss") + "结束时间-" + (vs.EndTime == null ? "" : ((DateTime)vs.EndTime).ToString("yyyy-MM-dd HH:mm:ss")) + ")");
                 });

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        private void PlotArea_MouseMove(object sender, Visifire.Charts.PlotAreaMouseEventArgs e)
        {
            this.lkj_chart.ToolTipText = e.XValue.ToString();
        }

        private void PlotArea_MouseLeftButtonDown(object sender, Visifire.Charts.PlotAreaMouseButtonEventArgs e)
        {
            try
            {
                this.lkj_chart_trendLine.Value = e.XValue;
                Task.Run(() =>
                {
                    this.ChangeVideoEvent(null, new ChangeVideoEventArgs(e.XValue as DateTime?));
                });
            }
            catch { }
        }
    }
}
