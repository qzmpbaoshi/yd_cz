using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VideoAnalysis.Annotations;

namespace VideoAnalysis.DriverInfo.ViewModel
{
    public class DriverInfoViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string _id;
        private int? _order;
        private string _card;
        private string _name;
        private string _locomotiveDepot;
        private string _team;


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
        /// 司机号
        /// </summary>
        public string Card
        {
            get
            {
                return _card;
            }

            set
            {
                _card = value;
            }
        }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
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
        /// 班组
        /// </summary>
        public string Team
        {
            get
            {
                return _team;
            }

            set
            {
                _team = value;
            }
        }

    }
}
