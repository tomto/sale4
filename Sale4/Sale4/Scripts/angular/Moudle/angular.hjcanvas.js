





//cavas 控件

angular.module("hjcanvas", [])
    .directive("hjcanvas", function () {
    return {
        restrict: "AE",
        templateUrl: "/Moudle/PromSlice.html",
        scope: {
            anchormodel: "=",
            imgmodel: "@imgmodel"
        },
        replace: true,
        link: function (scope, element, attrs, ngmodel) {
            var that = $(element).find("canvas"),
                cxt = that[0].getContext("2d"),
                onclicknum = 0, fx, fy, sx, sy, maxtrial = 0;
            scope.toolsState = false;

            scope.$watch("imgmodel", function () {
                scope.fitcanvas();
            });

            scope.initcanvas = function () {
                cxt.fillStyle = cxt.strokeStyle = "#FF0000";
                cxt.font = "30px Verdana";
            };

            scope.fitcanvas = function () {
                scope.fitImg() || scope.fitAnchor();
            };

            scope.fitImg = function () {
                var img = new Image();
                img.src = scope.imgmodel;
                if (maxtrial > 5) {
                    maxtrial = 0;
                    return true;
                } else if (scope.imgmodel === "") {
                    img.src = "../images/image_null.png";
                    that.attr("width", img.width).attr("height", img.height);
                    cxt.drawImage(img, 0, 0);
                    return true;
                }else if ((scope.imgmodel !== "" && scope.imgmodel !== null) && img.width == 0) {
                    setTimeout(function () {
                        scope.fitcanvas(); // 递归扫描
                    }, 1000);
                    maxtrial++;
                    return true;
                } else {
                    that.attr("width", img.width).attr("height", img.height);
                    scope.initcanvas();
                    cxt.drawImage(img, 0, 0);
                }
                maxtrial = 0;
                return false;
            };

            scope.fitAnchor = function () {
                if (scope.anchormodel !== null && scope.anchormodel !== undefined && scope.anchormodel.length > 0) {
                    $.each(scope.anchormodel, function (idx, val) {
                        if (val != "" && val != undefined) {
                            if (val.Coord != "") {
                                var temp = val.Coord.split(","),
                                    x1 = temp[0],
                                    y1 = temp[1],
                                    x2 = temp[2],
                                    y2 = temp[3];
                                //图片
                                cxt.strokeRect(x1, y1, x2 - x1, y2 - y1);
                                //文字
                                cxt.fillText(idx + 1, Number(x1) + 10, Number(y1) + 10);
                            }
                        }
                    });
                }
            };

            scope.delAnchor = function (idx) {
                if (idx >= 0 && idx <= scope.anchormodel.length ) {
                    scope.anchormodel.splice(idx, 1);
                    scope.fitcanvas();
                } 
            };
            
            scope.changePlaceholder = function (x) {
                switch (x.Type) {
                    case "0":
                        x.Placeholder = "链接如:http://www.google.com";
                        break;
                    case "1":
                        x.Placeholder = "商品code";
                        break;
                    case "2":
                        x.Placeholder = "promotionsId,组合商品ID";
                        break;
                    case "3":
                        x.Placeholder = "锚点:#1";
                        break;
                    case "4":
                        x.Placeholder = "优惠券批次号";
                        break;
                }
            };
            
            scope.addAnchor = function (xyz) {
                var floorAnchor = new fmModel.fmAnchor();
                floorAnchor.Coord = xyz;
                floorAnchor.Type = "0";
                floorAnchor.Index = scope.anchormodel.length + 1;
                scope.anchormodel.push(floorAnchor);
            };

            

            //画图
            that.mousemove(function (e) {
                var ele = $(this),
                    curx = parseInt(e.pageX - ele.offset().left),
                    cury = parseInt(e.pageY - ele.offset().top);
                $("#logxyz").text("pageX: " + curx + ", pageY: " + cury);

                if (onclicknum % 2 != 0) {
                    scope.fitcanvas();
                    cxt.strokeRect(fx, fy, curx - fx, cury - fy);
                }
            });

            that.on("click", function (e) {
                var ele = $(this);
                if (onclicknum % 2 == 0) {
                    fx = parseInt(e.pageX - ele.offset().left) || 0;
                    fy = parseInt(e.pageY - ele.offset().top) || 0;
                } else {
                    sx = parseInt(e.pageX - ele.offset().left) || 0;
                    sy = parseInt(e.pageY - ele.offset().top) || 0;
                    cxt.strokeRect(fx, fy, sx - fx, sy - fy);
                    scope.addAnchor("{0},{1},{2},{3}".format(fx, fy, sx, sy));

                    scope.fitcanvas();
                }
                onclicknum++;
            });
        }
    };
});







