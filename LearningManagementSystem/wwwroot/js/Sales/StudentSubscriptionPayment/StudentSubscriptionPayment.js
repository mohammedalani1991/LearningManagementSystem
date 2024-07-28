function Get() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentSubscriptionPayment/GetData",
        dataType: 'html',
        success: function (data) {
            HideSpinner();
            $('#main').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function ShowStudentSubscriptionPaymentTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentSubscriptionPayment/ShowTable/",
        data: { cheque: $('#cheque').val() },
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

function ShowAddStudentSubscriptionPayment(studentId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentSubscriptionPayment/ShowCreate/",
        data: { id: studentId, cheque: $('#cheque').val() },
        success: function (data) {
            HideSpinner();
            $('#StudentSubscriptionPayment .modal-content').html(data);
            $('#StudentSubscriptionPayment').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function CreateStudentSubscriptionPaymente() {
    var StudentSubscriptionPaymentFrm = $('#StudentSubscriptionPayment-Create');
    StudentSubscriptionPaymentFrm.validate({
        errorElement: "span",
        rules: {
            Amount: {
                required: true,
                min: 1
            },
        }
    })
    if (StudentSubscriptionPaymentFrm.valid()) {
        ShowSpinner();
        $.ajax({
            type: StudentSubscriptionPaymentFrm.attr('method'),
            url: StudentSubscriptionPaymentFrm.attr('action'),
            data: StudentSubscriptionPaymentFrm.serialize(),
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                if (data.amount) {
                    toastr.success(addMassege);
                    $('#StudentSubscriptionPayment').modal("hide");
                    if (typeof GetStudentSubscriptionPayments === "function") {

                        if (typeof GetStudentSubscriptions === "function") {
                            GetStudentSubscriptions();
                        } else {
                            GetStudentSubscriptionPayments($('#cheque').val());
                        }
                    }
                    else
                        Get()
                }
                else
                    toastr.warning(ErrorPayment);
                HideSpinner();
            },
            error: function (data) {
                HideSpinner();
                toastr.error(addMassegeError);
            },
        });
    }
}


function ShowEditStudentSubscriptionPayment(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentSubscriptionPayment/ShowEdit/",
        data: { id: id, languageId: langId, cheque: $('#cheque').val() },
        success: function (data) {
            HideSpinner();
            $('#StudentSubscriptionPayment .modal-content').html(data);
            $('#StudentSubscriptionPayment').modal("show");
            ChequeChange();
        }

    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function ShowDetailsStudentSubscriptionPayment(id, langId, studentSubscriptionId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentSubscriptionPayment/ShowDetails/",
        data: { id: id, languageId: langId, studentSubscriptionId: studentSubscriptionId, cheque: $('#cheque').val() },
        success: function (data) {
            HideSpinner();
            $('#StudentSubscriptionPayment .modal-content').html(data);
            $('#StudentSubscriptionPayment').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function ShowDeleteStudentSubscriptionPayment(id, langId, studentSubscriptionId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentSubscriptionPayment/ShowDelete/",
        data: { id: id, languageId: langId, studentSubscriptionId: studentSubscriptionId, cheque: $('#cheque').val() },
        success: function (data) {
            HideSpinner();
            $('#StudentSubscriptionPayment .modal-content').html(data);
            $('#StudentSubscriptionPayment').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}
function ShowRefundStudentSubscriptionPayment(id) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentSubscriptionPayment/ShowRefund/",
        data: { id: id },
        success: function (data) {
            HideSpinner();
            $('#StudentSubscriptionRefundPayment .modal-content').html(data);
            $("#Refund").val("");
            $('#StudentSubscriptionRefundPayment').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function EditStudentSubscriptionPaymente() {
    var StudentSubscriptionPaymentEditFrm = $('#StudentSubscriptionPayment-Edit');
    if (StudentSubscriptionPaymentEditFrm.valid()) {
        ShowSpinner();
        $.ajax({
            type: StudentSubscriptionPaymentEditFrm.attr('method'),
            url: StudentSubscriptionPaymentEditFrm.attr('action'),
            data: StudentSubscriptionPaymentEditFrm.serialize(),
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                if (data.success) {
                    toastr.success(editMassege);
                    $('#StudentSubscriptionPayment').modal("hide");
                    HideSpinner();
                    if (typeof GetStudentSubscriptionPayments === "function")
                        GetStudentSubscriptionPayments($('#cheque').val());
                    else
                        Get()
                }
                else{
                    HideSpinner();
                    toastr.error(data.error);
                }

            }, error: function (data) {
                toastr.error(editMassegeError);
            },
        });
    }
}
function RefundStudentSubscriptionPaymente() {
    var StudentSubscriptionPaymentRefundFrm = $('#StudentSubscriptionPayment-Refund');
    if (StudentSubscriptionPaymentRefundFrm.valid()) {
        ShowSpinner();
        $.ajax({
            type: StudentSubscriptionPaymentRefundFrm.attr('method'),
            url: StudentSubscriptionPaymentRefundFrm.attr('action'),
            data: StudentSubscriptionPaymentRefundFrm.serialize(),
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                if (data.success) {
                    toastr.success(editMassege);
                    GetStudentSubscriptionPayments($('#cheque').val());
                    $('#StudentSubscriptionPayment').modal("hide");
                    HideSpinner();
                    $(".modal-backdrop").remove();
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

function ConfirmDeleteStudentSubscriptionPayment(id) {
    ShowSpinner();
    $.post("/Sales/StudentSubscriptionPayment/DeleteStudentSubscriptionPayment/",
        { id: id },
        function (data) {
            if (data !== null) {
                toastr.success(deleteMassege);
                $('#StudentSubscriptionPayment').modal("hide");
                HideSpinner();
                if (typeof GetStudentSubscriptionPayments === "function")
                    GetStudentSubscriptionPayments($('#cheque').val());
                else
                    Get()
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
    $('.modal-backdrop').hide();

}

function ChequeChange() {
    if ($('input[name="Cheque"]:checked').val() == 'true') {
        $("#ChequeDiv").show();
        $("#ChequeDiv input").prop('required', true);
    } else {
        $("#ChequeDiv").hide();
        $("#ChequeDiv input").prop('required', false);
    }
}