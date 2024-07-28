function Get() {
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CmsProject/GetData",
        dataType: 'html',
        success: function (data) {
            $('#main').html(data);
        }
    });
}

Get();

function ShowAddCmsProject() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CmsProject/ShowCreate/",
        success: function (data) {
            if (data !== null) {
                $('#CreateCmsProject .modal-content').html(data);
                $('#CreateCmsProject').modal("show");
            }
            HideSpinner();

        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

$('#CreateCmsProject').on('hidden.bs.modal', function () {
    $('#CreateCmsProject .modal-content').empty();
})

function ShowEditTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CmsProject/ShowTable/",
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


var CmsProjectFrm = $('#CmsProject-Create');
CmsProjectFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: CmsProjectFrm.attr('method'),
        url:  CmsProjectFrm.attr('action'),
        data: CmsProjectFrm.serialize(),
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            toastr.success(addMassege);
            $('#CreateCmsProject').modal("hide");
            HideSpinner();
                Get()
        },
        error: function (data) {
            HideSpinner();
            toastr.error(addMassegeError);
        },
    });
});

function ShowDetailsCmsProject(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CmsProject/ShowDetails/",
        data: { id: id, languageId: langId },
        success: function (data) {
            if (data !== null) {
                $('#DetailsCmsProject .modal-content').html(data);
                $('#DetailsCmsProject').modal("show");
            }
            HideSpinner();
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

$('#DetailsCmsProject').on('hidden.bs.modal', function () {
    $('#DetailsCmsProject .modal-content').empty();
})

function ShowDeleteCmsProject(id) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CmsProject/ShowDelete/",
        data: { id: id },
        success: function (data) {
            if (data !== null) {
                $('#DeleteCmsProject .modal-content').html(data);
                $('#DeleteCmsProject').modal("show");
            }
            HideSpinner();
        }

    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

$('#DeleteCmsProject').on('hidden.bs.modal', function () {
    $('#DeleteCmsProject .modal-content').empty();
})


function ConfirmDelete(id) {
    ShowSpinner();
    $.post("/ControlPanel/CmsProject/DeleteCmsProject/",
        { id: id },
        function (data) {
            if (data !== null) {
                toastr.success(deleteMassege);
                $('#DeleteCmsProject').modal("hide");
                    HideSpinner();
                    Get()
            }
            else {
                HideSpinner();
                toastr.error(deleteMassegeError);
            }
        });
}

function onDdlChanged(langId) {
    ShowEditCmsProject($('#CmsProjectId').val(), langId.value);
}


var CmsProjectEditFrm = $('#CmsProject-Edit');
CmsProjectEditFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: CmsProjectEditFrm.attr('method'),
        url: CmsProjectEditFrm.attr('action'),
        data: CmsProjectEditFrm.serialize(),
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            toastr.success(editMassege);
            $('#EditCmsProject').modal("hide");
            HideSpinner();
                Get()
        },
        error: function (data) {
            HideSpinner();
            toastr.error(editMassegeError);
        },
    });
});

function ShowEditCmsProject(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CmsProject/ShowEdit/",
        data: { id: id, languageId: langId },
        success: function (data) {
            if (data !== null) {

                $('#EditCmsProject .modal-content').html(data);
                $('#EditCmsProject').modal("show");
            }
            HideSpinner();
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

$('#EditCmsProject').on('hidden.bs.modal', function () {
    $('#EditCmsProject .modal-content').empty();
})

function ShowImageView(ImageUrl) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CmsProject/ShowImage/",
        data: { ImageUrl: ImageUrl },
        success: function (data) {
            HideSpinner();
            $('#ShowImageCmsProject .modal-content').html(data);
            $('#ShowImageCmsProject').modal("show");
        }

    });
}

$('#ShowImageCmsProject').on('hidden.bs.modal', function () {
    $('#ShowImageCmsProject .modal-content').empty();
})

function GetData(data) {
    $('#main').html(data);
}

function ShowEditSource(id) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CmsProject/ShowEditSource/",
        data:{id :id},
        success: function (data) {
            if (data !== null) {
                $('#EditCmsProjectSource .modal-content').html(data);
                $('#EditCmsProjectSource').modal("show");
            }
            HideSpinner();

        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

$('#EditCmsProjectSource').on('hidden.bs.modal', function () {
    $('#EditCmsProjectSource .modal-content').empty();
})

function ShowEditCosts(id, LanguageId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CmsProject/ShowEditProjectCosts/",
        data: { id: id, LanguageId: LanguageId },
        success: function (data) {
            if (data !== null) {
                $('#EditCmsProjectCosts .modal-content').html(data);
                $('#EditCmsProjectCosts').modal("show");
            }
            HideSpinner();

        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

$('#EditCmsProjectCosts').on('hidden.bs.modal', function () {
    $('#EditCmsProjectCosts .modal-content').empty();
})