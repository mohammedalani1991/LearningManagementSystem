function Get() {
    var URL = "/ControlPanel/CoursesContent/GetData";
    if ($("#CoursesContentCurrentUrl").val())
        URL = $("#CoursesContentCurrentUrl").val();

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

function ShowAddCoursesContent(langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CoursesContent/ShowCreate/",
        data: { languageId: langId },
        success: function (data) {
            $('#EditCoursesContentDiv').empty(); 
            $('#EditLecturesContentDiv').empty(); 
            HideSpinner();
            $('#CreateCoursesContent .modal-content').html(data);
            $('#CreateCoursesContent').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    }); 
}

function ShowEditTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CoursesContent/ShowTable/",
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

var CoursesContentFrm = $('#CoursesContent-Create');
CoursesContentFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: CoursesContentFrm.attr('method'),
        url: CoursesContentFrm.attr('action'),
        data: CoursesContentFrm.serialize(),
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            if (data == "AddMassegeErrorInvalidSectionData") {
                toastr.error(AddMassegeErrorInvalidSectionData);
                HideSpinner();
            } else if (data == "AddMassegeErrorInvalidLectureData") {
                toastr.error(AddMassegeErrorInvalidLectureData);
                HideSpinner();
            } else {
                toastr.success(addMassege);
                $('#CreateCoursesContent').modal("hide");
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

function ShowDetailsCoursesContent(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CoursesContent/ShowDetails/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#DetailsCoursesContent .modal-content').html(data);
            $('#DetailsCoursesContent').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}
function onDdlChangedForCreate(langId) {
    var page = $('#Page').val();
    ShowAddCoursesContent(langId.value);
}

function ShowDeleteCoursesContent(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CoursesContent/ShowDelete/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#DeleteCoursesContent .modal-content').html(data);
            $('#DeleteCoursesContent').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}
function ShowLecturesContent(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/LecturesContent/ShowEdit/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#EditLecturesContent .modal-content').html(data);
            $('#EditLecturesContent').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ConfirmDelete(id) {
    ShowSpinner();
    $.post("/ControlPanel/CoursesContent/DeleteCoursesContent/",
        { id: id },
        function (data) {
            console.log(data);
            if (data !== null) {
                if (data == "Fail") {
                    HideSpinner();
                    toastr.error(deleteMassegeError);
                } else {
                    toastr.success(deleteMassege);
                    HideSpinner();
                    $('#DeleteCoursesContent').modal("hide");
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
    ShowEditCoursesContent($('#SectionId').val(), langId.value);
}


var CoursesContentEditFrm = $('#CoursesContent-Edit');
CoursesContentEditFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: CoursesContentEditFrm.attr('method'),
        url: CoursesContentEditFrm.attr('action'),
        data: CoursesContentEditFrm.serialize(),
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            if (data == "EditMassegeErrorInvalidSectionData") {
                toastr.error(EditMassegeErrorInvalidSectionData);
                HideSpinner();
            } else if (data == "EditMassegeErrorInvalidLectureData") {
                toastr.error(EditMassegeErrorInvalidLectureData);
                HideSpinner();
            } else {
                toastr.success(editMassege);
                $('#EditCoursesContent').modal("hide");
                HideSpinner();
                Get();
            }
        },
        error: function (data) {
            HideSpinner();
            toastr.error(editMassegeError);
        },
    });
});

function ShowEditCoursesContent(id, langId) {
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CoursesContent/ShowEdit/",
        data: { id: id, languageId: langId },
        success: function (data) {
            $('#CreateCoursesContentDiv').empty(); 
            $('#EditLecturesContentDiv').empty(); 
            $('#EditCoursesContent .modal-content').html(data);
            $('#EditCoursesContent').modal("show");
        }

    });
}

$("#CreateCoursesContent,#EditCoursesContent").on('hide.bs.modal', function () {
    setTimeout(function () {
        $('#CreateCoursesContentDiv').empty();
        $('#EditLecturesContentDiv').empty();
    }, 100)
});

function BtnSearchRestPageNumber() {
    $('#Page').val("0");
}
function GetData(data) {
    $('#main').html(data);
}


function ShowQuizzes(id) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CoursesContent/GetQuizzes/",
        data: { id: id },
        success: function (data) {
            HideSpinner();
            $('#ShowQuizzesContent .modal-content').html(data);
            $('#ShowQuizzesContent').modal("show");
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
        url: "/ControlPanel/CoursesContent/RefetchQuizzes/",
        data: { id: id },
        success: function (data) {
            toastr.success(addMassege);
            HideSpinner();
            ShowQuizzes(id)
        },
        error: function (data) {
            HideSpinner();
            toastr.error(addMassegeError);
        },
    });
}