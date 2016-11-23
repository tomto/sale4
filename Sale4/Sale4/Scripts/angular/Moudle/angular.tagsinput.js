







//tag 控件
//依赖Flat


angular.module("hjtagsinput", []).directive("hjtagsinput", function () {
    return {
        restrict: "EA",
        //require: "^ngModel",
        template: "<div  class='tagsinput-primary'><input placeholder='{{plac}}' ng-model='tagmodel' name='tagsinput' class='tagsinput' /></div>",
        scope: {
            tagmodel: "=",
            plac:"@"
        },
        replace: true,
        link: function (scope, element, attrs) {
            $(".tagsinput").on("change", function () {
                var tag = $(this).val();
                if (tag != "") {
                    scope.tagmodel = $(this).val().replace(/\s+/g,"");     ;    
                }
            });

            scope.$watch(function () {
                $(".tagsinput").tagsinput();
                var newValue = scope.tagmodel;
                return newValue;
            });
        }
    };
});