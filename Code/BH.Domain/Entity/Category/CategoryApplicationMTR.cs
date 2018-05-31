using BH.Domain.Entity.Base;
using DapperInfrastructure.Extensions.Attr;

namespace BH.Domain.Entity.Category
{

    /// <summary>
    /// 应用分类
    /// </summary> 
    [Table("CategoryApplicationMTR")]
    public class CategoryApplicationMtr : EntityWidthIntType 
	{

        /// <summary>
        /// 分类名称
        /// </summary>	  
        [Column("Name")]
        public string Name { get;set;}     
			   
	}
}

