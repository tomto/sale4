using System;
using Newtonsoft.Json;

namespace Feature.YiGuo.Framework.Utility.Utils
{
    /// <summary>
    /// Nlog本地日志记录
    /// 日志公共类
    /// </summary>
    public class LogHelper
    {
        /// <summary>
        /// localAllLog本地版
        /// </summary>
        private readonly NLog.Logger localLog;
        private readonly static LogHelper Errorlog = new LogHelper("error");
        private readonly static LogHelper InfoLog = new LogHelper("info");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loggerName"></param>
        public LogHelper(string loggerName = "default", bool isCurrent = false)
        {
            if (isCurrent)
            {
                localLog = NLog.LogManager.GetCurrentClassLogger();
            }
            else
            {
                localLog = NLog.LogManager.GetLogger(loggerName);
            }
        }

        /// <summary>
        /// 写入提示日志
        /// </summary>
        /// <param name="content"></param>
        /// <param name="loggerName"></param>
        public static void WriteInfo(dynamic content, string loggerName = "info")
        {
            if (!loggerName.Equals("info"))
            {
                LogHelper log = new LogHelper(loggerName);
                log.Info(content);
            }
            else
            {
                InfoLog.Info(content);
            }
        }

        /// <summary>
        /// 写入错误提示
        /// </summary>
        /// <param name="ex"></param>
        public static void WriteError(Exception ex)
        {
            Errorlog.Error(ex);
        }

        /// <summary>
        /// 写入提示信息
        /// </summary>
        public void Info(dynamic content)
        {
            localLog.Info(BuildContent(content));
        }

        /// <summary>
        /// 写入调试信息
        /// </summary>
        public void Debug(dynamic content)
        {
            localLog.Debug(BuildContent(content));
        }

        /// <summary>
        /// 写入异常
        /// </summary>
        /// <param name="ex"></param>
        public void Error(Exception ex)
        {
            localLog.Error(ex);
        }

        /// <summary>
        /// 创建内容
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        private dynamic BuildContent(dynamic content)
        {
            if (content.GetType().IsClass == true && content.GetType().Name != "String" && content.GetType().Name != "Int32")
            {
                return JsonConvert.SerializeObject(content);
            }

            return content;
        }
    }
}