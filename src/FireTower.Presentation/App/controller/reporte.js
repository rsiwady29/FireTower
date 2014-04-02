angular.module('firetower')
    .controller('ReporteController', ['$scope', '$stateParams', 'data', function($scope, $stateParams, data) {
        $scope.reporte = data.getReportById($stateParams.reporteId);

        $scope.map = {
            center: {
                latitude: $scope.reporte.location.lat,
                longitude: $scope.reporte.location.lng
            },
            zoom: 12
        };
    }]);