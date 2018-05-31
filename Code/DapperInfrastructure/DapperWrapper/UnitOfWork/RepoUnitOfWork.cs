using System;
using System.Data;
using DapperInfrastructure.DapperWrapper.Repository;
using DapperInfrastructure.DapperWrapper.Repository.SQL;
using DapperInfrastructure.Extensions.Domain.Base;

namespace DapperInfrastructure.DapperWrapper.UnitOfWork
{
    public class RepoUnitOfWork : IDisposable
    {
        protected DapperUnitOfWork UnitOfWork;

        public RepoUnitOfWork(DapperUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
         

        /// <summary>
        /// 数据库连接对象
        /// </summary>
        public IDbConnection DbConnection {
            get
            {
                return UnitOfWork.DbConnection;
                
            }
        }

        /// <summary>
        /// 切换数据库
        /// </summary>
        /// <param name="dbName"></param>
        public void ChangeDatabase(string dbName)
        {
            UnitOfWork.ChangeDatabase(dbName);
        }
         
        /// <summary>
        /// 获取仓储对象
        /// </summary>
        /// <typeparam name="TEntity">仓储对象</typeparam> 
        /// <returns></returns>
        public virtual IRepository<TEntity> GetRepository<TEntity>() where TEntity : EntityByType
        {
            return UnitOfWork.GetRepository<TEntity>();
        }


        /// <summary>
        /// 获取只读仓储
        /// </summary>
        public SqlQueryBase GetSqlQuery
        {
            get
            {
                return new SqlQueryBase(UnitOfWork);
            }
        }



        public void Dispose()
        {
            if(this.UnitOfWork!=null)
            { 
                this.UnitOfWork.Dispose();
            } 
        }

        ~RepoUnitOfWork()
        {
            GC.SuppressFinalize(this);
        } 
    }
}
