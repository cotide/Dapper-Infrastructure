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
        public void Execute(string sql,object param = null)
        {
            Execute(new Sql(sql, param));
        }



        /// <summary>
        /// 执行SQL
        /// </summary>
        /// <param name="sql">SQL</param>  
        public void Execute(Sql sql)
        { 
            UnitOfWork.DbConnection.Execute(sql.SQL, GetParams(sql.Arguments), UnitOfWork.DbTransaction);
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
