import { Injectable } from './../../utility/injectable';

export class HttpService extends Injectable {
    static get $inject() {
        return ['$http', 'RRLoggingService'];
    }

    constructor(...deps) {
        super(...deps);

        this.logger = this.RRLoggingService.createLogger(this.constructor.name);
        this.getCache = {};
    }

    get(url, maxAge = 0) {
        let logger = this.logger;
        return this._getGetRequest(url).then(result => {
            logger.debug(result);
            return result.data;
        });
    }

    getModel(url, model, maxAge = 0) {
        return this.get(url, maxAge).then(data => {
            if (angular.isArray(model)) {
                model.length = 0;
                model.push(...data);
            } else if (angular.isObject(model)) {
                angular.merge(model, data);
            }
        });
    }

    post(...args) {
        let logger = this.logger;
        return this.$http.post(...args).then(result => {
            logger.debug(result);
            return result.data;
        });
    }

    _getGetRequest(url, maxAge) {
        let requestTime = new Date().getTime();
        let responseFromCache = this.getCache[url];
        if (!responseFromCache || responseFromCache.requestTime < (requestTime - maxAge)) {
            let responsePromise = this.$http.get(url);
            responseFromCache = { requestTime, responsePromise };
            this.getCache[url] = responseFromCache;
        }
        return responseFromCache.responsePromise;
    }
}