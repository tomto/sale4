using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Sale4.Controllers.API.Models;
using Sale4.Controllers.Common;
using Sale4.Models;

namespace Sale4.Controllers.API
{
    public class ActivityApiController : BaseController
    {

        public JsonResult GetDetail(string id)
        {
            var staticHtml =new StaticHtmlViewModel();
            var firstOrDefault = GetStatics(id).FirstOrDefault();
            if (firstOrDefault != null && firstOrDefault.StaticHtmlId != Guid.Empty)
            {
                staticHtml = new StaticHtmlViewModel
                {
                    StaticHtmlId = firstOrDefault.StaticHtmlId,
                    HtmlCode = firstOrDefault.HtmlCode,
                    HtmlUrl = firstOrDefault.htmlUrl,
                    HtmlName = firstOrDefault.htmlName,
                    HtmlBannerUrl = firstOrDefault.HtmlBannerUrl,
                    HtmlAnimateUrl = firstOrDefault.HtmlAnimateUrl,
                    HtmlBackgroundUrl = firstOrDefault.HtmlBackgroundUrl,
                    HtmlType = firstOrDefault.HtmlType,
                    REC_CreateTime = firstOrDefault.REC_CreateTime.ToString("yyyy年MM月dd日"),
                    REC_CreateBy = firstOrDefault.REC_CreateBy,
                    REC_ModifyBy = firstOrDefault.REC_ModifyBy,
                    StartTime = firstOrDefault.StartTime.ToString("yyyy年MM月dd日"),
                    EndTime = firstOrDefault.EndTime.ToString("yyyy年MM月dd日")
                };
            }

            return GetJsonResult(new {data = staticHtml});
        }

        /// <summary>
        /// 获取静态活动列表
        /// </summary>
        /// <returns></returns>
        public JsonResult GetDetails()
        {
            var statics = new List<StaticHtmlViewModel>();
            var staticHtml = GetStatics();
            foreach (var s in staticHtml)
            {
                statics.Add(
                new StaticHtmlViewModel()
                {
                    StaticHtmlId = s.StaticHtmlId,
                    HtmlCode = s.HtmlCode,
                    HtmlUrl = s.htmlUrl,
                    HtmlName = s.htmlName,
                    HtmlBannerUrl = s.HtmlBannerUrl,
                    HtmlAnimateUrl = s.HtmlAnimateUrl,
                    HtmlBackgroundUrl = s.HtmlBackgroundUrl,
                    HtmlType = s.HtmlType,
                    REC_CreateTime = s.REC_CreateTime.ToString("yyyy年MM月dd日"),
                    REC_CreateBy = s.REC_CreateBy,
                    REC_ModifyBy = s.REC_ModifyBy,
                    StartTime = s.StartTime.ToString("yyyy年MM月dd日"),
                    EndTime = s.EndTime.ToString("yyyy年MM月dd日")
                });
            }
            return GetJsonResult(new { data = statics });
        }


        public JsonResult GetStaticsPage(int pageSize, int index)
        {
            var page = new PageResult<StaticHtmlViewModel>()
            {
                PageSize = pageSize,
                List = new List<StaticHtmlViewModel>()
            };

            var result = GetBasePage(pageSize, index);
            if (result != null && result.Count > 0)
            {
                page.List = (from r in result.List
                    select new StaticHtmlViewModel
                    {
                        StaticHtmlId = r.StaticHtmlId,
                        HtmlCode = r.HtmlCode,
                        HtmlName = r.htmlName,
                        EndTime = r.EndTime.ToString("yyyy年MM月dd日"),
                        StartTime = r.StartTime.ToString("yyyy年MM月dd日"),
                        REC_CreateBy = r.REC_CreateBy,
                        REC_ModifyBy = r.REC_ModifyBy,
                        REC_CreateTime = r.REC_CreateTime.ToString("yyyy年MM月dd日")
                    }
                    ).ToList();
                page.List = page.List.OrderBy(e => e.ExpiresState).ToList();
                page.Count = page.Count;
            }
            return GetJsonResult(new {data = page.List, pageCount = page.PageCount, allCount = page.Count});
        }

        //public JsonResult DeleteStatics(string id)
        //{
        //    var result = _activityServer.DeleteStatics(id);
        //    YGOP.ESB.Log.LogManager.WriteAppWork("DeleteStaticsDetail", string.Format("用户名：{0} Ip:{1} id:{2}", base.UserName, IPUtil.GetIPAddr(), id));
        //    return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult DeleteStaticsDetail(string id)
        //{
        //    var result = _activityServer.DeleteStaticsDetail(id);
        //    YGOP.ESB.Log.LogManager.WriteAppWork("DeleteStaticsDetail", string.Format("用户名：{0} Ip:{1} id:{2}", base.UserName, IPUtil.GetIPAddr(), id));
        //    return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult Detail(string id)
        //{
        //    var roleCode = base.UserSession.User.EmployeeType;
        //    var html = new YgwStaticHtml();

        //    if (!string.IsNullOrWhiteSpace(id) && roleCode != ERoleCode.None)
        //    {
        //        var result = _activityServer.GetStatics((int)roleCode, id);
        //        if (result.State && result.ResultObj != null)
        //        {
        //            var statics = result.ResultObj as List<YgwStaticHtml>;
        //            if (statics != null)
        //            {
        //                html = statics.FirstOrDefault();
        //            }
        //        }
        //    }

        //    return View(html);
        //}


        public JsonResult GetStaticsDetails(string id)
        {
            var details = new List<StaticDetailViewModel>();
            if (!string.IsNullOrWhiteSpace(id))
            {
                var sql = @"
SELECT * FROM Fct_StaticDetail WHERE Disabled = 0 AND StaticHtmlId ='{0}' ORDER BY Sort DESC";
                var modelFctStaticDetail = BaseConnection.Query<Model_Fct_StaticDetail>(string.Format(sql, id));
                details.AddRange(modelFctStaticDetail.Select(m => new StaticDetailViewModel()
                {
                    StaticDetailId = m.StaticDetailId,
                    StaticHtmlId = m.StaticHtmlId,
                    Name = m.Name,
                    Title = m.Title,
                    DetailType = m.DetailType,
                    HtmlBackgroundUrl = m.HtmlBackgroundUrl,
                    LucencyAnchor = m.LucencyAnchor,
                    CommodityCodes = m.CommodityCodes,
                    Tag = m.Tag,
                    Sort = m.Sort,
                    REC_CreateTime = m.REC_CreateTime.ToString("yyyy年MM月dd日"),
                    REC_CreateBy = m.REC_CreateBy,
                    REC_ModifyBy = m.REC_ModifyBy,
                }));
            }
            return GetJsonResult(new { data = details });
        }


        public JsonResult GetStaticsDetail(string id)
        {
            var detail = new StaticDetailViewModel();
            if (!string.IsNullOrWhiteSpace(id))
            {
                var sql = @"
SELECT * FROM Fct_StaticDetail WHERE Disabled = 0 AND StaticDetailId ='{0}' ORDER BY Sort DESC";
                var m = BaseConnection.Single<Model_Fct_StaticDetail>(string.Format(sql, id));
                detail = new StaticDetailViewModel()
                {
                    StaticDetailId = m.StaticDetailId,
                    StaticHtmlId = m.StaticHtmlId,
                    Name = m.Name,
                    Title = m.Title,
                    DetailType = m.DetailType,
                    HtmlBackgroundUrl = m.HtmlBackgroundUrl,
                    LucencyAnchor = m.LucencyAnchor,
                    CommodityCodes = m.CommodityCodes,
                    Tag = m.Tag,
                    Sort = m.Sort,
                    REC_CreateTime = m.REC_CreateTime.ToString("yyyy年MM月dd日"),
                    REC_CreateBy = m.REC_CreateBy,
                    REC_ModifyBy = m.REC_ModifyBy,
                };
            }
            return GetJsonResult(new { data = detail });
        }

        //public JsonResult SaveStaticsHtml(YgwStaticHtml html)
        //{
        //    var isSuccess = "0";
        //    if (html == null)
        //    {
        //        return Json(new { data = isSuccess }, JsonRequestBehavior.AllowGet);
        //    }
        //    if (string.IsNullOrWhiteSpace(html.StaticHtmlId))
        //    {
        //        html.REC_CreateBy = base.UserName == null ? "" : base.UserName.ToString();
        //    }
        //    else
        //    {
        //        html.REC_ModifyBy = base.UserName == null ? "" : base.UserName.ToString();
        //    }

        //    if (html.IsAutoDisabled == 1)
        //    {
        //        if (string.IsNullOrWhiteSpace(html.StartTime) || string.IsNullOrWhiteSpace(html.EndTime) || Convert.ToDateTime(html.EndTime) <= Convert.ToDateTime(html.StartTime))
        //        {
        //            return Json(new { data = "2" }, JsonRequestBehavior.AllowGet);
        //        }
        //    }

        //    var roleCode = base.UserSession.User.EmployeeType;
        //    if (roleCode != ERoleCode.None)
        //    {
        //        html.RoleCode = (int)roleCode;
        //    }

        //    #region  validate

        //    if (string.IsNullOrWhiteSpace(html.HtmlCode))
        //    {
        //        return Json(new { data = "4" }, JsonRequestBehavior.AllowGet);
        //    }
        //    if (string.IsNullOrWhiteSpace(html.HtmlName))
        //    {
        //        return Json(new { data = "5" }, JsonRequestBehavior.AllowGet);
        //    }

        //    #endregion

        //    var result = _activityServer.SaveHtml(html);
        //    if (result.State && result.ResultObj != null)
        //    {
        //        isSuccess = result.ResultObj.ToString();
        //    }
        //    YGOP.ESB.Log.LogManager.WriteAppWork("SaveStaticsHtml", string.Format("用户名：{0} Ip:{1}", base.UserName, IPUtil.GetIPAddr()));
        //    return Json(new { data = isSuccess }, JsonRequestBehavior.AllowGet);

        //}


        //public JsonResult SaveStaticsDetail(YgwStaticDetail detail)
        //{
        //    var isSuccess = "0";

        //    if (detail == null)
        //    {
        //        return Json(new { data = isSuccess }, JsonRequestBehavior.AllowGet);
        //    }

        //    var roleCode = base.UserSession.User.EmployeeType;
        //    if (roleCode == ERoleCode.None)
        //    {
        //        return Json(new { data = isSuccess }, JsonRequestBehavior.AllowGet);
        //    }
        //    if (string.IsNullOrWhiteSpace(detail.StaticDetailId))
        //    {
        //        detail.REC_CreateBy = base.UserName == null ? "" : base.UserName.ToString();
        //    }
        //    else
        //    {
        //        detail.REC_ModifyBy = base.UserName == null ? "" : base.UserName.ToString();
        //    }


        //    #region  validate

        //    if (string.IsNullOrWhiteSpace(detail.Name))
        //    {
        //        return Json(new { data = "5" }, JsonRequestBehavior.AllowGet);
        //    }

        //    #endregion

        //    if (!string.IsNullOrWhiteSpace(detail.StaticHtmlId))
        //    {
        //        if (string.IsNullOrWhiteSpace(detail.StaticDetailId))
        //        {
        //            detail.StaticDetailId = Guid.NewGuid().ToString();
        //        }
        //        if (detail.DetailType == (int)EDetailType.Html)
        //        {
        //            var checkCoupon = CheckCoupon(detail, roleCode);
        //            if (!checkCoupon.State)
        //            {
        //                return Json(new { data = isSuccess, msg = checkCoupon.BMsg }, JsonRequestBehavior.AllowGet);
        //            }
        //        }

        //        var result = _activityServer.SaveHtmlDetail(detail);
        //        if (result.State && result.ResultObj != null)
        //        {
        //            isSuccess = result.ResultObj.ToString();
        //        }
        //    }
        //    YGOP.ESB.Log.LogManager.WriteAppWork("SaveStaticsDetail", string.Format("用户名：{0} Ip:{1} detail.StaticHtmlId:{2}", base.UserName, IPUtil.GetIPAddr(), detail.StaticHtmlId));
        //    return Json(new { data = isSuccess }, JsonRequestBehavior.AllowGet);

        //}

        //public JsonResult CopyHtml(string code)
        //{
        //    var isSuccess = "";
        //    if (!string.IsNullOrWhiteSpace(code))
        //    {
        //        var result = _activityServer.CotyHtml(code, 1);
        //        if (result.State && result.ResultObj != null)
        //        {
        //            isSuccess = result.ResultObj.ToString();
        //        }
        //    }
        //    return Json(new { data = isSuccess }, JsonRequestBehavior.AllowGet);
        //}


        #region addCoupon


        //protected CallResult CheckCoupon(YgwStaticDetail m, ERoleCode roleCode)
        //{
        //    var result = new CallResult()
        //    {
        //        State = false,
        //        BMsg = "绑定优惠券失败"
        //    };
        //    if (string.IsNullOrWhiteSpace(m.LucencyAnchor))
        //    {
        //        result.State = true;
        //        return result;
        //    }
        //    var batchNoList = new List<string>();
        //    var anchors = m.LucencyAnchor.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
        //    if (anchors.Any())
        //    {
        //        batchNoList.AddRange(from a in anchors
        //                             let anchor = a.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries)
        //                             where anchor[1] == ((int)EActivityAreaType.GetCoupon).ToString()
        //                             select anchor[2]);
        //    }
        //    if (batchNoList.Count <= 0)
        //    {
        //        result.State = true;
        //        return result;
        //    }
        //    result = BindCoupon(new SaveActivityCouponRequest()
        //    {
        //        ActivityFloorId = m.StaticDetailId,
        //        Operater = base.UserName.ToString(),
        //        ActivityId = m.StaticHtmlId,
        //        DisplayRange = (int)PlatformEnum.Web,
        //        BatchNoList = batchNoList
        //    });
        //    return result;
        //}

        //protected CallResult BindCoupon(SaveActivityCouponRequest m)
        //{
        //    var result = new CallResult()
        //    {
        //        State = false,
        //        BMsg = "绑定优惠券失败"
        //    };
        //    var activityServer = new Proxy.ActivityServer();
        //    if (m != null && !string.IsNullOrWhiteSpace(m.ActivityId))
        //    {
        //        var response = activityServer.SaveActivityCoupon(m);
        //        if (response != null)
        //        {
        //            return response;
        //        }
        //    }
        //    return result;
        //}

        #endregion

        #region 预览

        //public ActionResult PreActivity(string id)
        //{
        //    var statics = new YgwStaticHtml();
        //    if (!string.IsNullOrWhiteSpace(id))
        //    {
        //        //html
        //        var result = _activityServer.GetStaticsByCode(id);
        //        if (result.State && result.ResultObj != null)
        //        {
        //            statics = result.ResultObj as YgwStaticHtml;
        //        }
        //    }

        //    //商品详情
        //    if (statics.Details != null && statics.Details.Any())
        //    {
        //        var commodityService = new CommodityService();
        //        foreach (var s in statics.Details)
        //        {
        //            if (s.DetailType == (int)EDetailType.List4 || s.DetailType == (int)EDetailType.List2 || s.DetailType == (int)EDetailType.List3)
        //            {
        //                var actresult = commodityService.GetActivityCommodities(id + s.Name, s.CommodityCodes, AreaCodeHelper.GetUserAreaCode().ToString(), YGWB.Web.Common.WebCommon.GetAreaId(), true);//上海
        //                if (actresult != null && actresult.State)
        //                {
        //                    s.Commodities = actresult.ResultObj as List<YgwCommodity>;
        //                }
        //            }
        //        }
        //    }

        //    return View(statics);
        //}



        #endregion 预览



        /// <summary>
        /// get act code
        /// </summary>
        /// <returns></returns>
        public string GetNewActCode()
        {
            var result = string.Empty;
            var now = DateTime.Now;
            var sql = @"SELECT COUNT(1) FROM Fct_StaticHtml WHERE Disabled = 0 AND Fct_StaticHtml.REC_CreateTime >= '{0}' AND Fct_StaticHtml.REC_CreateTime < '{1}'";
            var num = BaseConnection.Query<Model_Fct_StaticHtml>(string.Format(sql, now.ToString("yyyy-M-d 00:00:00"), now.ToString("yyyy-M-d 23:59:59")));
            result = now.ToString("yyyyMMdd") + num;
            return result;
        }

        #region private
        private PageResult<Model_Fct_StaticHtml> GetBasePage(int pageSize, int index)
        {
            var page = new PageResult<Model_Fct_StaticHtml>();
            index = index < 0 ? 0 : index;
            var start = (index - 1)* pageSize;
            var end = index * pageSize;
            var sql = @"
WITH LIST as
  (
    SELECT ROW_NUMBER() OVER(ORDER BY Fct_StaticHtml.Rec_CreateTime DESC) AS ROWNUM,
                          Fct_StaticHtml.*                   
FROM dbo.Fct_StaticHtml WHERE Fct_StaticHtml.Disabled = 0
  )  SELECT * FROM LIST WHERE ROWNUM BETWEEN {0} AND {1}";
            var sqlcount = @"
    SELECT COUNT(1) FROM dbo.Fct_StaticHtml WHERE Fct_StaticHtml.Disabled = 0";
            page.List = BaseConnection.Query<Model_Fct_StaticHtml>(string.Format(sql, start, end));
            page.Count = BaseConnection.Count(sqlcount);
            return page;
        }
        private List<Model_Fct_StaticHtml> GetStatics(string id = "")
        {
            var statics = new List<Model_Fct_StaticHtml>();
            var sql = string.Empty;
            if (string.IsNullOrWhiteSpace(id))
            {
                sql = @"SELECT * FROM Fct_StaticHtml WHERE Disabled = 0 AND HtmlType = 1 ORDER BY Fct_StaticHtml.REC_ModifyTime DESC";
                statics = BaseConnection.Query<Model_Fct_StaticHtml>(sql);
            }
            else
            {
                sql = @"SELECT * FROM Fct_StaticHtml WHERE Disabled = 0 AND HtmlType = 1 AND Fct_StaticHtml.StaticHtmlId ='{0}' ORDER BY Fct_StaticHtml.REC_ModifyTime DESC";
                statics = BaseConnection.Query<Model_Fct_StaticHtml>(string.Format(sql, id));
            }

            return statics;
        }

        #endregion
    }
}