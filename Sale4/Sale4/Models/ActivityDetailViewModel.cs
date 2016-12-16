using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sale4.Models
{
    public class ActivityDetailViewModel
    {
        public Guid FloorItemId { get; set; }
        public Guid ActivityFloorId { get; set; }
        public int ItemType { get; set; }
        public string ItemName { get; set; }
        public int Sort { get; set; }
        public string ItemBgImg { get; set; }
        public string EventCode { get; set; }
        public string EventParams { get; set; }
        public string ItemImgUrl { get; set; }
        public string CommodityCodes { get; set; }
        public int ShowCommodityNum { get; set; }
        public string LucencyAnchor { get; set; }
    }


    public class ActivityDetailsViewModel
    {
        public Guid ActivityId { get; set; }
        public Guid ActivityFloorId { get; set; }
        public int FloorType { get; set; }
        public string FloorName { get; set; }
        public string FontColor { get; set; }
        public string FontFocusColor { get; set; }
        public int Sort { get; set; }
        public List<ActivityDetailViewModel> Details { get; set; }
    }
}