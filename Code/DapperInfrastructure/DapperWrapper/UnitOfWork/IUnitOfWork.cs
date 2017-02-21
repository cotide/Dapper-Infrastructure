﻿using System; 
using DapperInfrastructure.DapperWrapper.Repository;
using DapperInfrastructure.Extensions.Domain;

namespace DapperInfrastructure.DapperWrapper.UnitOfWork
{

    /// <summary>
    /// 业务对象接口
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {  
        /// <summary>
        /// 获取仓储对象
        /// </summary>
        /// <typeparam name="TEntity">领域对象</typeparam>
        /// <returns></returns>

        IRepository<TEntity> GetRepository<TEntity>() where TEntity : EntityByType;
         
        /// <summary>
        /// 开始事务
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// 事务提交
        /// </summary>
        void Commit();

        /// <summary>
        /// 事务回滚
        /// </summary>
        void Rollback();
          
    }
}
