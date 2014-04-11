﻿angular.module('firetower').factory('userManagement', ['$http', '$location', function userManagementFactory($http, $location) {
    var user;
    return {
        setUser: function(facebookUser) {
            var loginSuccess = function(responselogin) {
                user = facebookUser;
                var token = responselogin.token;
                localStorage.setItem("firetowertoken", token);
                $location.path('/app/reportes');
            };

            $http.post('/login/facebook', { FacebookId: parseInt(facebookUser.id) })
                .success(loginSuccess)
                .error(function(xhr, statusCode) {
                    if (statusCode == "401") {
                        $http.post('/user/facebook', {
                            FirstName: facebookUser.first_name,
                            LastName: facebookUser.last_name,
                            Name: facebookUser.name,
                            FacebookId: facebookUser.id,
                            Locale: facebookUser.locale,
                            Username: facebookUser.username,
                            Verified: facebookUser.verified
                        }).success(function() {
                            $http.post('/login/facebook', { FacebookId: parseInt(facebookUser.id) })
                                .success(loginSuccess);
                        });
                    }
                });

        },
        getUser: function() {
            return user;
        },
        logoutUser: function () {            
            if (user != undefined) {
                $http.post('/logout', { FacebookId: parseInt(user.id) });                
            }
            
            var token = 'firetowertoken';
            if (token in localStorage) localStorage.removeItem(token);
            $location.path('/app/');
        }
    };
}]);