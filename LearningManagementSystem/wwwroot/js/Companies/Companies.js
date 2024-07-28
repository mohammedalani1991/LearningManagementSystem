function Get() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/Companies/GetData",
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

function ShowAddCompany() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/Companies/ShowCreate/",
        success: function (data) {
            HideSpinner();
            $('#CreateCompany .modal-content').html(data);
            $('#CreateCompany').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowEditTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/Companies/ShowTable/",
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


var companyFrm = $('#Company-Create');
companyFrm.submit(function (e) {
    e.preventDefault();
    ShowSpinner();
    $.ajax({
        type: companyFrm.attr('method'),
        url: companyFrm.attr('action'),
        data: companyFrm.serialize(),
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            HideSpinner();
            toastr.success(addMassege);
            $('#CreateCompany').modal("hide");
            Get()
        },
        error: function (data) {
            HideSpinner();
            toastr.error(addMassegeError);
        },
    });
});

function ShowDetailsCompany(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/Companies/ShowDetails/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#DetailsCompany .modal-content').html(data);
            $('#DetailsCompany').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function ShowCompanyIdentifiers(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/Companies/ShowCompanyIdentifiers/",
        data: { companyId: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#CompanyIdentifier .modal-content').html(data);
            $('#CompanyIdentifier').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowDeleteCompany(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/Companies/ShowDelete/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#DeleteCompany .modal-content').html(data);
            $('#DeleteCompany').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ConfirmDelete(id) {
    ShowSpinner();
    $.post("/Companies/Companies/DeleteCompany/",
        { id: id },
        function (data) {
            if (data !== null) {
                HideSpinner();
                toastr.success(deleteMassege);
                $('#DeleteCompany').modal("hide");
                Get()
            } else {
                toastr.error(deleteMassegeError);
            }
        }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function onDdlChanged(langId) {
    ShowEditCompany($('#CompanyId').val(), langId.value);
}

function EditCompany() {
    ShowSpinner();
    var companyEditFrm = $('#EditCompany');
    if (companyEditFrm.valid()) {
        $.ajax({
            type: companyEditFrm.attr('method'),
            url: companyEditFrm.attr('action'),
            data: companyEditFrm.serialize(),
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                HideSpinner();
                toastr.success(editMassege);
                GetEdit();
            },
            error: function (data) {
                toastr.error(editMassegeError);
                HideSpinner();
            },
        })
    }
}

function ShowEditCompany(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/Companies/ShowEdit/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#EditCompany .modal-content').html(data);
            $('#EditCompany').modal("show");
        }

    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

var companyAdvisorForm = $('#CompanyAdvisorForm');
companyAdvisorForm.submit(function (e) {
    e.preventDefault();
    $.ajax({
        type: companyAdvisorForm.attr('method'),
        url: companyAdvisorForm.attr('action'),
        data: companyAdvisorForm.serialize(),
        success: function (data) {
            toastr.success(addMassege);

            GetAdvisors();

        },
        error: function (data) {
            toastr.error(deleteMassegeError);
            console.log(data);
        },
    });
});


function DeleteCompanyAdvisors(contactId , companyId) {
    $.post("/Companies/Companies/DeleteCompanyAdvisors/",
        { contactId: contactId, companyId: companyId },
        function (data) {
            if (data !== null) {
                toastr.success(deleteMassege);

                GetAdvisors();
;
            } else {
                toastr.error(deleteMassegeError);
            }
        });
}
function EditCompanyToggle(){
    if($("#EditCompanyToggle").is(":hidden"))
        return;
    $("#RightTmsNavDiv").toggle();
    $("#myTabContent").toggle();
}
$("#first-tab").click(function () {
    GetEdit();
})

$("#second-tabBranches").click(function () {
    GetBranches();
})

$("#thered-tabTender").click(function () {
    GetTenders();
})
$("#forth-tabStudent").click(function () {
    GetStudents();
})

$("#fifeth-tabPayment").click(function () {
    GetPayments();
})
$("#sixth-tabNote").click(function () {
    GetNotes();
})
$("#seventh-tabService").click(function () {
    GetServices();
})
$("#eleven-tabServicePayment").click(function () {
    GetServicePayments();
})
$("#eight-tabIdentifier").click(function () {
    GetIdentifieres();
})
$("#ninth-tabAdvisors").click(function () {
    GetAdvisors();
})
$("#tenth-tabInstallment").click(function () {
    GetCompanyInstallment();
})
$("#eleven-tabFinancialConstraints").click(function () {
    GetFinancialConstraint();
})

function removeAllChildNodes() {
    $(".modal-backdrop").remove()
    while ($('#GetData').firstChild) {
        $('#GetData').removeChild(parent.firstChild);
    }
}

function GetEdit(langId = null) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/Companies/ShowEdit/",
        data: {
            id: $('#companyId').val(), languageId: langId ? langId : $('#LanguageId').val()
        },
        success: function (data) {
            HideSpinner();
            removeAllChildNodes()
            $('#GetData').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function GetBranches() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/CompanyBranches/GetData/",
        data: {
            companyId: $('#companyId').val(), languageId: $('#LanguageId').val()
        },
        success: function (data) {
            HideSpinner();
            removeAllChildNodes()
            $('#GetData').html(data);
            let s = ["Name", "Address", "Phone", "Major", "Status", "CreatedBy", "CreatedOn"]
            GetTable(s);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });

}

function GetTenders() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/CompanyTenders/GetData/",
        data: {
            companyId: $('#companyId').val(), languageId: $('#LanguageId').val()
        },
        success: function (data) {
            HideSpinner();
            removeAllChildNodes()
            $('#GetData').html(data);
            let s = ["Title", "Name", "Mobile", "Amount","Branch", "Status", "CreatedBy", "CreatedOn"]
            GetTable(s);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function GetStudents() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/Students/GetData/",
        data: {
            companyId: $('#companyId').val(), languageId: $('#LanguageId').val(), contactTypeId: $('#StudentContactTypeId').val()
        },
        success: function (data) {
            HideSpinner();
            removeAllChildNodes()
            $('#GetData').html(data);
            let s = ["FullName", "Email", "Mobile", "Address", "IdNumber", "Status", "CreatedBy", "CreatedOn"]
            GetTable(s);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}
function GetPayments() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/CompanyPayments/GetData/",
        data: {
            companyId: $('#companyId').val(), languageId: $('#LanguageId').val()
        },
        success: function (data) {
            HideSpinner();
            removeAllChildNodes()
            $('#GetData').html(data);
            let s = ["Ammont", "Cheque", "Number", "Name", "Bank", "Owner", "Status", "CreatedBy", "CreatedOn"]
            GetTable(s);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}
function GetNotes() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/CompanyNotes/GetData/",
        data: {
            companyId: $('#companyId').val(), languageId: $('#LanguageId').val()
        },
        success: function (data) {
            HideSpinner();
            removeAllChildNodes()
            $('#GetData').html(data);
            let s = ["Title", "Description", "CompanyNote", "Status", "CreatedBy", "CreatedOn"]
            GetTable(s);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}
function GetServices() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/Companies/ShowCompanyServices/",
        data: {
            companyId: $('#companyId').val(), languageId: $('#LanguageId').val()
        },
        success: function (data) {
            HideSpinner();
            removeAllChildNodes()
            $('#GetData').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}
function GetServicePayments() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/CompanyServicePayment/GetData/",
        data: {
            companyId: $('#companyId').val(), languageId: $('#LanguageId').val()
        },
        success: function (data) {
            HideSpinner();
            removeAllChildNodes()
            $('#GetData').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}
function GetIdentifieres() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/Companies/ShowCompanyIdentifiers/",
        data: {
            companyId: $('#companyId').val(), languageId: $('#LanguageId').val()
        },
        success: function (data) {
            HideSpinner();
            removeAllChildNodes()
            $('#GetData').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function GetCompanyInstallment() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/CompanyInstallment/GetData/",
        data: {
            companyId: $('#companyId').val()
        },
        success: function (data) {
            HideSpinner();
            removeAllChildNodes()
            $('#GetData').html(data);
            let s = ["Amount", "PaidAmount", "DueDate", "Editabled", "Paid", "Status", "CreatedBy", "CreatedOn"]
            GetTable(s);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function GetFinancialConstraint() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/FinancialConstraints/GetData/",
        data: {
            companyId: $('#companyId').val()
        },
        success: function (data) {
            HideSpinner();
            removeAllChildNodes()
            $('#GetData').html(data);
            let s = ["Amount", "SubscriptionDate", "Subscription", "SubscriptionType", "FullName"]
            GetTable(s);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowFinancialConstraintTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/FinancialConstraints/ShowTable/",
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
function GetService() {
    $.ajax({
        type: "GET",
        url: "/Companies/Services/GetData/",
        data: {
            languageId: $('#LanguageId').val()
        },
        success: function (data) {
            removeAllChildNodes()
            $('#GetData').html(data);
            let s = ["Title", "Description", "Amount", "Branch", "Status", "CreatedBy", "CreatedOn"]
            GetTable(s);
        }
    });
}

function GetAdvisors() {
    $.ajax({
        type: "GET",
        url: "/Companies/Companies/ShowCompanyAdvisors/",
        data: {
            companyId: $('#companyId').val(), languageId: $('#LanguageId').val()
        },
        success: function (data) {
            removeAllChildNodes()
            $('#GetData').html(data);
        }
    });
}


function GetData(data) {
    $('#main').html(data);
}

//Email//

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

function ShowSendEmail() {
    $("#CompanyEmail").modal("show");
}


$("#CompanyEmail").on('hidden.bs.modal', function () {
    $("#Send_Email").trigger("reset")
})

function SendEmail() {
    $("#Send_Email").validate({
        errorElement: "span",
        rules: {
            EmailTitle: "required",
        }
    })

    $('#EmailMessage').val(tinymce.get("EmailMessage").getContent())

    let obj = {
        Ids: arr,
        Title: $("#EmailTitle").val(),
        ProgramLink: $("#EmailCourse").val(),
        ImageLink: $("#EmailImage").val(),
        Message: $("#EmailMessage").val(),
    }

    if ($("#Send_Email").valid() && $('#EmailMessage').val()) {
        ShowSpinner();
        $.ajax({
            type: "POST",
            url: "/Companies/CompanyEmail/SendEmail/",
            data: { companyEmailViewModel: obj },
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                HideSpinner();
                toastr.success(addMassege);
                $('#CompanyEmail').modal("hide");
                SetAll();
            },
            error: function (data) {
                HideSpinner();
                toastr.error(addMassegeError);
            },
        });
    }
    else if (!$('#EmailMessage').val()) {
        $("#EmailMessage-error").show();
        $("#EmailMessage-error").html(Requiredfield);
    }
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

//Sms//

function ShowSendSms() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Global/Sms/GetSms/",
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
        IsExtraMobile: false,
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
                toastr.success(addMassege);
                $('#Sms').modal("hide");
                SetAll();
            },
            error: function (data) {
                HideSpinner();
                toastr.error(addMassegeError);
            },
        });
    }
}

$("#Sms").on('hidden.bs.modal', function () {
    $("#Send_Sms").trigger("reset")
})