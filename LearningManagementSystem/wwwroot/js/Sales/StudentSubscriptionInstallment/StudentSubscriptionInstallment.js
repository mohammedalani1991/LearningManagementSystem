function Get() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentSubscriptionInstallment/GetData",
        dataType: 'html',
        success: function (data) {
            HideSpinner();
            $('#main').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function ShowStudentSubscriptionInstallmentTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentSubscriptionInstallment/ShowTable/",
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

function ShowAddStudentSubscriptionInstallment(studentId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentSubscriptionInstallment/ShowCreate/",
        data: { id: studentId },
        success: function (data) {
            HideSpinner();
            $('#StudentSubscriptionInstallment .modal-content').html(data);
            $('#StudentSubscriptionInstallment').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function CreateStudentSubscriptionInstallmente() {
    var StudentSubscriptionInstallmentFrm = $('#StudentSubscriptionInstallment-Create');
    StudentSubscriptionInstallmentFrm.validate({
        errorElement: "span",
        rules: {
            Amount: {
                required: true,
                min: 1
            },
        }
    })
    if (StudentSubscriptionInstallmentFrm.valid()) {
        ShowSpinner();
        $.ajax({
            type: StudentSubscriptionInstallmentFrm.attr('method'),
            url: StudentSubscriptionInstallmentFrm.attr('action'),
            data: StudentSubscriptionInstallmentFrm.serialize(),
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                if (data.success) {
                    toastr.success(addMassege);
                    $('#StudentSubscriptionInstallment').modal("hide");
                    if (typeof GetStudentSubscriptionInstallments === "function") {

                        if (typeof GetStudentSubscriptions === "function") {
                            GetStudentSubscriptions();
                        } else {
                            GetStudentSubscriptionInstallments();
                        }
                    }

                    else
                        Get()
                }
                else
                    toastr.error(data.error);
                HideSpinner();
            },
            error: function (data) {
                HideSpinner();
                toastr.error(addMassegeError);
            },
        });
    }
}


function ShowEditStudentSubscriptionInstallment(id, langId, studentSubscriptionId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentSubscriptionInstallment/ShowEdit/",
        data: { id: id, languageId: langId, studentSubscriptionId: studentSubscriptionId },
        success: function (data) {
            HideSpinner();
            $('#StudentSubscriptionInstallment .modal-content').html(data);
            $('#StudentSubscriptionInstallment').modal("show");
            ChequeChange();
        }

    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function ShowDetailsStudentSubscriptionInstallment(id, langId, studentSubscriptionId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentSubscriptionInstallment/ShowDetails/",
        data: { id: id, languageId: langId, studentSubscriptionId: studentSubscriptionId },
        success: function (data) {
            HideSpinner();
            $('#StudentSubscriptionInstallment .modal-content').html(data);
            $('#StudentSubscriptionInstallment').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function ShowDeleteStudentSubscriptionInstallment(id, langId, studentSubscriptionId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentSubscriptionInstallment/ShowDelete/",
        data: { id: id, languageId: langId, studentSubscriptionId: studentSubscriptionId },
        success: function (data) {
            HideSpinner();
            $('#StudentSubscriptionInstallment .modal-content').html(data);
            $('#StudentSubscriptionInstallment').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}
function ShowRefundStudentSubscriptionInstallment(id) {
    $.ajax({
        type: "GET",
        url: "/Sales/StudentSubscriptionInstallment/ShowRefund/",
        data: { id: id },
        success: function (data) {
            $('#StudentSubscriptionInstallment .modal-content').html(data);
            $('#StudentSubscriptionInstallment').modal("show");
        }
    });
}

function EditStudentSubscriptionInstallmente() {
    var StudentSubscriptionInstallmentEditFrm = $('#StudentSubscriptionInstallment-Edit');
    if (StudentSubscriptionInstallmentEditFrm.valid()) {
        ShowSpinner();
        $.ajax({
            type: StudentSubscriptionInstallmentEditFrm.attr('method'),
            url: StudentSubscriptionInstallmentEditFrm.attr('action'),
            data: StudentSubscriptionInstallmentEditFrm.serialize(),
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                if (data.success) {
                    toastr.success(editMassege);
                    $('#StudentSubscriptionInstallment').modal("hide");
                    if (typeof GetStudentSubscriptionInstallments === "function")
                        GetStudentSubscriptionInstallments();
                    else
                        Get()
                }
                else
                    toastr.error(data.error);
                HideSpinner();

            }, error: function (data) {
                HideSpinner();
                toastr.error(editMassegeError);
            },
        });
    }
}
function RefundStudentSubscriptionInstallmente() {
    var StudentSubscriptionInstallmentRefundFrm = $('#StudentSubscriptionInstallment-Refund');
    if (StudentSubscriptionInstallmentRefundFrm.valid()) {
        $.ajax({
            type: StudentSubscriptionInstallmentRefundFrm.attr('method'),
            url: StudentSubscriptionInstallmentRefundFrm.attr('action'),
            data: StudentSubscriptionInstallmentRefundFrm.serialize(),
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                if (data.success) {
                    toastr.success(editMassege);
                    $('#StudentSubscriptionInstallment').modal("hide");
                    if (typeof GetStudentSubscriptionInstallments === "function")
                        GetStudentSubscriptionInstallments();
                    else
                        Get()
                }
                else
                    toastr.error(data.error);

            }, error: function (data) {
                toastr.error(editMassegeError);
            },
        });
    }
}

function ConfirmDeleteStudentSubscriptionInstallment(id) {
    ShowSpinner();
    $.post("/Sales/StudentSubscriptionInstallment/DeleteStudentSubscriptionInstallment/",
        { id: id },
        function (data) {
            if (data !== null) {
                HideSpinner();
                toastr.success(deleteMassege);
                $('#StudentSubscriptionInstallment').modal("hide");
                if (typeof GetStudentSubscriptionInstallments === "function")
                    GetStudentSubscriptionInstallments();
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
    if ($("#Cheque").is(":checked")) {
        $("#ChequeDiv").show();
        $("#ChequeDiv input").prop('required', true);
    } else {
        $("#ChequeDiv").hide();
        $("#ChequeDiv input").prop('required', false);
    }
}