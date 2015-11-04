angular.module('timetracking')
.directive('ngFooter', function () {
    return {
        restrict: 'E',
        templateUrl: '/app/directives/footer/footer.html'
    };
});