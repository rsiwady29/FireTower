/// <reference path="login.js" />
angular.module('firetower').controller('LoginController', ['$scope', '$timeout', '$location', 'userManagement', 'loginService', function ($scope, $timeout, $location, user, loginService) {
    $scope.logged = false;
    $scope.salutation = false;
    $scope.byebye = false;
    $scope.facebookReady = false;
    
    //if the user is logged in, send them to home
    if (localStorage.getItem("firetowertoken"))
        $location.path("/app/reportes");

    //FB.Event.subscribe('auth.login', function (response) {
    //    $scope.facebookReady = true;
    //    FB.getLoginStatus(function (response2) {
    //        if (response2.status == 'connected') {
    //            $scope.logged = true;
    //        }
    //    });
    //});

    //$scope.login = function () {
    //    FB.login(
    //        function (response) {
    //            if (response.status == 'connected') {
    //                $scope.me();
    //            } else {
    //                $scope.login();
    //            }
    //        });
    //};
    //$scope.me = function () {
    //    FB.api('/me', function (response) {
    //        $scope.$apply(function () {
    //            user.setUser(response);
    //        });

    //    });
    //};

    // BASIC LOGIN
    $scope.data = { };
    $scope.basicLogin = function () {
        var email = $scope.data.email;
        var password = $scope.data.password;
        loginService.authenticate(email, password).success(function (response) {
            var token = response.token;
            localStorage.setItem("firetowertoken", token);
            $location.path("/app/reportes");
        }).error(function () {
            $location.path("/error");
        });
    };
}]);