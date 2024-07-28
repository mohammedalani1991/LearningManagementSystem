function Get() {
    var URL = "/ControlPanel/EnrollmentCourse/GetData";
    if ($("#EnrollmentCourseCurrentUrl").val())
        URL = $("#EnrollmentCourseCurrentUrl").val();
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

function CreateExamsAndAssignments() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/EnrollmentCourse/GetExamsAndAssignmentsData",
        success: function (data) {
            HideSpinner();
            $('#CreateExamsAndAssignments .modal-content').html(data);
            $('#CreateExamsAndAssignments').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function SubmitForm2() {
    $("#Form2").submit();
}

function ShowAddEnrollmentCourse(langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/EnrollmentCourse/ShowCreate/",
        data: { languageId: langId },
        success: function (data) {
            $('#EditEnrollmentCourseDiv').empty();
            HideSpinner();
            $('#CreateEnrollmentCourse .modal-content').html(data);
            $('#CreateEnrollmentCourse').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowEditTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/EnrollmentCourse/ShowTable/",
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

var EnrollmentCourseFrm = $('#EnrollmentCourse-Create');
EnrollmentCourseFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: EnrollmentCourseFrm.attr('method'),
        url: EnrollmentCourseFrm.attr('action'),
        data: EnrollmentCourseFrm.serialize(),
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            if (data == "AddMassegeErrorInvalidData") {
                toastr.error(AddMassegeErrorInvalidData);
                HideSpinner();
            } else if (data == "AddMassegeErrorSameEnrollCourse") {
                toastr.error(AddMassegeErrorSameEnrollCourse);
                HideSpinner();
            } else if (data == "AddMassegeErrorInvalidCourseTime") {
                toastr.error(AddMassegeErrorInvalidCourseTime);
                HideSpinner();
            } else {
                toastr.success(addMassege);
                $('#CreateEnrollmentCourse').modal("hide");
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

function ShowDetailsEnrollmentCourse(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/EnrollmentCourse/ShowDetails/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#DetailsEnrollmentCourse .modal-content').html(data);
            $('#DetailsEnrollmentCourse').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}
function onDdlChangedForCreate(langId) {
    ShowAddEnrollmentCourse(langId.value);
}

function ShowDeleteEnrollmentCourse(id, langId, page) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/EnrollmentCourse/ShowDelete/",
        data: { id: id, languageId: langId, page: page },
        success: function (data) {
            HideSpinner();
            $('#DeleteEnrollmentCourse .modal-content').html(data);
            $('#DeleteEnrollmentCourse').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}
function ShowRegenerateEnrollmentCourse(id, langId, page) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/EnrollmentCourse/ShowRegenerateEnrollmentCourse/",
        data: { id: id, languageId: langId, page: page },
        success: function (data) {
            HideSpinner();
            $('#RegenerateEnrollmentCourse .modal-content').html(data);
            $('#RegenerateEnrollmentCourse').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}
function ShowLecturesContent(id, langId, page) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/LecturesContent/ShowEdit/",
        data: { id: id, languageId: langId, page: page },
        success: function (data) {
            HideSpinner();
            $('#EditLecturesContent .modal-content').html(data);
            $('#EditLecturesContent').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ConfirmDelete(id, page) {
    ShowSpinner();
    $.post("/ControlPanel/EnrollmentCourse/DeleteEnrollmentCourse/",
        { id: id, page: page },
        function (data) {
            if (data !== null) {
                if (data != "Fail") {
                    toastr.success(deleteMassege);
                    HideSpinner();
                    $('#DeleteEnrollmentCourse').modal("hide");
                    Get()
                } else {
                    HideSpinner();
                    toastr.error(deleteMassegeError);
                }

            } else {
                HideSpinner();
                toastr.error(deleteMassegeError);
            }
        });
}
function ConfirmRegenerateCourse(id, page) {
    ShowSpinner();
    $.post("/ControlPanel/EnrollmentCourse/RegenerateCourseData/",
        { id: id, page: page },
        function (data) {
            if (data !== null) {
                if (data != "Fail") {
                    toastr.success(RegenerateMassege);
                    HideSpinner();
                    $('#RegenerateEnrollmentCourse').modal("hide");
                    location.reload();
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

function onDdlChangedHome(langId) {
    ShowEditEnrollmentCourse($('#EnrollTeacherCourseViewModel_Id').val(), langId.value, true);
}

function onDdlChanged(langId) {
    ShowEditEnrollmentCourse($('#EnrollTeacherCourseViewModel_Id').val(), langId.value, false);
}


var EnrollmentCourseEditFrm = $('#EnrollmentCourse-Edit');
EnrollmentCourseEditFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/EnrollmentCourse/CheckStudentNumber",
        data: { id: $('#EnrollTeacherCourseViewModel_Id').val() },
        success: function (data) {
            if (data <= $("#EnrollTeacherCourseViewModel_CountOfStudent").val()) {
                $.ajax({
                    type: EnrollmentCourseEditFrm.attr('method'),
                    url: EnrollmentCourseEditFrm.attr('action'),
                    data: EnrollmentCourseEditFrm.serialize(),
                    headers: {
                        'RequestVerificationToken': Token
                    },
                    success: function (data) {
                        if (data == "AddMassegeErrorInvalidData") {
                            toastr.error(AddMassegeErrorInvalidData);
                            HideSpinner();
                        } else if (data == "AddMassegeErrorSameEnrollCourse") {
                            toastr.error(AddMassegeErrorSameEnrollCourse);
                            HideSpinner();
                        } else if (data == "AddMassegeErrorInvalidCourseTime") {
                            toastr.error(AddMassegeErrorInvalidCourseTime);
                            HideSpinner();
                        }
                        else {
                            toastr.success(editMassege);
                            $('#EditEnrollmentCourse').modal("hide");
                            HideSpinner();
                            Get();
                        }
                    },
                    error: function (data) {
                        HideSpinner();
                        toastr.error(editMassegeError);
                    },
                });
            } else {
                toastr.warning(editMassegeErrorCount);
                HideSpinner();
            }
        },
        error: function (data) {
            toastr.error(editMassegeError);
        },
    });
});

function ShowEditEnrollmentCourse(id, langId, home) {
    $.ajax({
        type: "GET",
        url: "/ControlPanel/EnrollmentCourse/ShowEdit/",
        data: { id: id, languageId: langId },
        success: function (data) {
            if (home) {
                $('#EditEnrollmentCourse .modal-content').html(data);
                $('#EditEnrollmentCourse').modal("show");
                $("#EnrollTeacherCourseViewModel_LanguageId").attr("onchange", "onDdlChangedHome(this)");
            }
            else {
                $('#GetData').html(data);
                $("#EnrollTeacherCourseViewModel_LanguageId").attr("onchange", "onDdlChanged(this)");
            }
        }
    });
}

$('#EditEnrollmentCourse').on('hidden.bs.modal', function () {
    $('#EditEnrollmentCourse .modal-content').empty();
})

function ShowCopyEnrollmentCourse(id, langId) {
    $.ajax({
        type: "GET",
        url: "/ControlPanel/EnrollmentCourse/ShowEdit/",
        data: { id: id, languageId: langId },
        success: function (data) {
            $('#EditEnrollmentCourse .modal-content').html(data);
            $('#EnrollmentCourse-Edit').attr('action', '/ControlPanel/EnrollmentCourse/Create');
            $('#EnrollmentCourse-Edit').attr('id', 'EnrollmentCourse-Create');
            $("#Title").html(Title)
            $('#EditEnrollmentCourse').modal("show");
        }
    });
}

$('#CreateEnrollmentCourse').on('hidden.bs.modal', function () {
    $('#CreateEnrollmentCourse .modal-content').empty();
})

function PassStudent(enrolId, id, langId, page) {
    ShowSpinner();
    $.post("/ControlPanel/EnrollmentCourse/PassStudent/",
        { id: id },
        function (data) {
            if (data.result == 'Success') {
                toastr.success(PassStudentMassege);
                HideSpinner();
                ShowEnrollStudent(enrolId, langId, page)
            } else {
                HideSpinner();
                toastr.error(PassStudentMassegeError);
            }
        });
}

function ShowEnrollStudent(id, langId, page) {
    ShowSpinner();
    $("#ChangeEnrollStudentId").val(id);
    $.ajax({
        type: "GET",
        url: "/ControlPanel/EnrollmentCourse/ShowEnrollStudent/",
        data: { id: id, languageId: langId, page: page },
        success: function (data) {
            $('#GetData').html(data);
            HideSpinner();
        },
        error: function (data) {
            HideSpinner();
        }
    });
}
function ShowCountOfEnrolledStudents(id, langId, page) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/EnrollmentCourse/ShowCountOfEnrolledStudents/",
        data: { id: id, languageId: langId, page: page },
        success: function (data) {
            $('#GetData').html(data);
            HideSpinner();
        },
        error: function (data) {
            HideSpinner();
        }
    });
}
function ShowAddNewStudent(id, langId, page) {
    ShowSpinner();
    var URL = "/ControlPanel/EnrollmentCourse/ShowAddNewStudent/";
    if ($("#ShowAddNewStudentCurrentUrl").val()) {
        $.ajax({
            type: "GET",
            url: $("#ShowAddNewStudentCurrentUrl").val(),
            success: function (data) {
                $('#ShowAddNewStudent .modal-content').html(data);
                $('#ShowAddNewStudent').modal("show");
                HideSpinner();
            },
            error: function (data) {
                HideSpinner();
            }

        });
    } else {
        $.ajax({
            type: "GET",
            url: URL,
            data: { id: id, languageId: langId, page: page },
            success: function (data) {
                $('#ShowAddNewStudent .modal-content').html(data);
                $('#ShowAddNewStudent').modal("show");
                HideSpinner();
            },
            error: function (data) {
                HideSpinner();
            }
        });
    }
}
function AddNewStudent(current, StudentId, enrollTeacherCourseId) {
    ShowSpinner();
    $.post("/ControlPanel/EnrollmentCourse/AddNewStudent/",
        { StudentId: StudentId, enrollTeacherCourseId: enrollTeacherCourseId },
        function (data) {
            if (data.result == 'Success') {
                toastr.success(editMassege);

                ShowAddNewStudent(enrollTeacherCourseId);
                ShowCountOfEnrolledStudents(enrollTeacherCourseId);

                HideSpinner();

            }
            else if (data.result == "FailExpilled") {
                $(current).parent().parent().find(".ForceAdd").show();
                toastr.error(FailExpilled + " " + data.toDate);
                HideSpinner();
            }
            else if (data.result == "FailMaxEnrollStudent") {
                $(current).parent().parent().find(".ForceAdd").show();
                HideSpinner();
                toastr.error(FailMaxEnrollStudent);
            } else if (data.result == "FailExistsStudent") {
                $(current).parent().parent().find(".ForceAdd").show();
                HideSpinner();
                toastr.error(FailExistsStudent);
            }
            else if (data.result == "FailAgeAllowedForRegistration") {
                $(current).parent().parent().find(".ForceAdd").show();
                HideSpinner();
                toastr.error(FailAgeAllowedForRegistration);
            }
            else if (data.result == "HasPreRequestsCourse") {
                $(current).parent().parent().find(".ForceAdd").show();
                HideSpinner();
                toastr.error(HasPreRequestsCourse);
            }
            else if (data.result == "FailAgeGroup") {
                $(current).parent().parent().find(".ForceAdd").show();
                HideSpinner();
                toastr.error(FailAgeGroup);
            }
            else {
                HideSpinner();
                toastr.error(editMassegeError);
            }
        });
}

function ForceAddNewStudent(StudentId, enrollTeacherCourseId) {
    ShowSpinner();
    $.post("/ControlPanel/EnrollmentCourse/ForceAddNewStudent/",
        { StudentId: StudentId, enrollTeacherCourseId: enrollTeacherCourseId },
        function (data) {
            if (data.result == 'Success') {
                toastr.success(editMassege);
                ShowAddNewStudent(enrollTeacherCourseId);
                ShowCountOfEnrolledStudents(enrollTeacherCourseId);
                HideSpinner();
            }
            else {
                HideSpinner();
                toastr.error(editMassegeError);
            }
        });
}

function ChangeEnrollStudent(enrolId, page, langId) {
    event.preventDefault();
    ShowSpinner();
    $.post("/ControlPanel/EnrollmentCourse/ChangeEnrollStudent/",
        { ChangeEnrollStudentId: $("#ChangeEnrollStudentId").val(), EnrollTeacherCourseId: $("#EnrollTeacherCourseId").val() },
        function (data) {
            if (data.result == 'Success') {
                toastr.success(editMassege);
                HideSpinner();
                $('#ChangeEnrollStudent').modal("hide");
                $($(".modal-backdrop")[1]).hide();
                ShowEnrollStudent(enrolId, langId, page);
            } else {
                HideSpinner();
                toastr.error(editMassegeError);
            }
        });

}

function ShowChangeEnrollStudent(id) {
    $('#ChangeEnrollStudent').modal("show");
    $("#ChangeEnrollStudentId").val(id);
}
function CloseChangeEnrollStudent() {
    $('#ChangeEnrollStudent').modal("hide");
}

$("#ShowAddNewStudent").on('show.bs.modal', function () {
    $('#ShowCountOfEnrolledStudents').addClass("hidden")

});
$("#ShowAddNewStudent").on('hide.bs.modal', function () {
    $('#ShowCountOfEnrolledStudents').removeClass("hidden")

});

$("#CreateEnrollmentCourse,#EditEnrollmentCourse").on('hide.bs.modal', function () {
    setTimeout(function () {
        $('#CreateEnrollmentCourseDiv').empty();
        $('#EditEnrollmentCourseDiv').empty();
    }, 500);

});

function BtnSearchRestPageNumber() {
    $('#Page').val("0");
    $('#ShowAddNewStudentForm #page').val("0");

}
function GetData(data) {
    $('#main').html(data);
}

function CreateExamsAndAssignmentsData(data) {
    $('#CreateExamsAndAssignmentsDiv').html(data);
}

function GetDataShowAddNewStudent(data) {
    $('#ShowAddNewStudent .modal-content').html(data);
}


function GetStudentAttendances(courseId, id) {
    ShowSpinner();
    $.ajax({
        type: "Get",
        url: "/ControlPanel/EnrollmentCourse/GetStudentAttendances",
        data: { CourseId: courseId, id: id },
        success: function (data) {
            HideSpinner();
            $('#ShowCountOfEnrolledStudents .modal-content').html(data);
            $('#ShowCountOfEnrolledStudents').modal("show");
        },
        error: function (data) {
            HideSpinner();
        },
    });
}


function OpenDeleteStudent(id) {
    $("#DeleteId").val(id);
    $("#DeleteModal").modal("show");
    $("#Delete_Modal").attr("onclick", "DeleteStudent()");
}

function DeleteStudent() {
    $.ajax({
        type: "POST",
        url: "/ControlPanel/EnrollmentCourse/RemoveStudent",
        data: {
            id: $("#DeleteId").val()
        },
        headers: {
            'RequestVerificationToken': Token
        },
        dataType: "json",
        success: function (result) {
            toastr.success(deleteMassege);
            ShowCountOfEnrolledStudents($("#Id").val(), $("#LangId").val());
            $("#DeleteModal").modal("hide");
        },
        error: function (req, status, error) {
            toastr.error(deleteMassegeError);
        }
    });
}

function ShowTransferStudent(id) {
    $("#TransferStudentId").val(id);
    $("#TransferStudent").modal('show');
}

function TransferStudent() {
    if ($("#TransferForm").valid())
        $.ajax({
            type: "POST",
            url: "/ControlPanel/EnrollmentCourse/TransferStudent",
            data: {
                StudentId: $("#TransferStudentId").val(), oldEnrollTeacherCourseId: $("#Id").val(), enrollTeacherCourseId: $("#TransferCourseId").val()
            },
            headers: {
                'RequestVerificationToken': Token
            },
            dataType: "json",
            success: function (data) {
                if (data.result == 'Success') {
                    toastr.success(editMassege);
                    ShowCountOfEnrolledStudents($("#Id").val(), $("#LangId").val());
                    HideSpinner();
                    $("#TransferStudent").modal("hide");

                } else if (data.result == "FailMaxEnrollStudent") {
                    HideSpinner();
                    toastr.warning(FailMaxEnrollStudent);
                } else if (data.result == "FailExistsStudent") {
                    HideSpinner();
                    toastr.warning(FailExistsStudent);
                }
                else if (data.result == "FailAgeAllowedForRegistration") {
                    HideSpinner();
                    toastr.warning(FailAgeAllowedForRegistration);
                }
                else if (data.result == "HasPreRequestsCourse") {
                    HideSpinner();
                    toastr.warning(HasPreRequestsCourse);
                }
                else if (data.result == "FailAgeGroup") {
                    HideSpinner();
                    toastr.warning(FailAgeGroup);
                }
                else {
                    HideSpinner();
                    toastr.error(editMassegeError);
                }
            },
            error: function (req, status, error) {
                toastr.error(editMassegeError);
                HideSpinner();
            }
        });
}

function ShowExpelStudent(id) {
    $("#ExpelStudentId").val(id);
    $("#ExpelModal").modal('show');
}

function ExpelStudent() {
    $.ajax({
        type: "POST",
        url: "/ControlPanel/EnrollmentCourse/ExpelStudent",
        data: {
            EnrollStudentCourseId: $("#ExpelStudentId").val()
        },
        headers: {
            'RequestVerificationToken': Token
        },
        dataType: "json",
        success: function (result) {
            toastr.success(expelMassege);
            ShowCountOfEnrolledStudents($("#Id").val(), $("#LangId").val());
            $("#ExpelModal").modal("hide");
        },
        error: function (req, status, error) {
            toastr.error(expelMassegeError);
        }
    });
}

function ShowDelayStudent(id) {
    $("#DelayStudentId").val(id);
    $("#DelayModal").modal('show');
}

function DelayStudent() {
    $.ajax({
        type: "POST",
        url: "/ControlPanel/EnrollmentCourse/DelayStudent",
        data: {
            EnrollStudentCourseId: $("#DelayStudentId").val()
        },
        headers: {
            'RequestVerificationToken': Token
        },
        dataType: "json",
        success: function (result) {
            toastr.success(DelayMassege);
            ShowCountOfEnrolledStudents($("#Id").val(), $("#LangId").val());
            $("#DelayModal").modal("hide");
        },
        error: function (req, status, error) {
            toastr.error(DelayMassegeError);
        }
    });
}

function ShowCancelExpulsion(id) {
    $("#CancelExpelStudentId").val(id);
    $("#CancelExpulsionModal").modal('show');
}

function CancelExpulsion() {
    $.ajax({
        type: "POST",
        url: "/ControlPanel/EnrollmentCourse/CancelExpulsion",
        data: {
            EnrollStudentCourseId: $("#CancelExpelStudentId").val()
        },
        headers: {
            'RequestVerificationToken': Token
        },
        dataType: "json",
        success: function (result) {
            toastr.success(CancelExpelMassege);
            ShowCountOfEnrolledStudents($("#Id").val(), $("#LangId").val());
            $("#CancelExpulsionModal").modal("hide");
        },
        error: function (req, status, error) {
            toastr.error(CancelExpelMassegeError);
        }
    });
}

function GetExamData(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/EnrollmentExams/GetData/",
        data: { CourseId: $("#Id").val(), languageId: $("#LangId").val() },
        success: function (data) {
            HideSpinner();
            $('#GetData').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function GetPracticalExams(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/PracticalEnrollmentExam/GetPracticalExams",
        data: { TecherCourseId: $("#Id").val(), languageId: $("#LangId").val() },
        success: function (data) {
            HideSpinner();
            $('#GetData').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowCreateMemorizationExam(id) {
    ShowSpinner();
    $.ajax({
        type: "Get",
        url: "/ControlPanel/PracticalEnrollmentExam/ShowCreateMemorizationExam",
        data: { id: id },
        success: function (data) {
            HideSpinner();
            $('#ShowCountOfEnrolledStudents .modal-content').html(data);
            $('#ShowCountOfEnrolledStudents').modal("show");
        },
        error: function (data) {
            HideSpinner();
        },
    });
}

function GetAssignmentData(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/EnrollmentAssignments/GetData/",
        data: { CourseId: $("#Id").val(), languageId: $("#LangId").val() },
        success: function (data) {
            HideSpinner();
            $('#GetData').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function GetAllowUserRatesData(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/EnrollCourseAllowUserRate/GetData/",
        data: { CourseId: $("#Id").val(), languageId: $("#LangId").val() },
        success: function (data) {
            HideSpinner();
            $('#GetData').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function GetEnrollStudentAlertsData(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/EnrollStudentAlert/GetData/",
        data: { CourseId: $("#Id").val(), languageId: $("#LangId").val() },
        success: function (data) {
            HideSpinner();
            $('#GetData').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}



function GetEnrollManagementRates(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/TrainerRate/GetEnrollManagementRates/",
        data: { enrollId: $("#Id").val(), languageId: $("#LangId").val() },
        success: function (data) {
            HideSpinner();
            $('#GetData').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function GetEnrollAcademicSupervisionRates(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/TrainerRate/GetEnrollAcademicSupervisionRates/",
        data: { enrollId: $("#Id").val(), languageId: $("#LangId").val() },
        success: function (data) {
            HideSpinner();
            $('#GetData').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function GetQuizzesData(id) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/EnrollCourseQuiz/GetQuizzes/",
        data: { page: $("#Page").val(), id: id },
        success: function (data) {
            HideSpinner();
            $('#GetData').html(data);
        },
        error: function (data) {
            HideSpinner();
        },
    });
}

function RefetchQuizzes(id) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/EnrollCourseQuiz/RefetchQuizzes/",
        data: { id: id },
        success: function (data) {
            toastr.success(addMassege);
            HideSpinner();
            GetQuizzesData(id)
        },
        error: function (data) {
            HideSpinner();
            toastr.error(addMassegeError);
        },
    });
}


function ShowCreateTeamMeeting(id) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/EnrollmentCourse/ShowCreateTeamMeeting/",
        data: { id: id },
        success: function (data) {
            HideSpinner();
            $('#ShowAddTeams .modal-content').html(data);
            $('#ShowAddTeams').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function CreateMeeting() {
    if ($("#TeamForm").valid()) {
        ShowSpinner();
        $.ajax({
            type: "POST",
            url: "/ControlPanel/EnrollmentCourse/CreateMeeting",
            data: {
                id: $("#EnrollTeacherCourseId").val(), start: $("#WorkStartDate").val(), end: $("#WorkEndDate").val(),
            },
            headers: {
                'RequestVerificationToken': Token
            },
            dataType: "json",
            success: function (result) {
                if (result) {
                    toastr.success(addMassege);
                    ShowCreateTeamMeeting($("#EnrollTeacherCourseId").val());
                    HideSpinner();
                } else {
                    toastr.error(addMassegeError);
                    HideSpinner();
                }
            },
            error: function (req, status, error) {
                toastr.error(addMassegeError);
                HideSpinner();
            }
        });
    }
}