using DBHandlerForMySQL.YDVS;
using System;
using System.Collections.Generic;
using System.Linq;
using SystemModel;
using SystemModel.DataModel;
using SystemModel.RequestResult;
using SystemModel.SearchConditionModel;

namespace WebAppService.DataAdapter
{
    /// <summary>
    /// 司机信息数据库适配器
    /// </summary>
    public class DriverAdapter : IDriverAdapter
    {
        public bool AddDrivers(List<DriverInfoModel> addDrivers, User user = null)
        {
            try
            {
                using (ydvsEntities entities = new ydvsEntities())
                {

                }
                return true;
            }
            catch (Exception ex)
            {
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "添加司机信息", ex);
                return false;
            }
        }

        public bool DelDriversByIds(List<int> ids, User user = null)
        {
            try
            {
                using (ydvsEntities entities = new ydvsEntities())
                {

                }
                return true;
            }
            catch (Exception ex)
            {
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "删除司机信息", ex);
                return false;
            }
        }

        public bool DelDrivers(List<DriverInfoModel> addDrivers, User user = null)
        {
            try
            {
                using (ydvsEntities entities = new ydvsEntities())
                {

                }
                return true;
            }
            catch (Exception ex)
            {
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "删除司机信息", ex);
                return false;
            }
        }

        public RequestPagingResult<DriverInfoModel> GetDrivers(PagingSearchCondition<DriverSearch> condition)
        {
            RequestPagingResult<DriverInfoModel> rsts = new RequestPagingResult<DriverInfoModel>();
            try
            {
                using (ydvsEntities entities = new ydvsEntities())
                {
                    bool isCondition = condition == null || string.IsNullOrWhiteSpace(condition.SearchCondition.Card);
                    var temp = entities.base_driver_info.Where(d => true);
                    rsts.TotalCount = temp.Count();
                    if (!isCondition)
                    {
                        temp = temp.Where(d => isCondition || d.card.Contains(condition.SearchCondition.Card));
                    }
                    rsts.ResultDatas = temp.Select(d => new DriverInfoModel
                    {
                        Id = d.id,
                        Order = d.order,
                        Card = d.card,
                        Name = d.name,
                        LocomotiveDepot = d.locomotive_depot,
                        Team = d.team

                    }).OrderBy(d => d.Order).ToList();
                }
                return rsts;
            }
            catch (Exception ex)
            {
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "查找机车信息", ex);
                return rsts;
            }
        }
    }
}