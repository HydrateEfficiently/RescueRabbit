import { supportDirectoryMap } from './../../components/support-directory-map/support-directory-map';

import { SupportDirectoryController } from './support-directory-controller';

let app = angular.module('rr.support-directory.app', [
    supportDirectoryMap
])
.controller('SupportDirectoryController', SupportDirectoryController);

import { appRunnerFactory } from './../../utility/app-runner-factory';
let run = appRunnerFactory(app);
export { run };