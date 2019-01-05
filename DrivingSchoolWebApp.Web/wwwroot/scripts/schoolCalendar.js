$(document).ready(function () {
    var events = [];
    var selectedEvent = null;
    var trainerId = $("#trainerId").val();
    fetchEventAndRenderCalendar();

    function fetchEventAndRenderCalendar() {
        events = [];
        $.ajax({
            type: "GET",
            url: "/Lessons/GetSchoolEvents",
            data: { trainerId: trainerId },
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
                alert("failed to get events " + error.message);
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
            selectable: true,
            select: function (start, end) {
                selectedEvent = {
                    eventID: 0,
                    title: "",
                    description: "",
                    start: start,
                    end: end,
                    allDay: false,
                    color: ""
                };
                openAddEditForm();
                $("#calendar").fullCalendar("unselect");
            },
            editable: true,
            eventDrop: function (event) {
                var data = {
                    Id: event.eventID,
                    Subject: event.title,
                    StartTime: event.start.format("DD/MM/YYYY HH:mm A"),
                    EndTime: event.end != null ? event.end.format("DD/MM/YYYY HH:mm A") : null,
                    Description: event.description,
                    ThemeColor: event.color,
                    IsFullDay: event.allDay
                };
                saveEvent(data);
            }
        });
    }

    $("#btnEdit").click(function () {
        //Open modal dialog for edit event
        openAddEditForm();
    });
    $("#btnDelete").click(function () {
        if (selectedEvent != null && confirm("Are you sure?")) {
            $.ajax({
                type: "POST",
                url: "/Lessons/Delete",
                data: { 'id': selectedEvent.eventID },
                success: function (data) {
                    //if (data.status) {
                    //Refresh the calender
                    fetchEventAndRenderCalendar();
                    $("#myModal").modal("hide");
                    //}
                },
                error: function () {
                    alert("Failed");
                }
            });
        }
    });

    $("#dtp1,#dtp2").datetimepicker({
        format: "DD/MM/YYYY HH:mm A"
    });

    $("#fcIsFullDay").change(function () {
        if ($(this).is(":checked")) {
            $("#divEndDate").hide();
        }
        else {
            $("#divEndDate").show();
        }
    });

    function openAddEditForm() {
        if (selectedEvent != null) {
            $("#fcId").val(selectedEvent.eventID);
            $("#fcSubject").val(selectedEvent.title);
            $("#fcStartTime").val(selectedEvent.start.format("DD/MM/YYYY HH:mm A"));
            $("#fcIsFullDay").prop("checked", selectedEvent.allDay || false);
            $("#fcIsFullDay").change();
            $("#fcEndTime").val(selectedEvent.end != null ? selectedEvent.end.format("DD/MM/YYYY HH:mm A") : "");
            $("#fcDescription").val(selectedEvent.description);
            $("#fcThemeColor").val(selectedEvent.color);
        }
        $("#myModal").modal("hide");
        $("#myModalSave").modal();
    }
    ////todo add validation
})
