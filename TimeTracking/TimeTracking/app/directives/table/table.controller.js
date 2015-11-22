angular.module('timetracking')
.controller('TableCtrl', ['$scope', 'TableService', 'EmployeeService',
    function ($scope, TableService, EmployeeService) {
        // get employees 
        $scope.employees = EmployeeService.query(function (employees) {

            // mark employees as used if it needs
            if ($scope.table.$promise) {
                $scope.table.$promise.then(function (table) {
                    markEmployeesAsUsed(employees, table);
                });
            }
        });

        // add an employee in the table
        $scope.addEmployee = function () {
            $scope.selectedEmployee.used = true;
            
            var query = {
                year: $scope.table.year,
                month: $scope.table.month,
                employeeId: $scope.selectedEmployee.employeeId
            };

            // calc month for the chosed employee
            TableService.calcMonth(query, function (result) {
                $scope.table.employees.push(result);
            });
        };

        $scope.removeEmployee = function (employee) {
            var index = $scope.table.employees.indexOf(employee);
            $scope.table.employees.splice(index, 1);

            for (var i = 0; i < $scope.employees.length; i++) {
                if ($scope.employees[i].employeeId == employee.employeeId) {
                    $scope.employees[i].used = false;
                    break;
                }
            }
        };

        // recalc fact worktime for the employee
        $scope.changeHours = function (employee) {
            var fact = 0.0;
            for (var i = 0; i < employee.records.length; i++) {
                fact += employee.records[i].hours;
            }

            employee.fact = fact;
        };

        function markEmployeesAsUsed(employees, table) {
            for (var i = 0; i < employees.length; i++) {
                for (var j = 0; j < table.employees.length; j++) {
                    if (employees[i].employeeId == table.employees[j].employeeId) {
                        employees[i].used = true;
                    }
                }
            }
        }
    }]);