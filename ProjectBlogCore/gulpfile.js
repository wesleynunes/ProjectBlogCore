var gulp = require('gulp');
var cssmin = require('gulp-cssmin');
//var browserSync = require('browser-sync').create();
//var uncss = require('gulp-uncss'); // retirar class do boostrap que não estaja ultilizando
//var concat = require('gulp-concat'); // variavel para concatenar

//gulp.task('browser-sync', function () {
//    browserSync.init({
//        proxy: 'localhost: 5000'
//    });

//    gulp.watch('./Assets/css/**/*.css', ['css']);
//    gulp.watch('./Assets/js/*.js', ['js']);
//});

// fica excultado quando tiver alteração execulta a função css
// gulp.task('watch-css', function () {
//     gulp.watch('./Assets/css/**/*.css', ['css']);
//     gulp.watch('./Assets/js/*.js', ['js']);
// });

gulp.task('js', function () {
    return gulp.src([
        './node_modules/bootstrap/dist/js/bootstrap.min.js',
        './node_modules/bootstrap/dist/js/bootstrap.bundle.min.js',
        './node_modules/jquery/dist/jquery.min.js',        
        './node_modules/jquery-validation/dist/jquery.validate.min.js',
        './node_modules/jquery-validation-unobtrusive/dist/jquery.validate.unobtrusive.min.js',
        './node_modules/glyphicons/glyphicons.js',
        './node_modules/@fortawesome/fontawesome-free/js/fontawesome.min.js',
        './node_modules/@fortawesome/fontawesome-free/js/all.min.js', 
        './Assets/js/site.min.js',
        './Assets/qrcodejs/qrcode.min.js',
        //Painel 
         './Assets/panel/js/dashboard.js',
         './Assets/panel/js/main.js',
         './Assets/panel/js/widgets.js',
         './Assets/panel/js/lib/vector-map/jquery.vmap.min.js',
         './Assets/panel/js/lib/vector-map/jquery.vmap.sampledata.js',
         './Assets/panel/js/lib/vector-map/country/jquery.vmap.world.js',
         './Assets/panel/js/lib/chart-js/Chart.bundle.js',
         './Assets/panel/js/plugins.js'
    ])
        //.pipe(browserSync.stream())
        .pipe(gulp.dest('wwwroot/js/'));
});

gulp.task('css', function () {
    return gulp.src([
        './Assets/css/PagedList.css',
        './Assets/css/site.css',
        './node_modules/bootstrap/dist/css/bootstrap.min.css',
        './node_modules/@fortawesome/fontawesome-free/css/fontawesome.min.css',
        './node_modules/@fortawesome/fontawesome-free/css/all.min.css',  
        //painel
         './Assets/panel/css/style.css',
         './Assets/panel/css/flag-icon.min.css',
         './Assets/panel/css/normalize.css',
         './Assets/panel/css/font-awesome.min.css',
         './Assets/panel/css/lib/vector-map/jqvmap.min.css',
         './Assets/panel/css/cs-skin-elastic.css',
         './Assets/panel/css/themify-icons.css',
         './Assets/panel/scss/style-scss.css'
    ])
        //.pipe(concat('site.min.css'))  // concatena os arquivos esta com erro ao cocatenar
        //.pipe(uncss({ html: ['Views/**/*.cshtml'] }))
        //.pipe(browserSync.stream())
        .pipe(cssmin())    
        .pipe(gulp.dest('wwwroot/css/'));
});

gulp.task('images', function () {
    return gulp.src([
        './Assets/images/favicon.ico',
        './Assets/panel/images/avatar/1.jpg'
    ])
        //.pipe(browserSync.stream())        
        .pipe(gulp.dest('wwwroot/images/'));
});
