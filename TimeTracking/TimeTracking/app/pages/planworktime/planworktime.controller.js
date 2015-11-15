angular.module('timetracking')
.controller('PlanWorktimeCtrl', ['$scope', '$routeParams', 'HolidayService', 'EmployeeService', 'PlanWorktimeService',
    function ($scope, $routeParams, HolidayService, EmployeeService, PlanWorktimeService) {
        // year
        $scope.year = $routeParams.year || new Date().getFullYear();

        // group and plan work day
        $scope.planWorkDays = PlanWorktimeService.query();
        $scope.planWorkDayModel = new PlanWorktimeService();
        $scope.addPlanWorkDay = function () {
            if ($scope.planWorkDayForm.$invalid)
                return;

            $scope.planWorkDayModel.$save(function (planWorkDay) {
                $scope.planWorkDays.push(planWorkDay);
                $scope.planWorkDayModel = new PlanWorktimeService();
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