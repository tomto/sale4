using System;

namespace Sale4.Controllers.API.Models
{
    public class Model_Fct_StaticDetail
    {
		private Guid _staticdetailid;
		private Guid _statichtmlid;
		private string _name;
		private string _title;
		private int _detailtype;
		private int _sort;
		private string _htmlbackgroundurl;
		private string _lucencyanchor;
		private string _commoditycodes;
		private int _disabled;
		private DateTime _rec_createtime;
		private string _rec_createby;
		private DateTime _rec_modifytime;
		private string _rec_modifyby;
		private string _tag;
		
		public Model_Fct_StaticDetail(){}
		
		 
		/// <summary>
        /// Description:  
        /// </summary>
		public Guid StaticDetailId
		{
			get{ return this._staticdetailid;}
			set{ this._staticdetailid = value;}
		}
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
		public string Name
		{
			get{ return this._name;}
			set{ this._name = value;}
		}
		/// <summary>
        /// Description:  
        /// </summary>
		public string Title
		{
			get{ return this._title;}
			set{ this._title = value;}
		}
		/// <summary>
        /// Description:  
        /// </summary>
		public int DetailType
		{
			get{ return this._detailtype;}
			set{ this._detailtype = value;}
		}
		/// <summary>
        /// Description:  
        /// </summary>
		public int Sort
		{
			get{ return this._sort;}
			set{ this._sort = value;}
		}
		/// <summary>
        /// Description:  
        /// </summary>
		public string HtmlBackgroundUrl
		{
			get{ return this._htmlbackgroundurl;}
			set{ this._htmlbackgroundurl = value;}
		}
		/// <summary>
        /// Description:  
        /// </summary>
		public string LucencyAnchor
		{
			get{ return this._lucencyanchor;}
			set{ this._lucencyanchor = value;}
		}
		/// <summary>
        /// Description:  
        /// </summary>
		public string CommodityCodes
		{
			get{ return this._commoditycodes;}
			set{ this._commoditycodes = value;}
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
		public string Tag
		{
			get{ return this._tag;}
			set{ this._tag = value;}
		}
    }
}


