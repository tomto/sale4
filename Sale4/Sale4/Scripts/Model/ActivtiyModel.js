
var fmModel = {};


(function (fmModel) {
    var fmStaticHtml = (function () {
        function fmStaticHtml() {
            this.StaticHtmlId = "";
            this.HtmlCode = "";
            this.HtmlUrl = "";
            this.HtmlName = "";
            this.HtmlBannerUrl = "";
            this.HtmlAnimateUrl = "";
            this.HtmlBackgroundUrl = "";
            this.HtmlType = 1;
            this.REC_CreateTime = "";
            this.REC_CreateBy = "";
            this.REC_ModifyBy = "";
            this.StartTime = "";
            this.EndTime = "";
            this.RoleCode = "";
            /// <summary>
            /// 1:过期活动自动下架
            /// 0:不适用自动过期
            /// </summary>
            this.IsAutoDisabled = 1;

            this.preUrl = "";
        }
        return fmStaticHtml;
    })();

    var fmStaticDetail = (function () {
        function fmStaticDetail() {
            this.StaticDetailId = "";
            this.StaticHtmlId = "";
            this.Name = "";
            this.Title = "";
            this.DetailType = 1;//default 图片
            this.HtmlBackgroundUrl = "";
            this.LucencyAnchor = "";
            this.CommodityCodes = "";
            this.Tag = "";
            this.Sort = 0;
            this.REC_CreateTime = "";
            this.REC_CreateBy = "";
            this.REC_ModifyBy = "";
        }
        return fmStaticDetail;
    })();

    var fmAnchor = (function () {
        function fmAnchor() {
            this.Coord = "";
            this.Type = "";
            this.Operate = "";
            this.Placeholder = "链接如:http://www.google.com";
            this.Index = "";
        }
        return fmAnchor;
    })();

    var fmProCode = (function () {
        function fmProCode() {
            this.Coord = "";
            this.CoordX = 0;
            this.CoordY = 0;
            this.Content = "";
            this.FontColor = "";
            this.FontSize = "";
            this.FontWidth = 0;
            this.Index = "";
        }
        return fmProCode;
    })();

    (function (eAnchorType) {
        eAnchorType[eAnchorType["Link"] = 0] = "Link";
        eAnchorType[eAnchorType["AddCart"] = 1] = "AddCart";
        eAnchorType[eAnchorType["AddCartCombination"] = 2] = "AddCartCombination";
        eAnchorType[eAnchorType["Anchor"] = 3] = "Anchor";
        eAnchorType[eAnchorType["GetCoupon"] = 4] = "GetCoupon";
    })(fmModel.eAnchorType || (fmModel.eAnchorType = {}));

    (function (eDetailType) {
        eDetailType[eDetailType["List4"] = 0] = "List4";
        eDetailType[eDetailType["Html"] = 1] = "Html";
        eDetailType[eDetailType["SideBar"] = 2] = "SideBar";
        eDetailType[eDetailType["List2"] = 3] = "List2";
        eDetailType[eDetailType["List3"] = 4] = "List3";
        eDetailType[eDetailType["ProCode"] = 5] = "ProCode";
    })(fmModel.eDetailType || (fmModel.eDetailType = {}));


    fmModel.fmStaticHtml = fmStaticHtml;
    fmModel.fmStaticDetail = fmStaticDetail;
    fmModel.fmAnchor = fmAnchor;
    fmModel.fmProCode = fmProCode;
    

})(fmModel || (fmModel = {}));




