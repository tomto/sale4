




//时间控件
//依赖bootstrap - datetimepicker.js, bootstrap - datetimepicker.zh - CN.js, bootstrap.css
//2016-04-22 15:50   必须时间格式


angular.module("datetimepicker", []).directive("datetimepicker", function () {
    return {
        restrict: "EA",
        template: "<div class='input-group focus date form_datetime'><span class='input-group-addon'><span class='glyphicon glyphicon-th'></span></span>" +
                                    "<input type='text' size='16' class='form-control' readonly ng-model='recipient'>" +
                                    "</div>",
        scope: {
            recipient: '='
        },
        link: function (scope, element, attrs, ngmodel) {
            var now = new Date();
            var time = $(element).val();
            var input = element.find('input');
            
            $(element).on('hide', function () {
                scope.recipient = input.val();
            });

            scope.dateformat = function (fmt, date) { //author: meizz 
                var o = {
                    "M+": date.getMonth() + 1, //月份 
                    "d+": date.getDate(), //日 
                    "h+": date.getHours(), //小时 
                    "m+": date.getMinutes(), //分 
                    "s+": date.getSeconds(), //秒 
                    "q+": Math.floor((date.getMonth() + 3) / 3), //季度 
                    "S": date.getMilliseconds() //毫秒 
                };
                if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (date.getFullYear() + "").substr(4 - RegExp.$1.length));
                for (var k in o)
                    if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
                return fmt;
            };

            scope.init = function() {
                if (time === "" || time === undefined) {
                    time = scope.dateformat("yyyy-mm-dd 00:00:00", now);
                }
                $(".form_datetime").datetimepicker({
                    language: 'zh-CN',
                    format: 'yyyy-mm-dd hh:mm:ss',
                    autoclose: true,
                    showMeridian: true,
                    todayBtn: true,
                    startDate: time,
                    todayHighlight: true,
                });
            };
            scope.init();
        }
    };
});