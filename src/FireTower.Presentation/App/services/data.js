angular.module('firetower')
    .factory('data', [function () {
    var factory = {};

    factory.obtener_reportes = function () {
        return [
            {
                id: 1,
                title: 'Incendio en Comayagua',
                description: 'Lleva aproximadamente 1 hora encendido',
                location: { lat: 15.49, lng: -89.88 },
                photo: "http://placehold.it/60x60",
                rating: 3
            }
        ];
    };
    
    return factory;
}]);