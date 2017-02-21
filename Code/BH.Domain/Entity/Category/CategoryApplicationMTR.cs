using BH.Domain.Entity.Base;
using BH.Framework.Swagger;

namespace BH.Domain.Entity.Category
{

    /// <summary>
    /// 应用分类
    /// </summary> 
    [Dapper.Contrib.Extensions.Table("CategoryApplicationMTR")]
    public class CategoryApplicationMtr : EntityWidthIntType 
	{

        /// <summary>
        /// 分类名称
        /// </summary>	  
        public string Name { get;set;}     
			   
	}
}

