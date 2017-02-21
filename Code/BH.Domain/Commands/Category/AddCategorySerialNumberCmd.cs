using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BH.Domain.Commands.Category
{
    /// <summary>
    /// 新增取号规则
    /// </summary>
    public class AddCategorySerialNumberCmd
    {

        /// <summary>
        /// 分类树ID
        /// </summary>
        public readonly Guid CategoryID;
         
        /// <summary>
        /// 
        /// </summary>
        public  int SerialNrLength { get; set; }
         

        /// <summary>
        /// 
        /// </summary>
        public int NextNr { get; set; }

    }
}
 