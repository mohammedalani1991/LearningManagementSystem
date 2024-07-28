function Get() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/CompanyPayments/GetData",
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

function ShowAddCompanyPayment(companyId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/CompanyPayments/ShowCreate/",
        data: { id: companyId },
        success: function (data) {
            HideSpinner();
            $('#CreateCompanyPayments .modal-content').html(data);
            $('#CreateCompanyPayments').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowEditPaymentTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/CompanyPayments/ShowTable/",
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

function CreatePayment() {
    var companyPaymentFrm = $('#CompanyPayment-Create');
    if (companyPaymentFrm.valid()) {
        ShowSpinner();
        $.ajax({
            type: companyPaymentFrm.attr('method'),
            url: companyPaymentFrm.attr('action'),
            data: companyPaymentFrm.serialize(),
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                toastr.success(addMassege);
                $('#CreateCompanyPayments').modal("hide");
                HideSpinner();
                if (typeof GetPayments === "function")
                    GetPayments();
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

function ShowEditCompanyPayment(id, companyId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/CompanyPayments/ShowEdit/",
        data: { id: id, companyId: companyId},
        success: function (data) {
            HideSpinner();
            $('#EditCompanyPayments .modal-content').html(data);
            $('#EditCompanyPayments').modal("show");
        }

    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function ShowDetailsCompanyPayment(id, companyId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/CompanyPayments/ShowDetails/",
        data: { id: id, companyId: companyId},
        success: function (data) {
            HideSpinner();
            $('#DetailsCompanyPayments .modal-content').html(data);
            $('#DetailsCompanyPayments').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function CloseDetailsPayment() {
    $('#DetailsCompanyPayments').modal("hide");
}

function ShowDeleteCompanyPayment(id, companyId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/CompanyPayments/ShowDelete/",
        data: { id: id, companyId: companyId},
        success: function (data) {
            HideSpinner();
            $('#DeleteCompanyPayments .modal-content').html(data);
            $('#DeleteCompanyPayments').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function EditPayment() {
    var companyPaymentEditFrm = $('#CompanyPayment-Edit');
    if (companyPaymentEditFrm.valid()) {
        ShowSpinner();
        $.ajax({
            type: companyPaymentEditFrm.attr('method'),
            url: companyPaymentEditFrm.attr('action'),
            data: companyPaymentEditFrm.serialize(),
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                toastr.success(editMassege);
                $('#EditCompanyPayments').modal("hide");
                HideSpinner();
                if (typeof GetPayments === "function")
                    GetPayments();
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

function ConfirmDeletePayment(id) {
    ShowSpinner();
    $.post("/Companies/CompanyPayments/DeleteCompanyPayment/",
        { id: id },
        function (data) {
            if (data !== null) {
                toastr.success(deleteMassege);
                $('#DeleteCompanyPayments').modal("hide");
                HideSpinner();
                if (typeof GetPayments === "function")
                    GetPayments();
                else
                    Get()
            }else {
                HideSpinner();
                toastr.error(deleteMassegeError);
            }
        }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function GetData1(data) {
    if (typeof GetPayments === "undefined")
        $('#main').html(data);
    else
        $('#GetData').html(data);
}