using System;
using System.Collections.Generic;
using System.Linq;
using DapperInfrastructure.Extensions.Domain;
using DapperInfrastructure.DapperWrapper.IFactory;
using DapperInfrastructure.DapperWrapper.Repository;
using DapperInfrastructure.DapperWrapper.UnitOfWork;

namespace DapperInfrastructure.DapperWrapper.Factory
{
    /// <summary>
    /// 仓储实例
    /// </summary>
    public class RepositoryResolver : IRepositoryResolver
    { 
        private static readonly RepositoryResolver _instance = new RepositoryResolver();

        /// <summary>
        /// 实例 (单例模式)
        /// </summary>
        public static RepositoryResolver Instance { get { return _instance; } }

        /// <summary>
        /// 缓存对象
        /// </summary>
        public Dictionary<Type, Type> Repositories = new Dictionary<Type, Type>();

        /// <summary>
        /// 默认构造函数
        /// </summary>
        private RepositoryResolver()
        {
            
        }


        /// <summary>
        /// 获取仓储对象
        /// </summary>
        /// <typeparam name="TEntity">领域对象</typeparam>
        /// <returns></returns>
        public IRepository<TEntity> GetRepositoryForEntity<TEntity>(IUnitOfWork unitOfWork) where TEntity : EntityByType
        {
            var type = typeof(DapperRepositoryBase<TEntity>);

            if (Instance != null && Repositories.Any(x => x.Key == type))
            {
                var foundInterface = Repositories.Single(x => x.Key == type);
                if (foundInterface.Key != null)
                    return (DapperRepositoryBase<TEntity>)Activator.CreateInstance(foundInterface.Value, this);
            }

            return new DapperRepositoryBase<TEntity>(unitOfWork as DapperUnitOfWork);
        }
    }
}
