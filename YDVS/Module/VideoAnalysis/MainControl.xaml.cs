using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.Generic;

namespace VideoAnalysis
{
    /// <summary>
    /// MainControl.xaml 的交互逻辑
    /// </summary>
    public partial class MainControl : UserControl
    {
        public event EventHandler CloseEvent;
        public MainControl()
        {
            InitializeComponent();
        }
        #region 导航按钮事件
        private void NavSelect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RadioButton rBtn = sender as RadioButton;
                this.SetMainContainer(rBtn.Uid);
                DockPanel dp = rBtn.Content as DockPanel;
                foreach (var ui in dp.Children)
                {
                    if (ui is Image)
                    {
                        crumb_img.Source = ((Image)ui).Source;
                    }
                    if (ui is TextBlock)
                    {
                        crumb_tb.Text = ((TextBlock)ui).Text;
                    }
                }

            }
            catch (Exception ex)
            {
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "视频分析模块，功能选择按钮", ex);
            }
        }
        private void SetMainContainer(string pageType)
        {
            UIElement mainUI = null;
            switch (pageType)
            {
                case "1":
                    mainUI = new HistoryData.HistoryData();
                    break;
                case "2":
                    mainUI = new TrainProprietorship.TrainProprietorship();
                    break;
                case "3":
                    mainUI = new DriverInfo.DriverInfo();
                    break;
                default:
                    break;
            }
            this.main_container_bo.Child = mainUI;
        }

        #endregion
        /// <summary>
        /// 退出当前模块
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ModuleCloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.CloseEvent(this, e);
        }
        #region 是否隐藏导航按钮
        private bool isShowNav = true;
        /// <summary>
        /// 显示或隐藏导航
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Nav_Show_Btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.isShowNav = !this.isShowNav;
                this.nav_bo.Visibility = this.isShowNav ? Visibility.Visible : Visibility.Collapsed;
                Grid.SetColumn(this.main_container_bo, this.isShowNav ? 1 : 0);
                Grid.SetColumnSpan(this.main_container_bo, this.isShowNav ? 1 : 2);
                this.nav_show_btn_path.Data = this.isShowNav ? this.LeftArrowBtnPath() : this.RightArrowBtnPath();
            }
            catch (Exception ex)
            {
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "显示或隐藏导航", ex);
            }
        }
        private PathGeometry LeftArrowBtnPath()
        {
            PathGeometry pg = new PathGeometry();
            PolyLineSegment pls = new PolyLineSegment();
            pls.Points.Add(new Point(0, 12));
            pls.Points.Add(new Point(10, 24));
            List<PathSegment> psList = new List<PathSegment>();
            psList.Add(pls);
            PathFigure pf = new PathFigure(new Point(10, 0), psList.ToArray(), false);
            pg.Figures.Add(pf);

            PolyLineSegment pls2 = new PolyLineSegment();
            pls2.Points.Add(new Point(10, 12));
            pls2.Points.Add(new Point(20, 24));
            List<PathSegment> psList2 = new List<PathSegment>();
            psList2.Add(pls2);
            PathFigure pf2 = new PathFigure(new Point(20, 0), psList2.ToArray(), false);

            pg.Figures.Add(pf2);
            return pg;
        }
        private PathGeometry RightArrowBtnPath()
        {
            PathGeometry pg = new PathGeometry();
            PolyLineSegment pls = new PolyLineSegment();
            pls.Points.Add(new Point(10, 12));
            pls.Points.Add(new Point(0, 24));
            List<PathSegment> psList = new List<PathSegment>();
            psList.Add(pls);
            PathFigure pf = new PathFigure(new Point(0, 0), psList.ToArray(), false);
            pg.Figures.Add(pf);

            PolyLineSegment pls2 = new PolyLineSegment();
            pls2.Points.Add(new Point(20, 12));
            pls2.Points.Add(new Point(10, 24));
            List<PathSegment> psList2 = new List<PathSegment>();
            psList2.Add(pls2);
            PathFigure pf2 = new PathFigure(new Point(10, 0), psList2.ToArray(), false);

            pg.Figures.Add(pf2);
            return pg;
        }
        #endregion
    }
}
