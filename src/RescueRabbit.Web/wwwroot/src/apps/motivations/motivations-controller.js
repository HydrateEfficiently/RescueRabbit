import { Injectable } from './../../utility/injectable';

export class MotivationsController extends Injectable {
    static get $inject() {
        return ['$window', 'RRLoggingService', 'RRHttpService', 'RRUrlService'];
    }

    constructor(...deps) {
        super(...deps);

        this.logger = this.RRLoggingService.createLogger(this.constructor.name);
        this.motivations = [];

        this.loadMotivations();
    }

    loadMotivations() {
        let url = this.RRUrlService.list();
    }

    saveMotivation() {
        let { $window, RRUrlService, RRHttpService } = this;
        let url = RRUrlService.getUrl('api.motivation.create');
        let motivationsUrl = RRUrlService.getUrl('motivation.index');
        this.RRHttpService.post(url, this.motivation)
            .then(() => $window.location.href = motivationsUrl);
    }

    cancelMotivation() {
        this.$window.location.href = this.RRUrlService.getUrl('motivation.index');
    }
}