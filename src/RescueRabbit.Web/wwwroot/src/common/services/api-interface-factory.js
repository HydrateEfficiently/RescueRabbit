import { Injectable } from './../../utility/injectable';

export class ApiInterfaceFactory extends Injectable {
    static get $inject() {
        return ['RRHttpService', 'RRUrlService'];
    }

    constructor(...deps) {
        super(...deps);
    }

    createApiInterface(controllerName) {
        debugger;
        let { RRUrlService, RRHttpService } = this;

        let controllerPath = `api.${controllerName}`;
        let actions = RRUrlService.getActions(controllerPath);

        let apiInterface = {};
        for (let actionName in actions) {
            let action = actions[actionName];
            apiInterface[actionName] = (params, ...args) => {
                debugger;
                let url = this.RRUrlService.resolveUrl(action.urlPattern, params);
                return this.RRHttpService[action.method](url, ...args);
            };
        }

        return apiInterface;
    }
}