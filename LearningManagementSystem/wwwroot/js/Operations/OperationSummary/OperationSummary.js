function Get() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Operations/OperationSummary/GetData",
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

function ShowEditTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Operations/OperationSummary/ShowTable/",
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

function ShowEditStudentTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Operations/OperationSummary/ShowStudentTable/",
        success: function (data) {
            HideSpinner();
            $('#EditTable .modal-content').html(data);
            $(".td").map(function () {
                table1.forEach(element => {
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

function ShowStudents(id, fetched, unFetched) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Operations/OperationSummary/GetStudents",
        dataType: 'html',
        data: { id, fetched, unFetched },
        success: function (data) {
            HideSpinner();
            $('#Students .modal-content').html(data);
            $("#Students").modal('show');
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function Details(id, langId, contactTypeId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/UserProfiles/ShowDetails/",
        data: { id: id, languageId: langId, contactTypeId: contactTypeId },
        success: function (data) {
            HideSpinner();
            $('#StudentDetails .modal-content').html(data);
            $('#StudentDetails').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function GetData(data) {
    $('#main').html(data);
}

function GetData1(data) {
    $('#StudentsDiv').html(data);
}

function ChangePagenation1() {
    $("#pagination1").val($("#pagin1").val())
    $("#Form2").submit();
}

function ChangeTable1() {
    table1 = [];
    $(".TableCheckBox").map(function () {
        if ($(this).is(':checked'))
            table1.push($(this).parent().parent().find(".td").html().trim())
    })
    $("#table1").val(table1);
    $("#Form2").submit();
    $("#EditTable").modal("hide");
}

function GetTable1(s) {
    $(s).map(function (i, x) {
        table1.forEach(element => {
            if (x == element) {
                $("." + x + "").removeClass("d-none");
                $(this).parent().find("input[type='checkbox']").prop('checked', true)
            }
        }
        );
    })
}

//Sms//

function SetAll(item) {
    if ($(item).is(':checked'))
        $(".Checkbox").map((i, x) => {
            $(x).prop('checked', true);
            CheckboxClicked($(x), $(x).val())
        })
    else {
        $("#SetAll").prop('checked', false);
        $(".Checkbox").map((i, x) => {
            $(x).prop('checked', false);
            CheckboxClicked($(x), $(x).val())
        })
    }
}

function CheckboxClicked(item, current) {
    arr.map(function (x) {
        if (current == x)
            arr = arr.filter(e => e !== x)
    })
    if ($(item).is(':checked'))
        arr.push(current)

    SetDisable(arr.length)
}

function SetDisable(i) {
    if (i > 0)
        $("#Send_SMS_Btn").prop("disabled", false)
    else
        $("#Send_SMS_Btn").prop("disabled", true)
}

function ShowSendSms() {
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