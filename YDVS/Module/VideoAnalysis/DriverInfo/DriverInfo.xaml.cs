using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SystemModel;
using SystemModel.DataModel;
using SystemModel.RequestResult;
using SystemModel.SearchConditionModel;
using CommonLibrary.Extension;
using VideoAnalysis.DriverInfo.PageControl;
using VideoAnalysis.DriverInfo.ViewModel;

namespace VideoAnalysis.DriverInfo
{
    /// <summary>
    /// DriverInfo.xaml 的交互逻辑
    /// </summary>
    public partial class DriverInfo : UserControl
    {
        public DriverInfo()
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
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "司机信息管理功能，查询司机信息", ex);
            }
        }
        private void SetPageData(int pIndex)
        {
            try
            {
                this.data_panel_sp.Children.Clear();
                int pageSize = 23;
                this.self_wait.Visibility = Visibility.Visible;
                string requestUrl = CommonLibrary.ReadConfigHelper.BaseUrl + WebApiExtensionUrl.GetDriverInfos_URL;
                PagingSearchCondition<DriverInfoModel> condition = new PagingSearchCondition<DriverInfoModel>();
                condition.StartIndex = pIndex;
                condition.PageSize = pageSize;
                condition.SearchCondition = new DriverInfoModel
                {
                    Card = this.condition_tb.Text
                };
                Task<RequestPagingResult<DriverInfoModel>> task = Task<RequestPagingResult<DriverInfoModel>>.Run(() =>
                {
                    return CommonLibrary.Factory.HttpRequestFactroy.HttpPostRequest<RequestPagingResult<DriverInfoModel>>(requestUrl, condition);
                });
                task.GetAwaiter().OnCompleted(() =>
                {
                    RequestPagingResult<DriverInfoModel> rst = task.Result;
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
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "司机信息管理功能，查询司机信息", ex);
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

        private void Search_Btn_Click(object sender, RoutedEventArgs e)
        {
            this.SetPageData(1);
        }

        private void Edit_Btn_Click(object sender, RoutedEventArgs e)
        {
            TableItem selectedTableItem = this.SelectedTableItem;
            if (null == selectedTableItem)
            {
                MessageBox.Show("请选择需要修改的数据行！");
                return;
            }
            AddDialog addDialog = new AddDialog();
            addDialog.Title = "编辑司机信息";
            addDialog.Card_tb.Text = selectedTableItem.ViewModel.Card;
            addDialog.Name_tb.Text = selectedTableItem.ViewModel.Name;
            addDialog.LocomotiveDepot_tb.Text = selectedTableItem.ViewModel.LocomotiveDepot;
            addDialog.Team_tb.Text = selectedTableItem.ViewModel.Team;
            if ((bool)addDialog.ShowDialog())
            {
                DriverInfoViewModel driverInfoViewModel = selectedTableItem.ViewModel;
                DriverInfoModel driverInfoModel = new DriverInfoModel();
                driverInfoModel.Id = driverInfoViewModel.Id;
                Console.WriteLine(driverInfoViewModel.Id + "trainProprietorshipViewModelId");
                driverInfoModel.Card = addDialog.Card_tb.Text;
                driverInfoModel.Name = addDialog.Name_tb.Text;
                driverInfoModel.LocomotiveDepot = addDialog.LocomotiveDepot_tb.Text;
                driverInfoModel.Team = addDialog.Team_tb.Text;
                string requestUrl = CommonLibrary.ReadConfigHelper.BaseUrl + WebApiExtensionUrl.UpdateDriver_URL;
                Task<RequestEasyResult> task = Task.Run(() => CommonLibrary.Factory.HttpRequestFactroy.HttpPostRequest<RequestEasyResult>(requestUrl, driverInfoModel));
                if (task.Result.Flag)
                {
                    MessageBox.Show("修改成功！");
                    //修改后刷新数据
                    this.SetPageData(1);
                }
                else
                {
                    MessageBox.Show("修改失败！");
                    return;
                }
            }
        }

        private void Add_Btn_Click(object sender, RoutedEventArgs e)
        {
            AddDialog addDialog = new AddDialog();
            addDialog.Title = "新增司机信息";
            if ((bool)addDialog.ShowDialog())
            {
                DriverInfoModel driverInfoModel = new DriverInfoModel();
                List<DriverInfoModel> driverInfoModels = new List<DriverInfoModel>();
                driverInfoModel.Card = addDialog.Card_tb.Text;
                driverInfoModel.Card = addDialog.Card_tb.Text;
                driverInfoModel.Name = addDialog.Name_tb.Text;
                driverInfoModel.LocomotiveDepot = addDialog.LocomotiveDepot_tb.Text;
                driverInfoModel.Team = addDialog.Team_tb.Text;
                driverInfoModels.Add(driverInfoModel);
                string requestUrl = CommonLibrary.ReadConfigHelper.BaseUrl + WebApiExtensionUrl.AddDrivers_URL;
                Task<RequestEasyResult> task = Task.Run(() => CommonLibrary.Factory.HttpRequestFactroy.HttpPostRequest<RequestEasyResult>(requestUrl, driverInfoModels));
                if (task.Result.Flag)
                {
                    MessageBox.Show("添加成功！");
                    //新增后刷新数据
                    this.SetPageData(1);
                }
                else
                {
                    MessageBox.Show("添加失败！");
                    return;
                }
            }
        }

        private void Delete_Btn_Click(object sender, RoutedEventArgs e)
        {
            //执行删除
            TableItem selectedTableItem = this.SelectedTableItem;
            if (null == selectedTableItem)
            {
                MessageBox.Show("请选择需要删除的数据行！");
                return;
            }
            string id = selectedTableItem.ViewModel.Id;
            List<string> list = new List<string>();
            list.Add(id);
            string requestUrl = CommonLibrary.ReadConfigHelper.BaseUrl + WebApiExtensionUrl.DelDriversByIds_URL;
            Task<RequestEasyResult> task = Task.Run(() => CommonLibrary.Factory.HttpRequestFactroy.HttpPostRequest<RequestEasyResult>(requestUrl, list));
            if (task.Result.Flag)
            {
                MessageBox.Show("删除成功！");
                //删除后刷新数据
                this.SetPageData(1);
            }
            else
            {
                MessageBox.Show("删除失败！");
                return;
            }
        }

        private void Import_Btn_Click(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
        }
    }
}
