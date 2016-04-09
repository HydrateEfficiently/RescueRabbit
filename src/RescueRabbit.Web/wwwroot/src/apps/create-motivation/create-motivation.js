import { commonServices } from './../../common/common-services';

let app = angular.module('rr.create-motivation.app', [
    commonServices
]);

import { CreateMotivationController } from './create-motivation-controller';
app.controller('CreateMotivationController', CreateMotivationController);

import { appRunnerFactory } from './../../utility/app-runner-factory';
let run = appRunnerFactory(app);
export { run };