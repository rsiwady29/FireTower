var app = angular.module('firetower', ['ng', 'ngRoute']);

app.config(function ($routeProvider) {
    $routeProvider.
        when('/', {
            templateUrl: 'App/Views/index.html',
            controller: ''
        })
        otherwise({
            templateUrl: ''
        });
});