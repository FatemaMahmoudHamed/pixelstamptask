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
                url: "/zipcodes/getCities/",
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
        }
    }).focus(function () {
        $(this).attr('autocomplete', 'zip');
        });

    $("#zipcode").autocomplete({
        minLength: 5,
        source: function (request, response) {
            $.ajax({
                url: 'https://public.opendatasoft.com/api/records/1.0/search/?dataset=us-zip-code-latitude-and-longitude&q=' + document.getElementById('zipcode').value,
                type: "GET",
                dataType: "json",
                success: function (data) {
                    $("#Lat").val(data.records[0].fields.latitude);
                    $("#Lng").val(data.records[0].fields.longitude);
                    $("#Geopoint").val(data.records[0].fields.geopoint);
                    $("#Timezone").val(data.records[0].fields.timezone);           
                       return { data }; 
                }
            })
        },
        messages: {
            noResults: "", results: ""
        },
    }).focus(function () {
        $(this).attr('autocomplete', 'zipcode');
    });

    //$("#zipcode").autocomplete({
    //    minLength: 3,
    //    source: function (request, response) {
    //        $.ajax({
    //            url: "/ZipCodes/GetZipcodes",
    //            type: "POST",
    //            dataType: "json",
    //            data: { Prefix: request.term },
    //            success: function (data) {
    //                response($.map(data, function (item) {
    //                    console.log(item);
    //                    return { label: item.text, value: item.value };
    //                }))

    //            }
    //        })
    //    },
    //    messages: {
    //        noResults: "", results: ""
    //    },
    //    select: function (event, ui) {
    //        $("#zipcode").val(ui.item.label); 
    //        return false;
    //    }
    //}).focus(function () {
    //    $(this).attr('autocomplete', 'zip');
    //});
}) 