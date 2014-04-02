angular.module('firetower')
    .factory('data', [function () {
    var factory = {};

    factory.getAllReports = function () {
        return [
            {
                id: 1,
                title: 'Incendio en Comayagua',
                description: 'Lleva aproximadamente 1 hora encendido',
                location: { lat: 15.49, lng: -89.88 },
                photo: "http://placehold.it/350x300",
                rating: 1
            },
            {
                id: 2,
                title: 'Incendio en Villanueva',
                description: 'Atras de la casa de Richard, URGENTE!',
                location: { lat: 144.49, lng: -89.88 },
                photo: "http://placehold.it/350x300",
                rating: 4
            },
            {
                id: 3,
                title: 'Incendio en Sta Ana',
                description: 'Cerca de la municipalidad',
                location: { lat: 1.49, lng: -49.88 },
                photo: "http://placehold.it/350x300",
                rating: 2
            }
        ];
    };

    factory.getReportById = function(id) {
        var reports = this.getAllReports();
        for (var i = 0; i < reports.length; i++) {
            if (reports[i].id == id)
                return reports[i];
        }
        return null;
    };

    return factory;
}]);