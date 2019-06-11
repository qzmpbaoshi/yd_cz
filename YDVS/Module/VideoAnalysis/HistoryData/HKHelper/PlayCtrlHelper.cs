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
        #region 播放控制
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
        /// 建立索引回调函数 
        /// </summary>
        /// <param name="nPort">播放器通道号</param>
        /// <param name="pUser">用户数据 </param>
        public delegate void FileRefDoneCB(uint nPort, IntPtr nUser);
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
        /// <summary>
        /// 获取原始图像大小。
        /// </summary>
        /// <param name="nPort">播放通道号 </param>
        /// <param name="pWidth">原始图像的宽度 </param>
        /// <param name="pHeight">原始图像的高度 </param>
        /// <returns></returns>
        [DllImport(@"..\HKDlls\PlayCtrl.dll")]
        public static extern bool PlayM4_GetPictureSize(Int32 nPort,ref UInt32 pWidth,ref UInt32 pHeight);
        #endregion
        #region 抓图
        /// <summary>
        /// 显示回调函数
        /// </summary>
        /// <param name="nPort">播放器通道号</param>
        /// <param name="pBuf">返回图像数据的指针</param>
        /// <param name="nSize">返回图像数据大小 </param>
        /// <param name="nWidth">画面宽，单位像素 </param>
        /// <param name="nHeight">画面高</param>
        /// <param name="nStamp"> 时标信息，单位毫秒</param>
        /// <param name="nType">数据类型，T_YV12，T_RGB32，T_UYVY，具体定义如下表所示：</param>
        /// <param name="nReceved">保留 </param>
        public delegate void DisplayCBFun(uint nPort, byte[] pBuf, Int32 nSize, Int32 nWidth, Int32 nHeight, Int32 nStamp, Int32 nType, Int32 nReceved);
        /// <summary>
        /// 显示回调
        /// </summary>
        /// <param name="nPort">播放通道号 </param>
        /// <param name="fDisplayCBFun">显示回调函数，若不需要回调函数则可以为NULL，否则不能置为NULL </param>
        /// <returns>成功返回TRUE；失败返回FALSE。获取错误码调用PlayM4_GetLastError。</returns>
        [DllImport(@"..\HKDlls\PlayCtrl.dll")]
        public static extern bool PlayM4_SetDisplayCallBack(Int32 nPort, DisplayCBFun fDisplayCBFun);
        /// <summary>
        /// 直接抓取JPEG图像。
        /// </summary>
        /// <param name="nPort">播放通道号 </param>
        /// <param name="pJpeg">存放JEPG图像数据地址，由用户分配，不得小于JPEG图像大小，建议大小w * h * 3/2， 其中w和h分别为图像宽高 </param>
        /// <param name="nBufSize">申请的缓冲区大小 </param>
        /// <param name="pJpegSize">获取到的实际JPEG图像数据大小</param>
        /// <returns></returns>
        [DllImport(@"..\HKDlls\PlayCtrl.dll")]
        public static extern bool PlayM4_GetJPEG(Int32 nPort, byte[] pJpeg, UInt32 nBufSize, ref UInt32 pJpegSize);
        /// <summary>
        /// 图像数据转为JPEG 格式。
        /// </summary>
        /// <param name="pBuf">抓图回调函数中图像缓冲区</param>
        /// <param name="nSize">抓图回调函数中图像的大小 </param>
        /// <param name="nWidth">抓图回调函数中图像宽度 </param>
        /// <param name="nHeight">抓图回调函数中图像高度</param>
        /// <param name="nType">抓图回调函数中图像类型，（当前的播放库获取的类型是yv12） </param>
        /// <param name="sFileName">要保存的文件名，最好以JPG作为文件扩展名 </param>
        /// <returns>成功返回TRUE；失败返回FALSE。获取错误码调用PlayM4_GetLastError。</returns>
        [DllImport(@"..\HKDlls\PlayCtrl.dll")]
        public static extern bool PlayM4_ConvertToJpegFile(byte[] pBuf, UInt32 nSize, UInt32 nWidth, UInt32 nHeight, Int32 nType, string sFileName);
        #endregion
    }
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
