using System;
using DapperInfrastructure.Extensions.Domain.Base;

namespace DapperInfrastructure.Extensions.Domain
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
            Id = Guid.NewGuid();
        }

        [Dapper.Contrib.Extensions.ExplicitKey]
        public override Guid Id { get; set; }

    }
}
