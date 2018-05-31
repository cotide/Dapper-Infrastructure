using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BH.Domain.Entity.Base;
using DapperInfrastructure.Extensions.Attr;

namespace BH.Domain.Entity.Category
{

    /// <summary>
    /// 应用
    /// </summary>
    [Table("ApplicationMTR")]
    public class ApplicationMtr : EntityWidthStringType 
    {

        /// <summary>
        /// 应用名称
        /// </summary> 
        [Column("Name")]
        public string Name { get; set; }


        /// <summary>
        /// 所属分类应用
        /// </summary>
        [Column("CategoryId")]
        public int CategoryId { get; set; }


        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("CreateTime")]
        public DateTime CreateTime { get; set; }
    }
}
