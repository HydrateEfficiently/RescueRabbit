import { serviceDirectoryMap } from './../../components/service-directory-map/service-directory-map';

import { ServiceDirectoryController } from './service-directory-controller';

let app = angular.module('rr.service-directory.app', [
    serviceDirectoryMap
])
.controller('ServiceDirectoryController', ServiceDirectoryController);

export function start(data) {
    app.constant('RRServerData', data);
    angular.element(document).ready(() => angular.bootstrap(document, [app.name]));
}