angular.module('firetower').factory('userManagement', ['$http', '$location', function userManagementFactory($http, $location) {
    var user;
    return {
        setUser: function (facebookUser) {
            $http.post('/login/facebook', { FacebookId: parseInt(facebookUser.id) }).success(function (response) {
                user = facebookUser;
                var token = response.token;
                localStorage.setItem("firetowertoken", token);
                $location.path('/app/reportes');
            }).error(function (XMLHttpRequest, textStatus, errorThrown) {
                if (textStatus == "401") {
                    $http.post('/user/facebook', {
                        FirstName: facebookUser.first_name,
                        LastName: facebookUser.last_name,
                        Name: facebookUser.name,
                        FacebookId: facebookUser.id,
                        Locale: facebookUser.locale,
                        Username: facebookUser.username,
                        Verified: facebookUser.verified
                    }).success(function () {
                        $http.post('/login/facebook', { FacebookId: parseInt(facebookUser.id) }).success(function (responselogin) {
                            user = facebookUser;
                            var token = responselogin.token;
                            localStorage.setItem("firetowertoken", token);
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
            if (user != undefined) {
                $http.post('/logout', { FacebookId: parseInt(user.id) }).success(function (response) {
                    var item = 'firetowertoken';
                    if (item in localStorage) localStorage.removeItem(item);
                    $location.path('/app/');
                }).error(function (XMLHttpRequest, textStatus, errorThrown) {
                });
            } else {
                var item = 'firetowertoken';
                if (item in localStorage) localStorage.removeItem(item);
                $location.path('/app/');
            }
            
        }
    };
}]);