angular.module('firetower')
    .controller('NewFireLocationController', ['$scope', '$stateParams', 'data', function ($scope, $stateParams, data) {
        $scope.location = { latitude: 15.22, longitude: -89.88 };
        
        $scope.map = {
            center: $scope.location,
            zoom: 12
        };
        
        function getCurrentPosition(position) {
            $scope.location = {
                latitude: position.coords.latitude,
                longitude: position.coords.longitude
            };
            
            $scope.map = {
                center: $scope.location,
                zoom: 12
            };
        }
        
        function positionFailure(error) {
            console.log(error.message);
        }
        
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(getCurrentPosition, positionFailure);
        }
        //$scope.location = { lat: 15.22, lng: -89.88 };

        //$scope.map = {
        //    center: {
        //        latitude: $scope.location.lat,
        //        longitude: $scope.location.lng
        //    },
        //    zoom: 12
        //};
    }]);