function Get() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentSubscriptionFeesPayment/GetData",
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

function ShowStudentSubscriptionFeesPaymentTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentSubscriptionFeesPayment/ShowTable/",
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

function ShowAddStudentSubscriptionFeesPayment(subscriptionFeesId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentSubscriptionFeesPayment/ShowCreate/",
        data: { id: subscriptionFeesId },
        success: function (data) {
            HideSpinner();
            $('#StudentSubscriptionFeesPayment .modal-content').html(data);
            $('#StudentSubscriptionFeesPayment').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });

}

function CreateStudentSubscriptionFee() {
    var StudentSubscriptionFeeFrm = $('#StudentSubscriptionFeesPayment-Create');
    if (StudentSubscriptionFeeFrm.valid()) {
        $.ajax({
            type: StudentSubscriptionFeeFrm.attr('method'),
            url: StudentSubscriptionFeeFrm.attr('action'),
            data: StudentSubscriptionFeeFrm.serialize(),
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                if (data.amount) {
                    toastr.success(addMassege);
                    $('#StudentSubscriptionFeesPayment').modal("hide");
                    if (typeof GetStudentSubscriptionFeesPayment === "function") {
                        if (typeof GetStudentSubscriptionFees === "function") {
                            GetStudentSubscriptionFees()
                        } else {
                            GetStudentSubscriptionFeesPayment();
                        }
                    } else
                        Get()
                }
                else
                    toastr.warning(ErrorPayment);
            },
            error: function (data) {
                toastr.error(addMassegeError);
            },
        });
    }
}


function ShowEditStudentSubscriptionFeesPayment(id, langId, subscriptionFeesId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentSubscriptionFeesPayment/ShowEdit/",
        data: { id: id, languageId: langId, subscriptionFeesId: subscriptionFeesId },
        success: function (data) {
            HideSpinner();
            $('#StudentSubscriptionFeesPayment .modal-content').html(data);
            $('#StudentSubscriptionFeesPayment').modal("show");
        }

    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function ShowDetailsStudentSubscriptionFeesPayment(id, langId, subscriptionFeesId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentSubscriptionFeesPayment/ShowDetails/",
        data: { id: id, languageId: langId, subscriptionFeesId: subscriptionFeesId },
        success: function (data) {
            HideSpinner();
            $('#StudentSubscriptionFeesPayment .modal-content').html(data);
            $('#StudentSubscriptionFeesPayment').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function ShowDeleteStudentSubscriptionFeesPayment(id, langId, subscriptionFeesId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentSubscriptionFeesPayment/ShowDelete/",
        data: { id: id, languageId: langId, subscriptionFeesId: subscriptionFeesId },
        success: function (data) {
            HideSpinner();
            $('#StudentSubscriptionFeesPayment .modal-content').html(data);
            $('#StudentSubscriptionFeesPayment').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function EditStudentSubscriptionFee() {
    var StudentSubscriptionFeeEditFrm = $('#StudentSubscriptionFeesPayment-Edit');
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
                    $('#StudentSubscriptionFeesPayment').modal("hide");
                    HideSpinner();
                    if (typeof GetStudentSubscriptionFeesPayment === "function")
                        GetStudentSubscriptionFeesPayment();
                    else
                        Get()
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

function ConfirmDeleteStudentSubscriptionFeesPayment(id) {
    ShowSpinner();
    $.post("/Sales/StudentSubscriptionFeesPayment/DeleteStudentSubscriptionFeesPayment/",
        { id: id },
        function (data) {
            if (data !== null) {
                toastr.success(deleteMassege);
                $('#StudentSubscriptionFeesPayment').modal("hide");
                HideSpinner();
                if (typeof GetStudentSubscriptionFeesPayment === "function")
                    GetStudentSubscriptionFeesPayment();
                else
                    Get()
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

