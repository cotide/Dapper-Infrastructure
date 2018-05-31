using System;
using System.Collections.Generic;
using System.Linq;
using Dapper.Contrib.Extensions;
using DapperInfrastructure.Extensions.Domain;
using DapperInfrastructure.Extensions.Domain.Base;
using TableAttribute = DapperInfrastructure.Extensions.Attr.TableAttribute;

namespace DapperInfrastructure.Extensions.Mapper
{
    /// <summary>
    /// Table Mapper 信息
    /// </summary>
    public static  class TableMaper
    {
         
        /// <summary>
        /// Table 表信息
        /// </summary>
        public static IDictionary<string, string> TableMapperDictionary;
          

        static TableMaper()
        {
            TableMapperDictionary  = new Dictionary<string, string>();
        }

        /// <summary>
        /// 获取Domain 实体
        /// </summary>  
        /// <returns></returns>
        public static string GetName<T>() where T : EntityByType
        {
            SqlMapperExtensions.TableNameMapper = TableNameMapper;
            return TableNameMapper(typeof(T));
        }

         
        #region Helper

        /// <summary>
        /// 获取表名
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        private static string TableNameMapper(Type type)
        {
             
            if (TableMapperDictionary != null && TableMapperDictionary.ContainsKey(type.FullName))
            {
                return TableMapperDictionary[type.FullName];
            }
            else
            { 

                var name = type.Name;

                var tableattr = type.GetCustomAttributes(false).OfType<TableAttribute>()
                    .FirstOrDefault() ;

                if (tableattr != null)
                {
                    name = tableattr.Name;
                    // 缓存
                    if (TableMapperDictionary != null && !TableMapperDictionary.ContainsKey(type.FullName))
                    {
                        TableMapperDictionary.Add(type.FullName, name);
                    }
                }
                else
                {
                    throw new ArgumentNullException(@"TableAttribute is null");
                }
                return name;

            }

        } 
        #endregion
    }
}
