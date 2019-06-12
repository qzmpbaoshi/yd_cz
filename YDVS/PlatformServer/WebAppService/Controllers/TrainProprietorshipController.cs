using System.Collections.Generic;
using System.Web.Http;
using SystemModel.DataModel;
using SystemModel.RequestResult;
using SystemModel.SearchConditionModel;
using WebAppService.DataAdapter;

namespace WebAppService.Controllers
{
    public class TrainProprietorshipController : ApiController
    {
        private ITrainProprietorshipAdapter DBAdapter { get; set; }
        public TrainProprietorshipController()
        {
            this.DBAdapter = new TrainProprietorshipAdapter();
        }
        [HttpPost]
        public IHttpActionResult GetTrainProprietorships([FromBody]PagingSearchCondition<TrainProprietorshipSearch> condition)
        {
            //CommonLibrary.LogHelper.Log4Helper.Debug(this.GetType(), "机车配属段查询接口调用，参数：" + JsonConvert.SerializeObject(condition));
            RequestResult<List<TrainProprietorshipModel>> rsts = this.DBAdapter.GetTrainProprietorships(condition);
            //CommonLibrary.LogHelper.Log4Helper.Debug(this.GetType(), "机车配属段查询结果：" + JsonConvert.SerializeObject(rsts));
            return Json<RequestResult<List<TrainProprietorshipModel>>>(rsts);
        }
        //新增
        public IHttpActionResult AddTrainProprietorships([FromBody]List<TrainProprietorshipModel> addModels)
        {

            RequestEasyResult rsts = this.DBAdapter.AddTrainProprietorships(addModels);
            return Json<RequestEasyResult>(rsts);
         }
        //删除
        public IHttpActionResult DelTrainProprietorshipsByIds([FromBody]List<string> ids)
        {

            RequestEasyResult rsts = this.DBAdapter.DelTrainProprietorshipsByIds(ids);
            return Json<RequestEasyResult>(rsts);
        }
        //修改
        public IHttpActionResult UpdateTrainProprietorship([FromBody]TrainProprietorshipModel updateModel)
        {

            RequestEasyResult rsts = this.DBAdapter.UpdateTrainProprietorship(updateModel);
            return Json<RequestEasyResult>(rsts);
        }
    }
    }
