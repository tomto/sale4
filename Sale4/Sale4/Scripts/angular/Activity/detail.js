



var mainApp = angular.module("mainApp", ["ngRoute", "activityService", "datetimepicker", "hjtagsinput", "hjcanvas", "hjupload", "textcanvas", "ui.colorpicker", "fontColor"]);

mainApp.config(["$routeProvider",
  function ($routeProvider) {
      $routeProvider.
          when("/", {
              templateUrl: "/Activity/BasicInfo"
          }).
          when("/Edit/:id", {              
              templateUrl: "/Activity/BasicInfo"
          }).
          when("/Add", {
              templateUrl: "/Activity/BasicInfo"
          }).
          when("/Floor/Add/:id", {
              templateUrl: "/Activity/FloorDetail"
          }).
          when("/Floor/Edit/:id/:floorid", {
              templateUrl: "/Activity/FloorDetail"
          }).
          otherwise({
              redirectTo: "/"
          });
  }]);

mainApp.controller("detailCtrl", function ($scope, $routeParams, activityService) {
    $scope.norepeat = true;
    $scope.floorsData = [];
    $scope.ActivityBase = new fmModel.ActivityBase();
    $scope.StaticHtmlId = $routeParams.id || "";
    $scope.eDetailType = fmModel.eDetailType;

    $scope.initFloors = function() {
        activityService.GetStaticsDetails($scope.StaticHtmlId, function (result) {
            $scope.floorsData = result.data;
        });
    };

    $scope.initDetail = function () {
        if ($scope.norepeat && $scope.StaticHtmlId != "") {
            $scope.norepeat = false;
            activityService.GetDetail($scope.StaticHtmlId, function (result) {
                $scope.ActivityBase = result.data;
                $scope.norepeat = true;
            });
            $scope.initFloors();
        }
    };
    $scope.initDetail();


    $scope.delFloor = function (id) {
        $.messager.confirm("Delete", "是否删除!", function () {
            if ($scope.norepeat) {
                $scope.norepeat = false;
                activityService.DeleteStaticsDetail(id, function (result) {
                    $scope.norepeat = true;
                    if (result.data.ResultObj > 0) {
                        $scope.initFloors();
                    }
                });
            }
        });
    };

    $scope.timeChecked = function (ele) {
        $("#checkbox1").prop("checked") ? $scope.fmStaticHtml.IsAutoDisabled = 1 : $scope.fmStaticHtml.IsAutoDisabled = 0;
    };
    

    $scope.submitDetail = function () {
        if ($scope.fmStaticHtml.HtmlCode === "") {
            $.messager.alert("编号不能为空!");
            return false;
        } else if ($scope.fmStaticHtml.HtmlName === "") {
            $.messager.alert("名称不能为空!");
            return false;
        }
        $.messager.confirm("Save", "是否保存修改!", function() {
            if ($scope.norepeat) {
                $scope.norepeat = false;
                activityService.SaveStaticsHtml($scope.fmStaticHtml, function(result) {
                    $scope.norepeat = true;
                    if (result.data == 1) {
                        $.messager.alert("保存成功!");
                        window.location.href = "/actIndex";
                    } else if (result.data == 2) {
                        $.messager.alert("结束时间不能小于开始时间或者为空!");
                    } else if (result.data == 3) {
                        $.messager.alert("编号已存在!");
                    } else if (result.data == 4) {
                        $.messager.alert("编号不能为空!");
                    } else if (result.data == 5) {
                        $.messager.alert("名称不能为空!");
                    } else {
                        $.messager.alert("保存失败!");
                    }
                });
            }
        });
    };
});





mainApp.controller("floorCtrl", function ($scope, $routeParams, activityService) {
    $scope.norepeat = true;
    $scope.StaticHtmlId = $routeParams.id || "";
    $scope.StaticDetailId = $routeParams.floorid || "";
    $scope.fmStaticDetail = new fmModel.fmStaticDetail();
    $scope.fmStaticDetail.StaticHtmlId = $routeParams.id || "";
    $scope.eDetailType = fmModel.eDetailType;


    $scope.initFloor = function() {
        if ($scope.norepeat && $scope.StaticDetailId != "") {
            $scope.norepeat = false;
            activityService.GetStaticsDetail($scope.StaticDetailId, function (result) {
                    $scope.fmStaticDetail = result.data;
                $scope.norepeat = true;
            });
        };
    };
    $scope.initFloor();
    $scope.SelectDetailType = function (type) {
        $scope.fmStaticDetail.DetailType = type;
    };


    $scope.initAnchor = function () {
        $scope.floorAnchors = [];
        if ($scope.fmStaticDetail.LucencyAnchor !== "" && $scope.fmStaticDetail.LucencyAnchor != null && !$scope.fmStaticDetail.LucencyAnchor.startsWith("1#")) {
            $.each($scope.FtoH($scope.fmStaticDetail.LucencyAnchor).split("||"), function (idx, val) {
                if (val != "" && val != undefined) {
                    var xy = $scope.FtoH(val).split("|");
                    if (xy != null && xy.length > 0 && xy[0] != undefined && xy[0] != "") {
                        var floorAnchor = new fmModel.fmAnchor();
                        floorAnchor.Coord = xy[0];
                        floorAnchor.Type = xy[1];
                        floorAnchor.Operate = xy[2];
                        floorAnchor.Index = idx;
                        $scope.floorAnchors.push(floorAnchor);
                    }
                }
            });
        }
    };

    $scope.initProCode = function () {
        $scope.floorProCode = [];
        if ($scope.fmStaticDetail.LucencyAnchor !== "" && $scope.fmStaticDetail.LucencyAnchor != null && $scope.fmStaticDetail.LucencyAnchor.startsWith("1#")) {
            $.each($scope.FtoH($scope.fmStaticDetail.LucencyAnchor).substring(2).split("||"), function (idx, val) {
                if (val != "" && val != undefined) {
                    var xy = $scope.FtoH(val).split("|");
                    if (xy != null && xy.length > 0 && xy[0] != undefined && xy[0] != "") {
                        var floorProCode = new fmModel.fmProCode();
                        floorProCode.Coord = xy[0];
                        floorProCode.CoordX = xy[0].split(",")[0];
                        floorProCode.CoordY = xy[0].split(",")[1];
                        floorProCode.Content = xy[1];
                        floorProCode.FontColor = xy[2];
                        floorProCode.FontSize = xy[3];
                        floorProCode.Index = idx;
                        $scope.floorProCode.push(floorProCode);
                    }
                }
            });
        }
    };

    $scope.$watch("fmStaticDetail.LucencyAnchor", function () {
        if ($scope.fmStaticDetail.DetailType == $scope.eDetailType.Html) {
            $scope.initAnchor();
        } else if ($scope.fmStaticDetail.DetailType == $scope.eDetailType.ProCode) {
            $scope.initProCode();
        }

    });

    $scope.$watch("fmStaticDetail.DetailType", function () {
        if ($scope.fmStaticDetail.DetailType == $scope.eDetailType.Html) {
            $scope.initAnchor();
        } else if ($scope.fmStaticDetail.DetailType == $scope.eDetailType.ProCode) {
            $scope.initProCode();
        }
    });


    $scope.submitFloor = function() {
        $.messager.confirm("Save", "是否保存修改!", function () {
            if ($scope.norepeat) {
                var result = true, anchor = [];
                if ($scope.fmStaticDetail.Name === "") {
                    $.messager.alert("名称不能为空!");
                    return false;
                }
                if ($scope.fmStaticDetail.DetailType == $scope.eDetailType.Html) {
                    $.each($scope.floorAnchors, function (idx, val) {
                        if (val.Operate === "" || val.Operate === null) {
                            $.messager.alert("楼层操作不能为空!");
                            result = false;
                            return false;
                        } else {
                            anchor.push("{0}|{1}|{2}".format(val.Coord, val.Type, val.Operate));
                        }
                    });
                } else if ($scope.fmStaticDetail.DetailType == $scope.eDetailType.ProCode) {
                    $.each($scope.floorProCode, function (idx, val) {
                        if (val.Content === "" || val.Content === null || val.CoordX === "" || val.CoordX === null || val.CoordY === "" || val.CoordY === null || val.FontSize === "" || val.FontSize === null || val.FontColor === "" || val.FontColor === null) {
                            $.messager.alert("楼层操作不能为空!");
                            result = false;
                            return false;
                        } else {
                            anchor.push("{0},{1}|{2}|{3}|{4}".format(val.CoordX, val.CoordY, val.Content, val.FontColor, val.FontSize));
                        }
                    });
                    $scope.fmStaticDetail.Title = $(".panel-body canvas").attr("height") || 0;
                }
                if (result == false) {
                    return false;
                } else {
                    if ($scope.fmStaticDetail.DetailType == $scope.eDetailType.ProCode) {
                        $scope.fmStaticDetail.LucencyAnchor = "1#" + anchor.join("||");//这里通过1#,2#...标识不同的图片楼层
                    } else {
                        $scope.fmStaticDetail.LucencyAnchor = anchor.join("||");
                    }
                }
                $scope.norepeat = false;
                activityService.SaveStaticsDetail($scope.fmStaticDetail, function (result) {
                    $scope.norepeat = true;
                    if (result.data == 1) {
                        $.messager.alert("保存成功!");
                        window.location.href = "actDetail#/Edit/" + $scope.fmStaticDetail.StaticHtmlId;
                    } else if (result.data == 5) {
                        $.messager.alert("名称不能为空!");
                    } else {
                        if (result.msg != null && result.msg != "") $.messager.alert(result.msg);
                        else $.messager.alert("保存失败!");
                    }
                });
            }
        });
    };


    //全角转半角
    $scope.FtoH = function (str) {
        var result = "";
        if (str === null || str === "" || str === undefined) return false;
        for (var i = 0; i < str.length; i++) {
            if (str.charCodeAt(i) == 12288) {
                result += String.fromCharCode(str.charCodeAt(i) - 12256);
                continue;
            }
            if (str.charCodeAt(i) > 65280 && str.charCodeAt(i) < 65375) result += String.fromCharCode(str.charCodeAt(i) - 65248);
            else result += String.fromCharCode(str.charCodeAt(i));
        }
        return result.trim();
    };
});
