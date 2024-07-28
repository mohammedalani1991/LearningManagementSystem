function CreateEnrollmentExams() {
    var EnrollCourseExamFrm = $('#EnrollmentExams-Create');
    var formDataArray = EnrollCourseExamFrm.serializeArray();
    formDataArray.push({ name: 'Description1', value: tinymce.get('Description').getContent() });
    var serializedData = $.param(formDataArray);
    if (EnrollCourseExamFrm.valid()) {
        ShowSpinner();
        $.ajax({
            type: EnrollCourseExamFrm.attr('method'),
            url: EnrollCourseExamFrm.attr('action'),
            data: serializedData,
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                if (data == "Fail") {
                    HideSpinner();
                    toastr.error(addMassegeError);
                } else if (data == "FailExamTemplateId") {
                    HideSpinner();
                    toastr.error(FailExamTemplateId);
                } else if (data == "FailEnrollLectureId") {
                    HideSpinner();
                    toastr.error(FailEnrollLectureId);
                } else {
                    toastr.success(addMassege);
                    $('#CreateEnrollCourseExam').modal("hide");
                    HideSpinner();
                    GetExamData();
                }
            },
            error: function (data) {
                HideSpinner();
                toastr.error(addMassegeError);
            },
        });
    }
}

function EditEnrollmentExam() {
    var EnrollCourseExamEditFrm = $('#EnrollmentExams-Edit');
    var formDataArray = EnrollCourseExamEditFrm.serializeArray();
    formDataArray.push({ name: 'Description1', value: tinymce.get('Description').getContent() });
    var serializedData = $.param(formDataArray);
    console.log(serializedData)
    if (EnrollCourseExamEditFrm.valid()) {
        ShowSpinner();
        $.ajax({
            type: EnrollCourseExamEditFrm.attr('method'),
            url: EnrollCourseExamEditFrm.attr('action'),
            data: serializedData,
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                if (data == "Fail") {
                    HideSpinner();
                    toastr.error(editMassegeErrorQuestions);
                } else if (data == "FailExamTemplateId") {
                    HideSpinner();
                    toastr.error(FailExamTemplateId);
                } else if (data == "FailEnrollLectureId") {
                    HideSpinner();
                    toastr.error(FailEnrollLectureId);
                }
                else {
                    toastr.success(editMassege);
                    $('#EditEnrollCourseExam').modal("hide");
                    HideSpinner();
                    GetExamData();
                }
            },
            error: function (data) {
                HideSpinner();
                toastr.error(addMassegeError);
            },
        });
    }
}

function onDdlChangedForCreate(langId) {
    ShowAddEnrollCourseExam($("#EnrollmentExams-Create #EnrollTeacherCourseId").val(), langId.value);
}
function onDdlChanged(langId) {
    ShowEditCourseExam($("#EnrollmentExams-Edit #Id").val(), langId.value);
}
function ConfirmDeleteExam(id) {
    ShowSpinner();
    $.post("/ControlPanel/EnrollmentExams/DeleteEnrollCourseExam/",
        { id: id },
        function (data) {
            if (data !== null) {
                if (data == "Fail") {
                    HideSpinner();
                    toastr.error(deleteMassegeError);
                } else if(data == "error") {
                    HideSpinner();
                    toastr.warning(DeleteExamMessage);
                } else {
                    toastr.success(deleteMassege);
                    HideSpinner();
                    $('#DeleteEnrollCourseExam').modal("hide");
                    GetExamData()
                }

            } else {
                HideSpinner();
                toastr.error(deleteMassegeError);
            }
        });
}
function ShowDeleteEnrollCourseExam(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/EnrollmentExams/ShowDelete/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#DeleteEnrollCourseExam .modal-content').html(data);
            $('#DeleteEnrollCourseExam').modal("show");

        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}





function ConfirmRegenerate(id, page) {
    ShowSpinner();
    $.post("/ControlPanel/EnrollmentExams/RegenerateExamQuestions/",
        { id: id, page: page },
        function (data) {
            if (data !== null) {
                if (data != "Fail") {
                    toastr.success(RegenerateMassege);
                    HideSpinner();
                    $('#ShowRegenerateExamQuestions').modal("hide");
                    GetExamData()
                } else {
                    HideSpinner();
                    toastr.error(RegenerateMassegeError);
                }
            } else {
                HideSpinner();
                toastr.error(RegenerateMassegeError);
            }
        });
}
function ShowRegenerateExamQuestions(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/EnrollmentExams/ShowRegenerateExamQuestions/",
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
function ShowSubmittedExams(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/EnrollmentExams/ShowSubmittedExams/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#ShowSubmittedExams .modal-content').html(data);
            $('#ShowSubmittedExams').modal("show");

        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowAddRemoveExamQuestions(langId, EnrollCourseExamId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/EnrollQuestion/BankExamQuestionsGetData/",
        data: { languageId: langId, EnrollCourseExamId: EnrollCourseExamId },
        success: function (data) {
            HideSpinner();
            $('#BankExamQuestionsGetData .modal-content').html(data);
            $('#BankExamQuestionsGetData').modal("show");

        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowAddRemoveExamFromBankOfQuestions(langId, id) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/EnrollQuestion/ShowAddRemoveExamQuestions/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $("#ShowExamQuestions").modal("hide");
            $('#AddRemoveTemplateExamQuestions .modal-content').html(data);
            $('#AddRemoveTemplateExamQuestions').modal("show");
            setTimeout(function () {
                $("body").addClass("modal-open")
            }, 600)
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

$("#AddRemoveTemplateExamQuestions").on('hide.bs.modal', function () {
    ShowExamQuestions($("#ViewModleEnrollCourseExamId").val(), $("#ViewModleLangId").val())
});

$("#BankExamQuestionsGetData").on('show.bs.modal', function () {
    $('#ShowExamQuestions').addClass("CustmModle");
});



$("#ShowExamCorrection,#ShowExamViewmodal").on('show.bs.modal', function () {
    $('#ShowSubmittedExams').addClass("CustmModle");
});
$("#ShowExamCorrection,#ShowExamViewmodal,#BankExamQuestionsGetData").on('hide.bs.modal', function () {
    $('#ShowSubmittedExams').removeClass("CustmModle");
    setTimeout(function () {
        $('#ShowExamCorrectionDiv').empty();
    }, 300);
});

function TrainerShowExamView(EnrollCourseExamId, EnrollStudentExamID, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/EnrollmentExams/ShowExamView/",
        data: { EnrollCourseExamId: EnrollCourseExamId, EnrollStudentExamID: EnrollStudentExamID },
        success: function (data) {
            HideSpinner();
            $('#ShowExamViewmodal .modal-content').html(data);
            $('#ShowExamViewmodal').modal("show");

        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}
function ShowExamCorrection(EnrollCourseExamId, EnrollStudentExamID, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/EnrollmentExams/ShowExamCorrection/",
        data: { EnrollCourseExamId: EnrollCourseExamId, EnrollStudentExamID: EnrollStudentExamID },
        success: function (data) {
            HideSpinner();
            $('#ShowExamCorrection .modal-content').html(data);
            $('#ShowExamCorrection').modal("show");

        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowExamQuestions(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/EnrollmentExams/ShowExamQuestions/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#ShowExamQuestions .modal-content').html(data);
            $('#ShowExamQuestions').modal("show");

        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowDetailsEnrollCourseExam(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/EnrollmentExams/ShowDetails/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#DetailsEnrollCourseExam .modal-content').html(data);
            $('#DetailsEnrollCourseExam').modal("show");

        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}
function ShowAddEnrollCourseExam(EnrollTeacherCoursId, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/EnrollmentExams/ShowCreate/",
        data: { EnrollTeacherCoursId: EnrollTeacherCoursId, languageId: langId },
        success: function (data) {
            $('#EditEnrollCourseExamDiv').empty();
            HideSpinner();
            $('#CreateEnrollCourseExam .modal-content').html(data);
            $('#CreateEnrollCourseExam').modal("show");

        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}
function ShowEditCourseExam(Id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/EnrollmentExams/ShowEdit/",
        data: { Id: Id, languageId: langId },
        success: function (data) {
            $('#CreateEnrollCourseDiv').empty();
            HideSpinner();
            $('#EditEnrollCourseExam .modal-content').html(data);
            $('#EditEnrollCourseExam').modal("show");

        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


$("#ShowExamQuestions").off("change").on("change", ".btnEditMarkQuestion", function () {
    ShowSpinner();
    var ExamQuestionid = $(this).attr("attr-question-id");
    var Mark = $(this).val()
    $.ajax({
        type: "Post",
        url: "/ControlPanel/EnrollmentExams/EditMarkQuestion/",
        data: { ExamQuestionid: ExamQuestionid, Mark: Mark },
        success: function (data) {
            if (data == "Success") {
                HideSpinner();

            } else {
                HideSpinner();
                toastr.error(editMassegeErrorQuestions);
            }
        },
        error: function (data) {
            HideSpinner();
            toastr.error(editMassegeError);
        },
    });
});


function BtnSearchRestPageNumber() {
    $('#Page').val("0");
}
function GetData(data) {
    $('#UserProfile_Exams').html(data);
}

function GetExamQuestionsData(data) {
    $('#TemplateBankExamQuestionsGetData').html(data);
}

function MarkExamAdoption(id, adopt) {
    $.ajax({
        type: "Post",
        url: "/ControlPanel/EnrollmentExams/MarkAdoption/",
        data: { id, adopt },
        success: function (data) {
            HideSpinner();
            toastr.success(editMassege);
            GetExamData();
        },
        error: function (data) {
            HideSpinner();
            toastr.error(editMassegeError);
        },
    });
}