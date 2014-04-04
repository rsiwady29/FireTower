angular.module('firetower')
    .controller('ReportesController', ['$scope', '$ionicLoading', 'data', function($scope, $ionicLoading, data) {
        var getAllReports = function () {
            $scope.loading = $ionicLoading.show({
                content: 'Cargando datos de incendios...',
                showBackdrop: false
            });
            
            data.getAllReports()
                .success(function(data) {
                    $scope.reportes = data;
                    for (var i = 0; i < $scope.reportes.length; i++) {
                        var formattedDate = $scope.reportes[i].CreatedDate.$date;
                        formattedDate = moment((new Date()).toLocaleDateString()).fromNow();
                        $scope.reportes[i].CreatedDate.$date = formattedDate;
                        $scope.reportes[i].SeverityAverage = avg($scope.reportes[i].SeverityVotes);
                    }
                    
                    $scope.loading.hide();
                })
                .error(function(error) {

                });
        };
        var avg = function (array) {
            var result = 0;
            
            for (var i = 0; i < array.length; i++) {
                result += array[i];
            }

            return result / array.length;
        };
        
        $scope.reportes = null;
        getAllReports();
    }]);