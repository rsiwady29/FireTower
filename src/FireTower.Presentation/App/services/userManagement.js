angular.module('firetower').factory('userManagement', ['$http', '$location', function userManagementFactory($http, $location) {
    var user;
    return {
        setUser: function (facebookUser) {
            $http.post('/login', { FacebookId: parseInt(facebookUser.id) }).success(function (response) {
                user = facebookUser;
                $location.path('/app/reportes');
            }).error(function (XMLHttpRequest, textStatus, errorThrown) {
                if (textStatus == "401") {
                    $http.post('/user', { FirstName: facebookUser.first_name,
                        LastName: facebookUser.last_name,
                        Name: facebookUser.name,
                        FacebookId: facebookUser.id,
                        Locale: facebookUser.locale,
                        Username: facebookUser.username,
                        Verified: facebookUser.verified
                    }).success(function (response) {
                        $http.post('/login', { FacebookId: parseInt(facebookUser.id) }).success(function(response) {
                            user = facebookUser;
                            $location.path('/app/reportes');
                        });
                    });
                } 
            });
            
        },
        getUser: function() {
            return user;
        },
        logoutUser: function () {
            $http.post('/logout', { FacebookId: parseInt(user.id) }).success(function (response) {
                user = {};
                FB.api("/me/permissions", "delete", function (fResponse) { $location.path('/app/'); });
                
            }).error(function (XMLHttpRequest, textStatus, errorThrown) {
            });
            
        }
    };
}]);