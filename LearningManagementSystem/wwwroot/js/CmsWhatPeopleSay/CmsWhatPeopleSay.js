function Get() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CmsWhatPeopleSay/GetData",
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

function ShowAddCmsWhatPeopleSay(langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CmsWhatPeopleSay/ShowCreate/",
        success: function (data) {
            HideSpinner();
            $('#CreateCmsWhatPeopleSay .modal-content').html(data);
            $('#CreateCmsWhatPeopleSay').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    }); 
}

function ShowEditTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CmsWhatPeopleSay/ShowTable/",
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

var CmsWhatPeopleSayFrm = $('#CmsWhatPeopleSay-Create');
CmsWhatPeopleSayFrm.submit(function (e) {
    e.preventDefault();
    $('#Description').val(tinymce.get("Description").getContent())
    if ($('#Description').val()) {
        ShowSpinner();
        $.ajax({
            type: CmsWhatPeopleSayFrm.attr('method'),
            url: CmsWhatPeopleSayFrm.attr('action'),
            data: CmsWhatPeopleSayFrm.serialize(),
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                toastr.success(addMassege);
                $('#CreateCmsWhatPeopleSay').modal("hide");
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

function ShowDetailsCmsWhatPeopleSay(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CmsWhatPeopleSay/ShowDetails/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#DetailsCmsWhatPeopleSay .modal-content').html(data);
            $('#DetailsCmsWhatPeopleSay').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function ShowDeleteCmsWhatPeopleSay(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CmsWhatPeopleSay/ShowDelete/",
        data: { id: id, languageId: langId},
        success: function (data) {
            HideSpinner();
            $('#DeleteCmsWhatPeopleSay .modal-content').html(data);
            $('#DeleteCmsWhatPeopleSay').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ConfirmDelete(id) {
    ShowSpinner();
    $.post("/ControlPanel/CmsWhatPeopleSay/DeleteCmsWhatPeopleSay/",
        { id: id},
        function (data) {
            if (data !== null) {
                toastr.success(deleteMassege);
                HideSpinner();
                $('#DeleteCmsWhatPeopleSay').modal("hide");
                Get()
            } else {
                HideSpinner();
                toastr.error(deleteMassegeError);
            }
        });
}

function onDdlChanged(langId) {
    
    ShowEditCmsWhatPeopleSay($('#CmsWhatPeopleSayId').val(), langId.value);
}

function onDdlChangedForCreate(langId) {
    ShowAddCmsWhatPeopleSay(langId.value);
}
function ShowImageView(ImageUrl) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CmsWhatPeopleSay/ShowImage/",
        data: { ImageUrl: ImageUrl},
        success: function (data) {
            HideSpinner();
            $('#ShowImageCmsWhatPeopleSay .modal-content').html(data);
            $('#ShowImageCmsWhatPeopleSay').modal("show");
        }

    });
}

var CmsWhatPeopleSayEditFrm = $('#CmsWhatPeopleSay-Edit');
CmsWhatPeopleSayEditFrm.submit(function (e) {
    e.preventDefault();
    $('#Description').val(tinymce.get("Description").getContent())
    if ($('#Description').val()) {
        ShowSpinner();
        $.ajax({
            type: CmsWhatPeopleSayEditFrm.attr('method'),
            url: CmsWhatPeopleSayEditFrm.attr('action'),
            data: CmsWhatPeopleSayEditFrm.serialize(),
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                toastr.success(editMassege);
                $('#EditCmsWhatPeopleSay').modal("hide");
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

function ShowEditCmsWhatPeopleSay(id, langId) {
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CmsWhatPeopleSay/ShowEdit/",
        data: { id: id, languageId: langId },
        success: function (data) {
            $('#EditCmsWhatPeopleSay .modal-content').html(data);
            $('#EditCmsWhatPeopleSay').modal("show");
        }

    });
}

function GetData(data) {
    $('#main').html(data);
}
