import { Injectable } from './../../utility/injectable';

export class SessionService extends Injectable {
    static get $inject() {
        return ['$window', 'RRHttpService', 'RRUrlService', 'RRServerData'];
    }

    constructor(...deps) {
        super(...deps);
    }

    getUser() {
        return this.RRServerData.user;
    }

    logout() {
        let { $window, RRUrlService, RRHttpService } = this;
        let logOffUrl = RRUrlService.getUrl('api.account.logout');
        return RRHttpService.post(logOffUrl).then(() => 
            $window.location.href = RRUrlService.getUrl('home.index'));

        
        // var success = () => this.$window.location.href = urls.home.landing;
        // var error = () => this.ErrorService.post("Failed to log off!");
        // return this.$http.post(urls.account.logOff).then(
        //     success.bind(this),
        //     error.bind(this));
    }
}