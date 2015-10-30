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
            .otherwise({
                redirectTo: '/home'
            });
        }]);