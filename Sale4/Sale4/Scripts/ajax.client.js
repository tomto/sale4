
var AjaxClient = {};

/*
*    说明：异步调用
*    参数：url：        接口地址
*          requestType：   请求类型get、post
*          args：          请求参数，类型json格式{"key1":"value1","key2":"value2"}
*          callback:       回调函数
*/
AjaxClient.async = function (url, requestType, args, callback) {
    if (AjaxClient.noRepeat) {
        AjaxClient.noRepeat = false;
        args = args || {};
        $.ajax({
            url: url,
            headers: {
                'X-Requested-With': 'XMLHttpRequest'
            },
            data: args,
            async: true,
            type: requestType
        }).done(function (r, s, p) {
            if (p.getResponseHeader("seesion") == "timeout") {
                AjaxClient.toLogion();
            } else {
                if (r.IsSuccess) {
                    callback(r.Data);
                } else {
                    $.messager.alert(r.Message || "操作失败!");
                }
            }
            AjaxClient.noRepeat = true;
        }).fail(function (xhr, text, error) {
            console.log(text);
            AjaxClient.noRepeat = true;
        });
    }
};

/*
*    说明：同步调用
*    参数：url：        接口地址
*          requestType：   请求类型get、post
*          args：          请求参数，类型json格式{"key1":"value1","key2":"value2"}
*/
AjaxClient.sync = function (url, requestType, args) {
    args = args || {};
    var json =
        $.ajax({
            url: url,
            data: args,
            async: false,
            type: requestType
        }).responseText;

    return $.parseJSON(json);
};


AjaxClient.toLogion = function() {
    window.location.href = "/Login";
};
AjaxClient.noRepeat = true;