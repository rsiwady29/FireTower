angular.module('firetower')
    .factory('data', [function () {
    var factory = {};

    factory.obtener_reportes = function () {
        return [
            {
                id : 1,
                description: 'Incendio en Comayagua',
                location: { lat: 15.49, lng: -89.88 },
                photo: "http://placehold.it/50x50",
                rating: 3
            }
        ];
    };
    
    return factory;
}]);