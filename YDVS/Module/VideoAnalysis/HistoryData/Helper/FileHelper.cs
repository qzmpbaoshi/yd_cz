using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemModel.DataModel;
using SystemModel.SearchConditionModel;
using VideoAnalysis.HistoryData.ViewModel;

namespace VideoAnalysis.HistoryData.Helper
{
    public class FileHelper
    {
        /// <summary>
        /// 获取电脑磁盘信息
        /// </summary>
        /// <param name="dType">磁盘类型，默认为所有类型</param>
        /// <returns>磁盘信息列表，其中DriveInfo：【Name ： 盘符 ,例如："C:\"；TotalFreeSpace: 返回磁盘可用空间，返回值类型long。；DriveType : 磁盘类型 返回值如下：CDRom(光驱)、Fixed(固定磁盘)、Unknown(未知磁盘)、Network(网络磁盘)、NoRootDirectory(盘符不存在)、Ram(虚拟磁盘)、Removable(可移动磁盘)。；IsReady : 获取一个指示驱动器是否已准备好的值 返回bool类型。；RootDirectory : 获取驱动器根目录。；TotalSize : 空间总大小。；VolumeLabel : 获取驱动器卷标，返回string类型。；DriveFormat : 获取文件系统的名称，例如 NTFS 或 FAT32】</returns>
        public static DriveInfo[] GetDriveInfos(DriveType dType = DriveType.Unknown)
        {
            //检索计算机上的所有逻辑驱动器名称
            DriveInfo[] drives = DriveInfo.GetDrives().Where(d => d.IsReady).ToArray();
            if (drives != null)
            {
                drives = dType == DriveType.Unknown ? drives : drives.Where(d => d.DriveType == dType).ToArray();
            }
            return drives;
        }

        /// <summary>
        /// 获取指定路径下的文件夹
        /// </summary>
        /// <param name="basePath">指定的路径</param>
        /// <param name="IsVideoFolder">是否为视频文件夹，若若父文件夹为视频文件夹，则其子文件夹均为视频文件夹</param>
        /// <returns></returns>
        public static List<DirTreeViewItemModel> GetVideoDirTreeItemList(string basePath, bool IsVideoFolder = false)
        {
            try
            {
                List<DirTreeViewItemModel> dList = new List<DirTreeViewItemModel>();
                string[] directorys = Directory.GetDirectories(basePath);
                if (directorys != null && directorys.Count() > 0)
                {
                    DirTreeViewItemModel dt = null;
                    foreach (string d in directorys)
                    {
                        if (Directory.GetAccessControl(d).AreAccessRulesProtected)
                            continue;
                        dt = new DirTreeViewItemModel();
                        dt.Name = Path.GetFileName(d);
                        dt.FullPathName = d;
                        dt.IsVideoFolder = IsVideoFolder;
                        dt.IsBreaf = false;
                        dt.Children = null;
                        dList.Add(dt);
                    }
                }
                return dList;
            }
            catch (Exception ex)
            {
                CommonLibrary.LogHelper.Log4Helper.Error(typeof(FileHelper), "获取文件树节点", ex);
                return null;
            }
        }

        public static List<VideoSource> GetVideoSourceList(string videoDirPath)
        {
            try
            {
                List<VideoSource> vfList = new List<VideoSource>();
                string[] Files = Directory.GetFiles(videoDirPath);
                if (Files != null && Files.Count() > 0)
                {
                    VideoSource vf = null;
                    foreach (string f in Files)
                    {
                        vf = new VideoSource();
                        vf.Name = Path.GetFileName(f);
                        if (!AnalysisVideoName(ref vf))
                            continue;
                        vf.FullPathName = f;
                        vfList.Add(vf);
                    }
                }
                return vfList;
            }
            catch (Exception ex)
            {
                CommonLibrary.LogHelper.Log4Helper.Error(typeof(FileHelper), "获取文件树节点", ex);
                return null;
            }
        }
        private static bool AnalysisVideoName(ref VideoSource vf)
        {
            try
            {
                string vName = vf.Name;
                string[] subNames = vName.Split('_');
                if (subNames.Count() < 6) return false;
                vf.TrainShortName = subNames[0];
                vf.VideoFromSource = subNames[1];
                vf.VideoChannel = Convert.ToInt32(subNames[2]);
                vf.VideoChannelName = subNames[3];
                StringBuilder timeSB = new StringBuilder();
                timeSB.Append(subNames[4].Substring(0, 4) + "-");
                timeSB.Append(subNames[4].Substring(4, 2) + "-");
                timeSB.Append(subNames[4].Substring(6, 2) + " ");
                timeSB.Append(subNames[5].Substring(0, 2) + ":");
                timeSB.Append(subNames[5].Substring(2, 2) + ":");
                timeSB.Append(subNames[5].Substring(4, 2));
                vf.StartTime = Convert.ToDateTime(timeSB.ToString());
                return true;
            }
            catch (Exception ex)
            {
                CommonLibrary.LogHelper.Log4Helper.Error(typeof(FileHelper), "获取文件树节点", ex);
                return false;
            }
        }
    }
}

