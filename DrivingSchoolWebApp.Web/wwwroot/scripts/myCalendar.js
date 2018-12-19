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
                            eventID: v.Id,
                            title: v.Subject,
                            description: v.Description,
                            start: moment(v.StartTime),
                            end: v.EndTime != null ? moment(v.EndTime) : null,
                            color: v.ThemeColor,
                            allDay: v.IsFullDay
                        });
                    });

                generateCalender(events);
            },
            error: function (error) {
                alert("failed");
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
            eventClick: function(calEvent, jsEvent, view) {
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
            select: function(start, end) {
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
            eventDrop: function(event) {
                var data = {
                    Id: event.eventID,
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

    $("#btnEdit").click(function() {
        //Open modal dialog for edit event
        openAddEditForm();
    });
    $("#btnDelete").click(function() {
        if (selectedEvent != null && confirm("Are you sure?")) {
            $.ajax({
                type: "POST",
                url: "/Lessons/Delete",
                data: { 'eventID': selectedEvent.eventID },
                success: function(data) {
                    //if (data.status) {
                    //Refresh the calender
                    fetchEventAndRenderCalendar();
                    $("#myModal").modal("hide");
                    //}
                },
                error: function() {
                    alert("Failed");
                }
            });
        }
    });

    $("#dtp1,#dtp2").datetimepicker({
        format: "DD/MM/YYYY HH:mm A"
    });

    $("#chkIsFullDay").change(function () {
        if ($(this).is(":checked")) {
            $("#divEndDate").hide();
        }
        else {
            $("#divEndDate").show();
        }
    });

    function openAddEditForm() {
        if (selectedEvent != null) {
            $("#hdEventID").val(selectedEvent.eventID);
            $("#txtSubject").val(selectedEvent.title);
            $("#txtStart").val(selectedEvent.start.format("DD/MM/YYYY HH:mm A"));
            $("#chkIsFullDay").prop("checked", selectedEvent.allDay || false);
            $("#chkIsFullDay").change();
            $("#txtEnd").val(selectedEvent.end != null ? selectedEvent.end.format("DD/MM/YYYY HH:mm A") : "");
            $("#txtDescription").val(selectedEvent.description);
            $("#ddThemeColor").val(selectedEvent.color);
        }
        $("#myModal").modal("hide");
        $("#myModalSave").modal();
    }

    $("#btnSave").click(function () {
        //Validation/
        //if ($('#txtSubject').val().trim() == "") {
        //    alert('Subject required');
        //    return;
        //}
        if ($("#txtStart").val().trim() == "") {
            alert("Start date required");
            return;
        }
        if ($("#chkIsFullDay").is(":checked") == false && $("#txtEnd").val().trim() == "") {
            alert("End date required");
            return;
        } else {
            var startDate = moment($("#txtStart").val(), "DD/MM/YYYY HH:mm A").toDate();
            var endDate = moment($("#txtEnd").val(), "DD/MM/YYYY HH:mm A").toDate();
            if (startDate > endDate) {
                alert("Invalid end date");
                return;
            }
        }

        var dataInput = {
            Id: $("#hdEventID").val(),
            Subject: $("#txtSubject").val().trim(),
            StartTime: $("#txtStart").val().trim(),
            EndTime: $("#chkIsFullDay").is(":checked") ? null : $("#txtEnd").val().trim(),
            Description: $("#txtDescription").val(),
            ThemeColor: $("#ddThemeColor").val(),
            IsFullDay: $("#chkIsFullDay").is(":checked")
        }
        saveEvent(dataInput);
        // call function for submit data to the server 
    });

    function saveEvent(inputData) {
        $.ajax({
            type: "POST",
            url: "/Lessons/Create",
            data: inputData,
            success: function (data) {
               
                    fetchEventAndRenderCalendar();
                    $("#myModalSave").modal("hide");
                
            },
            error: function () {
                alert("Failed");
            }
        });
    }
})
