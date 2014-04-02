<<<<<<< HEAD
﻿angular.module('firetower', ['ionic'])
=======
﻿angular.module('firetower', ['ionic', 'google-maps'])
>>>>>>> 5adf70b17a0048a74e5d76aafc1b3d1d78d5ab3c

.run(function ($ionicPlatform) {
    $ionicPlatform.ready(function () {
        if (window.StatusBar) {
            StatusBar.styleDefault();
        }
    });
})

.config(function ($stateProvider, $urlRouterProvider) {
    $stateProvider
        .state('app', {
            url: "/app",
            templateUrl: "App/views/menu.html"
        })
        .state('app.inicio', {
            url: "/inicio",
            views: {
                'menuContent': {
                    templateUrl: "App/views/inicio.html"
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
                    templateUrl: "App/views/reporte.html",
                    controller: 'ReporteController'
                }
            }
        });
     
    $urlRouterProvider.otherwise('/app/inicio');
});