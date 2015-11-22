angular.module('timetracking')
.controller('EmployeeCtrl', ['$scope', 'EmployeeService', '$window',
    function ($scope, Employee, $window) {
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

        $scope.remove = function (employee) {
            if (!$window.confirm('Are you ready?'))
                return;

            console.dir(employee);

            employee.$delete(function () {
                var index = $scope.employees.indexOf(employee);
                $scope.employees.splice(index, 1);
            });
        };
    }]);