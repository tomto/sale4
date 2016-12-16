



var mainApp = angular.module("mainApp", ["ngRoute", "activityService"]);


mainApp.controller("indexCtrl", function ($scope, activityService) {
    $scope.norepeat = true;
    $scope.fmStaticHtml = new fmModel.fmStaticHtml();
    $scope.pageData = {
        pageSize: 20,
        Index: 1,
        pageCount: 0,
        allCount: 0
    };

    $scope.initIndex = function () {
        if ($scope.norepeat) {
            $scope.norepeat = false;
            $scope.pageData.Index = 1;
            $scope.pageData.pageSize = 20;
            activityService.GetStaticsPage($scope.pageData.pageSize , $scope.pageData.Index, function (result) {
                $scope.fmStaticHtml = result.data;
                $scope.pageData.pageCount = result.pageCount;
                $scope.pageData.allCount = result.allCount;
                $scope.norepeat = true;
            });
        };
    };

    $scope.applyIndex = function () {
        if ($scope.norepeat && $scope.pageData.Index < $scope.pageData.pageCount) {
            $scope.norepeat = false;
            $scope.pageData.Index++;
            activityService.GetStaticsPage($scope.pageData.pageSize, $scope.pageData.Index, function (result) {
                $scope.fmStaticHtml = $scope.fmStaticHtml.concat(result.data);
                $scope.norepeat = true;
            });
        };
    };

    $scope.initIndex();

    $scope.del = function(staticHtmlId) {
        $.messager.confirm("Delete", "是否删除!", function() {
            if ($scope.norepeat && staticHtmlId != "") {
                $scope.norepeat = false;
                activityService.DeleteStatics(staticHtmlId, function (result) {
                    $scope.norepeat = true;
                    $scope.initIndex();
                });
            };
        });
    };

    $scope.copy = function (htmlCode) {
        $.messager.confirm("Copy", "是否复制!", function() {
            if ($scope.norepeat && htmlCode != "") {
                $scope.norepeat = false;
                activityService.CopyHtml(htmlCode, function (result) {
                    $scope.norepeat = true;
                    $scope.initIndex();
                });
            };
        });
    };

    $scope.timeText = function (startTime, endTime, expiresState) {
        var result = startTime + "--" + endTime;
        if (expiresState == 0) {
            result = "手动下架";
        }
        return result;
    };

    $scope.timeStyle = function (expiresState) {
        var bgcolor = "";
        switch (expiresState) {
            case 1:
                bgcolor = "success";
                break;
            case 2:
                bgcolor = "info";
                break;
            case 3:
                bgcolor = "warning";
                break;
            default:
                bgcolor = "";
                break;
        }
        return bgcolor;
    };
});

