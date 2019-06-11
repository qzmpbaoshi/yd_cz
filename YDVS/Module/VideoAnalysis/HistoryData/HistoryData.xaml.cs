using System.Windows.Controls;
using System.Windows;
using System.Threading.Tasks;
using System;

namespace VideoAnalysis.HistoryData
{
    /// <summary>
    /// TrainProprietorshipModel.xaml 的交互逻辑
    /// </summary>
    public partial class HistoryData : UserControl
    {
        public HistoryData()
        {
            InitializeComponent();
            this.Loaded += HistoryData_Loaded;
            //this.InitPage();
        }

        private void HistoryData_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Dispatcher.InvokeAsync(() =>
                {
                    this.DirTree.dir_tree_wait.Visibility = Visibility.Visible;
                    this.VideoPlay.video_play_wait.Visibility = Visibility.Visible;
                    this.Chart.chart_wait.Visibility = Visibility.Visible;
                    Task.Run(() =>
                    {
                        this.DirTree.SetVideoSourceEvent += DirTree_SetVideoSourceEvent;
                        this.VideoPlay.ClearVideoChartEvent += this.Chart.ClearVideoSeries;
                        this.VideoPlay.DrawVideoLineEvent += this.Chart.SetVideoSeries;
                        this.VideoPlay.PlayTimeEvent += VideoPage_PlayTimeEvent;
                        this.Chart.ChangeVideoEvent += this.VideoPlay.ChangeVideoPlayByTime;
                        this.DirTree.InitPage();
                        this.VideoPlay.InitPage();
                    }).GetAwaiter().OnCompleted(() =>
                    {
                        this.DirTree.dir_tree_wait.Visibility = Visibility.Collapsed;
                        this.VideoPlay.video_play_wait.Visibility = Visibility.Collapsed;
                        this.Chart.chart_wait.Visibility = Visibility.Collapsed;
                    });
                });
            }
            catch { }
        }

        public void PlayDispose()
        {
            try
            {
                if (this.VideoPlay == null)
                    return;
                this.VideoPlay.PlayDispose();
            }
            catch { }
        }

        private void DirTree_SetVideoSourceEvent(object sender, EventHandler.VideoSourceEventArgs e)
        {
            try
            {
                if (this.VideoPlay == null) return;
                this.Dispatcher.Invoke(() =>
                {
                    this.VideoPlay.video_play_wait.Visibility = Visibility.Visible;
                    this.Chart.chart_wait.Visibility = Visibility.Visible;
                });
                this.Chart.SetLKJChartTitleAsync(e.ParentDirName);
                this.VideoPlay.SetPlayVideoSourceAsync(e.VideoSources);
            }
            catch { }
        }

        private void VideoPage_PlayTimeEvent(object sender, EventHandler.PlayTimeEventArgs e)
        {
            try
            {
                if (e.PlayCurrentTime == null) return;
                this.Chart.MoveTrendLineAsync(e.PlayCurrentTime);
                this.VideoData.RefreshDataByTime((DateTime)e.PlayCurrentTime);
            }
            catch (Exception ex)
            {
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "根据视频播放进度刷新页面数据", ex);
            }
        }
    }
}
