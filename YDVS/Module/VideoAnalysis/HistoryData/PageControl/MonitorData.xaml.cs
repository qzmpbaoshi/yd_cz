using CommonLibrary.Extension;
using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using VideoAnalysis.HistoryData.Common;
using VideoAnalysis.HistoryData.ViewModel;

namespace VideoAnalysis.HistoryData.PageControl
{
    /// <summary>
    /// VideoData.xaml 的交互逻辑
    /// </summary>
    public partial class MonitorData : UserControl
    {
        /// <summary>
        /// 是否正在解析数据
        /// </summary>
        private bool isParsing = false;
        private MonitorDataViewModel ViewModel { get; set; }
        public MonitorData()
        {
            InitializeComponent();
            this.ViewModel = new MonitorDataViewModel();
            this.DataContext = this.ViewModel;
        }
        /// <summary>
        /// 根据时间刷新页面数据
        /// </summary>
        /// <param name="refTime">刷新时间</param>
        public void RefreshDataByTime(DateTime refTime)
        {
            try
            {
                if (isParsing || refTime == null) return;
                this.isParsing = true;
                Task<MonitorDataViewModel> task = Task<MonitorDataViewModel>.Run(() =>
                {
                    return MonitorDataHelper.GetMonitorData(refTime);
                });
                task.GetAwaiter().OnCompleted(() =>
                {
                    this.RefreshDataByViewModel(task.Result);
                    this.isParsing = false;
                });
            }
            catch (Exception ex)
            {
                this.isParsing = false;
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "根据时间刷新LKJ和TCMS数据", ex);
            }
        }
        /// <summary>
        /// 根据页面模型数据刷新页面
        /// </summary>
        /// <param name="_viewModel">待刷新的模型</param>
        public void RefreshDataByViewModel(MonitorDataViewModel _viewModel)
        {
            try
            {
                this.Dispatcher.Invoke(() =>
                {
                    this.ViewModel.ObjectCopyProperty(_viewModel);
                });
            }
            catch { }
        }
       
    }
}
