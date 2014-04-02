angular.module('firetower')
    .controller('ReportesController', ['$scope', 'data', function($scope, data) {

        $scope.reportes = data.getAllReports();
/*        data.getAllReports()
            .success(function (data) {
                $scope.reportes = data;
            })
            .error(function(error) {
                alert(error);
            });*/
    }]);