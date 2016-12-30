using System.Configuration;
using Utility.Serialize;

namespace Utility.Configuration
{
    /// <summary>
    /// 泛型配置块处理程序
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ConfigurationSectionHandler<T> : IConfigurationSectionHandler where T : class
    {
        /// <summary>
        /// 设置
        /// </summary>
        public static T Settings
        {
            get
            {
                return settings ?? (settings = ConfigurationManager.GetSection(typeof(T).Name) as T);
            }
        }

        static T settings;

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="configContext"></param>
        /// <param name="section"></param>
        /// <returns></returns>
        public virtual object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            return XmlSerialize.Deserialize<T>(section.OuterXml);
        }
    }
}
