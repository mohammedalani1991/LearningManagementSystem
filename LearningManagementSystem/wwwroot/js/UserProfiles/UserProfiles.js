function ReactivateAccount(id) {
    $.ajax({
        type: "GET",
        url: "/ControlPanel/UserProfiles/ReactivateAccount/",
        data: { id },
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            toastr.success(ReactivateSucceeded);
            $('#EditContact').modal("hide");
        },
        error: function () {
            toastr.error(ReactivateFailed);
        }
    });
}


function CloseCreateContact() {
    $('#CreateContact').modal("hide");
}

function CloseAddLeadInterest() {
    $('#CreateLeadInterest').modal("hide");
}

function onDdlChanged(id, contactTypeId) {
    ShowEditContact(id, $("#ContactViewModel_LanguageId").val(), contactTypeId)
}


function ShowEditTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/UserProfiles/ShowTable/",
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

function CreateLeadInterest() {
    var CompanyLeadInterestCreateFrm = $('#CompanyLeadInterest-Create');
    if (CompanyLeadInterestCreateFrm.valid()) {
        $.ajax({
            type: CompanyLeadInterestCreateFrm.attr('method'),
            url: CompanyLeadInterestCreateFrm.attr('action'),
            data: CompanyLeadInterestCreateFrm.serialize(),
            success: function (data) {
                toastr.success(addMassege);
                $('#CreateLeadInterest').modal("hide");
                $(".modal-backdrop").remove();

            },
            error: function (data) {
                toastr.error(addMassegeError);
            },
        });
    }
}


function ShowCreateContact(contactTypeId, companyId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/UserProfiles/ShowCreate/",
        data: { contactTypeId: contactTypeId, companyId: companyId },
        success: function (data) {
            HideSpinner();
            $('#CreateContact .modal-content').html(data);
            $('#CreateContact').modal("show");
            $('#StudentViewModel_SubscriptionDate').val(new Date().toDateInputValue());
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

$("#CreateContact").on('hidden.bs.modal', function () {
    $('#CreateContact .modal-content').empty();
})

function CreateContact() {
    var ContactCreateFrm = $('#ContactForm-Create');
    $("#TrainerViewModel_Description").val(tinymce.get("TrainerViewModel_Description").getContent())
    $("#ContactForm-Create").validate({
        errorElement: "span",
        rules: {
            'ContactViewModel.Mobile': {
                number: true,
            },
            'ContactViewModel.IdNumber': {
                number: true,
            },
            'UserProfileViewModel.Password': {
                custompassword: true,
            },
            'UserProfileViewModel.ConfirmPassword': {
                equalTo: "#UserProfileViewModel_Password"
            },
        }
    });
    if (ContactCreateFrm.valid()) {
        const phoneNumber = libphonenumber.parsePhoneNumber($("#ContactViewModel_PhoneNumberCode").val() + $("#ContactViewModel_Mobile").val());
        if (phoneNumber.isValid()) {
            ShowSpinner();
            $.ajax({
                type: "GET",
                url: "/ControlPanel/UserProfiles/CheckIfUserExist/",
                data: { email: $("#ContactViewModel_Email").val() },
                headers: {
                    'RequestVerificationToken': Token
                },
                success: function (data) {
                    if (data.exist) {
                        toastr.warning(EmailMassegeError);
                        HideSpinner();
                    }
                    else
                        $.ajax({
                            type: ContactCreateFrm.attr('method'),
                            url: ContactCreateFrm.attr('action'),
                            data: ContactCreateFrm.serialize(),
                            headers: {
                                'RequestVerificationToken': Token
                            },
                            success: function (data) {
                                if (data.success) {
                                    if ($('#StudentContactTypeId').val() == 3)
                                        GetStudents();
                                    toastr.success(editMassege);
                                    if (data.message)
                                        toastr.warning(data.message);
                                    $('#CreateContact').modal("hide");
                                    setTimeout(function () {
                                        location.reload();
                                    }, 3000)
                                }
                                else if (!data.success && data.message)
                                    toastr.error(data.message);
                                else
                                    toastr.error(editMassegeError);
                                HideSpinner();
                            },
                            error: function (data) {
                                toastr.error(addMassegeError);
                                HideSpinner();
                            },
                        });
                },
                error: function (data) {
                    HideSpinner();
                },
            })
        }
        else
            toastr.warning(phoneError);
    }
}

function EditContact() {
    var ContactEditFrm = $('#ContactForm-Edit');
    $("#TrainerViewModel_Description").val(tinymce.get("TrainerViewModel_Description").getContent())

    $("#ContactForm-Edit").validate({
        errorElement: "span",
        rules: {
            'ContactViewModel.Mobile': {
                number: true
            },
            'ContactViewModel.IdNumber': {
                number: true,
            },
        }
    });
    if (ContactEditFrm.valid()) {
        const phoneNumber = libphonenumber.parsePhoneNumber($("#ContactViewModel_PhoneNumberCode").val() + $("#ContactViewModel_Mobile").val());
        if (phoneNumber.isValid()) {
            ShowSpinner();
            $.ajax({
                type: "GET",
                url: "/ControlPanel/UserProfiles/CheckIfUserExist/",
                data: { email: $("#ContactViewModel_Email").val(), id: $("#ContactViewModel_Id").val() },
                headers: {
                    'RequestVerificationToken': Token
                },
                success: function (data) {
                    if (data.exist) {
                        toastr.warning(EmailMassegeError);
                        HideSpinner();
                    }
                    else
                        $.ajax({
                            type: ContactEditFrm.attr('method'),
                            url: ContactEditFrm.attr('action'),
                            data: ContactEditFrm.serialize(),
                            headers: {
                                'RequestVerificationToken': Token
                            },
                            success: function (data) {
                                HideSpinner();
                                if (data.success) {
                                    if ($('#StudentContactTypeId').val() == 3)
                                        GetStudents();
                                    toastr.success(editMassege);
                                    if (data.message)
                                        toastr.warning(data.message);
                                    $('#EditContact').modal("hide");
                                    setTimeout(function () {
                                        location.reload();
                                    }, 3000)
                                }
                                else if (!data.success && data.message)
                                    toastr.error(data.message);
                                else
                                    toastr.error(editMassegeError);
                                HideSpinner();
                            },
                            error: function (data) {
                                HideSpinner();
                                toastr.error(editMassegeError);
                            },
                        });
                },
                error: function (data) {
                    HideSpinner();
                },
            })
        }
        else
            toastr.warning(phoneError);
    }
}

function ShowEditContact(id, langId, contactTypeId, companyId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/UserProfiles/ShowEdit/",
        data: { id: id, languageId: langId, contactTypeId: contactTypeId, companyId: companyId },
        dataType: 'html',
        success: function (data) {
            HideSpinner();
            $('#CreateContactDiv').empty();
            $('#EditContact .modal-content').html(data);
            $('#EditContact').modal("show");
            model?.userProfileViewModel?.roles?.map((x, i) => {
                $(".select_Roles option[value='" + x.roleId + "']").prop("selected", true);
            })
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

$("#EditContact").on('hidden.bs.modal', function () {
    $('#EditContact .modal-content').empty();
})

function ShowDetailsContact(id, langId, contactTypeId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/UserProfiles/ShowDetails/",
        data: { id: id, languageId: langId, contactTypeId: contactTypeId },
        success: function (data) {
            HideSpinner();
            $('#DetailsContact .modal-content').html(data);
            $('#DetailsContact').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowDeleteContact(id, langId, contactTypeId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/UserProfiles/ShowDelete/",
        data: { id: id, languageId: langId, contactTypeId: contactTypeId },
        success: function (data) {
            HideSpinner();
            $('#DeleteContact .modal-content').html(data);
            $('#DeleteContact').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function Delete(id) {
    ShowSpinner();
    $.ajax({
        type: "POST",
        url: "/ControlPanel/UserProfiles/Delete/",
        data: { id: id },
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            if (data.success) {
                toastr.success(deleteMassege);
                $('#DeleteContact').modal("hide");
                setTimeout(function () {
                    location.reload();
                }, 2000)
            }
            HideSpinner();
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
        toastr.error(deleteMassegeError);
    });
}

function ShowDeleteContactNotVerifyEmail(id, langId, contactTypeId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/UserProfiles/ShowDeleteNotVerifyEmail/",
        data: { id: id, languageId: langId, contactTypeId: contactTypeId },
        success: function (data) {
            HideSpinner();
            $('#DeleteContactNotVerifyEmail .modal-content').html(data);
            $('#DeleteContactNotVerifyEmail').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowVerifyEmail(id) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/ControlPanel/UserProfiles/ShowVerifyEmail/",
        data: { id: id },
        success: function (data) {
            HideSpinner();
            $('#VerifyEmail .modal-content').html(data);
            $('#VerifyEmail').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function CloseEditContact() {
    $('#EditContact').modal("hide");
}

function CloseDetailsContact() {
    $('#DetailsContact').modal("hide");
}

function CloseDeleteContact() {
    $('#DeleteContact').modal("hide");
}

function ConfirmDelete(id) {
    ShowSpinner();
    $.post("/ControlPanel/UserProfiles/DeleteContract/",
        { id: id },
        function (data) {
            if (data !== null) {
                $('#DeleteContact').modal("hide");
                HideSpinner();
                $(".modal-backdrop").remove();
                toastr.success(deleteMassege);
                setTimeout(
                    function () {
                        if ($('#StudentContactTypeId').val() == 3) {
                            GetStudents();
                            return;
                        }

                        location.reload();
                    }, 1000);
            } else {
                HideSpinner();
                toastr.error(deleteMassegeError);
            }
        }
    );
}

function ConfirmDeleteNotVerifyEmail(id) {
    ShowSpinner();
    $.post("/ControlPanel/UserProfiles/DeleteContractNotVerifyEmail/",
        { id: id },
        function (data) {
            if (data !== null) {
                $('#DeleteContactNotVerifyEmail').modal("hide");
                HideSpinner();
                $(".modal-backdrop").remove();
                toastr.success(deleteMassege);
                setTimeout(
                    function () {
                        if ($('#StudentContactTypeId').val() == 3) {
                            GetStudents();
                            return;
                        }

                        location.reload();
                    }, 1000);
            } else {
                HideSpinner();
                toastr.error(deleteMassegeError);
            }
        }
    );
}

function ConfirmVerifyEmail(id) {
    ShowSpinner();
    $.post("/ControlPanel/UserProfiles/ConfirmVerifyEmail/",
        { id: id },
        function (data) {
            if (data !== null) {
                $('#VerifyEmail').modal("hide");
                HideSpinner();
                $(".modal-backdrop").remove();
                toastr.success(editMassege);
                setTimeout(
                    function () {
                        location.reload();
                    }, 1000);
            } else {
                HideSpinner();
                toastr.error(editMassegeError);
            }
        }
    );
}

function ShowAssignStudent(companyId) {
    $.ajax({
        type: "GET",
        url: "/Companies/Students/ShowAssignStudent/",
        data: { companyId: companyId },
        success: function (data) {
            $('#AssignStudent .modal-content').html(data);
            $('#AssignStudent').modal("show");
        }
    });
}

function CloseAssignStudent() {
    $('#AssignStudent').modal("hide");
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
        $.ajax({
            type: "POST",
            url: "/Marketing/Lead/CreateLeadNote",
            data: { leadViewModel: obj },
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                toastr.success(addMassege);
                $('#AddLeadNotes').modal("hide");
                ShowNotes($("#AddNoteId").val())
            },
            error: function (data) {
                toastr.error(addMassegeError);
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
        $.ajax({
            type: "POST",
            url: "/ControlPanel/LeadInterests/Create/",
            data: { LeadInterestViewModel: obj },
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                toastr.success(addMassege);
                $('#CreateLeadInterest').modal("hide");
                ShowAddLeadInterest($("#LeadId").val())
            },
            error: function (data) {
                toastr.error(addMassegeError);
            },
        });
    }
}

function GetProgramData(id) {
    $.ajax({
        type: "GET",
        url: "/Training/Programs/GetProgram/",
        data: { id: $(id).val() },
        success: function (data) {
            $("#StudentViewModel_SubscriptionPrice").val(data.price)
            $("#StudentViewModel_InstallmentsNumber").val(data.installmentsNumber)
        }
    });
}

function ChangeCompany(val) {
    if ($(val).val() != 0)
        $(".Coverage").show()
    else {
        $(".Coverage").hide()
        $("#StudentViewModel_CoverageRatio").val(null)
        $("#StudentViewModel_CoverageValue").val(null)
    }
}

function ChangeCoverageRate(val) {
    if (!$("#StudentViewModel_SubscriptionPrice").val()) {
        toastr.warning("Please Choose a Program First");
        $("#StudentViewModel_CoverageValue").val(0)
    }
    else
        $("#StudentViewModel_CoverageRatio").val((($(val).val() / $("#StudentViewModel_SubscriptionPrice").val()) * 100).toFixed(2))
}

function ChangeCoverageValue(val) {
    if (!$("#StudentViewModel_SubscriptionPrice").val()) {
        toastr.warning("Please Choose a Program First");
        $("#StudentViewModel_CoverageRatio").val(0)
    }
    else
        $("#StudentViewModel_CoverageValue").val(($("#StudentViewModel_SubscriptionPrice").val() * ($(val).val()) / 100))
}

Date.prototype.toDateInputValue = (function () {
    var local = new Date(this);
    local.setMinutes(this.getMinutes() - this.getTimezoneOffset());
    return local.toJSON().slice(0, 10);
});
