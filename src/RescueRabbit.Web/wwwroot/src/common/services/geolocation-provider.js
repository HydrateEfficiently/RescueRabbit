import { Injectable } from './../../utility/injectable';

const GeolocationErrorMessages = {
    'UnsupportedBrowser': 'Browser does not support location services',
    'PermissionDenied': 'You have rejected access to your location',
    'PositionUnavailable': 'Unable to determine your location',
    'Timeout': 'Service timeout has been reached'
};

class GeolocationProvider extends Injectable {
    static get $inject() {
        return ['$q', '$window'];
    }

    constructor(...deps) {
        super(...deps);
    }

    get(options) {
        let deferred = this.$q.defer();
        let navigator = this.$window.navigator;

        if (navigator && navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(
                position => {
                    deferred.resolve(position);
                },
                error => {
                    let errorMessage = 'Unknown error determining location';
                    switch (error.code) {
                        case 1:
                            errorMessage = GeolocationErrorMessages.PermissionDenied;
                            break;
                        case 2:
                            errorMessage = GeolocationErrorMessages.PositionUnavailable;
                            break;
                        case 3:
                            errorMessage = GeolocationErrorMessages.Timeout;
                            break;
                    }
                    deferred.reject(errorMessage);
                },
                options);
        } else {
            deferred.reject(GeolocationErrorMessages.UnsupportedBrowser);
        }
        return deferred.promise;
    }
}

export { GeolocationProvider };