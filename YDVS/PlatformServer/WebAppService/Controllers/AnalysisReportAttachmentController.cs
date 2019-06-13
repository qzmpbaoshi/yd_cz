using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.IO;
using SystemModel.RequestResult;
using SystemModel.DataModel;
using WebAppService.DataAdapter;

namespace WebAppService.Controllers
{
    public class AnalysisReportAttachmentController : Controller
    {
        private IAnalysisReportAttachmentAdapter aReportAttAdapter { get; set; }
        private string basePath = ConfigerHelper.GetBaseAttPath();
        public AnalysisReportAttachmentController()
        {
            this.aReportAttAdapter = new AnalysisReportAttachmentAdapter();
        }

        /// <summary>
        /// 添加方法
        /// </summary>
        /// <param name="addModel"></param>
        /// <returns></returns>
        [HttpPost]
        public string AddAnalysisReportAttachment([System.Web.Http.FromBody]List<AnalysisReportAttachmentModel> addModels)
        {
            RequestEasyResult rst = this.aReportAttAdapter.AddAnalysisReportAttachments(addModels, basePath);
            return JsonConvert.SerializeObject(rst);
        }

        [HttpPost]
        public string SetExFolder([System.Web.Http.FromBody]AnalysisReportAttachmentModel attModel)
        {
            RequestEasyResult rst = new RequestEasyResult();
            if (string.IsNullOrWhiteSpace(attModel.AnalysisReportInfoId))
            {
                rst.Flag = false;
                rst.Msg = "设置文件夹失败！";
                return JsonConvert.SerializeObject(rst);
            }
            try
            {
                string dirPath = basePath + attModel.RelativePath;
                if (!Directory.Exists(dirPath))
                    Directory.CreateDirectory(dirPath);
                else
                {
                    string[] files = Directory.GetFiles(dirPath);
                    foreach (string f in files)
                    {
                        System.IO.File.Delete(f);
                    }
                }
                rst.Flag = true;
                rst.Msg = "设置文件夹成功！";
            }
            catch (Exception ex)
            {
                rst.Flag = false;
                rst.Msg = "设置文件夹失败！";
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "设置分析报表附件文件夹", ex);
            }
            return JsonConvert.SerializeObject(rst);
        }
        /// <summary>
        /// 文件上传。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string FileUpload()
        {
            RequestEasyResult rst = new RequestEasyResult();
            try
            {
                var fileStream = Request.InputStream;
                var fileName = Request.QueryString["fileName"];
                SaveAs(this.basePath + fileName, fileStream);
                rst.Flag = true;
                rst.Msg = "上传成功！";
            }
            catch (Exception ex)
            {
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "上传文件出错", ex);
                rst.Flag = false;
                rst.Msg = "上传失败！";

            }
            return JsonConvert.SerializeObject(rst);
        }

        /// <summary>
        /// 给已有文件追加文件流。
        /// </summary>
        /// <param name="saveFilePath"></param>
        /// <param name="stream"></param>
        private void SaveAs(string saveFilePath, System.IO.Stream stream)
        {
            //接收到的字节信息。
            long startPosition = 0;
            long endPosition = 0;
            var contentRange = Request.Headers["Content-Range"];//contentRange样例：bytes 10000-20000/59999
            if (!string.IsNullOrEmpty(contentRange))
            {
                contentRange = contentRange.Replace("bytes", "").Trim();
                contentRange = contentRange.Substring(0, contentRange.IndexOf("/"));
                string[] ranges = contentRange.Split('-');
                startPosition = long.Parse(ranges[0]);
                endPosition = long.Parse(ranges[1]);
            }
            //默认写针位置。
            System.IO.FileStream fs;
            long writeStartPosition = 0;
            if (System.IO.File.Exists(saveFilePath))
            {
                fs = System.IO.File.OpenWrite(saveFilePath);
                writeStartPosition = fs.Length;
            }
            else
            {
                fs = new System.IO.FileStream(saveFilePath, System.IO.FileMode.Create);
            }
            //调整写针位置。
            if (writeStartPosition > endPosition)
            {
                fs.Close();
                return;
            }
            else if (writeStartPosition < startPosition)
            {
                fs.Close();
                return;
            }
            else if (writeStartPosition > startPosition && writeStartPosition < endPosition)
            {
                writeStartPosition = startPosition;
            }
            fs.Seek(writeStartPosition, System.IO.SeekOrigin.Current);
            //向文件追加文件流。
            byte[] nbytes = new byte[512];
            int nReadSize = 0;
            nReadSize = stream.Read(nbytes, 0, 512);
            while (nReadSize > 0)
            {
                fs.Write(nbytes, 0, nReadSize);
                nReadSize = stream.Read(nbytes, 0, 512);
            }
            fs.Close();
        }

    }
}
