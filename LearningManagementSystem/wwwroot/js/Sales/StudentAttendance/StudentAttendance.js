function Get() {
    $.ajax({
        type: "GET",
        url: "/Sales/StudentAttendance/GetData",
        dataType: 'html',
        success: function (data) {
            $('#main').html(data);
        }
    });
}


function ShowStudentAttendanceTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentAttendance/ShowTable/",
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

function GetData1(data) {
    $('#GetData').html(data);
    $('.modal-backdrop').hide();

}

function GetStudentAttendanceGroupByCourse(programId, studentId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentAttendance/GetStudentAttendanceGroupByCourse/",
        data: { programId: programId, studentId: studentId },
        success: function (data) {
            HideSpinner();
            $('#GetStudentAttendanceGroupByCourse .modal-content').html(data);
            $('#GetStudentAttendanceGroupByCourse').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function GetStudentAttendanceForCourse(courseId,programId, studentId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentAttendance/GetStudentAttendanceForCourse/",
        data: { programId: programId, studentId: studentId, courseId: courseId },
        success: function (data) {
            HideSpinner();
            $('#GetStudentAttendanceForCourse .modal-content').html(data);
            $('#GetStudentAttendanceForCourse').modal("show");
            $('#GetStudentAttendanceForCourse').show();

            $('#GetStudentAttendanceGroupByCourse').hide();

        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function HideGetStudentAttendanceForCourse() {
    $('#GetStudentAttendanceGroupByCourse').show();
    $('#GetStudentAttendanceForCourse').hide();
    $('.modal-backdrop').first().hide();

}
function HideGetStudentAttendanceGroupByCourse() {
    $('#GetStudentAttendanceGroupByCourse').hide();
    $('#GetStudentAttendanceGroupByCourse').modal('hide');
    $('.modal-backdrop').last().hide();

}

function GetStudentAttendanceGroupByCourseForm() {
    var StudentAttendanceGroupByCourseForm = $('#StudentAttendanceGroupByCourseForm');
    if (StudentAttendanceGroupByCourseForm.valid()) {
        $.ajax({
            type: StudentAttendanceGroupByCourseForm.attr('method'),
            url: StudentAttendanceGroupByCourseForm.attr('action'),
            data: StudentAttendanceGroupByCourseForm.serialize(),
           
            success: function (data) {
                $('#GetStudentAttendanceGroupByCourse .modal-content').html(data);
                $('#GetStudentAttendanceGroupByCourse').modal("show");

            },
            error: function (data) {
                toastr.error(addMassegeError);
            },
        });
    }

} function GetStudentAttendanceForCourseForm() {
    var StudentAttendanceForCourse = $('#StudentAttendanceForCourse');
    if (StudentAttendanceForCourse.valid()) {
        $.ajax({
            type: StudentAttendanceForCourse.attr('method'),
            url: StudentAttendanceForCourse.attr('action'),
            data: StudentAttendanceForCourse.serialize(),
           
            success: function (data) {                
                $('#GetStudentAttendanceForCourse .modal-content').html(data);
                $('#GetStudentAttendanceForCourse').modal("show");

            },
            error: function (data) {
                toastr.error(addMassegeError);
            },
        });
    }
}