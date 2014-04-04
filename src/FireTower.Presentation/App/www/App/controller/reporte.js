angular.module('firetower')
    .controller('ReporteController', ['$scope', '$stateParams', '$ionicLoading', 'data', 'Math', function($scope, $stateParams, $ionicLoading, data, Math) {

        var init = function () {
            $scope.loading = $ionicLoading.show({
                content: 'Cargando datos del incendio...',
                showBackdrop: false
            });
            
            data.getReportById($stateParams.reporteId)
                .success(function(data) {
                    data = data[0];
                    var formattedDate = data.CreatedDate.$date;
                    formattedDate = moment((new Date()).toLocaleDateString()).fromNow();
                    data.CreatedDate.$date = formattedDate;
                    data.SeverityAverage = Math.Average(data.SeverityVotes);
                    $scope.reporte = data;

                    $scope.map = {
                        center: {
                            latitude: data.Location[0],
                            longitude: data.Location[1]
                        },
                        zoom: 12,
                        refresh: true
                    };
                    $scope.loading.hide();
                })
                .error(function(error) {
                    console.log(error);
                });

            $scope.map = {
                center: {
                    latitude: 0,
                    longitude: 1
                },
                zoom: 12,
                refresh: false
            };
        };

        $scope.reporte = null;
        init();        
    }]);