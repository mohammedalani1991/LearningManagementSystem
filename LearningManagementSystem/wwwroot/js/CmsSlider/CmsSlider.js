function Get() {
    var URL = "/ControlPanel/CmsSlider/GetData";
    if ($("#CmsSliderCurrentUrl").val())
        URL = $("#CmsSliderCurrentUrl").val();

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

function ShowAddCmsSlider() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CmsSlider/ShowCreate/",
        success: function (data) {
            HideSpinner();
            $("#EditCmsSliderDiv").empty();
            $('#CreateCmsSlider .modal-content').html(data);
            $('#CreateCmsSlider').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    }); 
}

function ShowEditTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CmsSlider/ShowTable/",
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

var CmsSliderFrm = $('#CmsSlider-Create');
CmsSliderFrm.submit(function (e) {
    e.preventDefault();
    $('#Description').val(tinymce.get("Description").getContent())
    if ($('#Description').val()) {
        ShowSpinner();
        $.ajax({
            type: CmsSliderFrm.attr('method'),
            url: CmsSliderFrm.attr('action'),
            data: CmsSliderFrm.serialize(),
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                toastr.success(addMassege);
                $('#CreateCmsSlider').modal("hide");
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

function ShowDetailsCmsSlider(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CmsSlider/ShowDetails/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#DetailsCmsSlider .modal-content').html(data);
            $('#DetailsCmsSlider').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function ShowDeleteCmsSlider(id, langId,page) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CmsSlider/ShowDelete/",
        data: { id: id, languageId: langId, page: page},
        success: function (data) {
            HideSpinner();
            $('#DeleteCmsSlider .modal-content').html(data);
            $('#DeleteCmsSlider').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ConfirmDelete(id,page) {
    ShowSpinner();
    $.post("/ControlPanel/CmsSlider/DeleteCmsSlider/",
        { id: id, page: page},
        function (data) {
            if (data !== null) {
                toastr.success(deleteMassege);
                HideSpinner();
                $('#DeleteCmsSlider').modal("hide");
                Get()
            } else {
                HideSpinner();
                toastr.error(deleteMassegeError);
            }
        });
}

function onDdlChanged(langId) {
    var page = $('#Page').val();
    ShowEditCmsSlider($('#CmsSliderId').val(), langId.value,page);
}


function ShowImageView(ImageUrl, Image2Url) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CmsSlider/ShowImage/",
        data: { ImageUrl: ImageUrl, Image2Url: Image2Url},
        success: function (data) {
            HideSpinner();
            $('#ShowImageCmsSlider .modal-content').html(data);
            $('#ShowImageCmsSlider').modal("show");
        }

    });
}

var CmsSliderEditFrm = $('#CmsSlider-Edit');
CmsSliderEditFrm.submit(function (e) {
    e.preventDefault();
    $('#Description').val(tinymce.get("Description").getContent())
    if ($('#Description').val()) {
        ShowSpinner();
        $.ajax({
            type: CmsSliderEditFrm.attr('method'),
            url: CmsSliderEditFrm.attr('action'),
            data: CmsSliderEditFrm.serialize(),
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                toastr.success(editMassege);
                $('#EditCmsSlider').modal("hide");
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

function ShowEditCmsSlider(id, langId, page) {
    $.ajax({
        type: "GET",
        url: "/ControlPanel/CmsSlider/ShowEdit/",
        data: { id: id, languageId: langId, page:page },
        success: function (data) {
            $("#CreateCmsSliderDiv").empty();
            $('#EditCmsSlider .modal-content').html(data);
            $('#EditCmsSlider').modal("show");
        }

    });
}
function BtnSearchRestPageNumber() {
    $('#page').val("0");
}
function GetData(data) {
    $('#main').html(data);
}
