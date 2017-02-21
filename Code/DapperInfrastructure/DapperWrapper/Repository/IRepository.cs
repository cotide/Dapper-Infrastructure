using System.Collections.Generic;
using DapperInfrastructure.Extensions.Domain;
using DapperInfrastructure.DapperWrapper.Pagination;
using DapperInfrastructure.DapperWrapper.SQLHelper;

namespace DapperInfrastructure.DapperWrapper.Repository
{
    /// <summary>
    /// basic CRUD
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity : EntityByType
    {
        #region 持久化

        /// <summary>
        /// 创建实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        TEntity Create(TEntity entity);

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
        TEntity Delete(TEntity entity);

        #endregion



        #region 获取列表数据



        /// <summary>
        /// 获取实体列表数据
        /// </summary>
        /// <param name="sql">SQL 对象</param>
        /// <returns></returns>
        IEnumerable<TEntity> GetList(Sql sql);



        /// <summary>
        /// 获取实体列表数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        IEnumerable<TEntity> GetList(string sql, params object[] param);


        /// <summary>
        /// 获取实体列表数据
        /// </summary>
        /// <param name="sql">SQL 对象</param>
        /// <returns></returns>
        IEnumerable<TDto> GetList<TDto>(Sql sql);


        /// <summary>
        /// 获取实体列表数据
        /// </summary>
        /// <param name="sql">SQL 对象</param>
        /// <param name="param"></param>
        /// <returns></returns>
        IEnumerable<TDto> GetList<TDto>(string sql, params object[] param);


        #endregion


         int Count(Sql sql);


        #region 获取单条数据


        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        TEntity Get(string sql, params object[] param);

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="sql">SQL 对象</param>
        /// <returns></returns>
        TEntity Get(Sql sql);

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        TDto Get<TDto>(string sql, params object[] param);

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="sql">SQL 对象</param>
        /// <returns></returns>
        TDto Get<TDto>(Sql sql);

        #endregion


        #region 分页处理

        /// <summary>
        /// 获取数据(分页)
        /// </summary>
        /// <param name="pageIndex">开始页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="sql">SQL语句</param>
        /// <returns></returns>
        PageList<TEntity> PageList(int pageIndex, int pageSize, Sql sql);


        /// <summary>
        /// 获取数据(分页)
        /// </summary>
        /// <typeparam name="TDto">返回泛型结果</typeparam>
        /// <param name="pageIndex">开始页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="sql">SQL语句</param>
        /// <returns></returns>
        PageList<TDto> PageList<TDto>(int pageIndex, int pageSize, Sql sql);

        #endregion
    }
}