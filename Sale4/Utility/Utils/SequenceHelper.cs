using System;

namespace Utility.Utils
{
    /// <summary>
    /// 自动产生序列号帮助类
    /// </summary>
    public class SequenceHelper
    {
        /// <summary>
        /// 创建创建一个新的编号(前缀+yyMMddHHmmss+3位随机数)
        /// <param name="prefix">前缀(1或者2个字符 例如（XS）)</param>
        /// </summary>
        public static string GetCode(string prefix)
        {
            var ran = new Random(Guid.NewGuid().GetHashCode());

            return string.Format("{0}{1}{2}", prefix, DateTime.Now.ToString("yyMMddHHmmss"), ran.Next(100, 999));
        }

        /// <summary>
        /// 创建创建一个新的编号 前缀+序列化+3位随机数
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="sequence"></param>
        /// <returns></returns>
        public static string GetCode(string prefix, long sequence)
        {
            var ran = new Random(Guid.NewGuid().GetHashCode());

            return string.Format("{0}{1}{2}", prefix, sequence, ran.Next(100, 999));
        }
    }
}
