using System.Collections.Generic;
using SystemModel;
using SystemModel.DataModel;
using SystemModel.RequestResult;

namespace WebAppService.DataAdapter
{
    interface IAnalysisReportAttachmentAdapter
    {
        /// <summary>
        /// 根据条件查询分析报告信息附件
        /// </summary>
        /// <param name="aReportInfoId">外键，分析报告id</param>
        /// <param name="user">当前登录人</param>
        /// <returns>分析报告附件信息列表</returns>
        RequestResult<List<AnalysisReportAttachmentModel>> GetAnalysisReportAttachmentsByPkey(string aReportInfoId);
        /// <summary>
        /// 添加分析报告信息
        /// </summary>
        /// <param name="addInfo">添的信息</param>
        /// <param name="user">当前登录人</param>
        /// <returns>true添加成功；false添加失败</returns>
        RequestEasyResult AddAnalysisReportAttachments(List<AnalysisReportAttachmentModel> addModels,string basePath, User user = null);
        /// <summary>
        /// 删除分析报告附件信息
        /// </summary>
        /// <param name="aReportInfoId">外键，分析报告id</param>
        /// <param name="user">当前登录人</param>
        /// <returns>true添加成功；false添加失败</returns>
        RequestEasyResult DelAnalysisReportAttachmentsByPkey(string aReportInfoId, User user = null);

    }
}
