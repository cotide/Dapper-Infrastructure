using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DapperInfrastructure.Extensions.Attr;

namespace DapperInfrastructure.DapperWrapper.Core
{
    public  class ColumnPropertyInfo
    {
        public PropertyInfo PropertyInfo;

        public ColumnPropertyInfo(PropertyInfo property)
        {
            PropertyInfo = property;
             
            var propertieAttr =
                property.GetCustomAttributes(false)
                    .OfType<ColumnAttribute>()
                    .FirstOrDefault();
            if (propertieAttr != null)
            {
                ColumnName = propertieAttr.Name;
            }
            else
            {
                ColumnName = property.Name;
            }
        }

        /// <summary>
        /// 属性名
        /// </summary>
        public string Name
        {
            get { return PropertyInfo.Name; }
        }

        /// <summary>
        /// 字段名
        /// </summary>
        public string ColumnName { get; }
         
    }
}
