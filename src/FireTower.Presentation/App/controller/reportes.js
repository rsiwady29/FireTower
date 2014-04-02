angular.module('starter.controllers',[])
    .controller('ReportesController', ['$scope', 'data', function ($scope, data) {
        $scope.reportes = data.obtener_reportes();
}]);