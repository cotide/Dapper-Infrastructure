using System;

namespace DapperInfrastructure.Extensions.Attr.Desc
{
    /// <summary>
    /// 实体描述特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class,AllowMultiple = false,Inherited = true)]
    public class EntityDescAttribute : BaseEntityDescAttribute
    { 
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="description">描述</param>
         public EntityDescAttribute(string description)
             : base(description){ 
        }
    }
}
