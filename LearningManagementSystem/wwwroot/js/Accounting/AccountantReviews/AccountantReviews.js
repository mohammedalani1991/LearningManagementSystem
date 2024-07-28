$(async function () {
    await Get('subscription', subscription)
    await Get('installment', installment)
    await Get('payment', payment)
    await Get('discount', discount)
    await Get('fee', fee)
    await Get('feePayment', feePayment)
});

async function Get(name, type) {
    var res = await $.ajax({
        type: "POST",
        url: "/Accounting/AccountantReviews/GetData",
        dataType: 'html',
        data: { type },
        success: function (data) {
            $("#" + name + "").html(data);
        },
        error: function (data) {
            toastr.error(ErrorMessage);
        }
    });

    return res;
}

function SetAll(item,title) {
    if ($(item).is(':checked'))
        $(".Checkbox." + title.toString().split(' ')[1] +"").map((i, x) => {
            $(x).prop('checked', true);
            CheckboxClicked($(x), $(x).val())
        })
    else {
        $("#SetAll").prop('checked', false);
        $(".Checkbox." + title.toString().split(' ')[1] + "").map((i, x) => {
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
        url: "/Accounting/AccountantReviews/Save",
        data: { ids: arr },
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
        url: "/Accounting/AccountantReviews/ShowSave",
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
        url: "/Accounting/AccountantReviews/GetSavedData",
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