using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Proxy
{
    public class Model_Fct_ActivityBase
    {
		private Guid _activityid;
		private string _activitycode;
		private string _activityname;
		private int _activitytype;
		private string _activitybgimg;
		private string _activitybgcolor;
		private DateTime _starttime;
		private DateTime _endtime;
		private int _ispublish;
		private string _fontcolor;
		private string _focusfontcolor;
		private int _displayrange;
		private int _disabled;
		private DateTime _rec_createtime;
		private string _rec_createby;
		private DateTime _rec_modifytime;
		private string _rec_modifyby;
		
		public Model_Fct_ActivityBase(){}
		
		 
		/// <summary>
        /// Description:  
        /// </summary>
		public Guid ActivityId
		{
			get{ return this._activityid;}
			set{ this._activityid = value;}
		}
		/// <summary>
        /// Description:活动code  
        /// </summary>
		public string ActivityCode
		{
			get{ return this._activitycode;}
			set{ this._activitycode = value;}
		}
		/// <summary>
        /// Description:活动名称  
        /// </summary>
		public string ActivityName
		{
			get{ return this._activityname;}
			set{ this._activityname = value;}
		}
		/// <summary>
        /// Description:活动类型0:h5  
        /// </summary>
		public int ActivityType
		{
			get{ return this._activitytype;}
			set{ this._activitytype = value;}
		}
		/// <summary>
        /// Description:活动背景图  
        /// </summary>
		public string ActivityBgImg
		{
			get{ return this._activitybgimg;}
			set{ this._activitybgimg = value;}
		}
		/// <summary>
        /// Description:活动背景色  
        /// </summary>
		public string ActivityBgColor
		{
			get{ return this._activitybgcolor;}
			set{ this._activitybgcolor = value;}
		}
		/// <summary>
        /// Description:活动开始时间  
        /// </summary>
		public DateTime StartTime
		{
			get{ return this._starttime;}
			set{ this._starttime = value;}
		}
		/// <summary>
        /// Description:活动结束时间  
        /// </summary>
		public DateTime EndTime
		{
			get{ return this._endtime;}
			set{ this._endtime = value;}
		}
		/// <summary>
        /// Description:发布状态0:未发布,1:发布  
        /// </summary>
		public int IsPublish
		{
			get{ return this._ispublish;}
			set{ this._ispublish = value;}
		}
		/// <summary>
        /// Description:字体色  
        /// </summary>
		public string FontColor
		{
			get{ return this._fontcolor;}
			set{ this._fontcolor = value;}
		}
		/// <summary>
        /// Description:字体选中色  
        /// </summary>
		public string FocusFontColor
		{
			get{ return this._focusfontcolor;}
			set{ this._focusfontcolor = value;}
		}
		/// <summary>
        /// Description:0全部，1网站，2移动 3:果易达 4:易海淘 5:团来团趣  
        /// </summary>
		public int DisplayRange
		{
			get{ return this._displayrange;}
			set{ this._displayrange = value;}
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
    }
}


