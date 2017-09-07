using System;
using System.Collections;
using System.Linq;

namespace DapperInfrastructure.DapperWrapper.SQLHelper
{

    /// <summary>
    /// SQL Class 扩展
    /// </summary>
    public static class SqlExtensions
    {  

        public static Sql WhereIf(this Sql source, bool ifTrue, Func<Sql, string> sql, Func<Sql, object[]> args)
        {
            if (ifTrue)
                return source.Where(sql(source), args(source));
            else
                return source;
        }

        public static Sql WhereIf(this Sql source, bool ifTrue, string sql, Func<Sql, object> arg)
        {
            if (ifTrue)
                return source.Where(sql, arg(source));
            else
                return source;
        }

        public static Sql WhereIf(this Sql source, bool ifTrue, string sql, params object[] args)
        {
            if (ifTrue)
                return source.Where(sql, args);
            else
                return source;
        }

        public static Sql WhereIf(this Sql source, string sql, object arg)
        {
            if (!IsEmpty(arg))
                return source.Where(sql, arg);
            else
                return source;
        }

        public static Sql WhereIfLike(this Sql source, string sql, string keyword)
        {
            keyword = keyword.SafeTrim();

            if (String.IsNullOrEmpty(keyword))
                return source;

            return source.Where(sql, String.Format("%{0}%", keyword));
        }

        public static Sql WhereIfLike(this Sql source, string keyword, params string[] columns)
        {
            keyword = keyword.SafeTrim();

            if (String.IsNullOrEmpty(keyword) ||
                !columns.HasElements())
                return source;

            return source.WhereIfLike(String.Join(" OR ", columns.Select(c => c.Contains(" @0") ? c : c + " LIKE @0")),
                keyword);
        }

 
        //public static Sql WhereIfInSet(this Sql source, string column, object[] args)
        //{
        //    if (!args.HasElements())
        //        return source;

        //    // MySQL
        //    return source.Where("find_in_set(" + column + ",@0)", String.Join(",", args));
        //}

        public static Sql WhereIfIn(this Sql source, string column, params object[] args)
        {
            if (!args.HasElements())
                return source;

            if (args is System.Enum[])
            {
                return source.Where(String.Format("{0} IN({1})", column, String.Join(",", args.Select(a => (int) a))));
            }
            else
            {
                return
                    source.Where(
                        String.Format("{0} IN({1})", column,
                            String.Join(",", Enumerable.Range(0, args.Length).Select(i => "@" + i))),
                        args);
            }
        }


        #region Helper


        /// <summary>
        /// 判断是否为空
        /// </summary>
        /// <param name="obj">检测对象</param>
        /// <returns></returns>
        private static bool IsEmpty(object obj)
        {
            if (obj == null)
                return true;
            else if (obj is string)
                return String.IsNullOrWhiteSpace((string)obj);
            else
                return false;
        }



        /// <summary>
        /// 安全Trim空格
        /// </summary>
        /// <param name="val">检测对象</param>
        /// <returns></returns>
        private static string SafeTrim(this string val)
        {
            val = !string.IsNullOrEmpty(val) ? val.Trim(): val;

            return val;
        }


        /// <summary>
        /// 判断集合是否有元素
        /// </summary>
        /// <param name="collection">检测对象</param>
        /// <returns></returns>
        private static bool HasElements(this ICollection collection)
        {
           return collection != null && collection.Count > 0; 
        } 

        #endregion
    }
}
