using DBHandlerForMySQL.YDVS;
using System;
using System.Collections.Generic;
using System.Linq;
using SystemModel;
using SystemModel.DataModel;
using SystemModel.RequestResult;
using SystemModel.SearchConditionModel;
using SystemModel.SearchConditionModel.Search;

namespace WebAppService.DataAdapter
{
    public class AnalysisReportInfoAdapter : IAnalysisReportInfoAdapter
    {
        public RequestResult<string> AddAnalysisReportInfo(AnalysisReportInfoModel addModel, User user = null)
        {
            RequestResult<string> rst = new RequestResult<string>();
            try
            {

                if (null == addModel)
                {
                    rst.Flag = false;
                    rst.ResultData = "";
                    rst.Msg = "无添加信息！";
                    return rst;
                }
                using (ydvsEntities entities = new ydvsEntities())
                {
                    analysis_report_info addInfoEntity = new analysis_report_info
                    {
                        id = Guid.NewGuid().ToString(),
                        train_type = addModel.TrainType,
                        train_no = addModel.TrainNo,
                        train_short_name = addModel.TrainShortName,
                        driver_name = addModel.DriverName,
                        driver_num = addModel.DriverNum,
                        ass_driver_name = addModel.AssDriverName,
                        ass_driver_num = addModel.AssDriverNum,
                        event_time = addModel.EventTime,
                        event_data_json_str = addModel.EventDataJsonStr,
                        analysis_time = addModel.AnalysisTime,
                        analysis_person = addModel.AnalysisPerson,
                        analysis_content = addModel.AnalysisContent,
                        creator = "",
                        creat_time = DateTime.Now
                    };
                    entities.analysis_report_info.Add(addInfoEntity);
                    entities.SaveChanges();
                    rst.ResultData = addInfoEntity.id;
                }
                rst.Flag = true;
                rst.Msg = "添加成功！";
                return rst;
            }
            catch (Exception ex)
            {
                rst.Flag = false;
                rst.Msg = "添加失败！" + ex.ToString();
                rst.ResultData = "";
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "删除机车配属段信息", ex);
                return rst;
            }
        }

        public RequestEasyResult DelAnalysisReportInfos(List<AnalysisReportInfoModel> delModels, User user = null)
        {
            throw new NotImplementedException();
        }

        public RequestEasyResult DelAnalysisReportInfosById(string id, User user = null)
        {
            RequestEasyResult rst = new RequestEasyResult();
            try
            {
                using (ydvsEntities entities = new ydvsEntities())
                {
                    var aReport = entities.analysis_report_info.Where(t => t.id == id).FirstOrDefault();
                    if (aReport != null)
                    {
                        entities.analysis_report_info.Remove(aReport);
                        entities.analysis_report_attachment.RemoveRange(entities.analysis_report_attachment.Where(att => att.analysis_report_info_id == id));
                        entities.SaveChanges();
                    }
                }
                rst.Flag = true;
                rst.Msg = "删除成功！";
            }
            catch (Exception ex)
            {
                rst.Flag = false;
                rst.Msg = "删除失败！" + ex.ToString();
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "删除机车配属段信息", ex);
            }
            return rst;
        }

        public RequestEasyResult DelAnalysisReportInfosByIds(List<string> ids, User user = null)
        {
            throw new NotImplementedException();
        }

        public RequestResult<List<AnalysisReportInfoModel>> GetAnalysisReportInfos(PagingSearchCondition<AnalysisReportInfoSearch> condition)
        {
            throw new NotImplementedException();
        }

        public RequestEasyResult UpdateAnalysisReportInfo(AnalysisReportInfoModel updateModel, User user = null)
        {
            throw new NotImplementedException();
        }
    }
}