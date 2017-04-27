﻿
var saleService = saleService || {};

saleService.noRepeat = true;

saleService.initService = function (name) {
    var service = angular.module(name, []);
    return service;
};


saleService.then = function ($http, url, type, data, call, faid) {
    if (type === "norepeat") {
        if (saleService.noRepeat) {
            saleService.noRepeat = false;
            return $http({
                method: 'POST',
                url: url,
                data: data,
                headers: {
                    'X-Requested-With': 'XMLHttpRequest'
                },
                cache: false
            }).success(function (respdata, status, headers, config) {
                saleService.noRepeat = true;
                if (headers("seesion") == "timeout") {
                    AjaxClient.toLogion();
                }
                else if (status == 200) {
                    if (respdata.IsSuccess) call(respdata.Data);
                    else $.messager.alert(respdata.Message || "操作失败!");
                } else {
                    if (typeof faid === "function") {
                        faid();
                    } else {
                        AjaxClient.toLogion();
                    }
                }
            }).error(function (respdata, status, headers, config) {
                saleService.noRepeat = true;
                console.log(respdata);
            });
        }
    } else {
        return $http({
            method: 'POST',
            url: url,
            data: data,
            headers: {
                'X-Requested-With': 'XMLHttpRequest'
            },
            cache: false
        }).success(function (respdata, status, headers, config) {
            if (headers("seesion") == "timeout") {
                AjaxClient.toLogion();
            }
            else if (status == 200) {
                if (respdata.IsSuccess) call(respdata.Data);
                else $.messager.alert(respdata.Message || "操作失败!");
            } else {
                if (typeof faid === "function") {
                    faid();
                } else {
                    AjaxClient.toLogion();
                }
            }
        }).error(function (respdata, status, headers, config) {
            console.log(respdata);
        });
    }
};


/*
*
*  pc活动服务
*
*/
var activityService = saleService.initService("activityService");
activityService.factory("activityService", ['$http', function ($http) {
    var factory = {};

    factory.GetStatics = function (call, faid) {
        saleService.then($http, $q, "/ActivityApi/GetDetails","", "", call, faid);
    };
    
    factory.GetStaticsPage = function (search,pagesize, index, call, faid) {
        var page = {
            pageSize: pagesize,
            index: index,
            searchKey: search
        };
        saleService.then($http, "/ActivityApi/GetStaticsPage", "", page, call, faid);
    };

    factory.DeleteStatics = function (id, call, faid) {
        var data = { id: id };
        saleService.then($http, "/ActivityApi/DeleteStatics", "norepeat", data, call, faid);
    };

    factory.DeleteStaticsDetail = function (id, call, faid) {
        var data = { id: id };
        saleService.then($http, "/ActivityApi/DeleteStaticsDetail", "norepeat", data, call, faid);
    };

    factory.GetDetail = function (id, call, faid) {
        var data = { id: id };
        saleService.then($http, "/ActivityApi/GetDetail", "", data, call, faid);
    };

    factory.GetStaticsDetails = function (id, call, faid) {
        var data = { id: id };
        saleService.then($http, "/ActivityApi/GetStaticsDetails", "", data, call, faid);
    };

    factory.GetStaticsDetail = function (id, call, faid) {
        var data = { id: id };
        saleService.then($http, "/ActivityApi/GetStaticsDetail", "", data, call, faid);
    };

    factory.SaveStaticsHtml = function (fmHtml, call, faid) {
        var data = fmHtml;
        saleService.then($http, "/ActivityApi/SaveStaticsHtml", "norepeat", data, call, faid);
    };

    factory.SaveStaticsDetail = function (fmDetail, call, faid) {
        var data = fmDetail;
        saleService.then($http, "/ActivityApi/SaveStaticsDetail", "norepeat", data, call, faid);
    };

    factory.CopyHtml = function (code, call, faid) {
        var data = { code: code };
        saleService.then($http, "/ActivityApi/CopyHtml", "norepeat", data, call, faid);
    };
    

    return factory;
}]);


