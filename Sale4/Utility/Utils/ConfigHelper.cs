using System;
using System.Configuration;
using Feature.YiGuo.Framework.Utility.Utils;

namespace Utility.Utils
{
    /// <summary>
    /// web.config操作类
    /// Copyright (C) Maticsoft
    /// </summary>
    public sealed class ConfigHelper
    {
        /// <summary>
        /// 得到AppSettings中的配置字符串信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetValue(string key)
        {
            string value = string.Empty;
            string cacheKey = "AppSettings_" + key;
            object objModel = CacheHelper.GetCache(cacheKey);

            if (objModel != null) return objModel.ToString();

            try
            {
                objModel = ConfigurationManager.AppSettings[key];
                if (objModel != null)
                {
                    value = objModel.ToString();
                    CacheHelper.SetCache(cacheKey, objModel, DateTime.Now.AddMinutes(180), TimeSpan.Zero);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex);
            }

            return value;
        }
    }
}
