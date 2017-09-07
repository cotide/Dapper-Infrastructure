using DapperInfrastructure.Extensions.Domain.Base;

namespace DapperInfrastructure.Extensions.Domain
{
    /// <summary>
    /// 基于类型Long的Domain实体
    /// </summary>
    public class EntityWidthLongType : EntityByType<long>
    {

        /// <summary>
        /// 主键ID
        /// </summary>
        [Dapper.Contrib.Extensions.Key]
        public override long Id { get; set; }
    }
}
