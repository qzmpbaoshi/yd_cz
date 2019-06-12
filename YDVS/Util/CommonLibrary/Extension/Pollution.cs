using System;
using System.Collections.Generic;
using System.Reflection;
using SystemModel.SearchConditionModel;

namespace CommonLibrary.Extension
{
    public static class Pollution
    {
        /// <summary>
        /// 将对象转化为字典，公共属性名字为字典的key，公共属性值为字典的value
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ClassToDictionary(this object obj)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            Type t = obj.GetType();
            PropertyInfo[] pis = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo pi in pis)
            {
                dic.Add(pi.Name, pi.GetValue(obj));
            }
            return dic;
        }
        /// <summary>
        /// 属性复制，公共属性名称和类型必须相同
        /// </summary>
        /// <param name="toObj">目标对象</param>
        /// <param name="fromObj">源对象</param>
        /// <returns></returns>
        public static bool ObjectCopyProperty(this object toObj, object fromObj)
        {
            try
            {
                if (toObj == null || fromObj == null) return false;
                Type toT = toObj.GetType();
                Type fromT = fromObj.GetType();
                foreach (PropertyInfo pi in toT.GetProperties())
                {
                    PropertyInfo fromPi = fromT.GetProperty(pi.Name);
                    if (fromPi != null)
                        pi.SetValue(toObj, fromPi.GetValue(fromObj));
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
