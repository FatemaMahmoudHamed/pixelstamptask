$('.showAssignEmpModal').click(function () {
    ShowLoaderWaitModal();
    let contactID = $(this).data('id')
    showAssignEmpModal(contactID)
});

function showAssignEmpModal(contactID) {
    $.ajax({
        url: "/Contacts/AssignEmployeeModal/?contactID=" + contactID,
        type: 'GET',
        success: function (result) {
            $("#LoaderWaitModal").modal('hide');
            $('#empModalContainer').html(result);
            $('#empModal').modal('show');
        }
    });
}

function FillDll(systemID) {
    console.log(systemID);
    if (systemID != "") {
        $("#spinner").show();
        $.ajax({
            url: "/Contacts/GetSalesmanListBySystemID/?systemID=" + systemID,
            type: 'GET',
            success: function (data) {
                console.log(data);
                $('#SalesManddl').empty();
                var options = '';
                options += '<option value="Please Select a Salesman ">Select</option>';
                for (var i = 0; i < data.length; i++) {
                    options += '<option value="' + data[i].value + '">' + data[i].text + '</option>';
                }
                $('#SalesManddl').append(options);
                $("#spinner").hide();
            },
            error: function () {
                alert("Error occured!!")
            }
        });
    }
    else {
        $("#SalesManddl").empty();
    }

}
$(function () {
    $('form').submit(function () {
        if ($(this).valid()) {
            $.ajax({
                url: "/Contacts/AssignContactToEmp",
                type: 'POST',
                data: $(this).serialize(),
                success: function (data) {
                    console.log(data);
                    if (data.isPassed) {
                        alert(data.message)
                        $('#empmodal').modal('hide');
                        return;
                    }
                    alert(data.message)
                }
            });
        }
        return false;
    });
});

function ShowLoaderWaitModal() {
    $.ajax({
        url: "/Contacts/LoaderModal",
        type: 'GET',
        success: function (result) {
            $('#LoaderWaitModalContainer').html(result);
            $("#LoaderWaitModal").modal('show');
        }
    });
}
