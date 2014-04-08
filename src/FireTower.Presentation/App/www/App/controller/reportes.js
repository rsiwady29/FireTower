angular.module('firetower')
    .controller('ReportesController', ['$scope', '$ionicLoading', 'data', 'Math', '$ionicPopup', '$http', '$location', function ($scope, $ionicLoading, data, Math, $ionicPopup, $http, $location) {
        
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
                        $scope.reportes[i].CreatedDate.$dateformatted = formattedDate;
                        $scope.reportes[i].SeverityAverage = Math.Average($scope.reportes[i].SeverityVotes);
                    }
                    
                    $scope.loading.hide();
                })
                .error(function(error) {
                    $scope.loading.hide();
                    showMessage('Error', 'No hemos podido cargar los reportes. Estas conectado a internet?');
                });
        };

        var showMessage = function (title, message) {
            $ionicPopup.alert({
                title: title,
                content: message
            });
        };

        $scope.isValidEmail = function(email) {
            var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            return re.test(email);
        };
        
        $scope.sendEmail = function (date, locationDescription, latitude, longitude) {
            $ionicPopup.prompt({
                title: 'Enviar Correo',
                subTitle: 'Ingrese un correo para enviar la información del incendio',
                inputType: 'email',
                inputPlaceholder: 'Correo'
            }).then(function (res) {
                if (res) {
                    if ($scope.isValidEmail(res)) {
                        $http.post('/SendDisasterByEmail', { Email: res, CreatedDate: date, LocationDescription: locationDescription, Latitude: latitude, Longitude: longitude }).success(function(response) {
                            $ionicPopup.alert({
                                title: 'Hey!',
                                content: 'El mensaje fue enviado.'
                            }).then(function(res) {

                            });
                        }).error(function(XMLHttpRequest, textStatus, errorThrown) {
                            $ionicPopup.alert({
                                title: 'Oh no!',
                                content: 'El mensaje no puede ser enviado, pruebe mas tarde.'
                            }).then(function(res) {

                            });
                        });
                    } else {
                        $ionicPopup.alert({
                            title: 'Error de correo',
                            content: 'La direccion de correo no es valida.'
                        }).then(function(res) {

                        });
                    }
                }
            });
        };
        
        $scope.reportes = null;
        getAllReports();

        $scope.crearReporte = function () {
            $location.path('/app/crear-reporte');
        };
    }]);