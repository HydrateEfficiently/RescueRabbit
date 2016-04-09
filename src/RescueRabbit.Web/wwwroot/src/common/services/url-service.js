import { Injectable } from './../../utility/injectable';

export class UrlService extends Injectable {
    static get $inject() {
        return ['RRServerData'];
    }

    constructor(...deps) {
        super(...deps);

        this.serverData = this.RRServerData;
    }

    getUrls() {
        return this.serverData && this.serverData.urls ? this.serverData.urls : null;
    }

    getUrl(actionPath, params) {
        let config = this._getConfig(actionPath);
        let url = this.resolveUrl(config.urlPattern, params);
        let method = config.method;
        return { url, method };
    }

    getActions(controllerPath) {
        let config = this._getConfig(controllerPath);
        return config;
    }

    resolveUrl(url, params) {
        if (params) {
            let paramsToReplace = url.split('/').filter(s =>
                s.indexOf(encodeURIComponent(':').toLowerCase()) === 0);

            if (typeof params !== 'object') {
                if (paramsToReplace.length === 1) {
                    url = url.replace(paramsToReplace[0], params);
                    return url;
                }
            }

            for (let key in params) {
                let param = encodeURIComponent(`:${key}`).toLowerCase();
                url = url.replace(param, params[key]);
            }
        }
        return url;
    }

    _getConfig(path) {
        let config = this.getUrls();
        if (!config) {
            throw 'No actions were configured for this page.';
        }

        let pathComponents = actionPath.split('.');
        for (let i = 0; i < pathComponents.length; i++) {
            config = config[pathComponents[i]];
            if (!config) {
                throw `Could not find action with path ${pathComponents}`;
            }
        }
    }
}