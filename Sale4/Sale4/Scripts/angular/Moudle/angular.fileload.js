






//上传控件

angular.module("hjupload", []).directive("hjupload", function () {
    return {
        restrict: "EA",
        template: "<div class='input-group focus'><span class='input-group-btn j_filePicker'>" +
            "<button class='btn btn-default ' ><span class='fui-plus-circle'></span></button>" +
            "</span><input type='text' class='form-control j_uploadval' ng-model='filemodel' ></div>",
        
        scope: {
            filemodel: '='
        },
        replace: true,
        link: function (scope, element, attrs, ngmodel) {
            var that = element.find("input");
            $(that).keyup(function () {
                scope.$apply(scope.filechange());
            });

            scope.filechange = function () {
                scope.filemodel = that.val();
                console.log("success");
            };


            scope.afterInit = function() {
                if ($(".j_filePicker").index($(element).find(".j_filePicker")) + 1 == scope.max) {
                    var pickbtn;
                    var uploader = WebUploader.create({
                        swf: '/Content/WebUploader/Uploader.swf',
                        server: 'http://img04.yiguo.com/Handlers/CMSWebUpload.ashx?swfupload=false&UserID=cms&Token=08DCA7DE5A7D445BA29B01BB20E4D9DA',
                        pick: '.j_filePicker',
                        resize: false,
                        auto: true,
                        compress: false,
                        accept: {
                            title: 'Images',
                            extensions: 'gif,jpg,jpeg,bmp,png',
                            mimeTypes: 'image/jpg,image/jpeg,image/png' //呵呵  bug
                        }
                    });
                    uploader.on('uploadSuccess', function(file, response) {
                        console.log(response);
                        console.log(file);
                        if (response.state === "SUCCESS") {
                            if (pickbtn.siblings(".j_uploadval").length > 0) {
                                pickbtn.siblings(".j_uploadval").val(response.url);
                                pickbtn.siblings(".j_uploadval").keyup();
                            }
                            //pickbtn.siblings(".j_uploadval").val(response.url);
                            //pickbtn.siblings(".j_fileList").find("img").attr("src", response.url);
                        } else {
                            $.messager.alert("上传出错!");
                        }
                    });
                    uploader.on('uploadError', function(file) {
                        $.messager.alert("上传出错!");
                    });
                    $(".j_filePicker").unbind("click").click(function(e) {
                        e.stopPropagation();
                        pickbtn = $(this);
                    });
                }
            };
            scope.max = $(".j_filePicker").length;
            scope.afterInit();
        }
    };
});