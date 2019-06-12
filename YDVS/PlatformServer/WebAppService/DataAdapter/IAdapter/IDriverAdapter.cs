using System.Collections.Generic;
using SystemModel;
using SystemModel.DataModel;
using SystemModel.RequestResult;
using SystemModel.SearchConditionModel;

namespace WebAppService.DataAdapter
{
    interface IDriverAdapter
    {
        /// <summary>
        /// 根据条件查询司机信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="user">当前登录人</param>
        /// <returns>司机信息列表</returns>
        RequestResult<List<DriverInfoModel>> GetDrivers(PagingSearchCondition<DriverSearch> condition);
        /// <summary>
        /// 添加司机信息
        /// </summary>
        /// <param name="addDrivers">添加列表</param>
        /// <param name="user">当前登录人</param>
        /// <returns>true添加成功；false添加失败</returns>
        RequestEasyResult AddDrivers(List<DriverInfoModel> addDrivers, User user = null);
        /// <summary>
        /// 修改司机信息
        /// </summary>
        /// <param name="updateDriver">修改机车内容</param>
        /// <param name="user">当前登录人</param>
        /// <returns>true添加成功；false添加失败</returns>
        RequestEasyResult UpdateDriver(DriverInfoModel updateDriver, User user = null);
        /// <summary>
        /// 删除司机信息
        /// </summary>
        /// <param name="delDrivers">删除列表</param>
        /// <param name="user">当前登录人</param>
        /// <returns>true删除成功；false删除失败</returns>
        RequestEasyResult DelDrivers(List<DriverInfoModel> delDrivers, User user = null);
        /// <summary>
        /// 根据ID，删除司机信息
        /// </summary>
        /// <param name="ids">删除id列表</param>
        /// <param name="user">当前登录人</param>
        /// <returns>true删除成功；false删除失败</returns>
        RequestEasyResult DelDriversByIds(List<string> ids, User user = null);
    }
}
