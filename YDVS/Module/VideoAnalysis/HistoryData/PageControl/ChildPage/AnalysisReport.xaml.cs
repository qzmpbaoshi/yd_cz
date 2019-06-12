using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SelfCommonTool;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SystemModel;
using SystemModel.DataModel;
using SystemModel.RequestResult;
using VideoAnalysis.HistoryData.ViewModel;

namespace VideoAnalysis.HistoryData.PageControl.ChildPage
{
    /// <summary>
    /// AnalysisReport.xaml 的交互逻辑
    /// </summary>
    public partial class AnalysisReport : Window
    {
        private AnalysisReportViewModel ViewModel { get; set; }
        public AnalysisReport()
        {
            InitializeComponent();
            this.aReport_wait.Visibility = Visibility.Visible;
        }

        private void WinClose_Btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch { }
        }
        public async void InitPageAsync(AnalysisReportViewModel _viewModel)
        {
            try
            {
                await this.Dispatcher.InvokeAsync(() =>
                {
                    this.image_wp.Children.Clear();
                    if (_viewModel == null) return;
                    this.ViewModel = _viewModel;
                    //设置标题
                    StringBuilder sbTitle = new StringBuilder();
                    sbTitle.Append(this.ViewModel.TrainShortName);
                    sbTitle.Append(string.IsNullOrWhiteSpace(this.ViewModel.MDataViewModel.TrainNum) ? "" : ("-" + this.ViewModel.MDataViewModel.TrainNum));
                    sbTitle.Append("（" + this.ViewModel.EventTimeStr + "）");
                    sbTitle.Append("视频分析报告");
                    this.aReport_title.Text = sbTitle.ToString();
                    //设置图片显示
                    foreach (CapVideoImage CapImage in this.ViewModel.CapImages)
                    {
                        byte[] buf = CapImage.CapImageBuf;
                        this.image_wp.Children.Add(this.GetImageRadioButtonByBytes(buf));
                    }
                    //设置LKJ监控数据
                    this.aReport_MonitorData.RefreshDataByViewModel(this.ViewModel.MDataViewModel);
                    //初始化分析数据
                    this.ViewModel.AnalysisTimeStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    this.aReport_time.Text = this.ViewModel.AnalysisTimeStr;
                    this.aReport_person.Text = "";
                    this.aReport_content.Text = "";
                    this.aReport_wait.Visibility = Visibility.Collapsed;
                });
            }
            catch (Exception ex)
            {
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "加载视频分析报告分析页面", ex);
            }
        }
        private RadioButton GetImageRadioButtonByBytes(byte[] iBytes)
        {
            try
            {
                BitmapImage bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.StreamSource = new MemoryStream(iBytes);
                bmp.EndInit();
                Image image = new Image();
                image.Width = 290;
                //image.Height = 290;
                image.Source = bmp;
                RadioButton rb = new RadioButton();
                rb.Margin = new Thickness(3);
                rb.BorderThickness = new Thickness(1);
                rb.SetResourceReference(RadioButton.BorderBrushProperty, "BorderColor");
                rb.SetResourceReference(RadioButton.StyleProperty, "NavRadioButton");
                rb.Content = image;
                return rb;
            }
            catch (Exception ex)
            {
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "二进制转换为图片控件赋值错误", ex);
                return null;
            }
        }

        private void SaveReport_Btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Dispatcher.Invoke(() =>
                {
                    this.aReport_wait.Visibility = Visibility.Visible;
                });
                //创建分析报告保存的Model
                AnalysisReportInfoModel addModel = new AnalysisReportInfoModel
                {
                    TrainType = this.ViewModel.TrainType,
                    TrainNo = this.ViewModel.TrainNo,
                    TrainShortName = this.ViewModel.TrainShortName,
                    EventTime = Convert.ToDateTime(this.ViewModel.EventTimeStr),
                    DriverName = this.ViewModel.MDataViewModel.DriverName,
                    DriverNum = this.ViewModel.MDataViewModel.DriverNum,
                    AssDriverName = this.ViewModel.MDataViewModel.AssDriverName,
                    AssDriverNum = this.ViewModel.MDataViewModel.AssDriverNum,
                    EventDataJsonStr = JsonConvert.SerializeObject(this.ViewModel.MDataViewModel),
                    AnalysisTime = Convert.ToDateTime(this.aReport_time.Text),
                    AnalysisPerson = this.aReport_person.Text,
                    AnalysisContent = this.aReport_content.Text
                };
                //获取报告保存的URL
                string requestUrl = CommonLibrary.ReadConfigHelper.BaseUrl + WebApiExtensionUrl.AddAnalysisReportInfo_URL;
                Task<RequestResult<string>> task = Task<RequestResult<string>>.Run(() =>
                {
                    //保存报告
                    return CommonLibrary.Factory.HttpRequestFactroy.HttpPostRequest<RequestResult<string>>(requestUrl, addModel);
                });
                task.GetAwaiter().OnCompleted(() =>
                {
                    RequestResult<string> rst = task.Result;
                    Console.WriteLine("保存成功！数据保存后的ID=" + rst.ResultData);
                    //报告保存成功，获取保存后的ID，并初步组装报告附件图片model
                    AnalysisReportAttachmentModel attModel = new AnalysisReportAttachmentModel
                    {
                        RelativePath = this.ViewModel.ReportDateStr + @"\" + this.ViewModel.TrainShortName + @"\" + rst.ResultData + @"\",
                        FileExtension = "jpeg",
                        AnalysisReportInfoId = rst.ResultData
                    };
                    try
                    {
                        if (rst.Flag)
                        {
                            requestUrl = CommonLibrary.ReadConfigHelper.BaseUrl + WebApiExtensionUrl.AnalysisReportAttachment_SetExFolder_URL;
                            //在服务端创建保存附件图片的文件夹
                            RequestEasyResult easyRst = CommonLibrary.Factory.HttpRequestFactroy.HttpPostRequest<RequestEasyResult>(requestUrl, attModel);
                            //创建成功
                            if (easyRst.Flag)
                            {
                                //上传图片
                                requestUrl = CommonLibrary.ReadConfigHelper.BaseUrl + WebApiExtensionUrl.AnalysisReportAttachment_FileUpload_URL;
                                this.uploadedLength = 0;
                                List<AnalysisReportAttachmentModel> attModels = new List<AnalysisReportAttachmentModel>();
                                Task<RequestEasyResult> task_upload = Task<RequestEasyResult>.Run(() =>
                                {
                                    RequestEasyResult uploadRst = new RequestEasyResult();
                                    foreach (var cImage in this.ViewModel.CapImages)
                                    {
                                        string channelStr = cImage.VideoChannel.ToString().PadLeft(2, '0');
                                        string fileName = "通道" + channelStr + "_" + cImage.VideoChannelName + "." + attModel.FileExtension;
                                        uploadRst = this.FileUpload(requestUrl, attModel.RelativePath + fileName, cImage.CapImageBuf, 0, 1024 * 500);
                                        if (uploadRst.Flag)
                                            attModels.Add(new AnalysisReportAttachmentModel
                                            {
                                                RelativePath = attModel.RelativePath,
                                                FileExtension = attModel.FileExtension,
                                                AnalysisReportInfoId = attModel.AnalysisReportInfoId,
                                                FileName = fileName,
                                                FileFromChannel = channelStr,
                                                FileFromChannelName = cImage.VideoChannelName
                                            });
                                        else break;
                                    }
                                    return uploadRst;
                                });
                                task_upload.GetAwaiter().OnCompleted(() =>
                                {
                                    RequestEasyResult uploadRst = task_upload.Result;
                                    //图片上传成功
                                    if (uploadRst.Flag)
                                    {
                                        //图片上传成功，保存图片附件信息
                                        requestUrl = CommonLibrary.ReadConfigHelper.BaseUrl + WebApiExtensionUrl.AddAnalysisReportAttachment_URL;
                                        uploadRst = CommonLibrary.Factory.HttpRequestFactroy.HttpPostRequest<RequestEasyResult>(requestUrl, attModels);
                                        if (!uploadRst.Flag)
                                        {
                                            //保存图片附件信息失败，事件回滚，删除对应的分析报告信息
                                            this.DelAnalysisReportInfo(attModel.AnalysisReportInfoId, attModel.RelativePath);
                                            this.Dispatcher.Invoke(() =>
                                            {
                                                MessageForm.Show("提示", "信息保存失败！", 1);

                                            });
                                        }
                                        else
                                        {
                                            this.Dispatcher.Invoke(() =>
                                            {
                                                MessageForm.Show("提示", "信息保存成功！", 0);
                                                this.Close();
                                            });
                                        }
                                    }
                                    else
                                    {
                                        //图片上传失败，事件回滚，删除对应的分析报告信息
                                        this.DelAnalysisReportInfo(attModel.AnalysisReportInfoId, attModel.RelativePath);
                                    }
                                    this.Dispatcher.Invoke(() =>
                                    {
                                        this.aReport_wait.Visibility = Visibility.Collapsed;
                                    });
                                });
                            }
                            else
                            {
                                //创建失败，事件回滚，删除对应的分析报告信息
                                this.DelAnalysisReportInfo(attModel.AnalysisReportInfoId, attModel.RelativePath);
                                MessageForm.Show("提示", "图片所在文件夹创建失败！", 1);
                            }
                        }
                        else
                            MessageForm.Show("提示", "分析报告保存失败！", 1);
                    }
                    catch (Exception ex)
                    {
                        //创建失败，事件回滚，删除对应的分析报告信息
                        this.DelAnalysisReportInfo(attModel.AnalysisReportInfoId, attModel.RelativePath);
                        CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "保存附件信息失败", ex);
                        this.Dispatcher.Invoke(() =>
                        {
                            this.aReport_wait.Visibility = Visibility.Collapsed;
                        });
                    }
                });
            }
            catch (Exception ex)
            {
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "保存报告错误", ex);
                this.Dispatcher.Invoke(() =>
                {
                    this.aReport_wait.Visibility = Visibility.Collapsed;
                });
            }
        }
        private void DelAnalysisReportInfo(string aReportId, string relativePath)
        {
            try
            {
                string requestUrl = CommonLibrary.ReadConfigHelper.BaseUrl + WebApiExtensionUrl.DelAnalysisReportInfo_URL;
                DelAnalysisReportInfoModel delModel = new DelAnalysisReportInfoModel
                {
                    AnalysisReportInfoId = aReportId,
                    RelativePath = relativePath
                };
                CommonLibrary.Factory.HttpRequestFactroy.HttpPostRequest<RequestEasyResult>(requestUrl, delModel);
            }
            catch { }
        }
        #region 上传文件
        private float uploadedLength = 0;
        private RequestEasyResult FileUpload(string hostUrl, string fileName, byte[] imageBuf, long startPoint, int byteCount)
        {
            RequestEasyResult rst = new RequestEasyResult
            {
                Flag = false,
                Msg = ""
            };
            try
            {
                WebClient webClient = new WebClient();
                long fileLength = imageBuf.Length;
                if (startPoint < 0 || startPoint >= fileLength)
                    return rst;
                byte[] data = null;
                #region 分割文件上传
                for (; startPoint <= fileLength - 1; startPoint = startPoint + byteCount)
                {
                    int step = 0;
                    if (startPoint + byteCount > fileLength)
                    {
                        data = imageBuf.Skip(Convert.ToInt32(startPoint)).Take(Convert.ToInt32(fileLength - startPoint)).ToArray();
                        step = Convert.ToInt32(fileLength - startPoint);
                    }
                    else
                    {
                        data = imageBuf.Skip(Convert.ToInt32(startPoint)).Take(byteCount).ToArray();
                        step = byteCount;
                    }
                    webClient.Headers.Remove(HttpRequestHeader.ContentRange);
                    webClient.Headers.Add(HttpRequestHeader.ContentRange, "bytes " + startPoint + "-" + (startPoint + step) + "/" + fileLength);
                    byte[] rstBytes = webClient.UploadData(hostUrl + "?fileName=" + fileName, "POST", data);
                    rst = JsonConvert.DeserializeObject<RequestEasyResult>(Encoding.UTF8.GetString(rstBytes));
                    if (rst.Flag)
                        this.Dispatcher.Invoke(() =>
                        {
                            this.uploadedLength += step;
                            this.aReport_wait.TextInfoStr = "上传:" + (this.uploadedLength / this.ViewModel.ImageStreamTotalLength).ToString("P");
                        });
                    else break;
                }
                #endregion
            }
            catch (Exception ex)
            {
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "上传文件出错", ex);
            }
            return rst;
        }
        #endregion
    }
}
