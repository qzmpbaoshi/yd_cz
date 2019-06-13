using System.IO;
using System.Web.Http;
using SystemModel.DataModel;
using SystemModel.RequestResult;
using WebAppService.DataAdapter;

namespace WebAppService.Controllers
{
    public class AnalysisReportInfoController : ApiController
    {
        private IAnalysisReportInfoAdapter DBAdapter { get; set; }
        public AnalysisReportInfoController()
        {
            this.DBAdapter = new AnalysisReportInfoAdapter();
        }
        /// <summary>
        /// 添加方法
        /// </summary>
        /// <param name="addModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddAnalysisReportInfo([FromBody]AnalysisReportInfoModel addModel)
        {
            RequestResult<string> rst = this.DBAdapter.AddAnalysisReportInfo(addModel);
            return Json<RequestResult<string>>(rst);
        }

        /// <summary>
        /// 删除方法
        /// </summary>
        /// <param name="delId"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult DelAnalysisReportInfo([FromBody]DelAnalysisReportInfoModel delModel)
        {
            RequestEasyResult rst = new RequestEasyResult();
            if (!string.IsNullOrWhiteSpace(delModel.AnalysisReportInfoId))
                rst = this.DBAdapter.DelAnalysisReportInfosById(delModel.AnalysisReportInfoId);
            if (string.IsNullOrWhiteSpace(delModel.RelativePath)) return Json<RequestEasyResult>(rst); ;
            string dirPath = ConfigerHelper.GetBaseAttPath() + delModel.RelativePath;
            if (!string.IsNullOrWhiteSpace(dirPath))
            {
                if (Directory.Exists(dirPath))
                    Directory.Delete(dirPath, true);
            }
            return Json<RequestEasyResult>(rst);
        }
    }
}
