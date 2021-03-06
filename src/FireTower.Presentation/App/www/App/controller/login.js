﻿/// <reference path="login.js" />
angular.module('firetower').controller('LoginController', ['$scope', '$timeout', '$location', 'userManagement', 'loginService', function ($scope, $timeout, $location, user, loginService) {
    $scope.logged = false;
    $scope.salutation = false;
    $scope.byebye = false;
    $scope.facebookReady = false;
    
    //if the user is logged in, send them to home
    if (localStorage.getItem("firetowertoken"))
        $location.path("/app/reportes");
    
    $scope.login = function () {
        OAuth.popup('facebook', function (err, result) {
            result.get('/me').done(function (data) {
                user.setUser(data);
            });
        });
    };

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