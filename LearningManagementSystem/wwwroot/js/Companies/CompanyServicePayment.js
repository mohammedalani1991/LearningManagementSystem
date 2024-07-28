

function ShowCompanyServicePaymentTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/CompanyServicePayment/ShowTable/",
        success: function (data) {
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
            HideSpinner();
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowAddCompanyServicePayment(id,companyId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/CompanyServicePayment/ShowCreate/",
        data: { id: id },
        success: function (data) {
            HideSpinner();
            $('#CompanyServicePayment .modal-content').html(data);
            $('#CompanyServicePayment').modal("show");
            $("#CompanyId").val(companyId);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });

}

function CreateStudentSubscriptionFee() {
    var StudentSubscriptionFeeFrm = $('#CompanyServicePayment-Create');
    if (StudentSubscriptionFeeFrm.valid()) {
        $.ajax({
            type: StudentSubscriptionFeeFrm.attr('method'),
            url: StudentSubscriptionFeeFrm.attr('action'),
            data: StudentSubscriptionFeeFrm.serialize(),
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                if (data.success) {
                    toastr.success(addMassege);
                    $('#CompanyServicePayment').modal("hide");
                    if (typeof GetCompanyServicePayment === "function") {
                        if (typeof GetStudentSubscriptionFees === "function") {
                            GetStudentSubscriptionFees()
                        } else {
                            GetCompanyServicePayment();
                        }
                    } else
                        Get()
                }
                else
                    toastr.error(data.error);
            },
            error: function (data) {
                toastr.error(addMassegeError);
            },
        });
    }
}


function ShowEditCompanyServicePayment(id, langId, subscriptionFeesId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/CompanyServicePayment/ShowEdit/",
        data: { id: id, languageId: langId, subscriptionFeesId: subscriptionFeesId },
        success: function (data) {
            HideSpinner();
            $('#CompanyServicePayment .modal-content').html(data);
            $('#CompanyServicePayment').modal("show");
        }

    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function ShowDetailsCompanyServicePayment(id, langId, subscriptionFeesId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/CompanyServicePayment/ShowDetails/",
        data: { id: id, languageId: langId, subscriptionFeesId: subscriptionFeesId },
        success: function (data) {
            HideSpinner();
            $('#CompanyServicePayment .modal-content').html(data);
            $('#CompanyServicePayment').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function ShowDeleteCompanyServicePayment(id, langId, subscriptionFeesId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/CompanyServicePayment/ShowDelete/",
        data: { id: id, languageId: langId, subscriptionFeesId: subscriptionFeesId },
        success: function (data) {
            HideSpinner();
            $('#CompanyServicePayment .modal-content').html(data);
            $('#CompanyServicePayment').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function EditStudentSubscriptionFee() {
    var StudentSubscriptionFeeEditFrm = $('#CompanyServicePayment-Edit');
    if (StudentSubscriptionFeeEditFrm.valid()) {
        ShowSpinner();
        $.ajax({
            type: StudentSubscriptionFeeEditFrm.attr('method'),
            url: StudentSubscriptionFeeEditFrm.attr('action'),
            data: StudentSubscriptionFeeEditFrm.serialize(),
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                
                if (data.success) {
                    toastr.success(editMassege);
                    HideSpinner();
                    $('#CompanyServicePayment').modal("hide");
                    GetServicePayments();
                }
                else{
                    HideSpinner();
                    toastr.error(data.error);
                }
            }, error: function (data) {
                HideSpinner();
                toastr.error(editMassegeError);
            },
        });
    }
}

function ConfirmDeleteCompanyServicePayment(id) {
    ShowSpinner();
    $.post("/Companies/CompanyServicePayment/DeleteCompanyServicePayment/",
        { id: id },
        function (data) {
            if (data !== null) {
                toastr.success(deleteMassege);
                $('#CompanyServicePayment').modal("hide");
                HideSpinner();
                GetServicePayments();
            } else {
                HideSpinner();
                toastr.error(ErrorMessage);
            }
        }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function GetData1(data) {
    $('#GetData').html(data);
    $('.modal-backdrop').hide();

}

