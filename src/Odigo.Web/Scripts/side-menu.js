$(document).ready(function () {
    $('.admin-accordion a').click(function () {
        $('.admin-accordion li a').removeClass('active');
        $(this).addClass('active');

    });



});