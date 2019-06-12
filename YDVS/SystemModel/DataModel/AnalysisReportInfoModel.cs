using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemModel.DataModel
{
    public class AnalysisReportInfoModel
    {
        private string _id;
        private string _trainType;
        private string _trainNo;
        private string _trainShortName;
        private string _driverName;
        private string _driverNum;
        private string _assDriverName;
        private string _assDriverNum;
        private DateTime? _eventTime;
        private DateTime? _analysisTime;
        private string _analysisPerson;
        private string _analysisContent;
        private string _eventDataJsonStr;
        /// <summary>
        /// 主键
        /// </summary>
        public string Id
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
            }
        }
        /// <summary>
        /// 车型
        /// </summary>
        public string TrainType
        {
            get
            {
                return _trainType;
            }

            set
            {
                _trainType = value;
            }
        }
        /// <summary>
        /// 车号
        /// </summary>
        public string TrainNo
        {
            get
            {
                return _trainNo;
            }

            set
            {
                _trainNo = value;
            }
        }
        /// <summary>
        /// 机车简称
        /// </summary>
        public string TrainShortName
        {
            get
            {
                return _trainShortName;
            }

            set
            {
                _trainShortName = value;
            }
        }
        /// <summary>
        /// 司机名称
        /// </summary>
        public string DriverName
        {
            get
            {
                return _driverName;
            }

            set
            {
                _driverName = value;
            }
        }
        /// <summary>
        /// 司机号
        /// </summary>
        public string DriverNum
        {
            get
            {
                return _driverNum;
            }

            set
            {
                _driverNum = value;
            }
        }
        /// <summary>
        /// 副司机名称
        /// </summary>
        public string AssDriverName
        {
            get
            {
                return _assDriverName;
            }

            set
            {
                _assDriverName = value;
            }
        }
        /// <summary>
        /// 副司机号
        /// </summary>
        public string AssDriverNum
        {
            get
            {
                return _assDriverNum;
            }

            set
            {
                _assDriverNum = value;
            }
        }
        /// <summary>
        /// 事件发生时间
        /// </summary>
        public DateTime? EventTime
        {
            get
            {
                return _eventTime;
            }

            set
            {
                _eventTime = value;
            }
        }
        /// <summary>
        /// 分析报告生成时间
        /// </summary>
        public DateTime? AnalysisTime
        {
            get
            {
                return _analysisTime;
            }

            set
            {
                _analysisTime = value;
            }
        }
        /// <summary>
        /// 分析人
        /// </summary>
        public string AnalysisPerson
        {
            get
            {
                return _analysisPerson;
            }

            set
            {
                _analysisPerson = value;
            }
        }
        /// <summary>
        /// 分析内容
        /// </summary>
        public string AnalysisContent
        {
            get
            {
                return _analysisContent;
            }

            set
            {
                _analysisContent = value;
            }
        }
        /// <summary>
        /// 事件发生时的LKJ和TCMS监控数据字符串
        /// </summary>
        public string EventDataJsonStr
        {
            get
            {
                return _eventDataJsonStr;
            }

            set
            {
                _eventDataJsonStr = value;
            }
        }
    }
}
