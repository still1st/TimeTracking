angular.module('timetracking')
.controller('TableCtrl', ['$scope', 'EmployeeService', 'TableService', 'DateCnst', '$location',
    function ($scope, EmployeeService, TableService, DateCnst, $location) {
        // year and months
        $scope.months = DateCnst.months;

        // table and table records
        $scope.table = new TableService();
        $scope.table.employees = [];

        // table params
        function ParamModel(employee, year, month) {
            this.employee = employee;

            var now = new Date();
            this.year = year || now.getFullYear();
            this.month = month || $scope.months[now.getMonth()];
        };

        // get all employees
        EmployeeService.query(function (result) {
            $scope.employees = result;
            $scope.paramModel = new ParamModel($scope.employees[0]);
        });

        // add the employee into the table
        $scope.addEmployee = function (employee) {
            // remove employee from list
            var index = $scope.employees.indexOf(employee);
            $scope.employees.splice(index, 1);

            // choose the next employee in the list
            if ($scope.employees.length > 0) {
                $scope.paramModel.employee = $scope.employees[0];
            }

            // calc month for the chosed employee
            var query = {
                year: $scope.paramModel.year,
                month: $scope.paramModel.month.key,
                employeeId: employee.employeeId
            };

            TableService.calcMonth(query, function (result) {
                $scope.table.employees.push(result);
            });
        };

        // recalc fact worktime for the employee
        $scope.changeHours = function (employee) {
            var fact = 0.0;
            for (var i = 0; i < employee.records.length; i++) {
                fact += employee.records[i].hours;
            }

            employee.fact = fact;
        };

        // save the table
        $scope.save = function () {
            if ($scope.table.employees === undefined || $scope.table.employees.length == 0)
                return;

            $scope.table.year = $scope.paramModel.year;
            $scope.table.month = $scope.paramModel.month.key;

            $scope.table.$save(function (result) {
                alert('Табель удачно добавлен');
                $location.path('home');
            });
        };
    }]);