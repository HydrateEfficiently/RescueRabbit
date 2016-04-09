import { commonServices } from './../../common/common-services';

let app = angular.module('rr.motivation.app', [
    commonServices
]);

import { MotivationsController } from './motivations-controller';
app.controller('MotivationsController', MotivationsController);

import { appRunnerFactory } from './../../utility/app-runner-factory';
let run = appRunnerFactory(app);
export { run };