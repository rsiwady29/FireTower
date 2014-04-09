angular.module('firetower')
    .factory('UserService', ['$http', function ($http) {
    var factory = {};

    factory.getUser = function () {
        var token = localStorage.getItem('firetowertoken');       
        return $http.get('/me?token=' + token);
    };

    return factory;
}]);