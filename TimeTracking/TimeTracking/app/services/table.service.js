angular.module('timetracking')
.factory('TableService', ['$resource', function ($resource) {
    var baseUrl = '/api/table/';

    return $resource(baseUrl + ':id', { id: "@tableId" }, {
        update: {
            method: 'PUT'
        },
        calcMonth: {
            method: 'GET', url: baseUrl + 'calcMonth?year=:year&month=:month&employeeId=:employeeId'
        }
    });
}]);