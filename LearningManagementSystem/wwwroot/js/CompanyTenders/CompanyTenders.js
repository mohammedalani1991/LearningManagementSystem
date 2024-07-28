function Get() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/CompanyTenders/GetData",
        dataType: 'html',
        success: function (data) {
            $('#main').html(data);
            HideSpinner();
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

Get();

function ShowAddCompanyTender(companyId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/CompanyTenders/ShowCreate/",
        data: { id: companyId },
        success: function (data) {
            $('#CreateCompanyTenders .modal-content').html(data);
            $('#CreateCompanyTenders').modal("show");
            HideSpinner();
        }

    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowCompanyTendersTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/CompanyTenders/ShowTable/",
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

function CreateTender() {
    var companyTenderFrm = $('#CompanyTender-Create');
    $('#Description').val(tinymce.get("Description").getContent())
    if (companyTenderFrm.valid()) {
        ShowSpinner();
        $.ajax({
            type: companyTenderFrm.attr('method'),
            url: companyTenderFrm.attr('action'),
            data: companyTenderFrm.serialize(),
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                toastr.success(addMassege);
                $('#CreateCompanyTenders').modal("hide");
                HideSpinner();
                if (typeof GetTenders === "function")
                    GetTenders();
                else
                    Get()
            },

            error: function (data) {
                HideSpinner();
                toastr.error(addMassegeError);
            },
        });
    }
}


function ShowEditCompanyTender(id, langId, companyId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/CompanyTenders/ShowEdit/",
        data: { id: id, languageId: langId, companyId: companyId },
        success: function (data) {
            $('#EditCompanyTenders .modal-content').html(data);
            $('#EditCompanyTenders').modal("show");
            HideSpinner();
        }

    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowDetailsCompanyTender(id, langId, companyId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/CompanyTenders/ShowDetails/",
        data: { id: id, languageId: langId, companyId: companyId },
        success: function (data) {
            $('#DetailsCompanyTenders .modal-content').html(data);
            $('#DetailsCompanyTenders').modal("show");
            HideSpinner();
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function ShowDeleteCompanyTender(id, langId, companyId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/CompanyTenders/ShowDelete/",
        data: { id: id, languageId: langId, companyId: companyId },
        success: function (data) {
            $('#DeleteCompanyTenders .modal-content').html(data);
            $('#DeleteCompanyTenders').modal("show");
            HideSpinner();
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function EditTender() {
    var companyTenderEditFrm = $('#CompanyTender-Edit');
    $('#Description').val(tinymce.get("Description").getContent())
    if (companyTenderEditFrm.valid()) {
        ShowSpinner();
        $.ajax({
            type: companyTenderEditFrm.attr('method'),
            url: companyTenderEditFrm.attr('action'),
            data: companyTenderEditFrm.serialize(),
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                HideSpinner();
                toastr.success(editMassege);
                $('#EditCompanyTenders').modal("hide");
                if (typeof GetTenders === "function")
                    GetTenders();
                else
                    Get()
            },
            error: function (data) {
                HideSpinner();
                toastr.error(editMassegeError);
            },
        });
    }
}


function onDdlChanged3(langId, companyId) {
    ShowEditCompanyTender($('#CompanyTenderId').val(), langId.value, companyId);
}

function ConfirmDeleteTender(id) {
    ShowSpinner();
    $.post("/Companies/CompanyTenders/DeleteCompanyTender/",
        { id: id },
        function (data) {
            HideSpinner();
            if (data !== null) {
                toastr.success(deleteMassege);
                $('#DeleteCompanyTenders').modal("hide");
                if (typeof GetTenders === "function")
                    GetTenders();
                else
                    Get()
            }
            else {
                toastr.error(deleteMassegeError);
            }
        }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });

}

function ShowCompanyTenderFiles(id, companyId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/CompanyTenderFiles/GetCompanyTenderFiles/",
        data: { companyTenderId: id, companyId: companyId },
        success: function (data) {
            $('#CompanyTenderFiels .modal-content').html(data);
            $('#CompanyTenderFiels').modal("show");
            HideSpinner();
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function GetData1(data) {
    if (typeof GetNotes === "undefined")
        $('#main').html(data);
    else
        $('#GetData').html(data);
}