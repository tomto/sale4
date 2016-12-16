








//cavas 控件

angular.module("textcanvas", [])
    .directive("textcanvas", function () {
        return {
            restrict: "AE",
            templateUrl: "/Moudle/ProCode.html",
            scope: {
                procodemodel: "=",
                imgmodel: "@imgmodel"
            },
            replace: true,
            link: function (scope, element, attrs, ngmodel) {
                var that = $(element).find("canvas"),
                    cxt = that[0].getContext("2d"),
                    isMove = false,moveIdx = -1, maxtrial = 0,sx,sy;
                scope.toolsState = false;

                //scope.$watch("procodemodel", function () {
                //    scope.fitcanvas();
                //});
                scope.$watch("imgmodel", function () {
                    scope.fitcanvas();
                });

                //scope.$watch("procodemodel", function () {
                //    scope.fitcanvas();
                //});


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
                    } else if ((scope.imgmodel !== "" && scope.imgmodel !== null) && img.width == 0) {
                        setTimeout(function () {
                            scope.fitcanvas(); // 递归扫描
                        }, 1000);
                        maxtrial++;
                        return true;
                    } else {
                        that.attr("width", img.width).attr("height", img.height);
                        cxt.drawImage(img, 0, 0);
                    }
                    maxtrial = 0;
                    return false;
                };

                scope.fitAnchor = function () {
                    if (scope.procodemodel !== null && scope.procodemodel !== undefined && scope.procodemodel.length > 0) {
                        $.each(scope.procodemodel, function (idx, val) {
                            if (val != "" && val != undefined) {
                                if (val.Coord != "") {
                                    //文字
                                    cxt.fillStyle = val.FontColor;
                                    cxt.font = "{0}px Arial".format(val.FontSize);
                                    val.FontWidth = cxt.measureText(val.Content).width;
                                    cxt.fillText(val.Content, Number(val.CoordX), Number(val.CoordY));
                                }
                            }
                        });
                    }
                };

                scope.delProCode = function (idx) {
                    if (idx >= 0 && idx <= scope.procodemodel.length) {
                        scope.procodemodel.splice(idx, 1);
                        scope.fitcanvas();
                    }
                };

                scope.addProCode = function (x,y) {
                    var floorProCode = new fmModel.fmProCode();
                    floorProCode.CoordX = x || 0;
                    floorProCode.CoordY = y || 40;
                    floorProCode.Coord = "0,40";
                    floorProCode.Content = "输入要显示的文字";
                    floorProCode.FontColor = "#f00";
                    floorProCode.FontSize = "40";
                    floorProCode.Index = scope.procodemodel.length + 1;
                    scope.procodemodel.push(floorProCode);
                    scope.fitcanvas();
                };

                scope.addCoordsX = function (idx) {
                    scope.procodemodel[idx].CoordX++;
                    scope.fitcanvas();
                };

                scope.minusCoordsX = function (idx) {
                    scope.procodemodel[idx].CoordX--;
                    scope.fitcanvas();
                };

                scope.addCoordsY = function (idx) {
                    scope.procodemodel[idx].CoordY++;
                    scope.fitcanvas();
                };

                scope.minusCoordsY = function (idx) {
                    scope.procodemodel[idx].CoordY--;
                    scope.fitcanvas();
                };

                scope.addFontSize = function (idx) {
                    scope.procodemodel[idx].FontSize++;
                    scope.fitcanvas();
                };

                scope.minusFontSize = function (idx) {
                    scope.procodemodel[idx].FontSize--;
                    scope.fitcanvas();
                };

                scope.clickOnMyBody = function(x,y) {
                    $.each(scope.procodemodel,function(idx, val) {
                        if (x > val.CoordX && x < (val.CoordX + val.FontWidth) && y < val.CoordY && y > (val.CoordY - val.FontSize <= 0 ? 0 : val.CoordY - val.FontSize)) {
                            moveIdx = idx;
                            return false;
                        }
                    });
                    return moveIdx > -1;
                };

                //画图
                that.mousemove(function (e) {
                    var ele = $(this),
                        curx = parseInt(e.pageX - ele.offset().left),
                        cury = parseInt(e.pageY - ele.offset().top);
                    $("#logxyz").text("pageX: " + curx + ", pageY: " + cury);

                    if (isMove && scope.procodemodel[moveIdx]) {
                        scope.procodemodel[moveIdx].CoordX = curx;
                        scope.procodemodel[moveIdx].CoordY = cury;
                        scope.fitcanvas();
                    }
                });

                that.mousedown(function (e) {
                    isMove = true;
                    var ele = $(this);
                    sx = parseInt(e.pageX - ele.offset().left) || 0;
                    sy = parseInt(e.pageY - ele.offset().top) || 0;
                    if (scope.clickOnMyBody(sx, sy)) {
                        isMove = true;
                    } else {
                        scope.addProCode(sx, sy);
                    }
                });

                that.mouseup(function (e) {
                    isMove = false;
                    moveIdx = -1;
                });

                //that.on("click", function (e) {
                //    var ele = $(this),
                //        sx = parseInt(e.pageX - ele.offset().left) || 0,
                //        sy = parseInt(e.pageY - ele.offset().top) || 0;
                //    console.log(111)
                //    if (scope.clickOnMyBody(sx, sy)) {
                //        isMove = true;
                //        console.log(222)
                //    } else {
                //        console.log(333)
                //        scope.addProCode(sx, sy);
                //    }
                //});

            }
        };
    });







