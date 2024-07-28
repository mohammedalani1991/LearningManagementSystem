function Get() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Operations/Attendances/GetData",
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

function ShowAddAttendance(id) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Operations/Attendances/ShowAddAttendance/",
        data: { contactId: id },
        success: function (data) {
            HideSpinner();
            $('#CreateAttendance .modal-content').html(data);
            $('#CreateAttendance').modal("show");
        }
    }
    ).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}
function ShowEditTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Operations/Attendances/ShowTable/",
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

function ShowEditAttendances(id, langId) {
    $.ajax({
        type: "GET",
        url: "/Operations/Attendances/ShowEdit/",
        data: { id: id, languageId: langId },
        success: function (data) {
            $('#EditAttendance .modal-content').html(data);
            $('#EditAttendance').modal("show");
        }
    }
    );
}

var AttendanceCreateFrm = $('#Attendance-Create');
AttendanceCreateFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: AttendanceCreateFrm.attr('method'),
        url: AttendanceCreateFrm.attr('action'),
        data: AttendanceCreateFrm.serialize(),
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            toastr.success(addMassege);
            $('#CreateAttendance').modal("hide");
            HideSpinner();
            Get();
        },
        error: function (data) {
            HideSpinner();
            toastr.error(addMassegeError);
            console.log(data);
        },
    });
});

var AttendanceEditFrm = $('#Attendance-Edit');
AttendanceEditFrm.submit(function (e) {    
    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: AttendanceEditFrm.attr('method'),
        url: AttendanceEditFrm.attr('action'),
        data: AttendanceEditFrm.serialize(),
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            toastr.success(editMassege);
            $('#EditAttendance').modal("hide");
            HideSpinner();
            Get();
        },
        error: function (data) {
            HideSpinner();
            toastr.error(editMassegeError);
            console.log(data);
        },
    });
});


function CloseAddAttendance() {
    $('#CreateAttendance').modal("hide");
}

function CloseEditAttendance() {
    $('#EditAttendance').modal("hide");
}


function GetData(data) {
    $('#main').html(data);
}