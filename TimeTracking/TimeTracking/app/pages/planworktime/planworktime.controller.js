angular.module('timetracking')
.controller('PlanWorktimeCtrl', ['$scope', '$routeParams', 'HolidayService', 'EmployeeService', 'PlanWorktimeService', '$window',
    function ($scope, $routeParams, HolidayService, EmployeeService, PlanWorktimeService, $window) {
        // year
        $scope.year = $routeParams.year || new Date().getFullYear();

        // group and plan work day
        $scope.planWorkDays = PlanWorktimeService.query();
        $scope.planWorkDayModel = new PlanWorktimeService();

        $scope.save = function () {
            if ($scope.planWorkDayForm.$invalid)
                return;

            $scope.planWorkDayModel.$save(function (planWorkDay) {
                console.dir(planWorkDay);

                $scope.planWorkDays.push(planWorkDay);
                $scope.planWorkDayModel = new PlanWorktimeService();
            });
        };

        $scope.remove = function (planWorkday) {
            if (!$window.confirm('Are you sure?'))
                return;

            planWorkday.$delete(function (result) {
                if (result) {
                    var index = $scope.planWorkDays.indexOf(planWorkday);
                    $scope.planWorkDays.splice(index, 1);
                }
            });
        };

        EmployeeService.getGroups(function (groups) {
            $scope.groups = groups;
            $scope.planWorkDayModel.groupId = groups[0].key;
        });

        // plan work months
        PlanWorktimeService.getPlanWorkMonths({ year: $scope.year }, function (planWorkMonths) {
            $scope.planGroups = planWorkMonths[0].groups;
            $scope.planWorkMonths = planWorkMonths;
        });
    }]);