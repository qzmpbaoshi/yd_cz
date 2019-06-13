using System;

namespace SystemModel.SearchConditionModel.Search
{
    public class AnalysisReportInfoSearch
    {
        /// <summary>
        /// 机车简称
        /// </summary>
        public string TrainShortName { get; set; }
        /// <summary>
        /// 车次
        /// </summary>
        public string TrainNum { get; set; }
        /// <summary>
        /// 司机名称
        /// </summary>
        public string DriverName { get; set; }
        /// <summary>
        /// 分析人
        /// </summary>
        public string AnalysisPerson { get; set; }
        /// <summary>
        /// 分析时间，查找开始时间
        /// </summary>
        public DateTime AnalysisTime_Start { get; set; }
        /// <summary>
        /// 分析时间，查找结束时间
        /// </summary>
        public DateTime AnalysisTime_End { get; set; }
    }
}
