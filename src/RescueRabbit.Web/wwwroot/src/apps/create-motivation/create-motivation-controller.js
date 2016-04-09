import { Injectable } from './../../utility/injectable';

export class CreateMotivationController extends Injectable {
    static get $inject() {
        return ['$window', 'RRLoggingService', 'RRUrlService', 'RRApiInterfaceFactory'];
    }

    constructor(...deps) {
        super(...deps);

        this.logger = this.RRLoggingService.createLogger(this.constructor.name);
        this.motivationApi = this.RRApiInterfaceFactory.createApiInterface('motivation');
        this.motivation = {};
    }

    saveMotivation() {
        this.motivationApi.create(this.motivation)
            .then(this.navigateToMotivations.bind(this));
    }

    navigateToMotivations() {
        this.$window.location.href = this.RRUrlService.getUrl('motivation.index');
    }
}