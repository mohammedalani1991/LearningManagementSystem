
function ShowCreatePracticalEnrollmentExam(courseId, techerCourseId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/PracticalEnrollmentExam/Create/",
        data: { courseId: courseId, techerCourseId: techerCourseId },
        success: function (data) {
            HideSpinner();
            $('#ShowCreatePracticalEnrollmentExam .modal-content').html(data);
            $('#ShowCreatePracticalEnrollmentExam').modal("show");

        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function CreatePracticalEnrollmentExam() {
    var EnrollCourseExamFrm = $('#PracticalEnrollmentExam-Create');
    if (EnrollCourseExamFrm.valid()) {
        if ($("#StartDate").val() < $("#EndDate").val()) {
            ShowSpinner();
            $.ajax({
                type: EnrollCourseExamFrm.attr('method'),
                url: EnrollCourseExamFrm.attr('action'),
                data: EnrollCourseExamFrm.serialize(),
                headers: {
                    'RequestVerificationToken': Token
                },
                success: function (data) {
                    toastr.success(addMassege);
                    $('#ShowCreatePracticalEnrollmentExam').modal("hide");
                    HideSpinner();
                    GetPracticalExams();
                },
                error: function (data) {
                    HideSpinner();
                    toastr.error(addMassegeError);
                },
            });
        }
        else
            toastr.warning(AddMassegeErrorInvalidDates);
    }
}

function ShowEditEnrollCourseExam(id, courseId, techerCourseId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/PracticalEnrollmentExam/Edit/",
        data: { id: id, courseId: courseId, techerCourseId: techerCourseId },
        success: function (data) {
            HideSpinner();
            $('#ShowCreatePracticalEnrollmentExam .modal-content').html(data);
            $('#ShowCreatePracticalEnrollmentExam').modal("show");

        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function EditPracticalEnrollmentExam() {
    var EnrollCourseExamEditFrm = $('#PracticalEnrollmentExam-Edit');
    if (EnrollCourseExamEditFrm.valid()) {
        if ($("#StartDate").val() < $("#EndDate").val()) {
            ShowSpinner();
            $.ajax({
                type: EnrollCourseExamEditFrm.attr('method'),
                url: EnrollCourseExamEditFrm.attr('action'),
                data: EnrollCourseExamEditFrm.serialize(),
                headers: {
                    'RequestVerificationToken': Token
                },
                success: function (data) {
                    toastr.success(editMassege);
                    $('#ShowCreatePracticalEnrollmentExam').modal("hide");
                    HideSpinner();
                    GetPracticalExams();
                },
                error: function (data) {
                    HideSpinner();
                    toastr.error(addMassegeError);
                },
            });
        } else
            toastr.warning(EditMassegeErrorInvalidDates);
    }
}

function GetPracticalExamsData(data) {
    $('#GetData').html(data);
}


function ShowEnrolledStudents(id, techerCourseId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/PracticalEnrollmentExam/ShowEnrolledStudents/",
        data: { PracticalEnrollmentExamId: id, EnrollTeacherCourseId: techerCourseId },
        success: function (data) {
            HideSpinner();
            $('#ShowEnrolledStudents .modal-content').html(data);
            $('#ShowEnrolledStudents').modal("show");

        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowAddTeacher(id) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/PracticalEnrollmentExam/ShowTeachers/",
        data: { PracticalEnrollmentExamId: id },
        success: function (data) {
            HideSpinner();
            $('#ShowAddTeachers .modal-content').html(data);
            $('#ShowAddTeachers').modal("show");

        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowAddSubjects(EnrollStudentCourseId, TecherCourseId, PracticalEnrollmentExamId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/PracticalEnrollmentExam/ShowSubjects/",
        data: { EnrollStudentCourseId: EnrollStudentCourseId, TecherCourseId: TecherCourseId, PracticalEnrollmentExamId },
        success: function (data) {
            HideSpinner();
            $('#ShowEnrolledStudents').modal("hide");
            $('#ShowAddSubjects .modal-content').html(data);
            $('#ShowAddSubjects').modal("show");

        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function GetSubjects(data) {
    $('#ShowAddSubjects .modal-content').html(data);
}


$('#ShowAddSubjects').on('hidden.bs.modal', function () {
    if (get)
        ShowEnrolledStudents($("#PracticalEnrollmentExamId").val(), $("#TecherCourseId").val())
});

$('#ShowEnrolledStudents').on('hidden.bs.modal', function () {
    GetPracticalExams();
});


function ShowAddPoints(EnrollStudentCourseId, PracticalEnrollmentExamId, count, subject) {
    if (subject ? subjectCount > 0 : count > 0) {
        ShowSpinner();
        $.ajax({
            type: "GET",
            url: "/ControlPanel/PracticalEnrollmentExam/ShowAddPoints/",
            data: { EnrollStudentCourseId, PracticalEnrollmentExamId },
            success: function (data) {
                HideSpinner();
                if (subject) {
                    $('#ShowAddSubjects').modal("hide");
                    get = false
                }
                else
                    $('#ShowEnrolledStudents').modal("hide");

                $('#ShowAddPoints .modal-content').html(data);
                $('#ShowAddPoints').modal("show");

            }
        }).fail(function (jqXHR, textStatus, error) {
            HideSpinner();
        });
    }
    else
        toastr.warning(AddSubjectMessage);
}

$('#ShowAddPoints').on('hidden.bs.modal', function () {
    get = true;
    ShowEnrolledStudents($("#PracticalEnrollmentExamId").val(), $("#TecherCourseId").val())
});


function OpenPracticalDelete(id, count) {
    if (count == 0) {
        $("#DeleteId").val(id);
        $("#DeleteModal").modal("show");
        $("#Delete_Modal").attr('onclick',"PracticalDelete()" )
    }
    else
        toastr.warning(DeleteExamMessage);
}

function PracticalDelete() {
    $.ajax({
        type: "POST",
        url: "/ControlPanel/PracticalEnrollmentExam/Delete",
        data: {
            id: $("#DeleteId").val()
        },
        headers: {
            'RequestVerificationToken': Token
        },
        dataType: "json",
        success: function (result) {
            toastr.success(deleteMassege);
            GetPracticalExams();
            $("#DeleteModal").modal("hide");
        },
        error: function (req, status, error) {
            toastr.error(deleteMassegeError);
        }
    });
}

function MarkPracticalExamAdoption(id, adopt) {
    $.ajax({
        type: "Post",
        url: "/ControlPanel/PracticalEnrollmentExam/MarkAdoption/",
        data: { id, adopt },
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            HideSpinner();
            toastr.success(editMassege);
            GetPracticalExams();
        },
        error: function (data) {
            HideSpinner();
            toastr.error(editMassegeError);
        },
    });
}

function GetEnrolledStudentsDataForExam(data) {
    $('#ShowEnrolledStudents .modal-content').html(data);
}