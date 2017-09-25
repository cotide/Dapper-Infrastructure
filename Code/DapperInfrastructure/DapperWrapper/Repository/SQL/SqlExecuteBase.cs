using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DapperInfrastructure.DapperWrapper.SQLHelper;
using DapperInfrastructure.DapperWrapper.UnitOfWork;

namespace DapperInfrastructure.DapperWrapper.Repository.SQL
{
    /// <summary>
    /// SQL 执行扩展
    /// </summary>
    public class SqlExecuteBase
    { 
        
        protected readonly DapperUnitOfWork UnitOfWork;
         
        protected int OneTimeCommandTimeout { get; set; } 
        protected bool EnableNamedParams { get; set; }
        protected bool ForceDateTimesToUtc { get; set; }


        public SqlExecuteBase(DapperUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }


        /// <summary>
        /// 执行SQL
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <param name="param">参数</param> 
        public void Execute(string sql, object param = null)
        {
            Execute(new Sql(sql, param));
        }



        /// <summary>
        /// 执行SQL
        /// </summary>
        /// <param name="sql">SQL</param>  
        public int Execute(Sql sql)
        {
            UnitOfWork.GetOpenConnection();
            return UnitOfWork.DbConnection.Execute(sql.SQL, GetParams(sql.Arguments), UnitOfWork.DbTransaction);
        }



        /// <summary>
        /// 执行储存过程 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procSql"></param>
        /// <returns></returns>
        public int ExecuteProc(ProcSql procSql)
        {
            var parm = GetParams(procSql.Arguments);
            UnitOfWork.GetOpenConnection();
            var result = UnitOfWork.DbConnection.Execute(
                procSql.ProcName,
                parm,
                transaction: UnitOfWork.DbTransaction,
                commandType: CommandType.StoredProcedure);

            if (procSql.Arguments != null)
            {
                foreach (var item in procSql.Arguments.Where(x => x.IsOut))
                {
                    item.OutValue = parm.Get<object>("@" + item.Name);
                }
            }
            return result;
        }


        /// <summary>
        /// 执行储存过程 返回列表数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procSql"></param>
        /// <returns></returns>
        public IList<T> ExecuteProcList<T>(ProcSql procSql)
        {
            var parm = GetParams(procSql.Arguments);
            UnitOfWork.GetOpenConnection();
            var result = UnitOfWork.DbConnection.Query<T>(
                procSql.ProcName,
                parm,
                transaction: UnitOfWork.DbTransaction,
                commandType: CommandType.StoredProcedure).ToList();

            if (procSql.Arguments != null)
            {
                foreach (var item in procSql.Arguments.Where(x => x.IsOut))
                {
                    item.OutValue = parm.Get<object>("@" + item.Name);
                }
            }
            return result;
        }


        /// <summary>
        /// 执行存储过程 返回单条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procSql"></param>
        /// <param name="fun"></param>
        /// <returns></returns>
        public T ExecuteProcObj<T>(ProcSql procSql)
        {
            var parm = GetParams(procSql.Arguments);
            UnitOfWork.GetOpenConnection();
            var result = UnitOfWork.DbConnection.ExecuteScalar<T>(
                procSql.ProcName,
                parm,
                transaction: UnitOfWork.DbTransaction,
                commandType: CommandType.StoredProcedure);

            if (procSql.Arguments != null)
            {
                foreach (var item in procSql.Arguments.Where(x => x.IsOut))
                {
                    item.OutValue = parm.Get<object>("@" + item.Name);
                }
            }
            return result;
        }



        /// <summary>
        /// 执行存储过程  返回GridReader 对象用户处理多个结果集合
        /// </summary>
        /// <param name="procSql"></param>
        /// <returns></returns>
        public Dapper.SqlMapper.GridReader ExecuteProcReader(ProcSql procSql)
        {
            UnitOfWork.GetOpenConnection();
            return UnitOfWork.DbConnection.QueryMultiple(
                procSql.ProcName,
                GetParams(procSql.Arguments),
                transaction: UnitOfWork.DbTransaction,
                commandType: CommandType.StoredProcedure);
        }

         

        /// <summary>
        /// 参数注入
        /// </summary>
        /// <param name="argsList"></param>
        /// <returns></returns>
        private DynamicParameters GetParams(IList<ProcSql.ProcParm> argsList)
        {
            var parm = new DynamicParameters();
            if (argsList == null) return parm;

            foreach (var item in argsList)
            {
                if (item.IsOut)
                {
                    parm.Add("@" + item.Name, item.Value, direction: ParameterDirection.Output);
                }
                else
                {
                    parm.Add("@" + item.Name, item.Value);
                }
            }
            return parm;
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
    }
}
