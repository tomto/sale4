using Feature.YiGuo.Framework.Utility.Utils;
using Utility.Utils;

namespace Utility.Constants
{
    /// <summary>
    /// 常量名称
    /// </summary>
    public static class VariableName
    {
        /// <summary>
        /// 上下文唯一Key
        /// </summary>
        public static string ContextKey = string.Format("{0}.{1}", ConfigHelper.GetValue("AppId"), "ContextKey");

        /// <summary>
        /// action请求统计名称
        /// </summary>
        public const string StopwatchKey = "action.log.stopwatch";

        /// <summary>
        /// 操作人Key
        /// </summary>
        public static string OperatorKey = string.Format("{0}.{1}", ConfigHelper.GetValue("AppId"), "Operator");
    }
}
