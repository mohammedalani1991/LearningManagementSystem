function Get() {
    var URL = "/ControlPanel/CoursePrerequisite/GetData";
    if ($("#CoursePrerequisiteCurrentUrl").val())
        URL = $("#CoursePrerequisiteCurrentUrl").val();

    ShowSpinner();
    $.ajax({
        type: "GET",
        url: URL,
        dataType: 'html',
        data: { searchText: $("#searchText").val(), page: $("#page").val() },
        success: function (data) {
            HideSpinner();
            $('#main').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

Get();

function ShowAddCoursePrerequisite(langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CoursePrerequisite/ShowCreate/",
        data: { languageId: langId },
        success: function (data) {
            $('#EditCoursePrerequisiteDiv').empty(); 
            HideSpinner();
            $('#CreateCoursePrerequisite .modal-content').html(data);
            $('#CreateCoursePrerequisite').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    }); 
}

function ShowEditTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CoursePrerequisite/ShowTable/",
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

var CoursePrerequisiteFrm = $('#CoursePrerequisite-Create');
CoursePrerequisiteFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: CoursePrerequisiteFrm.attr('method'),
        url: CoursePrerequisiteFrm.attr('action'),
        data: CoursePrerequisiteFrm.serialize(),
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            if (data == "AddMassegeErrorInvalidData") {
                toastr.error(AddMassegeErrorInvalidData);
                HideSpinner();
            } else if (data == "Fail") {
                toastr.error(addMassegeError);
                HideSpinner();
            } else {
                toastr.success(addMassege);
                $('#CreateCoursePrerequisite').modal("hide");
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

function ShowDetailsCoursePrerequisite(CourseId, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CoursePrerequisite/ShowDetails/",
        data: { CourseId: CourseId, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#DetailsCoursePrerequisite .modal-content').html(data);
            $('#DetailsCoursePrerequisite').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}
function onDdlChangedForCreate(langId) {
    ShowAddCoursePrerequisite(langId.value);
}

function ShowDeleteCoursePrerequisite(CourseId, langId,page) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CoursePrerequisite/ShowDelete/",
        data: { CourseId: CourseId, languageId: langId, page: page},
        success: function (data) {
            HideSpinner();
            $('#DeleteCoursePrerequisite .modal-content').html(data);
            $('#DeleteCoursePrerequisite').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function ConfirmDelete(id,page) {
    ShowSpinner();
    $.post("/ControlPanel/CoursePrerequisite/DeleteCoursePrerequisite/",
        { id: id, page: page},
        function (data) {
            if (data !== null) {
                toastr.success(deleteMassege);
                HideSpinner();
                $('#DeleteCoursePrerequisite').modal("hide");
                Get()
            } else {
                HideSpinner();
                toastr.error(deleteMassegeError);
            }
        });
}




var CoursePrerequisiteEditFrm = $('#CoursePrerequisite-Edit');
CoursePrerequisiteEditFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: CoursePrerequisiteEditFrm.attr('method'),
        url: CoursePrerequisiteEditFrm.attr('action'),
        data: CoursePrerequisiteEditFrm.serialize(),
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            if (data == "AddMassegeErrorInvalidData") {
                toastr.error(AddMassegeErrorInvalidData);
                HideSpinner();
            } else if (data == "Fail") {
                toastr.error(editMassegeError);
                HideSpinner();
            } else {
                toastr.success(editMassege);
                $('#EditCoursePrerequisite').modal("hide");
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

function ShowEditCoursePrerequisite(CourseId, langId, page) {
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CoursePrerequisite/ShowEdit/",
        data: { CourseId: CourseId, languageId: langId, page:page },
        success: function (data) {
            $('#CreateCoursePrerequisiteDiv').empty(); 
            $('#EditCoursePrerequisite .modal-content').html(data);
            $('#EditCoursePrerequisite').modal("show");
        }

    });
}
function onDdlChanged(langId) {
    var page = $('#Page').val();
    ShowEditCoursePrerequisite($('#CoursePrerequisiteCourseId').val(), langId.value, page);
}

function BtnSearchRestPageNumber() {
    $('#Page').val("0");
}
function GetData(data) {
    $('#main').html(data);
}


function OpenDelete(id) {
    $("#DeleteId").val(id);
    $("#DeleteModal").modal("show");
}

function Delete() {
    $.ajax({
        type: "POST",
        url: "/ControlPanel/CoursePrerequisite/Delete",
        data: {
            id: $("#DeleteId").val()
        },
        headers: {
            'RequestVerificationToken': Token
        },
        dataType: "json",
        success: function (result) {
            toastr.success(deleteMassege);
            Get()
            $("#DeleteModal").modal("hide");
        },
        error: function (req, status, error) {
            toastr.error(deleteMassegeError);
        }
    });
}