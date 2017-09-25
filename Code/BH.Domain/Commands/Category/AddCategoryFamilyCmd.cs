using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BH.Framework.Utility;

namespace BH.Domain.Commands.Category
{
    /// <summary>
    /// 创建分类树命令
    /// </summary>
    public class AddCategoryFamilyCmd
    {
        /// <summary>
        /// 编码
        /// </summary>		 
        public readonly string Code;

        /// <summary>
        /// 名称
        /// </summary>		 
        public readonly string Name;

        /// <summary>
        /// 所属分类应用ID
        /// </summary>		 
        public readonly int CategoryApplicationID;


        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="code">编码</param>
        /// <param name="name">名称</param>
        /// <param name="categoryApplicationId">所属分类应用ID</param>
        public AddCategoryFamilyCmd(
             string code,
             string name,
             int categoryApplicationId)
        {
            Guard.IsNotNullOrEmpty(code, "code");
            Guard.IsNotNullOrEmpty(name, "name");
            Guard.IsNotZeroOrNegative(categoryApplicationId, "categoryApplicationId");
            Code = code;
            CategoryApplicationID = categoryApplicationId;
            Name = name;
            IsEffective = true;
        }


        /// <summary>
        /// 前缀码
        /// </summary>		 
        public string PrefixCode { get; set; }

        /// <summary>
        /// 是否最后
        /// </summary>		 
        public bool IsLast { get; set; }

        /// <summary>
        /// 父级
        /// </summary>		 
        public Guid? ParentID { get; set; }

     
        /// <summary>
        /// 排序
        /// </summary>		 
        public int OrderNr { get; set; }

        /// <summary>
        /// 描述
        /// </summary>		 
        public string Description { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>		 
        public bool IsEffective { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>		 
        public DateTime EffectiveStart { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>		 
        public DateTime EffectiveEnd { get; set; }

    }
}
