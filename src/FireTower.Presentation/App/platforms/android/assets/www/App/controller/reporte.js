angular.module('firetower')
    .controller('ReporteController', ['$scope', '$stateParams', 'data', function($scope, $stateParams, data) {
        $scope.reporte = data.getReportById($stateParams.reporteId);

        $scope.map = {
            center: {
                latitude: 0.4,//$scope.reporte.Location[0],
                longitude: 4.4// $scope.reporte.Location[1]
            },
            zoom: 12
        };
    }]);