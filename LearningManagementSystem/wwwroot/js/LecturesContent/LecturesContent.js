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


function onDdlChanged(langId) {
    var page = $('#Page').val();
    ShowEditLecturesContent($('#SectionOfCourseViewModel_ForEditModleID').val(), langId.value);
}

var LecturesContentEditFrm = $('#LecturesContent-Edit');
LecturesContentEditFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: LecturesContentEditFrm.attr('method'),
        url: LecturesContentEditFrm.attr('action'),
        data: LecturesContentEditFrm.serialize(),
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
                toastr.error(editMassegeError);
                HideSpinner();
            } else {
                toastr.success(editMassege);
                $('#EditLecturesContent').modal("hide");
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

function ShowEditLecturesContent(id, langId) {
    $.ajax({
        type: "GET",
        url: "/ControlPanel/LecturesContent/ShowEdit/",
        data: { id: id, languageId: langId },
        success: function (data) {
            $('#CreateCoursesContentDiv').empty(); 
            $('#EditCoursesContentDiv').empty(); 

            $('#EditLecturesContent .modal-content').html(data);
            $('#EditLecturesContent').modal("show");
        }

    });
}


function BtnSearchRestPageNumber() {
    $('#Page').val("0");
}
function GetData(data) {
    $('#main').html(data);
}
