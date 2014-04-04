angular.module('starter.controllers',[])
    .controller('ReportesController', ['$scope', 'data', 'userManagement', '$location', function ($scope, data, user, $location) {
        $scope.user = user.getUser();
        if (!$scope.user || $scope.user == {}) {
            $location.path('/app/');
        }
        $scope.reportes = data.obtener_reportes();
    }]);
