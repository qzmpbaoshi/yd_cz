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
using VideoAnalysis.HistoryData.Helper;
using System.IO;
using VideoAnalysis.HistoryData.ViewModel;
using VideoAnalysis.HistoryData.EventHandler;

namespace VideoAnalysis.HistoryData.PageControl
{
    /// <summary>
    /// DirectoryTree.xaml 的交互逻辑
    /// </summary>
    public partial class DirectoryTree : UserControl
    {
        public event EventHandler<VideoSourceEventArgs> SetVideoSourceEvent;
        public DirectoryTree()
        {
            InitializeComponent();
        }

        public void InitPage()
        {
            this.SetDriveComboBox();
        }
        /// <summary>
        /// 设置电脑盘符选择下拉框
        /// </summary>
        private void SetDriveComboBox()
        {
            try
            {
                DriveInfo[] drivers = FileHelper.GetDriveInfos();
                drivers = drivers == null ? null : drivers.Where(d => d.DriveType == DriveType.Fixed || d.DriveType == DriveType.Removable).ToArray();
                this.Dispatcher.InvokeAsync(()=> {
                    this.drive_comboBox.ItemsSource = drivers;
                    this.drive_comboBox.DisplayMemberPath = "Name";
                    this.drive_comboBox.SelectedIndex = drivers.Count() - 1;
                });
            }
            catch (Exception ex)
            {
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "设置电脑盘符", ex);
            }
        }
        private void Drive_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ComboBox cb = sender as ComboBox;
                if (cb == null) return;
                DriveInfo dInfo = cb.SelectedItem as DriveInfo;
                if (dInfo == null) return;
                this.SetDicTreeView(dInfo.Name);
            }
            catch (Exception ex)
            {
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "电脑盘符选择", ex);
            }
        }

        private void SetDicTreeView(string basePath)
        {
            try
            {
                List<DirTreeViewItemModel> dtViewItemList = FileHelper.GetVideoDirTreeItemList(basePath);
                List<TreeViewItem> treeList = new List<TreeViewItem>();
                TreeViewItem tvi = new TreeViewItem();
                tvi.Header = basePath;
                tvi.Tag = new DirTreeViewItemModel
                {
                    FullPathName = basePath,
                    Name = basePath,
                    IsBreaf = false,
                    IsVideoFolder = false
                };
                tvi.Foreground = Brushes.White;
                tvi.SetResourceReference(TreeViewItem.StyleProperty, "TreeViewItemStyle");
                foreach (DirTreeViewItemModel item in dtViewItemList)
                {
                    TreeViewItem tvi2 = new TreeViewItem();
                    tvi2.Header = item.Name;
                    tvi2.Tag = item;
                    tvi2.Foreground = Brushes.White;
                    tvi2.SetResourceReference(TreeViewItem.StyleProperty, "TreeViewItemStyle");
                    if (!item.IsBreaf)
                    {
                        tvi2.Expanded += TreeViewItem_Expanded;
                        TreeViewItem tvitemp = new TreeViewItem();
                        tvitemp.Header = "";
                        tvitemp.Foreground = Brushes.White;
                        tvi2.Items.Add(tvitemp);
                    }
                    tvi.Items.Add(tvi2);
                }
                tvi.IsExpanded = true;
                tvi.IsSelected = true;
                treeList.Add(tvi);
                treeList[0].IsSelected = true;
                this.dir_treeView.ItemsSource = treeList;
            }
            catch (Exception ex)
            {
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "设着文件夹结构树", ex);
            }
        }

        private void TreeViewItem_Expanded(object sender, RoutedEventArgs e)
        {
            try
            {
                TreeViewItem treeItem = e.Source as TreeViewItem;
                treeItem.Items.Clear();
                DirTreeViewItemModel parentViewItem = treeItem.Tag as DirTreeViewItemModel;
                if (parentViewItem == null) return;
                List<DirTreeViewItemModel> children = FileHelper.GetVideoDirTreeItemList(parentViewItem.FullPathName, parentViewItem.IsVideoFolder || parentViewItem.Name.ToUpper().Contains("6A-VIDEO"));
                if (children == null) return;
                foreach (DirTreeViewItemModel item in children)
                {
                    TreeViewItem tvi2 = new TreeViewItem();
                    tvi2.Header = item.Name;
                    tvi2.Tag = item;
                    tvi2.Foreground = Brushes.White;
                    tvi2.SetResourceReference(TreeViewItem.StyleProperty, "TreeViewItemStyle");
                    if (!item.IsBreaf)
                    {
                        TreeViewItem tvitemp = new TreeViewItem();
                        tvitemp.Foreground = Brushes.White;
                        tvitemp.Header = "";
                        tvi2.Items.Add(tvitemp);
                    }
                    treeItem.Items.Add(tvi2);
                }
            }
            catch (Exception ex)
            {
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "设置文件夹结构树子节点展开", ex);
            }
        }
        private void Dir_TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                TreeView tv = sender as TreeView;
                if (tv == null) return;
                TreeViewItem viewItem = tv.SelectedItem as TreeViewItem;
                if (viewItem == null) return;
                DirTreeViewItemModel item = viewItem.Tag as DirTreeViewItemModel;
                //Console.WriteLine(viewItem.Header + "||" + item.IsVideoFolder.ToString());
                if (!string.IsNullOrWhiteSpace(item.FullPathName) && item.IsVideoFolder)
                {
                    List<VideoSource> videoSources = FileHelper.GetVideoSourceList(item.FullPathName);
                    if (videoSources != null && videoSources.Count > 0)
                        this.SetVideoSourceEvent(this, new VideoSourceEventArgs(item.Name, videoSources));
                }
            }
            catch (Exception ex)
            {
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "选择文件夹结构树子节点", ex);
            }
        }
    }
}
