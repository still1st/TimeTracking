angular.module('timetracking')
.controller('EmployeeCtrl', ['$scope', 'EmployeeService', function ($scope, Employee) {
    $scope.employeeModel = new Employee();

    Employee.getPosts(function (posts) {
        $scope.posts = posts;
        $scope.employeeModel.postId = posts[0].key;
    });

    $scope.employees = Employee.query();

    $scope.save = function () {
        if (employeeForm.$invalid)
            return;

        $scope.employeeModel.$save(function (employee) {
            $scope.employees.push(employee);
            $scope.employeeModel = new Employee();
        });
    }
}]);