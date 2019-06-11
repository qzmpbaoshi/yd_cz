using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemModel.DataModel
{
    public class AnalysisReportAttachmentModel
    {
        private string _id;
        private string _relativePath;
        private string _fileName;
        private string _fileExtension;
        private string _fileFromChannel;
        private string _fileFromChannelName;
        private string _analysisReportInfoId;
        /// <summary>
        /// 主键
        /// </summary>
        public string Id
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
            }
        }
        /// <summary>
        /// 相对路径
        /// </summary>
        public string RelativePath
        {
            get
            {
                return _relativePath;
            }

            set
            {
                _relativePath = value;
            }
        }
        /// <summary>
        /// 附件名称
        /// </summary>
        public string FileName
        {
            get
            {
                return _fileName;
            }

            set
            {
                _fileName = value;
            }
        }
        /// <summary>
        /// 附件扩展名
        /// </summary>
        public string FileExtension
        {
            get
            {
                return _fileExtension;
            }

            set
            {
                _fileExtension = value;
            }
        }
        /// <summary>
        /// 附件产生通道
        /// </summary>
        public string FileFromChannel
        {
            get
            {
                return _fileFromChannel;
            }

            set
            {
                _fileFromChannel = value;
            }
        }
        /// <summary>
        /// 附件产生通道名称
        /// </summary>
        public string FileFromChannelName
        {
            get
            {
                return _fileFromChannelName;
            }

            set
            {
                _fileFromChannelName = value;
            }
        }
        /// <summary>
        /// 附件关联的分析报告ID
        /// </summary>
        public string AnalysisReportInfoId
        {
            get
            {
                return _analysisReportInfoId;
            }

            set
            {
                _analysisReportInfoId = value;
            }
        }
    }
}
