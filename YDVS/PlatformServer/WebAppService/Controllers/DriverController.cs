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
        public RequestPagingResult<DriverInfoModel> GetDrivers(PagingSearchCondition<DriverSearch> condition)
        {
            RequestPagingResult<DriverInfoModel> rst = this.DBAdapter.GetDrivers(condition);
            return rst;
        }
    }
}
