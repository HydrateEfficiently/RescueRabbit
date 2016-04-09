export function appRunnerFactory(app) {
    return function (data) {
        app.constant('RRServerData', data);
        angular.element(document).ready(() => angular.bootstrap(document, [app.name]));
    };
}