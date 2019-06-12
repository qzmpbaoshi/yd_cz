using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoAnalysis.HistoryData.ViewModel;

namespace VideoAnalysis.HistoryData.Common
{
    public static class MonitorDataHelper
    {
        /// <summary>
        /// 根据时间获取LKJ和TCMS监测数据
        /// </summary>
        /// <param name="refTime"></param>
        /// <returns></returns>
        public static MonitorDataViewModel GetMonitorData(DateTime refTime)
        {
            MonitorDataViewModel tempModel = new MonitorDataViewModel();
            //TODO CPU16进制数据解析,并为tempModel赋值Start



            //End
            return tempModel;
        }
    }
}
