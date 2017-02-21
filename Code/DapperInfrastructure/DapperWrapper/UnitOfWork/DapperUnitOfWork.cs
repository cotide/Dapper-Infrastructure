using System;
using System.Data;
using DapperInfrastructure.Extensions.Domain;
using DapperInfrastructure.DapperWrapper.IFactory;
using DapperInfrastructure.DapperWrapper.Repository;
using DbType = DapperInfrastructure.DapperWrapper.Enum.DbType;

namespace DapperInfrastructure.DapperWrapper.UnitOfWork
{

    /// <summary>
    /// 业务对象实例
    /// </summary>
    public  class DapperUnitOfWork : IUnitOfWork
    { 
        /// <summary>
        /// 仓储接口
        /// </summary>
        private readonly IRepositoryResolver _resolver;
        private readonly IConnectionFactory _connectionFactory; 

        /// <summary>
        /// 数据库连接对象
        /// </summary>
        public IDbConnection DbConnection { get; private set; }

        /// <summary>
        /// 数据库事务对象
        /// </summary>
        public IDbTransaction DbTransaction { get; private set; }

 
        /// <summary>
        /// 数据库类型
        /// </summary>
        public DbType DbType
        {
            get { return _connectionFactory.DbType; }
        }



        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionFactory">连接工厂实例</param>
        /// <param name="resolver">仓储接口</param>
        public DapperUnitOfWork(
            IConnectionFactory connectionFactory,
            IRepositoryResolver resolver)
        {
             
            _resolver = resolver;
            _connectionFactory = connectionFactory; 
        }

        public virtual void GetOpenConnection()
        { 
            if (DbConnection != null) return;
            DbConnection = _connectionFactory.GetConnection();
            DbConnection.Open(); 
        }

        public virtual IRepository<TEntity> GetRepository<TEntity>() where TEntity : EntityByType
        {
            return _resolver.GetRepositoryForEntity<TEntity>(this);
        }

        public virtual void BeginTransaction()
        {
            GetOpenConnection();
            if (DbTransaction != null) return;
            DbTransaction = DbConnection.BeginTransaction();
        }

        public virtual void Commit()
        {
            DbTransaction?.Commit();
        }

        public virtual void Rollback()
        {
            DbTransaction?.Rollback();
        }

        public virtual void Dispose()
        {
            if (DbTransaction != null)
                DbTransaction.Dispose();

            if (DbConnection != null)
                DbConnection.Dispose();

            DbTransaction = null;
            DbConnection = null;
        }

        public virtual void ManageTransaction(Action action)
        {
            GetOpenConnection();
            bool outerTxStarted = (DbTransaction != null);
            if (!outerTxStarted) BeginTransaction();
            action();
            if (!outerTxStarted) Commit();
        }
    }
}