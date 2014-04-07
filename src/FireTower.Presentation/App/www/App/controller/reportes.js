angular.module('firetower')
    .controller('ReportesController', ['$scope', '$ionicLoading', 'data', 'Math', '$ionicPopup', '$http', function ($scope, $ionicLoading, data, Math, $ionicPopup, $http) {
        
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

        $scope.data = {};
        $scope.obj;
        var pictureSource;   
        var destinationType; 
        var url;

        ionic.Platform.ready(function () {
            console.log("ready get camera types");
            if (!navigator.camera) {
                return;
            }
            pictureSource = navigator.camera.PictureSourceType.CAMERA;
            destinationType = navigator.camera.DestinationType.FILE_URI;
        });

        $scope.takePicture = function () {
            console.log("got camera button click");
            var options = {
                quality: 50,
                destinationType: destinationType,
                sourceType: pictureSource,
                encodingType: 0
            };
            if (!navigator.camera) {
                return;
            }
            navigator.camera.getPicture(
                function (imageURI) {
                    console.log("got camera success ", imageURI);
                    $scope.mypicture = imageURI;                    
                },
                function (err) {
                    console.log("got camera error ", err);
                },
                options);
        };
    }]);