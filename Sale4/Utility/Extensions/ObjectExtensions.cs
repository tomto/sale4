using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace Utility.Extensions
{
    /// <summary>
    /// object对象扩展方法
    /// </summary>
    public static partial class ExtensionsHelper
    {
        /// <summary>
        /// 转换成Int32
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue">默认返回值</param>
        /// <returns></returns>
        public static int ToInt32(this object obj, int defaultValue = 0)
        {
            int result;

            if (!int.TryParse(obj.ToString(), out result))
            {
                result = defaultValue;
            }

            return result;
        }

        /// <summary>
        /// 转换成Int64
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static long ToInt64(this object obj, int defaultValue = 0)
        {
            long result;

            if (!long.TryParse(obj.ToString(), out result))
            {
                result = defaultValue;
            }
            return result;
        }

        /// <summary>
        /// 转换成Double
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static double ToDouble(this object obj)
        {
            double result;

            if (obj == null)
            {
                return 0;
            }

            double.TryParse(obj.ToString(), out result);

            return result;
        }

        public static decimal ToDecimal(this string str)
        {
            decimal result;

            var flag = decimal.TryParse(str.Trim(), out result);

            return flag ? result : 0m;
        }


        /// <summary>
        /// 将字符串转成decimal 型。
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static decimal ConvertToDecimal(this object str)
        {
            if (null == str)
            {
                return 0;
            }
            try
            {
                decimal num = Convert.ToDecimal(str);

                return num;
            }
            catch
            {
                return 0;
            }
        }
        /// <summary>
        /// 转换成Json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// 链接所有属性的值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="seperator"></param>
        /// <returns></returns>
        public static string JoinProperties(this object obj, string seperator = "")
        {
            List<string> valueList = new List<string>();

            PropertyInfo[] propertyInfos = obj.GetType().GetProperties();

            foreach (PropertyInfo pi in propertyInfos)
            {
                object value = pi.GetValue(obj, null);
                if (value != null)
                {
                    valueList.Add(string.Format("{0}={1}", pi.Name, value));
                }
            }

            return string.Join(seperator, valueList.ToArray());
        }

        /// <summary>
        /// 对象不能为空
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="msg"></param>
        public static void NotNullThrowException(this object obj, string msg = "对象不能为空")
        {
            if (obj == null || string.IsNullOrEmpty(obj + "")) throw new CustomAttributeFormatException(msg);
        }

        /// <summary>
        /// 对象是否为空
        /// </summary>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this object obj)
        {
            return obj == null;
        }

        /// <summary>
        /// 对象是否不为空不为空
        /// </summary>
        /// <returns></returns>
        public static bool IsNotNullOrEmpty(this object obj)
        {
            return obj != null;
        }

        #region decimal
        /// <summary>
        /// The dx number
        /// </summary>
        private static readonly String[] DxNum = { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖" };
        /// <summary>
        /// The dx yuan
        /// </summary>
        private static readonly String[] DxYuan = { "元", "拾", "佰", "仟", "万", "拾", "佰", "仟", "亿", "拾", "佰", "仟", "万" };
        /// <summary>
        /// The dw x
        /// </summary>
        private static readonly String[] DwX = { "角", "分" };

        /// <summary>
        /// 金额小写转中文大写。
        /// 整数支持到万亿；小数部分支持到分(超过两位将进行Banker舍入法处理)
        /// Author:Tinkerc
        /// CreateDate: 2014-09-20 23:32:02
        /// </summary>
        /// <param name="money">需要转换的双精度浮点数</param>
        /// <returns>转换后的字符串</returns>
        public static string ToMoneyString(this decimal money)
        {
            string NumStr;//整个数字字符串
            string NumStr_Zh;//整数部分
            string NumSr_X = "";//小数部分
            string NumStr_DQ;//当前的数字字符
            string NumStr_R = "";//返回的字符串

            money = Math.Round(money, 2);//四舍五入取两位
            //各种非正常情况处理
            if (money < 0)
                return "转换失败";
            else if (money > 9999999999999.99m)
                return "金额过大，无法转换";
            else if (money == 0)
                return DxNum[0] + DxYuan[0];
            else
            {
                NumStr = money.ToString();
                //分开整数与小数处理
                if (NumStr.IndexOf(".") != -1)
                {
                    NumStr_Zh = NumStr.Substring(0, NumStr.IndexOf("."));
                    NumSr_X = NumStr.Substring(NumStr.IndexOf(".") + 1);
                }
                else
                {
                    NumStr_Zh = NumStr;
                }
                //判断是否有整数部分
                if (long.Parse(NumStr_Zh) > 0)
                {
                    long len = NumStr_Zh.Length - 1;
                    //整数部分转换
                    for (int a = 0; a <= len; a++)
                    {
                        NumStr_DQ = NumStr_Zh.Substring(a, 1);
                        if (long.Parse(NumStr_DQ) != 0)
                        {
                            NumStr_R += DxNum[long.Parse(NumStr_DQ)] + DxYuan[len - a];
                        }
                        else
                        {
                            if ((len - a) == 0 || (len - a) == 4 || (len - a) == 8)
                                NumStr_R += DxYuan[len - a];
                            if ((a + 1) <= len)
                            {
                                NumStr_DQ = NumStr_Zh.Substring((a + 1), 1);
                                if (long.Parse(NumStr_DQ) == 0)
                                    continue;
                                else
                                    NumStr_R += DxNum[0];
                            }
                        }
                    }
                }
                //判断是否含有小数部分
                if (NumSr_X != "" && long.Parse(NumSr_X) > 0)
                {
                    //小数部分转换
                    for (int b = 0; b < NumSr_X.Length; b++)
                    {
                        NumStr_DQ = NumSr_X.Substring(b, 1);
                        if (int.Parse(NumStr_DQ) != 0)
                            NumStr_R += DxNum[long.Parse(NumStr_DQ)] + DwX[b];
                        else
                        {
                            if ((b + 1) < NumSr_X.Length)
                            {
                                NumStr_DQ = NumSr_X.Substring((b + 1), 1);
                                if (long.Parse(NumStr_DQ) == 0)
                                    continue;
                            }
                            if (b != (NumSr_X.Length - 1))
                                NumStr_R += DxNum[0];
                        }
                    }
                }
                else
                {
                    NumStr_R += "整";
                }
                return NumStr_R;
            }
        }

        #endregion

        #region 实体对象扩展

        /// <summary>
        /// 得到包含对象所有属性的字符串列表
        /// Author:Tinkerc
        /// CreateDate: 2014-09-20 23:32:02
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        public static List<string> GetPropertyList(this object model)
        {
            return model.GetType().GetProperties().Select(t => t.Name).ToList();
        }

        /// <summary>
        /// 连接实体对象的所有属性值
        /// Author:Tinkerc
        /// CreateDate: 2014-09-20 23:32:02
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="seperator">The seperator.</param>
        /// <returns>System.String.</returns>
        public static string JoinStringAll<T>(this T obj, string seperator = "")
        {
            PropertyInfo[] propertys = obj.GetType().GetProperties();

            string result = string.Join(seperator, (from pi in propertys select pi.GetValue(obj, null) into value where value != null select value.ToString()).ToArray());

            return result;
        }

        /// <summary>
        /// 复制一个model
        /// Author:Tinkerc
        /// CreateDate: 2014-09-20 23:32:02
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model">The model.</param>
        /// <returns>T.</returns>
        public static T Copy<T>(this T model) where T : class , new()
        {
            var result = new T();
            foreach (var p in model.GetPropertyList())
            {
                result.GetType().GetProperty(p).SetValue(result,
                    model.GetType().GetProperty(p).GetValue(model, null), null);
            }
            return result;
        }

        /// <summary>
        /// 将请求参数转换成字典集合
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetProperty<T>(this T model) where T : class ,new()
        {
            var parameters = model.GetPropertyList()
                .ToDictionary(
                item => item,
                item => (string)model.GetType().GetProperty(item).GetValue(model, null)
                );
            return parameters;
        }
        #endregion

        /// <summary>
        /// 返回是否默认选中
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static string GetChecked(this bool flag)
        {
            return flag ? "checked=\"checked\"" : "";
        }

        /// <summary>
        /// 返回是否默认选中
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static string GetSelected(this bool flag)
        {
            return flag ? "selected=\"selected\"" : "";
        }

        /// <summary>
        /// 根据int返回bool（1true，0false）
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool GetBool(this int value)
        {
            return value == 1;
        }

        /// <summary>
        /// 根据int返回bool（1 =true = 是，0=false=否）
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetBoolString(this int value)
        {
            return value == 1 ? "是" : "否";
        }

        /// <summary>
        /// 根据int返回bool（1 =true = 是，0=false=否）
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static string GetBoolString(this bool flag)
        {
            return flag ? "是" : "否";
        }

        /// <summary>
        /// 当值为0的时候返回空
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToAdvancedString(this int value)
        {
            return value > 0 ? value.ToString(CultureInfo.InvariantCulture) : string.Empty;
        }

        /// <summary>
        /// 当值为0的时候返回空
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToAdvancedString(this long value)
        {
            return value > 0 ? value.ToString(CultureInfo.InvariantCulture) : string.Empty;
        }

        /// <summary>
        /// 当值为0的时候返回空
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToAdvancedString(this decimal value, string format="")
        {
            return value > 0 ? value.ToString(format) : string.Empty;
        }

        /// <summary>
        /// 当值为0的时候返回空
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToAdvancedString(this double value, string format = "")
        {
            return value > 0 ? value.ToString(format) : string.Empty;
        }
    }
}
