angular.module('timetracking')
.directive('timeTrackingTable', function () {
    return {
        restrict: 'E',
        controller: 'TableCtrl',
        templateUrl: 'app/directives/table/table.html',
    };
});