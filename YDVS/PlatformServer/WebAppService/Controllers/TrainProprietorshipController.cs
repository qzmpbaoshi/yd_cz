using Newtonsoft.Json;
using System.Web.Http;
using SystemModel.DataModel;
using SystemModel.RequestResult;
using SystemModel.SearchConditionModel;
using WebAppService.DataAdapter;
using WebAppService.DataAdapter.IAdapter;

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
            RequestPagingResult<TrainProprietorshipModel> rsts = this.DBAdapter.GetTrainProprietorships(condition);
            //CommonLibrary.LogHelper.Log4Helper.Debug(this.GetType(), "机车配属段查询结果：" + JsonConvert.SerializeObject(rsts));
            return Json<RequestPagingResult<TrainProprietorshipModel>>(rsts);
        }
    }
}
