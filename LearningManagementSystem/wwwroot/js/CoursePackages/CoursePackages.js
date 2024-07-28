function Get() {
    var URL = "/ControlPanel/CoursePackages/GetData";
    if ($("#CoursePackagesCurrentUrl").val())
        URL = $("#CoursePackagesCurrentUrl").val();

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

function ShowAddCoursePackages(langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CoursePackages/ShowCreate/",
        data: { languageId: langId },
        success: function (data) {
            $('#EditCoursePackagesDiv').empty();
            HideSpinner();
            $('#CreateCoursePackages .modal-content').html(data);
            $('#CreateCoursePackages').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowEditTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CoursePackages/ShowTable/",
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

var CoursePackagesFrm = $('#CoursePackages-Create');
CoursePackagesFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: CoursePackagesFrm.attr('method'),
        url: CoursePackagesFrm.attr('action'),
        data: CoursePackagesFrm.serialize(),
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
                $('#CreateCoursePackages').modal("hide");
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

function ShowDetailsCoursePackages(CoursePackageId, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CoursePackages/ShowDetails/",
        data: { CoursePackageId: CoursePackageId, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#DetailsCoursePackages .modal-content').html(data);
            $('#DetailsCoursePackages').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}
function onDdlChangedForCreate(langId) {
    ShowAddCoursePackages(langId.value);
}

function ShowDeleteCoursePackages(CoursePackageId, langId, page) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CoursePackages/ShowDelete/",
        data: { CoursePackageId: CoursePackageId, languageId: langId, page: page },
        success: function (data) {
            HideSpinner();
            $('#DeleteCoursePackages .modal-content').html(data);
            $('#DeleteCoursePackages').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function ConfirmDelete(id, page) {
    ShowSpinner();
    $.post("/ControlPanel/CoursePackages/DeleteCoursePackages/",
        { id: id, page: page },
        function (data) {
            if (data !== "Fail") {
                toastr.success(deleteMassege);
                HideSpinner();
                $('#DeleteCoursePackages').modal("hide");
                Get()
            } else {
                HideSpinner();
                toastr.error(deleteMassegeError);
            }
        });
}




var CoursePackagesEditFrm = $('#CoursePackages-Edit');
CoursePackagesEditFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: CoursePackagesEditFrm.attr('method'),
        url: CoursePackagesEditFrm.attr('action'),
        data: CoursePackagesEditFrm.serialize(),
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
                $('#EditCoursePackages').modal("hide");
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

function ShowEditCoursePackages1(CoursePackageId, langId ,show = true) {
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CoursePackages/ShowEdit/",
        data: { CoursePackageId: CoursePackageId, languageId: langId },
        success: function (data) {
            $('#CreateCoursePackagesDiv').empty();
            $('#EditCoursePackages .modal-content').html(data);
            $('#CoursePackagesCourseId').val(CoursePackageId);
            if (show)
                $('#EditCoursePackages').modal("show");
        }

    });
}
function onDdlChangedGetEdit(langId) {
    ShowEditCoursePackages1($('#CoursePackagesCourseId').val(), langId.value ,false);
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
        url: "/ControlPanel/CoursePackages/DeleteCoursePackages",
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