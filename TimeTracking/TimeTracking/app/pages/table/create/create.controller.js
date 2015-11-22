angular.module('timetracking')
.controller('TableCreateCtrl', ['$scope', 'EmployeeService', 'TableService', 'DateCnst', '$location',
    function ($scope, EmployeeService, TableService, DateCnst, $location) {
        // months
        $scope.months = DateCnst.months;

        // table
        var now = new Date();

        $scope.table = new TableService();
        $scope.table.month = now.getMonth() + 1;
        $scope.table.year = now.getFullYear();
        $scope.table.employees = [];

        // save
        $scope.save = function () {
            if ($scope.table.employees.length == 0) {
                return;
            }

            $scope.table.$save(function (result) {
                $location.path('home');
            });
        };
    }]);