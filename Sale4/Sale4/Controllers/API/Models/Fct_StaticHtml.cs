using System;

namespace Sale4.Controllers.API.Models
{
    [Serializable]
    public class Fct_StaticHtml
    {
		private Guid _statichtmlid;
		private string _htmlcode;
		private string _htmlurl;
		private string _htmlname;
		private int _disabled;
		private DateTime _rec_createtime;
		private string _rec_createby;
		private DateTime _rec_modifytime;
		private string _rec_modifyby;
		private string _description;
		private int _htmltype;
		private string _htmlbackgroundurl;
		private string _htmlbannerurl;
		private string _htmlbackcolor1;
		private string _htmlbackcolor2;
		private string _htmlcartcolor;
		private string _htmlcartcolor1;
		private string _htmlcommoditycodes;
		private DateTime _starttime;
		private DateTime _endtime;
		private int _isautodisabled;
		private string _htmlanimateurl;
		private int _rolecode;
		
		public Fct_StaticHtml(){}
		
		 
		/// <summary>
        /// Description:  
        /// </summary>
		public Guid StaticHtmlId
		{
			get{ return this._statichtmlid;}
			set{ this._statichtmlid = value;}
		}
		/// <summary>
        /// Description:  
        /// </summary>
		public string HtmlCode
		{
			get{ return this._htmlcode;}
			set{ this._htmlcode = value;}
		}
		/// <summary>
        /// Description:  
        /// </summary>
		public string htmlUrl
		{
			get{ return this._htmlurl;}
			set{ this._htmlurl = value;}
		}
		/// <summary>
        /// Description:  
        /// </summary>
		public string htmlName
		{
			get{ return this._htmlname;}
			set{ this._htmlname = value;}
		}
		/// <summary>
        /// Description:  
        /// </summary>
		public int Disabled
		{
			get{ return this._disabled;}
			set{ this._disabled = value;}
		}
		/// <summary>
        /// Description:  
        /// </summary>
		public DateTime REC_CreateTime
		{
			get{ return this._rec_createtime;}
			set{ this._rec_createtime = value;}
		}
		/// <summary>
        /// Description:  
        /// </summary>
		public string REC_CreateBy
		{
			get{ return this._rec_createby;}
			set{ this._rec_createby = value;}
		}
		/// <summary>
        /// Description:  
        /// </summary>
		public DateTime REC_ModifyTime
		{
			get{ return this._rec_modifytime;}
			set{ this._rec_modifytime = value;}
		}
		/// <summary>
        /// Description:  
        /// </summary>
		public string REC_ModifyBy
		{
			get{ return this._rec_modifyby;}
			set{ this._rec_modifyby = value;}
		}
		/// <summary>
        /// Description:  
        /// </summary>
		public string Description
		{
			get{ return this._description;}
			set{ this._description = value;}
		}
		/// <summary>
        /// Description:活动类型 0是静态活动页面1，动态页面  
        /// </summary>
		public int HtmlType
		{
			get{ return this._htmltype;}
			set{ this._htmltype = value;}
		}
		/// <summary>
        /// Description:背景图片地址  
        /// </summary>
		public string HtmlBackgroundUrl
		{
			get{ return this._htmlbackgroundurl;}
			set{ this._htmlbackgroundurl = value;}
		}
		/// <summary>
        /// Description:中间广告位图片地址  
        /// </summary>
		public string HtmlBannerUrl
		{
			get{ return this._htmlbannerurl;}
			set{ this._htmlbannerurl = value;}
		}
		/// <summary>
        /// Description:最下面的大背景颜色  
        /// </summary>
		public string HtmlBackcolor1
		{
			get{ return this._htmlbackcolor1;}
			set{ this._htmlbackcolor1 = value;}
		}
		/// <summary>
        /// Description:第二层背景颜色  
        /// </summary>
		public string HtmlBackcolor2
		{
			get{ return this._htmlbackcolor2;}
			set{ this._htmlbackcolor2 = value;}
		}
		/// <summary>
        /// Description:购物车颜色  
        /// </summary>
		public string HtmlCartcolor
		{
			get{ return this._htmlcartcolor;}
			set{ this._htmlcartcolor = value;}
		}
		/// <summary>
        /// Description:购物车颜色  
        /// </summary>
		public string HtmlCartcolor1
		{
			get{ return this._htmlcartcolor1;}
			set{ this._htmlcartcolor1 = value;}
		}
		/// <summary>
        /// Description:关联商品  
        /// </summary>
		public string HtmlCommodityCodes
		{
			get{ return this._htmlcommoditycodes;}
			set{ this._htmlcommoditycodes = value;}
		}
		/// <summary>
        /// Description:  
        /// </summary>
		public DateTime StartTime
		{
			get{ return this._starttime;}
			set{ this._starttime = value;}
		}
		/// <summary>
        /// Description:  
        /// </summary>
		public DateTime EndTime
		{
			get{ return this._endtime;}
			set{ this._endtime = value;}
		}
		/// <summary>
        /// Description:  
        /// </summary>
		public int IsAutoDisabled
		{
			get{ return this._isautodisabled;}
			set{ this._isautodisabled = value;}
		}
		/// <summary>
        /// Description:背景动态图  
        /// </summary>
		public string HtmlAnimateUrl
		{
			get{ return this._htmlanimateurl;}
			set{ this._htmlanimateurl = value;}
		}
		/// <summary>
        /// Description:活动角色类型  
        /// </summary>
		public int RoleCode
		{
			get{ return this._rolecode;}
			set{ this._rolecode = value;}
		}
    }
}


