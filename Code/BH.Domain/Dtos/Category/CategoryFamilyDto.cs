using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BH.Domain.Dtos.Category
{
    /// <summary>
    /// 分类树 DTO
    /// </summary>
    public class CategoryFamilyDto
    {
        public CategoryFamilyDto()
        {
          
        }


        /// <summary>
        /// 编码
        /// </summary>		 
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>		 
        public string Name { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        public Guid? ParentId { get; set; }



    }
}
