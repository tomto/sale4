using System;
using System.Collections.Generic;
using Sale4.Controllers.Common;

namespace Sale4.Models
{
    [Serializable]
    public class StaticHtmlViewModel
    {
        public Guid StaticHtmlId { get; set; }
        public string HtmlCode { get; set; }
        public string HtmlUrl { get; set; }
        public string HtmlName { get; set; }
        public string HtmlBannerUrl { get; set; }
        public string HtmlAnimateUrl { get; set; }
        public string HtmlBackgroundUrl { get; set; }
        public int HtmlType { get; set; }
        public string REC_CreateTime { get; set; }
        public string REC_CreateBy { get; set; }
        public string REC_ModifyBy { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        /// <summary>
        /// 1:过期活动自动下架
        /// 0:不适用自动过期
        /// </summary>
        public int IsAutoDisabled { get; set; }

        /// <summary>
        /// 预览链接
        /// </summary>
        public string PreUrl {
            get { return string.Format("{0}/activity/sale/{1}", SaleCommon.SiteHost, HtmlCode); }
        }

        /// <summary>
        /// 0:未设置过期
        /// 1:未开始
        /// 2:进行中
        /// 3:已结束
        /// </summary>
        public int ExpiresState {
            get
            {
                var now = DateTime.Now;
                if (IsAutoDisabled == 1 && !string.IsNullOrWhiteSpace(StartTime.ToString()) && !string.IsNullOrWhiteSpace(EndTime.ToString()))
                {
                    if (Convert.ToDateTime(StartTime) > now)
                    {
                        return 1;
                    }
                    else if (Convert.ToDateTime(StartTime) < now && Convert.ToDateTime(EndTime) >= now)
                    {
                        return 2;
                    }
                    return 3;
                }
                return 0;
            }
        }
        public List<StaticDetailViewModel> Details { get; set; }

    }
}
