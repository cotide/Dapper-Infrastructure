using System.Collections.Generic;
using System.Linq; 
using Dapper;
using Dapper.Contrib.Extensions;
using DapperInfrastructure.DapperWrapper.Pagination;
using DapperInfrastructure.DapperWrapper.SQLHelper;
using DapperInfrastructure.DapperWrapper.UnitOfWork;
using DapperInfrastructure.Extensions.Collections;
using DapperInfrastructure.Extensions.Domain;
using DapperInfrastructure.Extensions.Domain.Base;

namespace DapperInfrastructure.DapperWrapper.Repository
{

    /// <summary>
    /// CRUD 仓储 实例
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class DapperRepositoryBase<TEntity> : SQL.SqlQueryBase where TEntity : EntityByType
    {

        public DapperRepositoryBase(DapperUnitOfWork unitOfWork) : base(unitOfWork)
        {
             
            EnableNamedParams = true;
            ForceDateTimesToUtc = true;
        }





        #region 获取列表数据

        /// <summary>
        /// 获取实体列表数据
        /// </summary>
        /// <param name="sql">SQL 对象</param> 
        /// <returns></returns>
        public IList<TEntity> GetList()
        {
            return GetList<TEntity>(Sql.Builder.Select("*").From<TEntity>());
        }

        /// <summary>
        /// 获取实体列表数据
        /// </summary>
        /// <param name="sql">SQL 对象</param> 
        /// <returns></returns>
        public IList<TEntity> GetList(Sql sql)
        {
            return GetList<TEntity>(sql);
        }
 
        /// <summary>
        /// 获取实体列表数据
        /// </summary>
        /// <param name="sql"></param> 
        /// <param name="param"></param>
        /// <returns></returns>
        public IList<TEntity> GetList(string sql, params object[] param)
        {
            return GetList<TEntity>(sql, param);
        }

     

        #endregion

        #region 获取实体数据


        /// <summary>
        /// 根据Id获取实体对象
        /// </summary>
        /// <param name="primaryKey">主键值</param>
        /// <returns></returns>
        public TEntity GetById(object primaryKey)
        {
            UnitOfWork.GetOpenConnection();
            return UnitOfWork.DbConnection.Get<TEntity>(primaryKey, UnitOfWork.DbTransaction);
        }
 

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="sql">SQL语句</param> 
        /// <param name="param">参数</param>
        /// <returns></returns>
        public TEntity Get(string sql, params object[] param)
        {
            return base.Get<TEntity>(sql, param);
        }

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="sql">SQL 对象</param>
        /// <returns></returns>
        public TEntity Get(Sql sql)
        {
            return base.Get<TEntity>(sql);
        }

    

        #endregion


        #region 分页获取数据

        /// <summary>
        /// 获取数据(分页)
        /// </summary>
        /// <param name="pageIndex">开始页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="sql">SQL语句</param>
        /// <returns></returns>
        public PageList<TEntity> PageList(int pageIndex, int pageSize, Sql sql)
        {
            var pageData = Page<TEntity>(pageIndex, pageSize, sql.SQL, sql.CountField, sql.Arguments);
            var result = pageData.GetPage<TEntity>(pageIndex, pageSize);
            return result;
        }


        #endregion
         
    }
}