angular.module('firetower')
    .controller('NewReportController', ['$scope', '$ionicPopup', 'DisasterService', 'PictureService', '$location', '$ionicLoading', 'UserService', function($scope, $ionicPopup, DisasterService, PictureService, $location, $ionicLoading, UserService) {

        var viewModelId = null;
        var imageUploadedSuccessfully = false;
        var viewModelCreatedSuccessfully = false;
        var userId = null;
        var pubnub = PUBNUB.init({
            subscribe_key: 'sub-c-e379a784-bff9-11e3-a219-02ee2ddab7fe'
        });
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
                function(imageData) {
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
                .success(function(response) {                    
                    userId = response.userId;

                    pubnub.subscribe({
                        channel: response.userId,
                        message: function(model) {
                            if (viewModelCreatedSuccessfully && imageUploadedSuccessfully) return;
                            
                            if (model.ViewModelId !== undefined && model.ViewModelId !== null) {
                                viewModelCreatedSuccessfully = true;
                                viewModelId = model.ViewModelId;
                                showDetails();
                            } else {
                                DisasterService.SaveImageToDisaster(model.Id, { Base64Image: $scope.base64foto })
                                    .success(function () {
                                        imageUploadedSuccessfully = true;
                                        showDetails();
                                    })
                                    .error(function() {
                                        $scope.loading.hide();
                                        showMessage('Error', 'El Reporte fue creado, pero no se ha podido cargar la foto');
                                    });
                            }
                        }
                    });
                })
                .error(function(error) {
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

        var showDetails = function () {
            if (viewModelCreatedSuccessfully && imageUploadedSuccessfully) {
                pubnub.unsubscribe({
                    channel: userId,
                });
                $scope.loading.hide();
                showMessage('Exito!', 'Reporte creado exitosamente!');
                $location.path('/app/reporte/' + viewModelId);
            }
        };

        var getLocationAddress = function(latLng) {
            var geocoder = new google.maps.Geocoder();
            
            geocoder.geocode({ 'latLng': latLng }, function (results, status) {
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

            var lat = $scope.location.latitude;
            var lng = $scope.location.longitude;
            var latlng = new google.maps.LatLng(lat, lng);

            getLocationAddress(latlng);
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

                    getLocationAddress(marker.getPosition());
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