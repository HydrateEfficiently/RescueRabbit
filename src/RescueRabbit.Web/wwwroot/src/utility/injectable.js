export class Injectable {
    static get $inject() {
        return [];
    }

    constructor(...deps) {
        var self = this;
        this.constructor.$inject.forEach((name, index) => {
            self[name] = deps[index];
        });
    }
}