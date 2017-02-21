using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BH.Domain.Entity.Base;

namespace BH.Domain.Entity.Category
{

    /// <summary>
    /// 应用
    /// </summary>
    [Dapper.Contrib.Extensions.Table("ApplicationMTR")]
    public class ApplicationMtr : EntityWidthStringType 
    {
       
        /// <summary>
        /// 应用名称
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// 所属分类应用
        /// </summary>
        public int CategoryId { get; set; }


        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
