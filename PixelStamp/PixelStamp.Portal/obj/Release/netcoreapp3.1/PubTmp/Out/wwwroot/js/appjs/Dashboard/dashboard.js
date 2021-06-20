{
    let Calendar = FullCalendar.Calendar;
    var calendarEl = document.getElementById('calendar')
    var calendar = new Calendar(calendarEl, {
        plugins: ['bootstrap', 'interaction', 'dayGrid', 'timeGrid'],
        header: {
            left: 'prev,next today',
            center: 'title',
            right: 'dayGridMonth,timeGridWeek,timeGridDay'
        },
        'themeSystem': 'bootstrap',
        eventClick: function (args) {
            let event = args.event;
            console.log(event)
            args.jsEvent.preventDefault();
            showTimeModal(event.id);
            /*
            $('#modalTitle').html(event.title);
            if (event.extendedProps)
                $('#modalDescription').html(event.extendedProps.description);

            $('#eventUrl').attr('href', event.url);
            $('#removeBtn').attr('data-id', event.id);
            $('#calendarModal').modal();
            */
            return false;
        },
        events: function (dates, callback) {
            let start = moment(dates.start).format('YYYY-MM-DD')
            let end = moment(dates.end).format('YYYY-MM-DD')
            $.ajax({
                url: '/CalenderEvent/GetCalendar?start=' + start + "&end=" + end,
                type: "GET",
                dataType: "JSON",
                success: function (result) {
                    var events = [];
                    var response = result.data;

                    $.each(response.data, function (i, item) {

                        events.push(
                            {
                                id: item.id,
                                title: item.title,
                                start: moment(item.start).local().format('YYYY-MM-DD HH:mm:ss'),
                                end: moment(item.end).local().format('YYYY-MM-DD HH:mm:ss'),
                                backgroundColor: item.backgroundColor,
                                borderColor: item.borderColor,
                                url: item.url === null ? "" : item.url,
                                description: item.description
                            });
                    });
                    console.log(events)

                    callback(events);
                }
            });
        },

        eventRender: function (info) {
            //console.log(info)
        },
    });
    calendar.render();

    
    $('#removeBtn').click(function () {
        $('#calendarModal').modal('hide');
        let id = $(this).data('id')
        removeEvent(id);
    })
}