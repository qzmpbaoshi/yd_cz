using System.Collections.Generic;
using System.Web.Http;
using SystemModel.DataModel;
using SystemModel.RequestResult;
using SystemModel.SearchConditionModel;
using WebAppService.DataAdapter;

namespace WebAppService.Controllers
{
    public class DriverController : ApiController
    {
        private IDriverAdapter DBAdapter { get; set; }
        public DriverController()
        {
            this.DBAdapter = new DriverAdapter();
        }
        [HttpPost]
        public RequestPagingResult<List<DriverInfoModel>> GetDrivers(PagingSearchCondition<DriverSearch> condition)
        {
            RequestPagingResult<List<DriverInfoModel>> rst = this.DBAdapter.GetDrivers(condition);
            return rst;
        }
        //新增
        public IHttpActionResult AddDrivers([FromBody]List<DriverInfoModel> addDrivers)
        {

            RequestEasyResult rsts = this.DBAdapter.AddDrivers(addDrivers);
            return Json<RequestEasyResult>(rsts);
        }
        //删除
        public IHttpActionResult DelDriversByIds([FromBody]List<string> ids)
        {

            RequestEasyResult rsts = this.DBAdapter.DelDriversByIds(ids);
            return Json<RequestEasyResult>(rsts);
        }
        //修改
        public IHttpActionResult UpdateDriver([FromBody]DriverInfoModel updateDriver)
        {

            RequestEasyResult rsts = this.DBAdapter.UpdateDriver(updateDriver);
            return Json<RequestEasyResult>(rsts);
        }
    }
}
