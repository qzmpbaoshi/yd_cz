﻿using SelfCommonTool;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using VideoAnalysis.HistoryData.Common;
using VideoAnalysis.HistoryData.EventHandler;
using VideoAnalysis.HistoryData.HKHelper;
using VideoAnalysis.HistoryData.PageControl.ChildPage;
using VideoAnalysis.HistoryData.ViewModel;
using Visifire.Charts;
using static VideoAnalysis.HistoryData.HKHelper.PlayCtrlHelper;

namespace VideoAnalysis.HistoryData.PageControl
{
    /// <summary>
    /// VideoPage.xaml 的交互逻辑
    /// </summary>
    public partial class VideoPlay : UserControl
    {
        public event EventHandler<PlayTimeEventArgs> PlayTimeEvent;
        public event EventHandler<DrawVideoLineEventArgs> DrawVideoLineEvent;
        public event EventHandler<EventArgs> ClearVideoChartEvent;
        /// <summary>
        /// 海康控件调取结果
        /// </summary>
        private bool bFlag = false;
        /// <summary>
        /// 页面中所有播放资源
        /// </summary>
        private static List<VideoViewModel> VideoViewModels = new List<VideoViewModel>();
        /// <summary>
        /// 播放器容器
        /// </summary>
        private List<Border> PlayBorderContainers = new List<Border>();
        public string VideDateStr { get; set; }
        public VideoPlay()
        {
            InitializeComponent();

        }
        #region 初始化视频播放页面数据
        /// <summary>
        /// 播放视频当前时间 计时器
        /// </summary>
        private Timer playTimer;
        public void InitPage()
        {
            this.PlayDispose();
            this.Dispatcher.InvokeAsync(() =>
            {
                this.win_16_btn.IsEnabled = false;
                this.selectBtn = this.win_16_btn;
                this.showWinCount = 16;
                this.InitPlayBorderContainers();
                this.InitVideoSource();
            });
            this.playTimer = new Timer();
            this.playTimer.Interval = 1000;
            this.playTimer.Elapsed += PlayTimer_Elapsed;
        }
        private void PlayTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                PLAYM4_SYSTEM_TIME time = new PLAYM4_SYSTEM_TIME();
                var tempModel = VideoPlay.VideoViewModels.Where(m => m.PlayIndex >= 0).FirstOrDefault();
                if (tempModel == null) return;
                bool bo = PlayCtrlHelper.PlayM4_GetSystemTime(tempModel.PlayPort, ref time);
                DateTime dt = new DateTime((int)time.dwYear, (int)time.dwMonth, (int)time.dwDay, (int)time.dwHour, (int)time.dwMinute, (int)time.dwSecond - 1);
                this.Dispatcher.Invoke(() =>
                {
                    this.play_time_tb.Text = dt.ToString("yyyy-MM-dd HH:mm:ss");
                });
                if (this.PlayTimeEvent != null)
                    this.PlayTimeEvent(this, new PlayTimeEventArgs(dt));

                foreach (VideoViewModel vvModel in VideoPlay.VideoViewModels)
                {
                    if (vvModel.PlayIndex < 0 || (vvModel.Videos[vvModel.PlayIndex].TotalFrames - 1) <= 0) continue;
                    uint playedFrames = PlayCtrlHelper.PlayM4_GetPlayedFrames(vvModel.PlayPort);
                    if (playedFrames >= (vvModel.Videos[vvModel.PlayIndex].TotalFrames - 1))
                    {
                        this.playTimer.Stop();
                        this.CloseFile_Video(vvModel);
                        this.ChangeVideoPlay(vvModel, vvModel.PlayIndex + 1);
                        this.playTimer.Start();
                    }
                }
            }
            catch
            {

            }
        }

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
            VideoPlay.VideoViewModels = new List<VideoViewModel>();
            this.AddVideoSources(new List<VideoSource>(), 1, this.player_1.Handle);
            this.AddVideoSources(new List<VideoSource>(), 2, this.player_2.Handle);
            this.AddVideoSources(new List<VideoSource>(), 3, this.player_3.Handle);
            this.AddVideoSources(new List<VideoSource>(), 4, this.player_4.Handle);
            this.AddVideoSources(new List<VideoSource>(), 5, this.player_5.Handle);
            this.AddVideoSources(new List<VideoSource>(), 6, this.player_6.Handle);
            this.AddVideoSources(new List<VideoSource>(), 7, this.player_7.Handle);
            this.AddVideoSources(new List<VideoSource>(), 8, this.player_8.Handle);
            this.AddVideoSources(new List<VideoSource>(), 9, this.player_9.Handle);
            this.AddVideoSources(new List<VideoSource>(), 10, this.player_10.Handle);
            this.AddVideoSources(new List<VideoSource>(), 11, this.player_11.Handle);
            this.AddVideoSources(new List<VideoSource>(), 12, this.player_12.Handle);
            this.AddVideoSources(new List<VideoSource>(), 13, this.player_13.Handle);
            this.AddVideoSources(new List<VideoSource>(), 14, this.player_14.Handle);
            this.AddVideoSources(new List<VideoSource>(), 15, this.player_15.Handle);
            this.AddVideoSources(new List<VideoSource>(), 16, this.player_16.Handle);
            //FileEndCallback fFileEndCallback = new FileEndCallback(SetFileEndCallback);//预览实时流回调函数
            //bool isSuccess = PlayCtrlHelper.PlayM4_SetFileEndCallback(VideoPlay.VideoSources[0].PlayPort, fFileEndCallback, IntPtr.Zero);
            //Console.WriteLine("*************通道：" + VideoPlay.VideoSources[0].PlayPort + "，" + (isSuccess ? "播放器回调函数设置成功" : "播放器回调函数设置失败") + "*************");
        }
        private void AddVideoSources(List<VideoSource> videos, int _videoChannel, IntPtr _playIntPtr)
        {
            int tempPort = -1;
            PlayCtrlHelper.PlayM4_GetPort(ref tempPort);
            VideoPlay.VideoViewModels.Add(new VideoViewModel(tempPort, _videoChannel, _videoChannel, _playIntPtr, videos, 0, true, true));
        }
        /// <summary>
        /// 切换视频播放
        /// </summary>
        /// <param name="vvModel">播放资源</param>
        /// <param name="_playIndex">播放视频的位置指针</param>
        private void ChangeVideoPlay(VideoViewModel vvModel, int _playIndex, uint nTime = 0)
        {
            try
            {

                bool flag = false;
                Int32 nPort = vvModel.PlayPort;
                //Console.WriteLine("通道" + nPort + "：当前视频播放结束，开始播放下一个视频");
                vvModel.PlayIndex = _playIndex;
                //Console.WriteLine("通道" + nPort + "：正在播放第" + (vvModel.PlayIndex + 1) + "个视频");
                this.OpenFile_Video(vvModel);
                this.Play_Video(vvModel);
                if (nTime > 0)
                    flag = PlayCtrlHelper.PlayM4_SetPlayedTimeEx(vvModel.PlayPort, nTime);
                if (flag)
                    Console.WriteLine("通道" + vvModel.VideoChannel + "：正在播放第" + (vvModel.PlayIndex + 1) + "个视频；跳转成功");
                else
                {
                    uint errorCode = PlayCtrlHelper.PlayM4_GetLastError(vvModel.PlayPort);
                    Console.WriteLine("通道" + vvModel.VideoChannel + "：正在播放第" + (vvModel.PlayIndex + 1) + "个视频；跳转失败！错误码：" + errorCode);
                }

            }
            catch (Exception ex)
            {
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "切换视频播放时", ex);
            }
        }
        public void ChangeVideoPlayByTime(object sender, ChangeVideoEventArgs eventArgs)
        {
            try
            {
                DateTime? xValue = eventArgs.XValue;
                if (xValue == null) return;
                this.playTimer.Stop();
                DateTime dt = Convert.ToDateTime(xValue);
                bool isPlay = false;
                foreach (VideoViewModel vvModel in VideoPlay.VideoViewModels)
                {
                    this.CloseFile_Video(vvModel);
                    VideoSource vs = vvModel.Videos.Where(v => v.StartTime <= dt && dt < v.EndTime).FirstOrDefault();
                    if (vs == null)
                    {
                        vvModel.PlayIndex = -1;
                        continue;
                    }
                    DateTime startDT = vs.StartTime;
                    TimeSpan ts = dt.Subtract(startDT);
                    uint nTime = (uint)ts.TotalMilliseconds;
                    this.ChangeVideoPlay(vvModel, vvModel.Videos.IndexOf(vs), nTime);
                    isPlay = true;
                }
                if (isPlay)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        this.play_btn.Content = "暂停";
                    });
                    this.nPause = 1;
                }
                this.playTimer.Start();
            }
            catch (Exception ex)
            {
                this.playTimer.Start();
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "改变视频进度", ex);
            }
        }
        public async void SetPlayVideoSourceAsync(string _videoDateStr, List<VideoSource> Videos)
        {
            try
            {
                this.VideDateStr = _videoDateStr;
                if (this.playTimer != null)
                    this.playTimer.Stop();
                this.nPause = -1;
                await this.Dispatcher.InvokeAsync(() =>
                 {
                     this.play_btn.Content = "播放";
                 });
                await Task.Run(() =>
                {
                    foreach (var vs in VideoPlay.VideoViewModels)
                    {
                        vs.PlayIndex = 0;
                        vs.Videos.Clear();
                    }
                    Videos = Videos.OrderBy(vs => vs.StartTime).ToList();
                    foreach (VideoSource vs in Videos)
                    {
                        if (vs.VideoChannel <= 0) continue;
                        VideoPlay.VideoViewModels[vs.VideoChannel - 1].Videos.Add(vs);
                    }
                    foreach (var vvModel in VideoPlay.VideoViewModels)
                    {
                        this.CloseFile_Video(vvModel);
                        this.OpenFile_Video(vvModel);
                    }
                });
                this.DrawVideoLineAsync(Videos.Count);
                this.video_play_wait.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "加载视频", ex);
            }
        }

        private async void DrawVideoLineAsync(int videoCount)
        {
            try
            {
                await Task.Run(() =>
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        this.ClearVideoChartEvent(null, null);
                    });
                    if (VideoPlay.VideoViewModels == null || VideoPlay.VideoViewModels.Count == 0) return;
                    int tPort = -1;
                    PlayCtrlHelper.PlayM4_GetPort(ref tPort);
                    int yValue = 202;
                    int i = 0;//已画视频个数
                    foreach (var vvm in VideoPlay.VideoViewModels)
                    {
                        yValue = yValue - 12;
                        if (vvm.PlayIndex < 0)
                            continue;
                        foreach (var vs in vvm.Videos)
                        {
                            i++;
                            PlayCtrlHelper.PlayM4_OpenFile(tPort, vs.FullPathName);
                            vs.DurationSecond = PlayCtrlHelper.PlayM4_GetFileTime(tPort);
                            vs.EndTime = vs.StartTime.AddSeconds(vs.DurationSecond);
                            vs.TotalFrames = PlayCtrlHelper.PlayM4_GetFileTotalFrames(tPort);
                            PlayCtrlHelper.PlayM4_CloseFile(tPort);
                            this.Dispatcher.Invoke(() =>
                            {
                                this.DrawVideoLineEvent(vs, new DrawVideoLineEventArgs(
                                new DataPoint
                                {
                                    XValue = vs.StartTime,
                                    //设置Y轴点                   
                                    YValue = yValue,
                                }, new DataPoint
                                {
                                    XValue = vs.EndTime,
                                    //设置Y轴点                   
                                    YValue = yValue,
                                }, i == videoCount));
                                //Console.WriteLine("通道" + vs.VideoChannel + ":" + "总时长-" + vs.DurationSecond + "秒(开始时间-" + vs.StartTime.ToString("yyyy-MM-dd HH:mm:ss") + "结束时间-" + (vs.EndTime == null ? "" : ((DateTime)vs.EndTime).ToString("yyyy-MM-dd HH:mm:ss")) + ")");
                            });
                        }
                    }
                    PlayCtrlHelper.PlayM4_FreePort(tPort);
                });
            }
            catch (Exception ex)
            {
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "绘制视频曲线", ex);
            }
        }

        #endregion
        #region 海康控件 视频控制
        private void OpenFile_Video(VideoViewModel vvModel)
        {
            if (vvModel.PlayIndex < 0) return;
            // 创建文件索引
            bool flag = PlayCtrlHelper.PlayM4_SetFileRefCallBack(vvModel.PlayPort, pFileRefDone, IntPtr.Zero);
            if (!flag)
            {
                //Console.WriteLine("通道" + vvModel.PlayPort + "设置创建索引事件成功！");
                uint errorCode = PlayCtrlHelper.PlayM4_GetLastError(vvModel.PlayPort);
                Console.WriteLine("通道" + vvModel.VideoChannel + "设置创建索引事件失败！失败码：" + errorCode);
            }
            vvModel.Videos[vvModel.PlayIndex].IsRefDone = false;
            PlayCtrlHelper.PlayM4_OpenFile(vvModel.PlayPort, vvModel.Videos[vvModel.PlayIndex].FullPathName);
            if (vvModel.IsNeedGroup)
            {
                PlayCtrlHelper.PlayM4_SetSycGroup(vvModel.PlayPort, 0);
            }
            //PlayCtrlHelper.PlayM4_SetDisplayCallBack(vvModel.PlayPort, fDisplayCBFun);
        }
        private static FileRefDoneCB pFileRefDone = new FileRefDoneCB(SetFileRefDoneCB);
        private static void SetFileRefDoneCB(uint nPort, IntPtr nUser)
        {
            var vvModel = VideoPlay.VideoViewModels.Where(vs => vs.PlayPort == nPort).FirstOrDefault();
            if (vvModel == null || vvModel.PlayIndex < 0) return;
            vvModel.Videos[vvModel.PlayIndex].IsRefDone = true;
        }
        private static DisplayCBFun fDisplayCBFun = new DisplayCBFun(SetDisplayCBFun);
        //public static string sFilePath = @"F:\";
        private static void SetDisplayCBFun(uint nPort, byte[] pBuf, Int32 nSize, Int32 nWidth, Int32 nHeight, Int32 nStamp, Int32 nType, Int32 nReceved)
        {
            var vvModel = VideoPlay.VideoViewModels.Where(vs => vs.PlayPort == nPort).FirstOrDefault();
            if (vvModel == null || vvModel.PlayIndex < 0) return;
            string sFilePath = @"F:\test\";
            sFilePath += DateTime.Now.Ticks.ToString() + ".jpeg";
            //连续抓BMP图片
            bool flag = false;// PlayCtrlHelper.PlayM4_ConvertToJpegFile(pBuf, nSize, nWidth, nHeight, nType, sFilePath);
            if (!flag)
            {
                //uint errorCode = PlayCtrlHelper.PlayM4_GetLastError(vvModel.PlayPort);
                //Console.WriteLine("通道" + vvModel.PlayPort + "：" + vvModel.Videos[vvModel.PlayIndex].FullPathName + "截图回调失败！错误号：" + errorCode);
            }
            else
            {
                Console.WriteLine("通道" + vvModel.PlayPort + "：" + vvModel.Videos[vvModel.PlayIndex].FullPathName + "截图回调成功！");
            }
        }
        public void CloseFile_Video(VideoViewModel vvm)
        {
            try
            {
                Int32 nPort = vvm.PlayPort;
                PlayCtrlHelper.PlayM4_Stop(nPort);
                PlayCtrlHelper.PlayM4_CloseFile(nPort);
                this.PlayWinRefresh(vvm.VideoChannel);
            }
            catch { }
        }
        private void PlayWinRefresh(int playPos)
        {
            try
            {
                this.Dispatcher.Invoke(() =>
                {
                    switch (playPos)
                    {
                        case 1:
                            this.player_1.Refresh();
                            break;
                        case 2:
                            this.player_2.Refresh();
                            break;
                        case 3:
                            this.player_3.Refresh();
                            break;
                        case 4:
                            this.player_4.Refresh();
                            break;
                        case 5:
                            this.player_5.Refresh();
                            break;
                        case 6:
                            this.player_6.Refresh();
                            break;
                        case 7:
                            this.player_7.Refresh();
                            break;
                        case 8:
                            this.player_8.Refresh();
                            break;
                        case 9:
                            this.player_9.Refresh();
                            break;
                        case 10:
                            this.player_10.Refresh();
                            break;
                        case 11:
                            this.player_11.Refresh();
                            break;
                        case 12:
                            this.player_12.Refresh();
                            break;
                        case 13:
                            this.player_13.Refresh();
                            break;
                        case 14:
                            this.player_14.Refresh();
                            break;
                        case 15:
                            this.player_15.Refresh();
                            break;
                        case 16:
                            this.player_16.Refresh();
                            break;
                        default:
                            break;
                    }
                });
            }
            catch { }
        }
        public void PlayDispose()
        {
            try
            {
                this.StopPlay_Btn_Click(null, null);
                if (this.playTimer != null)
                    this.playTimer.Dispose();
                if (VideoPlay.VideoViewModels == null || VideoPlay.VideoViewModels.Count == 0) return;
                foreach (var vs in VideoPlay.VideoViewModels)
                {
                    PlayCtrlHelper.PlayM4_CloseFile(vs.PlayPort);
                }
            }
            catch { }
        }

        private void Play()
        {
            foreach (var vvModel in VideoPlay.VideoViewModels)
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
            foreach (var vs in VideoPlay.VideoViewModels)
            {
                if (vs.Videos.Count <= 0) continue;
                PlayCtrlHelper.PlayM4_Pause(vs.PlayPort, nPause);
            }
        }
        private void Play_Video(VideoViewModel vvModel)
        {
            if (vvModel.PlayIndex < 0) return;
            this.bFlag = PlayCtrlHelper.PlayM4_Play(vvModel.PlayPort, vvModel.PlayIntPtr);
            if (!vvModel.IsNeedRef) return;
            int durTime = 0;
            while (true || durTime < 3000)
            {
                if (!vvModel.Videos[vvModel.PlayIndex].IsRefDone)
                {
                    System.Threading.Thread.Sleep(2);
                    durTime++;
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
        private int showWinCount = 0;
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
            this.HidePlayBorderContainer(16);
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
            this.showWinCount = startIndex;
            for (int i = startIndex; i < 16; i++)
            {
                this.PlayBorderContainers[i].Visibility = Visibility.Collapsed;
            }
        }
        #endregion
        #region 页面视频控制按钮事件
        /// <summary>
        /// 暂停播放开关量
        /// </summary>
        private int nPause = -1;

        private void StartPlay_Btn_Click(object sender, RoutedEventArgs e)
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
        private void StopPlay_Btn_Click(object sender, RoutedEventArgs e)
        {

            if (this.playTimer != null)
                this.playTimer.Stop();
            foreach (var vvm in VideoPlay.VideoViewModels)
            {
                if (vvm.PlayIndex < 0) continue;
                PlayCtrlHelper.PlayM4_Stop(vvm.PlayPort);
                this.PlayWinRefresh(vvm.VideoChannel);
            }
            this.nPause = -1;
            this.play_btn.Content = "播放";
        }
        private void GetJPEG_Btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (this.nPause == -1)
                {
                    MessageForm.Show("提示", "请先播放视频！", 0);
                    return;
                }
                if (string.IsNullOrWhiteSpace(this.play_time_tb.Text)) return;
                DateTime? refTime = Convert.ToDateTime(this.play_time_tb.Text);
                if (refTime == null)
                {
                    MessageForm.Show("提示", "请先等待视频正常播放后，在截屏！", 0);
                    return;
                }
                AnalysisReport aReport = new AnalysisReport();
                string videoTimeStr = this.play_time_tb.Text;
                Task.Run(() =>
                {
                    AnalysisReportViewModel aReportViewModel = new AnalysisReportViewModel();
                    aReportViewModel.ImageStreamTotalLength = 0;
                    int i = 0;
                    bool isGetNeedInfo = false;
                    List<CapVideoImage> capImages = new List<CapVideoImage>();
                    CapVideoImage capImage = null;
                    foreach (var vvm in VideoPlay.VideoViewModels)
                    {
                        if (i >= this.showWinCount) break;
                        if (vvm.PlayIndex < 0) continue;
                        if (!isGetNeedInfo)
                        {
                            aReportViewModel.ReportDateStr = this.VideDateStr;
                            aReportViewModel.TrainType = vvm.Videos[vvm.PlayIndex].TrainType;
                            aReportViewModel.TrainNo = vvm.Videos[vvm.PlayIndex].TrainNo;
                            aReportViewModel.TrainShortName = vvm.Videos[vvm.PlayIndex].TrainShortName;
                            aReportViewModel.EventTimeStr = videoTimeStr;
                            aReportViewModel.MDataViewModel = MonitorDataHelper.GetMonitorData((DateTime)refTime);
                            isGetNeedInfo = true;
                        }
                        capImage = new CapVideoImage();
                        capImage.VideoChannel = vvm.Videos[vvm.PlayIndex].VideoChannel;
                        capImage.VideoChannelName = vvm.Videos[vvm.PlayIndex].VideoChannelName;
                        UInt32 pWidth = 0;
                        UInt32 pHeight = 0;
                        PlayCtrlHelper.PlayM4_GetPictureSize(vvm.PlayPort, ref pWidth, ref pHeight);
                        UInt32 dwSize = pWidth * pHeight * 5;
                        byte[] m_pCapBuf = new byte[pWidth * pHeight];
                        //抓图jpeg图片
                        UInt32 dwCapSize = 0;
                        bool flag = PlayCtrlHelper.PlayM4_GetJPEG(vvm.PlayPort, m_pCapBuf, dwSize, ref dwCapSize);
                        if (!flag)
                        {
                            uint errorCode = PlayCtrlHelper.PlayM4_GetLastError(vvm.PlayPort);
                            Console.WriteLine("通道" + vvm.PlayPort + "：" + vvm.Videos[vvm.PlayIndex].FullPathName + "截图失败！错误号：" + errorCode);
                        }
                        capImage.CapImageBuf = m_pCapBuf;
                        aReportViewModel.ImageStreamTotalLength += m_pCapBuf.Length;
                        capImages.Add(capImage);
                        i++;
                    }
                    aReportViewModel.CapImages = capImages;
                    aReport.InitPageAsync(aReportViewModel);
                });
                aReport.ShowDialog();
            }
            catch (Exception ex)
            {
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "截图失败", ex);
            }
        }
        #endregion
    }
}
