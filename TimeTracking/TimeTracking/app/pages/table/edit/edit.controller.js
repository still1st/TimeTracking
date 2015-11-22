angular.module('timetracking')
.controller('TableEditCtrl', ['$scope', '$routeParams', 'TableService', 'EmployeeService', '$location',
    function ($scope, $routeParams, TableService, EmployeeService, $location) {
        $scope.tableId = $routeParams.id;

        $scope.table = TableService.get({ id: $scope.tableId });

        $scope.save = function () {
            TableService.update({id: $scope.table.tableId}, $scope.table, function () {
                $location.path('home');
            });
        };
    }]);