function Get() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/CallCenter/FollowUp/GetData",
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

function ShowEditTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/CallCenter/FollowUp/ShowTable/",
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

function GetData(data) {
    $("#main").html(data);
}

function GetData1(data) {
    $("#LeadNotesDiv").html(data);
}

function ChangePagenation1() {
    $("#pagination1").val($("#pagin1").val());
    $("#Form2").submit();
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

function ShowAddNotes(id) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Marketing/Lead/ShowAddNotes/",
        success: function (data) {
            HideSpinner();
            $('#LeadNotes').modal("hide");
            $('#AddLeadNotes .modal-content').html(data);
            $('#AddLeadNotes').modal("show");
            $("#AddNoteId").val(id);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
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


function ShowEdit(id) {
    $("#LeadId").val(id);
    $('#Edit').modal("show");
}

$("#Edit").on("hidden.bs.modal", function () {
    $("#EditLead")[0].reset();
});

function EditLead() {
    $("#EditLead").validate({
        errorElement: "span",
        rules: {
            CollegeExamId: "required",
            AcademicTestId: "required"
        }
    })

    let obj = {
        Id: $("#LeadId").val(),
        CollegeId: $("#CollegeExamId").val(),
        SpecialtyId: $("#AcademicTestId").val(),
        IsExternalStudy: $("#StudyOut").is(":checked"),
    }

    if ($("#EditLead").valid()) {
        ShowSpinner();
        $.ajax({
            type: "POST",
            url: "/CallCenter/FollowUp/Edit/",
            data: { leadViewModel: obj },
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                toastr.success(SuccesEdit);
                $('#Edit').modal("hide");
                HideSpinner();
            },
            error: function (data) {
                HideSpinner();
                toastr.error(ErrorEdit);
            },
        });
    }
}

function ShowFinish(id) {
    $("#FolloUpId").val(id);
    $("#Finish").modal("show");
}

function Finish() {
    ShowSpinner();
    $.ajax({
        type: "POST",
        url: "/CallCenter/FollowUp/Finish/",
        data: { id: $("#FolloUpId").val() },
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            toastr.success(SuccesFinish);
            $("#Finish").modal("hide");
            HideSpinner();
            Get();
        },
        error: function (data) {
            HideSpinner();
            toastr.error(ErrorFinish);
        },
    });
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

function ShowSendSms(id) {
    if (id > 0) {
        SetAll()
        arr.push(id)
    }
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
                SetAll();
            },
            error: function (data) {
                toastr.error(ErrorCreate);
            },
        });
    }
}

$("#Sms").on('hidden.bs.modal', function () {
    $("#Send_Sms").trigger("reset")
})