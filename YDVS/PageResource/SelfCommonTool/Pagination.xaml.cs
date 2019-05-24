using System;
using System.Windows;
using System.Windows.Controls;

namespace SelfCommonTool
{
    /// <summary>
    /// PageControl.xaml 的交互逻辑
    /// </summary>
    public partial class Pagination : UserControl
    {
        private int PageIndex { get; set; }
        private int PageSize { get; set; }
        private int TotalPages { get; set; }
        private int TotalCount { get; set; }
        public event EventHandler PageButtonClick;
        public Pagination()
        {
            InitializeComponent();
        }
        public void SetPageContent(int pIndex, int pSize, int pTotalCount)
        {
            try
            {
                this.PageIndex = pIndex;
                this.PageSize = pSize;
                this.TotalCount = pTotalCount;
                int totalPages = this.PageSize <= 0 ? 0 : (this.TotalCount % this.PageSize == 0 ? (this.TotalCount / this.PageSize) : (this.TotalCount / this.PageSize + 1));
                this.TotalPages = totalPages;
                if (this.PageIndex <= 1)
                    this.upBtn.IsEnabled = false;
                else
                    this.upBtn.IsEnabled = true;
                if (this.PageIndex >= this.TotalPages)
                    this.nextBtn.IsEnabled = false;
                else
                    this.nextBtn.IsEnabled = true;
                this.pageContent.Text = string.Format("当前第{0}页，共{1}条，共{2}页", new string[3] { this.PageIndex.ToString(), this.TotalCount.ToString(), this.TotalPages.ToString() });
            }
            catch { this.InitPageContent(); }
        }
        public void InitPageContent()
        {
            this.SetPageContent(1, 0, 0);
        }

        private void LeftBtn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null) return;
            if (PageIndex == 1)
            {
                btn.IsEnabled = false;
            }
            else
            {
                btn.IsEnabled = true;
                --this.PageIndex;
            }
            PageButtonClick(this, e);
        }

        private void RightBtn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null) return;
            if (PageIndex == TotalPages)
            {
                btn.IsEnabled = false;
            }
            else
            {
                btn.IsEnabled = true;
                ++this.PageIndex;
            }
            PageButtonClick(this, e);
        }
        public int GetPageIndex()
        {
            return this.PageIndex;
        }
    }
}
