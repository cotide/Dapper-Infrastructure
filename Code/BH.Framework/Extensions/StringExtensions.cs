using System;
using System.Text.RegularExpressions;

namespace BH.Framework.Extensions
{
    /// <summary>
    /// String 的扩展类
    /// </summary>
    /// <remarks>
    ///     <para>    Creator：LHC</para>
    ///     <para>CreatedTime：2013/7/26 11:32:46</para>
    /// </remarks>
    public static class StringExtensions
    {
        /// <summary>
        ///  获取版本号
        /// </summary>
        /// <param name="str">当前字符串</param>
        /// <param name="versionNo">版本号</param>
        /// <param name="versionName">版本号名车</param>
        /// <returns>是否是数值类型</returns>
        public static string GetVersionPar(
            this string str,
            string versionNo,
            string versionName = "Version")
        {
            return string.Format(IsHaveUrlParameter(str)
                ? "{0}&{1}={2}"
                : "{0}?{1}={2}",
                str,
                versionName,
                versionNo);
        }


        /// <summary>
        ///  获取地址参数
        /// </summary>
        /// <param name="str">当前字符串</param> 
        /// <returns>是否是数值类型</returns>
        public static string GetParStr(this string str)
        {

            return string.Format(IsHaveUrlParameter(str)
                ? "{0}&{1}={2}"
                : "{0}?{1}={2}",
                str,
                "_t",
               Guid.NewGuid().ToString("N"));
        }


        /// <summary>
        /// Url是否有参数
        /// </summary>
        /// <param name="str">当前字符串</param>
        /// <returns></returns>
        public static bool IsHaveUrlParameter(this string str)
        {
            return str.IndexOf('?') > 0;
        }


        /// <summary>
        /// 生成流水号
        /// </summary>
        /// <param name="str"></param>
        /// <param name="prefixStr"><c>prefixStr</c>流水号前缀,默认为空</param>
        /// <param name="format">流水号日期格式,<c>format</c>默认为yyyymmddhhmmss</param>
        /// <returns></returns>
        public static string GenerateSerialNo(
            this string str,
            string prefixStr = "",
            string format = "yyyymmddhhmmss")
        {
            return prefixStr ?? "" + DateTime.Now.ToString(format);
        }

        /// <summary>
        /// 获取域名
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetDomain(this string str)
        {
            string text = str;
            string pattern = @"(?<=http://)[\w\.]+[^/]";　//C#正则表达式提取匹配URL的模式，       
            string s = "";
            MatchCollection mc = Regex.Matches(text, pattern);//满足pattern的匹配集合        
            foreach (Match match in mc)
            {
                s = match.ToString();
            }
            return s;
        }
    }
}
