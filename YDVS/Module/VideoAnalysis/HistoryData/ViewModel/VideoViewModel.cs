using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoAnalysis.HistoryData.ViewModel
{
    public class VideoViewModel
    {
        public VideoViewModel(Int32 _playPort, int _videoChannel, IntPtr _playIntPtr, List<VideoSource> _videos, int _playIndex, bool _isNeedRef, bool _isNeedGroup)
        {
            this.PlayPort = _playPort;
            this.VideoChannel = _videoChannel;
            this.PlayIntPtr = _playIntPtr;
            this.Videos = _videos;
            this.PlayIndex = _playIndex;
            this.IsNeedRef = _isNeedRef;
            this.IsNeedGroup = _isNeedGroup;
            this.WinPosition = _videoChannel;
        }
        /// <summary>
        /// 播放端口号
        /// </summary>
        public Int32 PlayPort = -1;
        /// <summary>
        /// 播放时长 秒
        /// </summary>
        private int _playIndex;
        /// <summary>
        /// 播放的同位置视频的位置
        /// </summary>
        public int PlayIndex
        {
            get
            {
                return this.Videos == null || this._playIndex >= this.Videos.Count ? -1 : this._playIndex;
            }
            set
            {
                this._playIndex = value;
            }
        }
        /// <summary>
        /// 获取播放容器句柄
        /// </summary>
        public IntPtr PlayIntPtr { get; set; }
        /// <summary>
        /// 视频通道
        /// </summary>
        public int VideoChannel { get; set; }
        /// <summary>
        /// 窗口位置
        /// </summary>
        public int WinPosition { get; set; }
        /// <summary>
        /// 是否需要建立索引
        /// </summary>
        public bool IsNeedRef = false;
        /// <summary>
        /// 是否需要加入组
        /// </summary>
        public bool IsNeedGroup = false;
        /// <summary>
        /// 相同位置视频List
        /// </summary>
        public List<VideoSource> Videos { get; set; }

    }
    public class VideoSource
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 视频时长，单位 秒 无符号
        /// </summary>
        public uint DurationSecond { get; set; }
        /// <summary>
        /// 视频总帧数
        /// </summary>
        public uint TotalFrames { get; set; }
        /// <summary>
        /// 全路径
        /// </summary>
        public string FullPathName { get; set; }
        /// <summary>
        /// 视频名字
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 顺序
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// 机车简称
        /// </summary>
        public string TrainShortName { get; set; }
        /// <summary>
        /// 车型
        /// </summary>
        public string TrainType { get; set; }
        /// <summary>
        /// 车号
        /// </summary>
        public string TrainNo { get; set; }
        /// <summary>
        /// 视频来源
        /// </summary>
        public string VideoFromSource { get; set; }
        /// <summary>
        /// 视频通道
        /// </summary>
        public int VideoChannel { get; set; }
        /// <summary>
        /// 视频通道所属名称
        /// </summary>
        public string VideoChannelName { get; set; }

        public bool IsRefDone = false;

    }
}
