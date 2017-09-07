using System;
using System.Data;
using System.Data.Common;
using DapperInfrastructure.DapperWrapper.Factory;
using DapperInfrastructure.DapperWrapper.Factory.IFactory;
using DapperInfrastructure.DapperWrapper.Repository;
using DapperInfrastructure.DapperWrapper.Repository.SQL;
using DapperInfrastructure.DapperWrapper.UnitOfWork;
using DapperInfrastructure.Extensions.Domain;
using DapperInfrastructure.Extensions.Domain.Base;
using DapperInfrastructure.Extensions.Mapper; 

namespace DapperInfrastructure
{
    /// <summary>
    /// 数据库
    /// </summary>
    public class DB :IDisposable
    {
        /// <summary>
        /// 数据库工厂
        /// </summary> 
        private  IConnectionFactory _dbFactory;

      

        #region 数据库对象创建

        /// <summary>
        /// 初始化数据库
        /// </summary>
        public DB()
            : this("Default")
        {
          
        }
         
 


        /// <summary>
        /// 创建数据库
        /// </summary>
        /// <param name="connName">数据库连接名称</param> 
        /// <returns></returns>
        public static DB New(
            string connName)
        {
            return new DB(connName);
        }






         
        /// <summary>
        /// 创建数据库
        /// </summary>
        /// <returns></returns>
        public static DB New(DbConnection db)
        {
            return new DB(db);
        }

        /// <summary>
        /// 创建数据库
        /// </summary>
        /// <returns></returns>
        public static DB New(string connectionString, string providerName)
        {
            return new DB(connectionString, providerName);
        }


        /// <summary>
        /// 初始化数据库
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="provider">驱动对象</param>
        public static DB New(string connectionString, DbProviderFactory provider)
        {
            return new DB(connectionString, provider); 
        }


        /// <summary>
        /// 初始化数据库
        /// </summary>
        /// <param name="connectionName">连接字符串名称</param>
        public DB(string connectionName)
        {
            _dbFactory = new SqlConnectionFactory(connectionName);
        }


        /// <summary>
        /// 初始化数据库
        /// </summary> 
        public DB(DbConnection db)
        {
            _dbFactory = new SqlConnectionFactory(db);
        }
        


        /// <summary>
        /// 初始化数据库
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="providerName">驱动名称</param>
        public  DB(string connectionString, string providerName)
        { 
            _dbFactory = new SqlConnectionFactory(connectionString, providerName);
        }

        /// <summary>
        /// 初始化数据库
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="provider">驱动对象</param>
        public  DB(string connectionString, DbProviderFactory provider)
        {
            _dbFactory = new SqlConnectionFactory(connectionString, provider);
        }



        /// <summary>
        /// 获取Domain 实体
        /// </summary>  
        /// <returns></returns>
        public  string GetTableName<T>() where T : EntityByType
        {
            return TableMaper.GetName<T>();
        }


        #endregion


        private DapperUnitOfWork _unitOfWork;


        /// <summary>
        /// 事务对象
        /// </summary>
        /// <returns></returns>
        private IUnitOfWork GetUnitOfWork()
        {

            if (_dbFactory == null)
            {
                _dbFactory = new SqlConnectionFactory("default");
            }
            if (_unitOfWork == null)
            {
                _unitOfWork = new DapperUnitOfWork(_dbFactory, RepositoryResolver.Instance);
            }
            return _unitOfWork;
        }


        /// <summary>
        /// 获取仓储对象
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : EntityByType
        {
            return this.GetUnitOfWork().GetRepository<TEntity>();
        }



        /// <summary>
        /// 获取只读仓储
        /// </summary>
        public SqlQueryBase GetSqlQuery
        {
            get
            {
                return new SqlQueryBase(this.GetUnitOfWork() as DapperUnitOfWork);
            }
        }



              
        /// <summary>
        /// 获取SQL脚本执行处理对象
        /// </summary>
        public SqlExecuteBase GetSqlRun
        {
            get
            {
                return new SqlExecuteBase(this.GetUnitOfWork() as DapperUnitOfWork);
            }
        } 


        /// <summary>
        /// 开始事务
        /// </summary>
        public IDbTransaction BeginTransaction()
        {
           return this.GetUnitOfWork().BeginTransaction();
        }


        public void ChangeDatabase(string dbName)
        {
            this.GetUnitOfWork().ChangeDatabase(dbName);
        }
              
         
        /// <summary>
        /// 事务提交
        /// </summary>
        public void Commit()
        {
            this.GetUnitOfWork().Commit();
        }

        /// <summary>
        /// 事务回滚
        /// </summary>
        public void Rollback()
        {
            this.GetUnitOfWork().Rollback();
        }
         
        public void Dispose()
        {
            this.GetUnitOfWork().Dispose(); 

        } 

         ~DB()
         {
             GC.SuppressFinalize(this);
         }

    }
}
