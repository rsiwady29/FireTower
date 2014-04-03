angular.module('firetower').controller('MenuController', ['$scope', 'Facebook', '$location', 'userManagement', function ($scope, Facebook, $location, user) {
    
    $scope.logout = function () {
        $scope.$apply(function () {
            user.logoutUser();
        });
    };

}]);