using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Dapper.Contrib.Extensions.Option;
using DapperInfrastructure.DapperWrapper.UnitOfWork;
using DapperInfrastructure.Extensions.Domain.Base;

namespace DapperInfrastructure.DapperWrapper.Repository
{ 
    /// <summary>
    /// CRUD 仓储 实例
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public  class DapperRepositoryExBase<TEntity> : DapperRepositoryBase<TEntity>, IRepository<TEntity> where TEntity : EntityByType
    {
        public DapperRepositoryExBase(DapperUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }



        #region 持久化

        /// <summary>
        /// 创建实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public virtual TEntity Create(TEntity entity)
        {
            UnitOfWork.GetOpenConnection();
            UnitOfWork.DbConnection.Insert(entity, UnitOfWork.DbTransaction);
            return entity;
        }

        /// <summary>
        /// 创建实体 (批量)
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public virtual void CreateBatch(TEntity[] entity)
        {
            if (entity.Any())
            {
                UnitOfWork.GetOpenConnection();
                UnitOfWork.DbConnection.Insert(entity, UnitOfWork.DbTransaction);
            }
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public virtual TEntity Update(TEntity entity)
        {
            UnitOfWork.GetOpenConnection();
            UnitOfWork.DbConnection.Update(entity, UnitOfWork.DbTransaction);
            return entity;
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public virtual bool Delete(TEntity entity)
        {
            UnitOfWork.GetOpenConnection();
            return UnitOfWork.DbConnection.Delete(entity, UnitOfWork.DbTransaction);
        }

        #endregion
    }
}
