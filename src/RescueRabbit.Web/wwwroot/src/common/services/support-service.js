import { Injectable } from './../../utility/injectable';

export class SupportService extends Injectable {
    static get $inject() {
        return ['RRHttpService'];
    }

    constructor(...deps) {
        super(...deps);
    }

}