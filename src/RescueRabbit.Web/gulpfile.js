/// <binding Clean='clean' />
"use strict";

var gulp = require("gulp"),
    rimraf = require("rimraf"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify"),
    less = require("gulp-less"),
    rename = require("gulp-rename");

gulp.task("less-compile", function () {
    return gulp.src("./Styles/site.less")
        .pipe(less())
        .pipe(gulp.dest("./wwwroot/assets/css"));
});