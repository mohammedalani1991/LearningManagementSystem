function Get() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Templates/GetData",
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

function ShowAddTemplate() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Templates/ShowCreate/",
        success: function (data) {
            HideSpinner();
            $('#CreateTemplate .modal-content').html(data);
            $('#CreateTemplate').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    }); 
}

function ShowEditTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Templates/ShowTable/",
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

var TemplateFrm = $('#Template-Create');
TemplateFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: TemplateFrm.attr('method'),
        url: TemplateFrm.attr('action'),
        data: TemplateFrm.serialize(),
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            toastr.success(addMassege);
            $('#CreateTemplate').modal("hide");
            HideSpinner();
            Get()
        },
        error: function (data) {
            HideSpinner();
            toastr.error(addMassegeError);
        },
    });
});

function ShowDetailsTemplate(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Templates/ShowDetails/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#DetailsTemplate .modal-content').html(data);
            $('#DetailsTemplate').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function ShowDeleteTemplate(id, langId,page) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Templates/ShowDelete/",
        data: { id: id, languageId: langId, page: page},
        success: function (data) {
            HideSpinner();
            $('#DeleteTemplate .modal-content').html(data);
            $('#DeleteTemplate').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ConfirmDelete(id,page) {
    ShowSpinner();
    $.post("/ControlPanel/Templates/DeleteTemplate/",
        { id: id, page: page},
        function (data) {
            if (data !== null) {
                toastr.success(deleteMassege);
                HideSpinner();
                $('#DeleteTemplate').modal("hide");
                Get()
            } else {
                HideSpinner();
                toastr.error(deleteMassegeError);
            }
        });
}

function onDdlChanged(langId) {
    var page = $('#Page').val();
    ShowEditTemplate($('#TemplateId').val(), langId.value,page);
}


var TemplateEditFrm = $('#Template-Edit');
TemplateEditFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: TemplateEditFrm.attr('method'),
        url: TemplateEditFrm.attr('action'),
        data: TemplateEditFrm.serialize(),
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            toastr.success(editMassege);
            $('#EditTemplate').modal("hide");
            HideSpinner();
            Get();
        },
        error: function (data) {
            HideSpinner();
            toastr.error(editMassegeError);
        },
    });
});

function ShowEditTemplate(id, langId, page) {
    $.ajax({
        type: "GET",
        url: "/ControlPanel/Templates/ShowEdit/",
        data: { id: id, languageId: langId, page:page },
        success: function (data) {
            $('#EditTemplate .modal-content').html(data);
            $('#EditTemplate').modal("show");
        }

    });
}

function GetData(data) {
    $('#main').html(data);
}
