function Get() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CmsPage/GetData",
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

function ShowAddCmsPage(langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CmsPage/ShowCreate/",
        data: { languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#EditCmsPageDiv').empty();
            $('#CreateCmsPage .modal-content').html(data);
            $('#CreateCmsPage').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    }); 
}

function ShowEditTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CmsPage/ShowTable/",
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

var CmsPageFrm = $('#CmsPage-Create');
CmsPageFrm.submit(function (e) {
    e.preventDefault();
    $('#Description').val(tinymce.get("Description").getContent())
    if ($('#Description').val()) {
        ShowSpinner();
        $.ajax({
            type: CmsPageFrm.attr('method'),
            url: CmsPageFrm.attr('action'),
            data: CmsPageFrm.serialize(),
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                toastr.success(addMassege);
                $('#CreateCmsPage').modal("hide");
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

function ShowDetailsCmsPage(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CmsPage/ShowDetails/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#DetailsCmsPage .modal-content').html(data);
            $('#DetailsCmsPage').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function ShowDeleteCmsPage(id, langId,page) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CmsPage/ShowDelete/",
        data: { id: id, languageId: langId, page: page},
        success: function (data) {
            HideSpinner();
            $('#DeleteCmsPage .modal-content').html(data);
            $('#DeleteCmsPage').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ConfirmDelete(id,page) {
    ShowSpinner();
    $.post("/ControlPanel/CmsPage/DeleteCmsPage/",
        { id: id, page: page},
        function (data) {
            if (data !== null) {
                toastr.success(deleteMassege);
                HideSpinner();
                $('#DeleteCmsPage').modal("hide");
                Get()
            } else {
                HideSpinner();
                toastr.error(deleteMassegeError);
            }
        });
}

function onDdlChanged(langId) {
    var page = $('#Page').val();
    ShowEditCmsPage($('#CmsPageId').val(), langId.value,page);
}
function onDdlChangedForCreate(langId) {
    ShowAddCmsPage(langId.value);
}

function ShowImageView(ImageUrl) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CmsPage/ShowImage/",
        data: { ImageUrl: ImageUrl},
        success: function (data) {
            HideSpinner();
            $('#ShowImageCmsPage .modal-content').html(data);
            $('#ShowImageCmsPage').modal("show");
        }

    });
}

var CmsPageEditFrm = $('#CmsPage-Edit');
CmsPageEditFrm.submit(function (e) {
    e.preventDefault();
    $('#Description').val(tinymce.get("Description").getContent())
    if ($('#Description').val()) {
        ShowSpinner();
        $.ajax({
            type: CmsPageEditFrm.attr('method'),
            url: CmsPageEditFrm.attr('action'),
            data: CmsPageEditFrm.serialize(),
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                toastr.success(editMassege);
                $('#EditCmsPage').modal("hide");
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

function ShowEditCmsPage(id, langId, page) {
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CmsPage/ShowEdit/",
        data: { id: id, languageId: langId, page:page },
        success: function (data) {
            $('#CreateCmsPageDiv').empty();
            $('#EditCmsPage .modal-content').html(data);
            $('#EditCmsPage').modal("show");
        }

    });
}
function BtnSearchRestPageNumber() {
    $('#page').val("0");
}
function GetData(data) {
    $('#main').html(data);
}
