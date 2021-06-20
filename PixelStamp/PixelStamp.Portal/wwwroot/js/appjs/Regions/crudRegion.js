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
                url: "/regions/getCities/",
                type: "POST",
                dataType: "json",
                data: { Prefix: request.term, State: stateId },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.text, value: item.value };
                    }))

                }
            })
        },
        messages: {
            noResults: "", results: ""
        },
        select: function (event, ui) {
            $("#cityId").val(ui.item.value); // save selected id to hidden input
            $("#cityName").val(ui.item.label); // display the selected text
            cityId = ui.item.value;
            return false;
        },

    }).focus(function () {
        $(this).attr('autocomplete', 'city');
    });

    $('#zipcode-select').select2({
        theme: 'bootstrap4',
        placeholder: 'Search for zipcode',
        minimumInputLength: 3,
        ajax: {
            url: '/regions/getZipcodes',
            type: "POST",
            dataType: "json",
            data: function (params) {
                console.log(params)
                var query = {
                    Prefix: params.term,
                    City: cityId
                }
                return query;
            },
            processResults: function (data, params) {
                var items = $.map(data, function (item) {
                    return { id: item.value, text: item.text };
                });
                return {
                    results: items
                }
            },
            transport: function (params, success, failure) {
                var $request = $.ajax(params);
                $request.then(success);
                $request.fail(failure);
                return $request;
            },
           
        }
    }).focus(function () {
        $(this).attr('autocomplete', 'zip');
    });
}) 