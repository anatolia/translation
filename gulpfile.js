const { src, dest, parallel, series, watch } = require('gulp');
const sass = require('gulp-sass');
const minifyCSS = require('gulp-csso');
const terser = require("gulp-terser");
const concat = require('gulp-concat');

const basePath = 'Source/Translation.Client.Web/';

let DEV = false;

function siteCss() {
    return src([
            basePath + 'wwwroot/css/*.scss',
            basePath + 'wwwroot/plugins/**/css/*.scss'
        ], { sourcemaps: DEV })
        .pipe(sass())
        .pipe(minifyCSS())
        .pipe(concat('site.min.css'))
        .pipe(dest(basePath + 'wwwroot/css/', { sourcemaps: DEV }));
}

function siteJs() {
    return src([
        basePath + "wwwroot/js/src/site.js",
        basePath + "wwwroot/js/src/translation.js",
        basePath + "wwwroot/plugins/**/*.js",
    ], { sourcemaps: DEV })
        .pipe(terser())
        .pipe(concat('site.min.js'))
        .pipe(dest(basePath + 'wwwroot/js', { sourcemaps: DEV }))
}

function setDev(cb) {
    DEV = true;
    cb();
}

function setProd(cb) {
    DEV = false;
    cb();
}

const JS = series(
    siteJs,
);

const CSS = series(
    siteCss,
);

function watcher() {
    watch([
        basePath + 'wwwroot/css/*.scss',
        basePath + "wwwroot/plugins/**/css/*.scss",
    ], series(setDev, CSS));
    watch([
        basePath + "wwwroot/js/src/*.js",
        basePath + "wwwroot/plugins/**/*.js",
], series(setDev, JS));
}

const dev = series(setDev, parallel(JS, CSS), watcher);

exports.watcher = watcher;
exports.dev = dev;
exports.default = series(setProd, parallel(JS, CSS));