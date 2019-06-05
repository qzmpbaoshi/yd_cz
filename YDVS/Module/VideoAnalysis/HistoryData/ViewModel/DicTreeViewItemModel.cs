using System.Collections.Generic;

namespace VideoAnalysis.HistoryData.ViewModel
{
    public class DirTreeViewItemModel
    {
        /// <summary>
        /// 文件或文件夹全路径
        /// </summary>
        public string FullPathName { get; set; }
        /// <summary>
        /// 文件或文件夹名字
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 是否是子节点
        /// </summary>
        public bool IsBreaf { get; set; }
        /// <summary>
        /// 是否是文件夹
        /// </summary>
        public bool IsVideoFolder { get; set; }
        /// <summary>
        /// 子文件或文件夹
        /// </summary>
        public List<DirTreeViewItemModel> Children { get; set; }
    }
}
