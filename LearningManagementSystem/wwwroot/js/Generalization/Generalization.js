function Get() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Generalization/GetData",
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

function ShowAddGeneralization() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Generalization/ShowCreate/",
        success: function (data) {
            HideSpinner();
            $('#EditGeneralizationDiv').empty();
            $('#CreateGeneralization .modal-content').html(data);
            $('#CreateGeneralization').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowEditTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Generalization/ShowTable/",
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

var GeneralizationFrm = $('#Generalization-Create');
GeneralizationFrm.submit(function (e) {
    e.preventDefault();
    $('#Description').val(tinymce.get("Description").getContent())
    if ($('#Description').val()) {
        ShowSpinner();
        $.ajax({
            type: GeneralizationFrm.attr('method'),
            url: GeneralizationFrm.attr('action'),
            data: GeneralizationFrm.serialize(),
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                toastr.success(addMassege);
                $('#CreateGeneralization').modal("hide");
                HideSpinner();
                Get();
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

function ShowDetailsGeneralization(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Generalization/ShowDetails/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#DetailsGeneralization .modal-content').html(data);
            $('#DetailsGeneralization').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function ShowDeleteGeneralization(id, langId, page) {
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Generalization/ShowDelete/",
        data: { id: id, languageId: langId, page: page },
        success: function (data) {
            $('#DeleteGeneralization .modal-content').html(data);
            $('#DeleteGeneralization').modal("show");
        }
    });
}

function ConfirmDelete(id, page) {
    $.post("/ControlPanel/Generalization/DeleteGeneralization/",
        { id: id, page: page },
        function (data) {
            if (data !== null) {
                toastr.success(deleteMassege);
                $('#DeleteGeneralization').modal("hide");

                setInterval(
                    function () {
                        $(".modal-backdrop").remove();
                        location.reload()
                    }, 1000);
            } else {
                toastr.error(deleteMassegeError);
            }
        });
}

function onDdlChanged(langId) {
    var page = $('#Page').val();
    ShowEditGeneralization($('#GeneralizationId').val(), langId.value, page);
}


var GeneralizationEditFrm = $('#Generalization-Edit');
GeneralizationEditFrm.submit(function (e) {
    e.preventDefault();
    $('#Description').val(tinymce.get("Description").getContent())
    if ($('#Description').val()) {
        ShowSpinner();
        $.ajax({
            type: GeneralizationEditFrm.attr('method'),
            url: GeneralizationEditFrm.attr('action'),
            data: GeneralizationEditFrm.serialize(),
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                $('#EditGeneralization').modal("hide");
                HideSpinner();
                toastr.success(editMassege);
                Get()
            },
            error: function (data) {
                HideSpinner();
                toastr.error(editMassegeError);
            },
        });
    }
    else {
        $("#Description-error").show();
        $("#Description-error").html("This field is required.");
    }
});

function ShowEditGeneralization(id, langId, page) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Generalization/ShowEdit/",
        data: { id: id, languageId: langId, page: page },
        success: function (data) {
            HideSpinner();
            $('#CreateGeneralizationDiv').empty();
            $('#EditGeneralization .modal-content').html(data);
            $('#EditGeneralization').modal("show");
        }

    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function GetData(data) {
    $('#main').html(data);
}