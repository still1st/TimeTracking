angular.module('timetracking')
.controller('HomeCtrl', ['$scope', function ($scope) {
    function FormModel() {
        var now = new Date();
        this.month = now.getMonth();
        this.year = now.getFullYear();
    };

    $scope.formModel = new FormModel();

    function EmployeeModel(name) {
        this.name = name;
    };

    $scope.employees = [];
    $scope.addEmployee = function () {
        $scope.employees.push(new EmployeeModel('Employee'));
    };
}]);