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
            apiInterface[actionName] = (params) => {
                debugger;
                let url = this.RRUrlService.resolveUrl(action.urlPattern, params);
                this.RRHttpService[action.method]();
            };
        }

        return apiInterface;
    }
}