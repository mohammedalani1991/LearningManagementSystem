function Get() {
    var URL = "/ControlPanel/CourseCategory/GetData";
    if ($("#CourseCategoryCurrentUrl").val())
        URL = $("#CourseCategoryCurrentUrl").val();

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

function ShowAddCourseCategory(langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CourseCategory/ShowCreate/",
        data: { languageId: langId},
        success: function (data) {
            HideSpinner();
            $("#EditCourseCategoryDiv").empty();
            $('#CreateCourseCategory .modal-content').html(data);
            $('#CreateCourseCategory').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    }); 
}

function ShowEditTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CourseCategory/ShowTable/",
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

var CourseCategoryFrm = $('#CourseCategory-Create');
CourseCategoryFrm.submit(function (e) {
    e.preventDefault();
    $('#Description').val(tinymce.get("Description").getContent())
    if ($('#Description').val()) {
        ShowSpinner();
        $.ajax({
            type: CourseCategoryFrm.attr('method'),
            url: CourseCategoryFrm.attr('action'),
            data: CourseCategoryFrm.serialize(),
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                toastr.success(addMassege);
                $('#CreateCourseCategory').modal("hide");
                HideSpinner();
                Get()
            },
            error: function (data) {
                HideSpinner();
                toastr.error(addMassegeError);
            },
        });
    } else {
        $("#Description-error").show();
        $("#Description-error").html("This field is required.");
    }
});

function ShowDetailsCourseCategory(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CourseCategory/ShowDetails/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#DetailsCourseCategory .modal-content').html(data);
            $('#DetailsCourseCategory').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function ShowDeleteCourseCategory(id, langId,page) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CourseCategory/ShowDelete/",
        data: { id: id, languageId: langId, page: page},
        success: function (data) {
            HideSpinner();
            $('#DeleteCourseCategory .modal-content').html(data);
            $('#DeleteCourseCategory').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ConfirmDelete(id,page) {
    ShowSpinner();
    $.post("/ControlPanel/CourseCategory/DeleteCourseCategory/",
        { id: id, page: page},
        function (data) {
            if (data !== null) {
                toastr.success(deleteMassege);
                HideSpinner();
                $('#DeleteCourseCategory').modal("hide");
                Get()
            } else {
                HideSpinner();
                toastr.error(deleteMassegeError);
            }
        });
}

function onDdlChanged(langId) {
    var page = $('#Page').val();
    ShowEditCourseCategory($('#CourseCategoryId').val(), langId.value,page);
}

function onDdlChangedForCreate(langId) {
    ShowAddCourseCategory(langId.value);
}
function ShowImageView(ImageUrl) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CourseCategory/ShowImage/",
        data: { ImageUrl: ImageUrl},
        success: function (data) {
            HideSpinner();
            $('#ShowImageCourseCategory .modal-content').html(data);
            $('#ShowImageCourseCategory').modal("show");
        }

    });
}

var CourseCategoryEditFrm = $('#CourseCategory-Edit');
CourseCategoryEditFrm.submit(function (e) {
    e.preventDefault();
    $('#Description').val(tinymce.get("Description").getContent())
    if ($('#Description').val()) {
        ShowSpinner();
        $.ajax({
            type: CourseCategoryEditFrm.attr('method'),
            url: CourseCategoryEditFrm.attr('action'),
            data: CourseCategoryEditFrm.serialize(),
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                toastr.success(editMassege);
                $('#EditCourseCategory').modal("hide");
                HideSpinner();
                Get();
            },
            error: function (data) {
                HideSpinner();
                toastr.error(editMassegeError);
            },
        });
    } else {
        $("#Description-error").show();
        $("#Description-error").html("This field is required.");
    }
});

function ShowEditCourseCategory(id, langId, page) {
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CourseCategory/ShowEdit/",
        data: { id: id, languageId: langId, page:page },
        success: function (data) {
            $("#CreateCourseCategoryDiv").empty();
            $('#EditCourseCategory .modal-content').html(data);
            $('#EditCourseCategory').modal("show");
        }

    });
}
function BtnSearchRestPageNumber() {
    $('#page').val("0");
}
function GetData(data) {
    $('#main').html(data);
}
