$(function () {
    Get();
});

function Get() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/Accounting/GetData",
        dataType: 'html',
        success: function (data) {
            HideSpinner();
            $('#main').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function GetData(data) {
    $('#main').html(data);
}

function ShowEditTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/Accounting/ShowTable/",
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
        $("#Send_Btn").prop("disabled", false)
    else
        $("#Send_Btn").prop("disabled", true)
}


function Save() {
    $.ajax({
        type: "POST",
        url: "/Sales/Accounting/Save",
        data: {ids : arr},
        success: function (data) {
            ShowSave();
        },
        error: function (data) {
            toastr.error(ErrorMessage);
        }
    });
}

function ShowSave() {
    $.ajax({
        type: "POST",
        url: "/Sales/Accounting/ShowSave",
        dataType: 'html',
        data: { ids: arr },
        success: async function (data) {
            $('#main').html(data);
            await GetSavedData('subscription', subscription)
            await GetSavedData('installment', installment)
            await GetSavedData('payment', payment)
            await GetSavedData('discount', discount)
            await GetSavedData('fee', fee)
            await GetSavedData('feePayment', feePayment)
        }
    });
}

async function GetSavedData(name, type) {
    var res = await $.ajax({
        type: "POST",
        url: "/Sales/Accounting/GetSavedData",
        dataType: 'html',
        data: { ids: arr.map(String).join(","), type},
        success: function (data) {
            $("#" + name + "").html(data);
        },
        error: function (data) {
            toastr.error(ErrorMessage);
        }
    });

    return res;
}

function ChangeAccountPagenation(current, title) {
    $("#" + title.toString().split(' ')[1]).find("#pagination").val(current.value);
    $("#" + title.toString().split(' ')[1]).submit();
}

function Subscription (data) {
    $("#subscription").html(data);
}

function Installment (data) {
    $("#installment").html(data);
}

function Payment (data) {
    $("#payment").html(data);
}

function Discount (data) {
    $("#discount").html(data);
}

function Fee (data) {
    $("#fee").html(data);
}

function FeePayment (data) {
    $("#feePayment").html(data);
}

async function Print() {
    $("#Print").hide();
    $("#hide_print").hide();
    await GetPrintData('subscription', subscription)
    await GetPrintData('installment', installment)
    await GetPrintData('payment', payment)
    await GetPrintData('discount', discount)
    await GetPrintData('fee', fee)
    await GetPrintData('feePayment', feePayment)
    window.print()
}

async function GetPrintData(name, type) {
    var res = await $.ajax({
        type: "POST",
        url: "/Sales/Accounting/GetPrintData",
        dataType: 'html',
        data: { ids: arr.map(String).join(","), type },
        success: function (data) {
            $("#" + name + "").html(data);
        },
        error: function (data) {
            toastr.error(ErrorMessage);
        }
    });
    return res;
}

window.addEventListener('afterprint', (event) => {
    ShowSave();
    $("#hide_print").show();
});