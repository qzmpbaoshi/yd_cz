using System;
using System.Runtime.InteropServices;

namespace VideoAnalysis.HistoryData.HKHelper
{
    public class PlayCtrlHelper
    {
        /// <summary>
        /// 获取播放库SDK版本号和build号。
        /// </summary>
        /// <returns></returns>
        [DllImport(@"..\HKDlls\PlayCtrl.dll")]
        public static extern uint PlayM4_GetSdkVersion();
        /// <summary>
        /// 获取未使用的通道号
        /// </summary>
        /// <param name="nPort">播放通道号，指向用于获取端口号的LONG型变量指针</param>
        /// <returns></returns>
        [DllImport(@"..\HKDlls\PlayCtrl.dll")]
        public static extern bool PlayM4_GetPort(ref Int32 nPort);
        /// <summary>
        /// 释放已使用的通道号
        /// </summary>
        /// <param name="nPort">播放通道号</param>
        /// <returns></returns>
        [DllImport(@"..\HKDlls\PlayCtrl.dll")]
        public static extern bool PlayM4_FreePort(Int32 nPort);
        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="nPort">播放通道号</param>
        /// <param name="sFileName">文件名 </param>
        /// <returns></returns>
        [DllImport(@"..\HKDlls\PlayCtrl.dll")]
        public static extern bool PlayM4_OpenFile(Int32 nPort, string sFileName);
        /// <summary>
        /// 关闭文件。
        /// </summary>
        /// <param name="nPort">播放通道号 </param>
        /// <returns>成功返回TRUE；失败返回FALSE。获取错误码调用PlayM4_GetLastError</returns>
        [DllImport(@"..\HKDlls\PlayCtrl.dll")]
        public static extern bool PlayM4_CloseFile(Int32 nPort);
        /// <summary>
        /// 同步回放接口
        /// </summary>
        /// <param name="nPort">播放通道号 </param>
        /// <param name="dwGroupIndex">同步组序号，取值在0~3之间</param>
        /// <returns></returns>
        [DllImport(@"..\HKDlls\PlayCtrl.dll")]
        public static extern bool PlayM4_SetSycGroup(Int32 nPort, int dwGroupIndex);
        /// <summary>
        /// 开启播放
        /// </summary>
        /// <param name="nPort">播放通道号 </param>
        /// <param name="hWnd">播放视频的窗口句柄</param>
        /// <returns></returns>
        [DllImport(@"..\HKDlls\PlayCtrl.dll")]
        public static extern bool PlayM4_Play(Int32 nPort, IntPtr hWnd);
        /// <summary>
        /// 关闭播放
        /// </summary>
        /// <param name="nPort">播放通道号 </param>
        /// <returns></returns>
        [DllImport(@"..\HKDlls\PlayCtrl.dll")]
        public static extern bool PlayM4_Stop(Int32 nPort);
        /// <summary>
        /// 暂停/恢复播放
        /// </summary>
        /// <param name="nPort">播放通道号</param>
        /// <param name="nPause">1：暂停，0：恢复 </param>
        /// <returns></returns>
        [DllImport(@"..\HKDlls\PlayCtrl.dll")]
        public static extern bool PlayM4_Pause(Int32 nPort, int nPause);

        /// <summary>
        /// 设置同步回放开始时间
        /// </summary>
        /// <param name="nPort">播放通道号</param>
        /// <param name="pstSystemTime">同步的系统时间结构体指针</param>
        /// <returns></returns>
        [DllImport(@"..\HKDlls\PlayCtrl.dll")]
        public static extern bool PlayM4_SetSycStartTime(Int32 nPort, ref PLAYM4_SYSTEM_TIME pstSystemTime);
        /// <summary>
        /// 获取文件总时间
        /// </summary>
        /// <param name="nPort">播放通道号 </param>
        /// <returns>四舍五入后的文件总时间长度，单位：秒，若失败则返回0xffffffff。获取错误码调用PlayM4_GetLastError。</returns>
        [DllImport(@"..\HKDlls\PlayCtrl.dll")]
        public static extern uint PlayM4_GetFileTime(Int32 nPort);
        /// <summary>
        /// 获取已播放时间。
        /// </summary>
        /// <param name="nPort">播放通道号 </param>
        /// <returns>成功返回文件当前播放时间，单位秒；失败返回0xffffffff。获取错误码调用PlayM4_GetLastError</returns>
        [DllImport(@"..\HKDlls\PlayCtrl.dll")]
        public static extern uint PlayM4_GetPlayedTime(Int32 nPort);
        /// <summary>
        /// 获取当前播放帧的全局时间。
        /// </summary>播放通道号 
        /// <param name="nPort"></param>
        /// <param name="pstSystemTime">全局时间 </param>
        /// <returns>成功返回TRUE；失败返回FALSE。获取错误码调用PlayM4_GetLastError。</returns>
        [DllImport(@"..\HKDlls\PlayCtrl.dll")]
        public static extern bool PlayM4_GetSystemTime(Int32 nPort, ref PLAYM4_SYSTEM_TIME pstSystemTime);
        /// <summary>
        /// 文件结束回调函数 
        /// </summary>
        /// <param name="nPort">播放器通道号</param>
        /// <param name="pUser">用户数据 </param>
        public delegate void FileEndCallback(Int32 nPort, IntPtr pUser);
        /// <summary>
        /// 文件结束回调。
        /// 设置文件结束时要发送的消息；从2.4版本SDK起，当文件播放完时，解码线程将不会自动结束，需要用户做停止工作设置文件播放结束回调函数。
        /// 在PlayM4_OpenSteam/PlayM4_OpenFile之前调用才有效。
        /// 关于回调函数。因为vb不支持多线程，所以当回调函数是VB声明的函数时，在vc的线程中调用vb的函数，会有问题。
        /// 详见：Microsoft Knowledge Base Article - Q198607 “PRB: Access Violation in VB Run-Time Using AddressOf ”。
        /// 回调与消息不能同时使用。
        /// 回调函数中不能调用参数中带nPort的播放库接口，否则有可能造成死锁。
        /// </summary>
        /// <param name="nPort">播放通道号</param>
        /// <param name="fFileEndCBFun">文件结束回调函数</param>
        /// <param name="pUser">用户数据 </param>
        /// <returns></returns>
        [DllImport(@"..\HKDlls\PlayCtrl.dll")]
        public static extern bool PlayM4_SetFileEndCallback(Int32 nPort, FileEndCallback fFileEndCBFun, IntPtr pUser);


        /// <summary>
        /// 设置建立索引回调。
        /// 建立文件索引回调。为了能在文件中准确快速的定位，在文件打开的时候生成文件索引。
        /// 如果是大文件，建立索引时间会相对时间长，主要是因为从硬盘读数据比较慢。
        /// 建立索引的过程是在后台完成，需要使用索引的函数要等待这个过程结束，而其他接口不会受到影响。
        /// 打开文件时是否建立文件的关键帧索引, 如果索引回调未触发, 表明录像文件异常。
        /// 回调函数中不能调用参数中带nPort的播放库接口，否则有可能造成死锁。
        /// </summary>
        /// <param name="nPort">播放通道号 </param>
        /// <param name="pFileRefDone">索引回调函数 </param>
        /// <param name="nUser">用户指针</param>
        /// <returns></returns>
        [DllImport(@"..\HKDlls\PlayCtrl.dll")]
        public static extern bool PlayM4_SetFileRefCallBack(Int32 nPort, FileRefDoneCB pFileRefDone, IntPtr nUser);
        /// <summary>
        /// 获取错误号。
        /// </summary>
        /// <param name="nPort"></param>
        /// <returns></returns>
        [DllImport(@"..\HKDlls\PlayCtrl.dll")]
        public static extern uint PlayM4_GetLastError(Int32 nPort);

        /// <summary>
        /// 设置文件当前播放时间（毫秒）。
        /// </summary>
        /// <param name="nPort">播放通道号</param>
        /// <param name="nTime">设置文件播放到指定时间，单位毫秒 </param>
        /// <returns></returns>
        [DllImport(@"..\HKDlls\PlayCtrl.dll")]
        public static extern bool PlayM4_SetPlayedTimeEx(Int32 nPort, uint nTime);
        /// <summary>
        /// 获取文件总帧数。
        /// </summary>
        /// <param name="nPort">播放通道号 </param>
        /// <returns>成功返回文件总帧数；失败返回0xffffffff，获取错误码调用PlayM4_GetLastError。</returns>
        [DllImport(@"..\HKDlls\PlayCtrl.dll")]
        public static extern uint PlayM4_GetFileTotalFrames(Int32 nPort);
        /// <summary>
        /// 获取已解码的视频帧数
        /// </summary>
        /// <param name="nPort">播放通道号</param>
        /// <returns>成功返回已经解码的视频帧数；失败返回0xffffffff。获取错误码调用PlayM4_GetLastError。</returns>
        [DllImport(@"..\HKDlls\PlayCtrl.dll")]
        public static extern uint PlayM4_GetPlayedFrames(Int32 nPort);

    }
    public delegate void FileRefDoneCB(uint nPort, IntPtr nUser);
    [StructLayoutAttribute(LayoutKind.Sequential, Pack = 1)]
    public struct PLAYM4_SYSTEM_TIME
    {
        public uint dwYear;
        public uint dwMonth;
        public uint dwDay;
        public uint dwHour;
        public uint dwMinute;
        public uint dwSecond;
        public uint dwMs;
    }
}
