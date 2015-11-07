angular.module('timetracking')
.controller('AppCtrl', ['$scope', function ($scope) {
    function AppModel(){
        this.year = new Date().getFullYear();
    };

    $scope.App = new AppModel();
}]);