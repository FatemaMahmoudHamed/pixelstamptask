$(function () {
    $('form').submit(function () {
        if ($(this).valid()) {
            $.ajax({
                url: "/MCShippers/Adminstration/GetShippersTransactions",
                type: 'POST',
                data: $(this).serialize(),
                success: function (result) {
                    console.log(result);
                    $('#transactionContainer').html(result);
                }
            });
        }
        return false;
    });
});