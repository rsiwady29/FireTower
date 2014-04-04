angular.module('firetower')
    .controller('ReportesController', ['$scope', '$ionicLoading', 'data', 'Math', '$ionicPopup', '$http', function ($scope, $ionicLoading, data, Math, $ionicPopup, $http) {
        
        var getAllReports = function () {
            $scope.loading = $ionicLoading.show({
                content: 'Cargando datos de incendios...',
                showBackdrop: false
            });
            
            data.getAllReports()
                .success(function(data) {
                    $scope.reportes = data;
                    for (var i = 0; i < $scope.reportes.length; i++) {
                        var formattedDate = $scope.reportes[i].CreatedDate.$date;
                        formattedDate = moment((new Date()).toLocaleDateString()).fromNow();
                        $scope.reportes[i].CreatedDate.$dateformatted = formattedDate;
                        $scope.reportes[i].SeverityAverage = Math.Average($scope.reportes[i].SeverityVotes);
                    }
                    
                    $scope.loading.hide();
                })
                .error(function(error) {

                });
        };

        $scope.isValidEmail = function(email) {
            var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            return re.test(email);
        };
        
        $scope.sendEmail = function (date, locationDescription, latitude, longitude) {
            $ionicPopup.prompt({
                title: 'Send Email',
                subTitle: 'Enter email to send disaster information',
                inputType: 'email',
                inputPlaceholder: 'Email Address'
            }).then(function (res) {
                if (res) {
                    if ($scope.isValidEmail(res)) {
                        $http.post('/SendDisasterByEmail', { Email: res, CreatedDate: date, LocationDescription: locationDescription, Latitude: latitude, Longitude: longitude }).success(function(response) {
                            $ionicPopup.alert({
                                title: 'Hey!',
                                content: 'Your message was successfully send.'
                            }).then(function(res) {

                            });
                        }).error(function(XMLHttpRequest, textStatus, errorThrown) {
                            $ionicPopup.alert({
                                title: 'Oh no!',
                                content: 'Your message could not be sent, try again later.'
                            }).then(function(res) {

                            });
                        });
                    } else {
                        $ionicPopup.alert({
                            title: 'Email error',
                            content: 'The email address you entered is invalid.'
                        }).then(function(res) {

                        });
                    }
                }
            });
        };
        
        $scope.reportes = null;
        getAllReports();
        
        $scope.takePicture = function () {
            navigator.camera.getPicture(onPhotoDataSuccess, onFail, {
                quality: 50,
                destinationType: destinationType.DATA_URL
            });

            function onPhotoDataSuccess(imageData) {
                alert(imageData);
            }
            
            function onFail(message) {
                alert('Failed because: ' + message);
            }
        };
    }]);