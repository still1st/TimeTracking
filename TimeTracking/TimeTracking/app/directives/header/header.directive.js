angular.module('timetracking')
.directive('ngHeader', function () {
    return {
        restrict: 'E',
        templateUrl: '/app/directives/header/header.html'
    };
});