angular.module('timetracking')
.controller('HolidaysCtrl', ['$scope', '$routeParams', 'HolidayService',
    function ($scope, $routeParams, HolidayService) {
        $scope.year = $routeParams.year || new Date().getFullYear();

        $scope.holidayModel = new HolidayService();

        $scope.addHoliday = function () {
            if ($scope.holidayForm.$invalid)
                return;

            $scope.holidayModel.$save(function (day) {
                $scope.holidays.push(day);
                $scope.holidayModel = new HolidayService();
            });
        };

        HolidayService.getHolidays({ year: $scope.year }, function (holidays) {
            $scope.holidays = holidays;
        });

        HolidayService.getDayTypes(function (types) {
            $scope.dayTypes = types;
            $scope.holidayModel.typeId = types[0].key;
        });
    }]);