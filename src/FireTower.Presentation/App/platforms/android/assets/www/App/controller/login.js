angular.module('firetower').controller('LoginController', ['$scope', '$timeout', 'Facebook', '$location', 'userManagement', function ($scope, $timeout, Facebook, $location, user) {
    $scope.logged = false;
    $scope.salutation = false;
    $scope.byebye = false;
    $scope.$watch(function() {
        return Facebook.isReady();
    },
        function(newVal) {
            if (newVal) {
                $scope.facebookReady = true;
            }
        });
    
    $scope.IntentLogin = function () {
        
        Facebook.getLoginStatus(function (response) {
            if (response.status == 'connected') {
                $scope.logged = true;
                $scope.me();
            }
            else
                $scope.login();
        });
    };
    $scope.login = function () {
        Facebook.login(function (response) {
            if (response.status == 'connected') {
                $scope.logged = true;
                $scope.me();
            }

        });
    };
    $scope.me = function () {
        Facebook.api('/me', function (response) {
            /**
             * Using $scope.$apply since this happens outside angular framework.
             */
            $scope.$apply(function () {
                user.setUser(response);
            });

        });
    };
    $scope.logout = function() {
        Facebook.logout(function() {
            $scope.$apply(function() {
                user.setUser({});
                $scope.logged = false;
            });
        });
    };
    
    $scope.$on('Facebook:statusChange', function (ev, data) {
        console.log('Status: ', data);
        if (data.status == 'connected') {
            $scope.$apply(function () {
                $scope.salutation = true;
                $scope.byebye = false;
                //insert into database
                
                //redirect to home
                $location.path("/home");
            });
        } else {
            $scope.$apply(function () {
                $scope.salutation = false;
                $scope.byebye = true;

                // Dismiss byebye message after two seconds
                $timeout(function() {
                    $scope.byebye = false;
                }, 2000);
            });
            //redirect to login
            $location.path("/");
        }


    });

}]);