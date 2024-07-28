
function ShowStudentSubscriptionTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentSubscription/ShowTable/",
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

function ShowAddStudentSubscription(studentId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentSubscription/ShowCreate/",
        data: { id: studentId },
        success: function (data) {
            $('#StudentSubscription .modal-content').html(data);
            $('#StudentSubscription').modal("show");
            HideSpinner();
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function GetProgramData() {
    var id = $("#ProgramId").val();
    if (id != '') {
        $.ajax({
            type: "GET",
            url: "/Training/Programs/GetProgram/",
            data: { id: id },
            success: function (data) {
                $("#SubscriptionPrice").val(data.price)
                $("#InstallmentsNumber").val(data.installmentsNumber)
            }
        });
    }
}
function CreateStudentSubscriptione() {
    var StudentSubscriptionFrm = $('#StudentSubscription-Create');
    if (StudentSubscriptionFrm.valid()) {
        ShowSpinner();
        $.ajax({
            type: StudentSubscriptionFrm.attr('method'),
            url: StudentSubscriptionFrm.attr('action'),
            data: StudentSubscriptionFrm.serialize(),
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                if (data.success) {
                    toastr.success(addMassege);
                    $('#StudentSubscription').modal("hide");
                    HideSpinner();
                    if (typeof GetStudentSubscriptions === "function")
                        GetStudentSubscriptions();
                    else
                        Get()
                }
                else{
                    HideSpinner();
                    toastr.error(data.error);
                }
                    
            },
            error: function (data) {
                HideSpinner();
                toastr.error(addMassegeError);
            },
        });
    }
}


function ShowEditStudentSubscription(id, langId, studentId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentSubscription/ShowEdit/",
        data: { id: id, languageId: langId, studentId: studentId },
        success: function (data) {
            HideSpinner();
            $('#StudentSubscription .modal-content').html(data);
            $('#StudentSubscription').modal("show");
        }

    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function ShowDetailsStudentSubscription(id, langId, studentId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentSubscription/ShowDetails/",
        data: { id: id, languageId: langId, studentId: studentId },
        success: function (data) {
            HideSpinner();
            $('#StudentSubscription .modal-content').html(data);
            $('#StudentSubscription').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowDeleteStudentSubscription(id, langId, studentId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentSubscription/ShowDelete/",
        data: { id: id, languageId: langId, studentId: studentId },
        success: function (data) {
            HideSpinner();
            $('#StudentSubscription .modal-content').html(data);
            $('#StudentSubscription').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function EditStudentSubscriptione() {
    var StudentSubscriptionEditFrm = $('#StudentSubscription-Edit');
    if (StudentSubscriptionEditFrm.valid()) {
        ShowSpinner();
        $.ajax({
            type: StudentSubscriptionEditFrm.attr('method'),
            url: StudentSubscriptionEditFrm.attr('action'),
            data: StudentSubscriptionEditFrm.serialize(),
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                if (data.success) {
                    toastr.success(editMassege);
                    $('#StudentSubscription').modal("hide");
                    HideSpinner();
                    if (typeof GetStudentSubscriptions === "function")
                        GetStudentSubscriptions();
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

function ConfirmDeleteStudentSubscription(id) {
    ShowSpinner();
    $.post("/Sales/StudentSubscription/DeleteStudentSubscription/",
        { id: id },
        function (data) {
            if (data !== null) {
                toastr.success(deleteMassege);
                $('#StudentSubscription').modal("hide");
                HideSpinner();
                if (typeof GetStudentSubscriptions === "function")
                    GetStudentSubscriptions();
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
function TogelJudgement(id, IsJudgement) {
    ShowSpinner();
    $.post("/Sales/StudentSubscription/TogelJudgement/",
        { id: id },
        function (data) {
            HideSpinner();
            if (data !== null) {                
                if (IsJudgement == 'false') {
                    toastr.success(SuccesAddJudgement);
                } else {
                    toastr.success(SuccesDeleteJudgement);
                }
                if (typeof GetStudentSubscriptions === "function")
                    GetStudentSubscriptions();
                else
                    Get()
            } else {
                toastr.error(ErrorMessage);
            }
        }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function onDdlChanged2(langId, studentId) {

    ShowEditStudentSubscription($('#StudentSubscriptionId').val(), langId.value, studentId);
}

function GetData1(data) {
    $('#GetData').html(data);
    $('.modal-backdrop').hide();

}

function ShowAddStudentSubscriptionDiscount(id, langId) {
    ShowSpinner();
    $("#AddDiscounts").empty();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentSubscriptionDiscounts/GetDiscounts",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            if (data.length) {
                $("#AddDiscounts").append("<select id='Discounts' class='form-control'><option >" + SelectOption + "</option></select>")
                $("#Discount").modal("show");
                $("#SubDiscount").val(id);
                $.each(data, function (i, item) {
                    $('#Discounts').append($('<option>', {
                        value: item.id,
                        text: item.name
                    }));
                });
                $("#Discounts").addClass('chosen-select');
            }
            else
                toastr.warning(ErrorDiscount);
        },
        error: function (data) {
            HideSpinner();
            toastr.error(ErrorMessage);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function AddDiscount() {
    $("#AddDiscountForm").validate({
        errorElement: "span",
        rules: {
            Discounts: "required",
        }
    })

    if ($("#AddDiscountForm").valid()) {
        ShowSpinner();
        $.ajax({
            type: "POST",
            url: "/Sales/StudentSubscriptionDiscounts/CreateStudentSubscriptionDiscountst",
            data: {
                studentSubscriptionId: $("#SubDiscount").val(), discountId: $("#Discounts").val()
            },
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                toastr.success(SuccesCreate);
                $("#Discount").modal("hide");
                HideSpinner();
                setTimeout(function () {
                    GetStudentSubscriptions();
                },[150])
            },
            error: function (data) {
                HideSpinner();
                toastr.error(ErrorCreate);
            }
        });
    }
}

function Installment(studentSubscriptionId, studentId) {
    $.ajax({
        type: "GET",
        url: "/Sales/StudentSubscription/GetInstallment",
        dataType: 'html',
        data: { studentSubscriptionId, studentId },
        success: function (data) {
            $('#Installment').html(data);
        }
    });
}

function GetInstallment(data) {
    $('#Installment').html(data);
}

function ChangePagenation1() {
    $("#pagination1").val($("#pagin1").val());
    $("#Form2").submit();
}

function Fees(studentSubscriptionId, studentId) {
    $.ajax({
        type: "GET",
        url: "/Sales/StudentSubscription/GetFees",
        dataType: 'html',
        data: { studentSubscriptionId, studentId },
        success: function (data) {
            $('#Fees').html(data);
        }
    });
}


function GetFees(data) {
    $('#Fees').html(data);
}

function ChangePagenation2() {
    $("#pagination2").val($("#pagin2").val());
    $("#Form3").submit();
}

function ShowCancelStudentSubscription(studentSubscriptionId, studentId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentSubscription/CancelSubscription/",
        data: { studentSubscriptionId, studentId },
        success: function (data) {
            HideSpinner();
            $('#CancelStudentSubscription .modal-content').html(data);
            Installment(studentSubscriptionId, studentId);
            Fees(studentSubscriptionId, studentId);
            $('#CancelStudentSubscription').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function CancelSubscription() {
    $("#Cancel_Subscription").validate({
        errorElement: "span",
        rules: {
            CancellationReasonId: "required",
            Description: "required"
        }
    })

    let obj = {
        StudentId: $("#StudentId").val(),
        StudentSubscriptionId: $("#StudentSubscriptionId").val(),
        ReasonId: $("#CancellationReasonId").val(),
        Description: $("#Description").val(),
        TotalInstellmentAmount: $("#TotalInstellmentAmount").val(),
        InstellmentAmount: $("#InstellmentAmount").val(),
        DueDate: $("#DueDate").val(),
        TotalFeesAmount: $("#TotalFeesAmount").val(),
        FeesAmount: $("#FeesAmount").val(),
        FeesId: $("#FeesId").val(),
    }

    if ($("#Cancel_Subscription").valid()) {
        ShowSpinner();
        $.ajax({
            type: "POST",
            url: "/Sales/StudentSubscription/CancelSubscription",
            data: {
                cancellationSubscriptionViewModel:obj
            },
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                toastr.success(SuccesCancel);
                $('#CancelStudentSubscription').modal("hide");
                HideSpinner();
                GetStudentSubscriptions();
            },
            error: function (data) {
                HideSpinner();
                toastr.error(ErrorCancel);
            }
        });
    }
}