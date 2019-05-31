using DBHandlerForMySQL.YDVS;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
        public RequestEasyResult AddDrivers(List<DriverInfoModel> addDrivers, User user = null)
        {
            RequestEasyResult rst = new RequestEasyResult();
            try
            {
                if (null == addDrivers || 0 == addDrivers.Count)
                {
                    return rst;
                }
                using (ydvsEntities entities = new ydvsEntities())
                {
                    List<base_driver_info> addDriverInfos = new List<base_driver_info>();
                    foreach (var driver in addDrivers)
                    {
                        addDriverInfos.Add(new base_driver_info
                        {
                            id = Guid.NewGuid().ToString(),
                            card = driver.Card,
                            name = driver.Name,
                            team = driver.Team,
                            locomotive_depot = driver.LocomotiveDepot,
                            order = driver.Order,
                            creat_time = DateTime.Now
                        });
                    }
                    entities.base_driver_info.AddOrUpdate(addDriverInfos.ToArray());
                    entities.SaveChanges();
                }
                rst.Flag = true;
                rst.Msg = "添加成功！";
                return rst;
            }
            catch (Exception ex)
            {
                rst.Flag = false;
                rst.Msg = "添加失败！" + ex.ToString();
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "删除机车配属段信息", ex);
                return rst;
            }
        }

        public RequestEasyResult UpdateDriver(DriverInfoModel updateDriver, User user = null)
        {
            RequestEasyResult rst = new RequestEasyResult();
            try
            {
                
                using (ydvsEntities entities = new ydvsEntities())
                {
                    base_driver_info tp = entities.base_driver_info.FirstOrDefault(t => t.id == updateDriver.Id);
                    if (tp != null)
                    {
                        tp.card = updateDriver.Card;
                        tp.name = updateDriver.Name;
                        tp.team = updateDriver.Team;
                        tp.locomotive_depot = updateDriver.LocomotiveDepot;
                        tp.order = updateDriver.Order;
                        tp.update_time=DateTime.Now;
                        entities.base_driver_info.AddOrUpdate(tp);
                    }
                    entities.SaveChanges();
                }
                rst.Flag = true;
                rst.Msg = "修改成功！";
                return rst;
            }
            catch (Exception ex)
            {
                rst.Flag = false;
                rst.Msg = "修改失败！" + ex.ToString();
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "删除机车配属段信息", ex);
                return rst;
            }
        }

        public RequestEasyResult DelDrivers(List<DriverInfoModel> addDrivers, User user = null)
        {
            RequestEasyResult rst = new RequestEasyResult();
            try
            {
                
                using (ydvsEntities entities = new ydvsEntities())
                {

                }
                rst.Flag = true;
                rst.Msg = "删除成功！";
                return rst;
            }
            catch (Exception ex)
            {
                rst.Flag = false;
                rst.Msg = "删除失败！" + ex.ToString();
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "删除机车配属段信息", ex);
                return rst;
            }
        }

        public RequestEasyResult DelDriversByIds(List<string> ids, User user = null)
        {
            RequestEasyResult rst = new RequestEasyResult();
            try
            {
                
                using (ydvsEntities entities = new ydvsEntities())
                {
                    foreach (string delId in ids)
                    {
                        var tp = entities.base_driver_info.Where(t => t.id == delId).FirstOrDefault();
                        if (tp != null)
                            entities.base_driver_info.Remove(tp);
                    }
                    entities.SaveChanges();
                }
                rst.Flag = true;
                rst.Msg = "删除成功！";
                return rst;
            }
            catch (Exception ex)
            {
                rst.Flag = false;
                rst.Msg = "删除失败！" + ex.ToString();
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "删除机车配属段信息", ex);
                return rst;
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