angular.module('timetracking')
.controller('HomeCtrl', ['$scope', 'EmployeeService', 'TableService', 'DateCnst',
    function ($scope, EmployeeService, TableService, DateCnst) {
        // year and months
        $scope.months = DateCnst.months;

        var now = new Date();
        $scope.year = now.getFullYear();
        $scope.month = $scope.months[now.getMonth()];

        // table params
        function ParamModel(month, employee) {
            this.month = month;
            this.employee = employee;
        };

        EmployeeService.query(function (result) {
            $scope.employees = result;
            $scope.paramModel = new ParamModel($scope.month, $scope.employees[0]);
        });

        $scope.addEmployee = function (employee) {
            // remove employee from list
            var index = $scope.employees.indexOf(employee);
            $scope.employees.splice(index, 1);

            // choose the next employee in the list
            if($scope.employees.length > 0) {
                $scope.paramModel.employee = $scope.employees[0];
            }

            // calc month for the chosed employee
            var query = {
                year: $scope.year,
                month: $scope.month.key,
                employeeId: employee.employeeId
            };

            TableService.calcMonth(query, function (result) {
                var employeeModel = new EmployeeTableModel(employee.employeeId, employee.fullName, result.plan, result.records);
                $scope.table.employees.push(employeeModel);
            });
        };

        $scope.changeHours = function (employee) {
            var fact = 0.0;
            for (var i = 0; i < employee.records.length; i++) {
                fact += employee.records[i].hours;
            }

            employee.fact = fact;
        };

        // table
        function TableModel() {
            this.employees = [];
        };

        function EmployeeTableModel(employeeId, employeeName, plan, records) {
            this.id = employeeId;
            this.name = employeeName;
            this.plan = plan;
            this.fact = plan;
            this.records = records;
        };

        $scope.table = new TableModel();
    }]);