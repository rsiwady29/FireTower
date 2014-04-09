﻿angular.module('firetower')
    .controller('NewReportController', ['$scope', '$ionicPopup', 'DisasterService', 'PictureService', '$location', 'UserService', function($scope, $ionicPopup, DisasterService, PictureService, $location, UserService) {

        $scope.Severities = [];
        $scope.severity = 0;
        var init = function() {
            $scope.takePicture();
            for (var i = 1; i <= 5; i++) {
                $scope.Severities.push({
                    SeverityScore: i,
                    IsSelected: false
                });
            }

            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(getCurrentPosition, positionFailure);
            }
            $scope.foto = PictureService.getDefaultPicture();
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
                    $scope.foto = "data:image/jpeg;base64," + imageData;
                },
                function(err) {
                },
                options);
        };

        $scope.createDisaster = function() {
            if ($scope.severity == 0) {
                showMessage('Severity', '¿Qué tan Severo es el fuego?');
                return;
            }

            UserService.getUser()
                        .success(function (response) {
                            var pubnub = PUBNUB.init({
                                subscribe_key: 'sub-c-e379a784-bff9-11e3-a219-02ee2ddab7fe'
                            });

                            pubnub.subscribe({
                                channel: response.userId,
                                message: function(m) {
                                    alert(m.Id);
                                    $location.path('/app/reportes');
                                }
                            });
                        })
                        .error(function (error) {
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
                    showMessage('Error', "nein: " + error);
                });
        };

        var addImageToDisaster = function(id) {
            DisasterService.SaveImageToDisaster(id, $scope.foto);
        };

        var clearSeveritySelection = function() {
            for (var i = 0; i < 5; i++) {
                $scope.Severities[i].IsSelected = false;
            }
        };

        $scope.changeSeverity = function(severityScore) {
            $scope.severity = severityScore;
            clearSeveritySelection();
            for (var i = 0; i < 5; i++) {
                if ($scope.Severities[i].SeverityScore == severityScore)
                    $scope.Severities[i].IsSelected = true;
            }
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