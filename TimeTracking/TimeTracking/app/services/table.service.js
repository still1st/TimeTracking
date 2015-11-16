angular.module('timetracking')
.factory('TableService', ['$resource', function ($resource) {
    var baseUrl = '/api/table/';

    return $resource(baseUrl, {}, {
        calcMonth: {
            method: 'GET', url: baseUrl + 'calcMonth?year=:year&month=:month&employeeId=:employeeId'
        }
    });
}]);