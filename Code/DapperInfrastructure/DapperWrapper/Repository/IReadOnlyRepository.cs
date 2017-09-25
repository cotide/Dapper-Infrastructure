using System.Collections.Generic;
using DapperInfrastructure.DapperWrapper.SQLHelper;
using DapperInfrastructure.Extensions.Collections; 
using DapperInfrastructure.Extensions.Domain.Base;

namespace DapperInfrastructure.DapperWrapper.Repository
{
    /// <summary>
    /// 只读 CRUD
    /// </summary>
    public interface IReadOnlyRepository<TEntity> where TEntity : EntityByType
    {
        #region 获取列表数据

        /// <summary>
        /// 获取实体列表数据
        /// </summary>
        /// <param name="sql">SQL 对象</param> 
        /// <returns></returns>
        IList<TEntity> GetList();

        /// <summary>
        /// 获取实体列表数据
        /// </summary>
        /// <param name="sql">SQL 对象</param> 
        /// <returns></returns>
        IList<TEntity> GetList(Sql sql);
         

        /// <summary>
        /// 获取实体列表数据
        /// </summary>
        /// <param name="sql"></param> 
        /// <param name="param"></param>
        /// <returns></returns>
        IList<TEntity> GetList(string sql, params object[] param);

         

        /// <summary>
        /// 获取实体列表数据
        /// </summary>
        /// <param name="sql">SQL 对象</param>
        /// <returns></returns>
        IList<TDto> GetList<TDto>(Sql sql);



        /// <summary>
        /// 获取实体列表数据
        /// </summary>
        /// <param name="sql">SQL 对象</param>
        /// <param name="param"></param>
        /// <returns></returns>
        IList<TDto> GetList<TDto>(string sql, params object[] param);


        #endregion

        /// <summary>
        /// 统计总数
        /// </summary>
        /// <param name="sql">SQL 对象</param> 
        /// <returns></returns>
        int Count(Sql sql);



        #region 获取单条数据

        /// <summary>
        /// 根据Id获取实体对象
        /// </summary>
        /// <param name="primaryKey">主键值</param>
        /// <returns></returns>
        TEntity GetById(object primaryKey);

 

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="sql">SQL语句</param> 
        /// <param name="param">参数</param>
        /// <returns></returns>
        TEntity Get(string sql,  params object[] param);

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="sql">SQL 对象</param>
        /// <returns></returns>
        TEntity Get(Sql sql); 


        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="sql">SQL 对象</param>
        /// <returns></returns>
        TDto Get<TDto>(Sql sql);

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        TDto Get<TDto>(string sql, params object[] param);

 

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
