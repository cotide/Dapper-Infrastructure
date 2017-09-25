using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace BH.Domain.Input.Category
{
    /// <summary>
    /// 新增分类应用
    /// </summary>
    public class AddCategoryApplicationMtrInput
    {
        /// <summary>
        /// 分类应用名称
        /// </summary>
        /// <description>分类应用名称</description> 
        [Required(ErrorMessage = "作品ID必填")]
        [MaxLength(50, ErrorMessage = "作品ID小于50")]
        public string Name { get; set; }
    }
}
