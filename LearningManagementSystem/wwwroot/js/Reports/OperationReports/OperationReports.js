$('.tms-reports').addClass("active");
$('.tms-sub-operations-reports').addClass("active");
$('.tms-sub-top-nav  .tms_sub_reports').addClass("active");

$(".tms-nav-link").click(function (event) {
    $(".tms-nav-link").removeClass("active");
    $(".tms-col-nav-item-icon i").removeClass("active");
    $(this).addClass("active");
    $(this).parent().parent().children().eq(1).children().eq(0).addClass("active");
});
function OperationsReportsToggle(){
    if($("#EditCompanyToggle").is(":hidden"))
        return;
    $(".tms-nav-div").toggle();
    $("#myTabContent").toggle();
}
function ViewAttendanceReport() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/OperationsReports/ViewAttendanceReport",
        dataType: 'html',
        success: function (data) {
            $('#GetData').html(data);
            HideSpinner();
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ViewEmployeesAttendanceReport() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/OperationsReports/ViewEmployeesAttendanceReport",
        dataType: 'html',
        success: function (data) {
            HideSpinner();
            $('#GetData').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ViewUnsortedReport() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/OperationsReports/ViewUnsortedReport",
        dataType: 'html',
        success: function (data) {
            HideSpinner();
            $('#GetData').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowStudentAttTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/OperationsReports/ShowStudentAttTable/",
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

function ShowEmployeeAttTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/OperationsReports/ShowEmployeeAttTable/",
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

function ShowUnsortedTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/OperationsReports/ShowUnsortedTable/",
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


function GetData2(data) {
    $("#GetData").html(data);
}

function ShowStudents(id, fetched, unFetched) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/OperationsReports/GetStudents",
        dataType: 'html',
        data: { id, fetched, unFetched },
        success: function (data) {
            HideSpinner();
            $('#Students .modal-content').html(data);
            $("#Students").modal('show');
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowEditStudentTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Reports/OperationsReports/ShowStudentTable/",
        success: function (data) {
            HideSpinner();
            $('#EditTable .modal-content').html(data);
            $(".td").map(function () {
                table1.forEach(element => {
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
    $('#StudentsDiv').html(data);
}

function ChangeTable1() {
    table1 = [];
    $(".TableCheckBox").map(function () {
        if ($(this).is(':checked'))
            table1.push($(this).parent().parent().find(".td").html().trim())
    })
    $("#table1").val(table1);
    $("#Form2").submit();
    $("#EditTable").modal("hide");
}

function ChangePagenation1() {
    $("#pagination1").val($("#pagin1").val())
    $("#Form2").submit();
}



function GetStudentAttendanceGroupByCourse(programId, studentId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentAttendance/GetStudentAttendanceGroupByCourse/",
        data: { programId: programId, studentId: studentId },
        success: function (data) {
            $('#GetStudentAttendanceGroupByCourse .modal-content').html(data);
            $('#GetStudentAttendanceGroupByCourse').modal("show");
            HideSpinner();
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function GetStudentAttendanceForCourse(courseId, programId, studentId) {
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

}
function GetStudentAttendanceForCourseForm() {
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