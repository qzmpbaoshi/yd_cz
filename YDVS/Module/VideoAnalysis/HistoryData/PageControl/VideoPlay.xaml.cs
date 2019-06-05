using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using VideoAnalysis.HistoryData.Handler;
using VideoAnalysis.HistoryData.HKHelper;
using VideoAnalysis.HistoryData.ViewModel;
using static VideoAnalysis.HistoryData.HKHelper.PlayCtrlHelper;

namespace VideoAnalysis.HistoryData.PageControl
{
    /// <summary>
    /// VideoPage.xaml 的交互逻辑
    /// </summary>
    public partial class VideoPlay : UserControl
    {
        public event EventHandler<PlayTimeEventArgs> PlayTimeEvent;
        /// <summary>
        /// 海康控件调取结果
        /// </summary>
        private bool bFlag = false;
        /// <summary>
        /// 页面中所有播放资源
        /// </summary>
        private static List<VideoViewModel> VideoSources = new List<VideoViewModel>();
        /// <summary>
        /// 播放器容器
        /// </summary>
        private List<Border> PlayBorderContainers = new List<Border>();
        /// <summary>
        /// 暂停播放开关量
        /// </summary>
        private int nPause = -1;
        public VideoPlay()
        {
            InitializeComponent();
            this.win_16_btn.IsEnabled = false;
            this.selectBtn = this.win_16_btn;
        }
        /// <summary>
        /// 播放视频当前时间 计时器
        /// </summary>
        private Timer playTimer;
        public void InitPage()
        {
            this.PlayDispose();
            this.InitPlayBorderContainers();
            this.InitVideoSource();

            this.playTimer = new Timer();
            this.playTimer.Interval = 1000;
            this.playTimer.Elapsed += PlayTimer_Elapsed;
        }
        private void PlayTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                PLAYM4_SYSTEM_TIME time = new PLAYM4_SYSTEM_TIME();
                bool bo = PlayCtrlHelper.PlayM4_GetSystemTime(VideoPlay.VideoSources[0].PlayPort, ref time);
                DateTime dt = new DateTime((int)time.dwYear, (int)time.dwMonth, (int)time.dwDay, (int)time.dwHour, (int)time.dwMinute, (int)time.dwSecond - 1);
                if (this.PlayTimeEvent != null)
                    this.PlayTimeEvent(this, new PlayTimeEventArgs(dt));

                foreach (VideoViewModel vvModel in VideoPlay.VideoSources)
                {
                    if (vvModel.Videos == null || vvModel.Videos.Count() <= 0) continue;
                    uint playedFrames = PlayCtrlHelper.PlayM4_GetPlayedFrames(vvModel.PlayPort);
                    if (playedFrames >= (vvModel.Videos[vvModel.PlayIndex].TotalFrames - 1))
                    {
                        this.ChangeVideoPlay(vvModel, vvModel.PlayIndex + 1);
                    }
                }
            }
            catch
            {

            }
        }
        #region 初始化视频播放页面数据
        /// <summary>
        /// 初始化视频插件容器
        /// </summary>
        public void InitPlayBorderContainers()
        {
            this.PlayBorderContainers = new List<Border>();
            this.PlayBorderContainers.Add(this.player_1_bo);
            this.PlayBorderContainers.Add(this.player_2_bo);
            this.PlayBorderContainers.Add(this.player_3_bo);
            this.PlayBorderContainers.Add(this.player_4_bo);
            this.PlayBorderContainers.Add(this.player_5_bo);
            this.PlayBorderContainers.Add(this.player_6_bo);
            this.PlayBorderContainers.Add(this.player_7_bo);
            this.PlayBorderContainers.Add(this.player_8_bo);
            this.PlayBorderContainers.Add(this.player_9_bo);
            this.PlayBorderContainers.Add(this.player_10_bo);
            this.PlayBorderContainers.Add(this.player_11_bo);
            this.PlayBorderContainers.Add(this.player_12_bo);
            this.PlayBorderContainers.Add(this.player_13_bo);
            this.PlayBorderContainers.Add(this.player_14_bo);
            this.PlayBorderContainers.Add(this.player_15_bo);
            this.PlayBorderContainers.Add(this.player_16_bo);
        }
        /// <summary>
        /// 初始化播放视频资源
        /// </summary>
        private void InitVideoSource()
        {
            VideoPlay.VideoSources = new List<VideoViewModel>();
            this.AddVideoSources(new List<VideoSource>(), this.player_1.Handle);
            this.AddVideoSources(new List<VideoSource>(), this.player_2.Handle);
            this.AddVideoSources(new List<VideoSource>(), this.player_3.Handle);
            this.AddVideoSources(new List<VideoSource>(), this.player_4.Handle);
            this.AddVideoSources(new List<VideoSource>(), this.player_5.Handle);
            this.AddVideoSources(new List<VideoSource>(), this.player_6.Handle);
            this.AddVideoSources(new List<VideoSource>(), this.player_7.Handle);
            this.AddVideoSources(new List<VideoSource>(), this.player_8.Handle);
            this.AddVideoSources(new List<VideoSource>(), this.player_9.Handle);
            this.AddVideoSources(new List<VideoSource>(), this.player_10.Handle);
            this.AddVideoSources(new List<VideoSource>(), this.player_11.Handle);
            this.AddVideoSources(new List<VideoSource>(), this.player_12.Handle);
            this.AddVideoSources(new List<VideoSource>(), this.player_13.Handle);
            this.AddVideoSources(new List<VideoSource>(), this.player_14.Handle);
            this.AddVideoSources(new List<VideoSource>(), this.player_15.Handle);
            this.AddVideoSources(new List<VideoSource>(), this.player_16.Handle);
            //FileEndCallback fFileEndCallback = new FileEndCallback(SetFileEndCallback);//预览实时流回调函数
            //bool isSuccess = PlayCtrlHelper.PlayM4_SetFileEndCallback(VideoPlay.VideoSources[0].PlayPort, fFileEndCallback, IntPtr.Zero);
            //Console.WriteLine("*************通道：" + VideoPlay.VideoSources[0].PlayPort + "，" + (isSuccess ? "播放器回调函数设置成功" : "播放器回调函数设置失败") + "*************");
        }
        private void AddVideoSources(List<VideoSource> videos, IntPtr handle)
        {
            int tempPort = -1;
            PlayCtrlHelper.PlayM4_GetPort(ref tempPort);
            VideoPlay.VideoSources.Add(new VideoViewModel(tempPort, handle, videos, 0, true, true));
        }
        /// <summary>
        /// 切换视频播放
        /// </summary>
        /// <param name="vvModel">播放资源</param>
        /// <param name="_playIndex">播放视频的位置指针</param>
        private void ChangeVideoPlay(VideoViewModel vvModel, int _playIndex)
        {
            try
            {
                Int32 nPort = vvModel.PlayPort;
                Console.WriteLine("通道" + nPort + "：当前视频播放结束，开始播放下一个视频");
                this.playTimer.Stop();
                PlayCtrlHelper.PlayM4_Stop(nPort);
                PlayCtrlHelper.PlayM4_CloseFile(nPort);
                vvModel.PlayIndex = _playIndex;
                Console.WriteLine("通道" + nPort + "：正在播放第" + (vvModel.PlayIndex + 1) + "个视频");
                this.OpenFile_Video(vvModel);
                this.Play_Video(vvModel);
                this.playTimer.Start();
                Console.WriteLine("通道" + nPort + "：正在播放第" + (vvModel.PlayIndex + 1) + "个视频；跳转成功");
            }
            catch (Exception ex)
            {
                Console.WriteLine("**********切换视频播放时，错误：" + ex.ToString());
            }
        }
        public void SetPlayVideoSource(List<VideoSource> Videos)
        {
            try
            {
                this.PlayCloseFile();
                foreach (var vs in VideoPlay.VideoSources)
                {
                    vs.Videos.Clear();
                }
                Videos = Videos.OrderBy(vs => vs.StartTime).ToList();
                foreach (VideoSource vs in Videos)
                {
                    if (vs.VideoChannel <= 0) continue;
                    //int pPort = VideoPlay.VideoSources[vs.VideoChannel - 1].PlayPort;
                    //this.videoSourceDoneFileRef = vs;
                    //PlayCtrlHelper.PlayM4_OpenFile(pPort, vs.FullPathName);
                    //vs.DurationSecond = PlayCtrlHelper.PlayM4_GetFileTime(pPort);
                    //vs.EndTime = vs.StartTime.AddSeconds(vs.DurationSecond);
                    //vs.TotalFrames = PlayCtrlHelper.PlayM4_GetFileTotalFrames(pPort);
                    VideoPlay.VideoSources[vs.VideoChannel - 1].Videos.Add(vs);
                    //PlayCtrlHelper.PlayM4_CloseFile(pPort);
                }

                foreach (var vvModel in VideoPlay.VideoSources)
                {
                    this.OpenFile_Video(vvModel);
                }

                this.Dispatcher.Invoke(() =>
                {
                    this.play_total_time_tb.Text = "******视频准备就绪，请播放";
                });
            }
            catch (Exception ex)
            {
                this.Dispatcher.Invoke(() =>
                {
                    this.play_total_time_tb.Text = "视频准备过程报错：" + ex.ToString();
                });
            }
        }

        #endregion

        #region 视频播放控制
        private void StartPlay_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (this.nPause == -1)
            {
                //IntPtr hwnd1 = ((HwndSource)PresentationSource.FromVisual(this.player_1)).Handle;
                //IntPtr hwnd2 = ((HwndSource)PresentationSource.FromVisual(this.player_2)).Handle;
                this.Play();
                this.nPause = 1;
                btn.Content = "暂停";
            }
            else
            {
                this.Pause();
                if (this.nPause == 1)
                {
                    btn.Content = "播放";
                    this.nPause = 0;
                }
                else
                {
                    btn.Content = "暂停";
                    this.nPause = 1;
                }
            }
        }
        private void StopPlay_Click(object sender, RoutedEventArgs e)
        {
            this.Stop();
            this.nPause = -1;
            this.play_btn.Content = "播放";

        }

        #region 海康控件 视频控制
        public void PlayDispose()
        {
            try
            {
                if (this.playTimer != null)
                    this.playTimer.Dispose();
                this.StopPlay_Click(null, null);
                if (VideoPlay.VideoSources == null || VideoPlay.VideoSources.Count == 0) return;
                foreach (var vs in VideoPlay.VideoSources)
                {
                    PlayCtrlHelper.PlayM4_Stop(vs.PlayPort);
                    PlayCtrlHelper.PlayM4_CloseFile(vs.PlayPort);
                    PlayCtrlHelper.PlayM4_FreePort(vs.PlayPort);
                }
            }
            catch { }
        }
        public void PlayCloseFile()
        {
            try
            {
                if (this.playTimer != null)
                    this.playTimer.Stop();
                this.StopPlay_Click(null, null);
                if (VideoPlay.VideoSources == null || VideoPlay.VideoSources.Count == 0) return;
                foreach (var vs in VideoPlay.VideoSources)
                {
                    PlayCtrlHelper.PlayM4_Stop(vs.PlayPort);
                    PlayCtrlHelper.PlayM4_CloseFile(vs.PlayPort);
                }
            }
            catch { }
        }
        private void Play()
        {
            foreach (var vvModel in VideoPlay.VideoSources)
            {
                this.Play_Video(vvModel);
            }
            this.playTimer.Start();
        }

        private void Pause()
        {
            if (this.nPause == 1)
                this.playTimer.Stop();
            else
                this.playTimer.Start();
            foreach (var vs in VideoPlay.VideoSources)
            {
                if (vs.Videos.Count <= 0) continue;
                PlayCtrlHelper.PlayM4_Pause(vs.PlayPort, nPause);
            }
        }
        private void Stop()
        {
            this.playTimer.Stop();
            foreach (var vs in VideoPlay.VideoSources)
            {
                if (vs.Videos.Count <= 0) continue;
                PlayCtrlHelper.PlayM4_Stop(vs.PlayPort);
            }
        }
        private void OpenFile_Video(VideoViewModel vvModel)
        {
            if (vvModel.Videos == null || vvModel.Videos.Count <= 0) return;
            // 创建文件索引
            bool flag = PlayCtrlHelper.PlayM4_SetFileRefCallBack(vvModel.PlayPort, pFileRefDone, IntPtr.Zero);
            if (flag)
                Console.WriteLine("通道" + vvModel.PlayPort + "设置创建索引事件成功！");
            else
            {
                uint errorCode = PlayCtrlHelper.PlayM4_GetLastError(vvModel.PlayPort);
                Console.WriteLine("通道" + vvModel.PlayPort + "设置创建索引事件失败！失败码：" + errorCode);
            }
            Console.WriteLine("通道" + vvModel.PlayPort + "设置创建索引事件" + (flag ? "成功！" : "失败！"));
            vvModel.Videos[vvModel.PlayIndex].IsRefDone = false;
            PlayCtrlHelper.PlayM4_OpenFile(vvModel.PlayPort, vvModel.Videos[vvModel.PlayIndex].FullPathName);
            if (vvModel.IsNeedGroup)
            {
                PlayCtrlHelper.PlayM4_SetSycGroup(vvModel.PlayPort, 0);
            }
        }
        private static FileRefDoneCB pFileRefDone = new FileRefDoneCB(SetFileRefDoneCB);
        private static void SetFileRefDoneCB(uint nPort, IntPtr nUser)
        {
            var vvModel = VideoPlay.VideoSources.Where(vs => vs.PlayPort == nPort).FirstOrDefault();
            if (vvModel.Videos == null || vvModel.Videos.Count <= 0) return;
            vvModel.Videos[vvModel.PlayIndex].IsRefDone = true;
        }

        private void Play_Video(VideoViewModel vvModel)
        {
            if (vvModel.Videos == null || vvModel.Videos.Count <= 0) return;
            this.bFlag = PlayCtrlHelper.PlayM4_Play(vvModel.PlayPort, vvModel.PlayIntPtr);
            if (!vvModel.IsNeedRef) return;
            while (true)
            {
                if (!vvModel.Videos[vvModel.PlayIndex].IsRefDone)
                {
                    System.Threading.Thread.Sleep(2);
                    continue;
                }
                else//索引创建成功
                {
                    Console.WriteLine("通道" + vvModel.PlayPort + "：" + vvModel.Videos[vvModel.PlayIndex].FullPathName + "索引创建成功！");
                    break;
                }
            }
        }
        #endregion
        #endregion

        #region 页面视频选择及窗口大小控制
        private bool isMaxPlay = false;
        private void PlayWinMaxOrMin(Border play_bo)
        {
            if (!this.isMaxPlay)
            {
                UIElement playUI = play_bo.Child;
                play_bo.Child = null;
                this.player_max_bo.Child = playUI;
                this.player_max_bo.Visibility = Visibility.Visible;
                this.player_max_bo.BorderBrush = Brushes.Lime;
                this.isMaxPlay = true;
            }
            else
            {
                UIElement playUI = this.player_max_bo.Child;
                this.player_max_bo.Child = null;
                play_bo.Child = playUI;
                this.player_max_bo.Visibility = Visibility.Collapsed;
                this.player_max_bo.BorderBrush = Brushes.Lime;
                this.isMaxPlay = false;
            }
        }
        private void Player_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            System.Windows.Forms.PictureBox pBox = sender as System.Windows.Forms.PictureBox;
            Border selectBo = this.PlayBorderContainers.Where(b => b.Name == pBox.Tag.ToString()).FirstOrDefault();
            if (selectBo != null)
                this.PlayWinMaxOrMin(selectBo);
        }

        private void Player_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            System.Windows.Forms.PictureBox pBox = sender as System.Windows.Forms.PictureBox;
            string selectBoName = pBox.Tag.ToString();
            Border selectBo = null;
            foreach (Border bo in this.PlayBorderContainers)
            {
                if (selectBoName == bo.Name)
                {
                    selectBo = bo;
                    continue;
                }
                bo.BorderBrush = Brushes.White;
            }
            if (selectBo != null)
                selectBo.BorderBrush = selectBo.BorderBrush == Brushes.Lime ? Brushes.White : Brushes.Lime;
        }
        #endregion

        #region 播放窗口数量选择
        private Button selectBtn = null;
        private void SetSelectBtn(Button btn)
        {
            if (btn == null) return;
            if (this.selectBtn != null) this.selectBtn.IsEnabled = true;
            this.selectBtn = btn;
            this.selectBtn.IsEnabled = false;
        }
        private void Play_Win_1_Click(object sender, RoutedEventArgs e)
        {
            Border bo = this.PlayBorderContainers.FirstOrDefault();
            bo.Visibility = Visibility.Visible;
            Grid.SetColumnSpan(bo, 4);
            Grid.SetRowSpan(bo, 4);
            this.HidePlayBorderContainer(1);
            this.SetSelectBtn(sender as Button);
        }

        private void Play_Win_4_Click(object sender, RoutedEventArgs e)
        {
            this.HidePlayBorderContainer(4);
            for (int i = 0; i < 4; i++)
            {
                Border bo = this.PlayBorderContainers[i];
                switch (i)
                {
                    case 0:
                        Grid.SetColumn(bo, 0);
                        Grid.SetRow(bo, 0);
                        Grid.SetColumnSpan(bo, 2);
                        Grid.SetRowSpan(bo, 2);
                        break;
                    case 1:
                        Grid.SetColumn(bo, 2);
                        Grid.SetRow(bo, 0);
                        Grid.SetColumnSpan(bo, 2);
                        Grid.SetRowSpan(bo, 2);
                        break;
                    case 2:
                        Grid.SetColumn(bo, 0);
                        Grid.SetRow(bo, 2);
                        Grid.SetColumnSpan(bo, 2);
                        Grid.SetRowSpan(bo, 2);
                        break;
                    case 3:
                        Grid.SetColumn(bo, 2);
                        Grid.SetRow(bo, 2);
                        Grid.SetColumnSpan(bo, 2);
                        Grid.SetRowSpan(bo, 2);
                        break;
                }
                bo.Visibility = Visibility.Visible;
            }
            this.SetSelectBtn(sender as Button);
        }

        private void Play_Win_6_Click(object sender, RoutedEventArgs e)
        {
            this.HidePlayBorderContainer(6);
            for (int i = 0; i < 6; i++)
            {
                Border bo = this.PlayBorderContainers[i];
                switch (i)
                {
                    case 0:
                        Grid.SetColumn(bo, 0);
                        Grid.SetRow(bo, 0);
                        Grid.SetColumnSpan(bo, 2);
                        Grid.SetRowSpan(bo, 2);
                        break;
                    case 1:
                        Grid.SetColumn(bo, 2);
                        Grid.SetRow(bo, 0);
                        Grid.SetColumnSpan(bo, 2);
                        Grid.SetRowSpan(bo, 2);
                        break;
                    case 2:
                        Grid.SetColumn(bo, 0);
                        Grid.SetRow(bo, 2);
                        Grid.SetColumnSpan(bo, 2);
                        Grid.SetRowSpan(bo, 1);
                        break;
                    case 3:
                        Grid.SetColumn(bo, 2);
                        Grid.SetRow(bo, 2);
                        Grid.SetColumnSpan(bo, 2);
                        Grid.SetRowSpan(bo, 1);
                        break;
                    case 4:
                        Grid.SetColumn(bo, 0);
                        Grid.SetRow(bo, 3);
                        Grid.SetColumnSpan(bo, 2);
                        Grid.SetRowSpan(bo, 1);
                        break;
                    case 5:
                        Grid.SetColumn(bo, 2);
                        Grid.SetRow(bo, 3);
                        Grid.SetColumnSpan(bo, 2);
                        Grid.SetRowSpan(bo, 1);
                        break;
                }
                bo.Visibility = Visibility.Visible;
            }
            this.SetSelectBtn(sender as Button);
        }

        private void Play_Win_8_Click(object sender, RoutedEventArgs e)
        {
            this.HidePlayBorderContainer(8);
            for (int i = 0; i < 8; i++)
            {
                Border bo = this.PlayBorderContainers[i];
                switch (i)
                {
                    case 0:
                        Grid.SetColumn(bo, 0);
                        Grid.SetRow(bo, 0);
                        Grid.SetColumnSpan(bo, 3);
                        Grid.SetRowSpan(bo, 3);
                        break;
                    case 1:
                        Grid.SetColumn(bo, 3);
                        Grid.SetRow(bo, 0);
                        Grid.SetColumnSpan(bo, 1);
                        Grid.SetRowSpan(bo, 1);
                        break;
                    case 2:
                        Grid.SetColumn(bo, 3);
                        Grid.SetRow(bo, 1);
                        Grid.SetColumnSpan(bo, 1);
                        Grid.SetRowSpan(bo, 1);
                        break;
                    case 3:
                        Grid.SetColumn(bo, 3);
                        Grid.SetRow(bo, 2);
                        Grid.SetColumnSpan(bo, 1);
                        Grid.SetRowSpan(bo, 1);
                        break;
                    case 4:
                        Grid.SetColumn(bo, 0);
                        Grid.SetRow(bo, 3);
                        Grid.SetColumnSpan(bo, 1);
                        Grid.SetRowSpan(bo, 1);
                        break;
                    case 5:
                        Grid.SetColumn(bo, 1);
                        Grid.SetRow(bo, 3);
                        Grid.SetColumnSpan(bo, 1);
                        Grid.SetRowSpan(bo, 1);
                        break;
                    case 6:
                        Grid.SetColumn(bo, 2);
                        Grid.SetRow(bo, 3);
                        Grid.SetColumnSpan(bo, 1);
                        Grid.SetRowSpan(bo, 1);
                        break;
                    case 7:
                        Grid.SetColumn(bo, 3);
                        Grid.SetRow(bo, 3);
                        Grid.SetColumnSpan(bo, 1);
                        Grid.SetRowSpan(bo, 1);
                        break;
                }
                bo.Visibility = Visibility.Visible;
            }
            this.SetSelectBtn(sender as Button);
        }

        private void Play_Win_9_Click(object sender, RoutedEventArgs e)
        {
            this.HidePlayBorderContainer(9);
            for (int i = 0; i < 9; i++)
            {
                Border bo = this.PlayBorderContainers[i];
                switch (i)
                {
                    case 0:
                        Grid.SetColumn(bo, 0);
                        Grid.SetRow(bo, 0);
                        Grid.SetColumnSpan(bo, 4);
                        Grid.SetRowSpan(bo, 2);
                        break;
                    case 1:
                        Grid.SetColumn(bo, 0);
                        Grid.SetRow(bo, 2);
                        Grid.SetColumnSpan(bo, 1);
                        Grid.SetRowSpan(bo, 1);
                        break;
                    case 2:
                        Grid.SetColumn(bo, 1);
                        Grid.SetRow(bo, 2);
                        Grid.SetColumnSpan(bo, 1);
                        Grid.SetRowSpan(bo, 1);
                        break;
                    case 3:
                        Grid.SetColumn(bo, 2);
                        Grid.SetRow(bo, 2);
                        Grid.SetColumnSpan(bo, 1);
                        Grid.SetRowSpan(bo, 1);
                        break;
                    case 4:
                        Grid.SetColumn(bo, 3);
                        Grid.SetRow(bo, 2);
                        Grid.SetColumnSpan(bo, 1);
                        Grid.SetRowSpan(bo, 1);
                        break;
                    case 5:
                        Grid.SetColumn(bo, 0);
                        Grid.SetRow(bo, 3);
                        Grid.SetColumnSpan(bo, 1);
                        Grid.SetRowSpan(bo, 1);
                        break;
                    case 6:
                        Grid.SetColumn(bo, 1);
                        Grid.SetRow(bo, 3);
                        Grid.SetColumnSpan(bo, 1);
                        Grid.SetRowSpan(bo, 1);
                        break;
                    case 7:
                        Grid.SetColumn(bo, 2);
                        Grid.SetRow(bo, 3);
                        Grid.SetColumnSpan(bo, 1);
                        Grid.SetRowSpan(bo, 1);
                        break;
                    case 8:
                        Grid.SetColumn(bo, 3);
                        Grid.SetRow(bo, 3);
                        Grid.SetColumnSpan(bo, 1);
                        Grid.SetRowSpan(bo, 1);
                        break;
                }
                bo.Visibility = Visibility.Visible;
            }
            this.SetSelectBtn(sender as Button);
        }

        private void Play_Win_10_Click(object sender, RoutedEventArgs e)
        {
            this.HidePlayBorderContainer(10);
            for (int i = 0; i < 10; i++)
            {
                Border bo = this.PlayBorderContainers[i];
                switch (i)
                {
                    case 0:
                        Grid.SetColumn(bo, 0);
                        Grid.SetRow(bo, 0);
                        Grid.SetColumnSpan(bo, 2);
                        Grid.SetRowSpan(bo, 2);
                        break;
                    case 1:
                        Grid.SetColumn(bo, 2);
                        Grid.SetRow(bo, 0);
                        Grid.SetColumnSpan(bo, 2);
                        Grid.SetRowSpan(bo, 2);
                        break;
                    case 2:
                        Grid.SetColumn(bo, 0);
                        Grid.SetRow(bo, 2);
                        Grid.SetColumnSpan(bo, 1);
                        Grid.SetRowSpan(bo, 1);
                        break;
                    case 3:
                        Grid.SetColumn(bo, 1);
                        Grid.SetRow(bo, 2);
                        Grid.SetColumnSpan(bo, 1);
                        Grid.SetRowSpan(bo, 1);
                        break;
                    case 4:
                        Grid.SetColumn(bo, 2);
                        Grid.SetRow(bo, 2);
                        Grid.SetColumnSpan(bo, 1);
                        Grid.SetRowSpan(bo, 1);
                        break;
                    case 5:
                        Grid.SetColumn(bo, 3);
                        Grid.SetRow(bo, 2);
                        Grid.SetColumnSpan(bo, 1);
                        Grid.SetRowSpan(bo, 1);
                        break;
                    case 6:
                        Grid.SetColumn(bo, 0);
                        Grid.SetRow(bo, 3);
                        Grid.SetColumnSpan(bo, 1);
                        Grid.SetRowSpan(bo, 1);
                        break;
                    case 7:
                        Grid.SetColumn(bo, 1);
                        Grid.SetRow(bo, 3);
                        Grid.SetColumnSpan(bo, 1);
                        Grid.SetRowSpan(bo, 1);
                        break;
                    case 8:
                        Grid.SetColumn(bo, 2);
                        Grid.SetRow(bo, 3);
                        Grid.SetColumnSpan(bo, 1);
                        Grid.SetRowSpan(bo, 1);
                        break;
                    case 9:
                        Grid.SetColumn(bo, 3);
                        Grid.SetRow(bo, 3);
                        Grid.SetColumnSpan(bo, 1);
                        Grid.SetRowSpan(bo, 1);
                        break;
                }
                bo.Visibility = Visibility.Visible;
            }
            this.SetSelectBtn(sender as Button);
        }

        private void Play_Win_13_Click(object sender, RoutedEventArgs e)
        {
            this.HidePlayBorderContainer(13);
            for (int i = 0; i < 13; i++)
            {
                Border bo = this.PlayBorderContainers[i];
                switch (i)
                {
                    case 0:
                        Grid.SetColumn(bo, 0);
                        Grid.SetRow(bo, 0);
                        Grid.SetColumnSpan(bo, 2);
                        Grid.SetRowSpan(bo, 2);
                        break;
                    case 1:
                        Grid.SetColumn(bo, 2);
                        Grid.SetRow(bo, 0);
                        Grid.SetColumnSpan(bo, 1);
                        Grid.SetRowSpan(bo, 1);
                        break;
                    case 2:
                        Grid.SetColumn(bo, 3);
                        Grid.SetRow(bo, 0);
                        Grid.SetColumnSpan(bo, 1);
                        Grid.SetRowSpan(bo, 1);
                        break;
                    case 3:
                        Grid.SetColumn(bo, 2);
                        Grid.SetRow(bo, 1);
                        Grid.SetColumnSpan(bo, 1);
                        Grid.SetRowSpan(bo, 1);
                        break;
                    case 4:
                        Grid.SetColumn(bo, 3);
                        Grid.SetRow(bo, 1);
                        Grid.SetColumnSpan(bo, 1);
                        Grid.SetRowSpan(bo, 1);
                        break;
                    case 5:
                        Grid.SetColumn(bo, 0);
                        Grid.SetRow(bo, 2);
                        Grid.SetColumnSpan(bo, 1);
                        Grid.SetRowSpan(bo, 1);
                        break;
                    case 6:
                        Grid.SetColumn(bo, 1);
                        Grid.SetRow(bo, 2);
                        Grid.SetColumnSpan(bo, 1);
                        Grid.SetRowSpan(bo, 1);
                        break;
                    case 7:
                        Grid.SetColumn(bo, 2);
                        Grid.SetRow(bo, 2);
                        Grid.SetColumnSpan(bo, 1);
                        Grid.SetRowSpan(bo, 1);
                        break;
                    case 8:
                        Grid.SetColumn(bo, 3);
                        Grid.SetRow(bo, 2);
                        Grid.SetColumnSpan(bo, 1);
                        Grid.SetRowSpan(bo, 1);
                        break;
                    case 9:
                        Grid.SetColumn(bo, 0);
                        Grid.SetRow(bo, 3);
                        Grid.SetColumnSpan(bo, 1);
                        Grid.SetRowSpan(bo, 1);
                        break;
                    case 10:
                        Grid.SetColumn(bo, 1);
                        Grid.SetRow(bo, 3);
                        Grid.SetColumnSpan(bo, 1);
                        Grid.SetRowSpan(bo, 1);
                        break;
                    case 11:
                        Grid.SetColumn(bo, 2);
                        Grid.SetRow(bo, 3);
                        Grid.SetColumnSpan(bo, 1);
                        Grid.SetRowSpan(bo, 1);
                        break;
                    case 12:
                        Grid.SetColumn(bo, 3);
                        Grid.SetRow(bo, 3);
                        Grid.SetColumnSpan(bo, 1);
                        Grid.SetRowSpan(bo, 1);
                        break;
                }
                bo.Visibility = Visibility.Visible;
            }
            this.SetSelectBtn(sender as Button);
        }

        private void Play_Win_16_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 16; i++)
            {
                Border bo = this.PlayBorderContainers[i];
                switch (i)
                {
                    case 0:
                        Grid.SetColumn(bo, 0);
                        Grid.SetRow(bo, 0);
                        Grid.SetColumnSpan(bo, 1);
                        Grid.SetRowSpan(bo, 1);
                        break;
                    case 1:
                        Grid.SetColumn(bo, 1);
                        Grid.SetRow(bo, 0);
                        Grid.SetColumnSpan(bo, 1);
                        Grid.SetRowSpan(bo, 1);
                        break;
                    case 2:
                        Grid.SetColumn(bo, 2);
                        Grid.SetRow(bo, 0);
                        Grid.SetColumnSpan(bo, 1);
                        Grid.SetRowSpan(bo, 1);
                        break;
                    case 3:
                        Grid.SetColumn(bo, 3);
                        Grid.SetRow(bo, 0);
                        Grid.SetColumnSpan(bo, 1);
                        Grid.SetRowSpan(bo, 1);
                        break;
                    case 4:
                        Grid.SetColumn(bo, 0);
                        Grid.SetRow(bo, 1);
                        Grid.SetColumnSpan(bo, 1);
                        Grid.SetRowSpan(bo, 1);
                        break;
                    case 5:
                        Grid.SetColumn(bo, 1);
                        Grid.SetRow(bo, 1);
                        Grid.SetColumnSpan(bo, 1);
                        Grid.SetRowSpan(bo, 1);
                        break;
                    case 6:
                        Grid.SetColumn(bo, 2);
                        Grid.SetRow(bo, 1);
                        Grid.SetColumnSpan(bo, 1);
                        Grid.SetRowSpan(bo, 1);
                        break;
                    case 7:
                        Grid.SetColumn(bo, 3);
                        Grid.SetRow(bo, 1);
                        Grid.SetColumnSpan(bo, 1);
                        Grid.SetRowSpan(bo, 1);
                        break;
                    case 8:
                        Grid.SetColumn(bo, 0);
                        Grid.SetRow(bo, 2);
                        Grid.SetColumnSpan(bo, 1);
                        Grid.SetRowSpan(bo, 1);
                        break;
                    case 9:
                        Grid.SetColumn(bo, 1);
                        Grid.SetRow(bo, 2);
                        Grid.SetColumnSpan(bo, 1);
                        Grid.SetRowSpan(bo, 1);
                        break;
                    case 10:
                        Grid.SetColumn(bo, 2);
                        Grid.SetRow(bo, 2);
                        Grid.SetColumnSpan(bo, 1);
                        Grid.SetRowSpan(bo, 1);
                        break;
                    case 11:
                        Grid.SetColumn(bo, 3);
                        Grid.SetRow(bo, 2);
                        Grid.SetColumnSpan(bo, 1);
                        Grid.SetRowSpan(bo, 1);
                        break;
                    case 12:
                        Grid.SetColumn(bo, 0);
                        Grid.SetRow(bo, 3);
                        Grid.SetColumnSpan(bo, 1);
                        Grid.SetRowSpan(bo, 1);
                        break;
                    case 13:
                        Grid.SetColumn(bo, 1);
                        Grid.SetRow(bo, 3);
                        Grid.SetColumnSpan(bo, 1);
                        Grid.SetRowSpan(bo, 1);
                        break;
                    case 14:
                        Grid.SetColumn(bo, 2);
                        Grid.SetRow(bo, 3);
                        Grid.SetColumnSpan(bo, 1);
                        Grid.SetRowSpan(bo, 1);
                        break;
                    case 15:
                        Grid.SetColumn(bo, 3);
                        Grid.SetRow(bo, 3);
                        Grid.SetColumnSpan(bo, 1);
                        Grid.SetRowSpan(bo, 1);
                        break;
                }
                bo.Visibility = Visibility.Visible;
            }
            this.SetSelectBtn(sender as Button);
        }
        private void HidePlayBorderContainer(int startIndex)
        {
            for (int i = startIndex; i < 16; i++)
            {
                this.PlayBorderContainers[i].Visibility = Visibility.Collapsed;
            }
        }
        #endregion

        private void SetSycStartTime_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null) return;
            DateTime dt = Convert.ToDateTime(btn.Content);
            foreach (VideoViewModel vvModel in VideoPlay.VideoSources)
            {
                if (vvModel.Videos == null || vvModel.Videos.Count() <= 0) continue;
                VideoSource vs = vvModel.Videos.Where(v => v.StartTime <= dt && dt < v.EndTime).FirstOrDefault();
                if (vs == null) continue;
                this.ChangeVideoPlay(vvModel, vvModel.Videos.IndexOf(vs));
                DateTime startDT = vs.StartTime;
                TimeSpan ts = dt.Subtract(startDT);
                uint nTime = (uint)ts.TotalMilliseconds;
                bool flag = PlayCtrlHelper.PlayM4_SetPlayedTimeEx(vvModel.PlayPort, nTime);
                if (flag)
                {
                    Console.WriteLine("通道" + vvModel.PlayPort + "视频时间跳转成功！");
                }
                else
                {
                    uint errorCode = PlayCtrlHelper.PlayM4_GetLastError(vvModel.PlayPort);
                    Console.WriteLine("通道" + vvModel.PlayPort + "视频时间跳转成功！失败码：" + errorCode);
                }
            }
        }
    }
}
