const gulp = require('gulp');
const concat = require('gulp-concat');
const terser = require('gulp-terser');
const cleanCss = require('gulp-clean-css');

const bundles = require('./bundles.json');

const scripts = bundles
    .filter(bundle => bundle.tipo === 'script')
    .map(bundle => () => gulp
        .src(bundle.conteudo)
        .pipe(terser())
        .pipe(concat(bundle.nome))
        .pipe(gulp.dest('./wwwroot/bundles/'))
    );

const estilos = bundles
    .filter(bundle => bundle.tipo === 'estilo')
    .map(bundle => () => gulp
        .src(bundle.conteudo)
        .pipe(cleanCss())
        .pipe(concat(bundle.nome))
        .pipe(gulp.dest('./wwwroot/bundles/'))
    );

exports.build = gulp.parallel([...scripts, ...estilos]);