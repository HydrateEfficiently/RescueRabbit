import { HttpService } from './services/http-service';
import { SupportService } from './services/support-service';

import { GeolocationProvider } from './services/geolocation-provider';

let commonServices = angular.module('rr.common.services', [])
    .service('RRHttpService', HttpService)
    .service('RRSupportService', SupportService)
    .service('RRGeolocationProvider', GeolocationProvider)
    .name;

export { commonServices };