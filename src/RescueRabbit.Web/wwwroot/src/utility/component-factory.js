import { paths } from './../paths';

export function componentFactory(name, controller, deps = [], directiveOptions = {}) {
    let dashedName = name
        .replace(/(?:^|\.?)([A-Z])/g, (x, y) => `-${y.toLowerCase()}`)
        .replace(/^_/, "");
    let moduleName = `rr.components.${dashedName}`;
    let templateUrl = `/${paths.components}${dashedName}/${dashedName}.html`;

    let options = {
        controllerAs: 'ctrl'
    };
    angular.extend(options, directiveOptions);

    const componentOptions = {
        controller,
        templateUrl,
        restrict: 'E',
        scope: {}
    };
    angular.merge(options, componentOptions);

    angular.module(moduleName, deps).directive(name, () => options);

    return moduleName;
}