using System.ComponentModel;

namespace DapperInfrastructure.DapperWrapper.Enum
{
    /// <summary>
    /// 数据库类型
    /// </summary>
    public enum DbType
    {
        /// <summary>
        /// Sql Server 数据库
        /// </summary>
        [Description("Sql Server 数据库")]
        SqlServer,

        /// <summary>
        /// SQL Server CE 数据库
        /// </summary>
        [Description("SQL Server CE 数据库")]
        SqlServerCe,

        /// <summary>
        /// MySql 数据库
        /// </summary>
        [Description("MySql 数据库")]
        MySql,

        /// <summary>
        /// PostgreSQL 数据库
        /// </summary>
        [Description("PostgreSQL 数据库")]
        PostgreSql,

        /// <summary>
        /// Oracle 数据库
        /// </summary>
        [Description("Oracle 数据库")]
        Oracle,

        /// <summary>
        /// SqLite
        /// </summary>
        [Description("SqLite 数据库")]
        SqLite
    }
}
