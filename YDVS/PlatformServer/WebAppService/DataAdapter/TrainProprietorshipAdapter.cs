using DBHandlerForMySQL.YDVS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        public bool AddTrainProprietorships(List<TrainProprietorshipModel> addDrivers, User user = null)
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
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "添加机车配属段信息", ex);
                return false;
            }
        }

        public bool DelTrainProprietorships(List<TrainProprietorshipModel> delDrivers, User user = null)
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
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "删除机车配属段信息", ex);
                return false;
            }
        }

        public bool DelTrainProprietorshipsByIds(List<int> ids, User user = null)
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
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "删除机车配属段信息", ex);
                return false;
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
                        WorkShop = d.work_shop

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