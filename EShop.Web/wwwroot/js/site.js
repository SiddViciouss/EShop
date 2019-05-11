// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
(function ($) {
    var section = location.pathname.split("/")[1].toLowerCase();

    $('a', '.main_menu').each(function (a, b) {
        if (section) {
            var menuRef = $(b).attr("href").toLowerCase();
            if ((menuRef.indexOf(section) >= 0)) {
                $(b).parent().addClass("sale-noti");
            }
            else {
                $(b).parent().removeClass("sale-noti");
            }
        }
        else if (a===0) {
            $(b).parent().addClass("sale-noti");
        }
    });

    //$("body > .container").css("min-height", $("body").height() - $("header").outerHeight() - $("footer").outerHeight() - 23 + "px");

})(jQuery);