using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace DapperInfrastructure.DapperWrapper
{
    /// <summary>
    /// 数据库工厂
    /// </summary>
    public class DbFactory
    {
        /// <summary>
        /// 数据库初始化
        /// </summary>
        public  static void Init()
        {
            if (SqlMapperExtensions.TableNameMapper != null)
                return;

            SqlMapperExtensions.TableNameMapper = (type) =>
            {
                var tableattr = type.GetCustomAttributes(false).SingleOrDefault(attr => attr.GetType().Name == "TableAttribute") as dynamic;
                if (tableattr != null)
                    return tableattr.Name;

                var name = type.Name;
                if (type.IsInterface && name.StartsWith("I"))
                    name = name.Substring(1);
                return name;
            };
        }

    }
}
