angular.module('firetower')
    .factory('DisasterService', ['$http', function ($http) {
        var factory = {};

        factory.SaveSeverity = function (severity) {
            return $http.post('/votes/severity', severity);
        };

        factory.VoteControlled = function (isControlled) {
            return $http.post('/votes/controlled', isControlled);
        };
        
        return factory;
}]);