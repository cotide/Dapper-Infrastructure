using DapperInfrastructure.DapperWrapper.UnitOfWork;

namespace DapperInfrastructure.DapperWrapper.Repository.Inter
{
    /// <summary>
    /// 数据库仓储 基类
    /// </summary>
    public abstract class BaseRepository : IBaseRepository
    {

        /// <summary>
        /// 业务对象实例
        /// </summary>
        public RepoUnitOfWork DB { get; set; }
      
    }
}
