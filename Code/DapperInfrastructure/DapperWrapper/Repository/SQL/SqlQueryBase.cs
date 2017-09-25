using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Dapper;
using DapperInfrastructure.DapperWrapper.Pagination;
using DapperInfrastructure.DapperWrapper.SQLHelper;
using DapperInfrastructure.DapperWrapper.UnitOfWork;
using DapperInfrastructure.Extensions.Collections;
using DbType = DapperInfrastructure.DapperWrapper.Enum.DbType;

namespace DapperInfrastructure.DapperWrapper.Repository.SQL
{
    /// <summary>
    ///  CRUD 仓储 实例
    /// </summary>
    public  class SqlQueryBase
    {
        /// <summary>
        /// 业务对象实例
        /// </summary>
        public readonly DapperUnitOfWork UnitOfWork;
         
        /// <summary>
        /// 超时时间
        /// </summary>
        protected int OneTimeCommandTimeout { get; set; } 

        /// <summary>
        /// 是否使用名称参数
        /// </summary>
        protected bool EnableNamedParams { get; set; }

        /// <summary>
        /// 是否进行时间格式化
        /// </summary>
        protected bool ForceDateTimesToUtc { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="unitOfWork"></param>
        public SqlQueryBase(DapperUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        #region 获取列表数据 



        /// <summary>
        /// 获取实体列表数据
        /// </summary>
        /// <param name="sql">SQL 对象</param> 
        /// <returns></returns>
        public IList<TDto> GetList<TDto>(
            Sql sql)
        { 
            UnitOfWork.GetOpenConnection();
            var results = UnitOfWork.DbConnection.Query<TDto>(
                sql.SQL,
                GetParams(sql.Arguments),
                UnitOfWork.DbTransaction);
            return results.ToList();
        }
 

        /// <summary>
        /// 获取实体列表数据 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public IList<TDto> GetList<TDto>(
            string sql,
            params object[] param)
        {
            return GetList<TDto>(new Sql(sql,param));  
        }



        #endregion

        #region 获取单条数据


        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="sql">SQL 对象</param> 
        /// <returns></returns>
        public TDto Get<TDto>(
            Sql sql)
        {
            UnitOfWork.GetOpenConnection();
            var results = UnitOfWork.DbConnection.QueryFirstOrDefault<TDto>(
                sql.SQL,
                GetParams(sql.Arguments),
                UnitOfWork.DbTransaction);
            return results;
        }
 

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public TDto Get<TDto>(string sql, params object[] param)
        {  
            return Get<TDto>(new Sql(sql,param));
        }
 

        #endregion

        /// <summary>
        /// 统计总数
        /// </summary>
        /// <param name="sql">SQL 对象</param> 
        /// <returns></returns>
        public int Count(Sql sql)
        {
            UnitOfWork.GetOpenConnection();
            var results = UnitOfWork.DbConnection.ExecuteScalar<int>(
                sql.SQL,
                GetParams(sql.Arguments),
                UnitOfWork.DbTransaction);
            return results;
        }
         

        #region 分页获取数据

        /// <summary>
        /// 获取数据(分页)
        /// </summary>
        /// <typeparam name="TDto">返回泛型结果</typeparam>
        /// <param name="pageIndex">开始页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="sql">SQL语句</param>
        /// <returns></returns>
        public PageList<TDto> PageList<TDto>(int pageIndex, int pageSize, Sql sql)
        {
            var pageData = Page<TDto>(pageIndex, pageSize, sql.SQL, sql.CountField, sql.Arguments);
            var result = pageData.GetPage<TDto>(pageIndex, pageSize);
            return result;
        }

        #endregion

        #region Helper

        /// <summary>
        /// 分页处理
        /// </summary>
        /// <param name="pageIndex">开始页码</param>
        /// <param name="itemsPerPage">总页数</param>
        /// <param name="sql">SQL脚本</param>
        /// <param name="sqlCountField">查询字段</param>
        /// <param name="param">参数</param>
        /// <typeparam name="Dto">返回结果集</typeparam>
        /// <returns></returns>
        protected Page<Dto> Page<Dto>(
            int pageIndex, 
            int itemsPerPage, 
            string sql,
            string sqlCountField,
            params object[] param)
        {
            UnitOfWork.GetOpenConnection();
            //UnitOfWork.DbConnection..GetPage<TEntity>()
            //   DapperExtensions.DapperExtensions.GetMap<T>();

            string sqlCount, sqlPage;
            object[] sqlCountArgs;
            BuildPageQueries<Dto>(
                (pageIndex - 1) * itemsPerPage, 
                itemsPerPage,
                sql,
                sqlCountField,
                ref param, 
                out sqlCount,
                out sqlCountArgs,
                out sqlPage);

            // Save the one-time command time out and use it for both queries
            int saveTimeout = OneTimeCommandTimeout;

            // Setup the paged result
            var result = new Page<Dto>();
            result.CurrentPage = pageIndex;
            result.ItemsPerPage = itemsPerPage;
            result.TotalItems = Count(new Sql(sqlCount, sqlCountArgs));
            result.TotalPages = result.TotalItems / itemsPerPage;
            if ((result.TotalItems % itemsPerPage) != 0)
                result.TotalPages++;

            OneTimeCommandTimeout = saveTimeout;

            // Get the records
            result.Items = GetList<Dto>(sqlPage, param);

            // Done
            return result;
        }

         
        private void BuildPageQueries<T>(
            long skip,
            long take,
            string sql,
            string sqlCountField,
            ref object[] args,
            out string sqlCount,
            out object[] sqlCountArgs,
            out string sqlPage)
        {

            var _dbType = UnitOfWork.DbType;

            // Split the SQL into the bits we need
            string sqlSelectRemoved, sqlOrderBy;
            if (!SplitSqlForPaging(
                sql,
                sqlCountField,
                out sqlCount,
                out sqlSelectRemoved, 
                out sqlOrderBy))
                throw new Exception("Unable to parse SQL statement for paged query");
            if (_dbType == DbType.Oracle && sqlSelectRemoved.StartsWith("*"))
                throw new Exception("GetList must alias '*' when performing a paged query.\neg. select t.* from table t order by t.id");

            // Build the SQL for the actual final result
            if (_dbType == DbType.SqlServer || _dbType == DbType.Oracle)
            {
                sqlSelectRemoved = rxOrderBy.Replace(sqlSelectRemoved, "");
                if (rxDistinct.IsMatch(sqlSelectRemoved))
                {
                    sqlSelectRemoved = "peta_inner.* FROM (SELECT " + sqlSelectRemoved + ") peta_inner";
                }
                sqlPage = string.Format("SELECT * FROM (SELECT ROW_NUMBER() OVER ({0}) peta_rn, {1}) peta_paged WHERE peta_rn>@{2} AND peta_rn<=@{3}",
                                        sqlOrderBy == null ? "ORDER BY (SELECT NULL)" : sqlOrderBy, sqlSelectRemoved, args.Length, args.Length + 1);
                sqlCountArgs = args;
                args = args.Concat(new object[] { skip, skip + take }).ToArray();
            }
            else if (_dbType == DbType.SqlServer)
            {
                sqlPage = string.Format("{0}\nOFFSET @{1} ROWS FETCH NEXT @{2} ROWS ONLY", sql, args.Length, args.Length + 1);
                sqlCountArgs = args;
                args = args.Concat(new object[] { skip, take }).ToArray();
            }
            else
            {
                sqlPage = string.Format("{0}\nLIMIT @{1} OFFSET @{2}", sql, args.Length, args.Length + 1);
                sqlCountArgs = args;
                args = args.Concat(new object[] { take, skip }).ToArray();
            }

        }


        static Regex rxColumns = new Regex(@"\A\s*SELECT\s+((?:\((?>\((?<depth>)|\)(?<-depth>)|.?)*(?(depth)(?!))\)|.)*?)(?<!,\s+)\s\bFROM\b", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.Compiled);
        static Regex rxOrderBy = new Regex(@"\bORDER\s+BY\s+(?:\((?>\((?<depth>)|\)(?<-depth>)|.?)*(?(depth)(?!))\)|[\w\(\)\.])+(?:\s+(?:ASC|DESC))?(?:\s*,\s*(?:\((?>\((?<depth>)|\)(?<-depth>)|.?)*(?(depth)(?!))\)|[\w\(\)\.])+(?:\s+(?:ASC|DESC))?)*", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.Compiled);
        static Regex rxDistinct = new Regex(@"\ADISTINCT\s", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.Compiled);
        private static bool SplitSqlForPaging(
            string sql, 
            string sqlCountField,
            out string sqlCount,
            out string sqlSelectRemoved,
            out string sqlOrderBy)
        {
            sqlSelectRemoved = null;
            sqlCount = null;
            sqlOrderBy = null;

            // Extract the columns from "SELECT <whatever> FROM"
            var m = rxColumns.Match(sql);
            if (!m.Success)
                return false;

            // Save column list and replace with COUNT(*)
            Group g = m.Groups[1];
            sqlSelectRemoved = sql.Substring(g.Index);

            if (!string.IsNullOrEmpty(sqlCountField))
            {
                sqlCount = sql.Substring(0, g.Index) + "COUNT(" + sqlCountField + ") " +
                           sql.Substring(g.Index + g.Length);
            }
            else
            {


                if (rxDistinct.IsMatch(sqlSelectRemoved))
                    sqlCount = sql.Substring(0, g.Index) + "COUNT(" + m.Groups[1].ToString().Trim() + ") " +
                               sql.Substring(g.Index + g.Length);
                else
                    sqlCount = sql.Substring(0, g.Index) + "COUNT(*) " + sql.Substring(g.Index + g.Length);
            }

            // Look for an "ORDER BY <whatever>" clause
            m = rxOrderBy.Match(sqlCount);
            if (!m.Success)
            {
                sqlOrderBy = null;
            }
            else
            {
                g = m.Groups[0];
                sqlOrderBy = g.ToString();
                sqlCount = sqlCount.Substring(0, g.Index) + sqlCount.Substring(g.Index + g.Length);
            }

            return true;
        }




        /// <summary>
        /// 参数注入
        /// </summary>
        /// <param name="argsList"></param>
        /// <returns></returns>
        private DynamicParameters GetParams(params object[] argsList)
        {
            var parm = new DynamicParameters();
            if (argsList == null) return parm;
            for (var i = 0; i < argsList.Length; i++)
            {
                parm.Add("arg" + i, argsList[i]);
            }

            return parm;
        }

        #endregion

    }
}
