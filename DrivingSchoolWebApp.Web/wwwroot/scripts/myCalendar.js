$(document).ready(function () {
    var events = [];
    var selectedEvent = null;
    fetchEventAndRenderCalendar();

    function fetchEventAndRenderCalendar() {
        events = [];
        $.ajax({
            type: "GET",
            url: "/api/Schedule/GetMyEvents",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
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
                data: { 'eventID': selectedEvent.eventID },
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

    $("#IsFullDay").change(function () {
        if ($(this).is(":checked")) {
            $("#divEndDate").hide();
        }
        else {
            $("#divEndDate").show();
        }
    });

    function openAddEditForm() {
        if (selectedEvent != null) {
            $("#Id").val(selectedEvent.eventID);
            $("#Subject").val(selectedEvent.title);
            $("#StartTime").val(selectedEvent.start.format("DD/MM/YYYY HH:mm A"));
            $("#IsFullDay").prop("checked", selectedEvent.allDay || false);
            $("#IsFullDay").change();
            $("#EndTime").val(selectedEvent.end != null ? selectedEvent.end.format("DD/MM/YYYY HH:mm A") : "");
            $("#Description").val(selectedEvent.description);
            $("#ThemeColor").val(selectedEvent.color);
        }
        $("#myModal").modal("hide");
        $("#myModalSave").modal();
    }


    $("#btnSave").click(function(e) {
      
        ////Validation/
        //if ($("#txtSubject").val().trim() == "") {
        //    alert("Subject required");
        //    return;
        //}
        //if ($("#txtStart").val().trim() == "") {
        //    alert("Start date required");
        //    return;
        //}
        //if ($("#chkIsFullDay").is(":checked") == false && $("#txtEnd").val().trim() == "") {
        //    alert("End date required");
        //    return;
        //} else {
        //    var startDate = moment($("#txtStart").val(), "DD/MM/YYYY HH:mm A").toDate();
        //    var endDate = moment($("#txtEnd").val(), "DD/MM/YYYY HH:mm A").toDate();
        //    if (startDate > endDate) {
        //        alert("Invalid end date");
        //        return;
        //    }
        //}

        var dataInput = {
            Id: $("#Id").val(),
            CustomerId:$("#CustomerId").val(),
            CourseId:$("#CourseId").val(),
            Subject: $("#Subject").val().trim(),
            StartTime: $("#StartTime").val().trim(),
            EndTime: $("#IsFullDay").is(":checked") ? null : $("#EndTime").val().trim(),
            Description: $("#Description").val(),
            ThemeColor: $("#ThemeColor").val(),
            IsFullDay: $("#IsFullDay").is(":checked")
        }
        saveEvent(dataInput);
        // call function for submit data to the server 
    });

    function saveEvent(inputData) {
        var token = $('input[name="`__RequestVerificationToken`"]').val();
        var dataWithAntiforgeryToken = $.extend(inputData, { '__RequestVerificationToken': token });
        $.ajax({
            type: "POST",
            url: "/api/Schedule/SaveAjax",
            data: dataWithAntiforgeryToken,
            success: function (data) {

                    fetchEventAndRenderCalendar();
                    $("#myModalSave").modal("hide");

            },
            error: function (e) {
                alert(e.message);
            }
        });
    }
})
