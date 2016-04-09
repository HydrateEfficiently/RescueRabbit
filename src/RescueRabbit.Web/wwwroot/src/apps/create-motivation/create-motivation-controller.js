import { Injectable } from './../../utility/injectable';

export class CreateMotivationController extends Injectable {
    static get $inject() {
        return ['$window', 'RRLoggingService', 'RRHttpService', 'RRUrlService', 'RRApiInterfaceFactory'];
    }

    constructor(...deps) {
        super(...deps);

        this.logger = this.RRLoggingService.createLogger(this.constructor.name);
        this.motivationApi = this.RRApiInterfaceFactory.createApiInterface('motivation');
        this.motivation = {};
    }

    saveMotivation() {
        // let { $window, RRUrlService, RRHttpService } = this;
        // let url = RRUrlService.getUrl('api.motivation.create');
        // let motivationsUrl = RRUrlService.getUrl('motivation.index');
        // this.RRHttpService.post(url, this.motivation)
        //     .then(() => $window.location.href = motivationsUrl);

        this.motivationApi.create(null, this.motivation)
            .then(this.navigateToMotivations.bind(this));
    }

    navigateToMotivations() {
        this.$window.location.href = this.RRUrlService.getUrl('motivation.index');
    }
}