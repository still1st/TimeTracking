angular.module('timetracking')
.factory('HolidayService', ['$resource', function ($resource) {
    return $resource('/api/holiday/:id', { id: '@holidayId' }, {
        getHolidays: {
            method: 'GET', url: '/api/holiday/?year=:year', isArray: true
        },
        getDayTypes: {
            method: 'GET', url: '/api/holiday/types', isArray: true
        }
    });
}]);