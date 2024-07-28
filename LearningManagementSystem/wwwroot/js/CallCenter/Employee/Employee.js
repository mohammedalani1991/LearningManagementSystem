function Get() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/CallCenter/Employee/GetData",
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

function ShowAddEmployee() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/CallCenter/Employee/ShowCreate/",
        success: function (data) {
            HideSpinner();
            $('#EditEmployeeDiv').empty();
            $('#CreateEmployee .modal-content').html(data);
            $('#CreateEmployee').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowEditTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/CallCenter/Employee/ShowTable/",
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


function ShowDeleteEmployee(id) {
    $("#EmployeeId").val(id);
    $('#Delete').modal("show");
}

function Delete() {
    ShowSpinner();
    $.post("/CallCenter/Employee/DeleteEmployee/",
        { id: $("#EmployeeId").val() },
        function (data) {
            if (data !== null) {
                $('#Delete').modal("hide");
                HideSpinner();
                Get();
            } else {
                HideSpinner();
                toastr.error(ErrorMessage);
            }
        }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function onDdlChanged(langId) {
    ShowEditEmployee($('#Id').val(), langId.value);
}


function ShowEditEmployee(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/CallCenter/Employee/ShowEdit/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#CreateEmployeeDiv').empty();
            $('#EditEmployee .modal-content').html(data);
            $('#EditEmployee').modal("show");
            model.roles.map((x, i) => {
                $("#RoleIds option[value='" + x.roleId + "']").prop("selected", true);
            })
        }

    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function AddEmployee() {
    $("#Employee-Create").validate({
        errorElement: "span",
        rules: {
            LanguageId: "required",
            FirstName: "required",
            LastName: "required",
            IdNumber: {
                number: true,
                required: true
            },
            GenderId: "required",
            Email: {
                email: true,
                required: true
            },
            CreatePassword: {
                required: true,
                custompassword: true
            },
            ConformPassword: {
                required: true,
                equalTo: "#CreatePassword"
            },
            Mobile: {
                number: true,
                required: true
            },
            StartWorkDate: "required",
            JobId: "required",
            BranchId: "required",
            RoleIds: "required",
        },
        messages: {
            ConformPassword: {
                equalTo: ConformPasswordMessage,
            }
        }
    })

    let obj = {
        LanguageId: $("#LanguageId").val(),
        FirstName: $("#FirstName").val(),
        SecondName: $("#SecondName").val(),
        ThirdName: $("#ThirdName").val(),
        LastName: $("#LastName").val(),
        'Contact.IdNumber': $("#IdNumber").val(),
        'Contact.GenderId': $("#GenderId").val(),
        'Contact.Email': $("#Email").val(),
        Password: $("#CreatePassword").val(),
        'Contact.Mobile': $("#Mobile").val(),
        StartWorkDate: $("#StartWorkDate").val(),
        JobId: $("#JobId").val(),
        'Contact.BranchId': $("#BranchId").val(),
        RoleIds: $("#RoleIds").val(),
    }

    if ($("#Employee-Create").valid()) {
        ShowSpinner();
        $.ajax({
            type: "POST",
            url: "/CallCenter/Employee/CreateEmployee/",
            data: { employeeViewModel: obj },
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                
                if (data.success) {
                    toastr.success(SuccesCreate);
                    $('#CreateEmployee').modal("hide");
                    HideSpinner();
                    Get()
                }
                else {
                    HideSpinner();
                    toastr.error(data.message);
                }
            },
            error: function (data) {
                HideSpinner();
                toastr.error(ErrorCreate);
            },
        });
    }
}

function EditEmployee() {
    $("#Employee-Edit").validate({
        errorElement: "span",
        rules: {
            LanguageId: "required",
            FirstName: "required",
            LastName: "required",
            IdNumber: {
                number: true,
                required: true
            },
            'Contact.GenderId': "required",
            Email: {
                email: true,
                required: true
            },
            Mobile: {
                number: true,
                required: true
            },
            StartWorkDate: "required",
            JobId: "required",
            BranchId: "required",
            RoleIds: "required",
        }
    })

    let obj = {
        Id: $("#Id").val(),
        LanguageId: $("#LanguageId").val(),
        FirstName: $("#FirstName").val(),
        SecondName: $("#SecondName").val(),
        ThirdName: $("#ThirdName").val(),
        LastName: $("#LastName").val(),
        'Contact.IdNumber': $("#IdNumber").val(),
        'Contact.GenderId': $("#Contact_GenderId").val(),
        'Contact.Email': $("#Email").val(),
        Password: $("#Password").val(),
        'Contact.Mobile': $("#Mobile").val(),
        StartWorkDate: $("#StartWorkDate").val(),
        JobId: $("#JobId").val(),
        'Contact.BranchId': $("#BranchId").val(),
        RoleIds: $("#RoleIds").val(),
    }

    if ($("#Employee-Edit").valid()) {
        ShowSpinner();
        $.ajax({
            type: "POST",
            url: "/CallCenter/Employee/EditEmployee/",
            data: { employeeViewModel: obj },
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                toastr.success(SuccesEdit);
                $('#EditEmployee').modal("hide");
                HideSpinner();
                Get()
            },
            error: function (data) {
                HideSpinner();
                toastr.error(ErrorEdit);
            },
        });
    }
}

function GetData(data) {
    $('#main').html(data);
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
                toastr.success(SuccesCreate);
                $('#Sms').modal("hide");
                HideSpinner();
                SetAll();
            },
            error: function (data) {
                HideSpinner();
                toastr.error(ErrorCreate);
            },
        });
    }
}

$("#Sms").on('hidden.bs.modal', function () {
    $("#Send_Sms").trigger("reset")
})

//ResetPassword

function ShowResetPassword(id) {
    $("#Password-error , #ConformPassword-error").remove();
    $("#ResetPasswordForm").trigger("reset");
    $("#userId").val(id);
    $("#ResetPassword").modal("show");
}

function ResetPassword() {
    if ($("#ResetPasswordForm").valid()) {
        $.ajax({
            type: "POST",
            url: "/CallCenter/Employee/ResetPassword",
            data: {
                id: $("#userId").val(), password: $("#Password").val()
            },
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (msg) {
                $("#ResetPassword").modal("hide");
                toastr.success(changePassowrdSuccessfully)
            },
            error: function (req, status, error) {
                toastr.error(changePassowrdError);
            }
        });
    }
}