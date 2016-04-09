import { Injectable } from './../../utility/injectable';

export class HttpService extends Injectable {
    static get $inject() {
        return [];
    }

    constructor(...deps) {
        super(...deps);

        this.cache = {};
    }

    get(url, maxAge = 0) {
        let requestTime = new Date().getTime();
        let responseFromCache = this.cache[url];
        if (!responseFromCache || responseFromCache.requestTime < (requestTime - maxAge)) {
            let responsePromise = this.$http.get(url);
            responseFromCache = { requestTime, responsePromise };
            this.cache[url] = responseFromCache;
        }
        return responseFromCache.responsePromise;
    }
}