using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Sale4.Models
{
    /// <summary>
    /// 易果商品 
    /// </summary>
    [Serializable]
    public class Commodity
    {
        /// <summary>
        /// 商品 Id
        /// </summary>
        public string CommodityId
        {
            get;
            set;
        }

        /// <summary>
        /// 商品代码
        /// </summary>
        public string CommodityCode
        {
            get;
            set;
        }

        /// <summary>
        /// 商品名
        /// </summary>
        public string CommodityName
        {
            get;
            set;
        }
        
        /// <summary>
        /// 对应产品编号
        /// </summary>
        public string ProductId
        {
            get;
            set;
        }
        

        /// <summary>
        /// 商品数量
        /// </summary>
        public decimal CommodityAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 商品类别
        /// </summary>
        public int CommodityType
        {
            get;
            set;
        }

        /// <summary>
        /// 原始价
        /// </summary>
        public decimal OriginalPrice
        {
            get;
            set;
        }

        /// <summary>
        /// 商品价格（最终售价）
        /// </summary>
        public decimal CommodityPrice
        {
            get;
            set;
        }

        /// <summary>
        /// 商品总价格
        /// </summary>
        public decimal CommodityTotalPrice
        {
            get
            {
                return this.CommodityAmount * this.CommodityPrice;
            }
        }


        /// <summary>
        /// 重量
        /// </summary>
        public decimal GrossWeight { get; set; }

        /// <summary>
        /// 是否为液体0不是，1是
        /// </summary>
        public int IsLiquid { get; set; }

        /// <summary>
        /// 商品状态 1.上架 2.下架 4.预订
        /// </summary>
        public int State
        {
            get;
            set;
        }

        /// <summary>
        /// 商品可配送区域
        /// </summary>
        public string DeliveryAreas
        {
            get;
            set;
        }
        /// <summary>
        /// 是否在当前站点发货区
        /// </summary>
        public bool InDeliveryArea { get; set; }

        /// <summary>
        /// 商品规格
        /// </summary>
        public string Spec
        {
            get;
            set;
        }

        /// <summary>
        /// 用于关联同一商品的不同规格
        /// </summary>
        public string SpecId
        {
            get;
            set;
        }

        /// <summary>
        /// 品牌
        /// </summary>
        public string BrandName
        {
            get;
            set;
        }

        /// <summary>
        /// 产地
        /// </summary>
        public string PlaceOfOrigin
        {
            get;
            set;
        }

        /// <summary>
        /// 图片 Url 地址
        /// </summary>
        public List<string> Pictures
        {
            get;
            set;
        }

        /// <summary>
        /// 图片索缩略图 Url 地址
        /// </summary>
        public List<string> PicturesSmall
        {
            get;
            set;
        }

        /// <summary>
        /// 列表小图 URL
        /// </summary>
        public string SmallPic
        {
            get;
            set;
        }
        /// <summary>
        /// 列表小图 URL
        /// </summary>
        public string PictureUrl
        {
            get;
            set;
        }

        /// <summary>
        /// 详情页图片
        /// </summary>
        public List<string> PicturesUrl { get; set; }
        
        /// <summary>
        /// 商品描述
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// 是否为自由拼（是否为小包装商品）
        /// </summary>
        public int IsFreedom
        {
            get;
            set;
        }

        /// <summary>
        /// 是否为特殊商品
        /// </summary>
        public int IsSpecial
        {
            get;
            set;
        }

        /// <summary>
        /// 是否为赠品
        /// </summary>
        public bool IsGift
        {
            get;
            set;
        }

        /// <summary>
        /// 优惠券代码
        /// </summary>
        public string CouponCode
        {
            get;
            set;
        }

        /// <summary>
        /// 赠品规则 Id
        /// </summary>
        public string GiftRuleId
        {
            get;
            set;
        }

        /// <summary>
        /// 赠品规则类别
        /// </summary>
        public int GiftRuleType
        {
            get;
            set;
        }

        /// <summary>
        /// 赠品所对应的原始商品 Id（用于促销）
        /// </summary>
        public string GiftCommodityId
        {
            get;
            set;
        }

        /// <summary>
        /// 订单明细1.正常购买2.钱+U币购买4.U币购买8.礼品选购16.促销商品32.其他
        /// </summary>
        public int OrderDetailsType
        {
            get;
            set;
        }

        /// <summary>
        /// 是否使用 U 币
        /// </summary>
        public int UsePoint
        {
            get;
            set;
        }

        /// <summary>
        /// 是否显示原始价
        /// </summary>
        public bool ShowOriginalPrice
        {
            get
            {
                if (this.OriginalPrice <= 0)
                {
                    return false;
                }

                if (this.CommodityPrice < this.OriginalPrice)
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// 商品可用的配送日期如：1234567
        /// </summary>
        public int CommodityDeliveryDate
        {
            get;
            set;
        }

        /// <summary>
        /// 商品结单时间：1:12:00 2:16:30 3:20:00 4:18:00
        /// </summary>
        public int CommodityDeliveryTime
        {
            get;
            set;
        }

        /// <summary>
        /// 默认发货区
        /// </summary>
        public string DefaultAreaCode
        {
            get;
            set;
        }

        /// <summary>
        /// 结单时间文本
        /// </summary>
        public string CloseTimeText
        {
            get;
            set;
        }

        /// <summary>
        /// 发货时间文本
        /// </summary>
        public string DeliveryDateText
        {
            get;
            set;
        }

        /// <summary>
        /// 默认发货区文本
        /// </summary>
        public string DefaultAreaText
        {
            get;
            set;
        }


        /// <summary>
        /// 商品网站价格
        /// </summary>
        public decimal WebPrice
        {
            get;
            set;
        }

        /// <summary>
        /// 商品手机专享价格
        /// </summary>
        public decimal AppPrice
        {
            get;
            set;
        }

        /// <summary>
        /// 最大限购数量
        /// </summary>
        public int MaxLimitCount
        {
            get;
            set;
        }

        /// <summary>
        /// 商品所属的订单 Id
        /// </summary>
        public string OrderId
        {
            get;
            set;
        }

        /// <summary>
        /// 商品促销 Id 对应数据库表 [Rel_OrderDetails].PromotionsDetailsId
        /// </summary>
        public string PromotionsDetailsId
        {
            get;
            set;
        }
        /// <summary>
        /// 促销表主Id值
        /// </summary>
        public string PromotionsId
        {
            get;
            set;
        }

        /// <summary>
        /// 评价整体评分
        /// </summary>
        public string VoteScore
        {
            get;
            set;
        }

        /// <summary>
        /// 评价总数
        /// </summary>
        public int VoteCount
        {
            get;
            set;
        }


    }
}
