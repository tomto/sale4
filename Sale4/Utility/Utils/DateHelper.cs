using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Utility.Extensions;

namespace Utility.Utils
{
    public class DateHelper
    {        
        /// <summary>
        /// 检查指定日期的月份里面所有节假日和周末是否是工作日
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public static List<DateModel> GetHolidayWorkday(DateTime date)
        {
            //获取当月节假日
            var dates = GetHoliday(date);

            //获取周末是工作日的数据
            var result = GetWeekendIsWorkday(date, dates);

            return result;
        }

        /// <summary>
        /// 获取指定年份或者月份节日信息
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public static List<DateModel> GetHoliday(DateTime date)
        {
            var dateYearMonth = date.ToString("yyyyMM");

            var holiday = GetDateType(dateYearMonth);

            var jsonObject = holiday.ToJObject();
            List<DateModel> dates = new List<DateModel>();

            var holidyResult = jsonObject[dateYearMonth].ToString();
            if (holidyResult != "False")
            {
                var holidyDates = holidyResult.ToObjectFromJson<List<string>>();
                dates.AddRange(holidyDates.Select(item => string.Format("{0}/{1}/{2}", date.Year, date.Month, item.Substring(2))).Select(
                    holidyDate => new DateModel()
                {
                    Date = holidyDate.ToDatetime(),
                    DateType = 2
                }));
            }

            return dates;
        }

        /// <summary>
        /// 获取周末是工作日的数据
        /// </summary>
        /// <param name="date"></param>
        /// <param name="dates"></param>
        /// <returns></returns>
        public static List<DateModel> GetWeekendIsWorkday(DateTime date, List<DateModel> dates = null)
        {
            int days = DateTime.DaysInMonth(date.Year, date.Month);

            List<DateTime> weekendList = new List<DateTime>();

            if (dates == null)
            {
                dates = new List<DateModel>();
            }

            for (int i = 0; i < days; i++)
            {
                var curDate = date.AddDays(i);

                if (dates.Any(item => item.Date.Day == curDate.Day))
                {
                    continue;
                }

                if (curDate.DayOfWeek == DayOfWeek.Saturday || curDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    weekendList.Add(curDate);
                }
            }

            var result = GetDateType(weekendList.Select(item => item.ToString("yyyyMMdd")).ToArray());

            var holidyDates = result.ToObjectFromJson<Dictionary<string, int>>();

            foreach (var holidyDate in holidyDates.Where(item => item.Value == 0))
            {
                var dd = string.Format("{0}/{1}/{2}", date.Year, date.Month, holidyDate.Key.Substring(6));

                dates.Add(new DateModel()
                {
                    Date = dd.ToDatetime(),
                    DateType = holidyDate.Value,
                });
            }

            return dates;
        }

        /// <summary>
        /// 校验日期是否是工作日或者节假日
        /// </summary>
        /// <param name="dates">日期参数（2012/201201/20120101）</param>
        /// <returns>请求结果</returns>
        public static string GetDateType(params string[] dates)
        {
            /*
            单个日期返回数字
            0 工作日
            1 休息日
            2 节假日
            用法举例
            检查一个日期是否为节假日 ?d=20130101
            检查多个日期是否为节假日 ?d=20130101,20130103,20130105,20130201
            获取2012年1月份节假日 ?d=201201
            获取2012年所有节假日 ?d=2012
            获取2013年1/2月份节假日 ?d=201301,201302
             */
            const string holidayApiUrl = "http://apis.baidu.com/xiaogg/holiday/holiday";
            string param = "d=" + string.Join(",", dates);
            string strUrl = holidayApiUrl + '?' + param;

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(strUrl);
            webRequest.Method = "GET";
            // 添加header
            webRequest.Headers.Add("apikey", "b6a5372e959478fcb1adfaa296c084d5");

            string strValue = "";

            using (WebResponse response = webRequest.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                        string strDate;
                        while ((strDate = reader.ReadLine()) != null)
                        {
                            strValue += strDate + "\r\n";
                        }
                    }
                }
            }

            return strValue;
        }
    }

    public class DateModel
    {
        public DateTime Date { get; set; }

        /// <summary>
        /// 工作日 = 0,节假日 = 2, 休息日 = 1
        /// </summary>
        public int DateType { get; set; }
    }
}
