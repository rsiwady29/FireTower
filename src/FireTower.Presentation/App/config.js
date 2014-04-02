var app = angular.module('firetower', ['ionic']);


app.config(function ($stateProvider, $urlRouterProvider) {
    $stateProvider
        .state('Login', {
            url: "/login",
            controller: 'LoginController',
            templateUrl: "App/views/login.html"
        })
        .state('Fuegos Reportados', {
            url: "/fuegos_reportados",
            controller: 'FuegoReportadoController',
            templateUrl: "App/views/fuegos_reportados.html"
        });

    $urlRouterProvider.otherwise('/fuegos_reportados');
})
.controller('MainCtrl', function ($scope, $ionicSideMenuDelegate) {
    $scope.attendees = [
      { firstname: 'Nicolas', lastname: 'Cage' },
      { firstname: 'Jean-Claude', lastname: 'Van Damme' },
      { firstname: 'Keanu', lastname: 'Reeves' },
      { firstname: 'Steven', lastname: 'Seagal' }
    ];

    $scope.toggleLeft = function () {
        $ionicSideMenuDelegate.toggleLeft();
    };
})

.controller('CheckinCtrl', function ($scope) {
    $scope.showForm = true;

    $scope.shirtSizes = [
      { text: 'Large', value: 'L' },
      { text: 'Medium', value: 'M' },
      { text: 'Small', value: 'S' }
    ];

    $scope.attendee = {};
    $scope.submit = function () {
        if (!$scope.attendee.firstname) {
            alert('Info required');
            return;
        }
        $scope.showForm = false;
        $scope.attendees.push($scope.attendee);
    };

})

.controller('AttendeesCtrl', function ($scope) {

    $scope.activity = [];
    $scope.arrivedChange = function (attendee) {
        var msg = attendee.firstname + ' ' + attendee.lastname;
        msg += (!attendee.arrived ? ' has arrived, ' : ' just left, ');
        msg += new Date().getMilliseconds();
        $scope.activity.push(msg);
        if ($scope.activity.length > 3) {
            $scope.activity.splice(0, 1);
        }
    };

});
