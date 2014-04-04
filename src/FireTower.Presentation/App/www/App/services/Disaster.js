angular.module('firetower')
    .factory('DisasterService', ['$http', function ($http) {
        var factory = {};

        factory.SaveSeverity = function (severity) {
            return $http.post('/severity', severity);
        };

        return factory;
}]);