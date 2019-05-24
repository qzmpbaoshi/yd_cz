using System.Windows.Controls;
using VideoAnalysis.TrainProprietorship.ViewModel;

namespace VideoAnalysis.TrainProprietorship.PageControl
{
    /// <summary>
    /// TableHead.xaml 的交互逻辑
    /// </summary>
    public partial class TableItem : UserControl
    {
        public TrainProprietorshipViewModel ViewModel { get; set; }
        public TableItem()
        {
            InitializeComponent();
            this.ViewModel = new TrainProprietorshipViewModel();
            this.DataContext = this.ViewModel;
        }
    }
}
