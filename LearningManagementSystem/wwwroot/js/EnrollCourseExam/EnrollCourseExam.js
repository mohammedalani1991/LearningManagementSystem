function Get() {
    var URL = "/Trainer/EnrollCourseExam/GetData";
    if ($("#EnrollCourseExamCurrentUrl").val())
        URL = $("#EnrollCourseExamCurrentUrl").val();

    ShowSpinner();
    $.ajax({
        type: "GET",
        url: URL,
        dataType: 'html',
        success: function (data) {
            HideSpinner();
            $('#UserProfile_Exams').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

var EnrollCourseExamFrm = $('#EnrollCourseExam-Create');
EnrollCourseExamFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    var formDataArray = EnrollCourseExamFrm.serializeArray();
    formDataArray.push({ name: 'Description1', value: tinymce.get('Description').getContent() });
    var serializedData = $.param(formDataArray);
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
           }
           else if (data == "FailEnrollLectureId") {
               HideSpinner();
               toastr.error(FailEnrollLectureId);
           }
           else {
                toastr.success(addMassege);
                $('#CreateEnrollCourseExam').modal("hide");
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

var EnrollCourseExamEditFrm = $('#EnrollCourseExam-Edit');
EnrollCourseExamEditFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    var formDataArray = EnrollCourseExamEditFrm.serializeArray();
    formDataArray.push({ name: 'Description1', value: tinymce.get('Description').getContent() });
    var serializedData = $.param(formDataArray);
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
                Get();
            }
        },
        error: function (data) {
            HideSpinner();
            toastr.error(addMassegeError);
        },
    });
});
function onDdlChangedForCreate(langId) {
    ShowAddEnrollCourseExam($("#EnrollCourseExam-Create #EnrollTeacherCourseId").val(), langId.value);
}
function onDdlChanged(langId) {
    ShowEditEnrollCourseExam($("#EnrollCourseExam-Edit #Id").val(), langId.value);
}
function ConfirmDelete(id) {
    ShowSpinner();
    $.post("/Trainer/EnrollCourseExam/DeleteEnrollCourseExam/",
        { id: id  },
        function (data) {
            if (data !== null) {
                if (data == "Fail") {
                    HideSpinner();
                    toastr.error(deleteMassegeError);
                } else {
                    toastr.success(deleteMassege);
                    HideSpinner();
                    $('#DeleteEnrollCourseExam').modal("hide");
                    Get()
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
        url: "/Trainer/EnrollCourseExam/ShowDelete/",
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
    $.post("/Trainer/EnrollCourseExam/RegenerateExamQuestions/",
        { id: id, page: page },
        function (data) {
            if (data !== null) {
                if (data != "Fail") {
                    toastr.success(RegenerateMassege);
                    HideSpinner();
                    $('#ShowRegenerateExamQuestions').modal("hide");
                    Get()
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
        url: "/Trainer/EnrollCourseExam/ShowRegenerateExamQuestions/",
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
        url: "/Trainer/EnrollCourseExam/ShowSubmittedExams/",
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
        url: "/Trainer/EnrollExamQuestion/BankExamQuestionsGetData/",
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
        url: "/Trainer/EnrollExamQuestion/ShowAddRemoveExamQuestions/",
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
        url: "/Trainer/EnrollCourseExam/ShowExamView/",
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
        url: "/Trainer/EnrollCourseExam/ShowExamCorrection/",
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
        url: "/Trainer/EnrollCourseExam/ShowExamQuestions/",
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
        url: "/Trainer/EnrollCourseExam/ShowDetails/",
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
function ShowAddEnrollCourseExam(EnrollTeacherCoursId,langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Trainer/EnrollCourseExam/ShowCreate/",
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
function ShowEditEnrollCourseExam(Id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Trainer/EnrollCourseExam/ShowEdit/",
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
        url: "/Trainer/EnrollCourseExam/EditMarkQuestion/",
        data: { ExamQuestionid: ExamQuestionid, Mark: Mark},
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
        url: "/Trainer/EnrollCourseExam/MarkAdoption/",
        data: { id, adopt },
        success: function (data) {
            HideSpinner();
            toastr.success(editMassege);
            Get()
        },
        error: function (data) {
            HideSpinner();
            toastr.error(editMassegeError);
        },
    });
}