SystemJS.config({
  transpiler: "plugin-babel",
  packages: {
    "app": {
      "main": "app.js",
      "meta": {
        "*.js": {
          "loader": "plugin-babel"
        }
      }
    }
  }
});

SystemJS.config({
  packageConfigPaths: [
    "npm:@*/*.json",
    "npm:*.json",
    "github:*/*.json"
  ],
  map: {
    "angular": "npm:angular@1.5.3",
    "angular-simple-logger": "github:nmccready/angular-simple-logger@0.1.7",
    "css": "github:systemjs/plugin-css@0.1.20",
    "leaflet": "github:Leaflet/Leaflet@0.7.7",
    "plugin-babel": "npm:systemjs-plugin-babel@0.0.8",
    "plugin-css": "github:systemjs/plugin-css@0.1.20",
    "ui-leaflet": "github:angular-ui/ui-leaflet@1.0.0"
  },
  packages: {}
});
