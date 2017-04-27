

var business = {
    init: function () {
        this.setOptions();
        this.bindEvent();
        this.initpaging();
        this.initUploadFile();
        this.frameId = window.frameElement && window.frameElement.id || '';
    },
    setOptions: function () {
        this.addForm = $("#j_addForm");
        this.detailForm = $("#j_detailForm");
        this.detailList = this.addForm.find(".j_detailList");
        this.searchForm = $("#j_searchForm");
        this.commonDialog = $('#commonDialog');
        this.ImportDialog = $('#ImportDialog');
        
        this.idx = 0;

        this.detailModel = (function () {
            function detailModel() {
                this.Id = "";
                this.ActivityDate = "";
                this.TmallCommodityId = "";
                this.TmallCommodityName = "";
                this.IsBack = "";
                this.AvgCostPrice = "";
                this.CurrentCostPrice = "";
                this.FrontStockNum = "";
                this.BMRecommendPrice = "";
                this.BMRecommendDailyLimitNum = "";
                this.BMRecommendTotalLimitNum = "";
                this.BMRemark = "";
                this.StatusFlag = "";
            }
            return detailModel;
        })();

        this.searchModel = (function () {
            function searchModel() {
                this.Id = "";
                this.ResourceId = "";
                this.AreaId = "";
                this.AreaIds = "";
                this.StatusFlag = "";
                this.TmallCommodityName = "";
                this.TmallCommodityId = "";
                this.IsBack = "";
                this.StartTime = "";
                this.EndTime = "";
                this.CreateBy = "";
                this.PageIndex = 1;
                this.PageSize = 6;
            }
            return searchModel;
        })();
    },
    bindEvent: function () {
        var self = this;
        self.addForm.on("focusout", ".j_validTmallID", function () {
            var that = $(this),
                tr = $(that).parents(".t-tr"),
                result = true,
                detail = new self.detailModel(),
                data = {
                    business: {
                        ResourceId: self.addForm.find("#ResourceId").val(),
                        ActivityDate: self.addForm.find("#ActivityDate").val(),
                        AreaId: self.addForm.find("#AreaId").val(),
                        StatusFlag: self.addForm.find("#StatusFlag").val(),
                        Commoditys: [],
                    },
                    idx: self.idx
                };
            detail.TmallCommodityId = that.val();
            if (!that.valid()) {
                return false;
            }
            data.business.Commoditys.push(detail);
            ygopAjaxClient.async("/Business/SearchCommodity", "post", data, function (response) {
                result = response === "" || response.isSuccess === false ? false : true;
                if (result) {
                    tr.removeClass("j_trclone").html(response);
                } else {
                    common.failPopup(response.message);
                }
            });
        });

        self.addForm.validate({
            onkeyup: false,
            submitHandler: function () {
                var trs = self.addForm.find(".t-tbody .t-tr:not(.j_trclone)"),
                    list = [],
                    data = {
                        business: {
                            ResourceId: self.addForm.find("#ResourceId").val(),
                            ActivityDate: self.addForm.find("#ActivityDate").val(),
                            AreaId: self.addForm.find("#AreaId").val(),
                            StatusFlag: self.addForm.find("#StatusFlag").val(),
                            Commoditys: {},
                        }
                    };
                var HasNotGetDetail = false;
                $.each(trs, function (i, v) {
                    var tr = $(v).find(":input").serializeArray(),
                        detail = new self.detailModel();

                    if (tr.length < 3) {
                        HasNotGetDetail = true;
                    }
                    $.each(tr, function (i2, v2) {
                        for (var b in detail) {
                            if (v2.name.startsWith(b)) detail[b] = v2.value;
                        }
                    });
                    list.push(detail);
                });
                if (list.length <= 0 || HasNotGetDetail) {
                    common.failPopup("请先添加商品并填写信息");
                    return false;
                }
                
                data.business.Commoditys = list;
                ygopAjaxClient.async("/Business/PreCheck", "post", data, function (result) {
                    if (result.isSuccess) {
                        ygopAjaxClient.async("/Business/AddDetails", "post", data, function (result) {
                            if (result.isSuccess) {
                                common.successPopup(function () {
                                    window.tab.closeTab('商管新增商品', null);
                                });
                            } else {
                                common.failPopup(result.message);
                            }
                        });
                    } else {
                        common.confirm("提示", result.message, function () {
                            ygopAjaxClient.async("/Business/AddDetails", "post", data, function (result) {
                                if (result.isSuccess) {
                                    common.successPopup(function () {
                                        window.tab.closeTab('商管新增商品', null);
                                    });
                                } else {
                                    common.failPopup(result.message);
                                }
                            });
                        }, false);
                    }
                });
            }
        });


        self.addForm.on("focusout", "input[name^='BMRecommendTotalLimitNum_']", function () {
            var ele = $(this),
                result = false,
                than = self.addForm.find("input[name=" + ele.data("than") + "]"),
                data1 = Number(ele.val()),
                data2 = Number(than.val());
            if (ele.length > 0) {
                if (!isNaN(data1) && !isNaN(data2)) {
                    result = data1 <= data2;
                }
            }
            if (!result) {
                alert($(this).data("name") + "商品的活动数量大于前台库存数，请慎重填写");
            }
        });

        self.addForm.on("click", ".j_add", function () {
            var that = $(this),
               template = wrapper.find("trtemplate"),
               tbody = wrapper.find("div.t-tbody");
            if (template.length <= 0) {
                return false;
            }
            tbody.has("div.t-tr").length || tbody.html("");
            if (tbody.has("div.j_trclone").length) {
                alert("一次只能添加一条数据");
                return false;
            }
            self.idx++;
            tbody.append(template.html());
            commoninit.afterDataInit();
        });

        self.addForm.on("click", ".j_commit,.j_save", function () {
            var that = $(this);
            self.addForm.find("#StatusFlag").val(that.data("status"));
        });
        
        self.searchForm.on("click", ".j_search", function () {
            var startTime = new Date(self.searchForm.find("input[name='StartTime']").val()),
                endTime = new Date(self.searchForm.find("input[name='EndTime']").val());
            if (startTime >= endTime) {
                common.failPopup("进行时间搜索范围开始时间不能大于等于结束时间");
                return false;
            }
            common.pageIndex = 1;
            self.getSearchList(self.searchForm.find(".divlist"));
        });

        self.getSearchList= function (ele) {
            var tr = self.searchForm.find(":input").serializeArray(),
            areaIds = self.searchForm.find("select[name='AreaId']"),
            search = new self.searchModel(),
             data = {
                 request: []
             };
            $.each(tr, function (i2, v2) {
                for (var b in search) {
                        if (v2.name.startsWith(b)) search[b] = v2.value;
                }
            });
            search.AreaIds = areaIds.val() === "" || areaIds.val() === null ? "" : areaIds.val().join(",");
            search.PageIndex = common.pageIndex;
            data.request = search;
            ygopAjaxClient.async("/Business/GetGroupList", "post", data, function (result) {
                if (result != "" && $(ele).length > 0) {
                    $(ele).html(result);
                }
            });
        }

        self.searchForm.on("click", ".j_deldetail", function () {
            var that = $(this),
                data = {
                    id: that.data("id")
                },
            msg = "是否确定要" + that.text();

            common.confirm(msg, "", function () {
                ygopAjaxClient.async("/Business/Delete", "post", data, function (result) {
                    if (result.isSuccess) {
                        common.successPopup(function () {
                            self.getSearchList(self.searchForm.find(".divlist"));
                        });
                    } else {
                        common.failPopup(result.message);
                    }
                });
            });
        });

        self.searchForm.on("click", ".j_commit", function () {
            var that = $(this),
                data = {
                    id: that.data("id")
                },
            msg = "是否确定要" + that.text();

            common.confirm(msg, "", function () {
                ygopAjaxClient.async("/Business/Commit", "post", data, function (result) {
                    if (result.isSuccess) {
                        common.successPopup(function () {
                            self.getSearchList(self.searchForm.find(".divlist"));
                        });
                    } else {
                        common.failPopup(result.message);
                    }
                });
            });
        });

        self.searchForm.on("click", ".j_allcommit", function () {
            var that = $(this),
                data = {
                    id: self.searchForm.find("input[name='counterhid']").val()
                },
            msg = "是否确定要" + that.text();
            if (data.id === undefined || data.id === "") {
                common.failPopup("请选择提报商品");
                return false;
            }
            common.confirm(msg, "", function () {
                ygopAjaxClient.async("/Business/Commit", "post", data, function (result) {
                    if (result.isSuccess) {
                        common.successPopup(function () {
                            self.getSearchList(self.searchForm.find(".divlist"));
                        });
                    } else {
                        common.failPopup(result.message);
                    }
                });
            });
        });


        self.searchForm.find("select[name^='AreaId']").selectpicker({
            size: 6
        });
        self.searchForm.find(".t-tbody input[name='AreaId']").each(function (i, v) {
            var val = $(v).val().split(","),
                select = $(v).siblings("div.bootstrap-select");
            select.find("select[name^='AreaId']").selectpicker('val', val);
        });


        //self.searchForm.on("click", ".j_edit", function () {
        //    var that = $(this),
        //         data = {
        //             id: that.data("id")
        //         };
        //    ygopAjaxClient.async("/Business/GetDetail", "post", data, function (result) {
        //        if (result != "") {
        //            self.commonDialog.find("div.j_dialoglist").html(result);
        //            self.openDialog(data.id, "商品编辑");
        //            commoninit.dateInputInit();
        //            commoninit.afterDataInit();
        //        }
        //        if (result.message != "" && result.message != undefined) {
        //            common.failPopup(result.message);
        //        }
        //    });
        //});

        //self.searchForm.on("click", "#btnImportExcel", function () {
        //    self.ImportDialog.modal('show');
        //});

        //self.searchForm.on("click", ".j_downexcel", function () {
        //    var that = $(this),
        //         data = {
        //             id: that.data("id")
        //         };
        //    self.DownExcel();
        //});

        self.detailForm.on("click", ".j_commit", function () {
            var that = $(this);
            self.detailForm.find("#StatusFlag").val(that.data("status"));
        });
        self.detailForm.validate({
            onkeyup: false,
            submitHandler: function() {
                var that = $(this),
                    detail = new self.detailModel(),
                    data = {
                        business: {
                            ResourceId: self.detailForm.find("#ResourceId").val(),
                            ActivityDate: self.detailForm.find("#ActivityDate").val(),
                            AreaId: self.detailForm.find("#AreaId").val(),
                            StatusFlag: self.detailForm.find("#StatusFlag").val(),
                            Commoditys: [],
                        }
                    };
                var tr = $(self.detailForm).find(":input").serializeArray();
                $.each(tr, function(i2, v2) {
                    for (var b in detail) {
                        if (v2.name.startsWith(b)) detail[b] = v2.value;
                    }
                });
                data.business.Commoditys.push(detail);
                ygopAjaxClient.async("/Business/Edit", "post", data, function(result) {
                    if (result.isSuccess) {
                        common.successPopup(function () {
                            window.tab.closeTab('编辑商品', null);
                        });
                    } else {
                        common.failPopup(result.message);
                    }
                });
            }
        });


        self.detailForm.on("click", ".j_deldetail", function () {
            var self = this;
            var that = $(this),
                data = {
                    id: that.data("id")
                },
            msg = "是否确定要" + that.text();
            common.confirm(msg, "", function () {
                ygopAjaxClient.async("/Business/Delete", "post", data, function (result) {
                    if (result.isSuccess) {
                        common.successPopup(function () {
                            window.tab.closeTab(self.frameId);
                        });
                    } else {
                        common.failPopup(result.message);
                    }
                });
            });
        });
    },
    initpaging: function () {
        var self = this;
        if (self.searchForm.length > 0) {
            common.paging2(self.searchForm.find(".divlist"), self.getSearchList);
        }
        //else if (detailForm.length > 0) {
        //    common.paging2(detailForm.find(".j_couponList"), getCouponList);
        //    common.paging2(detailForm.find(".j_activityList"), getActList);
        //    common.paging2(detailForm.find(".j_commdityList"), getCommdityList);
        //}
    },
    initUploadFile: function () {
        var self = this;
        var btnImport = this.searchForm.find("#btnImport");
        var up = new BDFileUpload({

            pick: "btnImport",
            accept: {
                title: "Excel",
                extensions: "xlsx",
                mimeTypes: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            },
            baseUrl: btnImport.data("baseurl"),
            server: btnImport.data("upurl"),
            reload: true,
            onUploadSuccess: function (response) {
                if (response.isSuccess) {
                    btnImport.text("excel导入商品");
                    var data = { fileName: response.data.fileName };
                    ygopAjaxClient.async("/Business/InsertExcel", "post", data, function (result) {
                        if (result.isSuccess) {
                            alert(result.message);
                            self.getSearchList(self.searchForm.find(".divlist"));
                        } else {
                            common.failPopup(result.message);
                        }
                    });
                    
                } else {
                    alert("上传失败!");
                }
            }
        });
    },
    openDialog : function (id, tips) {
        var self = this;
        if (tips != "") {
            self.commonDialog.find("h4.modal-title").html(tips);
        }
        self.commonDialog.data("id", id);
        self.commonDialog.modal('show');
    }
};

$(function () {
    business.init();
});
