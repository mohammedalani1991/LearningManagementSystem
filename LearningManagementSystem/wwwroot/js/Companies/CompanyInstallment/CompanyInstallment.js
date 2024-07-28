
function ShowCompanyInstallmentTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/CompanyInstallment/ShowTable/",
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

function ShowAddCompanyInstallment(id) {
    ShowSpinner(); 
    $.ajax({
        type: "GET",
        url: "/Companies/CompanyInstallment/ShowCreate/",
        data: { id: id },
        success: function (data) {
            HideSpinner();
            $('#CreateCompanyInstallment .modal-content').html(data);
            $('#CreateCompanyInstallment').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function CreateCompanyInstallmente() {
    var CompanyInstallmentFrm = $('#CompanyInstallment-Create');
    if (CompanyInstallmentFrm.valid()) {
        ShowSpinner();
        $.ajax({
            type: CompanyInstallmentFrm.attr('method'),
            url: CompanyInstallmentFrm.attr('action'),
            data: CompanyInstallmentFrm.serialize(),
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                toastr.success(addMassege);
                $('#CreateCompanyInstallment').modal("hide");
                GetCompanyInstallment()
                HideSpinner();
            },
            error: function (data) {
                HideSpinner();
                toastr.error(addMassegeError);
            },
        });
    }
}


function ShowEditCompanyInstallment(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/CompanyInstallment/ShowEdit/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#EditCompanyInstallment .modal-content').html(data);
            $('#EditCompanyInstallment').modal("show");
        }

    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function ShowDetailsCompanyInstallment(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/CompanyInstallment/ShowDetails/",
        data: { id: id },
        success: function (data) {
            HideSpinner();
            $('#DetialsCompanyInstallment .modal-content').html(data);
            $('#DetialsCompanyInstallment').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function ShowDeleteCompanyInstallment(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/CompanyInstallment/ShowDelete/",
        data: { id: id },
        success: function (data) {
            HideSpinner();
            $('#DeleteCompanyInstallment .modal-content').html(data);
            $('#DeleteCompanyInstallment').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function EditCompanyInstallment() {
    var CompanyInstallmentEditFrm = $('#CompanyInstallment-Edit');
    if (CompanyInstallmentEditFrm.valid()) {
        ShowSpinner();
        $.ajax({
            type: CompanyInstallmentEditFrm.attr('method'),
            url: CompanyInstallmentEditFrm.attr('action'),
            data: CompanyInstallmentEditFrm.serialize(),
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                toastr.success(editMassege);
                $('#EditCompanyInstallment').modal("hide");
                HideSpinner();
                GetCompanyInstallment()


            }, error: function (data) {
                HideSpinner();
                toastr.error(editMassegeError);
            },
        });
    }
}


function ConfirmDeleteCompanyInstallment(id) {
    ShowSpinner();
    $.post("/Companies/CompanyInstallment/DeleteCompanyInstallment/",
        { id: id },
        function (data) {
            if (data !== null) {
                toastr.success(deleteMassege);
                $('#DeleteCompanyInstallment').modal("hide");
                HideSpinner();
                GetCompanyInstallment()
            } else {
                HideSpinner();
                toastr.error(deleteMassegeError);
            }
        }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function GetData1(data) {
    $('#GetData').html(data);
}
