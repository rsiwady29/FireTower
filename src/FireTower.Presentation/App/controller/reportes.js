angular.module('firetower')
    .controller('ReportesController', ['$scope', 'data', function($scope, data) {
        $scope.reportes = data.getAllReports();
    }]);