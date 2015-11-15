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

        $scope.employees = EmployeeService.query();
        $scope.paramModel = new ParamModel($scope.month, $scope.employees[0]);

        $scope.addEmployee = function () {
            var employee = $scope.paramModel.employee;
            var index = $scope.employees.indexOf(employee);
            $scope.employees.splice(index, 1);

            TableService.calcMonth({ year: $scope.year, month: $scope.month.key, employeeId: employee.employeeId }, function (result) {
                var employeeModel = new EmployeeTableModel(employee.employeeId, employee.fullName, result);
                $scope.table.employees.push(employeeModel);
            });
        };

        // table
        function TableModel() {
            this.employees = [];
        };

        function EmployeeTableModel(employeeId, employeeName, records) {
            this.id = employeeId;
            this.name = employeeName;
            this.records = records;
        };

        $scope.table = new TableModel();
    }]);