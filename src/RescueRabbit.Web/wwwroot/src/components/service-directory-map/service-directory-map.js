import { Injectable } from './../../utility/injectable';

class ServiceDirectoryMapController extends Injectable {
    static get $inject() {
        return ['RRGeolocationProvider'];
    }

    constructor(...deps) {
        super(...deps);

        let self = this;

        this.map = {
            center: {
                lat: 51.505,
                lng: -0.09,
                zoom: 8
            },
            markers: [],
            layers: {
                baselayers: {
                    osm: {
                        name: "OpenStreetMap (XYZ)",
                        type: "xyz",
                        url: 'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png',
                        layerOptions: {
                            subdomains: ['a', 'b', 'c'],
                            attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors',
                            continuousWorld: true
                        }
                    }
                }
            }
        };

        this.RRGeolocationProvider.get().then(location => {
            self.map.center.lat = location.coords.lat;
        });
    }
}

import { componentFactory } from './../../utility/component-factory';

import 'leaflet';
import 'leaflet/dist/leaflet.css!';
import 'angular-simple-logger/dist/angular-simple-logger.min.js';
import 'ui-leaflet';

import { commonServices } from './../../common/common-services';

export let serviceDirectoryMap = componentFactory(
    'serviceDirectoryMap',
    ServiceDirectoryMapController,
    ['nemLogging', 'ui-leaflet', commonServices]);