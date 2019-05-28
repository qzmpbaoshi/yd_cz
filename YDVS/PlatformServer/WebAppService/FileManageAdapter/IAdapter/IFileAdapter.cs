using System.Collections.Generic;
using SystemModel.DataModel;
using SystemModel.SearchConditionModel;

namespace WebAppService.FileManageAdapter
{
    interface IFileAdapter
    {
        /// <summary>
        /// 获取子文件或文件夹
        /// </summary>
        /// <param name="searchCondition">查询条件；path：相对文件夹路径；isGetDic：是否获取文件夹节点；isGetFile：是否获取文件节点</param>
        /// <returns></returns>
        List<FileTreeItemModel> GetFileTreeItemList(FileSearchModel searchCondition);
    }
}
