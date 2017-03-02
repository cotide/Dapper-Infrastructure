using System; 
using DapperInfrastructure.Extensions.Domain;

namespace BH.Domain.Entity.Base
{
    /// <summary>
    /// 基于类型Guid的Domain实体
    /// </summary>
    public abstract class EntityWidthGuidType : EntityByType<Guid>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        protected EntityWidthGuidType()
        {
            ID = Guid.NewGuid();
        }

        [Dapper.Contrib.Extensions.ExplicitKey]
        public override Guid ID { get; set; }

    }
}
