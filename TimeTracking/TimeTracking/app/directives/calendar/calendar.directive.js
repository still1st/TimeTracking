angular.module('timetracking')
.directive('calendar', function () {
    function getDaysInMonth(month, year)
    {
        return new Date(year, month, 0).getDate();
    }

    return {
        restrict: 'E',
        link: function (scope, element, attrs) {
            var now = new Date();
            var days = getDaysInMonth(now.getMonth(), now.getFullYear());

            for (var i = 1; i <= days; i++) {
                var newElement = angular.element('<li>' + i + '</li>');
                element.append(newElement);
            }
        }
    };
});