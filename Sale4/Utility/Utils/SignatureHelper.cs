using System.Collections.Generic;
using System.Linq;
using Feature.YiGuo.Framework.Utility.Utils;
using Utility.Extensions;

namespace Utility.Utils
{
    /// <summary>
    /// 签名帮助类
    /// </summary>
    public class SignatureHelper
    {
        /// <summary>
        /// 计算参数签名
        /// </summary>
        /// <param name="parameters">参数集合</param>
        /// <param name="appSecret">App密钥</param>
        /// <returns>签名</returns>
        public static string GetSignature(IDictionary<string, string> parameters, string appSecret)
        {
            parameters.Remove("Signature");
            parameters.Add("AppSecret", appSecret);

            // 先将参数以其参数名的字典序升序进行排序
            var sortedParams = new SortedDictionary<string, string>(parameters);

            //遍历排序后的字典，将所有参数按"key=value"格式拼接在一起
            
            var list = sortedParams.Select(item => item.Key + "=" + item.Value).ToList();
            
            var paramString = string.Join("&", list);

            string signature = EncryptHelper.MD5(paramString).ToUpper();

            return signature;
        }

        /// <summary>
        /// 将请求参数转换成字典集合再获取签名
        /// </summary>
        /// <param name="request"></param>
        /// <param name="appSecret"></param>
        /// <returns></returns>
        public static string GetSignature(object request, string appSecret)
        {
            var parameters = request.GetProperty();

            return GetSignature(parameters, appSecret);
        }
    }
}
