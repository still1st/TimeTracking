angular.module('timetracking')
.factory('EmployeeService', ['$resource', function ($resource) {
    return $resource('/api/employee/:id', { id: '@employeeId' }, {
        getPosts: {
            method: 'GET', url: '/api/employee/posts', isArray: true
        }
    });
}]);