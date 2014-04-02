angular.module('firetower').controller('FuegoReportadoController', ['$scope', 'data', function ($scope, data) {
    $scope.reportes = data.obtener_reportes();

    $scope.testClick = function(desc) {
        alert(desc);
    };
    
    $scope.playlists = [
                        { title: 'Reggae', id: 1 },
                        { title: 'Chill', id: 2 },
                        { title: 'Dubstep', id: 3 },
                        { title: 'Indie', id: 4 },
                        { title: 'Rap', id: 5 },
                        { title: 'Cowbell', id: 6 }
                        ];
}]);