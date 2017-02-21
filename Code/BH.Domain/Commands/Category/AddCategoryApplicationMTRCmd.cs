using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BH.Framework.Utility;

namespace BH.Domain.Commands.Category
{

    /// <summary>
    /// 创建分类应用命令
    /// </summary>
    public class AddCategoryApplicationMTRCmd
    {
        /// <summary>
        /// 分类名称
        /// </summary>

        public readonly string Name;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">分类名称</param>
        public AddCategoryApplicationMTRCmd(string name)
        {
            Guard.IsNotNullOrEmpty(name,"name");
            Name = name;
        }
    }
}
