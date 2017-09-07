using System;
using DapperInfrastructure.Extensions.Helper;

namespace DapperInfrastructure.Extensions.Domain.Base
{
    public class EntityWidthStringType : EntityByType<string>
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public EntityWidthStringType()
        {
            // 默认初始化
            Id = Guid.NewGuid().ToId();
        }

        [Dapper.Contrib.Extensions.ExplicitKey]
        public override string Id { get; set; }
         
    }
}
