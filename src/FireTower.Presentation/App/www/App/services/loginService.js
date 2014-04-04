angular.module('firetower')
    .service('loginService', ['$q', '$http', function ($q, $http) {

        return {
            authenticate: function (email, password) {

                debugger;
                return $http.post("/login", { email: email, password: password });
            }
        };
    }]);