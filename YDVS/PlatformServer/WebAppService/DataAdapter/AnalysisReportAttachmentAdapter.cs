using DBHandlerForMySQL.YDVS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Migrations;
using SystemModel;
using SystemModel.DataModel;
using SystemModel.RequestResult;

namespace WebAppService.DataAdapter
{
    public class AnalysisReportAttachmentAdapter : IAnalysisReportAttachmentAdapter
    {
        public RequestEasyResult AddAnalysisReportAttachments(List<AnalysisReportAttachmentModel> addModels, string basePath, User user = null)
        {
            RequestEasyResult rst = new RequestEasyResult();
            try
            {
                if (addModels == null || addModels.Count() == 0) return rst;
                using (ydvsEntities entities = new ydvsEntities())
                {
                    List<analysis_report_attachment> addEntities = new List<analysis_report_attachment>();
                    foreach (var addModel in addModels)
                    {
                        addEntities.Add(new analysis_report_attachment
                        {
                            id = Guid.NewGuid().ToString(),
                            base_path = basePath,
                            analysis_report_info_id = addModel.AnalysisReportInfoId,
                            file_extension = addModel.FileExtension,
                            file_from_channel = addModel.FileFromChannel,
                            file_from_channel_name = addModel.FileFromChannelName,
                            file_name = addModel.FileName,
                            relative_path = addModel.RelativePath,
                            creat_time = DateTime.Now
                        });
                    }
                    entities.analysis_report_attachment.AddOrUpdate(addEntities.ToArray());
                    entities.SaveChanges();
                }
                rst.Flag = true;
                rst.Msg = "添加成功！";
                return rst;
            }
            catch (Exception ex)
            {
                rst.Flag = false;
                rst.Msg = "添加失败！";
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "添加分析报告附件信息", ex);
                return rst;
            }
        }

        public RequestEasyResult DelAnalysisReportAttachmentsByPkey(string aReportInfoId, User user = null)
        {
            RequestEasyResult rst = new RequestEasyResult();
            try
            {
                if (string.IsNullOrWhiteSpace(aReportInfoId)) return rst;
                using (ydvsEntities entities = new ydvsEntities())
                {
                    entities.analysis_report_attachment.RemoveRange(entities.analysis_report_attachment.Where(att => att.analysis_report_info_id == aReportInfoId));
                    entities.SaveChanges();
                }
                rst.Flag = true;
                rst.Msg = "删除成功！";
                return rst;
            }
            catch (Exception ex)
            {
                rst.Flag = false;
                rst.Msg = "删除失败！";
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "删除分析报告附件信息", ex);
                return rst;
            }
        }

        public RequestResult<List<AnalysisReportAttachmentModel>> GetAnalysisReportAttachmentsByPkey(string aReportInfoId)
        {
            RequestResult<List<AnalysisReportAttachmentModel>> rst = new RequestResult<List<AnalysisReportAttachmentModel>>();
            try
            {
                rst.Flag = true;
                using (ydvsEntities entities = new ydvsEntities())
                {
                    rst.ResultData = entities.analysis_report_attachment.Where(att => att.analysis_report_info_id == aReportInfoId)
                        .Select(att => new AnalysisReportAttachmentModel
                        {
                            AnalysisReportInfoId = att.analysis_report_info_id,
                            FileExtension = att.file_extension,
                            FileFromChannel = att.file_from_channel,
                            FileFromChannelName = att.file_from_channel_name,
                            FileName = att.file_name,
                            Id = att.id,
                            RelativePath = att.relative_path
                        }).ToList();
                }
                rst.Msg = "";
                return rst;
            }
            catch (Exception ex)
            {
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "查找分析报告附件信息", ex);
                rst.Flag = false;
                rst.Msg = ex.ToString();
                return rst;
            }
        }

    }
}