
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoAnalysis.HistoryData.ViewModel
{
    public class MonitorDataViewModel
    {
        #region LKJ数据
        private string _trainNum;
        private string _weight;
        private string _speed;
        private string _trainLong;
        private string _trainUse;
        private string _trainType;
        private string _stationNo;
        private string _vehicleCount;
        private string _driverNum;
        private string _driverName;
        private string _assDriverNum;
        private string _assDriverName;
        private string _kilometreSign;
        private string _trainSignal;
        private string _annunciatorNum;
        private string _workCondition;
        private string _annunciatorKind;
        private string _deviceStatus;
        private string _routesNo;
        private string _pipePressure;
        /// <summary>
        /// 车次
        /// </summary>
        public string TrainNum
        {
            get
            {
                return _trainNum;
            }

            set
            {
                _trainNum = value;
            }
        }
        /// <summary>
        /// 总重
        /// </summary>
        public string Weight
        {
            get
            {
                return _weight;
            }

            set
            {
                _weight = value;
            }
        }
        /// <summary>
        /// 速度
        /// </summary>
        public string Speed
        {
            get
            {
                return _speed;
            }

            set
            {
                _speed = value;
            }
        }
        /// <summary>
        /// 计长
        /// </summary>
        public string TrainLong
        {
            get
            {
                return _trainLong;
            }

            set
            {
                _trainLong = value;
            }
        }
        /// <summary>
        /// 客/货
        /// </summary>
        public string TrainUse
        {
            get
            {
                return _trainUse;
            }

            set
            {
                _trainUse = value;
            }
        }
        /// <summary>
        /// 本/补
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
        /// 车站号
        /// </summary>
        public string StationNo
        {
            get
            {
                return _stationNo;
            }

            set
            {
                _stationNo = value;
            }
        }
        /// <summary>
        /// 车辆数
        /// </summary>
        public string VehicleCount
        {
            get
            {
                return _vehicleCount;
            }

            set
            {
                _vehicleCount = value;
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
        /// 司机名
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
        /// 副司机名
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
        /// 公里标
        /// </summary>
        public string KilometreSign
        {
            get
            {
                return _kilometreSign;
            }

            set
            {
                _kilometreSign = value;
            }
        }
        /// <summary>
        /// 机车信号
        /// </summary>
        public string TrainSignal
        {
            get
            {
                return _trainSignal;
            }

            set
            {
                _trainSignal = value;
            }
        }
        /// <summary>
        /// 信号机编号
        /// </summary>
        public string AnnunciatorNum
        {
            get
            {
                return _annunciatorNum;
            }

            set
            {
                _annunciatorNum = value;
            }
        }
        /// <summary>
        /// 机车工况
        /// </summary>
        public string WorkCondition
        {
            get
            {
                return _workCondition;
            }

            set
            {
                _workCondition = value;
            }
        }
        /// <summary>
        /// 信号机种类
        /// </summary>
        public string AnnunciatorKind
        {
            get
            {
                return _annunciatorKind;
            }

            set
            {
                _annunciatorKind = value;
            }
        }
        /// <summary>
        /// 装置状态
        /// </summary>
        public string DeviceStatus
        {
            get
            {
                return _deviceStatus;
            }

            set
            {
                _deviceStatus = value;
            }
        }
        /// <summary>
        /// 实际交路号
        /// </summary>
        public string RoutesNo
        {
            get
            {
                return _routesNo;
            }

            set
            {
                _routesNo = value;
            }
        }
        /// <summary>
        /// 列车管压力
        /// </summary>
        public string PipePressure
        {
            get
            {
                return _pipePressure;
            }

            set
            {
                _pipePressure = value;
            }
        }
        #endregion
        #region TCMS数据
        private string _cabStatus;
        private string _breakerStatus;
        private string _pantographStatus;
        private string _pantographPos;
        private string _reconnectionInfo;
        private string _bigBrakeCommand;
        private string _littleBrakeCommand;
        private string _otherCommand;
        /// <summary>
        /// 司机室状态
        /// </summary>
        public string CabStatus
        {
            get
            {
                return _cabStatus;
            }

            set
            {
                _cabStatus = value;
            }
        }
        /// <summary>
        /// 主断路器状态
        /// </summary>
        public string BreakerStatus
        {
            get
            {
                return _breakerStatus;
            }

            set
            {
                _breakerStatus = value;
            }
        }
        /// <summary>
        /// 受电弓状态
        /// </summary>
        public string PantographStatus
        {
            get
            {
                return _pantographStatus;
            }

            set
            {
                _pantographStatus = value;
            }
        }
        /// <summary>
        /// 手柄级位
        /// </summary>
        public string PantographPos
        {
            get
            {
                return _pantographPos;
            }

            set
            {
                _pantographPos = value;
            }
        }
        /// <summary>
        /// 重联信息
        /// </summary>
        public string ReconnectionInfo
        {
            get
            {
                return _reconnectionInfo;
            }

            set
            {
                _reconnectionInfo = value;
            }
        }
        /// <summary>
        /// 大闸指令
        /// </summary>
        public string BigBrakeCommand
        {
            get
            {
                return _bigBrakeCommand;
            }

            set
            {
                _bigBrakeCommand = value;
            }
        }
        /// <summary>
        /// 小闸指令
        /// </summary>
        public string LittleBrakeCommand
        {
            get
            {
                return _littleBrakeCommand;
            }

            set
            {
                _littleBrakeCommand = value;
            }
        }
        /// <summary>
        /// 其他指令
        /// </summary>
        public string OtherCommand
        {
            get
            {
                return _otherCommand;
            }

            set
            {
                _otherCommand = value;
            }
        }
        #endregion
    }
}
