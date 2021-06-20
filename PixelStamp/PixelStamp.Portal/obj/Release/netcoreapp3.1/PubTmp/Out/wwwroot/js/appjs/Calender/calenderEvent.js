{
    var eventColor = ""

    $(document).ready(function () {
        initEventModal();
    });


    $('.showTimeModal').click(function () {
        let timeUrl = $(this).data('id')
        showNewTimeModal(timeUrl) 
    });

    function showNewTimeModal(timeUrl) {
        console.log(timeUrl)
        $.ajax({
            url: "/CalenderEvent/AddEventModal/?timeUrl=" + timeUrl,
            type: 'GET',
            success: function (result) {
                $('#modalContainer').html(result);
                $('#modal').modal('show');
            }
        });
    }
    

    function showTimeModal(id) {
        console.log(id)
        $.ajax({
            url: "/CalenderEvent/EditEventModal/?id=" + id,
            type: 'GET',
            success: function (result) {
                $('#modalContainer').html(result);
                $('#modal').modal('show');
            }
        });
    }

    initEventModal = function () {
        $("#eventForm").validate({
            rules: {
                Title: { required: true },
                Description: { required: true },
                Start: { required: true, date: true, minStartDate: true },
                End: { required: true, date: true, minEndDate: true },
            },
            messages: {
                Title: {
                    required: "Time title is requird",
                },
                Description: {
                    required: "Time description is requird",
                },
                Start: {
                    required: "Start time is required",
                    date: "Start time should be datetime",
                    minStartDate: "Start time should be greater than or equal today",
                }, 
                End: {
                    required: "End time is required",
                    date: "End time should be datetime",
                    minEndDate: "End time should be greater than start date",
                }
            },
            submitHandler: function (form) {
                saveEvent($(form));
            }
        });
    }

    $.validator.addMethod("minStartDate", function (value, element) {
        var curDate = new Date();
        var inputDate = new Date(value);
        if (inputDate >= curDate)
            return true;
        return false;
    });  

    $.validator.addMethod("minEndDate", function (value, element) {
        var startDate = new Date($('#Start').val());
        var inputDate = new Date(value);
        if (inputDate > startDate)
            return true;
        return false;
    });  

    $.validator.addMethod("minDate", function (value, element) {
        var curDate = new Date();
        var inputDate = new Date(value);
        if (inputDate >= curDate)
            return true;
        return false;
    }, "Date should be greater than or equal today");  

    saveEvent = function (form) {
        var url = "/CalenderEvent/SaveEventModal"
        if (!eventColor)
            eventColor = "rgb(0, 86, 179)";
        var postData = form.serialize() + "&BackgroundColor=" + eventColor + "&BorderColor=" + eventColor
        console.log(postData)
        $.post(url, postData, function (response) {
            var data = response.data; 
            if (data.isPassed) {
                alert(data.message)
                $('#modal').modal('hide');
                return;
            }
            alert(data.message)
        });
    }

    function removeEvent(id) {
        $('#modal').modal('hide');
        $.ajax({
            url: '/CalenderEvent/RemoveEvent/' + id,
            type: "GET",
            dataType: "JSON",
            success: function (result) {
                let response = result.data;
                if (response.isPassed) {
                    if (calendar) {
                        var event = calendar.getEventById(id);
                        event.remove();
                    }
                    alert(response.message)
                    return;
                }
                alert(response.message)
            }
        });
    }
}