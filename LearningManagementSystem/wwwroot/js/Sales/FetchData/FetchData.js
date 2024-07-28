$(function () {
    // SetChoices("#CommunicationEmployeeIds");
});

function Get() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/FetchData/GetData",
        dataType: 'html',
        success: function (data) {
            HideSpinner();
            $('#main').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

Get()

function GetEdit(id, contactId, key, open = true) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/FetchData/GetEdit",
        dataType: 'html',
        data: { id, contactId, key },
        success: function (data) {
            HideSpinner();
            $('#Edit .modal-content').html(data);
            if (open)
                $('#Edit').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function OpenTransfer(id, name, title) {
    document.getElementById('TransferName').innerHTML = TransferText + " " + name;
    $("#EmployeeId").val(id);
    $("#Advicer").val(id);
    $("#FetchTitle").val(title);
    $('#Transfer').modal("show");
}

function Transfer() {
    var obj = {
        Id: $("#EmployeeId").val(),
        Title: $("#FetchTitle").val(),
        ToAdvicer: $("#Advicer").val(),
        CommunicationEmployeeIds: $("#CommunicationEmployeeIds").val(),
        ReasonId: $("#ReasonId").val(),
    };

    ShowSpinner();
    $.ajax({
        type: "POST",
        url: "/Sales/FetchData/Transfer",
        data: { transferViewModel: obj },
        success: function (data) {
            $('#Transfer').modal("hide");
            HideSpinner();
            toastr.success(SuccesEdit);
            Get()
        },
        error: function (data) {
            HideSpinner();
            toastr.error(ErrorEdit);
        },
    });
}

function GetDelete(id, contactId) {
    $("#CommunicationChannelId").val(id);
    $("#ContactId").val(contactId);
    $("#Delete").modal('show');
}

function Delete() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/FetchData/Delete",
        dataType: 'html',
        data: { id: $("#CommunicationChannelId").val(), contactId: $("#ContactId").val() },
        success: function (data) {
            $('#Delete').modal("hide");
            HideSpinner();
            toastr.success(SuccesDelete);
            Get()
        },
        error: function (data) {
            HideSpinner();
            toastr.error(ErrorDelete);
        },
    });
}

function ShowEditTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Marketing/Lead/ShowTable/",
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

function SetInterest() {
    $("#Interest-Form").validate({
        errorElement: "span",
        rules: {
            Programs: "required",
            HowSerious: "required",
            Interest_Description: "required",
        }
    })
    if ($("#Interest-Form").valid()) {
        $("#Interest").val($("#Programs").find(":selected").text());
        $("#ProgramId").val($("#Programs").val());
        $("#HowSerious1").val($("#HowSerious").val());
        $("#Interest_Description1").val($("#Interest_Description").val());
        $("#InterestModal").modal("hide");
    }
}

function CheckBeforeFetch(id, id1, isCustom, contactId) {
    if (id) {
        ShowSpinner();
        $.ajax({
            type: "GET",
            url: "/Sales/FetchData/CheckBeforeFetch",
            dataType: 'json',
            data: { id: id },
            success: function (result) {
                HideSpinner();
                if (result.success) {
                    GetFetch(id, id1, isCustom, contactId)
                }
                else {
                    if (result.isUsed) {
                        Get();
                        if (id1 != 0 || isCustom != 0)
                            GetEdit(id1, isCustom, contactId, false)
                        toastr.warning(ErrorFetch);
                    }
                    else {
                        toastr.warning(result.message);
                    }
                }
            },
            error: function (data) {
                toastr.error(ErrorMessage);
            },
        }).fail(function (jqXHR, textStatus, error) {
            HideSpinner();
        });
    }
}

function GetFetch(id, id1, isCustom, contactId) {
    if (id) {
        $.ajax({
            type: "GET",
            url: "/Sales/FetchData/GetFetch",
            dataType: 'html',
            data: { id: id },
            success: function (result) {
                Get();
                if (id1 != 0 || isCustom != 0)
                    GetEdit(id1, isCustom, contactId, false)
                $('#Fetch .modal-content').html(result);
                $('#Fetch').modal("show");
            },
            error: function (data) {
                toastr.error(ErrorMessage);
            },
        });
    }
}

function Fetch() {
    $("#Fetch-Form").validate({
        errorElement: "span",
        rules: {
            Title: "required",
            Description: "required",
            NoteType: "required",
            NoteDate: "required",
            'ContactViewModel.Mobile': {
                required: true,
                number: true
            },
            ExtraMobile: {
                required: true,
                number: true
            },
            CollegeExam: "required",
            AcademicTest: "required",
        }
    })

    var obj = {
        Id: $("#Id").val(),
        'ContactViewModel.Mobile': $("#ContactViewModel_Mobile").val(),
        ExtraMobile: $("#ExtraMobile").val(),
        Title: $("#Title").val(),
        Description: $("#Description").val(),
        CollegeId: $("#CollegeExam").val(),
        SpecialtyId: $("#AcademicTest").val(),
        IsExternalStudy: $("#StudyOut").is(":checked"),
        IsTawjehe: $("#Options").is(":checked"),
        TypeId: $("#NoteType").val(),
        ProgramId: $("#ProgramId").val(),
        HowSerious1: $("#HowSerious1").val(),
        Interest_Description1: $("#Interest_Description1").val(),
    }

    if ($("#Fetch-Form").valid()) {
        if ($("#Interest").val() == "") {
            $("#InterestModal").modal("show");
        } else {
            ShowSpinner();
            $.ajax({
                type: "POST",
                url: "/Sales/FetchData/Fetch",
                data: { leadViewModel: obj },
                headers: {
                    'RequestVerificationToken': Token
                },
                success: function (data) {
                    toastr.success(SuccesEdit);
                    $('#Fetch').modal("hide");
                    $('#Edit').modal("hide");
                    HideSpinner();
                    $("#Interest-Form").trigger("reset");
                    Get()
                },
                error: function (data) {
                    HideSpinner();
                    toastr.error(ErrorEdit);
                },
            });
        }
    }
}

function GetData(data) {
    $('#Edit .modal-content').html(data);
}


function GetData1(data) {
    $('#Fetch .modal-content').html(data);
}

function ChangePagenation1() {
    $("#pagination").val($("#pagin").val())
    $("#Form2").submit();
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

$("#InterestModal").on("hidden.bs.modal", function () {
    setTimeout(function () {
        $("body").addClass("modal-open");
    }, 500)
});

//Sms

function ShowSendSms(ids) {
    ids.map(x => {
        arr.push(x);
    })
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
