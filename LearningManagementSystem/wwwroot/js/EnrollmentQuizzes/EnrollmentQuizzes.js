function ShowStudentForQuiz(quizId, enrollTeacherCourseId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/EnrollCourseQuiz/ShowEnrolledStudents/",
        data: { quizId, enrollTeacherCourseId },
        success: function (data) {
            HideSpinner();
            $('#ShowStudentForQuiz .modal-content').html(data);
            $('#ShowStudentForQuiz').modal("show");

        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

$('#ShowStudentForQuiz').on('hidden.bs.modal', function () {
    GetQuizzesData($("#EnrollTeacherCourseId").val());
});

function GetEnrolledStudentsData(data) {
    $('#ShowStudentForQuiz .modal-content').html(data);
}

function SetMark(current, quizId, enrollStudentCourseId, num) {
    ShowSpinner();
    $.ajax({
        type: "POSt",
        url: "/ControlPanel/EnrollCourseQuiz/SubmitMark",
        data: {
            value: current.value, quizId, enrollStudentCourseId, num
        },
        success: function (data) {
            HideSpinner();
            toastr.success(addMassege);
            SetColor(current)
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
        toastr.error(addMassegeError);
        $(current).val('');
    });
}

function ShowStudentForMarks(enrollTeacherCourseId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/EnrollCourseQuiz/ShowEnrolledStudentsForMarks/",
        data: {enrollTeacherCourseId },
        success: function (data) {
            HideSpinner();
            $('#ShowStudentForQuiz .modal-content').html(data);
            $('#ShowStudentForQuiz').modal("show");

        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowStudentQuizMarks(enrollStudentCourseId, enrollTeacherCourseId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/EnrollCourseQuiz/ShowStudentQuizMarks/",
        data: { enrollStudentCourseId, enrollTeacherCourseId },
        success: function (data) {
            HideSpinner();
            $('#ShowStudentForQuiz').modal("hide");
            $('#ShowStudentQuizMarks .modal-content').html(data);
            $('#ShowStudentQuizMarks').modal("show");

        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

$('#ShowStudentQuizMarks').on('hidden.bs.modal', function () {
    ShowStudentForMarks($("#EnrollTeacherCourseId").val());
});