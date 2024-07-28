function Get() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Marketing/Lead/GetData",
        dataType: "html",
        success: function (data) {
            HideSpinner();
            $('#main').html(data);
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

Get()

function ShowAddLead() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Marketing/Lead/ShowCreate/",
        success: function (data) {
            HideSpinner();
            $('#CreateLead .modal-content').html(data);
            $('#CreateLead').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
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


function ShowDetailsLead(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Marketing/Lead/ShowDetails/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#DetailsLead .modal-content').html(data);
            $('#DetailsLead').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function ShowDeleteLead(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Marketing/Lead/ShowDelete/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#DeleteLead .modal-content').html(data);
            $('#DeleteLead').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
});
}

function ConfirmDelete(id) {
    ShowSpinner();
    $.post("/Marketing/Lead/DeleteLead/",
        { id: id },
        function (data) {
            if (data !== null) {
                $('#DeleteLead').modal("hide");
                HideSpinner();
                $(".modal-backdrop").remove();
                location.reload();
            } else {
                HideSpinner();
                iqwerty.toast.toast("Error while delete Lead", optionsError);
            }
        }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function onDdlChanged(langId) {
    ShowEditLead($('#Id').val(), langId.value);
}

function ShowEditLead(id, langId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Marketing/Lead/ShowEdit/",
        data: { id: id, languageId: langId },
        success: function (data) {
            HideSpinner();
            $('#EditLead .modal-content').html(data);
            $('#EditLead').modal("show");
        }

    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function AddLead() {
    $("#Lead-Create").validate({
        errorElement: "span",
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
            CommunicationChannelId: "required",
            BirthPlace: "required",
            Country: "required",
            Address: "required",
            WorkPlace: "required",
            ExtraMobile: {
                number: true
            },
            TypeId: "required",
            Title: "required",
            Description: "required",
            CampaignId: "required",
            AdvisorId: "required",
        }
    })

    let obj = {
        LanguageId: $("#LanguageId").val(),
        "ContactViewModel.LanguageId": $("#LanguageId").val(),
        "ContactViewModel.FirstName":$("#FirstName").val(),
        "ContactViewModel.SecondName": $("#SecondName").val(),
        "ContactViewModel.ThirdName": $("#ThirdName").val(),
        "ContactViewModel.LastName": $("#LastName").val(),
        "ContactViewModel.FullName": $("#FullName").val(),
        "ContactViewModel.Email": $("#Email").val(),
        "ContactViewModel.Mobile": $("#Mobile").val(),
        "ContactViewModel.GenderId": $("#GenderId").val(),
        "ContactViewModel.BranchId": parseInt($("#BranchId").val()),
        "ContactViewModel.IdNumber": $("#IdNumber").val(),
        IdNumber: $("#IdNumber").val(),
        BirthDate: $("#BirthDate").val(),
        BirthPlace: $("#BirthPlace").val(),
        Work: $("#Work").val(),
        EducationalLevelId: $("#EducationalLevelId").val(),
        Country: $("#Country").val(),
        Address: $("#Address").val(),
        WorkPlace: $("#WorkPlace").val(),
        CommunicationChannelId: $("#CommunicationChannelId").val(),
        FirstSubChannelId: $("#FirstSubChannelId").val(),
        SecondSubChannelId: $("#SecondSubChannelId").val(),
        CampaignId: $("#CampaignId").val(),
        TypeId: $("#TypeId").val(),
        Title: $("#Title").val(),
        Description: $("#Description").val(),
        ExtraMobile: $("#ExtraMobile").val(),
        TrainingConsultantId: $("#AdvisorId").val()
    }
    if ($("#Lead-Create").valid()) {
        ShowSpinner();
        $.ajax({
            type: "POST",
            url: "/Marketing/Lead/CreateLead",
            data: { leadViewModel: obj },
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                $('#CreateLead').modal("hide");
                HideSpinner();
                toastr.success(SuccesCreate);
                Get();
            },
            error: function (data) {
                HideSpinner();
                toastr.error(ErrorCreate);
            },
        });
    }
}

function EditLead() {
    $("#Lead-Edit").validate({
        errorElement: "span",
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
            CommunicationChannelId: "required",
            IdNumber: "required",
            BirthPlace: "required",
            Country: "required",
            Address: "required",
            WorkPlace: "required",
            ExtraMobile: {
                number: true
            },
            TypeId: "required",
            Title: "required",
            Description: "required",
            AdvisorId: "required",
        }
    })

    let obj = {
        Id: $("#Id").val(),
        LanguageId: $("#LanguageId").val(),
        "ContactViewModel.LanguageId": $("#LanguageId").val(),
        "ContactViewModel.FirstName": $("#FirstName").val(),
        "ContactViewModel.SecondName": $("#SecondName").val(),
        "ContactViewModel.ThirdName": $("#ThirdName").val(),
        "ContactViewModel.LastName": $("#LastName").val(),
        "ContactViewModel.FullName": $("#FullName").val(),
        "ContactViewModel.Email": $("#Email").val(),
        "ContactViewModel.Mobile": $("#Mobile").val(),
        "ContactViewModel.GenderId": $("#GenderId").val(),
        "ContactViewModel.BranchId": parseInt($("#BranchId").val()),
        "ContactViewModel.IdNumber": $("#IdNumber").val(),
        IdNumber: $("#IdNumber").val(),
        BirthDate: $("#BirthDate").val(),
        BirthPlace: $("#BirthPlace").val(),
        Work: $("#Work").val(),
        EducationalLevelId: $("#EducationalLevelId").val(),
        Country: $("#Country").val(),
        Address: $("#Address").val(),
        WorkPlace: $("#WorkPlace").val(),
        CommunicationChannelId: $("#CommunicationChannelId").val(),
        FirstSubChannelId: $("#FirstSubChannelId").val(),
        SecondSubChannelId: $("#SecondSubChannelId").val(),
        CampaignId: $("#CampaignId").val(),
        TypeId: $("#TypeId").val(),
        Title: $("#Title").val(),
        Description: $("#Description").val(),
        ExtraMobile: $("#ExtraMobile").val(),
        TrainingConsultantId: $("#AdvisorId").val()
    }
    if ($("#Lead-Edit").valid()) {
        ShowSpinner();
        $.ajax({
            type: "POST",
            url: "/Marketing/Lead/EditLead",
            data: { leadViewModel: obj },
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                $('#EditLead').modal("hide");
                HideSpinner();
                toastr.success(SuccesEdit);
                Get();
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


function ChangeFullName() {
    $("#FullName").val($("#FirstName").val() + " " + $("#SecondName").val() + " " + $("#ThirdName").val() + " " + $("#LastName").val());
}

function Filter() {
    $("#filterStatus").val($("#FilterStatus").val());
    $("#filterAdvicer").val($("#FilterAdvicer").val());
    $("#Form1").submit();
}

function FetchData() {
    $("#FetchDataForm").validate({
        errorElement: "span",
        rules: {
            FetchTitle: "required",
            FetchDescription: "required",
            Advicer: "required",
            CommunicationChannelId: "required",
        }
    })

    let obj = {
        FetchTitle: $("#FetchTitle").val(),
        FetchDescription: $("#FetchDescription").val(),
        Advicer: $("#Advicer").val(),
        FilterAdvicer: $("#filterAdvicer").val(),
        CommunicationChannelId: $("#CommunicationChannelId").val(),
        IsImportent: $("#IsImportent").is(":checked"),
    }

    if ($("#FetchDataForm").valid()) {
        $.ajax({
            type: "POST",
            url: "/Marketing/Lead/FetchData",
            data: { fetchFilterViewModel: obj },
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                $('#FetchData').modal("hide");
                toastr.success(SuccesEdit);
                Get();
            },
            error: function (data) {
                toastr.error(ErrorEdit);
            },
        });
    }
}