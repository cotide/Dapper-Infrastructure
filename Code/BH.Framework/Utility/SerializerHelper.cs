#region Using

using System.IO;
using System.Text;
using System.Xml.Serialization;

#endregion

namespace BH.Framework.Utility
{
    ///<summary>
    /// 序列化辅助类
    ///</summary>
    public class SerializerHelper
    {

        /// <summary>
        /// 将XML字符串反序列化为一个对象
        /// </summary>
        /// <typeparam name="T">对象的类型</typeparam>
        /// <param name="xml">需要反序列化的XML字符串</param>
        /// <returns>对象的实例</returns>
        public static T XmlToObject<T>(string xml) where T : class
        {
            using (var ms = new MemoryStream())
            {
                using (var sr = new StreamWriter(ms, Encoding.UTF8))
                {
                    sr.Write(xml);
                    sr.Flush();
                    ms.Seek(0, SeekOrigin.Begin);
                    var serializer = new XmlSerializer(typeof(T));
                    return serializer.Deserialize(ms) as T;
                }
            }
        }


        /// <summary>
        /// 序列化对象为XML字符串
        /// </summary>
        /// <param name="instance">需要序列化的对象</param>
        /// <returns>对象序列化之后生成的XML字符串</returns>
        public static string ObjectToXml<T>(T instance)
        {
            using (var ms = new MemoryStream())
            {
                var ns = new XmlSerializerNamespaces();

                ns.Add(string.Empty, string.Empty);
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(ms, instance, ns);
                ms.Seek(0, SeekOrigin.Begin);
                using (var reader = new StreamReader(ms, Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// 序列化成xml字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>序列化后的字符串</returns>
        public static string ObjectToXml(object obj)
        {
            var xs = new XmlSerializer(obj.GetType());
            using (var ms = new MemoryStream())
            {
                var xtw = new System.Xml.XmlTextWriter(ms, System.Text.Encoding.UTF8);
                xtw.Formatting = System.Xml.Formatting.Indented; 
                xs.Serialize(xtw, obj);
                ms.Seek(0, SeekOrigin.Begin);
                using (var sr = new StreamReader(ms))
                {
                    string str = sr.ReadToEnd();
                    xtw.Close();
                    ms.Close();
                    return str;
                }
            }
        }

    }
}