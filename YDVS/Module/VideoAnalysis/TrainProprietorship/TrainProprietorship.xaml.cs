using System;
using System.Windows.Controls;
using SystemModel.DataModel;
using SystemModel.SearchConditionModel;
using CommonLibrary.Extension;
using SystemModel.RequestResult;
using Newtonsoft.Json;
using SystemModel;
using System.Windows;
using System.Threading.Tasks;
using VideoAnalysis.TrainProprietorship.PageControl;

namespace VideoAnalysis.TrainProprietorship
{
    /// <summary>
    /// TrainProprietorshipControl.xaml 的交互逻辑
    /// </summary>
    public partial class TrainProprietorship : UserControl
    {
        public TrainProprietorship()
        {
            InitializeComponent();
            this.self_pagination.PageButtonClick += Self_pagination_PageButtonClick;
            this.SetPageData(1);
        }

        private void Self_pagination_PageButtonClick(object sender, EventArgs e)
        {
            try
            {
                this.SetPageData(this.self_pagination.GetPageIndex());
            }
            catch (Exception ex)
            {
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "机车配属段功能，查询配属段信息", ex);
            }
        }

        private void SetPageData(int pIndex)
        {
            try
            {
                this.data_panel_sp.Children.Clear();
                int pageSize = 23;
                this.self_wait.Visibility = Visibility.Visible;
                string requestUrl = CommonLibrary.ReadConfigHelper.BaseUrl + WebApiExtensionUrl.GetTrainProprietorships_URL;
                PagingSearchCondition<TrainProprietorshipSearch> condition = new PagingSearchCondition<TrainProprietorshipSearch>();
                condition.StartIndex = pIndex;
                condition.PageSize = pageSize;
                condition.SearchCondition = new TrainProprietorshipSearch
                {
                    TrainShortName = this.condition_tb.Text
                };
                Task<RequestPagingResult<TrainProprietorshipModel>> task = Task<RequestPagingResult<TrainProprietorshipModel>>.Run(() =>
                {
                    return CommonLibrary.Factory.HttpRequestFactroy.HttpPostRequest<RequestPagingResult<TrainProprietorshipModel>>(requestUrl, condition);
                });
                task.GetAwaiter().OnCompleted(() =>
                {
                    RequestPagingResult<TrainProprietorshipModel> rst = task.Result;
                    this.Dispatcher.Invoke(() =>
                    {
                        TableItem itemControl = null;
                        int order = 1;
                        foreach (var item in rst.ResultDatas)
                        {
                            itemControl = new TableItem();
                            itemControl.ViewModel.ObjectCopyProperty(item);
                            itemControl.ViewModel.Order = order++;
                            itemControl.MouseLeftButtonDown += TableItemControl_MouseLeftButtonDown;
                            this.data_panel_sp.Children.Add(itemControl);
                        }
                        this.self_pagination.SetPageContent(pIndex, pageSize, rst.TotalCount);
                        this.self_wait.Visibility = Visibility.Collapsed;
                    });
                });
            }
            catch (Exception ex)
            {
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "机车配属段功能，查询配属段信息", ex);
            }
        }
        private TableItem SelectedTableItem;
        private void TableItemControl_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                TableItem tItem = sender as TableItem;
                if (tItem == null) return;
                tItem.Selected = !tItem.Selected;
                this.SelectedTableItem = tItem.Selected ? tItem : null;
                foreach (TableItem item in this.data_panel_sp.Children)
                {
                    if (item != this.SelectedTableItem)
                        item.Selected = false;
                }
            }
            catch { }
        }

        private void Search_Btn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.SetPageData(1);
        }
    }
}
