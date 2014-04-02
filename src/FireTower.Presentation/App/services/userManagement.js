angular.module('firetower').factory('userManagement', function userManagementFactory() {
    var user;
    return {
        setUser: function(facebookUser) {
            user = facebookUser;
        },
        getUser: function() {
            return user;
        }
    };
});