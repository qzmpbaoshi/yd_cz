using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemModel.DataModel;

namespace WebAppService.FileAdapter
{
    interface IFileAdapter
    {
        /// <summary>
        /// 获取子文件或文件夹
        /// </summary>
        /// <param name="path">相对文件夹路径</param>
        /// <param name="path">是否获取文件节点，默认为flase只获取文件夹</param>
        /// <returns></returns>
        List<FileTreeItemModel> GetFileTreeList(string path, bool isGetFile = false);
    }
}
