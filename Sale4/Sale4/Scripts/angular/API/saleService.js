﻿
var saleService = saleService || {};

saleService.initService = function (name) {
    var service = angular.module(name, []);
    return service;
};


saleService.then = function ($http, url, data, call, faid) {
    return $http({
        method: 'POST',
        url: url,
        data: data,
        headers: {
            'X-Requested-With': 'XMLHttpRequest'
        },
        cache: false
    }).success(function(respdata, status, headers, config) {
        if (headers("seesion") == "timeout") {
            toLogion();
        }
        else if (status == 200) {
            call(respdata);
        } else {
            if (typeof faid === "function") {
                faid();
            } else {
                toLogion();
            }
        }
    }).error(function (respdata, status, headers, config) {
        toLogion();
    });
};

function toLogion() {
    window.location.href = "Home";
}


/*
*
*  pc活动服务
*
*/
var activityService = saleService.initService("activityService");
activityService.factory("activityService", ['$http', function ($http) {
    var factory = {};

    factory.GetStatics = function (call, faid) {
        saleService.then($http, "/ActivityApi/GetDetails", "", call, faid);
    };
    
    factory.GetStaticsPage = function (pagesize,index, call, faid) {
        var page = {
            pageSize: pagesize,
            index: index
        };
        saleService.then($http, "/ActivityApi/GetStaticsPage", page, call, faid);
    };

    factory.DeleteStatics = function (id, call, faid) {
        var data = { id: id };
        saleService.then($http, "/ActivityApi/DeleteStatics", data, call, faid);
    };

    factory.DeleteStaticsDetail = function (id, call, faid) {
        var data = { id: id };
        saleService.then($http, "/ActivityApi/DeleteStaticsDetail", data, call, faid);
    };

    factory.GetDetail = function (id, call, faid) {
        var data = { id: id };
        saleService.then($http, "/ActivityApi/GetDetail", data, call, faid);
    };

    factory.GetStaticsDetails = function (id, call, faid) {
        var data = { id: id };
        saleService.then($http, "/ActivityApi/GetStaticsDetails", data, call, faid);
    };

    factory.GetStaticsDetail = function (id, call, faid) {
        var data = { id: id };
        saleService.then($http, "/ActivityApi/GetStaticsDetail", data, call, faid);
    };

    factory.SaveStaticsHtml = function (fmHtml, call, faid) {
        var data = fmHtml;
        saleService.then($http, "/ActivityApi/SaveStaticsHtml", data, call, faid);
    };

    factory.SaveStaticsDetail = function (fmDetail, call, faid) {
        var data = fmDetail;
        saleService.then($http, "/ActivityApi/SaveStaticsDetail", data, call, faid);
    };

    factory.CopyHtml = function (code, call, faid) {
        var data = { code: code };
        saleService.then($http, "/ActivityApi/CopyHtml", data, call, faid);
    };
    

    return factory;
}]);


