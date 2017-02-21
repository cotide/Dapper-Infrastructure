using System;

namespace DapperInfrastructure.Extensions.Attr.Desc
{
    /// <summary>
    /// 抽象 - 实体描述
    /// </summary>
    public abstract class BaseEntityDescAttribute : Attribute
    {
        /// <summary>
        /// 描述
        /// </summary>
        public readonly string Description;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="description">描述</param> 
        protected BaseEntityDescAttribute(string description)
        {
            Description = description;
        }
    }
}
