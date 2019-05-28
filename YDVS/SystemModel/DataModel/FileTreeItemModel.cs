using System.Collections.Generic;

namespace SystemModel.DataModel
{
    /// <summary>
    /// 文件夹
    /// </summary>
    public class FileTreeItemModel
    {
        public string IDX { get; set; }
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
        public bool IsFolder { get; set; }
        /// <summary>
        /// 子文件或文件夹
        /// </summary>
        public List<FileTreeItemModel> Children { get; set; }
    }
}
