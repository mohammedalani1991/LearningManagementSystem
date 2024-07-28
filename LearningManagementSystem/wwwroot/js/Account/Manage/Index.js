function ShowPassword() {
    $.ajax({
        type: "Get",
        url: "/Identity/Account/Manage/ChangePassword",
        data: {},

        success: function (data) {

            $('#Password').html(data);

        },
        error: function (data) {
        },
    });
}
function ShowChangeEmail() {
    $.ajax({
        type: "Get",
        url: "/Identity/Account/Manage/Email",
        data: {},

        success: function (data) {

            $('#E-Mail').html(data);

        },
        error: function (data) {
        },
    });
}

function openprof(evt, profile) {
    if (profile == "Calendar")
        GetCalendar()
    else if (profile == "Certificates")
        GetCertificates()

    var i, tabcontent, tablinks;
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }
    document.getElementById(profile).style.display = "block";
    evt.currentTarget.className += " active";
    document.getElementById("nameprofile").innerHTML = evt.currentTarget.innerHTML;
}


function openCourse(evt, courseDetails) {
    var i, tabcontent, tablinks;
    tabcontent = document.getElementsByClassName("tabcontentt");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }
    tablinks = document.getElementsByClassName("tablinkss");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }
    document.getElementById(courseDetails).style.display = "block";
    evt.currentTarget.className += " active";
}

document.getElementById("defaultOpen").click();

window.addEventListener('load', function () {
    document.querySelector('input[type="file"]').addEventListener('change', function () {
        if (this.files && this.files[0]) {
            var img = document.getElementById('myImg');
            img.onload = () => {
                URL.revokeObjectURL(img.src);
            }
            img.src = URL.createObjectURL(this.files[0]);
        }
    });
});
document.getElementById('button-file').addEventListener('click', (e) => {
    e.preventDefault();
    document.getElementById('input-file').click();
})


function GetTrainerHomePageCoursesPackages(CoursesPackagesID, EnrollTeacherCourseId) {
    ShowSpinner();
    $.ajax({
        type: "Get",
        data: { CoursesPackagesID: CoursesPackagesID, EnrollTeacherCourseId: EnrollTeacherCourseId },
        url: "/Trainer/CourseHome/IndexCoursesPackages",
        success: function (data) {
            HideSpinner();
            $('#MyCoursesPackagesData').html("");
            $('#MyCoursesData').html(data);
        },
        error: function (data) {
            HideSpinner();
        },
    });
}


function GetTrainerHomePage(EnrollTeacherCourseId) {
    ShowSpinner();
    $.ajax({
        type: "Get",
        data: { EnrollTeacherCourseId: EnrollTeacherCourseId },
        url: "/Trainer/CourseHome/Index",
        success: function (data) {
            HideSpinner();
            $('#MyCoursesPackagesData').html("");
            $('#MyCoursesData').html(data);
        },
        error: function (data) {
            HideSpinner();
        },
    });
}

function GetSupportCourse(EnrollTeacherCourseId) {
    ShowSpinner();
    $.ajax({
        type: "Get",
        data: { EnrollTeacherCourseId: EnrollTeacherCourseId },
        url: "/Trainer/CourseHome/GetSupportCourse",
        success: function (data) {
            HideSpinner();
            $('#MySupportCourses').html(data);
        },
        error: function (data) {
            HideSpinner();
        },
    });
}

function GetTrainerViewCourses() {
    $.ajax({
        type: "Get",
        url: "/Trainer/EnrollCourses/GetData",
        success: function (data) {
            HideSpinner();
            $('#MyCoursesData').html(data);
        },
        error: function (data) {

        },
    });
}

function GetTrainerViewSupportCourses() {
    $.ajax({
        type: "Get",
        url: "/Trainer/EnrollCourses/GetSupportData",
        success: function (data) {
            HideSpinner();
            $('#MySupportCourses').html(data);
        },
        error: function (data) {

        },
    });
}

function GetTrainerViewCoursePackages() {
    $.ajax({
        type: "Get",
        url: "/Trainer/EnrollCourses/GetCoursesPackagesData",
        success: function (data) {
            HideSpinner();
            $('#MyCoursesPackagesData').html(data);
        },
        error: function (data) {

        },
    });
}

function GetDetails(CourseId) {
    ShowSpinner();
    $.ajax({
        type: "Get",
        url: "/Trainer/CourseHome/Details",
        data: { id: CourseId },
        success: function (data) {
            HideSpinner();
            $('html, body').animate({ scrollTop: 0 }, 'fast');
            $('#MyCourses #Details').html(data);
            $('#MyCourses #UserProfile_Exams').empty();
            $('#MyCourses #UserProfile_Students').empty();
            $('#MyCourses #UserProfile_CoursesContent').empty();
            $('#MyCourses #UserProfile_Assignments').empty();
            $('#MyCourses #UserProfile_Quizzes').empty();
            $('#MyCourses #UserProfile_Attendances').empty();
            $('#MyCourses #UserProfile_TeacherAttendances').empty();
        },
        error: function (data) {
            HideSpinner();
        },
    });
}

function GetSupportDetails(CourseId) {
    ShowSpinner();
    $.ajax({
        type: "Get",
        url: "/Trainer/CourseHome/Details",
        data: { id: CourseId },
        success: function (data) {
            HideSpinner();
            $('html, body').animate({ scrollTop: 0 }, 'fast');
            $('#MySupportCourses #Details').html(data);
        },
        error: function (data) {
            HideSpinner();
        },
    });
}

function GetTrainerEnrollCoursesContent(CourseId) {
    ShowSpinner();
    $.ajax({
        type: "Get",
        url: "/Trainer/EnrollCoursesContent/GetData",
        data: { CourseId: CourseId },
        success: function (data) {
            HideSpinner();
            $('#MyCourses #Details').empty();
            $('#MyCourses #UserProfile_Exams').empty();
            $('#MyCourses #UserProfile_Students').empty();
            $('#MyCourses #UserProfile_CoursesContent').html(data);
            $('#MyCourses #UserProfile_PracticalExams').empty();
            $('#MyCourses #UserProfile_Assignments').empty();
            $('#MyCourses #UserProfile_Quizzes').empty();
            $('#MyCourses #UserProfile_Attendances').empty();
            $('#MyCourses #UserProfile_TeacherAttendances').empty();
        },
        error: function (data) {
            HideSpinner();
        },
    });
}

function GetTrainerEnrollStudents(CourseId) {
    ShowSpinner();
    $.ajax({
        type: "Get",
        url: "/Trainer/EnrollStudents/GetData",
        data: { CourseId: CourseId },
        success: function (data) {
            HideSpinner();
            $('#MyCourses #Details').empty();
            $('#MyCourses #UserProfile_Exams').empty();
            $('#MyCourses #UserProfile_Students').html(data);
            $('#MyCourses #UserProfile_CoursesContent').empty();
            $('#MyCourses #UserProfile_PracticalExams').empty();
            $('#MyCourses #UserProfile_Assignments').empty();
            $('#MyCourses #UserProfile_Quizzes').empty();
            $('#MyCourses #UserProfile_Attendances').empty();
            $('#MyCourses #UserProfile_TeacherAttendances').empty();
        },
        error: function (data) {
            HideSpinner();
        },
    });
}

function GetTrainerEnrollCourseExam(CourseId) {
    ShowSpinner();
    $.ajax({
        type: "Get",
        url: "/Trainer/EnrollCourseExam/GetData",
        data: { CourseId: CourseId },
        success: function (data) {
            HideSpinner();
            $('#MyCourses #Details').empty();
            $('#MyCourses #UserProfile_Students').empty();
            $('#MyCourses #UserProfile_CoursesContent').empty();
            $('#MyCourses #UserProfile_Exams').html(data);
            $('#MyCourses #UserProfile_PracticalExams').empty();
            $('#MyCourses #UserProfile_Assignments').empty();
            $('#MyCourses #UserProfile_Quizzes').empty();
            $('#MyCourses #UserProfile_Attendances').empty();
            $('#MyCourses #UserProfile_TeacherAttendances').empty();
        },
        error: function (data) {
            HideSpinner();
        },
    });
}

function GetPracticalExams(CourseId) {
    ShowSpinner();
    $.ajax({
        type: "Get",
        url: "/Trainer/EnrollPracticalExam/GetPracticalExams",
        data: { TecherCourseId: CourseId },
        success: function (data) {
            HideSpinner();
            $('#MyCourses #Details').empty();
            $('#MyCourses #UserProfile_Students').empty();
            $('#MyCourses #UserProfile_CoursesContent').empty();
            $('#MyCourses #UserProfile_Exams').empty();
            $('#MyCourses #UserProfile_PracticalExams').html(data);
            $('#MyCourses #UserProfile_Assignments').empty();
            $('#MyCourses #UserProfile_Quizzes').empty();
            $('#MyCourses #UserProfile_Attendances').empty();
            $('#MyCourses #UserProfile_TeacherAttendances').empty();
        },
        error: function (data) {
            HideSpinner();
        },
    });
}

function GetSupportPracticalExams(CourseId) {
    ShowSpinner();
    $.ajax({
        type: "Get",
        url: "/Trainer/EnrollPracticalExam/GetSupportPracticalExams",
        data: { TecherCourseId: CourseId },
        success: function (data) {
            HideSpinner();
            $('#MySupportCourses #UserProfile_PracticalExams').html(data);
        },
        error: function (data) {
            HideSpinner();
        },
    });
}

function GetTrainerEnrollAssignments(CourseId) {
    ShowSpinner();
    $.ajax({
        type: "Get",
        url: "/Trainer/EnrollCourseAssignment/GetData",
        data: { CourseId: CourseId },
        success: function (data) {
            HideSpinner();
            $('#MyCourses #Details').empty();
            $('#MyCourses #UserProfile_Students').empty();
            $('#MyCourses #UserProfile_CoursesContent').empty();
            $('#MyCourses #UserProfile_Exams').empty();
            $('#MyCourses #UserProfile_PracticalExams').empty();
            $('#MyCourses #UserProfile_Assignments').html(data);
            $('#MyCourses #UserProfile_Quizzes').empty();
            $('#MyCourses #UserProfile_Attendances').empty();
            $('#MyCourses #UserProfile_TeacherAttendances').empty();
        },
        error: function (data) {
            HideSpinner();
        },
    });
}

function GetTrainerEnrollQuizzes(CourseId) {
    ShowSpinner();
    $.ajax({
        type: "Get",
        url: "/Trainer/EnrollCourseQuiz/GetQuizzes",
        data: { page: $("#Page").val() , id: CourseId },
        success: function (data) {
            HideSpinner();
            $('#MyCourses #Details').empty();
            $('#MyCourses #UserProfile_Students').empty();
            $('#MyCourses #UserProfile_CoursesContent').empty();
            $('#MyCourses #UserProfile_Exams').empty();
            $('#MyCourses #UserProfile_PracticalExams').empty();
            $('#MyCourses #UserProfile_Assignments').empty();
            $('#MyCourses #UserProfile_Quizzes').html(data);
            $('#MyCourses #UserProfile_Attendances').empty();
            $('#MyCourses #UserProfile_TeacherAttendances').empty();
        },
        error: function (data) {
            HideSpinner();
        },
    });
}

function GetAttendances(CourseId) {
    ShowSpinner();
    $.ajax({
        type: "Get",
        url: "/Trainer/Attendances/GetData",
        data: { CourseId: CourseId },
        success: function (data) {
            HideSpinner();
            $('#MyCourses #Details').empty();
            $('#MyCourses #UserProfile_Students').empty();
            $('#MyCourses #UserProfile_CoursesContent').empty();
            $('#MyCourses #UserProfile_Exams').empty();
            $('#MyCourses #UserProfile_PracticalExams').empty();
            $('#MyCourses #UserProfile_Assignments').empty();
            $('#MyCourses #UserProfile_Quizzes').empty();
            $('#MyCourses #UserProfile_Attendances').html(data);
            $('#MyCourses #UserProfile_TeacherAttendances').empty();
        },
        error: function (data) {
            HideSpinner();
        },
    });
}

function GetTeacherAttendances(CourseId) {
    ShowSpinner();
    $.ajax({
        type: "Get",
        url: "/Trainer/Attendances/GetTeacherData",
        data: { CourseId: CourseId },
        success: function (data) {
            HideSpinner();
            $('#MyCourses #Details').empty();
            $('#MyCourses #UserProfile_Students').empty();
            $('#MyCourses #UserProfile_CoursesContent').empty();
            $('#MyCourses #UserProfile_Exams').empty();
            $('#MyCourses #UserProfile_PracticalExams').empty();
            $('#MyCourses #UserProfile_Assignments').empty();
            $('#MyCourses #UserProfile_Quizzes').empty();
            $('#MyCourses #UserProfile_Attendances').empty();
            $('#MyCourses #UserProfile_TeacherAttendances').html(data);
        },
        error: function (data) {
            HideSpinner();
        },
    });
}

function GetStudentEnrollCoursesContent() {
    ShowSpinner();
    $.ajax({
        type: "Get",
        url: "/Student/EnrollCoursesContent/GetData",
        success: function (data) {
            HideSpinner();
            $('#MyCourses #UserProfile_ViewCourses').empty();
            $('#MyCourses #UserProfile_CoursesContent').html(data);
        },
        error: function (data) {
            HideSpinner();
        },
    });
}


function GetStudentHomePage() {
    ShowSpinner();
    $.ajax({
        type: "Get",
        url: "/Student/StudentCourses/GetData",
        success: function (data) {
            HideSpinner();
            $('#MyCourses').html(data);
        },
        error: function (data) {
            HideSpinner();
        },
    });
}

function GetCertificates() {
    ShowSpinner();
    $.ajax({
        type: "Get",
        url: "/Student/StudentCertificates/GetData",
        success: function (data) {
            HideSpinner();
            $('#Certificates').html(data);
        },
        error: function (data) {
            HideSpinner();
        },
    });
}

function GetBalanceHistory() {
    ShowSpinner();
    $.ajax({
        type: "Get",
        url: "/Student/StudentBalanceHistory/GetData",
        success: function (data) {
            HideSpinner();
            $('#MyBalance').html(data);
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
        url: "/Trainer/EnrollCourseQuiz/RefetchQuizzes/",
        data: { id: id },
        success: function (data) {
            toastr.success(addMassege);
            HideSpinner();
            GetTrainerEnrollQuizzes(id)
        },
        error: function (data) {
            HideSpinner();
            toastr.error(addMassegeError);
        },
    });
}

function ShowStudentForQuiz(quizId, enrollTeacherCourseId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Trainer/EnrollCourseQuiz/ShowEnrolledStudents/",
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


function GetEnrolledStudentsData(data) {
    $('#ShowStudentForQuiz .modal-content').html(data);
}

function SetMark(current, quizId, enrollStudentCourseId, num) {
    ShowSpinner();
    $.ajax({
        type: "POSt",
        url: "/Trainer/EnrollCourseQuiz/SubmitMark",
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
        url: "/Trainer/EnrollCourseQuiz/ShowEnrolledStudentsForMarks/",
        data: { enrollTeacherCourseId },
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
        url: "/Trainer/EnrollCourseQuiz/ShowStudentQuizMarks/",
        data: { enrollStudentCourseId, enrollTeacherCourseId },
        success: function (data) {
            HideSpinner();
            get = false;
            $('#ShowStudentForQuiz').modal("hide");
            $('#ShowStudentQuizMarks .modal-content').html(data);
            $('#ShowStudentQuizMarks').modal("show");

        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}
