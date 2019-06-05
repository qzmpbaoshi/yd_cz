using System.Windows.Controls;
using VideoAnalysis.HistoryData.PageControl;
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
        public VideoPlay VideoPage { get; set; }
        public HistoryData()
        {
            InitializeComponent();
            this.InitPage();
        }

        public void PlayDispose()
        {
            try
            {
                if (this.VideoPage == null)
                    return;
                this.VideoPage.PlayDispose();
            }
            catch { }
        }

        private void InitPage()
        {
            this.self_wait.Visibility = Visibility.Visible;
            //加载文件夹树结构
            DirectoryTree dirTree = new DirectoryTree();
            dirTree.SetVideoSourceEvent += DirTree_SetVideoSourceEvent;
            this.dic_tree_container_bo.Child = dirTree;
            //加载视频播放页面
            this.VideoPage = new VideoPlay();
            this.VideoPage.PlayTimeEvent += VideoPage_PlayTimeEvent;
            this.video_container_bo.Child = this.VideoPage;
            //加载图形
            LKJChart chart = new LKJChart();
            this.chart_container_tabItem.Content = chart;
            this.Dispatcher.InvokeAsync(() =>
            {
                dirTree.InitPage();
                this.VideoPage.InitPage();
                this.self_wait.Visibility = Visibility.Collapsed;
            });
        }

        private async void DirTree_SetVideoSourceEvent(object sender, Handler.VideoSourceEventArgs e)
        {
            try
            {
                if (this.VideoPage == null) return;
                await this.VideoPage.Dispatcher.InvokeAsync(() =>
                 {
                     this.VideoPage.play_total_time_tb.Text = "******正在准备视频，请稍候";
                 });

                this.VideoPage.SetPlayVideoSource(e.VideoSources);
            }
            catch { }
        }

        private void VideoPage_PlayTimeEvent(object sender, Handler.PlayTimeEventArgs e)
        {
            try
            {
                this.Dispatcher.Invoke(() =>
                {
                    this.play_time_tb.Text = e.PlayCurrentTime == null ? "" : Convert.ToDateTime(e.PlayCurrentTime).ToString("yyyy-MM-dd HH:mm:ss");
                });
            }
            catch
            {
                this.Dispatcher.Invoke(() =>
                {
                    this.play_time_tb.Text = "时间解析错误";
                });
            }
        }
    }
}
