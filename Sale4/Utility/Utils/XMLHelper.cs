using System.IO;
using System.Xml.Serialization;

namespace Utility.Utils
{
    /// <summary>
    /// XML帮助类
    /// </summary>
    public class XmlHelper
    {
        /// <summary>
        /// 保存xml文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="sourceObj"></param>
        /// <param name="xmlRootName"></param>
        public static void SaveToXml(string filePath, object sourceObj, string xmlRootName = "")
        {
            if (!string.IsNullOrWhiteSpace(filePath) && sourceObj != null)
            {
                var type = sourceObj.GetType();

                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    XmlSerializerNamespaces ns = new XmlSerializerNamespaces();

                    ns.Add("", "");

                    XmlSerializer xmlSerializer =
                        new XmlSerializer(type);

                    xmlSerializer.Serialize(writer, sourceObj, ns);
                }
            }
        }

        /// <summary>
        /// 加载xml文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T LoadFromXml<T>(string filePath)
        {
            object result = null;

            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
                    result = xmlSerializer.Deserialize(reader);
                }
            }

            return (T)result;
        }
    }
}
