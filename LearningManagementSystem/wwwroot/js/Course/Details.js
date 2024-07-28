function ShowCourseTrainer() {
    var EnrollTeacherCourseId = $("#EnrollTeacherCourseId").val();
    if (EnrollTeacherCourseId != '' && EnrollTeacherCourseId != null) {
        $.ajax({
            type: "GET",
            url: "/Courses/ShowCourseTrainer/",
            data: { id: EnrollTeacherCourseId },
            success: function (data) {
                if (data !== null) {
                    $('#CourseTrainer').html(data);
                }
            }
        });
    } else {
        $('#CourseTrainer').html("");
    }
}

function ShowExamView(EnrollCourseExamId, EnrollStudentExamID) {
    event.preventDefault();
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/CourseExam/ShowExamView/",
        data: { EnrollCourseExamId: EnrollCourseExamId, EnrollStudentExamID: EnrollStudentExamID },
        success: function (data) {
            HideSpinner();
            $('#ShowExamView .modal-content').html(data);
            $('#ShowExamView').modal("show");

        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}
function ShowExamPrerequisite(EnrollCourseExamId) {
    event.preventDefault();
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/CourseExam/ShowExamPrerequisite/",
        data: { EnrollCourseExamId: EnrollCourseExamId, TakeAgain: false },
        success: function (data) {
            HideSpinner();
            $('#ShowExamPrerequisite .modal-content').html(data);
            $('#ShowExamPrerequisite').modal("show");

        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}
function ShowExamPrerequisiteAgain(EnrollCourseExamId) {
    event.preventDefault();
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/CourseExam/ShowExamPrerequisite/",
        data: { EnrollCourseExamId: EnrollCourseExamId, TakeAgain: true },
        success: function (data) {
            HideSpinner();
            $('#ShowExamPrerequisite .modal-content').html(data);
            $('#ShowExamPrerequisite').modal("show");

        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}
$("#ShowExamPrerequisite").on('hide.bs.modal', function () {
    setTimeout(function () {
        $(this).data('bs.modal', null);
        $('#ShowExamPrerequisiteDiv').empty();
    }, 300);
});


function CourseTrainerEnrollDetails() {
    var EnrollTeacherCourseId = $("#EnrollTeacherCourseId").val();
    if (EnrollTeacherCourseId != '' && EnrollTeacherCourseId != null) {
        $.ajax({
            type: "GET",
            url: "/Courses/ShowCourseTrainerEnrollDetails/",
            data: { id: EnrollTeacherCourseId },
            success: function (data) {
                if (data !== null) {

                    $('#CourseTrainerEnrollDetails').html(data);
                }

            }
        });
    } else {
        $('#CourseTrainerEnrollDetails').html("");
    }
}

function ShowAddAttachment() {
    var EnrollTeacherCourseId = $("#EnrollTeacherCourseId").val();
    if (EnrollTeacherCourseId != '' && EnrollTeacherCourseId != null) {
        $.ajax({
            type: "GET",
            url: "/Courses/ShowAddAttachment/",
            data: { id: EnrollTeacherCourseId, enrollStudentCourseId: $("#EnrollStudentCourseId").val() },
            success: function (data) {
                if (data !== null) {
                    $('#ShowAddAttachment .modal-content').html(data);
                    $('#ShowAddAttachment').modal("show");

                    $('#ShowAddAttachment').on('hide.bs.modal', function (e) {
                        e.preventDefault();
                        e.stopPropagation();
                        return false;
                    });
                }

            }
        });
    } else {
        $('#ShowAddAttachment').html("");
    }
}

function ShowLessons() {
    var EnrollTeacherCourseId = $("#EnrollTeacherCourseId").val();
    if (EnrollTeacherCourseId != '' && EnrollTeacherCourseId != null) {
        $.ajax({
            type: "GET",
            url: "/Courses/ShowLessons/",
            data: { id: EnrollTeacherCourseId },
            success: function (data) {
                if (data !== null) {

                    $('#ShowLessons').html(data);
                }

            }
        });
    } else {
        $('#ShowLessons').html("");
    }
}
function ShowExams() {
    var EnrollTeacherCourseId = $("#EnrollTeacherCourseId").val();
    if (EnrollTeacherCourseId != '' && EnrollTeacherCourseId != null) {
        $.ajax({
            type: "GET",
            url: "/Courses/ShowExams/",
            data: { EnrollTeacherCourseId: EnrollTeacherCourseId },
            success: function (data) {
                if (data !== null) {

                    $('#ShowExams').html(data);
                }

            }
        });
    } else {
        $('#ShowExams').html("");
    }
}

function ShowPracticalExams() {
    var EnrollTeacherCourseId = $("#EnrollTeacherCourseId").val();
    if (EnrollTeacherCourseId != '' && EnrollTeacherCourseId != null) {
        $.ajax({
            type: "GET",
            url: "/PracticalExam/GetPracticalExams/",
            data: { TecherCourseId: EnrollTeacherCourseId },
            success: function (data) {
                if (data !== null) {
                    $('#ShowPracticalExams').html(data);
                }
            }
        });
    } else {
        $('#ShowPracticalExams').html("");
    }
}

function ShowPreRequests() {
    var EnrollTeacherCourseId = $("#EnrollTeacherCourseId").val();
    if (EnrollTeacherCourseId != '' && EnrollTeacherCourseId != null) {
        $.ajax({
            type: "GET",
            url: "/Courses/ShowPreRequests/",
            data: { EnrollTeacherCourseId: EnrollTeacherCourseId },
            success: function (data) {
                if (data !== null) {

                    $('#ShowPreRequests').html(data);
                }

            }
        });
    } else {
        $('#ShowPreRequests').html("");
    }
}

function ShowAssignments() {
    var EnrollTeacherCourseId = $("#EnrollTeacherCourseId").val();
    if (EnrollTeacherCourseId != '' && EnrollTeacherCourseId != null) {
        $.ajax({
            type: "GET",
            url: "/Courses/ShowAssignments/",
            data: { enrollTeacherCourseId: EnrollTeacherCourseId },
            success: function (data) {
                if (data !== null) {
                    $('#ShowAssignments').html(data);
                }

            }
        });
    } else {
        $('#ShowAssignments').html("");
    }
}

function ShowMarks() {
    var EnrollTeacherCourseId = $("#EnrollTeacherCourseId").val();
    if (EnrollTeacherCourseId != '' && EnrollTeacherCourseId != null) {
        $.ajax({
            type: "GET",
            url: "/Courses/ShowMarks/",
            data: { enrollTeacherCourseId: EnrollTeacherCourseId },
            success: function (data) {
                if (data !== null) {
                    $('#ShowMarks').html(data);
                }
            }
        });
    } else {
        $('#ShowMarks').html("");
    }
}

function ShowAttendances() {
    var EnrollTeacherCourseId = $("#EnrollTeacherCourseId").val();
    if (EnrollTeacherCourseId != '' && EnrollTeacherCourseId != null) {
        $.ajax({
            type: "GET",
            url: "/Courses/ShowAttendances/",
            data: { enrollTeacherCourseId: EnrollTeacherCourseId },
            success: function (data) {
                if (data !== null) {
                    $('#ShowAttendances').html(data);
                }

            }
        });
    } else {
        $('#ShowAttendances').html("");
    }
}


function ShowLessonDetails(id, current) {
    $.ajax({
        type: "GET",
        url: "/Courses/ShowLessonDetails/",
        data: { id: id },
        success: function (data) {
            if (data !== null) {
                $('#LessonDetails').html(data);
                $('.active1').removeClass('active1');
                $(current).addClass("active1")
                $(".w-col-Lesson").css("overflow", "auto")
                $("#LessonDetails").removeClass("d-none");
            }

        }
    });
}

function moreLesson() {
    $(".w-col-Lesson").css("overflow", "visible")
    $("#LessonDetails").addClass("d-none");
}


$(document).ready(function () {
    ShowCourseTrainer();
    CourseTrainerEnrollDetails();
});

function SenangPay() {
    $("#AddInvoicesPay").hide();
    $("#AddBalance").hide();

    var Amount = $("#CoursePrice").val();
    var senangPay = {};
    senangPay.Amount = Amount;
    senangPay.EnrollTeacherCourseId = $("#EnrollTeacherCourseId").val();

    $.ajax({
        type: "POST",
        url: "/SenangPay/Create",
        dataType: 'json',
        cache: false,
        data: { senangPayViewModel: senangPay },
        success: function (data) {
            if (data.url != undefined && data.url != '') {
                window.location = data.url;
            } else if (data.result != undefined && data.result != '') {
                if (data.result == "FailMaxEnrollStudent") {
                    toastr.error(FailMaxEnrollStudent);
                } else if (data.result == "FailExistsStudent") {
                    toastr.error(FailExistsStudent);
                }
                else if (data.result == "FailAgeAllowedForRegistration") {
                    toastr.error(FailAgeAllowedForRegistration);
                }
                else if (data.result == "HasPreRequestsCourse") {
                    toastr.error(HasPreRequestsCourse);
                }
                else if (data.result == "FailAgeGroup") {
                    toastr.error(FailAgeGroup);
                }
                else {
                    toastr.error(data.result);
                }

            }
        }
    });
}

function ShowCourseTimes() {
    var EnrollTeacherCourseId = $("#EnrollTeacherCourseId").val();
    if (EnrollTeacherCourseId != '' && EnrollTeacherCourseId != null) {
        $.ajax({
            type: "GET",
            url: "/Courses/ShowCourseTimes/",
            data: { id: EnrollTeacherCourseId },
            success: function (data) {
                if (data !== null) {
                    $('#CourseTimes').html(data);
                }

            }
        });
    } else {
        $('#CourseTimes').html("");
    }
}
ShowCourseTimes();

function SignUp() {
    $.ajax({
        type: "Post",
        url: "/Courses/SignUp/",
        data: { enrollTeacherCourseId: $("#EnrollTeacherCourseId").val() },
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            HideSpinner();
            if (data.result == "FailMaxEnrollStudent") {
                toastr.error(FailMaxEnrollStudent);
            } else if (data.result == "FailExistsStudent") {
                toastr.error(FailExistsStudent);
            }
            else if (data.result == "FailAgeAllowedForRegistration") {
                toastr.error(FailAgeAllowedForRegistration);
            }
            else if (data.result == "HasPreRequestsCourse") {
                toastr.error(HasPreRequestsCourse);
            }
            else if (data.result == "FailAgeGroup") {
                toastr.error(FailAgeGroup);
            }
            else {
                if (data.result > 0) {
                    toastr.success(addMassege);
                    setTimeout(function () { location.reload() }, 1000)
                } else if (data.result == -1) {
                    toastr.error(addMassegeAddBefore);
                } else {

                    toastr.error(addMassegeError);
                }
            }
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowSelectPayMethod() {
    ShowSpinner();
    var EnrollTeacherCourseId = $("#EnrollTeacherCourseId").val();
    $.ajax({
        type: "GET",
        url: "/Courses/ShowSelectPayMethod/",
        data: { selectedEnrollTeacherCourseId: EnrollTeacherCourseId },
        success: function (data) {
            HideSpinner();
            if (data.result == "FailExpilled")
                toastr.error(FailExpilled + " " + data.toDate);
            else if (data.result == "FailMaxEnrollStudent") {
                toastr.error(FailMaxEnrollStudent);
            } else if (data.result == "FailExistsStudent") {
                toastr.error(FailExistsStudent);
            }
            else if (data.result == "FailAgeAllowedForRegistration") {
                toastr.error(FailAgeAllowedForRegistration);
            }
            else if (data.result == "HasPreRequestsCourse") {
                toastr.error(HasPreRequestsCourse);
            }
            else if (data.result == "FailAgeGroup") {
                toastr.error(FailAgeGroup);
            } else {

                $('#SelectPayMethod .modal-content').html(data);
                $('#SelectPayMethod').modal("show");
            }

        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

//$("#SelectPayMethod").on('hide.bs.modal', function () {
//    //setTimeout(function () {
//    //    window.location = window.location;
//    //}, 500);
//});


function AddInvoicesPay() {
    var SelectPayMethodFrm = $('#AddInvoicesPay');
    ShowSpinner();
    var EnrollTeacherCourseId = $("#EnrollTeacherCourseId").val();
    var DataSent = SelectPayMethodFrm.serialize() + "&EnrollTeacherCourseId=" + EnrollTeacherCourseId;
    if (SelectPayMethodFrm.valid())
        if ($("#AttachmentUrl").val() != "")
        $.ajax({
            type: SelectPayMethodFrm.attr('method'),
            url: SelectPayMethodFrm.attr('action'),
            data: DataSent,
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                HideSpinner();
                $('#SelectPayMethod').modal("hide");
                if (data.result == "FailMaxEnrollStudent") {
                    toastr.error(FailMaxEnrollStudent);
                } else if (data.result == "FailExistsStudent") {
                    toastr.error(FailExistsStudent);
                }
                else if (data.result == "FailAgeAllowedForRegistration") {
                    toastr.error(FailAgeAllowedForRegistration);
                }
                else if (data.result == "HasPreRequestsCourse") {
                    toastr.error(HasPreRequestsCourse);
                }
                else if (data.result == "FailAgeGroup") {
                    toastr.error(FailAgeGroup);
                }
                else {
                    if (data.result > 0) {
                        toastr.success(addMassege);
                    } else if (data.result == -1) {
                        toastr.error(addMassegeAddBefore);
                    } else {

                        toastr.error(addMassegeError);
                    }
                }
            },
            error: function (data) {
                HideSpinner();
                toastr.error(addMassegeError);
            },
        });
        else
            $("#AttachmentUrlError").show()
    HideSpinner();
}
