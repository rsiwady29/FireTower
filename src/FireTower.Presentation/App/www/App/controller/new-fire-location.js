angular.module('firetower')
    .controller('NewFireLocationController', ['$scope', function($scope) {

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
                zoom: 12
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
            zoom: 12
        };

        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(getCurrentPosition, positionFailure);
        }
    }]);