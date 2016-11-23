






//color控件
//依赖color-picker.js

angular.module("fontColor", []).directive("fontColor", function () {
    return {
        restrict: 'E',
        scope: {
            pmodel: "=",
            fitthecanvas: "&"
        },
        replace: false,
        

    template: '<div default-color="#ff0000" class="input-group focus" >' +
                        '<span class="input-group-btn"><button  title="色号" color-picker class="btn btn-default"><span class="fui-eye"></span></button></span>' +
                        '<input class="form-control" placeholder="色号" ng-model="pmodel.FontColor" ng-style="{\'background-color\': pmodel.FontColor}"/>' +
                        '</div>',
        link: function(scope) {
            scope.$on('colorPicked', function(event, color) {
                scope.pmodel.FontColor = color;
                scope.fitthecanvas();
            });
        }
    };
});