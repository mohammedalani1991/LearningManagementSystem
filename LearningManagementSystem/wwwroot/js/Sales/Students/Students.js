$(function () {
    Get();
});

function Get() {
    $.ajax({
        type: "GET",
        url: "/Sales/Students/GetData",
        dataType: 'html',
        success: function (data) {
            $('#main').html(data);
        }
    });
}

function SalesStudentsToggles(){
    if($("#EditCompanyToggle").is(":hidden"))
        return;
    $(".tms-nav-div").toggle();
    $("#myTabContent").toggle();
}
function ShowEditTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/Students/ShowTable/",
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

function ShowAdvisor(id, advicer) {
    $("#StudentId").val(id);
    $("#Advicer").val(advicer).trigger("chosen:updated");
    $("#TransferAdviser").modal('show');
}

function SetAdvisor() {
    if ($("#SetAdvisor").valid()) {
        ShowSpinner();
        $.ajax({
            type: "POST",
            url: "/Sales/Students/SetAdvisor",
            data: { id: $("#StudentId").val(), advisorId: $("#Advicer").val() },
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                toastr.success(SuccesCreate);
                $("#TransferAdviser").modal('hide');
                HideSpinner();
            },
            error: function (data) {
                HideSpinner();
                toastr.error(ErrorCreate);
            }
        });
    }
}

$("#TransferAdviser").on('hidden.bs.modal', function () {
    $("#Advicer").val().trigger("chosen:updated");
})

function ShowObserver(id) {
    $("#StudentId").val(id);
    $("#Observer").modal('show');
}

function SetObserver() {
    $("#SetObserver").validate({
        errorElement: "span",
        rules: {
            ReasonId: "required",
            CommunicationEmployeeIds: "required"
        }
    })

    var obj = {
        Ids:arr1,
        StudentId: $("#StudentId").val(),
        CommunicationEmployeeIds: $("#CommunicationEmployeeIds").val(),
        ReasonId: $("#ReasonId").val(),
        IsUrgent: $("#IsUrgent").is(":checked"),
        Note: $("#Note").val(),
    }

    if ($("#SetObserver").valid()) {
        ShowSpinner();
        $.ajax({
            type: "POST",
            url: "/Sales/Students/SetObserver",
            data: { transferViewModel: obj },
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                toastr.success(SuccesCreate);
                $("#Observer").modal('hide');
                HideSpinner();
                SetAll();
            },
            error: function (data) {
                HideSpinner();
                toastr.error(ErrorCreate);
            }
        });
    }
}

function GetStudentSubscriptions() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentSubscription/GetData",
        data: { studentId: $('#StudentId').val() },
        dataType: 'html',
        success: function (data) {
            HideSpinner();
            $('#GetData').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function GetStudentSubscriptionPayments(cheque) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentSubscriptionPayment/GetData",
        data: { studentId: $('#StudentId').val(), cheque: cheque },
        dataType: 'html',
        success: function (data) {
            HideSpinner();
            $('#GetData').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function GetStudentSubscriptionDiscounts() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentSubscriptionDiscounts/GetData",
        data: { studentId: $('#StudentId').val() },
        dataType: 'html',
        success: function (data) {
            HideSpinner();
            $('#GetData').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function GetStudentSubscriptionInstallments() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentSubscriptionInstallment/GetData",
        data: { studentId: $('#StudentId').val() },
        dataType: 'html',
        success: function (data) {
            HideSpinner();
            $('#GetData').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}
function GetStudentNotes() {
    ShowSpinner(); 
    $.ajax({
        type: "GET",
        url: "/Sales/StudentNote/GetData",
        data: { studentId: $('#StudentId').val() },
        dataType: 'html',
        success: function (data) {
            HideSpinner();
            $('#GetData').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function GetStudentSubscriptionFees() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentSubscriptionFees/GetData",
        data: { studentId: $('#StudentId').val() },
        dataType: 'html',
        success: function (data) {
            HideSpinner();
            $('#GetData').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}
function GetStudentSubscriptionFeesPayment() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentSubscriptionFeesPayment/GetData",
        data: { studentId: $('#StudentId').val() },
        dataType: 'html',
        success: function (data) {
            HideSpinner();
            $('#GetData').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}
function GetStudentSubscriptionTransactions() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentSubscriptionTransaction/GetData",
        data: { studentId: $('#StudentId').val() },
        dataType: 'html',
        success: function (data) {
            HideSpinner();
            $('#GetData').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}
function GetLeadNotes() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Marketing/LeadNotes/GetData",
        data: { studentId: $('#StudentId').val() },
        dataType: 'html',
        success: function (data) {
            HideSpinner();
            $('#GetData').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function GetStudentAttendance() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/StudentAttendance/GetData",
        data: { studentId: $('#StudentId').val() },
        dataType: 'html',
        success: function (data) {
            HideSpinner();
            $('#GetData').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function SetChoices(name) {
    setTimeout(function () {
        var multipleCancelButton = new Choices(name, {
            removeItemButton: true,
            maxItemCount: 100,
            searchResultLimit: 100,
            renderChoiceLimit: 100,
            noResultsText: 'No results found',
            noChoicesText: 'No choices to choose from',
        });
    }, 50);
}


$("#Observer").on("hidden.bs.modal", function () {
    $("#CommunicationEmployeeIds").val("").trigger("chosen:updated");
    $("#ReasonId").val("").trigger("chosen:updated");
    $("#SetObserver")[0].reset();
    $("#Observer .error").hide();
});


function Edit() {
    var studentFrm = $('#StudentsForm-Edit');
    ShowSpinner();
    $.ajax({
        type: studentFrm.attr('method'),
        url: studentFrm.attr('action'),
        data: studentFrm.serialize(),
        success: function (data) {
            HideSpinner();
            toastr.success(SuccesEdit);
            EditStudent();
        },
        error: function (data) {
            HideSpinner();
            toastr.error(ErrorEdit);
        },
    });
}

//Sms//

function SetAll(item) {
    if ($(item).is(':checked'))
        $(".Checkbox").map((i, x) => {
            $(x).prop('checked', true);
            CheckboxClicked($(x), $(x).val(), $(x).parent().find(".id").val())
        })
    else {
        $("#SetAll").prop('checked', false);
        $(".Checkbox").map((i, x) => {
            $(x).prop('checked', false);
            CheckboxClicked($(x), $(x).val(), $(x).parent().find(".id").val())
        })
    }
}

function CheckboxClicked(item, current, student) {
    arr.map(function (x) {
        if (current == x)
            arr = arr.filter(e => e !== x)
    })
    if ($(item).is(':checked'))
        arr.push(current)

    SetDisable(arr.length)

    arr1.map(function (x) {
        if (student == x)
            arr1 = arr1.filter(e => e !== x)
    })
    if ($(item).is(':checked'))
        arr1.push(student)
}

function SetDisable(i) {
    if (i > 0) {
        $("#Send_Btn").prop("disabled", false)
        $("#Send_SMS_Btn").prop("disabled", false)
    }
    else {
        $("#Send_Btn").prop("disabled", true)
        $("#Send_SMS_Btn").prop("disabled", true)
    }
}

function ShowSendSms(id) {
    if (id)
        arr.push(id)
    ShowSpinner();

    $.ajax({
        type: "GET",
        url: "/Global/Sms/GetSmsStudent/",
        success: function (data) {
            HideSpinner();
            $('#Sms .modal-content').html(data);
            $('#Sms').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function SendSms() {
    $("#Send_Sms").validate({
        errorElement: "span",
        rules: {
            SmsMessage: "required",
        }
    })
    let obj = {
        Ids: arr,
        Message: $("#SmsMessage").val(),
        TypeId: typeId,
        Source: smsSource,
        IsExtraMobile: $("#IsExtraMobile").is(":checked"),
    }

    if ($("#Send_Sms").valid()) {
        ShowSpinner();
        $.ajax({
            type: "POST",
            url: "/Global/Sms/SendSms/",
            data: { smsViewModel: obj },
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                HideSpinner();
                toastr.success(SuccesCreate);
                $('#Sms').modal("hide");
                SetAll();
            },
            error: function (data) {
                toastr.error(ErrorCreate);
                HideSpinner();
            },
        }).fail(function (jqXHR, textStatus, error) {
            HideSpinner();
        });
    }
}

$("#Sms").on('hidden.bs.modal', function () {
    $("#Send_Sms").trigger("reset")
})

function SetMesaage(data) {
    if (data.value > 0)
        $("#SmsMessage").val($(data).find('option:selected').text());
}


function onDdlChanged(langId) {
    EditStudent(langId.value);
}

function GetData(data) {
    $('#main').html(data);
}