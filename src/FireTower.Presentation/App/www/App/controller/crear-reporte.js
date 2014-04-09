angular.module('firetower')
    .controller('NewReportController', ['$scope', '$ionicPopup', 'DisasterService', 'PictureService', '$location', '$ionicLoading', 'UserService', function($scope, $ionicPopup, DisasterService, PictureService, $location, $ionicLoading, UserService) {

       
        var init = function() {
            $scope.takePicture();
            
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(getCurrentPosition, positionFailure);
            }
            
            $scope.base64foto = PictureService.getDefaultPictureWithoutDataType();
        };

        $scope.data = {};
        $scope.obj;
        var pictureSource;
        var destinationType;
        var url;

        ionic.Platform.ready(function() {
            if (!navigator.camera) {
                return;
            }
            pictureSource = navigator.camera.PictureSourceType.CAMERA;
            destinationType = navigator.camera.DestinationType.DATA_URL;
        });

        $scope.takePicture = function() {
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
                function (imageData) {
                    $scope.base64foto = imageData;
                },
                function(err) {
                },
                options);
        };

        $scope.createDisaster = function() {            
            $scope.loading = $ionicLoading.show({
                content: 'Guardando reporte...',
                showBackdrop: false
            });

            UserService.getUser()
                        .success(function (response) {
                            var pubnub = PUBNUB.init({
                                subscribe_key: 'sub-c-e379a784-bff9-11e3-a219-02ee2ddab7fe'
                            });

                            pubnub.subscribe({
                                channel: response.userId,
                                message: function(disasterModel) {
                                    DisasterService.SaveImageToDisaster(disasterModel.Id, { Base64Image: $scope.base64foto })
                                        .success(function () {
                                            $scope.loading.hide();
                                            showMessage('Exito!', 'Reporte creado exitosamente!');
                                            $location.path('/app/reporte/' + disasterModel.Id);
                                        })
                                        .error(function () {
                                            $scope.loading.hide();
                                            showMessage('Error', 'El Reporte fue creado, pero no se ha podido cargar la foto');
                                        });
                                    
                                    pubnub.unsubscribe({
                                        channel: response.userId,
                                    });
                                }
                            });
                        })
                        .error(function (error) {
                            $scope.loading.hide();
                            showMessage('Error', 'No hemos podido guardar el reporte. Estas conectado a internet?');
                        });

            DisasterService.CreateDisaster({
                LocationDescription: $scope.LocationDescription,
                Latitude: $scope.location.latitude,
                Longitude: $scope.location.longitude,
                FirstSeverity: $scope.severity,
            })
                .success(function(response) {
                                
                })
                .error(function(error) {
                    showMessage('Error', 'Error creando el reporte.');
                });
        };

        var addImageToDisaster = function(id) {
            DisasterService.SaveImageToDisaster(id, $scope.foto);
        };

        var getCurrentPosition = function(position) {
            $scope.location = {
                latitude: position.coords.latitude,
                longitude: position.coords.longitude
            };

            $scope.marker.coords = {
                latitude: $scope.location.latitude,
                longitude: $scope.location.longitude
            };

            $scope.map = {
                center: $scope.location,
                zoom: 17
            };

            var geocoder = new google.maps.Geocoder();
            var address = '';

            var lat = $scope.location.latitude;
            var lng = $scope.location.longitude;
            var latlng = new google.maps.LatLng(lat, lng);
            geocoder.geocode({ 'latLng': latlng }, function(results, status) {
                if (status == google.maps.GeocoderStatus.OK) {
                    if (results[1]) {
                        $scope.LocationDescription = results[1].formatted_address;
                    } else {
                        alert('No results found');
                    }
                } else {
                    alert('Geocoder failed due to: ' + status);
                }
            });
        };

        var positionFailure = function(error) {
            console.log(error.message);
        };

        $scope.location = { latitude: 15.22, longitude: -89.88 };
        $scope.marker = {
            coords: { latitude: 15.22, longitude: -89.88 },
            options: { draggable: true },
            events: {
                dragend: function(marker, eventName, args) {
                    this.coords.latitude = marker.getPosition().lat();
                    this.coords.longitude = marker.getPosition().lng();
                }
            }
        };

        $scope.map = {
            center: $scope.location,
            zoom: 17
        };

        var showMessage = function(title, message) {
            $ionicPopup.alert({
                title: title,
                content: message
            });
        };

        init();
    }]);