angular.module('firetower')
    .controller('NewFireLocationController', ['$scope', '$stateParams', 'data', function ($scope, $stateParams, data) {
        $scope.location = { lat: 15.22, lng: -89.88 };

        $scope.map = {
            center: {
                latitude: $scope.location.lat,
                longitude: $scope.location.lng
            },
            zoom: 12
        };
    }]);