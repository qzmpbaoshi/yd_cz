using System.Collections.Generic;
using SystemModel;
using SystemModel.DataModel;
using SystemModel.RequestResult;
using SystemModel.SearchConditionModel;

namespace WebAppService.DataAdapter
{
    interface ITrainProprietorshipAdapter
    {
        /// <summary>
        /// 根据条件查询机车配属段信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="user">当前登录人</param>
        /// <returns>司机信息列表</returns>
        RequestResult<List<TrainProprietorshipModel>> GetTrainProprietorships(PagingSearchCondition<TrainProprietorshipSearch> condition);
        /// <summary>
        /// 添加机车配属段信息
        /// </summary>
        /// <param name="addModels">添加列表</param>
        /// <param name="user">当前登录人</param>
        /// <returns>true添加成功；false添加失败</returns>
        RequestEasyResult AddTrainProprietorships(List<TrainProprietorshipModel> addModels, User user = null);
        /// <summary>
        /// 删除机车配属段信息
        /// </summary>
        /// <param name="delModels">删除列表</param>
        /// <param name="user">当前登录人</param>
        /// <returns>true删除成功；false删除失败</returns>
        RequestEasyResult DelTrainProprietorships(List<TrainProprietorshipModel> delModels, User user = null);
        /// <summary>
        /// 根据ID，删除机车配属段信息
        /// </summary>
        /// <param name="ids">删除id列表</param>
        /// <param name="user">当前登录人</param>
        /// <returns>true删除成功；false删除失败</returns>
        RequestEasyResult DelTrainProprietorshipsByIds(List<string> ids, User user = null);
        /// <summary>
        /// 修改机车配属段信息
        /// </summary>
        /// <param name="updateModel">修改内容</param>
        /// <param name="user">当前登录人</param>
        /// <returns></returns>
        RequestEasyResult UpdateTrainProprietorship(TrainProprietorshipModel updateModel, User user = null);
    }
}
