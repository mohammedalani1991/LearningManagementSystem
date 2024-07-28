$(function () {
    // SetChoices("#CommunicationEmployeeIds");
    Get();
});

function Get() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/FollowUp/GetData",
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
        url: "/Sales/FollowUp/ShowTable/",
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

function ShowNotes(id) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Marketing/Lead/ShowNotes/",
        data: { id: id },
        success: function (data) {
            HideSpinner();
            $('#LeadNotes .modal-content').html(data);
            $('#LeadNotes').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function GetData1(data) {
    $("#LeadNotesDiv").html(data);
}

function ChangePagenation1() {
    $("#pagination1").val($("#pagin1").val());
    $("#Form2").submit();
}

function ShowAddNotes(id) {
    $.ajax({
        type: "GET",
        url: "/Marketing/Lead/ShowAddNotes/",
        success: function (data) {
            $('#LeadNotes').modal("hide");
            $('#AddLeadNotes .modal-content').html(data);
            $('#AddLeadNotes').modal("show");
            $("#AddNoteId").val(id);
        }
    });
}

function CloseAddNotes() {
    $('#AddLeadNotes').modal("hide");
    ShowNotes($("#AddNoteId").val())
}

function AddLeadNote() {
    $("#LeadNote-Create").validate({
        errorElement: "span",
        rules: {
            TypeId: "required",
            Title: "required",
            Description: "required",
        }
    })

    let obj = {
        Id: $("#AddNoteId").val(),
        TypeId: $("#TypeId").val(),
        Title1: $("#Title").val(),
        Description1: $("#Description").val(),
    }

    if ($("#LeadNote-Create").valid()) {
        ShowSpinner();
        $.ajax({
            type: "POST",
            url: "/Marketing/Lead/CreateLeadNote",
            data: { leadViewModel: obj },
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                toastr.success(SuccesCreate);
                $('#AddLeadNotes').modal("hide");
                HideSpinner();
                ShowNotes($("#AddNoteId").val())
            },
            error: function (data) {
                HideSpinner();
                toastr.error(ErrorCreate);
            },
        });
    }
}

function GetData2(data) {
    $("#LeadInterestDiv").html(data);
}

function ChangePagenation2() {
    $("#pagination2").val($("#pagin2").val());
    $("#Form3").submit();
}

function ShowAddLeadInterest(LeadId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/LeadInterests/Index/",
        data: { LeadId: LeadId },
        success: function (data) {
            HideSpinner();
            $('#LeadInterest .modal-content').html(data);
            $('#LeadInterest').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowCreateLeadInterest(id) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/LeadInterests/ShowCreate/",
        success: function (data) {
            HideSpinner();
            $('#LeadInterest').modal("hide");
            $('#CreateLeadInterest .modal-content').html(data);
            $('#CreateLeadInterest').modal("show");
            $("#LeadId").val(id);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function CloseLeadInterest() {
    $('#CreateLeadInterest').modal("hide");
    ShowAddLeadInterest($("#LeadId").val())
}

function CreateLeadInterest() {
    $("#CompanyLeadInterest-Create").validate({
        errorElement: "span",
        rules: {
            ProgramId: "required",
            HowSeriousId: "required",
            Status: "required",
            NoteNote: "required",
        }
    })

    let obj = {
        LeadId: $("#LeadId").val(),
        ProgramId: $("#ProgramId").val(),
        HowSeriousId: $("#HowSeriousId").val(),
        Status: $("#Status").val(),
        Note: $("#Note").val(),
    }

    if ($("#CompanyLeadInterest-Create").valid()) {
        ShowSpinner();
        $.ajax({
            type: "POST",
            url: "/ControlPanel/LeadInterests/Create/",
            data: { LeadInterestViewModel: obj },
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                toastr.success(SuccesCreate);
                $('#CreateLeadInterest').modal("hide");
                HideSpinner();
                ShowAddLeadInterest($("#LeadId").val())
            },
            error: function (data) {
                HideSpinner();
                toastr.error(ErrorCreate);
            },
        });
    }
}

function ShowAdvisor(id, Advicer) {
    $("#LeadId").val(id);
    $("#Advicer").val("").trigger("chosen:updated");
    $("#TransferAdviser").modal('show');
    if (Advicer)
        $("#Advicer").val(Advicer).trigger("chosen:updated");
}

function SetAdvisor() {
    $("#SetAdvisor").validate({
        errorElement: "span",
        rules: {
            Advicer: "required",
        }
    })
    if ($("#SetAdvisor").valid()) {
        ShowSpinner();
        $.ajax({
            type: "POST",
            url: "/Sales/FollowUp/Transfer/",
            data: { id: $("#LeadId").val(), advicer: $("#Advicer").val() },
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                toastr.success(SuccesEdit);
                HideSpinner();
                $("#TransferAdviser").modal('hide');
            },
            error: function (data) {
                HideSpinner();
                toastr.error(ErrorEdit);
            },
        });
    }
}

$("#TransferAdviser").on('hidden.bs.modal', function () {
    $("#Advicer").val("").trigger("chosen:updated");
})

function ShowObserver(id) {
    $("#LeadId").val(id);
    $("#Observer").modal('show');
}

function SetObserver() {
    $("#SetObserver").validate({
        errorElement: "span",
        rules: {
            ReasonId: "required",
            CommunicationEmployeeIds:'required'
        }
    })
    var obj = {
        Id: $("#LeadId").val(),
        Ids:arr1,
        CommunicationEmployeeIds: $("#CommunicationEmployeeIds").val(),
        ReasonId: $("#ReasonId").val(),
        IsUrgent:$("#IsUrgent").is(":checked"),
        Note:$("#Note").val(),
    }

    if ($("#SetObserver").valid()) {
        if ($('#CommunicationEmployeeIds').val() != "") {
            ShowSpinner();
            $.ajax({
                type: "POST",
                url: "/Sales/FollowUp/Observer/",
                data: { transferViewModel: obj },
                headers: {
                    'RequestVerificationToken': Token
                },
                success: function (data) {
                    toastr.success(SuccesCreate);
                    $("#Observer").modal('hide');
                    HideSpinner();
                    Get()
                    SetAll();
                },
                error: function (data) {
                    HideSpinner();
                    toastr.error(ErrorCreate);
                },
            });
        } else
            $("#error2").show();
    }
}

function ChangeFullName() {
    $("#FullName").val($("#FirstName").val() + " " + $("#SecondName").val() + " " + $("#ThirdName").val() + " " + $("#LastName").val());
}

function ShowNote(val) {
    if ($(val).is(":checked"))
        $("#Medical_Note").show()
    else
        $("#Medical_Note").hide()
}

function ShowCompany(val) {
    if ($(val).is(":checked"))
        $("#Company_Form").show()
    else
        $("#Company_Form").hide()
}

function ChangeCoverageRate(val) {
    if (!$("#SubscriptionPrice").val()) {
        toastr.warning("Please Choose a Program First");
        $("#CoverageValue").val(0)
    }
    else
        $("#CoverageRate").val((($(val).val() / $("#SubscriptionPrice").val()) * 100).toFixed(2))
}

function ChangeCoverageValue(val) {
    if (!$("#SubscriptionPrice").val()) {
        toastr.warning("Please Choose a Program First");
        $("#CoverageRate").val(0)
    }
    else
        $("#CoverageValue").val(($("#SubscriptionPrice").val() * ($(val).val())/100))
}

function GetProgramData(id) {
    $.ajax({
        type: "GET",
        url: "/Training/Programs/GetProgram/",
        data: { id : $(id).val() },
        success: function (data) {
            $("#SubscriptionPrice").val(data.price)
            $("#InstallmentsNumber").val(data.installmentsNumber)
        }
    });
}

function ShowTransferStudent(id) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Sales/FollowUp/ShowTransferStudent/",
        data: { id },
        success: function (data) {
            HideSpinner();
            $('#TransferStudent .modal-content').html(data);
            $('#TransferStudent').modal("show");
            $('#SubscriptionDate').val(new Date().toDateInputValue());
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function TransferStudent() {
    $("#Transfer_Student").validate({
        errorElement: "span",
        showErrors: function (errorMap, errorList) {

            for (let x of errorList) {
                if (x.element.type == 'select-one') {
                    x.element = $(`#${x.element.id}_chosen`)[0];
                }
            }
            this.defaultShowErrors();
        },
        rules: {
            LanguageId: "required",
            FirstName: "required",
            LastName: "required",
            Email: "required",
            Mobile: {
                required: true,
                number: true
            },
            GenderId: "required",
            BranchId: "required",
            IdNumber: "required",
            BirthPlace: "required",
            Country: "required",
            Address: "required",
            Work: "required",
            WorkPlace: "required",
            ExtraMobile: {
                number: true
            },
            SubscriptionPrice: {
                required: true,
                number: true
            },
            InstallmentsNumber: {
                required: true,
                number: true
            },
            CompanyId: "required",
            PaymentWayId: "required",
            RightTimeId: "required",
            ProgramId: "required",
            RegimentId: "required",
            FirstInstallmentDate: "required",
            EducationalLevelId: "required",
        }
    })

    var obj = {
        Id: $("#LeadId").val(),
        ContactId: $("#ContactId").val(),
        FirstName: $("#FirstName").val(),
        SecondName: $("#SecondName").val(),
        ThirdName: $("#ThirdName").val(),
        LastName: $("#LastName").val(),
        FullName: $("#FullName").val(),
        Email: $("#Email").val(),
        Mobile: $("#Mobile").val(),
        GenderId: $("#GenderId").val(),
        BranchId: $("#BranchId").val(),
        IdNumber: $("#IdNumber").val(),
        BirthDate: $("#BirthDate").val(),
        BirthPlace: $("#BirthPlace").val(),
        Work: $("#Work").val(),
        EducationalLevelId: $("#EducationalLevelId").val(),
        Country: $("#Country").val(),
        Address: $("#Address").val(),
        WorkPlace: $("#WorkPlace").val(),
        ExtraMobile: $("#ExtraMobile").val(),
        ProgramId: $("#ProgramId").val(),
        RegimentId: $("#RegimentId").val(),
        SubscriptionPrice: $("#SubscriptionPrice").val(),
        SubscriptionDate: $("#SubscriptionDate").val(),
        FirstInstallmentDate: $("#FirstInstallmentDate").val(),
        InstallmentsNumber: $("#InstallmentsNumber").val(),
        CoverageValue: $("#CoverageValue").val(),
        CoverageRatio: $("#CoverageRate").val(),
        RightTimeId: $("#RightTimeId").val(),
        PaymentWayId: $("#PaymentWayId").val(),
        CompanyId: $("#CompanyId").val(),
        CollegeId: $("#CollegeId").val(),
        SpecialtyId: $("#SpecialtyId").val(),
        IsExternalStudy: $("#IsExternalStudy").is(":checked"),
        IsMedicalPast: $("#IsMedicalPast").is(":checked"),
        isFastSubscription: $("#isFastSubscription").is(":checked"),
        Note: $("#Note").val(),
        DiscountId: $("#DiscountId").val(),
    }
    

    if ($("#Transfer_Student").valid()) {
        ShowSpinner();
        $.ajax({
            type: "POST",
            url: "/Sales/FollowUp/TransferStudent/",
            data: { studentViewModel: obj },
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                toastr.success(SuccesCreate);
                $("#TransferStudent").modal('hide');
                HideSpinner();
                Get()
            },
            error: function (data) {
                HideSpinner();
                toastr.error(ErrorCreate);
            },
        });
    }
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

Date.prototype.toDateInputValue = (function () {
    var local = new Date(this);
    local.setMinutes(this.getMinutes() - this.getTimezoneOffset());
    return local.toJSON().slice(0, 10);
});

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

function CheckboxClicked(item, current,lead) {
    arr.map(function (x) {
        if (current == x)
            arr = arr.filter(e => e !== x)
    })
    if ($(item).is(':checked'))
        arr.push(current)

    SetDisable(arr.length)

    arr1.map(function (x) {
        if (lead == x)
            arr1 = arr1.filter(e => e !== x)
    })
    if ($(item).is(':checked'))
        arr1.push(lead)
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

function ShowSendSms() {
    $.ajax({
        type: "GET",
        url: "/Global/Sms/GetSms/",
        success: function (data) {
            $('#Sms .modal-content').html(data);
            $('#Sms').modal("show");
        }
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
