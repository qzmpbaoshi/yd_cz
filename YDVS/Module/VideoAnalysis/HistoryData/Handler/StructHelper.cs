using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace VideoAnalysis.HistoryData.Handler
{
    public static class StructHelper
    {
        /// <summary>
        /// byte数组转目标结构体
        /// </summary>
        /// <param name="bytes">byte数组</param>
        /// <param name="type">目标结构体类型</param>
        /// <returns>目标结构体</returns>
        public static T ByteToStuct<T>(byte[] DataBuff_) where T : struct
        {
            Type t = typeof(T);
            //得到结构体大小
            int size = Marshal.SizeOf(t);
            //数组长度小于结构体大小
            if (size > DataBuff_.Length)
            {
                return default(T);
            }

            //分配结构体大小的内存空间
            IntPtr structPtr = Marshal.AllocHGlobal(size);
            //将byte数组cpoy到分配好的内存空间内
            Marshal.Copy(DataBuff_, 0, structPtr, size);
            //将内存空间转换为目标结构体
            T obj = (T)Marshal.PtrToStructure(structPtr, t);
            //释放内存空间
            Marshal.FreeHGlobal(structPtr);
            return obj;
        }
        /// <summary>
        /// 结构体转byte数组
        /// </summary>
        /// <param name="objstuct">结构体</param>
        /// <returns>byte数组</returns>
        public static byte[] StuctToByte(object objstuct)
        {
            //得到结构体大小
            int size = Marshal.SizeOf(objstuct);
            //创建byte数组
            byte[] bytes = new byte[size];
            //分配结构体大小的空间
            IntPtr structPtr = Marshal.AllocHGlobal(size);
            //将结构体copy到分配好的内存空间内
            Marshal.StructureToPtr(objstuct, structPtr, false);
            //从内存空间copy到byte数组
            Marshal.Copy(structPtr, bytes, 0, size);
            //释放内存空间
            Marshal.FreeHGlobal(structPtr);
            return bytes;
        }
    }
}
