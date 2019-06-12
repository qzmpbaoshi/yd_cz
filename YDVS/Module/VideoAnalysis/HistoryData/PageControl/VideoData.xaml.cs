using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VideoAnalysis.HistoryData.Handler;
using VideoAnalysis.HistoryData.ViewModel;
using UserControl = System.Windows.Controls.UserControl;

namespace VideoAnalysis.HistoryData.PageControl
{
    /// <summary>
    /// VideoData.xaml 的交互逻辑
    /// </summary>
    public partial class VideoData : UserControl
    {
        public VideoData()
        {
            InitializeComponent();
            //读取本地idx文件
            //dataconverter();
        }
        private  void dataconverter()
        {

            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog
            {
                Filter = "二进制文件(*.idx)|*.idx",
                Title = "打开",
                Multiselect = false
            };
            if (ofd.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            BinaryReader br;
            byte[] bytes;
            using (FileStream fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read))
            {
                //book = WorkbookFactory.Create(fs);
                br = new BinaryReader(fs);
                bytes = br.ReadBytes((int)fs.Length);
            }
            //字节数组转结构体
            DataStruct? dataStruct = StructUtils.GetDataStruct(bytes);
            DataStruct dataStructValue = dataStruct.Value;
            //ushort head = dataStructValue.head;
            //Console.WriteLine(head);
            //获取车次结构体数字部分
            TrainNumbStruct trainNumbStruct = dataStructValue.tainNumbStruct;
            ushort length = dataStructValue.length;
            //结构体大小
            int sizeOf = Marshal.SizeOf(trainNumbStruct);
            //转换字节数组
            byte[] stuctToByte = StructHelper.StuctToByte(trainNumbStruct);
            //字节数组转asc码字符串
            string s = Encoding.ASCII.GetString(stuctToByte);
            trainNum.Text = s;
        }

        private void trainNum_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog
            {
                Filter = "二进制文件(*.idx)|*.idx",
                Title = "打开",
                Multiselect = false
            };
            if (ofd.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            BinaryReader br;
            byte[] bytes;
            using (FileStream fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read))
            {
                //book = WorkbookFactory.Create(fs);
                br = new BinaryReader(fs);
                bytes = br.ReadBytes((int)fs.Length);
            }
            //字节数组转结构体
            DataStruct? dataStruct = StructUtils.GetDataStruct(bytes);
            DataStruct dataStructValue = dataStruct.Value;
            //ushort head = dataStructValue.head;
            //Console.WriteLine(head);
            //获取车次结构体数字部分
            TrainNumbStruct trainNumbStruct = dataStructValue.tainNumbStruct;
            ushort length = dataStructValue.length;
            //结构体大小
            int sizeOf = Marshal.SizeOf(trainNumbStruct);
            //转换字节数组
            byte[] stuctToByte = StructHelper.StuctToByte(trainNumbStruct);
            //字节数组转asc码字符串
            string s = Encoding.ASCII.GetString(stuctToByte);
            trainNum.Text = s;
        }
    }
}
