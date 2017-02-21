using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using DapperInfrastructure.DapperWrapper.Encrypt;
using DapperInfrastructure.DapperWrapper.IFactory;
using DbType = DapperInfrastructure.DapperWrapper.Enum.DbType;

namespace DapperInfrastructure.DapperWrapper.Factory
{
    /// <summary>
    /// 连接工厂实例
    /// </summary>
    public class SqlConnectionFactory : IConnectionFactory
    { 
         
        readonly string connName; 
        readonly string connStr; 
        readonly string providerTypeName; 
        readonly DbProviderFactory _factory;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="connectionName">连接字符串 Key</param>
        public SqlConnectionFactory(string connectionName)
        {
            connName = connectionName;
            // Use first?
            if (connName == "")
                connStr = ConfigurationManager.ConnectionStrings[0].Name;

            // Work out connection string and provider name
            var providerKey = "System.Data.SqlClient";
            if (ConfigurationManager.ConnectionStrings[connName] != null)
            {
                if (!string.IsNullOrEmpty(ConfigurationManager.ConnectionStrings[connName].ProviderName))
                    providerKey = ConfigurationManager.ConnectionStrings[connName].ProviderName;
            }
            else
            {
                throw new InvalidOperationException("Can't find a connection string with the name '" + connName + "'");
            }

            // Store factory and connection string
            connStr = DesCode.DecryptDes(ConfigurationManager.ConnectionStrings[connName].ConnectionString);
            providerTypeName = providerKey;
            Init();

        }

        public SqlConnectionFactory(
            string connectionString, 
            string providerName)
        {
            connStr = connectionString;
            providerTypeName = providerName; 
            _factory = DbProviderFactories.GetFactory(providerTypeName);
            Init();
        }
         
        public SqlConnectionFactory(
            string connectionString, 
            DbProviderFactory provider)
        { 
            connStr = connectionString;
            _factory = provider;
            providerTypeName = _factory.GetType().Name;
            Init();
        }
         
        public IDbConnection GetConnection()
        {
            var sharedConnection =  _factory.CreateConnection(); 
            sharedConnection.ConnectionString = connStr;
            return sharedConnection; 
        }

         
         
        /// <summary>
        /// 获取数据库连接类型
        /// </summary>
        /// <returns></returns>
        public DbType DbType { get; set; }


        #region Helper

        private void Init()
        {
            string dbtype = (_factory?.GetType() ?? GetConnection().GetType()).Name;

            DbType dbType = DbType.SqlServer; 
            if (dbtype.StartsWith("MySql")) dbType = DbType.MySql;
            else if (dbtype.StartsWith("SqlCe")) dbType = DbType.SqlServerCe;
            else if (dbtype.StartsWith("Npgsql")) dbType = DbType.PostgreSql;
            else if (dbtype.StartsWith("Oracle")) dbType = DbType.Oracle;
            else if (dbtype.StartsWith("SQLite")) dbType = DbType.SqLite;
            else if (dbtype.StartsWith("System.Data.SqlClient.")) dbType = DbType.SqlServer;
            // else try with provider name
            else if (providerTypeName.IndexOf("MySql", StringComparison.InvariantCultureIgnoreCase) >= 0) dbType = DbType.MySql;
            else if (providerTypeName.IndexOf("SqlServerCe", StringComparison.InvariantCultureIgnoreCase) >= 0) dbType = DbType.SqlServerCe;
            else if (providerTypeName.IndexOf("Npgsql", StringComparison.InvariantCultureIgnoreCase) >= 0) dbType = DbType.PostgreSql;
            else if (providerTypeName.IndexOf("Oracle", StringComparison.InvariantCultureIgnoreCase) >= 0) dbType = DbType.Oracle;
            else if (providerTypeName.IndexOf("SQLite", StringComparison.InvariantCultureIgnoreCase) >= 0) dbType = DbType.SqLite;
            DbType = dbType;

        } 

        #endregion

    }
}
