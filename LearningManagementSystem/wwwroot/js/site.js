var width = (window.innerWidth > 0) ? window.innerWidth : screen.width;

$('#show-seacond-nav').click(function () {
    if ($("#seacond-nav").hasClass("d-none")) {
        $('#seacond-nav').removeClass('d-none');
        if ($("body").hasClass('nav-md')) {
            $('.right_col').css('margin-left', '460px');
            $('.top_nav').css('margin-left', '460px');
        }
        else {
            $('.right_col').css('margin-left', '140px');
            $('.top_nav').css('margin-left', '140px');

        }
    }
    else {
        $('#seacond-nav').addClass('d-none');
        if ($("body").hasClass('nav-md')) {
            $('.right_col').css('margin-left', '230px');
            $('.top_nav').css('margin-left', '230px');
        }
        else {
            $('.right_col').css('margin-left', '70px');
            $('.top_nav').css('margin-left', '70px');
        }
    }
});

if (width < 700) {
    setTimeout(function () {
        $('.right_col').css('margin-left', '0');
        $('.top_nav').css('margin-left', '0');
    }, 200);
}

function SidebarMenuClose(current) {
    if ($(current).find('ul').hasClass("d-block")) {
        $(current).find('ul').removeClass('d-block')
        $(current).find('ul').addClass('d-none')
    }
    else {
        $(current).find('ul').removeClass('d-none')
        $(current).find('ul').addClass('d-block')
    }
}

function SidebarMenu(current) {
    $('.ul-child').removeClass('d-block')
    $('.ul-child').addClass('d-none')
    $(current).find('ul').removeClass('d-none')
    $(current).find('ul').addClass('d-block')
}