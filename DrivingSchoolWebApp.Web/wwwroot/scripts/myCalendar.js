$(document).ready(function () {
    var events = [];
    var selectedEvent = null;
    fetchEventAndRenderCalendar();

    function fetchEventAndRenderCalendar() {
        events = [];
        $.ajax({
            type: "GET",
            url: "/Lessons/GetMyEvents",
            success: function (data) {
                $.each(data,
                    function (i, v) {
                        events.push({
                            eventID: v.id,
                            title: v.subject,
                            description: v.description,
                            start: moment(v.startTime),
                            end: v.endTime != null ? moment(v.endTime) : null,
                            color: v.themeColor,
                            allDay: v.isFullDay
                        });
                    });

                generateCalender(events);
            },
            error: function (error) {
                alert("failed to get events "+ error.message);
            }
        });
    }

    function generateCalender(events) {
        $("#calender").fullCalendar("destroy");
        $("#calender").fullCalendar({
            contentHeight: 400,
            defaultDate: new Date(),
            timeFormat: "h(:mm)a",
            header: {
                left: "prev,next today",
                center: "title",
                right: "month,basicWeek,basicDay,agenda"
            },
            eventLimit: true,
            eventColor: "#378006",
            events: events,
            eventClick: function (calEvent, jsEvent, view) {
                selectedEvent = calEvent;
                $("#myModal #eventTitle").text(calEvent.title);
                var $description = $("<div/>");
                $description.append($("<p/>").html("<b>Start:</b>" + calEvent.start.format("DD-MMM-YYYY HH:mm a")));
                if (calEvent.end != null) {
                    $description.append($("<p/>").html("<b>End:</b>" + calEvent.end.format("DD-MMM-YYYY HH:mm a")));
                }
                $description.append($("<p/>").html("<b>Description:</b>" + calEvent.description));
                $("#myModal #pDetails").empty().html($description);

                $("#myModal").modal();
            },
           
        });
    }

   
})
