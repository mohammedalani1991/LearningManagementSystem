
function GetPracticalExamsData(data) {
    $('#UserProfile_PracticalExams').html(data);
}


function ShowEnrolledStudents(id, techerCourseId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Trainer/EnrollPracticalExam/ShowEnrolledStudents/",
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

function ShowAddSubjects(EnrollStudentCourseId, TecherCourseId, PracticalEnrollmentExamId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Trainer/EnrollPracticalExam/ShowSubjects/",
        data: { EnrollStudentCourseId: EnrollStudentCourseId, TecherCourseId: TecherCourseId, PracticalEnrollmentExamId },
        success: function (data) {
            HideSpinner();
            get = false;
            $('#ShowEnrolledStudents').modal("hide");
            $('#ShowAddSubjects .modal-content').html(data);
            $('#ShowAddSubjects').modal("show");

        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function GetData1(data) {
    $('#ShowAddSubjects .modal-content').html(data);
}


$('#ShowAddSubjects').on('hidden.bs.modal', function () {
    if (get)
        ShowEnrolledStudents($("#PracticalEnrollmentExamId").val(), $("#TecherCourseId").val())
});

$('#ShowEnrolledStudents').on('hidden.bs.modal', function () {
    if (get)
        GetPracticalExams($("#TecherCourseId").val());
});


function ShowAddPoints(EnrollStudentCourseId, PracticalEnrollmentExamId, count, subject) {
    if (subject ? subjectCount > 0 : count > 0) {
        ShowSpinner();
        $.ajax({
            type: "GET",
            url: "/Trainer/EnrollPracticalExam/ShowAddPoints/",
            data: { EnrollStudentCourseId, PracticalEnrollmentExamId },
            success: function (data) {
                HideSpinner();
                if (subject) {
                    $('#ShowAddSubjects').modal("hide");
                }
                else
                    $('#ShowEnrolledStudents').modal("hide");
                get = false;
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

function GetEnrolledStudentsDataForExam(data) {
    $('#ShowEnrolledStudents .modal-content').html(data);
}

function MarkPracticalExamAdoption(id, adopt) {
    $.ajax({
        type: "Post",
        url: "/Trainer/EnrollPracticalExam/MarkAdoption/",
        data: { id, adopt },
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            HideSpinner();
            toastr.success(editMassege);
            GetPracticalExams($("#TecherCourseId").val());
        },
        error: function (data) {
            HideSpinner();
            toastr.error(editMassegeError);
        },
    });
}