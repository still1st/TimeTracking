angular.module('timetracking')
    .config(['$routeProvider', '$locationProvider',
        function ($routeProvider, $locationProvider) {
            $locationProvider.html5Mode(true);

            function Route(templateUrl, controller) {
                return {
                    templateUrl: '/app/pages/' + templateUrl,
                    controller: controller,
                    caseInsensitiveMatch: true
                }
            };

            $routeProvider
            .when('/home', new Route('home/home.html', 'HomeCtrl'))
            .when('/table', new Route('table/create/create.html', 'TableCreateCtrl'))
            .when('/table/:id/edit', new Route('table/edit/edit.html', 'TableEditCtrl'))
            .when('/employee', new Route('employee/employee.html', 'EmployeeCtrl'))
            .when('/planworktime/:year', new Route('planworktime/planworktime.html', 'PlanWorktimeCtrl'))
            .when('/holidays/:year', new Route('holidays/holidays.html', 'HolidaysCtrl'))
            .otherwise({
                redirectTo: '/home'
            });
        }]);