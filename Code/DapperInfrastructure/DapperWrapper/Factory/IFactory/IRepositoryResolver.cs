using DapperInfrastructure.DapperWrapper.Repository;
using DapperInfrastructure.DapperWrapper.UnitOfWork;
using DapperInfrastructure.Extensions.Domain;
using DapperInfrastructure.Extensions.Domain.Base;

namespace DapperInfrastructure.DapperWrapper.Factory.IFactory
{
    /// <summary>
    /// 仓储接口
    /// </summary>
    public interface IRepositoryResolver
    { 

        /// <summary>
        /// 后去仓储对象
        /// </summary>
        /// <typeparam name="TEntity">仓储对象</typeparam>
        /// <param name="unitOfWork"></param>
        /// <returns></returns>
        IRepository<TEntity> GetRepositoryForEntity<TEntity>(IUnitOfWork unitOfWork) where TEntity : EntityByType;
    }
}
