using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Sale4.Controllers.Common
{
    public static class SaleCommon
    {
        public static string SiteHost = ConfigurationManager.AppSettings["SiteHost"];

        public static string ImgUrl = ConfigurationManager.AppSettings["ImgUrl"];
        
    }


    public enum EHtmlType
    {
        /// <summary>
        /// 静态页面
        /// </summary>
        Static,
        /// <summary>
        /// 动态模板
        /// </summary>
        Temple

    }

    public enum EDetailType
    {
        /// <summary>
        /// 列表
        /// </summary>
        List4,
        /// <summary>
        /// html标签(map标签)
        /// </summary>
        Html,
        /// <summary>
        /// 侧边栏
        /// </summary>
        SideBar,
        /// <summary>
        /// 列表
        /// </summary>
        List2,
        /// <summary>
        /// 列表
        /// </summary>
        List3,
        /// <summary>
        /// 优惠代码
        /// </summary>
        ProCode

    }

    public enum EActivityAreaType
    {
        /// <summary>
        /// 链接
        /// </summary>
        Link,
        /// <summary>
        /// 加入购物车
        /// </summary>
        AddCart,
        /// <summary>
        /// 多份组合优惠
        /// </summary>
        AddCartCombination,
        /// <summary>
        /// 锚点
        /// </summary>
        Anchor,
        /// <summary>
        /// 优惠券
        /// </summary>
        GetCoupon
    }
}