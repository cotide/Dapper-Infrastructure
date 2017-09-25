using System.ComponentModel;

namespace DapperInfrastructure.Tests.Enum
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public enum DBName
    {
         

        /// <summary>
        /// DB1 数据库
        /// </summary>
        [Description("DB1 数据库")]
        DB1,

        /// <summary>
        /// DB2 数据库
        /// </summary>
        [Description("DB2 数据库")]
        DB2 
        

    }
}
