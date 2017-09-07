 
using DapperInfrastructure.Extensions.Domain.Base;

namespace DapperInfrastructure.DapperWrapper.Repository
{
    /// <summary>
    /// 持久化 CRUD
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : EntityByType
    {
        #region 持久化

        /// <summary>
        /// 创建实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        TEntity Create(TEntity entity);

        /// <summary>
        /// 创建实体 (批量)
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        void CreateBatch(TEntity[] entity);

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        TEntity Update(TEntity entity);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        bool Delete(TEntity entity);

        #endregion



        
    }
}