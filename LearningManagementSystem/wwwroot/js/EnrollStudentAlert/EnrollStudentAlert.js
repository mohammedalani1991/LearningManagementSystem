

function CreateEnrollStudentAlert() {
    var EnrollStudentAlertFrm = $('#EnrollStudentAlert-Create');
    if (EnrollStudentAlertFrm.valid()) {
        ShowSpinner();
        $.ajax({
            type: EnrollStudentAlertFrm.attr('method'),
            url: EnrollStudentAlertFrm.attr('action'),
            data: EnrollStudentAlertFrm.serialize(),
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                if (data == "Fail") {
                    HideSpinner();
                    toastr.warning(AddMassegeErrorInvalidDates);
                } else {
                    toastr.success(addMassege);
                    $('#CreateEnrollStudentAlert').modal("hide");
                    HideSpinner();
                    GetEnrollStudentAlertsData();
                }
            },
            error: function (data) {
                HideSpinner();
                toastr.error(addMassegeError);
            },
        });
    }
}

function EditEnrollStudentAlert() {
    var EnrollStudentAlertEditFrm = $('#EnrollStudentAlert-Edit');
    if (EnrollStudentAlertEditFrm.valid()) {
        ShowSpinner();
        $.ajax({
            type: EnrollStudentAlertEditFrm.attr('method'),
            url: EnrollStudentAlertEditFrm.attr('action'),
            data: EnrollStudentAlertEditFrm.serialize(),
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                if (data == "Fail") {
                    HideSpinner();
                    toastr.warning(EditMassegeErrorInvalidDates);
                } else {
                    toastr.success(editMassege);
                    $('#EditEnrollStudentAlert').modal("hide");
                    HideSpinner();
                    GetEnrollStudentAlertsData();
                }
            },
            error: function (data) {
                HideSpinner();
                toastr.error(addMassegeError);
            },
        });    
    }
}

function ConfirmDeleteEnrollStudentAlert(id) {
    ShowSpinner();
    $.ajax({
        type: "POST",
        url: "/ControlPanel/EnrollStudentAlert/DeleteEnrollStudentAlert/",
        data: { id: id },
        success: function (data) {
            if (data !== null) {
                if (data == "Fail") {
                    HideSpinner();
                    toastr.error(deleteMassegeError);
                } else {
                    toastr.success(deleteMassege);
                    HideSpinner();
                    $('#DeleteEnrollStudentAlert').modal("hide");
                    GetEnrollStudentAlertsData();
                }
            }
        }
    }).fail(function (jqXHR, textStatus, error) {
        toastr.error(deleteMassegeError);
    });
}
function ShowDeleteEnrollStudentAlert(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/EnrollStudentAlert/ShowDelete/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#DeleteEnrollStudentAlert .modal-content').html(data);
            $('#DeleteEnrollStudentAlert').modal("show");

        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function ShowDetailsEnrollStudentAlert(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/EnrollStudentAlert/ShowDetails/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#DetailsEnrollStudentAlert .modal-content').html(data);
            $('#DetailsEnrollStudentAlert').modal("show");

        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowAddEnrollStudentAlert(EnrollTeacherCoursId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/EnrollStudentAlert/ShowCreate/",
        data: { enrollTeacherCoursId: EnrollTeacherCoursId },
        success: function (data) {
            $('#EditEnrollStudentAlertDiv').empty();
            HideSpinner();
            $('#CreateEnrollStudentAlert .modal-content').html(data);
            $('#CreateEnrollStudentAlert').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}
function ShowEditEnrollStudentAlert(Id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/EnrollStudentAlert/ShowEdit/",
        data: { Id: Id, languageId: langId },
        success: function (data) {
            $('#CreateEnrollCourseDiv').empty();
            HideSpinner();
            $('#EditEnrollStudentAlert .modal-content').html(data);
            $('#EditEnrollStudentAlert').modal("show");

        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function GetData(data) {
    $('#UserProfile_Exams').html(data);
}
