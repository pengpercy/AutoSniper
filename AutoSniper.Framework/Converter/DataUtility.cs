using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace AutoSniper.Framework.Converter
{
    public static class DataUtility
    {
        #region 转DateTime
        /// <summary>
        /// Convert to date time 
        /// </summary>
        /// <param name="timeStamp">millseconds</param>
        /// <returns>DateTime</returns>
        public static DateTime ToDateTime(this long timeStamp)
        {
            return new DateTime(1970, 1, 1).ToLocalTime().AddMilliseconds(timeStamp);
        }
        /// <summary>
        /// Convert to date time stamp (millseconds)
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns>millseconds</returns>
        public static long ToTimeStamp(this DateTime dateTime)
        {
            return (dateTime.ToLocalTime() - new DateTime(1970, 1, 1).ToLocalTime()).Ticks / 10000;
        }
        #endregion

        #region 转QueryString
        public static string ToQueryString(this object obj)
        {
            var list = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                          .Where(s => s.GetGetMethod() != null && s.GetGetMethod().IsPublic)
                          .Select(p => p.Name + "=" + p.GetGetMethod().Invoke(obj, null) ?? "");
            return string.Join("&", list);
        }

        public static String ToQueryString(this Dictionary<String, String> dic)
        {
            return dic.Aggregate("", (current, i) => current + (i.Key + "=" + i.Value + "&")).TrimEnd('&');
        }

        public static String ToQueryString(this NameValueCollection nv)
        {
            return String.Join("&", nv.AllKeys.Select(i => String.Format("{0}={1}", i, nv[i])).ToList());
        }
        #endregion

        #region 转Dictionary
        /// <summary>
        /// Xml转Dictionary
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static Dictionary<String, Object> XmlToDictionary(this XmlDocument doc)
        {
            var json = JsonConvert.SerializeXmlNode(doc);
            return JsonConvert.DeserializeObject<Dictionary<String, Object>>(json);
        }

        /// <param name="nvc"></param>
        /// <param name="handleMultipleValuesPerKey">处理重复Key的方式</param>
        /// <returns></returns>
        public static Dictionary<String, String> ToDictionary(this NameValueCollection nvc, bool handleMultipleValuesPerKey)
        {
            var result = new Dictionary<String, String>();
            foreach (String key in nvc.Keys)
            {
                if (handleMultipleValuesPerKey)
                {
                    String[] values = nvc.GetValues(key);
                    if (values != null && values.Length == 1)
                    {
                        result.Add(key, values[0]);
                    }
                    else
                    {
                        result.Add(key, values.ToString());
                    }
                }
                else
                {
                    result.Add(key, nvc[key]);
                }
            }
            return result;
        }
        #endregion

        /// <summary>
        /// 枚举转List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<string> ToList<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>().Select(m => Enum.GetName(typeof(T), m)).ToList();
        }

        /// <summary>
        /// 将小数值按指定的小数位数截断
        /// </summary>
        /// <param name="d">要截断的小数</param>
        /// <param name="s">小数位数，s大于等于0，小于等于28</param>
        /// <returns></returns>
        public static decimal ToFixed(this decimal d, int s)
        {
            decimal sp = Convert.ToDecimal(Math.Pow(10, s));
            if (d < 0)
                return Math.Truncate(d) + Math.Ceiling((d - Math.Truncate(d)) * sp) / sp;
            else
                return Math.Truncate(d) + Math.Floor((d - Math.Truncate(d)) * sp) / sp;
        }
    }
}
