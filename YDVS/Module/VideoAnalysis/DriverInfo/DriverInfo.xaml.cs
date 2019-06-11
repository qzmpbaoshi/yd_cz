using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
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
using NPOI.SS.UserModel;
using SelfCommonTool;
using VideoAnalysis.DriverInfo.PageControl;
using VideoAnalysis.DriverInfo.ViewModel;
using MessageBox = System.Windows.MessageBox;
using UserControl = System.Windows.Controls.UserControl;

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

            List<DriverInfoModel> DriverInfoModels = new List<DriverInfoModel>();
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
                    DriverInfoModel driverInfoModel = new DriverInfoModel();
                    driverInfoModel.Card = sheet.GetRow(i).GetCell(0).ToString();
                    driverInfoModel.Name = sheet.GetRow(i).GetCell(1).ToString();
                    driverInfoModel.LocomotiveDepot = sheet.GetRow(i).GetCell(2).ToString();
                    driverInfoModel.Team = sheet.GetRow(i).GetCell(3).ToString();
                    DriverInfoModels.Add(driverInfoModel);
                    driverInfoModel = null;
                }
            }
            string requestUrl = CommonLibrary.ReadConfigHelper.BaseUrl + WebApiExtensionUrl.AddDrivers_URL;
            Task<RequestEasyResult> task = Task.Run(() => CommonLibrary.Factory.HttpRequestFactroy.HttpPostRequest<RequestEasyResult>(requestUrl, DriverInfoModels));
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
        public void pictureconfig()
        {
            //图片路径信息
            string pathName =null;
            //System.IO.FileStream fs = new System.IO.FileStream(pathName,System.IO.FileMode.Open,System.IO.FileAccess.Read);
            var image = new Image();
            try
            {
                System.Net.WebRequest webreq = System.Net.WebRequest.Create(pathName);
                System.Net.WebResponse webres = webreq.GetResponse();
                System.IO.Stream stream = webres.GetResponseStream();
                System.Drawing.Image img1 = System.Drawing.Image.FromStream(stream);
                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(img1);
                IntPtr hBitmap = bmp.GetHbitmap();
                System.Windows.Media.ImageSource WpfBitmap = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                image.Source = WpfBitmap;
                image.Stretch = Stretch.Uniform;
                stream.Dispose();
            }
            catch (Exception e)
            {

            }
            //return image;
           
        }
    }
}
