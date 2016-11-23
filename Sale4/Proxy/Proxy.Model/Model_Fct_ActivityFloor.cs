using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Proxy
{
    public class Model_Fct_ActivityFloor
    {
		private Guid _activityfloorid;
		private Guid _activityid;
		private int _floortype;
		private int _sort;
		private string _fontcolor;
		private string _fontfocuscolor;
		private int _disabled;
		private DateTime _rec_createtime;
		private string _rec_createby;
		private DateTime _rec_modifytime;
		private string _rec_modifyby;
		private string _floorname;
		
		public Model_Fct_ActivityFloor(){}
		
		 
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
		public Guid ActivityId
		{
			get{ return this._activityid;}
			set{ this._activityid = value;}
		}
		/// <summary>
        /// Description:楼层类型0:普通,1:banner,2:Special  
        /// </summary>
		public int FloorType
		{
			get{ return this._floortype;}
			set{ this._floortype = value;}
		}
		/// <summary>
        /// Description:楼层排序  
        /// </summary>
		public int Sort
		{
			get{ return this._sort;}
			set{ this._sort = value;}
		}
		/// <summary>
        /// Description:  
        /// </summary>
		public string FontColor
		{
			get{ return this._fontcolor;}
			set{ this._fontcolor = value;}
		}
		/// <summary>
        /// Description:  
        /// </summary>
		public string FontFocusColor
		{
			get{ return this._fontfocuscolor;}
			set{ this._fontfocuscolor = value;}
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
		public string FloorName
		{
			get{ return this._floorname;}
			set{ this._floorname = value;}
		}
    }
}


