using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BH.Domain.Input.Category
{
    /// <summary>
    /// 新增分类树 Input
    /// </summary>
    public class AddCategoryFamilyInput
    {
        /// <summary>
        /// 编码
        /// </summary>		  
        [Required(ErrorMessage = "编码")] 
        public  string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>		 
        [Required(ErrorMessage = "名称")]
        public  string Name { get; set; }

        /// <summary>
        /// 所属分类应用ID
        /// </summary>		
        [Required(ErrorMessage = "所属分类应用ID")]
        public  int CategoryApplicationID { get; set; }


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
