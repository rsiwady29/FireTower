var app = angular.module('firetower', ['ionic']);


app.config(function($stateProvider, $urlRouterProvider) {
    $stateProvider
        .state('Login', {
            url: "/login",
            controller: 'LoginController',
            templateUrl: "App/views/login.html"
        })
        .state('Fuegos Reportados', {
            url: "/fuegos_reportados",
            controller: 'FuegoReportadoController',
            templateUrl: "App/views/fuegos_reportados.html"
        });

    $urlRouterProvider.otherwise('/fuegos_reportados');
});
