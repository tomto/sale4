using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Utility.Serialize
{
    /// <summary>
    /// 
    /// </summary>
    public class XmlSerialize
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string xml)
        {
            var t = default(T);
            var serialize = new XmlSerializer(typeof(T));
            var xDocument = XDocument.Parse(xml, LoadOptions.SetBaseUri | LoadOptions.SetLineInfo);

            using (var memoryStream = new MemoryStream())
            {
                xDocument.Save(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);
                t = (T)serialize.Deserialize(memoryStream);
            }

            return t;
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string Serialize<T>(T t)
        {
            var xml = string.Empty;
            var serialize = new XmlSerializer(typeof(T));
            using (var memoryStream = new MemoryStream())
            {
                serialize.Serialize(memoryStream, t);
                memoryStream.Seek(0, SeekOrigin.Begin);
                using (var streamReader = new StreamReader(memoryStream))
                {
                    xml = streamReader.ReadToEnd();
                }
            }
            return xml;
        }
    }
}
