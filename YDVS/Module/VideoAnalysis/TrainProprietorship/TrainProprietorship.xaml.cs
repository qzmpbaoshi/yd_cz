using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using SystemModel.DataModel;
using SystemModel.SearchConditionModel;
using CommonLibrary.Extension;
using SystemModel.RequestResult;
using SystemModel;
using System.Windows;
using System.Threading.Tasks;
using System.Windows.Forms;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SelfCommonTool;
using VideoAnalysis.TrainProprietorship.PageControl;
using VideoAnalysis.TrainProprietorship.ViewModel;
using MessageBox = System.Windows.MessageBox;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using UserControl = System.Windows.Controls.UserControl;

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
                Task<RequestResult<List<TrainProprietorshipModel>>> task = Task<RequestResult<List<TrainProprietorshipModel>>>.Run((Func<RequestResult<List<TrainProprietorshipModel>>>)(() =>
                {
                    return (RequestResult<List<TrainProprietorshipModel>>)CommonLibrary.Factory.HttpRequestFactroy.HttpPostRequest<RequestResult<List<TrainProprietorshipModel>>>(requestUrl, condition);
                }));
                task.GetAwaiter().OnCompleted(() =>
                {
                    RequestResult<List<TrainProprietorshipModel>> rst = task.Result;
                    this.Dispatcher.Invoke(() =>
                    {
                        TableItem itemControl = null;
                        int order = 1;
                        foreach (var item in rst.ResultData)
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
        /// <summary>
        /// 编辑
        /// </summary>
        private void Edit_Btn_Click(object sender, RoutedEventArgs e)
        {
            TableItem selectedTableItem = this.SelectedTableItem;
            if (null == selectedTableItem)
            {
                MessageBox.Show("请选择需要修改的数据行！");
                return;
            }
            AddDialog addDialog = new AddDialog();
            addDialog.Title = "编辑机车信息";
            addDialog.trainno_tb.Text = selectedTableItem.ViewModel.TrainNo;
            addDialog.traintype_tb.Text = selectedTableItem.ViewModel.TrainType;
            addDialog.workshop_tb.Text = selectedTableItem.ViewModel.WorkShop;
            addDialog.workshop_tb.Text = selectedTableItem.ViewModel.WorkShop;
            addDialog.locomotivedepot_tb.Text = selectedTableItem.ViewModel.LocomotiveDepot;
            if ((bool)addDialog.ShowDialog())
            {
                TrainProprietorshipViewModel trainProprietorshipViewModel = selectedTableItem.ViewModel;
                TrainProprietorshipModel trainProprietorshipModel = new TrainProprietorshipModel();
                trainProprietorshipModel.Id = trainProprietorshipViewModel.Id;
                Console.WriteLine(trainProprietorshipViewModel.Id + "trainProprietorshipViewModelId");
                trainProprietorshipModel.TrainType = addDialog.traintype_tb.Text;
                Console.WriteLine(addDialog.traintype_tb.Text + "addDialog.traintype_tb");
                trainProprietorshipModel.TrainNo = addDialog.trainno_tb.Text;
                Console.WriteLine(addDialog.trainno_tb.Text + "addDialog.trainno_tb");
                trainProprietorshipModel.WorkShop = addDialog.workshop_tb.Text;
                trainProprietorshipModel.LocomotiveDepot = addDialog.locomotivedepot_tb.Text;
                trainProprietorshipModel.RailwayAdministration = addDialog.RailwayAdministration_tb.Text;
                string requestUrl = CommonLibrary.ReadConfigHelper.BaseUrl + WebApiExtensionUrl.UpdateTrainProprietorship_URL;
                Task<RequestEasyResult> task = Task.Run(() => CommonLibrary.Factory.HttpRequestFactroy.HttpPostRequest<RequestEasyResult>(requestUrl, trainProprietorshipModel));
                if (task.Result.Flag)
                {
                    //修改后刷新数据
                    this.SetPageData(1);
                    MessageForm.Show("提示", "修改成功！", 0);
                }
                else
                {
                    MessageForm.Show("提示", "修改失败！", 0);

                    return;
                }
            }

        }
        /// <summary>
        /// 新增
        /// </summary>
        private void Add_Btn_Click(object sender, RoutedEventArgs e)
        {
            AddDialog addDialog = new AddDialog();
            addDialog.Title = "新增机车信息";
            //this.edit_bo.Child = addDialog;
            if ((bool)addDialog.ShowDialog())
            {
                TrainProprietorshipModel trainProprietorshipModel = new TrainProprietorshipModel();
                List<TrainProprietorshipModel> trainProprietorshipModels = new List<TrainProprietorshipModel>();
                trainProprietorshipModel.TrainType = addDialog.traintype_tb.Text;
                trainProprietorshipModel.TrainNo = addDialog.trainno_tb.Text;
                trainProprietorshipModel.WorkShop = addDialog.workshop_tb.Text;
                trainProprietorshipModel.LocomotiveDepot = addDialog.locomotivedepot_tb.Text;
                trainProprietorshipModel.RailwayAdministration = addDialog.RailwayAdministration_tb.Text;
                trainProprietorshipModels.Add(trainProprietorshipModel);
                string requestUrl = CommonLibrary.ReadConfigHelper.BaseUrl + WebApiExtensionUrl.AddTrainProprietorships_URL;
                Task<RequestEasyResult> task = Task.Run(() => CommonLibrary.Factory.HttpRequestFactroy.HttpPostRequest<RequestEasyResult>(requestUrl, trainProprietorshipModels));
                if (task.Result.Flag)
                {
                    //新增后刷新数据
                    this.SetPageData(1);
                    MessageForm.Show("提示", "添加成功！", 0);
                }
                else
                {
                    MessageForm.Show("提示", "添加失败！", 0);
                    return;
                }
            }

        }
        /// <summary>
        /// 删除
        /// </summary>
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
            string requestUrl = CommonLibrary.ReadConfigHelper.BaseUrl + WebApiExtensionUrl.DelTrainProprietorshipsByIds_URL;
            Task<RequestEasyResult> task = Task.Run(() => CommonLibrary.Factory.HttpRequestFactroy.HttpPostRequest<RequestEasyResult>(requestUrl, list));
            if (task.Result.Flag)
            {
                //删除后刷新数据
                this.SetPageData(1);
                MessageForm.Show("提示", "删除成功！", 0);
            }
            else
            {
                MessageForm.Show("提示", "删除失败！", 0);
                return;
            }

        }
        /// <summary>
        /// 导入
        /// </summary>
        private void Import_Btn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog
            {
                //ofd.DefaultExt = "升级文件";
                Filter = "Excel(*.xlsx)|*.xlsx",
                Title = "导入",
                Multiselect = false

            };
            if (ofd.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            List<TrainProprietorshipModel> trainProprietorshipModels = new List<TrainProprietorshipModel>();
            IWorkbook book;
            using (FileStream fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read))
            {
                book = WorkbookFactory.Create(fs);
            }
            int sheetCount = book.NumberOfSheets;
            for (int sheetIndex = 0; sheetIndex < sheetCount; sheetIndex++)
            {
                ISheet sheet = book.GetSheetAt(sheetIndex);
                if (sheet == null) continue;
                IRow row = sheet.GetRow(0);
                if (row == null) continue;
                int firstCellNum = row.FirstCellNum;
                int lastCellNum = row.LastCellNum;
                if (firstCellNum == lastCellNum) continue;
                for (int i = 0; i <= sheet.LastRowNum; i++)
                {
                    TrainProprietorshipModel trainProprietorshipModel = new TrainProprietorshipModel();
                    trainProprietorshipModel.TrainType = sheet.GetRow(i).GetCell(0).ToString();
                    trainProprietorshipModel.TrainNo = sheet.GetRow(i).GetCell(1).ToString();
                    trainProprietorshipModel.RailwayAdministration = sheet.GetRow(i).GetCell(2).ToString();
                    trainProprietorshipModel.LocomotiveDepot = sheet.GetRow(i).GetCell(3).ToString();
                    trainProprietorshipModel.WorkShop = sheet.GetRow(i).GetCell(4).ToString();
                    trainProprietorshipModel.Order = null;
                    trainProprietorshipModels.Add(trainProprietorshipModel);
                    trainProprietorshipModel = null;
                }
            }
            string requestUrl = CommonLibrary.ReadConfigHelper.BaseUrl + WebApiExtensionUrl.AddTrainProprietorships_URL;
            Task<RequestEasyResult> task = Task.Run(() => CommonLibrary.Factory.HttpRequestFactroy.HttpPostRequest<RequestEasyResult>(requestUrl, trainProprietorshipModels));
            if (task.Result.Flag)
            {
                //新增后刷新数据
                this.SetPageData(1);
                MessageForm.Show("提示", "导入成功！", 0);
            }
            else
            {
                MessageForm.Show("提示", "导入失败！", 0);
                return;
            }

            return;

        }

        private void Refresh_Btn_Click(object sender, RoutedEventArgs e)
        {
            this.SetPageData(1);
        }
    }
}
