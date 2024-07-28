function Get() {
    var URL = "/ControlPanel/Calendar/GetData";
    if ($("#CmsCalendarCurrentUrl").val())
        URL = $("#CmsCalendarCurrentUrl").val();

    ShowSpinner();
    $.ajax({
        type: "GET",
        url: URL,
        dataType: 'html',
        success: function (data) {
            HideSpinner();
            $('#main').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

Get();

function ShowAddCalendar() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Calendar/ShowCreate/",
        success: function (data) {
            HideSpinner();
            $("#EditCalendarDiv").empty();
            $('#CreateCalendar .modal-content').html(data);
            $('#CreateCalendar').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    }); 
}

function ShowEditTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Calendar/ShowTable/",
        success: function (data) {
            HideSpinner(); 
            $('#EditTable .modal-content').html(data);
            $(".td").map(function () {
                table.forEach(element => {
                    if ($(this).html().trim() == element) {
                        $(this).parent().find("input[type='checkbox']").prop('checked', true)
                    }
                }
                );
            })
            $('#EditTable').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

var CalendarFrm = $('#Calendar-Create');
CalendarFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: CalendarFrm.attr('method'),
        url: CalendarFrm.attr('action'),
        data: CalendarFrm.serialize(),
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            if (data == "AddMassegeErrorInvalidDates") {
                toastr.error(AddMassegeErrorInvalidDates);
                HideSpinner();
            } else {
                toastr.success(addMassege);
                $('#CreateCalendar').modal("hide");
                HideSpinner();
                Get()
            }
        },
        error: function (data) {
            HideSpinner();
            toastr.error(addMassegeError);
        },
    });
});

function ShowDetailsCalendar(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Calendar/ShowDetails/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#DetailsCalendar .modal-content').html(data);
            $('#DetailsCalendar').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function ShowDeleteCalendar(id, langId,page) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Calendar/ShowDelete/",
        data: { id: id, languageId: langId, page: page},
        success: function (data) {
            HideSpinner();
            $('#DeleteCalendar .modal-content').html(data);
            $('#DeleteCalendar').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ConfirmDelete(id,page) {
    ShowSpinner();
    $.post("/ControlPanel/Calendar/DeleteCalendar/",
        { id: id, page: page},
        function (data) {
            if (data !== null) {
                toastr.success(deleteMassege);
                HideSpinner();
                $('#DeleteCalendar').modal("hide");
                Get()
            } else {
                HideSpinner();
                toastr.error(deleteMassegeError);
            }
        });
}

function onDdlChanged(langId) {
    var page = $('#Page').val();
    ShowEditCalendar($('#CalendarId').val(), langId.value,page);
}


var CalendarEditFrm = $('#Calendar-Edit');
CalendarEditFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: CalendarEditFrm.attr('method'),
        url: CalendarEditFrm.attr('action'),
        data: CalendarEditFrm.serialize(),
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            if (data == "EditMassegeErrorInvalidDates") {
                toastr.error(EditMassegeErrorInvalidDates);
                HideSpinner();
            } else {
                toastr.success(editMassege);
                $('#EditCalendar').modal("hide");
                HideSpinner();
                Get();
            }
  
        },
        error: function (data) {
            HideSpinner();
            toastr.error(editMassegeError);
        },
    });
});

function ShowEditCalendar(id, langId, page) {
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Calendar/ShowEdit/",
        data: { id: id, languageId: langId, page:page },
        success: function (data) {
            $("#CreateCalendarDiv").empty();
            $('#EditCalendar .modal-content').html(data);
            $('#EditCalendar').modal("show");
        }

    });
}
function BtnSearchRestPageNumber() {
    $('#page').val("0");
}
function GetData(data) {
    $('#main').html(data);
}
