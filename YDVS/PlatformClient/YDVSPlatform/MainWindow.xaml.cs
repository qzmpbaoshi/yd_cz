using SelfCommonTool;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SystemModel;

namespace YDVSPlatform
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        public User User { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }
        #region 加载系统功能模块
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.LoadModule();
        }
        /// <summary>
        /// 系统功能模块容器
        /// </summary>
        [ImportMany(typeof(ModuleInterface.ModuleInterface))]
        public IEnumerable<Lazy<ModuleInterface.ModuleInterface>> PlatformModules { get; set; }
        /// <summary>
        /// 系统加载可用模块
        /// </summary>
        private void LoadModule()
        {
            try
            {
                //读取模块
                this.ReadModules();
                //初始化模块显示容器
                this.InitModuleContainer();
            }
            catch (Exception ex)
            {
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "主页面加载功能模块", ex);
            }
        }
        private void InitModuleContainer()
        {
            try
            {
                if (this.PlatformModules != null && PlatformModules.Count() > 0)
                {
                    ModuleInterface.ModuleInterface control = PlatformModules.First().Value;
                    control.CloseEvent += ModuleControl_CloseEvent;
                    this.container_bo.Child = control.GetControl(this.User);
                    this.title_2_tb.Text = control.GetControlName();
                }
            }
            catch (Exception ex)
            {
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "主页面为容器加载模块", ex);
            }
        }

        private void ModuleControl_CloseEvent(object sender, EventArgs e)
        {
            try
            {
                //this.container_bo.Child = null;
                //this.container_bo.Child.Visibility = Visibility.Collapsed;
                //MessageBox.Show("退出", "功能退出", MessageBoxButton.OKCancel, MessageBoxImage.Information, MessageBoxResult.OK);
                MessageForm.Show("退出", "功能退出",2);
            }
            catch (Exception ex)
            {
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "主页面关闭当前模块", ex);
            }
        }
        /// <summary>
        /// 读取系统中的可用模块
        /// </summary>
        private void ReadModules()
        {
            try
            {
                var aggCat = new AggregateCatalog();
                aggCat.Catalogs.Add(new DirectoryCatalog(AppDomain.CurrentDomain.BaseDirectory + @"Module"));
                var container = new CompositionContainer(aggCat);
                container.ComposeParts(this);
            }
            catch (Exception ex)
            {
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "主页面获取功能模块", ex);
            }
        }
        #endregion
        #region 主页面最大、最小、退出按钮事件
        private void WinCloseBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "退出程序", ex);
            }
        }

        private void WinMinBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.WindowState = System.Windows.WindowState.Minimized;
            }
            catch (Exception ex)
            {
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "窗口最小化", ex);
            }
        }

        private void WinChangeBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                if (this.WindowState == System.Windows.WindowState.Maximized)
                {
                    this.WindowState = System.Windows.WindowState.Normal;
                    btn.SetResourceReference(Button.StyleProperty, "WinMaxButton");
                }

                else
                {
                    this.WindowState = System.Windows.WindowState.Maximized;
                    btn.SetResourceReference(Button.StyleProperty, "WinNormalButton");
                }
            }
            catch (Exception ex)
            {
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "窗口最小化", ex);
            }
        }

        private void WinDragMoveBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch (Exception ex)
            {
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "窗口移动", ex);
            }
        }
        #endregion


    }
}
