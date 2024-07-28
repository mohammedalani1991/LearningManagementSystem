function Get() {
    var URL = "/ControlPanel/Course/GetData";
    if ($("#CourseCurrentUrl").val())
        URL = $("#CourseCurrentUrl").val();

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

function ShowAddCourse(langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Course/ShowCreate/",
        data: { languageId: langId },
        success: function (data) {
            HideSpinner();
            $("#EditCourseDiv").empty();
            $('#CreateCourse .modal-content').html(data);
            $('#CreateCourse').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}
function onDdlChangedForCreate(langId) {
    ShowAddCourse(langId.value);
}
function ShowEditTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Course/ShowTable/",
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

var CourseFrm = $('#Course-Create');
CourseFrm.submit(function (e) {
    e.preventDefault();

    if ($('#NeedQuestion').is(':checked') == true) {

        var QuestionDescription = tinymce.get("QuestionDescription").getContent();
        if (QuestionDescription == '') {
            toastr.error("Question Description is required");

            return;
        }
    }

    ShowSpinner();
    CourseFrm.validate({
        rules: {
            "CategoryId": {
                required: true,
                min: 1
            }
        }
    });
    if (CourseFrm.valid())
        $.ajax({
            type: CourseFrm.attr('method'),
            url: CourseFrm.attr('action'),
            data: CourseFrm.serialize(),
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                toastr.success(addMassege);
                $('#CreateCourse').modal("hide");
                HideSpinner();
                Get()
            },
            error: function (data) {
                HideSpinner();
                toastr.error(addMassegeError);
            },
        });
    else
        HideSpinner();

});

function ShowDetailsCourse(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Course/ShowDetails/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#DetailsCourse .modal-content').html(data);
            $('#DetailsCourse').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function ShowDeleteCourse(id, langId, page) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Course/ShowDelete/",
        data: { id: id, languageId: langId, page: page },
        success: function (data) {
            HideSpinner();
            $('#DeleteCourse .modal-content').html(data);
            $('#DeleteCourse').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ConfirmDelete(id, page) {
    ShowSpinner();
    $.post("/ControlPanel/Course/DeleteCourse/",
        { id: id, page: page },
        function (data) {
            if (data !== null) {
                if (data == "FailEnrollTeacherCourse") {
                    HideSpinner();
                    toastr.error(deleteMassegeErrorEnrollTeacherCourse);

                } else if (data == "Fail") {
                    HideSpinner();
                    toastr.error(deleteMassegeError);

                } else {
                    toastr.success(deleteMassege);
                    HideSpinner();
                    $('#DeleteCourse').modal("hide");
                    Get()
                }

            } else {
                HideSpinner();
                toastr.error(deleteMassegeError);
            }
        });
}

function onDdlChanged(langId) {
    var page = $('#Page').val();
    ShowEditCourse($('#CourseId').val(), langId.value, page);
}


var CourseEditFrm = $('#Course-Edit');
CourseEditFrm.submit(function (e) {
    e.preventDefault();
    if ($('#NeedQuestion').is(':checked') == true) {

        var QuestionDescription = tinymce.get("QuestionDescription").getContent();
        if (QuestionDescription == '') {
            toastr.error("Question Description is required");

            return;
        }
    }
    ShowSpinner();
    CourseEditFrm.validate({
        rules: {
            "CategoryId": {
                required: true,
                min: 1
            }
        }
    });
    if (CourseEditFrm.valid())
        $.ajax({
            type: CourseEditFrm.attr('method'),
            url: CourseEditFrm.attr('action'),
            data: CourseEditFrm.serialize(),
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                toastr.success(editMassege);
                $('#EditCourse').modal("hide");
                HideSpinner();
                Get();
            },
            error: function (data) {
                HideSpinner();
                toastr.error(editMassegeError);
            },
        });
    else
        HideSpinner();

});

function ShowEditCourse(id, langId, page) {
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Course/ShowEdit/",
        data: { id: id, languageId: langId, page: page },
        success: function (data) {
            $("#CreateCourseDiv").empty();
            $('#EditCourse .modal-content').html(data);
            $('#EditCourse').modal("show");
        }

    });
}
function BtnSearchRestPageNumber() {
    $('#page').val("0");
}
function GetData(data) {
    $('#main').html(data);
}

function ShowImageView(ImageUrl) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Course/ShowImage/",
        data: { ImageUrl: ImageUrl },
        success: function (data) {
            HideSpinner();
            $('#ShowImageCourse .modal-content').html(data);
            $('#ShowImageCourse').modal("show");
        }

    });
}

function NeedQuestionChange() {
    if ($('#NeedQuestion').is(':checked') == true) {
        $("#QuestionDescriptionDiv").show();
    } else {
        $("#QuestionDescriptionDiv").hide();
    }
}


function ShowAddExams(id) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Course/GetExams",
        dataType: 'html',
        data: { CourseId: id, searchText1: $("#searchText1").val(), page1: $("#page1").val() },
        success: function (data) {
            HideSpinner();
            $('#AddExams .modal-content').html(data);
            $('#AddExams').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function GetData1(data) {
    $('#AddExams .modal-content').html(data);
}

function ShowSubjects(id) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Course/GetExamsForCourse",
        dataType: 'html',
        data: { CourseId: id, searchText1: $("#searchText1").val(), page1: $("#page1").val() },
        success: function (data) {
            HideSpinner();
            $('#AddExams .modal-content').html(data);
            $('#AddExams').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowAddSubjects(id ,typeId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Course/GetSubjectData",
        dataType: 'html',
        data: { PracticalExamCourseId: id, ExamTypeId: typeId, searchText1: $("#searchText1").val(), page2: $("#page1").val() },
        success: function (data) {
            HideSpinner();
            $('#AddExams').modal("hide");
            $('#AddSubjects .modal-content').html(data);
            $('#AddSubjects').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function GetData2(data) {
    $('#AddSubjects .modal-content').html(data);
}


$('#AddSubjects').on('hidden.bs.modal', function () {
    $('#AddExams').modal("show");
});

function ChangePagenation1() {
    $("#pagination1").val($("#pagin1").val());
    $("#Form2").submit();
}

function ChangePagenation2() {
    $("#pagination2").val($("#pagin2").val());
    $("#Form2").submit();
}

function ShowEditSubjectMark(id) {
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Course/GetSubjectMark",
        data: { PracticalExamCourseId: id},
        success: function (data) {
            HideSpinner();
            $('#AddExams').modal("hide");
            $("#SubjectMarkCourseId").val(data.id);
            $("#SubjectMark").val(data.subjectMark);
            $('#EditSubjectMark').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

$('#EditSubjectMark').on('hidden.bs.modal', function () {
    $('#AddExams').modal("show");
});

function EditSubjectMark(id) {
    ShowSpinner();
    $.ajax({
        type: "POSt",
        url: "/ControlPanel/Course/EditSubjectMark",
        data: { PracticalExamCourseId: $("#SubjectMarkCourseId").val(), mark: $("#SubjectMark").val()},
        success: function (data) {
            HideSpinner();
            toastr.success(editMassege);
            $('#EditSubjectMark').modal("hide");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
        toastr.error(addMassegeError);
    });
}

function ShowRefetch(id) {
    $("#RefetchCourseId").val(id);
    $("#RefetchQuizzesModal").modal("show");
}

function Refetch() {
    ShowSpinner();
    var id = $("#RefetchCourseId").val();
    $.ajax({
        type: "POSt",
        url: "/ControlPanel/Course/RefetchQuizzes",
        data: { id },
        success: function (data) {
            HideSpinner();
            toastr.success(addMassege);
            $("#RefetchQuizzesModal").modal("hide");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
        toastr.error(addMassegeError);
    });
}


function ShowMarks(id) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Course/ShowMarks/",
        data: { id: id },
        success: function (data) {
            HideSpinner();
            $('#CourseMarksModal .modal-content').html(data);
            $('#CourseMarksModal').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}
