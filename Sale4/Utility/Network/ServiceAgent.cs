using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Utility.Network
{
    /// <summary>
    /// .Net Http Service代理
    /// </summary>
    public static class ServiceAgent
    {
        #region Fields

        private const string MediaType = "application/json";
        private static Uri uri;

        #endregion

        #region 私有方法

        /// <summary>
        /// 根据请求方式发送请求
        /// </summary>
        /// <param name="uri">服务器地址</param>
        /// <param name="httpContent"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        private static Task<HttpResponseMessage> RequestByMethod(Uri uri, StringContent httpContent, HttpClient client, HttpMethod method)
        {
            Task<HttpResponseMessage> task = null;
            switch (method)
            {
                case HttpMethod.Get:
                    task = client.GetAsync(uri);
                    break;
                case HttpMethod.Put:
                    task = client.PutAsync(uri, httpContent);
                    break;
                case HttpMethod.Delete:
                    task = client.DeleteAsync(uri);
                    break;
                default:
                    task = client.PostAsync(uri, httpContent);
                    break;
            }

            return task;
        }

        /// <summary>
        /// 序列化Post请求参数
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static string RequestParamSerializeJson(dynamic obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        #endregion

        #region Methods

        /// <summary>
        /// 发送http请求
        /// </summary>
        /// <param name="svcUrl"></param>
        /// <param name="requestContent">请求参数</param>
        /// <param name="method"></param>
        /// <param name="timeOut">超时设置（单位秒）</param>
        /// <returns></returns>
        public static string SendRequest(string svcUrl, dynamic requestContent,
            HttpMethod method = HttpMethod.Post, int timeOut = 0)
        {
            return SendRequest<string>(svcUrl, requestContent, method, timeOut);
        }

        /// <summary>
        /// 发送http请求
        /// </summary>
        /// <param name="svcUrl"></param>
        /// <param name="requestContent">请求参数</param>
        /// <param name="method"></param>
        /// <param name="timeOut">超时设置（单位秒）</param>
        /// <returns></returns>
        public static TResponse SendRequest<TResponse>(string svcUrl, dynamic requestContent, HttpMethod method = HttpMethod.Post, int timeOut = 0) where TResponse : class
        {
            //dynamic content = new ExpandoObject();
            var result = string.Empty;
            DateTime requestTime = DateTime.Now;
            uri = new Uri(svcUrl);
            using (HttpClient httpClient = new HttpClient())
            {
                if (timeOut > 0)
                {
                    httpClient.Timeout = new TimeSpan(0, 0, timeOut);
                }

                var contentString = requestContent != null ? RequestParamSerializeJson(requestContent) : "";

                using (StringContent httpContent = new StringContent(contentString, Encoding.UTF8, MediaType))
                {
                    var postTask = RequestByMethod(uri, httpContent, httpClient, method);
                    postTask.Wait();

                    if (!postTask.Result.IsSuccessStatusCode)
                    {
                        StringBuilder sb = new StringBuilder();

                        sb.AppendFormat("Status Code:{0}, Reason:{1}", postTask.Result.StatusCode, postTask.Result.ReasonPhrase);

                        sb.AppendLine();

                        sb.AppendFormat("Content:{0}", postTask.Result.Content);

                        sb.AppendLine();
                        sb.Append(result);

                        throw new Exception(sb.ToString());
                    }

                    var readTask = postTask.Result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    result = readTask.Result;
                    /*var responseTime = DateTime.Now;
                    content.RequestMilliseconds = (responseTime - requestTime).TotalMilliseconds;
                    content.RequestLength = contentString.Length;
                    content.ResponseLength = result.Length;
                    content.Result = result;*/
                }
            }
            if (typeof(TResponse).FullName == "System.String")
            {
                return result as TResponse;
            }
            if (!string.IsNullOrEmpty(result))
            {
                return JsonConvert.DeserializeObject<TResponse>(result);
            }

            return default(TResponse);
        }

        /// <summary>
        /// 通过HttpWebRequest发送Http请求
        /// </summary>
        /// <param name="url">请求的URL地址</param>
        /// <param name="requestParams">请求参数</param>
        /// <returns></returns>
        public static string Post(string url, Dictionary<string, string> requestParams)
        {
            var postDataStr = GetParam(requestParams);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            //request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
            //request.CookieContainer = cookie;
            Stream myRequestStream = request.GetRequestStream();
            StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
            myStreamWriter.Write(postDataStr);
            myStreamWriter.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //response.Cookies = cookie.GetCookies(response.ResponseUri);
            Stream myResponseStream = response.GetResponseStream();
            if (myResponseStream != null)
            {
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();

                return retString;
            }

            return "";
        }

        /// <summary>
        /// 通过HttpWebRequest 发送Get请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="requestParams"></param>
        /// <returns></returns>
        public static string Get(string url, Dictionary<string, string> requestParams = null)
        {
            var param = "";

            if (requestParams != null && requestParams.Any())
            {
                param = GetParam(requestParams);
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + (param == "" ? "" : "?") + param);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            if (myResponseStream != null)
            {
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();

                return retString;
            }
            return "";
        }

        private static string GetParam(IEnumerable<KeyValuePair<string, string>> requestParams)
        {
            return requestParams.Aggregate("", (current, item) => current + "&" + (item.Key + "=" + item.Value));
        }

        #endregion
    }

    /// <summary>
    /// 请求方式枚举
    /// </summary>
    public enum HttpMethod
    {
        /// <summary>
        /// GET请求方式
        /// </summary>
        Get,

        /// <summary>
        /// POST请求方式
        /// </summary>
        Post,

        /// <summary>
        /// PUT请求方式
        /// </summary>
        Put,

        /// <summary>
        /// DELETE请求方式
        /// </summary>
        Delete
    }
}
