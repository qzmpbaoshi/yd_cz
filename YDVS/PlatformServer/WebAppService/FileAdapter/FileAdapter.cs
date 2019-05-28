using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using SystemModel.DataModel;

namespace WebAppService.FileAdapter
{
    public class FileAdapter : IFileAdapter
    {
        public List<FileTreeItemModel> GetFileTreeList(string path, bool isGetDic = true, bool isGetFile = false)
        {
            try
            {
                List<FileTreeItemModel> dList = new List<FileTreeItemModel>();
                string tempPath = ConfigurationManager.AppSettings["Root"];
                path = string.IsNullOrWhiteSpace(path) ? tempPath : tempPath + path;
                //path = StringToFilePath(path);
                if (isGetDic)
                {
                    string[] directorys = Directory.GetDirectories(path);
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
                if (isGetFile)
                {
                    string[] files = Directory.GetFiles(path);
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