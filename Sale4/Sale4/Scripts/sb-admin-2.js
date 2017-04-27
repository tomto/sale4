
//Loads the correct sidebar on window load,
//collapses the sidebar on window resize.
// Sets the min-height of #page-wrapper to window size
$(function() {
    //$(window).bind("load resize", function() {
    //    topOffset = 50;
    //    width = (this.window.innerWidth > 0) ? this.window.innerWidth : this.screen.width;
    //    if (width < 768) {
    //        $('div.navbar-collapse').addClass('collapse');
    //        topOffset = 100; // 2-row-menu
    //    } else {
    //        $('div.navbar-collapse').removeClass('collapse');
    //    }

    //    height = ((this.window.innerHeight > 0) ? this.window.innerHeight : this.screen.height) - 1;
    //    height = height - topOffset;
    //    if (height < 1) height = 1;
    //    if (height > topOffset) {
    //        $("#page-wrapper").css("min-height", (height) + "px");
    //    }
    //});

    //var url = window.location;
    //var element = $('ul.nav a').filter(function () {
    //    var u1 = url.href;
    //    var u2 = this.href;
    //    if (u1.indexOf("#")>0) {
    //        u1 = u1.substring(0, u1.indexOf("#"));
    //    }
    //    if (u2.indexOf("#") > 0) {
    //        u2 = u2.substring(0, u2.indexOf("#"));
    //    }
    //    return (u1 == u2 || u1.indexOf(u2) >= 0);
    //}).addClass('active').parent().parent().addClass('in').parent();
    //if (element.is('li')) {
    //    element.addClass('active');
    //}



    $('.input-group').on('focus', '.form-control', function () {
        $(this).closest('.input-group, .form-group').addClass('focus');
    }).on('blur', '.form-control', function () {
        $(this).closest('.input-group, .form-group').removeClass('focus');
    });
});



