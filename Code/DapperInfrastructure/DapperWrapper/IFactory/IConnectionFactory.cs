using System.Data;
using DbType = DapperInfrastructure.DapperWrapper.Enum.DbType;

namespace DapperInfrastructure.DapperWrapper.IFactory
{
    /// <summary>
    /// 连接工厂接口
    /// </summary>
    public interface IConnectionFactory
    {

        /// <summary>
        /// 获取连接对象
        /// </summary>
        /// <returns></returns>
        IDbConnection GetConnection();


        /// <summary>
        /// 获取数据库连接类型
        /// </summary>
        /// <returns></returns>
        DbType DbType { get; }
         
    }
}
