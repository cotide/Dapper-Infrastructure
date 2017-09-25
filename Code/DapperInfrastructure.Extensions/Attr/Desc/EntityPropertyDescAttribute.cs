using System;

namespace DapperInfrastructure.Extensions.Attr.Desc
{
    /// <summary>
    /// 实体属性描述
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class EntityPropertyDescAttribute : BaseEntityDescAttribute
    {
        /// <summary> 
        /// 构造函数
        /// </summary>
        /// <param name="description">实体描述</param>
        public EntityPropertyDescAttribute(string description)
            : base(description)
        {
        }
    }
}
