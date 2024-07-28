function Get() {
    $.ajax({
        type: "GET",
        url: "/Companies/CompanyNotes/GetData",
        dataType: 'html',
        success: function (data) {
            $('#main').html(data);
        }
    });
}

Get();

function ShowAddCompanyNote(companyId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/CompanyNotes/ShowCreate/",
        data: { id: companyId },
        success: function (data) {
            HideSpinner();
            $('#EditCompanyNotesDiv').empty();
            $('#CreateCompanyNotes .modal-content').html(data);
            $('#CreateCompanyNotes').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowNoteTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/CompanyNotes/ShowTable/",
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

function CreateNote() {
    var CompanyNotesFrm = $('#CompanyNote-Create');
    $('#Description').val(tinymce.get("Description").getContent())
    if (CompanyNotesFrm.valid()) {
        if ($('#Description').val()) {
            ShowSpinner();
            $.ajax({
                type: CompanyNotesFrm.attr('method'),
                url: CompanyNotesFrm.attr('action'),
                data: CompanyNotesFrm.serialize(),
                headers: {
                    'RequestVerificationToken': Token
                },
                success: function (data) {
                    toastr.success(addMassege);
                    $('#CreateCompanyNotes').modal("hide");
                    HideSpinner();
                    if (typeof GetNotes === "function")
                        GetNotes()
                    else
                        Get()
                },
                error: function (data) {
                    HideSpinner();
                    toastr.error(addMassegeError);
                },
            })
        }
        else {
            $("#Description-error").show();
            $("#Description-error").html("This field is required.");
        }
    }
}

function ShowEditCompanyNote(id, langId, companyId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/CompanyNotes/ShowEdit/",
        data: { id: id, languageId: langId, companyId: companyId },
        success: function (data) {
            HideSpinner();
            $('#CreateCompanyNotesDiv').empty();
            $('#EditCompanyNotes .modal-content').html(data);
            $('#EditCompanyNotes').modal("show");
        }

    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowDetailsCompanyNote(id, langId, companyId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/CompanyNotes/ShowDetails/",
        data: { id: id, languageId: langId, companyId: companyId },
        success: function (data) {
            HideSpinner();
            $('#DetailsCompanyNotes .modal-content').html(data);
            $('#DetailsCompanyNotes').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function ShowDeleteCompanyNote(id, langId, companyId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/CompanyNotes/ShowDelete/",
        data: { id: id, languageId: langId, companyId: companyId },
        success: function (data) {
            HideSpinner();
            $('#DeleteCompanyNotes .modal-content').html(data);
            $('#DeleteCompanyNotes').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function EditNote() {
    var CompanyNotesEditFrm = $('#CompanyNote-Edit');
    $('#Description').val(tinymce.get("Description").getContent())
    if (CompanyNotesEditFrm.valid()) {
        if ($('#Description').val()) {
            ShowSpinner();
            $.ajax({
                type: CompanyNotesEditFrm.attr('method'),
                url: CompanyNotesEditFrm.attr('action'),
                data: CompanyNotesEditFrm.serialize(),
                headers: {
                    'RequestVerificationToken': Token
                },
                success: function (data) {
                    toastr.success(editMassege);
                    $('#EditCompanyNotes').modal("hide");
                    HideSpinner();
                    if (typeof GetNotes === "function")
                        GetNotes()
                    else
                        Get()
                },

                error: function (data) {
                    HideSpinner();
                    toastr.error(editMassegeError);
                },
            });
        }
        else {
            $("#Description-error").show();
            $("#Description-error").html("This field is required.");
        }
    }
}

function ConfirmNoteDelete(id) {
    $.post("/Companies/CompanyNotes/DeleteCompanyNote/",
        { id: id },
        function (data) {
            if (data !== null) {
                toastr.success(deleteMassege);
                $('#DeleteCompanyNotes').modal("hide");
                if (typeof GetNotes === "function")
                    GetNotes();
                else
                    Get()
            }
            else {
                toastr.error(deleteMassegeError);
            }
        });
}

function onDdlChanged1(langId, companyId) {
    ShowEditCompanyNote($('#CompanyNoteId').val(), langId.value, companyId);
}


function GetData1(data) {
    if (typeof GetNotes === "undefined")
        $('#main').html(data);
    else
        $('#GetData').html(data);
}

