
function CreateAssignment() {
    var EnrollCourseAssignmentFrm = $('#EnrollmentAssignments-Create');
    var formDataArray = EnrollCourseAssignmentFrm.serializeArray();
    formDataArray.push({ name: 'Description1', value: tinymce.get('Description').getContent() });
    var serializedData = $.param(formDataArray);
    if (EnrollCourseAssignmentFrm.valid()) {
        if ($("#PublishDate").val() >= $("#PublishEndDate").val())
            toastr.warning(AddMassegeErrorInvalidDates);
        else {
            ShowSpinner();
            $.ajax({
                type: EnrollCourseAssignmentFrm.attr('method'),
                url: EnrollCourseAssignmentFrm.attr('action'),
                data: serializedData,
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
                        GetAssignmentData();
                    }
                },
                error: function (data) {
                    HideSpinner();
                    toastr.error(addMassegeError);
                },
            });
        }
    }
}

$('#CreateEnrollCourseAssignment').on('hidden.bs.modal', function () {
    $('#CreateEnrollCourseAssignmentDiv').empty();
})


$('#EditEnrollCourseAssignment').on('hidden.bs.modal', function () {
    $('#EditEnrollCourseAssignmentDiv').empty();
})


function EditAssignment() {
    debugger;
    var EnrollCourseAssignmentEditFrm = $('#EnrollmentAssignments-Edit');
    var formDataArray = EnrollCourseAssignmentEditFrm.serializeArray();
    formDataArray.push({ name: 'Description1', value: tinymce.get('Description').getContent() });
    var serializedData = $.param(formDataArray);
    if (EnrollCourseAssignmentEditFrm.valid()) {
        if ($("#PublishDate").val() >= $("#PublishEndDate").val())
            toastr.warning(EditMassegeErrorInvalidDates);
        else {
            ShowSpinner();
            $.ajax({
                type: EnrollCourseAssignmentEditFrm.attr('method'),
                url: EnrollCourseAssignmentEditFrm.attr('action'),
                data: serializedData,
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
                        GetAssignmentData();
                    }
                },
                error: function (data) {
                    HideSpinner();
                    toastr.error(addMassegeError);
                },
            });
        }
    }
}

function onDdlChanged(langId) {
    ShowEditEnrollCourseAssignment($("#EnrollmentAssignments-Edit #Id").val(), langId.value);
}

function ConfirmDeleteAssignment(id) {
    ShowSpinner();
    $.ajax({
        type: "POST",
        url: "/ControlPanel/EnrollmentAssignments/DeleteEnrollCourseAssignment/",
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
                    GetAssignmentData()
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
        url: "/ControlPanel/EnrollmentAssignments/ShowDelete/",
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
        url: "/ControlPanel/EnrollmentAssignments/ShowRegenerateExamQuestions/",
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
        url: "/ControlPanel/EnrollmentAssignments/ShowQuestions/",
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
        url: "/ControlPanel/EnrollmentAssignments/ShowAssignmentQuestions/",
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
        url: "/ControlPanel/EnrollmentAssignments/ShowEditAssignmentQuestion/",
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
        url: "/ControlPanel/EnrollmentAssignments/ShowDetails/",
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
        url: "/ControlPanel/EnrollmentAssignments/ShowCreate/",
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
        url: "/ControlPanel/EnrollmentAssignments/ShowEdit/",
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
function GetAssigmentData(data) {
    $('#GetData').html(data);
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
        url: "/ControlPanel/EnrollmentAssignments/DeleteQuestion/",
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
        url: "/ControlPanel/EnrollmentAssignments/GetStudentAssigments/",
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
        url: "/ControlPanel/EnrollmentAssignments/GetAssignmentQuestions/",
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