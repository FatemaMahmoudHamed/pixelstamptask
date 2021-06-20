$(document).ready(function () {
    var stateId = 0;
    var cityId = 0;
    //Initialize Select2 Elements
    $('#states-select').select2({
        theme: 'bootstrap4'
    })
    $('#states-select').on('select2:select', function (e) {
        var data = e.params.data;
        stateId = data.id;
    });

    $("#cityName").autocomplete({
        minLength: 3,
        source: function (request, response) {
            $.ajax({
                url: "/cities/getCities/",
                type: "POST",
                dataType: "json",
                data: { Prefix: request.term},
                success: function (data) {
                    response($.map(data, function (item) {
                        console.log(item);
                        return { label: item.text, value: item.value };
                    }))

                }
            })
        },
        messages: {
            noResults: "", results: ""
        },
        select: function (event, ui) {
            $("#cityName").val(ui.item.label); // display the selected text
            cityId = ui.item.value;
            return false;
        }
    }).focus(function () {
        $(this).attr('autocomplete', 'city');
    });

}) 