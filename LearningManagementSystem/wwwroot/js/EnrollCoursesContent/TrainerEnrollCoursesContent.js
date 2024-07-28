function Get() {
    var URL = "/Trainer/EnrollCoursesContent/GetData";
    if ($("#EnrollCoursesContentCurrentUrl").val())
        URL = $("#EnrollCoursesContentCurrentUrl").val();

    ShowSpinner();
    $.ajax({
        type: "GET",
        url: URL,
        dataType: 'html',
        success: function (data) {
            HideSpinner();
            $('#UserProfile_CoursesContent').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function ShowAddCoursesContent(CourseId, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Trainer/EnrollCoursesContent/ShowCreate/",
        data: { EnrollTeacherCourseId: CourseId, languageId: langId },
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
        url: "/Trainer/EnrollCoursesContent/ShowTable/",
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
            } else if (data == "Fail") {
                HideSpinner();
                toastr.error(addMassegeError);
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
        url: "/Trainer/EnrollCoursesContent/ShowDetails/",
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
function onDdlChangedForCreate(CourseId,langId) {
    ShowAddCoursesContent(CourseId,langId.value);
}

function ShowDeleteCoursesContent(id, langId,page) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Trainer/EnrollCoursesContent/ShowDelete/",
        data: { id: id, languageId: langId, page: page},
        success: function (data) {
            HideSpinner();
            $('#DeleteCoursesContent .modal-content').html(data);
            $('#DeleteCoursesContent').modal("show");
           
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}
function ShowLecturesContent(id, langId, page) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Trainer/EnrollLecturesContent/ShowEdit/",
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

function ConfirmDelete(id,page) {
    ShowSpinner();
    $.post("/Trainer/EnrollCoursesContent/DeleteCoursesContent/",
        { id: id, page: page},
        function (data) {
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
    ShowEditCoursesContent($('#SectionId').val(), langId.value,page);
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
            } else if (data == "Fail") {
                HideSpinner();
                toastr.error(editMassegeError);
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

function ShowEditCoursesContent(id, langId, page) {
    $.ajax({
        type: "GET",
        url: "/Trainer/EnrollCoursesContent/ShowEdit/",
        data: { id: id, languageId: langId, page:page },
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
    }, 500);
});

function BtnSearchRestPageNumber() {
    $('#Page').val("0");
}
function GetData(data) {
    $('#UserProfile_CoursesContent').html(data);
}
