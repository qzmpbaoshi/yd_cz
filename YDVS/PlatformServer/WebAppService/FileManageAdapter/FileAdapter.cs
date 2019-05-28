using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using SystemModel.DataModel;
using SystemModel.SearchConditionModel;

namespace WebAppService.FileManageAdapter
{
    public class FileAdapter : IFileAdapter
    {
        public List<FileTreeItemModel> GetFileTreeItemList(FileSearchModel searchCondition)
        {
            try
            {
                if (searchCondition == null) return null;
                List<FileTreeItemModel> dList = new List<FileTreeItemModel>();
                string tempPath = ConfigurationManager.AppSettings["FileBasePath"];
                string exPath = searchCondition.Path;
                exPath = string.IsNullOrWhiteSpace(exPath) ? tempPath : tempPath + exPath;
                //path = StringToFilePath(path);
                if (searchCondition.isGetDic)
                {
                    string[] directorys = Directory.GetDirectories(exPath);
                    if (directorys != null && directorys.Count() > 0)
                    {
                        FileTreeItemModel dt = null;
                        foreach (string d in directorys)
                        {
                            dt = new FileTreeItemModel();
                            dt.IDX = "";
                            dt.Name = Path.GetFileName(d);
                            dt.FullPathName = d.Substring(tempPath.Length);
                            dt.IsBreaf = false;
                            dt.IsFolder = true;
                            dList.Add(dt);
                        }
                    }
                }
                if (searchCondition.isGetFile)
                {
                    string[] files = Directory.GetFiles(exPath);
                    if (files != null && files.Count() > 0)
                    {
                        FileTreeItemModel dt = null;
                        foreach (string f in files)
                        {
                            dt = new FileTreeItemModel();
                            dt.IDX = "";
                            dt.Name = Path.GetFileName(f);
                            dt.FullPathName = f.Substring(tempPath.Length); ;
                            dt.IsBreaf = true;
                            dt.IsFolder = false;
                            dList.Add(dt);
                        }
                    }
                }
                return dList;
            }
            catch (Exception ex)
            {
                CommonLibrary.LogHelper.Log4Helper.Error(this.GetType(), "获取文件树节点", ex);
                return null;
            }
        }
    }
}