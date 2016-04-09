import { UserSearchService } from './services/user-search-service';
import { HttpService } from './services/http-service';
import { UrlService } from './services/url-service';
import { SessionService } from './services/session-service';
import { LoggingService } from './services/logging-service';

import { GeolocationProvider } from './services/geolocation-provider';

let commonServices = angular.module('rr.common.services', [])
    .service('RRUserSearchService', UserSearchService)
    .service('RRHttpService', HttpService)
    .service('RRUrlService', UrlService)
    .service('RRSessionService', SessionService)
    .service('RRLoggingService', LoggingService)
    .service('RRGeolocationProvider', GeolocationProvider)
    .name;

export { commonServices };