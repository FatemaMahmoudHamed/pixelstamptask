﻿$(function () {
    $('form').submit(function () {
        if ($(this).valid()) {
            $.ajax({
                url: "/MCShippers/Adminstration/GetDueToCarriers",
                type: 'POST',
                data: $(this).serialize(),
                success: function (result) {
                    $('#CarriersListContainer').html(result);
                }
            });
        }
        return false;
    });
});