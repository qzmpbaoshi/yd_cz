using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoAnalysis.HistoryData.ViewModel
{
    public class AnalysisReportViewModel
    {
        /// <summary>
        /// 车型
        /// </summary>
        public string TrainType { get; set; }
        /// <summary>
        /// 车号
        /// </summary>
        public string TrainNo { get; set; }
        /// <summary>
        /// 机车简称
        /// </summary>
        public string TrainShortName { get; set; }
        /// <summary>
        /// 截图时视频播放时间字符串
        /// </summary>
        public string EventTimeStr { get; set; }
        /// <summary>
        /// 视频截图字节流数组
        /// </summary>
        public List<CapVideoImage> CapImages { get; set; }
        /// <summary>
        /// 截屏时的LKJ和TCMS数据
        /// </summary>
        public MonitorDataViewModel MDataViewModel { get; set; }
        /// <summary>
        /// 分析时间，系统当前填写
        /// </summary>
        public string AnalysisTimeStr { get; set; }
        /// <summary>
        /// 分析人名称
        /// </summary>
        public string AnalysisPerson { get; set; }
        /// <summary>
        /// 分析内容
        /// </summary>
        public string AnalysisContent { get; set; }
    }
    public class CapVideoImage {
        /// <summary>
        /// 视频通道
        /// </summary>
        public int VideoChannel { get; set; }
        /// <summary>
        /// 视频通道所属名称
        /// </summary>
        public string VideoChannelName { get; set; }
        /// <summary>
        /// 视频截图字节流数组
        /// </summary>
        public byte[] CapImageBuf { get; set; }
    }
}
