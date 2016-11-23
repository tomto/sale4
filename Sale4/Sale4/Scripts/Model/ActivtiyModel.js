
var fmModel = {};


(function (fmModel) {
    var ActivityBase = (function () {
        function ActivityBase() {
            this.ActivityId = "";
            this.ActivityCode = "";
            this.ActivityName = "";
            this.ActivityType = 0;
            this.ActivityBgColor = "";
            this.StartTime = "";
            this.EndTime = "";
            this.IsPublish = 0;
            this.FontColor = "";
            this.FocusFontColor = "";
            this.DisplayRange = "";
            this.REC_CreateBy = "";
            this.REC_ModifyBy = "";
            
            this.preUrl = "";
        }
        return ActivityBase;
    })();

    var fmItemDetail = (function () {
        function fmItemDetail() {
            this.ActivityId = "";
            this.ActivityFloorId = "";
            this.FloorItemId = "";
            this.FloorItemName = "";
            this.Title = "";
            this.FloorItemType = 1;//default 图片
            this.HtmlBackgroundUrl = "";
            this.LucencyAnchor = "";
            this.CommodityCodes = "";
            this.Sort = 0;
            this.REC_CreateTime = "";
            this.REC_CreateBy = "";
            this.REC_ModifyBy = "";
        }
        return fmItemDetail;
    })();

    var fmAnchor = (function () {
        function fmAnchor() {
            this.Coord = "";
            this.Type = "";
            this.Operate = "";
            this.Placeholder = "链接如:http://www.yiguo.com";
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


    fmModel.ActivityBase = ActivityBase;
    fmModel.fmItemDetail = fmItemDetail;
    fmModel.fmAnchor = fmAnchor;
    fmModel.fmProCode = fmProCode;
    

})(fmModel || (fmModel = {}));




