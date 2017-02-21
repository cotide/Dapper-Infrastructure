using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BH.Domain.Dtos.Category
{

    /// <summary>
    /// 分类树 DTO （全结构）
    /// </summary>
    public class CategoryFamilyFullDto
    {

        /// <summary>
        /// ID
        /// </summary>
        public  Guid? ID { get; set; }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public CategoryFamilyFullDto()
        {
            Item = new List<CategoryFamilyFullDto>();
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


        /// <summary>
        /// 子项
        /// </summary>
        public IList<CategoryFamilyFullDto> Item;
    }
}
