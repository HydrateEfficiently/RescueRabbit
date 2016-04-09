import { Injectable } from './../../utility/injectable';

class Logger {
    constructor($log, categoryName) {
        this.$log = $log;
        this.categoryName = categoryName;
    }

    info(...args) {
        this._log('info', ...args);
    }

    debug(...args) {
        this._log('debug', ...args);
    }

    error(...args) {
        this._log('error', ...args);
    }

    warn(...args) {
        this._log('warn', ...args);
    }

    logInitialized(item) {
        this.debug(`${item.constructor.name} initialized`);
    }

    _log(logFuncName, ...args) {
        if (this.categoryName) {
            args.splice(0, 0, this.categoryName);
        }
        args.splice(0, 0, logFuncName.toUpperCase());
        this.$log[logFuncName](...args);
    }
}

export class LoggingService extends Injectable {
    static get $inject() {
        return ['$log'];
    }

    constructor(...deps) {
        super(...deps);

        this.logger = this.createLogger();
    }

    createLogger(categoryName) {
        let logger = new Logger(this.$log, categoryName)
        logger.debug('initialized');
        return logger;
    }

    info(...args) {
        this.logger.info(...args);
    }

    debug(...args) {
        this.logger.debug(...args);
    }

    error(...args) {
        this.logger.erro(...args);
    }

    warn(...args) {
        this.logger.warn(...args);
    }

    logInitialized(item) {
        this.logger.logInitialized(item);
    }
}