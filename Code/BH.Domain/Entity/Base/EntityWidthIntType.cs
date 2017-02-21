using DapperInfrastructure.Extensions.Domain;

namespace BH.Domain.Entity.Base
{
    /// <summary>
    /// 基于类型Int的Domain实体
    /// </summary>
    public abstract class EntityWidthIntType : EntityByType<int>
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Dapper.Contrib.Extensions.Key]
        public override int ID { get; set; }

    }
}
