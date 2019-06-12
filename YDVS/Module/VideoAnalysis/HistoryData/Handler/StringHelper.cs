using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoAnalysis.HistoryData.Handler
{
    public static class StringHelper
    {
        public static char[] GetFixLengthChar(this string s, int length)
        {
            char[] chaVal = new char[length];
            Array.Copy(s.PadRight(length, '\0').ToCharArray(), chaVal, length);
            return chaVal;
        }
        public static string GetString(this char[] cc)
        {
            return GetString(cc, true);
        }
        public static string GetString(this char[] cc, bool isTrimEnd)
        {
            if (isTrimEnd)
            {
                return new string(cc).TrimEnd('\0');
            }
            else
            {
                return new string(cc);
            }
        }
        /// <summary>
        /// 二进制字符串转数组
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte[] HexStringToByteArray(this string s)
        {
            s = s.Replace(" ", "");
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;
        }
        /// <summary>
        /// 数组转二进制形式的字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ByteArrayToHexString(this byte[] data, string spit)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0') + spit);
            return sb.ToString().ToUpper();
        }
        /// <summary>
        /// string类型转成ASCII byte[]:（"01" 转成 byte[] = new byte[]{ 0x30,0x31}）
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte[] StringToASCIIByte(this string s)
        {
            return System.Text.Encoding.ASCII.GetBytes(s);
        }
    }
}
