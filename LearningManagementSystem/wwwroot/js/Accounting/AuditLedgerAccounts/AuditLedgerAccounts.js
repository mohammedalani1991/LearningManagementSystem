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
        url: "/Accounting/AuditLedgerAccounts/GetData",
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