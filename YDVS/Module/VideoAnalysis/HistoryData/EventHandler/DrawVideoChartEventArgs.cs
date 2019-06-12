using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visifire.Charts;

namespace VideoAnalysis.HistoryData.EventHandler
{
    public class DrawVideoLineEventArgs : EventArgs
    {
        public DataPoint StartPoint { get; set; }
        public DataPoint EndPoint { get; set; }
        public bool IsDrawFinish { get; set; }
        public DrawVideoLineEventArgs(DataPoint _startPoint, DataPoint _endPoint, bool _isDrawFinish)
        {
            this.StartPoint = _startPoint;
            this.EndPoint = _endPoint;
            this.IsDrawFinish = _isDrawFinish;
        }
    }
}
