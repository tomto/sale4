using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Proxy
{
    public class Model_Fct_FloorItem
    {
		private Guid _flooritemid;
		private Guid _activityfloorid;
		private string _flooritemname;
		private int _flooritemtype;
		private int _sort;
		private string _flooritemlink;
		private string _eventcode;
		private string _eventparams;
		private string _flooritemimgurl;
		private string _commoditycodes;
		private int _showcommoditynum;
		private string _lucencyanchor;
		private int _disabled;
		private DateTime _rec_createtime;
		private string _rec_createby;
		private DateTime _rec_modifytime;
		private string _rec_modifyby;
		
		public Model_Fct_FloorItem(){}
		
		 
		/// <summary>
        /// Description:  
        /// </summary>
		public Guid FloorItemId
		{
			get{ return this._flooritemid;}
			set{ this._flooritemid = value;}
		}
		/// <summary>
        /// Description:  
        /// </summary>
		public Guid ActivityFloorId
		{
			get{ return this._activityfloorid;}
			set{ this._activityfloorid = value;}
		}
		/// <summary>
        /// Description:  
        /// </summary>
		public string FloorItemName
		{
			get{ return this._flooritemname;}
			set{ this._flooritemname = value;}
		}
		/// <summary>
        /// Description:楼层内容类型  
        /// </summary>
		public int FloorItemType
		{
			get{ return this._flooritemtype;}
			set{ this._flooritemtype = value;}
		}
		/// <summary>
        /// Description:楼层内容排序  
        /// </summary>
		public int Sort
		{
			get{ return this._sort;}
			set{ this._sort = value;}
		}
		/// <summary>
        /// Description:楼层内容链接  
        /// </summary>
		public string FloorItemLink
		{
			get{ return this._flooritemlink;}
			set{ this._flooritemlink = value;}
		}
		/// <summary>
        /// Description:楼层内容事件  
        /// </summary>
		public string EventCode
		{
			get{ return this._eventcode;}
			set{ this._eventcode = value;}
		}
		/// <summary>
        /// Description:楼层内容事件参数  
        /// </summary>
		public string EventParams
		{
			get{ return this._eventparams;}
			set{ this._eventparams = value;}
		}
		/// <summary>
        /// Description:楼层内容图片url  
        /// </summary>
		public string FloorItemImgUrl
		{
			get{ return this._flooritemimgurl;}
			set{ this._flooritemimgurl = value;}
		}
		/// <summary>
        /// Description:楼层内容商品code  
        /// </summary>
		public string CommodityCodes
		{
			get{ return this._commoditycodes;}
			set{ this._commoditycodes = value;}
		}
		/// <summary>
        /// Description:显示的商品数量  
        /// </summary>
		public int ShowCommodityNum
		{
			get{ return this._showcommoditynum;}
			set{ this._showcommoditynum = value;}
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


