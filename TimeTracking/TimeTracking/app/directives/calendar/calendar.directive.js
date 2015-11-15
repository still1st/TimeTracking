angular.module('timetracking')
.directive('calendar', function () {
    function getDaysInMonth(month, year)
    {
        return new Date(year, month, 0).getDate();
    }

    return {
        restrict: 'E',
        scope: {
            employee: '='
        },
        link: function (scope, element, attrs) {
            var records = scope.employee.records;

            for (var i = 0; i < records.length; i++) {
                var div = angular.element('<div class="day"></div>');
                var header = records[i].isDayoff ? 
                    angular.element('<div class="day-header dayoff"><span>' + records[i].dayNumber + '</span></div>') :
                    angular.element('<div class="day-header"><span>' + records[i].dayNumber + '</span></div>');

                var input = angular.element('<div class="day-body"><input type="text" value="' + records[i].hours + '"/></div>');
                div.append(header);
                div.append(input);

                element.append(div);
            }
        }
    };
});