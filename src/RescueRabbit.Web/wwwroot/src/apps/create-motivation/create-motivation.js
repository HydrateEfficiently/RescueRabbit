import { CreateMotivationController } from './create-motivation-controller';

let app = angular.module('rr.create-motivation.app', [
    supportDirectoryMap
])
.controller('CreateMotivationController', CreateMotivationController);

import { appRunnerFactory } from './../../utility/app-runner-factory';
let run = appRunnerFactory(app);
export { run };