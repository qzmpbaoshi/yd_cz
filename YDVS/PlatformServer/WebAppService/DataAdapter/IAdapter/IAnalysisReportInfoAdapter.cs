using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemModel.RequestResult;

namespace WebAppService.DataAdapter.IAdapter
{
    interface IAnalysisReportInfoAdapter
    {
        /// <summary>
        /// 根据条件查询司机信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="user">当前登录人</param>
        /// <returns>司机信息列表</returns>
        RequestPagingResult<AnalysisReportInfoModel> GetDrivers(PagingSearchCondition<DriverSearch> condition);
        /// <summary>
        /// 添加司机信息
        /// </summary>
        /// <param name="addDrivers">添加列表</param>
        /// <param name="user">当前登录人</param>
        /// <returns>true添加成功；false添加失败</returns>
        RequestEasyResult AddDrivers(List<AnalysisReportInfoModel> addDrivers, User user = null);
        /// <summary>
        /// 修改司机信息
        /// </summary>
        /// <param name="updateDriver">修改机车内容</param>
        /// <param name="user">当前登录人</param>
        /// <returns>true添加成功；false添加失败</returns>
        RequestEasyResult UpdateDriver(AnalysisReportInfoModel updateDriver, User user = null);
        /// <summary>
        /// 删除司机信息
        /// </summary>
        /// <param name="delDrivers">删除列表</param>
        /// <param name="user">当前登录人</param>
        /// <returns>true删除成功；false删除失败</returns>
        RequestEasyResult DelDrivers(List<AnalysisReportInfoModel> delDrivers, User user = null);
        /// <summary>
        /// 根据ID，删除司机信息
        /// </summary>
        /// <param name="ids">删除id列表</param>
        /// <param name="user">当前登录人</param>
        /// <returns>true删除成功；false删除失败</returns>
        RequestEasyResult DelDriversByIds(List<string> ids, User user = null);
    }
}
