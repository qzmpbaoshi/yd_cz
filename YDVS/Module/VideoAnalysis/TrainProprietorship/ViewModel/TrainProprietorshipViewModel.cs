using System.ComponentModel;

namespace VideoAnalysis.TrainProprietorship.ViewModel
{
    public class TrainProprietorshipViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string _id;
        private int? _order;
        private string _trainType;
        private string _trainNo;
        private string _locomotiveDepot;
        private string _workShop;
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
        /// 顺序号
        /// </summary>
        public int? Order
        {
            get
            {
                return _order;
            }

            set
            {
                _order = value;
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
        public string TrainShortName
        {
            get
            {
                return this.TrainType + "-" + this.TrainNo;
            }
        }
        /// <summary>
        /// 机务段
        /// </summary>
        public string LocomotiveDepot
        {
            get
            {
                return _locomotiveDepot;
            }

            set
            {
                _locomotiveDepot = value;
            }
        }
        /// <summary>
        /// 车间
        /// </summary>
        public string WorkShop
        {
            get
            {
                return _workShop;
            }

            set
            {
                _workShop = value;
            }
        }
    }
}
