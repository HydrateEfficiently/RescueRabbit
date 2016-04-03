import { GeolocationProvider } from './services/geolocation-provider';

let commonServices = angular.module('rr.common.services', [])
    .service('RRGeolocationProvider', GeolocationProvider)
    .name;

export { commonServices };