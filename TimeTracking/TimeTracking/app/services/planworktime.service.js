angular.module('timetracking')
.factory('PlanWorktimeService', ['$resource', function ($resource) {

    return $resource('/api/planworktime/:id', { id: '@planWorkDayId' }, {
        getPlanWorkMonths: {
            method: 'GET', url: '/api/planworktime/calc?year=:year', isArray: true
        }
    });
}]);