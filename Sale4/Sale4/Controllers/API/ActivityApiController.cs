using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Proxy;
using Sale4.Models;

namespace Sale4.Controllers.API
{
    public class ActivityApiController : BaseApiController
    {
        BLL_Fct_ActivityBase _activityBase = new BLL_Fct_ActivityBase();

        //public JsonResult GetDetail(string id)
        //{
        //    var now = DateTime.Now;
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

        //    return GetJsonResult(new { data = html });
        //}


        //public JsonResult GetStatics()
        //{
        //    var roleCode = base.UserSession.User.EmployeeType;

        //    var statics = new List<YgwStaticHtml>();
        //    if (roleCode != ERoleCode.None)
        //    {
        //        var result = _activityServer.GetStatics((int)roleCode);
        //        if (result.State && result.ResultObj != null)
        //        {
        //            statics = result.ResultObj as List<YgwStaticHtml>;
        //        }
        //        if (statics != null && statics.Any())
        //        {
        //            statics = statics.OrderBy(e => e.ExpiresState).ToList();
        //        }
        //    }

        //    return Json(new { data = statics }, JsonRequestBehavior.AllowGet);
        //}


        public JsonResult GetStaticsPage(int pageSize, int index)
        {
            try
            {
                var page = new PageResult<ActivityBaseViewModels>()
                {
                    PageSize = pageSize,
                    List = new List<ActivityBaseViewModels>()
                };

                var result = _activityBase.GetBasePage(pageSize, index);
                if (result != null && result.Count > 0)
                {
                    page.List = (from r in result.List
                        select new ActivityBaseViewModels
                        {
                            ActivityId = r.ActivityId,
                            ActivityCode = r.ActivityCode,
                            ActivityName = r.ActivityName,
                            EndTime = r.EndTime.ToString("yyyy年MM月dd日"),
                            StartTime = r.StartTime.ToString("yyyy年MM月dd日"),
                            IsPublish = r.IsPublish,
                            REC_CreateBy = r.REC_CreateBy,
                            REC_ModifyBy = r.REC_ModifyBy,
                            REC_CreateTime = r.REC_CreateTime.ToString("yyyy年MM月dd日"),
                            REC_ModifyTime = r.REC_ModifyTime.ToString("yyyy年MM月dd日")
                        }
                        ).ToList();
                    page.List = page.List.OrderBy(e => e.ExpiresState).ToList();
                    page.Count = page.Count;
                }
                
                return Json(new { data = page.List, pageCount = page.PageCount, allCount = page.Count }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

            }
            return null;
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


        //public JsonResult GetStaticsDetails(string id)
        //{
        //    var details = new List<YgwStaticDetail>();
        //    if (!string.IsNullOrWhiteSpace(id))
        //    {
        //        var result = _activityServer.GetStaticsDetailByHtmlId(id);
        //        if (result.State && result.ResultObj != null)
        //        {
        //            details = result.ResultObj as List<YgwStaticDetail>;
        //        }
        //    }
        //    return Json(new { data = details }, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult GetStaticsDetail(string id)
        //{
        //    var detail = new YgwStaticDetail();
        //    if (!string.IsNullOrWhiteSpace(id))
        //    {
        //        var result = _activityServer.GetStaticsDetailByDetailId(id);
        //        if (result.State && result.ResultObj != null)
        //        {
        //            detail = result.ResultObj as YgwStaticDetail;
        //        }
        //    }
        //    return Json(new { data = detail }, JsonRequestBehavior.AllowGet);
        //}

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
    }
}