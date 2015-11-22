angular.module('timetracking')
.config(['$httpProvider', function ($httpProvider) {
    $httpProvider.interceptors.push(function ($q, $window) {
        return {
            responseError: function (response) {
                if (response) {
                    var message = 'Error! Status code: ' + response.status + ' Status text: ' + response.statusText;
                    if (response.data && response.data.message)
                        message += '\nMessage: ' + response.data.message;

                    $window.alert(message);
                }

                return $q.reject(response);
            }
        }
    });
}]);