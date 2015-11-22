angular.module('timetracking')
.controller('HomeCtrl', ['$scope', 'TableService', '$window', '$location',
    function ($scope, TableService, $window, $location) {
        $scope.tables = TableService.query();

        $scope.edit = function (tableId) {
            $location.path('table/' + tableId + '/edit');
        };

        $scope.remove = function (table) {
            if (!$window.confirm('Are you sure?'))
                return;

            table.$delete(function () {
                var index = $scope.tables.indexOf(table);
                $scope.tables.splice(index, 1);
            });
        };
    }]);