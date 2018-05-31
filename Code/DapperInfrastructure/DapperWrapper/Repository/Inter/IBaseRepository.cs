using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperInfrastructure.DapperWrapper.UnitOfWork;

namespace DapperInfrastructure.DapperWrapper.Repository.Inter
{

    /// <summary>
    /// 数据库仓储
    /// </summary>
    public interface IBaseRepository
    {

        /// <summary>
        /// 业务对象实例
        /// </summary>
        RepoUnitOfWork DB { get; }
    }
}
