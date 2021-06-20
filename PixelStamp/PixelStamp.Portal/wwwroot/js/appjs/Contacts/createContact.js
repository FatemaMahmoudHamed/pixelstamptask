//$(document).ready(function () {
//    //Initialize Select2 Elements
//    //$('.select2bs4').select2({
//    //    theme: 'bootstrap4'
//    //})
//$(document).ready(function () {
//    initAssignModal();
//});
    $("#zipcode").autocomplete({
        minLength: 3,
        source: function (request, response) {
            $.ajax({
                url: "/Contacts/GetRegions",
                type: "POST",
                dataType: "json",
                data: { Prefix: request.term },
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
            $("#ZipId").val(ui.item.value); // save selected id to hidden input
            $("#zipcode").val(ui.item.label); // display the selected text
            return false;
        }
    }).focus(function () {
        $(this).attr('autocomplete', 'zip');
    });

    $("#Phyzipcode").autocomplete({
        minLength: 3,
        source: function (request, response) {
            $.ajax({
                url: "/Contacts/GetRegions",
                type: "POST",
                dataType: "json",
                data: { Prefix: request.term },
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
            $("#PhyiscalZipCodeId").val(ui.item.value); // save selected id to hidden input
            $("#Phyzipcode").val(ui.item.label); // display the selected text
            return false;
        }
    }).focus(function () {
        $(this).attr('autocomplete', 'zip');
    });

    $("#Mailzipcode").autocomplete({
        minLength: 3,
        source: function (request, response) {
            $.ajax({
                url: "/Contacts/GetRegions",
                type: "POST",
                dataType: "json",
                data: { Prefix: request.term },
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
            $("#MailingZipCodeId").val(ui.item.value); // save selected id to hidden input
            $("#Mailzipcode").val(ui.item.label); // display the selected text
            return false;
        }
    }).focus(function () {
        $(this).attr('autocomplete', 'zip');
    });
    $("#SICCode").autocomplete({
        minLength: 3,
        source: function (request, response) {
            $.ajax({
                url: "/Contacts/GetSicCode",
                type: "POST",
                dataType: "json",
                data: { Prefix: request.term },
                success: function (data) {
                    console.log(data);

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
            $("#SICCodeId").val(ui.item.value); // save selected id to hidden input
            $("#SICCode").val(ui.item.label); // display the selected text
            return false;
        }
    }).focus(function () {
        $(this).attr('autocomplete', 'Siccode');
    });

 
