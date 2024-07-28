function Get() {
    var URL = "/ControlPanel/InvoicesPay/GetData";
    if ($("#InvoicesPayCurrentUrl").val())
        URL = $("#InvoicesPayCurrentUrl").val();

    ShowSpinner();
    $.ajax({
        type: "GET",
        url: URL,
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


function BtnSearchRestPageNumber() {
    $('#Page').val("0");
}
function GetData(data) {
    $('#main').html(data);
}
function ShowImageView(ImageUrl) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/InvoicesPay/ShowImage/",
        data: { ImageUrl: ImageUrl },
        success: function (data) {
            HideSpinner();
            $('#ShowInvoicesPay .modal-content').html(data);
            $('#ShowInvoicesPay').modal("show");
        }

    });
}
function ShowChangeInvoicesPayStatus(id) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/InvoicesPay/ShowChangeInvoicesPayStatus/",
        data: { id: id },
        success: function (data) {
            HideSpinner();
            $('#ShowChangeStatusInvoicePay .modal-content').html(data);
            $('#ShowChangeStatusInvoicePay').modal("show");
        }

    });
}

var ChangeInvoicesPayStatusFrm = $('#ChangeInvoicesPayStatus-Create');
ChangeInvoicesPayStatusFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: ChangeInvoicesPayStatusFrm.attr('method'),
        url: ChangeInvoicesPayStatusFrm.attr('action'),
        data: ChangeInvoicesPayStatusFrm.serialize(),
        headers: {
            'RequestVerificationToken': Token
        }, 
        success: function (data) {
            
            if (data.result == "Success") {
                toastr.success(addMassege);
                $('#ShowChangeStatusInvoicePay').modal("hide");
                HideSpinner();
                Get();
            } else if (data.result == "FailContactNotRegisteredAsStudent") {
                HideSpinner();
                toastr.error(FailContactNotRegisteredAsStudent);
            } else if (data.result == "FailMaxEnrollStudent") {
                HideSpinner();
                toastr.error(FailMaxEnrollStudent);
            } else if (data.result == "FailExistsStudent") {
                HideSpinner();
                toastr.error(FailExistsStudent);
            } else if (data.result == "FailAgeAllowedForRegistration") {
                HideSpinner();
                toastr.error(FailAgeAllowedForRegistration);
            }
            else if (data.result == "HasPreRequestsCourse") {
                HideSpinner();
                toastr.error(HasPreRequestsCourse);
            }
            else if (data.result == "FailAgeGroup") {
                HideSpinner();
                toastr.error(FailAgeGroup);
            } else {
                HideSpinner();
                toastr.error(addMassegeError);
            }
        },
        error: function (data) {
            HideSpinner();
            toastr.error(addMassegeError);
        },
    });
});