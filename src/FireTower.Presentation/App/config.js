var app = angular.module('firetower', ['ng', 'ngRoute']);

app.config(function ($routeProvider) {
    $routeProvider.
        when('/', {
            templateUrl: 'App/Views/login.html',
            controller: 'LoginController'
        });
});