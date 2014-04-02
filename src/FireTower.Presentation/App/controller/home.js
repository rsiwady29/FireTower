angular.module('firetower').controller('HomeController', ['$scope', 'userManagement', function ($scope, user) {
    $scope.user = user.getUser();
}]);