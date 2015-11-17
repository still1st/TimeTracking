angular.module('timetracking')
.controller('HomeCtrl', ['$scope', 'TableService',
    function ($scope, TableService) {
        $scope.tables = TableService.query();
    }]);