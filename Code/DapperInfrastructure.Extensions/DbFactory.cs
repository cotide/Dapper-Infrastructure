using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Dapper.Contrib.Extensions;
using DapperInfrastructure.Extensions.Attr;
using DapperInfrastructure.Extensions.Domain.Base;
using DapperInfrastructure.Extensions.Mapper; 
using TableAttribute = DapperInfrastructure.Extensions.Attr.TableAttribute;

namespace DapperInfrastructure.Extensions
{
    /// <summary>
    /// 数据库工厂
    /// </summary>
    public class DbFactory
    { 
        /// <summary>
        /// 数据库初始化
        /// </summary>
        public static void Init<T>() where T: EntityByType
        {
            if (SqlMapperExtensions.TableNameMapper != null)
                return;

            // 映射TableName
            SqlMapperExtensions.TableNameMapper = (type) =>
            {
                // 根据TableAttribute 获取表名
                var tableattr = type.GetCustomAttributes(false)
                .OfType<TableAttribute>().FirstOrDefault(); 
                var name = type.Name; 
                if (tableattr != null)
                {
                    
                    name = tableattr.Name;
                }
                return name; 
            };

            // 映射读取Column
            var domainList = typeof(T).Assembly.GetTypes().Where(x => x.IsSubclassOf(typeof(EntityByType)));
            foreach (var domain in domainList)
            {
                SqlMapper.SetTypeMap(domain, new CustomPropertyTypeMap(domain, (type, columnName) => type
                    .GetProperties()
                    .FirstOrDefault(prop => prop.GetCustomAttributes(false)
                    .OfType<ColumnAttribute>()
                        .Any(attr => attr.Name.Equals(columnName, StringComparison.OrdinalIgnoreCase))))); 
            } 

        }

    }
}
