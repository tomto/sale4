using System;
using System.Collections.Generic;

namespace Sale4.Models
{
    [Serializable]
    public class StaticDetailViewModel
    {
        public Guid StaticDetailId { get; set; }
        public Guid StaticHtmlId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public int DetailType { get; set; }
        public string HtmlBackgroundUrl { get; set; }
        public string LucencyAnchor { get; set; }
        public string CommodityCodes { get; set; }
        public string Tag { get; set; }
        public int Sort { get; set; }
        public string REC_CreateTime { get; set; }
        public string REC_CreateBy { get; set; }
        public string REC_ModifyBy { get; set; }

        public List<YgwCommodity> Commodities { get; set; }

    }
}
