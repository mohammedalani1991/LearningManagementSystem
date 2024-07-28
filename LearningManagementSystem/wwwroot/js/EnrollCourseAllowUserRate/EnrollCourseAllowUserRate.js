

function CreateAllowUserRate() {
    var EnrollCourseAllowUserRateFrm = $('#EnrollCourseAllowUserRate-Create');
    if (EnrollCourseAllowUserRateFrm.valid()) {
        ShowSpinner();
        $.ajax({
            type: EnrollCourseAllowUserRateFrm.attr('method'),
            url: EnrollCourseAllowUserRateFrm.attr('action'),
            data: EnrollCourseAllowUserRateFrm.serialize(),
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                if (data == "Fail") {
                    HideSpinner();
                    toastr.warning(AddMassegeErrorInvalidDates);
                } else {
                    toastr.success(addMassege);
                    $('#CreateEnrollCourseAllowUserRate').modal("hide");
                    HideSpinner();
                    GetAllowUserRatesData();
                }
            },
            error: function (data) {
                HideSpinner();
                toastr.error(addMassegeError);
            },
        });
    }
}

function EditAllowUserRate() {
    var EnrollCourseAllowUserRateEditFrm = $('#EnrollCourseAllowUserRate-Edit');
    EnrollCourseAllowUserRateEditFrm.submit(function (e) {
        e.preventDefault();
        ShowSpinner();
        $.ajax({
            type: EnrollCourseAllowUserRateEditFrm.attr('method'),
            url: EnrollCourseAllowUserRateEditFrm.attr('action'),
            data: EnrollCourseAllowUserRateEditFrm.serialize(),
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                if (data == "Fail") {
                    HideSpinner();
                    toastr.warning(EditMassegeErrorInvalidDates);
                } else {
                    toastr.success(editMassege);
                    $('#EditEnrollCourseAllowUserRate').modal("hide");
                    HideSpinner();
                    Get();
                }
            },
            error: function (data) {
                HideSpinner();
                toastr.error(addMassegeError);
            },
        });    
    });
}

function ConfirmDeleteAllowUserRate(id) {
    ShowSpinner();
    $.ajax({
        type: "POST",
        url: "/ControlPanel/EnrollCourseAllowUserRate/DeleteEnrollCourseAllowUserRate/",
        data: { id: id },
        success: function (data) {
            if (data !== null) {
                if (data == "Fail") {
                    HideSpinner();
                    toastr.error(deleteMassegeError);
                } else {
                    toastr.success(deleteMassege);
                    HideSpinner();
                    $('#DeleteEnrollCourseAllowUserRate').modal("hide");
                    GetAllowUserRatesData();
                }
            }
        }
    }).fail(function (jqXHR, textStatus, error) {
        toastr.error(deleteMassegeError);
    });
}
function ShowDeleteEnrollCourseAllowUserRate(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/EnrollCourseAllowUserRate/ShowDelete/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#DeleteEnrollCourseAllowUserRate .modal-content').html(data);
            $('#DeleteEnrollCourseAllowUserRate').modal("show");

        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function ShowDetailsEnrollCourseAllowUserRate(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Trainer/EnrollCourseAllowUserRate/ShowDetails/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#DetailsEnrollCourseAllowUserRate .modal-content').html(data);
            $('#DetailsEnrollCourseAllowUserRate').modal("show");

        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowAddEnrollCourseAllowUserRate(EnrollTeacherCoursId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/EnrollCourseAllowUserRate/ShowCreate/",
        data: { enrollTeacherCoursId: EnrollTeacherCoursId },
        success: function (data) {
            $('#EditEnrollCourseAllowUserRateDiv').empty();
            HideSpinner();
            $('#CreateEnrollCourseAllowUserRate .modal-content').html(data);
            $('#CreateEnrollCourseAllowUserRate').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}
function ShowEditEnrollCourseAllowUserRate(Id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Trainer/EnrollCourseAllowUserRate/ShowEdit/",
        data: { Id: Id, languageId: langId },
        success: function (data) {
            $('#CreateEnrollCourseDiv').empty();
            HideSpinner();
            $('#EditEnrollCourseAllowUserRate .modal-content').html(data);
            $('#EditEnrollCourseAllowUserRate').modal("show");

        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function GetData(data) {
    $('#UserProfile_Exams').html(data);
}
