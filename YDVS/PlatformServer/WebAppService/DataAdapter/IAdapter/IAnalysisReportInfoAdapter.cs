using System.Collections.Generic;
using SystemModel;
using SystemModel.DataModel;
using SystemModel.RequestResult;
using SystemModel.SearchConditionModel;
using SystemModel.SearchConditionModel.Search;

namespace WebAppService.DataAdapter
{
    interface IAnalysisReportInfoAdapter
    {
        /// <summary>
        /// 根据条件查询分析报告信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="user">当前登录人</param>
        /// <returns>分析报告信息列表</returns>
        RequestResult<List<AnalysisReportInfoModel>> GetAnalysisReportInfos(PagingSearchCondition<AnalysisReportInfoSearch> condition);
        /// <summary>
        /// 添加分析报告信息
        /// </summary>
        /// <param name="addInfo">添的信息</param>
        /// <param name="user">当前登录人</param>
        /// <returns>true添加成功；false添加失败；并返回添加成功的id</returns>
        RequestResult<string> AddAnalysisReportInfo(AnalysisReportInfoModel addModel, User user = null);
        /// <summary>
        /// 修改分析报告信息
        /// </summary>
        /// <param name="updateInfo">修改分析报告信息内容</param>
        /// <param name="user">当前登录人</param>
        /// <returns>true添加成功；false添加失败</returns>
        RequestEasyResult UpdateAnalysisReportInfo(AnalysisReportInfoModel updateModel, User user = null);
        /// <summary>
        /// 删除分析报告信息
        /// </summary>
        /// <param name="delInfos">删除列表</param>
        /// <param name="user">当前登录人</param>
        /// <returns>true删除成功；false删除失败</returns>
        RequestEasyResult DelAnalysisReportInfos(List<AnalysisReportInfoModel> delModels, User user = null);
        /// <summary>
        /// 根据ID列表，删除分析报告信息
        /// </summary>
        /// <param name="ids">删除id列表</param>
        /// <param name="user">当前登录人</param>
        /// <returns>true删除成功；false删除失败</returns>
        RequestEasyResult DelAnalysisReportInfosByIds(List<string> ids, User user = null);

        /// <summary>
        /// 根据ID，删除分析报告信息
        /// </summary>
        /// <param name="ids">删除id</param>
        /// <param name="user">当前登录人</param>
        /// <returns>true删除成功；false删除失败</returns>
        RequestEasyResult DelAnalysisReportInfosById(string id, User user = null);
    }
}
