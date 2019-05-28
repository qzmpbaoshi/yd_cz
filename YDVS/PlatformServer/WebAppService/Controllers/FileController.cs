using System.Collections.Generic;
using System.Web.Http;
using SystemModel.DataModel;
using SystemModel.SearchConditionModel;
using WebAppService.FileManageAdapter;

namespace WebAppService.Controllers
{
    public class FileController : ApiController
    {
        private IFileAdapter FileAdapter { get; set; }
        public FileController()
        {
            FileAdapter = new FileAdapter();
        }
        [HttpPost]
        public IHttpActionResult GetFileTreeItemList(FileSearchModel searchCondition)
        {
            return Json<List<FileTreeItemModel>>(FileAdapter.GetFileTreeItemList(searchCondition));
        }
    }
}
