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
    public class YgwCommodity
    {
        public int CookieNum { get; set; }
        public string PointExchangeId { get; set; }

        /// <summary>
        /// Description:0=普通商品，1 随心配，2积分，3商品方式促销，4额满换购，5特价，6礼品赠言卡，8组合，9限时抢购，11团购,14x选y,30=充值送的商品，31=充值金额，40=xml满足钱送商品，41=xml买指定商品送商品，42=xml买指定分类送商品   DataType:int
        /// </summary>
        public int IsWordsCard { get; set; }
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
        /// 1.易果 2.考拉 4.原鲜 8.锦色 16乐醇
        /// </summary>
        public int WebId { get; set; }

        /// <summary>
        /// 是否为当日配送0不是1是
        /// </summary>
        public int IsTodayDistr { get; set; }

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
        /// 商品多规格
        /// </summary>
        public List<YgmCommoditySpec> Speces
        {
            get;
            set;
        }

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

        /// <summary>
        /// 卡券类型 0.普通商品 1.储值卡 2.现金券 3.抵用券 4.提券券
        /// </summary>
        public int CardType
        {
            get;
            set;
        }

        /// <summary>
        /// 是否库存倒扣结束时自动下架商品 1.是
        /// </summary>
        public int IsAutoHide
        {
            get;
            set;
        }

        /// <summary>
        /// 是否代销商品，1是，0,其它不是
        /// </summary>
        public int IsDealers
        {
            get;
            set;
        }

        /// <summary>
        /// 商品所属的分类 Id，一个商品可以属于多个分类，此分类 Id 用户指定场景
        /// </summary>
        public string CategoryId
        {
            get;
            set;
        }

        public string SmallCategoryId { get; set; }

        /// <summary>
        /// 是否可退换货 1是，0,其它不是
        /// </summary>
        public int IsCanReturn
        {
            get;
            set;
        }

        /// <summary>
        /// 是否支持7天无理由退换货 1是，0不是
        /// </summary>
        public int CanNoReasonToReturn
        {
            get;
            set;
        }

        /// <summary>
        /// 送货提示
        /// </summary>
        public string DeliveryTips
        {
            get;
            set;
        }
        /// <summary>
        /// 商品位置
        /// </summary>
        public int Index
        {
            get;
            set;
        }


        /// <summary>
        /// 是否选中
        /// </summary>
        public bool Selected
        {
            get;
            set;
        }

        /// <summary>
        /// 能否选中
        /// </summary>
        public bool CanSelect
        {
            get;
            set;
        }
        /// <summary>
        /// 商品副标题
        /// </summary>
        public string SubTitle
        {
            get;
            set;
        }

        /// <summary>
        /// 易果商品（用于移动端）
        /// </summary>
        public YgwCommodity()
        {
            this.OrderDetailsType = 1;
            this.Speces = new List<YgmCommoditySpec>();
            this.Pictures = new List<string>();
            this.PicturesSmall = new List<string>();
            this.PicturesUrl = new List<string>();
        }

        /// <summary>
        /// 商品名，方便调试
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.CommodityName;
        }

        /// <summary>
        /// 将数组转化为字典
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static Dictionary<string, YgwCommodity> ParseToDict(YgwCommodity[] array)
        {
            var dict = new Dictionary<string, YgwCommodity>();
            if (array == null || array.Length <= 0)
            {
                return dict;
            }

            foreach (var m in array)
            {
                if (string.IsNullOrEmpty(m.CommodityId))
                {
                    throw new ArgumentException("CommodityId 不能为空");
                }
                string id = m.CommodityId.ToLower();
                if (!dict.ContainsKey(id))
                {
                    dict.Add(id, m);
                }
            }
            return dict;
        }

        #region [ new Add]
        public string PromotionTips { get; set; }


        /// <summary>
        /// 优惠类型
        /// </summary>
        public int PromotionsType { get; set; }
        /// <summary>
        /// 优惠Id
        /// </summary>
        public string PromotionId { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// 促销语
        /// </summary>
        public string PromotionalDesc { get; set; }
        /// <summary>
        /// 多分优惠折扣
        /// </summary>
        public decimal Redution { get; set; }
        /// <summary>
        /// 多分优惠最小购买数量
        /// </summary>
        public int PromoAmount { get; set; }
        /// <summary>
        /// 优惠开始时间
        /// </summary>
        public DateTime ProStartTime { get; set; }
        /// <summary>
        /// 优惠结束时间
        /// </summary>
        public DateTime ProEndTime { get; set; }
        /// <summary>
        /// 优惠商品已售数量
        /// </summary>
        public int BuyNum { get; set; }
        /// <summary>
        /// 优惠商品数量总数
        /// </summary>
        public int MinBuyNum { get; set; }
        /// <summary>
        /// 抢购剩余时间
        /// </summary>
        public int TotalSeconds { get; set; }
        /// <summary>
        /// 抢购商品是否可购买
        /// </summary>
        public bool CanBuy { get; set; }
        /// <summary>
        /// 促销标签
        /// </summary>
        public int ProTag { get; set; }
        /// <summary>
        /// 买赠商品购买刷量
        /// </summary>
        public int AssignCommodityAmount { get; set; }

        /// <summary>
        /// 用来判定是否需要刀叉
        /// </summary>
        public string CakeId { get; set; }

        #endregion

        #region [product detail]

        /// <summary>
        /// 温馨提示
        /// </summary>
        public string ShoppingTips { get; set; }

        /// <summary>
        /// 储存状态
        /// </summary>
        public string CommodityStore { get; set; }
        /// <summary>
        /// 结单时间
        /// </summary>
        public string SettleTime { get; set; }
        /// <summary>
        /// 商品标签
        /// </summary>
        public string CommodityTag { get; set; }
        /// <summary>
        /// 库存描述
        /// </summary>
        public string Stock { get; set; }
        /// <summary>
        /// 产地国旗图标
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 导航面包屑
        /// </summary>
        public string PathHtml { get; set; }
        /// <summary>
        /// 分类为尊享区的预定
        /// </summary>
        public string CommodityState4ZXQ { get; set; }
        /// <summary>
        /// 是否是蔬菜
        /// </summary>
        public bool IsVegetable { get; set; }
        /// <summary>
        /// 商品总销量
        /// </summary>
        public int SalesCount { get; set; }
        /// <summary>
        /// 优惠类型描述
        /// </summary>
        public string PromotionTypeStr { get; set; }

        public decimal VoteHP { get; set; }
        public decimal VoteZP { get; set; }
        public decimal VoteCP { get; set; }
        

        public string DuiHuanId { get; set; }
        public string ExchangePoints { get; set; }
        public string DuiHuan { get; set; }
        public string HuanGouId { get; set; }
        public string HuanGou { get; set; }

        #region U币兑换
        /// <summary>
        /// U币兑换优惠Id
        /// </summary>
        public string ExchangeId { get; set; }
        /// <summary>
        /// 纯U币兑换
        /// </summary>
        public string OnlyPoints { get; set; }
        /// <summary>
        /// 积分数量
        /// </summary>
        public decimal Points { get; set; }
        /// <summary>
        /// 原始积分数量
        /// </summary>
        public decimal OriginalPoints { get; set; }
        /// <summary>
        /// Ubi加钱兑换所需现金金额
        /// </summary>
        public decimal Money { get; set; }

        #endregion

        #endregion

        /// <summary>
        /// 秒杀（限时抢购）商品id
        /// </summary>
        public string GroupBuyId { get; set; }

        /// <summary>
        /// 是否使用库存倒扣0:不使用1:使用
        /// </summary>
        public int IsUseProductStock { get; set; }
    }

    /// <summary>
    /// 商品规格
    /// </summary>
    [Serializable]
    public class YgmCommoditySpec
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
        /// 商品名称
        /// </summary>
        public string CommodityName
        {
            get;
            set;
        }

        /// <summary>
        /// 规格名
        /// </summary>
        public string Spec
        {
            get;
            set;
        }
        /// <summary>
        /// 原价
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 现价
        /// </summary>
        public decimal CommodityPrice { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }
    }

    /// <summary>
    /// 多分优惠商品
    /// </summary>
    [Serializable]
    public class YgwCommodityMultiTeHui
    {
        /// <summary>
        /// 优惠Id
        /// </summary>
        public string PromotionsID { get; set; }
        /// <summary>
        /// 组合购买价
        /// </summary>
        public decimal CombinedCommodityPrice { get; set; }
        /// <summary>
        /// 正常价
        /// </summary>
        public decimal LastPrice { get; set; }
        /// <summary>
        /// 图片名称
        /// </summary>
        public string PictureName { get; set; }
        /// <summary>
        /// 商品Id
        /// </summary>
        public string CommodityID { get; set; }
        /// <summary>
        /// 商品编号
        /// </summary>
        public string CommodityCode { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string CommodityName { get; set; }
        /// <summary>
        /// 单品原价
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// 起购数量
        /// </summary>
        public int Amount { get; set; }
        /// <summary>
        /// 单品现价
        /// </summary>
        public decimal CommodityPrice { get; set; }
        /// <summary>
        /// 优惠开始时间
        /// </summary>
        public string StartDate { get; set; }
        /// <summary>
        /// 优惠结束时间
        /// </summary>
        public string EndDate { get; set; }
        public string PictureUrl { get; set; }

    }

    /// <summary>
    /// 组合优惠
    /// </summary>
    [Serializable]
    public class YgwCommodityCombined
    {
        /// <summary>
        /// 组合价
        /// </summary>
        public decimal CombinedCommodityPrice { get; set; }
        /// <summary>
        /// 原价
        /// </summary>
        public decimal LastPrice { get; set; }
        /// <summary>
        /// 活动开始时间
        /// </summary>
        public string StartDate { get; set; }
        /// <summary>
        /// 活动结束时间
        /// </summary>
        public string EndDate { get; set; }
        public string PromotionsId { get; set; }
        /// <summary>
        /// 组合商品集合
        /// </summary>
        public List<YgwCommodity> Commodities { get; set; }
    }

    [Serializable]
    public class YgwCommodityMjz
    {
        /// <summary>
        /// 商品Id
        /// </summary>
        public string CommodityID { get; set; }
        /// <summary>
        /// 平台id
        /// </summary>
        public int WebId { get; set; }
        /// <summary>
        /// 组合购买价格
        /// </summary>
        public decimal CombinedCommodityPrice { get; set; }
        /// <summary>
        /// 原价
        /// </summary>
        public decimal LastPrice { get; set; }
        /// <summary>
        /// 优惠开始时间
        /// </summary>
        public string StartDate { get; set; }
        /// <summary>
        /// 优惠结束时间
        /// </summary>
        public string EndDate { get; set; }
        /// <summary>
        /// 买赠购买数量或赠品数量
        /// </summary>
        public int Amount { get; set; }
        /// <summary>
        /// 商品编号
        /// </summary>
        public string CommodityCode { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string CommodityName { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string Spec { get; set; }
        /// <summary>
        /// 图片名称
        /// </summary>
        public string PictureName { get; set; }
        public string PictureUrl { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 是否是赠品
        /// </summary>
        public bool IsZp { get; set; }


    }
}
