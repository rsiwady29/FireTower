angular.module('firetower', ['ionic', 'facebook'])

.run(function ($ionicPlatform) {
    $ionicPlatform.ready(function () {
        if (window.StatusBar) {
            StatusBar.styleDefault();
        }
    });
})

.config(['FacebookProvider', '$stateProvider', '$urlRouterProvider', function (FacebookProvider, $stateProvider, $urlRouterProvider) {
    $stateProvider
        .state('app', {
            url: "/app",
            templateUrl: "App/views/menu.html",
            controller: "MenuController"
        })
        .state('app.inicio', {
            url: "/",
            views: {
                'menuContent': {
                    templateUrl: "App/views/login.html",
                    controller: "LoginController"
                }
            }
        })
        .state('app.reportes', {
            url: "/reportes",
            views: {
                'menuContent': {
                    templateUrl: "App/views/reportes.html",
                    controller: 'ReportesController'
                }
            }
        })
        .state('app.reporte', {
            url: "/reporte/:reporteId",
            views: {
                'menuContent': {
                    templateUrl: "App/views/reporte.html"
                }
            }
        })
        .state('otherwise', {
            url: '*path',
            templateUrl: 'App/views/login.html',
            controller: 'LoginController'
        });

    var myAppId = '294203754077185';

    FacebookProvider.init(myAppId);
}]);
