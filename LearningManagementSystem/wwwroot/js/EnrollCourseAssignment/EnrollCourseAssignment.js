function Get() {
    var URL = "/Trainer/EnrollCourseAssignment/GetData";
    if ($("#EnrollCourseAssignmentCurrentUrl").val())
        URL = $("#EnrollCourseAssignmentCurrentUrl").val();

    ShowSpinner();
    $.ajax({
        type: "GET",
        url: URL,
        dataType: 'html',
        success: function (data) {
            HideSpinner();
            $('#UserProfile_Assignments').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

var EnrollCourseAssignmentFrm = $('#EnrollCourseAssignment-Create');
EnrollCourseAssignmentFrm.submit(function (e) {
    e.preventDefault();
    if ($("#PublishDate").val() >= $("#PublishEndDate").val())
        toastr.warning(AddMassegeErrorInvalidDates);
    else {
        ShowSpinner();
        $.ajax({
            type: EnrollCourseAssignmentFrm.attr('method'),
            url: EnrollCourseAssignmentFrm.attr('action'),
            data: EnrollCourseAssignmentFrm.serialize(),
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                if (data == "Fail") {
                    HideSpinner();
                    toastr.warning(AddMassegeErrorInvalidDates);
                } else {
                    toastr.success(addMassege);
                    $('#CreateEnrollCourseAssignment').modal("hide");
                    HideSpinner();
                    Get();
                }
            },
            error: function (data) {
                HideSpinner();
                toastr.error(addMassegeError);
            },
        });
    }
});

var EnrollCourseAssignmentEditFrm = $('#EnrollCourseAssignment-Edit');
EnrollCourseAssignmentEditFrm.submit(function (e) {
    e.preventDefault();
    if ($("#PublishDate").val() >= $("#PublishEndDate").val())
        toastr.warning(EditMassegeErrorInvalidDates);
    else {
        ShowSpinner();
        $.ajax({
            type: EnrollCourseAssignmentEditFrm.attr('method'),
            url: EnrollCourseAssignmentEditFrm.attr('action'),
            data: EnrollCourseAssignmentEditFrm.serialize(),
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                if (data == "Fail") {
                    HideSpinner();
                    toastr.warning(EditMassegeErrorInvalidDates);
                } else {
                    toastr.success(editMassege);
                    $('#EditEnrollCourseAssignment').modal("hide");
                    HideSpinner();
                    Get();
                }
            },
            error: function (data) {
                HideSpinner();
                toastr.error(addMassegeError);
            },
        });
    }
});

function onDdlChanged(langId) {
    ShowEditEnrollCourseAssignment($("#EnrollCourseAssignment-Edit #Id").val(), langId.value);
}

function ConfirmDelete(id) {
    ShowSpinner();
    $.ajax({
        type: "POST",
        url: "/Trainer/EnrollCourseAssignment/DeleteEnrollCourseAssignment/",
        data: { id: id },
        success: function (data) {
            if (data !== null) {
                if (data == "Fail") {
                    HideSpinner();
                    toastr.error(deleteMassegeError);
                } else {
                    toastr.success(deleteMassege);
                    HideSpinner();
                    $('#DeleteEnrollCourseAssignment').modal("hide");
                    Get()
                }
            }
        }
    }).fail(function (jqXHR, textStatus, error) {
        toastr.error(deleteMassegeError);
    });
}
function ShowDeleteEnrollCourseAssignment(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Trainer/EnrollCourseAssignment/ShowDelete/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#DeleteEnrollCourseAssignment .modal-content').html(data);
            $('#DeleteEnrollCourseAssignment').modal("show");

        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function ShowRegenerateExamQuestions(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Trainer/EnrollCourseAssignment/ShowRegenerateExamQuestions/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#ShowRegenerateExamQuestions .modal-content').html(data);
            $('#ShowRegenerateExamQuestions').modal("show");

        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowQuestions(EnrollCourseAssigmentId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Trainer/EnrollCourseAssignment/ShowQuestions/",
        data: { EnrollCourseAssigmentId: EnrollCourseAssigmentId },
        success: function (data) {
            HideSpinner();
            $('#ShowAssignmentQuestions .modal-content').html(data);
            $('#ShowAssignmentQuestions').modal("show");

        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowAssignmentQuestions(EnrollCourseAssigmentId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Trainer/EnrollCourseAssignment/ShowAssignmentQuestions/",
        data: { EnrollCourseAssigmentId: EnrollCourseAssigmentId },
        success: function (data) {
            HideSpinner();
            $('#Questions').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowEditAssignmentQuestions(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Trainer/EnrollCourseAssignment/ShowEditAssignmentQuestion/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#Questions').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function CleanQuestion() {
    $('#Questions').empty();
}

function ShowDetailsEnrollCourseAssignment(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Trainer/EnrollCourseAssignment/ShowDetails/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#DetailsEnrollCourseAssignment .modal-content').html(data);
            $('#DetailsEnrollCourseAssignment').modal("show");

        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowAddEnrollCourseAssignment(EnrollTeacherCoursId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Trainer/EnrollCourseAssignment/ShowCreate/",
        data: { EnrollTeacherCoursId: EnrollTeacherCoursId },
        success: function (data) {
            $('#EditEnrollCourseAssignmentDiv').empty();
            HideSpinner();
            $('#CreateEnrollCourseAssignment .modal-content').html(data);
            $('#CreateEnrollCourseAssignment').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}
function ShowEditEnrollCourseAssignment(Id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Trainer/EnrollCourseAssignment/ShowEdit/",
        data: { Id: Id, languageId: langId },
        success: function (data) {
            $('#CreateEnrollCourseDiv').empty();
            HideSpinner();
            $('#EditEnrollCourseAssignment .modal-content').html(data);
            $('#EditEnrollCourseAssignment').modal("show");

        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function BtnSearchRestPageNumber() {
    $('#Page').val("0");
}
function GetData(data) {
    $('#UserProfile_Exams').html(data);
}

function ShowDeleteQuestion(id, enrollCourseAssigmentId) {
    if (confirm("Delete Question!")) {
        DeleteQuestion(id, enrollCourseAssigmentId);
    } else {
    }
}

function DeleteQuestion(id, enrollCourseAssigmentId) {
    $.ajax({
        type: "POST",
        url: "/Trainer/EnrollCourseAssignment/DeleteQuestion/",
        data: { id: id },
        success: function (data) {
            toastr.success(deleteMassege);
            ShowQuestions(enrollCourseAssigmentId);
        }
    }).fail(function (jqXHR, textStatus, error) {
        toastr.error(deleteMassegeError);
    });
}

function ShowSubmittedAssignments(id) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Trainer/EnrollCourseAssignment/GetStudentAssigments/",
        data: { assigmentId: id },
        success: function (data) {
            HideSpinner();
            $('#ShowStudentsAssignment .modal-content').html(data);
            $('#ShowStudentsAssignment').modal("show");

        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function GetData1(data) {
    $('#ShowStudentsAssignment .modal-content').html(data);
}

var assigId;

function ShowAssignmentQuestionsDeitelts(id) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Trainer/EnrollCourseAssignment/GetAssignmentQuestions/",
        data: { id: id },
        success: function (data) {
            HideSpinner();
            assigId = $("#assigmentId").val();
            $('#ShowStudentsAssignment .modal-content').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function CloseAnswers() {
    ShowSubmittedAssignments(assigId)
}