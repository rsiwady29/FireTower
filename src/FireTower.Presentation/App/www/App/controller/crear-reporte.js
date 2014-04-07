angular.module('firetower')
    .controller('NewReportController', ['$scope', '$ionicPopup', 'DisasterService', function ($scope, $ionicPopup, DisasterService) {

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
            destinationType = navigator.camera.DestinationType.DATA_URL;
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
                function (imageData) {
                    console.log("got camera success ", imageData);
                    $scope.mypicture = "data:image/jpeg;base64," + imageData;
                },
                function (err) {
                    console.log("got camera error ", err);
                },
                options);
        };
        
        $scope.takePicture();

        $scope.createDisaster = function () {
            DisasterService.CreateDisaster({
                CreatedDate : new Date().valueOf(),
            });
        };
        
        var getCurrentPosition = function (position) {
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
        };

        var positionFailure = function (error) {
            console.log(error.message);
        };

        $scope.location = { latitude: 15.22, longitude: -89.88 };
        $scope.marker = {
            coords: { latitude: 15.22, longitude: -89.88 },
            options: { draggable: true },
            events: {
                dragend: function (marker, eventName, args) {
                    this.coords.latitude = marker.getPosition().lat();
                    this.coords.longitude = marker.getPosition().lng();
                }
            }
        };

        $scope.map = {
            center: $scope.location,
            zoom: 17
        };

        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(getCurrentPosition, positionFailure);
        }
    }]);