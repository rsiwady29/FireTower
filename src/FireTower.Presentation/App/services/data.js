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
                photo: "http://placehold.it/50x50",
                rating: 1
            },
            {
                id: 1,
                title: 'Incendio en Villanueva',
                description: 'Atras de la casa de Richard, URGENTE!',
                location: { lat: 144.49, lng: -89.88 },
                photo: "http://placehold.it/50x50",
                rating: 4
            },
            {
                id: 1,
                title: 'Incendio en Sta Ana',
                description: 'Cerca de la municipalidad',
                location: { lat: 1.49, lng: -49.88 },
                photo: "http://placehold.it/50x50",
                rating: 2
            }
        ];
    };
    
    return factory;
}]);