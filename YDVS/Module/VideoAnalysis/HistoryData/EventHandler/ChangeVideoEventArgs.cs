using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoAnalysis.HistoryData.EventHandler
{
    public class ChangeVideoEventArgs : EventArgs
    {
        public DateTime? XValue { get; set; }
        public ChangeVideoEventArgs(DateTime? _xValue)
        {
            this.XValue = _xValue;
        }
    }
}
