import { Injectable } from './../../utility/injectable';

export class ApiInterfaceFactory extends Injectable {
    static get $inject() {
        return ['RRHttpService', 'RRUrlService'];
    }

    constructor(...deps) {
        super(...deps);
    }

    createApiInterface(controllerName) {
        let { RRUrlService, RRHttpService } = this;

        let controllerPath = `api.${controllerName}`;
        let actions = RRUrlService.getActions(controllerPath);

        let apiInterface = {};
        for (let actionName in actions) {
            let action = actions[actionName];
            apiInterface[actionName] = (...args) => {
                let url = action.urlPattern;
                if (action.parameterNames.length) {
                    let params = args.shift();
                    url = this.RRUrlService.resolveUrl(action.urlPattern, params);
                }
                return this.RRHttpService[action.method](url, ...args);
            };
        }

        return apiInterface;
    }
}