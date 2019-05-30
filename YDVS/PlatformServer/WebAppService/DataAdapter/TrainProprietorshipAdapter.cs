using DBHandlerForMySQL.YDVS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using SystemModel;
using SystemModel.DataModel;
using SystemModel.RequestResult;
using SystemModel.SearchConditionModel;
using WebAppService.DataAdapter.IAdapter;

namespace WebAppService.DataAdapter
{
    public class TrainProprietorshipAdapter : ITrainProprietorshipAdapter
    {
        public RequestEasyResult AddTrainProprietorships(List<TrainProprietorshipModel> addTrains, User user = null)
        {
            RequestEasyResult rst = new RequestEasyResult();
            try
            {
                rst.Flag = true;
                rst.Msg = "添加成功！";
                using (ydvsEntities entities = new ydvsEntities())
                {
                    foreach (var train in addTrains)
                    {
                        entities.base_train_proprietorship.Add(new base_train_proprietorship
                        {
                            id = Guid.NewGuid().ToString(),
                            train_type = train.TrainType,
                            train_no = train.TrainNo,
                            work_shop = train.WorkShop,
                            locomotive_depot = train.LocomotiveDepot,
                            order = train.Order,
                        });
                    }
                    entities.SaveChanges();
                }
                return rst;
            }
            catch (Exception ex)
            {
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "添加机车配属段信息", ex);
                return rst;
            }
        }

        public RequestEasyResult DelTrainProprietorships(List<TrainProprietorshipModel> delDrivers, User user = null)
        {
            RequestEasyResult rst = new RequestEasyResult();
            try
            {
                using (ydvsEntities entities = new ydvsEntities())
                {

                }
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

        public RequestEasyResult DelTrainProprietorshipsByIds(List<string> ids, User user = null)
        {
            RequestEasyResult rst = new RequestEasyResult();
            try
            {
                rst.Flag = true;
                rst.Msg = "删除成功！";
                using (ydvsEntities entities = new ydvsEntities())
                {
                    foreach (string delId in ids)
                    {
                        var tp = entities.base_train_proprietorship.Where(t => t.id == delId).FirstOrDefault();
                        if (tp != null)
                            entities.base_train_proprietorship.Remove(tp);
                    }
                    entities.SaveChanges();
                }
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

        public RequestEasyResult UpdateTrainProprietorship(TrainProprietorshipModel updateModel, User user = null)
        {
            RequestEasyResult rst = new RequestEasyResult();
            try
            {
                rst.Flag = true;
                rst.Msg = "修改成功！";
                using (ydvsEntities entities = new ydvsEntities())
                {
                    base_train_proprietorship tp = entities.base_train_proprietorship.FirstOrDefault(t => t.id == updateModel.Id);
                    if (tp != null)
                    {
                        tp.train_type = updateModel.TrainType;
                        tp.train_no = updateModel.TrainNo;
                        tp.work_shop = updateModel.WorkShop;
                        tp.locomotive_depot = updateModel.LocomotiveDepot;
                        tp.order = updateModel.Order;
                        entities.base_train_proprietorship.AddOrUpdate(tp);
                    }
                }
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

        public RequestPagingResult<TrainProprietorshipModel> GetTrainProprietorships(PagingSearchCondition<TrainProprietorshipSearch> condition)
        {
            RequestPagingResult<TrainProprietorshipModel> rsts = new RequestPagingResult<TrainProprietorshipModel>();
            try
            {
                rsts.Flag = true;
                using (ydvsEntities entities = new ydvsEntities())
                {
                    bool isCondition = condition == null || string.IsNullOrWhiteSpace(condition.SearchCondition.TrainShortName);
                    var temp = entities.base_train_proprietorship.Where(d => true);
                    if (!isCondition)
                    {
                        temp = temp.Where(d => isCondition || d.train_type.Contains(condition.SearchCondition.TrainShortName) || d.train_no.Contains(condition.SearchCondition.TrainShortName));
                    }
                    rsts.TotalCount = temp.Count();
                    int startIndex = condition.StartIndex <= 0 ? 0 : (condition.StartIndex - 1);
                    int pageSize = condition.PageSize <= 0 ? rsts.TotalCount : condition.PageSize;
                    var temp2 = temp.Select(d => new TrainProprietorshipModel
                    {
                        Id = d.id,
                        Order = d.order,
                        TrainType = d.train_type,
                        TrainNo = d.train_no,
                        LocomotiveDepot = d.locomotive_depot,
                        WorkShop = d.work_shop,
                        RailwayAdministration = d.railway_administration

                    }).OrderBy(d => d.Order).Skip(startIndex * condition.PageSize);
                    rsts.ResultDatas = pageSize == 0 ? temp2.ToList() : temp2.Take(pageSize).ToList();
                }
                rsts.Msg = "";
                return rsts;
            }
            catch (Exception ex)
            {
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "查找机车配属段信息", ex);
                rsts.Flag = false;
                rsts.Msg = ex.ToString();
                return rsts;
            }
        }
    }
}