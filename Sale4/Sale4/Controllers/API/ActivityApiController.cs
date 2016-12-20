﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using DapperExtensions;
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
            var staticHtml = GetStatics();
            var statics = staticHtml.Select(s => new StaticHtmlViewModel()
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
            }).ToList();
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

        public JsonResult DeleteStatics(string id)
        {
            var result = 0;
            if (!string.IsNullOrWhiteSpace(id) && new Guid(id)!=Guid.Empty)
            {
                var staticHtml = new Fct_StaticHtml()
                {
                    StaticHtmlId = new Guid(id),
                    Disabled = 1
                };
                result = BaseConnection.Update(staticHtml);
            }
            //Log.LogManager.WriteAppWork("DeleteStaticsDetail", string.Format("用户名：{0} Ip:{1} id:{2}", base.UserName, IPUtil.GetIPAddr(), id));
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteStaticsDetail(string id)
        {
            var result = 0;
            if (!string.IsNullOrWhiteSpace(id) && new Guid(id) != Guid.Empty)
            {
                var staticDetail = new Fct_StaticDetail()
                {
                    StaticDetailId = new Guid(id),
                    Disabled = 1
                };
                var sql = @"UPDATE dbo.Fct_StaticDetail SET Disabled = 1 WHERE StaticDetailId =@StaticDetailId";
                result = BaseConnection.Update(staticDetail);
            }
            //Log.LogManager.WriteAppWork("DeleteStaticsDetail", string.Format("用户名：{0} Ip:{1} id:{2}", base.UserName, IPUtil.GetIPAddr(), id));
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStaticsDetails(string id)
        {
            var details = new List<StaticDetailViewModel>();
            if (!string.IsNullOrWhiteSpace(id))
            {
                var sql = @"
SELECT * FROM Fct_StaticDetail WHERE Disabled = 0 AND StaticHtmlId =@StaticHtmlId ORDER BY Sort DESC";
                var modelFctStaticDetail = BaseConnection.Query<Fct_StaticDetail>(sql, new Fct_StaticDetail { StaticHtmlId = new Guid(id) });
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
                var m = BaseConnection.QueryFirst<Fct_StaticDetail>(new Fct_StaticDetail
                {
                    StaticDetailId = new Guid(id)
                });
                if (m != null)
                {
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
            }
            return GetJsonResult(new { data = detail });
        }

        public JsonResult SaveStaticsHtml(StaticHtmlViewModel html)
        {
            var result = 0;
            var now = DateTime.Now;
            var userName = base.UserSession ?? "";
            #region  validate
            if (html == null)
            {
                return GetJsonResult(new { data = 0 ,msg="保存失败"});
            }
            if (html.IsAutoDisabled == 1)
            {
                if (string.IsNullOrWhiteSpace(html.StartTime) || string.IsNullOrWhiteSpace(html.EndTime) || Convert.ToDateTime(html.EndTime) <= Convert.ToDateTime(html.StartTime))
                {
                return GetJsonResult(new { data = 0 ,msg="结束时间不能小于开始时间或者为空"});
                }
            }
            if (string.IsNullOrWhiteSpace(html.HtmlCode))
            {
                return GetJsonResult(new { data = 0 ,msg="编号不能为空"});
            }
            if (string.IsNullOrWhiteSpace(html.HtmlName))
            {
                return GetJsonResult(new { data = 0 ,msg="名称不能为空"});
            }
            #endregion

            if (html.StaticHtmlId != Guid.Empty)
            {
                var m = BaseConnection.QueryFirst<Fct_StaticHtml>(new Fct_StaticHtml
                {
                    StaticHtmlId = html.StaticHtmlId
                });
                m.htmlName = html.HtmlName;
                m.StartTime = Convert.ToDateTime(html.StartTime);
                m.EndTime = Convert.ToDateTime(html.EndTime);
                m.HtmlAnimateUrl = html.HtmlAnimateUrl;
                m.htmlUrl = html.HtmlUrl;
                m.HtmlBackgroundUrl = html.HtmlBackgroundUrl;
                m.HtmlBannerUrl = html.HtmlBannerUrl;
                m.IsAutoDisabled = html.IsAutoDisabled;
                m.REC_ModifyBy = userName;
                m.REC_ModifyTime = now;
                result = BaseConnection.Update(m);
                
            }
            else
            {
                var m = new Fct_StaticHtml()
                {
                    StaticHtmlId = Guid.NewGuid(),
                    HtmlCode = GetNewActCode(),
                    htmlName = html.HtmlName,
                    StartTime = Convert.ToDateTime(html.StartTime),
                    EndTime = Convert.ToDateTime(html.EndTime),
                    HtmlAnimateUrl = html.HtmlAnimateUrl,
                    htmlUrl = html.HtmlUrl,
                    HtmlType = 1,
                    Disabled = 0,
                    HtmlBackgroundUrl = html.HtmlBackgroundUrl,
                    HtmlBannerUrl = html.HtmlBannerUrl,
                    IsAutoDisabled = html.IsAutoDisabled,
                    REC_ModifyBy = userName,
                    REC_ModifyTime = now,
                    REC_CreateBy = userName,
                    REC_CreateTime = now
                };
                result = BaseConnection.Insert(m);
            }
            if (result > 0)
            {
                return GetJsonResult(new { data = 1});
            }
            //add log

            return GetJsonResult(new { data = 0, msg = "保存失败" });
        }


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
            var num = BaseConnection.Query<Fct_StaticHtml>(string.Format(sql, now.ToString("yyyy-M-d 00:00:00"), now.ToString("yyyy-M-d 23:59:59")));
            result = now.ToString("yyyyMMdd") + num;
            return result;
        }

        #region private
        private PageResult<Fct_StaticHtml> GetBasePage(int pageSize, int index)
        {
            var page = new PageResult<Fct_StaticHtml>();
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
            page.List = BaseConnection.Query<Fct_StaticHtml>(string.Format(sql, start, end));
            page.Count = BaseConnection.Scalar<int>(sqlcount);
            return page;
        }
        private List<Fct_StaticHtml> GetStatics(string id = "")
        {
            var statics = new List<Fct_StaticHtml>();
            var sql = string.Empty;
            if (string.IsNullOrWhiteSpace(id))
            {
                sql = @"SELECT * FROM Fct_StaticHtml WHERE Disabled = 0 AND HtmlType = 1 ORDER BY Fct_StaticHtml.REC_ModifyTime DESC";
                statics = BaseConnection.Query<Fct_StaticHtml>(sql);
            }
            else
            {
                sql = @"SELECT * FROM Fct_StaticHtml WHERE Disabled = 0 AND HtmlType = 1 AND Fct_StaticHtml.StaticHtmlId ='{0}' ORDER BY Fct_StaticHtml.REC_ModifyTime DESC";
                statics = BaseConnection.Query<Fct_StaticHtml>(string.Format(sql, id));
            }

            return statics;
        }


        #endregion
    }
}