using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using DapperExtensions;
using Sale4.Controllers.API.Models;
using Sale4.Controllers.Common;
using Sale4.Models;
using Utility.Extensions;

namespace Sale4.Controllers.API
{
    public class ActivityApiController : BaseController
    {

        public JsonResult GetDetail(string id)
        {
            var staticHtml = new StaticHtmlViewModel();
            var now = DateTime.Now;
            var htmlId = id.ToGuid();

            if (htmlId == Guid.Empty)
            {
                staticHtml.HtmlCode = "";
                staticHtml.IsAutoDisabled = 1;
                staticHtml.StartTime = now.ToString("yyyy-MM-dd hh:mm:ss");
                staticHtml.EndTime = now.AddDays(1).ToString("yyyy-MM-dd hh:mm:ss");                
            }
            else
            {
                var firstOrDefault = BaseConnection.QueryFirst(new Fct_StaticHtml { StaticHtmlId = htmlId });
                if (firstOrDefault != null && firstOrDefault.StaticHtmlId != Guid.Empty)
                {
                    staticHtml = new StaticHtmlViewModel
                    {
                        StaticHtmlId = firstOrDefault.StaticHtmlId,
                        HtmlCode = firstOrDefault.HtmlCode,
                        HtmlUrl = firstOrDefault.htmlUrl,
                        IsAutoDisabled = firstOrDefault.IsAutoDisabled,
                        HtmlName = firstOrDefault.htmlName,
                        HtmlBannerUrl = firstOrDefault.HtmlBannerUrl,
                        HtmlAnimateUrl = firstOrDefault.HtmlAnimateUrl,
                        HtmlBackgroundUrl = firstOrDefault.HtmlBackgroundUrl,
                        HtmlType = firstOrDefault.HtmlType,
                        REC_CreateTime = firstOrDefault.REC_CreateTime.ToString("yyyy-MM-dd hh:mm:ss"),
                        REC_CreateBy = firstOrDefault.REC_CreateBy,
                        REC_ModifyBy = firstOrDefault.REC_ModifyBy,
                        StartTime = firstOrDefault.StartTime.ToString("yyyy-MM-dd hh:mm:ss"),
                        EndTime = firstOrDefault.EndTime.ToString("yyyy-MM-dd hh:mm:ss"),
                    };
                }
            }

            return JsonSuccess(staticHtml);
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
                        IsAutoDisabled = r.IsAutoDisabled,
                        EndTime = r.EndTime.ToString("yyyy年MM月dd日"),
                        StartTime = r.StartTime.ToString("yyyy年MM月dd日"),
                        REC_CreateBy = r.REC_CreateBy,
                        REC_ModifyBy = r.REC_ModifyBy,
                        REC_CreateTime = r.REC_CreateTime.ToString("yyyy年MM月dd日")
                    }
                    ).ToList();
                page.List = page.List.OrderByDescending(e => e.REC_CreateTime).ThenBy(e => e.ExpiresState).ToList();
                page.Count = page.List.Count;
            }

            return JsonSuccess(new { data = page.List, pageCount = page.PageCount, allCount = page.Count });
        }

        public JsonResult DeleteStatics(string id)
        {
            var staticid = id.ToGuid();
            var now = DateTime.Now;
            
            var result = 0;
            if (staticid != Guid.Empty)
            {
                var staticHtml = BaseConnection.QueryFirst(new Fct_StaticHtml()
                {
                    StaticHtmlId = staticid
                });
                staticHtml.Disabled = 1;
                staticHtml.REC_ModifyBy = UserSession;
                staticHtml.REC_ModifyTime = now;
                result = BaseConnection.Update(staticHtml);
            }
            //Log.LogManager.WriteAppWork("DeleteStaticsDetail", string.Format("用户名：{0} Ip:{1} id:{2}", base.UserName, IPUtil.GetIPAddr(), id));
            return JsonSuccess(result);
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
            return JsonSuccess(result);
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
            return JsonSuccess(details);
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
            return JsonSuccess(detail);
        }

        public JsonResult SaveStaticsHtml(StaticHtmlViewModel html)
        {
            var result = 0;
            var now = DateTime.Now;
            var htmlId = html.StaticHtmlId == Guid.Empty ? Guid.NewGuid() : html.StaticHtmlId;
            #region  validate
            if (html == null)
            {
                return JsonFail();
            }
            if (html.IsAutoDisabled == 1)
            {
                if (string.IsNullOrWhiteSpace(html.StartTime) || string.IsNullOrWhiteSpace(html.EndTime) || html.EndTime.ToDatetime() <= html.StartTime.ToDatetime())
                {
                    return JsonFail("结束时间不能小于开始时间或者为空");
                }
            }
            if (string.IsNullOrWhiteSpace(html.HtmlName))
            {
                return JsonFail("名称不能为空");
            }
            #endregion

            if (html.StaticHtmlId != Guid.Empty)
            {
                var m = BaseConnection.QueryFirst<Fct_StaticHtml>(new Fct_StaticHtml
                {
                    StaticHtmlId = html.StaticHtmlId
                });
                m.htmlName = html.HtmlName;
                m.StartTime = html.StartTime.ToDatetime();
                m.EndTime = html.EndTime.ToDatetime();
                m.HtmlAnimateUrl = html.HtmlAnimateUrl;
                m.htmlUrl = html.HtmlUrl;
                m.HtmlBackgroundUrl = html.HtmlBackgroundUrl;
                m.HtmlBannerUrl = html.HtmlBannerUrl;
                m.IsAutoDisabled = html.IsAutoDisabled;
                m.REC_ModifyBy = base.UserSession;
                m.REC_ModifyTime = now;
                result = BaseConnection.Update(m);
                
            }
            else
            {
                var m = new Fct_StaticHtml()
                {
                    StaticHtmlId = htmlId,
                    HtmlCode = GetNewActCode(),
                    htmlName = html.HtmlName,
                    StartTime = html.StartTime.ToDatetime(),
                    EndTime = html.EndTime.ToDatetime(),
                    HtmlAnimateUrl = html.HtmlAnimateUrl,
                    htmlUrl = html.HtmlUrl,
                    HtmlType = 1,
                    Disabled = 0,
                    HtmlBackgroundUrl = html.HtmlBackgroundUrl,
                    HtmlBannerUrl = html.HtmlBannerUrl,
                    IsAutoDisabled = html.IsAutoDisabled,
                    REC_ModifyBy = base.UserSession,
                    REC_ModifyTime = now,
                    REC_CreateBy = base.UserSession,
                    REC_CreateTime = now
                };
                result = BaseConnection.Insert(m);
            }
            if (result > 0)
            {
                return JsonSuccess(htmlId);
            }
            //add log

            return JsonFail();
        }


        public JsonResult SaveStaticsDetail(StaticDetailViewModel detail)
        {
            var result = 0;
            var now = DateTime.Now;
            #region  validate
            if (detail == null || detail.StaticHtmlId == Guid.Empty)
            {
                return JsonFail();
            }
            else if (string.IsNullOrWhiteSpace(detail.Name))
            {
                return JsonFail("名称不能为空");
            }

            #endregion
            //to do 优惠券
            //if (detail.DetailType == (int)EDetailType.Html)
            //{
            //    var checkCoupon = CheckCoupon(detail, roleCode);
            //    if (!checkCoupon.State)
            //    {
            //        return Json(new { data = isSuccess, msg = checkCoupon.BMsg }, JsonRequestBehavior.AllowGet);
            //    }
            //}
            if (detail.StaticDetailId == Guid.Empty)
            {
                var m = new Fct_StaticDetail()
                {
                    StaticHtmlId = detail.StaticHtmlId,
                    StaticDetailId = Guid.NewGuid(),
                    Title = detail.Title,
                    Name = detail.Name,
                    HtmlBackgroundUrl = detail.HtmlBackgroundUrl,
                    CommodityCodes = detail.CommodityCodes,
                    LucencyAnchor = detail.LucencyAnchor,
                    Tag = detail.Tag,
                    DetailType =  detail.DetailType,
                    Sort = detail.Sort,

                    Disabled = 0,
                    REC_ModifyBy = base.UserSession,
                    REC_ModifyTime = now,
                    REC_CreateBy = base.UserSession,
                    REC_CreateTime = now
                };
                result = BaseConnection.Insert(m);
            }
            else
            {
                var m = BaseConnection.QueryFirst(new Fct_StaticDetail
                {
                    StaticDetailId = detail.StaticDetailId
                });
                m.Title = detail.Title;
                m.Name = detail.Name;
                m.HtmlBackgroundUrl = detail.HtmlBackgroundUrl;
                m.CommodityCodes = detail.CommodityCodes;
                m.LucencyAnchor = detail.LucencyAnchor;
                m.Tag = detail.Tag;
                m.DetailType = detail.DetailType;
                m.Sort = detail.Sort;
                m.REC_ModifyBy = base.UserSession;
                m.REC_ModifyTime = now;

                result = BaseConnection.Update(m);
            }

          //  YGOP.ESB.Log.LogManager.WriteAppWork("SaveStaticsDetail", string.Format("用户名：{0} Ip:{1} detail.StaticHtmlId:{2}", base.UserName, IPUtil.GetIPAddr(), detail.StaticHtmlId));
            return result > 0 ? JsonSuccess() : JsonFail();

        }

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


        #region pre
        /// <summary>
        /// 获取活动页面详情ByCode
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public PreActivityViewModel GetActivityBaseByCode(string code)
        {
            var activity = new PreActivityViewModel();

            try
            {
                var statichtml = new Fct_StaticHtml()
                {
                    HtmlCode = code,
                    Disabled = 0
                };
                if (!string.IsNullOrWhiteSpace(code))
                {
                    activity.FctStaticHtml = BaseConnection.QueryFirst(statichtml);
                }
                if (activity.FctStaticHtml != null && activity.FctStaticHtml.StaticHtmlId != Guid.Empty)
                {
                    var sql = @"SELECT * FROM Fct_StaticDetail WHERE Disabled = @Disabled AND StaticHtmlId =@StaticHtmlId ORDER BY sort DESC";
                    activity.FctStaticDetails = BaseConnection.Query<Fct_StaticDetail>(sql, new Fct_StaticDetail()
                    {
                        StaticHtmlId = statichtml.StaticHtmlId,
                        Disabled = 0
                    });
                }
            }
            catch (Exception ex)
            {
                //YGOP.ESB.Log.LogManager.WriteAppError(code, ex);
            }

            return activity;
        }
        /// <summary>
        /// 获取活动商品ByCode
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public PreActivityViewModel GetActivityCommodity(string code)
        {
            var activity = new PreActivityViewModel();

            try
            {
                var statichtml = new Fct_StaticHtml()
                {
                    HtmlCode = code,
                    Disabled = 0
                };
                if (!string.IsNullOrWhiteSpace(code))
                {
                    activity.FctStaticHtml = BaseConnection.QueryFirst(statichtml);
                }
                if (activity.FctStaticHtml != null && activity.FctStaticHtml.StaticHtmlId != Guid.Empty)
                {
                    var sql = @"SELECT * FROM Fct_StaticDetail WHERE Disabled = 0 AND StaticHtmlId =@StaticHtmlId AND DetailType IN ({0},{1},{2}) ORDER BY sort DESC";
                    activity.FctStaticDetails = BaseConnection.Query<Fct_StaticDetail>(string.Format(sql,(int)EDetailType.List4,(int)EDetailType.List2,(int)EDetailType.List3),new Fct_StaticDetail()
                    {
                        StaticHtmlId = statichtml.StaticHtmlId,
                        Disabled = 0
                    });
                }
            }
            catch (Exception ex)
            {
                //YGOP.ESB.Log.LogManager.WriteAppError(code, ex);
            }

            return activity;
        }
        #endregion

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

        public ActionResult PreActivity(string code)
        {
            var activity = new PreActivityViewModel();
            if (!string.IsNullOrWhiteSpace(code))
            {
                //html
                activity = GetActivityBaseByCode(code);
            }


            return View(activity);
        }


        #endregion 预览



        /// <summary>
        /// get act code by createtime
        /// </summary>
        /// <returns></returns>
        public string GetNewActCode()
        {
            var result = string.Empty;
            var now = DateTime.Now;

            var sql = @"SELECT COUNT(1) FROM Fct_StaticHtml WHERE Disabled = 0 AND Fct_StaticHtml.REC_CreateTime >= '{0}' AND Fct_StaticHtml.REC_CreateTime < '{1}'";
            var num = BaseConnection.Scalar<int>(string.Format(sql, now.ToString("yyyy-M-d 00:00:00"), now.ToString("yyyy-M-d 23:59:59")));
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