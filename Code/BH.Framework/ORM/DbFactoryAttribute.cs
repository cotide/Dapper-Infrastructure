using System;
using System.Configuration;
using System.Linq;

namespace BH.Framework.ORM
{
    /// <summary>
    /// 数据库特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class DbFactoryAttribute : Attribute
    {
        public readonly string ConnectionName;

        public DbFactoryAttribute(string connectionName)
        {
            ConnectionName = connectionName;
        }

        public static string GetKeyFrom(object target)
        {
            const string defaultConnectionName = "DefaultDB";
            var objectType = target.GetType();
            var attributes = objectType.GetCustomAttributes(typeof(DbFactoryAttribute), true);
            if (attributes.Length > 0)
            {
                var attribute = (DbFactoryAttribute)attributes[0];
                return attribute.ConnectionName;
            }

            var namespaces = objectType.Namespace;
            if (string.IsNullOrEmpty(namespaces))
                return defaultConnectionName;
            if (namespaces.ToLower().EndsWith("data") || namespaces.ToLower().EndsWith("dao") || namespaces.ToLower().EndsWith("daos") || namespaces.ToLower().EndsWith("daoes"))
                return defaultConnectionName;

            return namespaces.Split(new[] { "." }, StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
        }



        public static string GetKeyFrom(Type type)
        {
            const string defaultConnectionName = "DefaultDB";
            var objectType = type;
            var attributes = objectType.GetCustomAttributes(typeof(DbFactoryAttribute), true);
            if (attributes.Length > 0)
            {
                var attribute = (DbFactoryAttribute)attributes[0];
                return attribute.ConnectionName;
            }

            var namespaces = objectType.Namespace;
            if (string.IsNullOrEmpty(namespaces))
                return defaultConnectionName;
            if (namespaces.ToLower().EndsWith("data") || namespaces.ToLower().EndsWith("dao") || namespaces.ToLower().EndsWith("daos") || namespaces.ToLower().EndsWith("daoes"))
                return defaultConnectionName;

            return namespaces.Split(new[] { "." }, StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
        }


        public static string GetDBName(Type type)
        {
            var connKey = DbFactoryAttribute.GetKeyFrom(type);
            if (ConfigurationManager.ConnectionStrings[connKey] != null)
            {
                var conn = ConfigurationManager.ConnectionStrings[connKey].ConnectionString;
                var zu = conn.Split(';');
                var value = zu.FirstOrDefault(x => x.StartsWith("database="));
                if (!string.IsNullOrEmpty(value))
                {
                    return value.Split('=')[1];
                }
                throw new Exception("无效的数据库名");
            }
            else
            {
                throw new Exception("无效的数据库名");
            }
        }
    }
}
