using System;

namespace Sale4.Models
{
    public class ActivityBaseViewModels
    {

        public Guid ActivityId { get; set; }
        public string ActivityCode { get; set; }
        public string ActivityName { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        /// <summary>
        /// 发布状态0:未发布,1:发布,2:取消
        /// </summary>
        public int IsPublish { get; set; }
        public string REC_CreateTime { get; set; }
        public string REC_CreateBy { get; set; }
        public string REC_ModifyTime { get; set; }
        public string REC_ModifyBy { get; set; }


        /// <summary>
        /// 预览链接
        /// </summary>
        public string PreUrl
        {
            get { return string.Format("sale2017/activity/sale/{0}",ActivityCode); }
        }

        /// <summary>
        /// 1:未开始
        /// 2:进行中
        /// 3:已结束
        /// 4:未发布
        /// 5:取消
        /// </summary>
        public int ExpiresState
        {
            get
            {
                var now = DateTime.Now;
                if (IsPublish == 2)
                {
                    return 5;
                }
                else if (IsPublish == 0)
                {
                    return 4;
                }
                else if (IsPublish == 1 && !string.IsNullOrWhiteSpace(StartTime) && !string.IsNullOrWhiteSpace(EndTime))
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
    }
}