function Get() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/CompanyBranches/GetData",
        dataType: 'html',
        success: function (data) {
            $('#main').html(data);
            HideSpinner();
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

Get();

function ShowBranchTable() {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/CompanyBranches/ShowTable/",
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

function ShowAddCompanyBranch(companyId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/CompanyBranches/ShowCreate/",
        data: { id: companyId },
        success: function (data) {
            HideSpinner();
            $('#CreateCompanyBranchs .modal-content').html(data);
            $('#CreateCompanyBranchs').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function CreateBranche() {
    var companyBranchFrm = $('#CompanyBranch-Create');
    if (companyBranchFrm.valid()) {
        ShowSpinner();
        $.ajax({
            type: companyBranchFrm.attr('method'),
            url: companyBranchFrm.attr('action'),
            data: companyBranchFrm.serialize(),
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                HideSpinner();
                if (data.success) {
                    toastr.success(addMassege);
                    $('#CreateCompanyBranchs').modal("hide");
                    if (typeof GetBranches === "function")
                        GetBranches();
                    else
                        Get()
                }
                else
                    toastr.error(data.error);
            },
            error: function (data) {
                HideSpinner();
                toastr.error(addMassegeError);
            },
        });
    }
}


function ShowEditCompanyBranch(id, langId, companyId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/CompanyBranches/ShowEdit/",
        data: { id: id, languageId: langId, companyId: companyId },
        success: function (data) {
            HideSpinner();
            $('#EditCompanyBranchs .modal-content').html(data);
            $('#EditCompanyBranchs').modal("show");
        }

    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function ShowDetailsCompanyBranch(id, langId, companyId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/CompanyBranches/ShowDetails/",
        data: { id: id, languageId: langId, companyId: companyId },
        success: function (data) {
            HideSpinner();
            $('#DetailsCompanyBranchs .modal-content').html(data);
            $('#DetailsCompanyBranchs').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}


function ShowDeleteCompanyBranch(id, langId, companyId) {
    ShowSpinner();
    $.ajax({
        type: "GET",
        url: "/Companies/CompanyBranches/ShowDelete/",
        data: { id: id, languageId: langId, companyId: companyId },
        success: function (data) {
            HideSpinner();
            $('#DeleteCompanyBranchs .modal-content').html(data);
            $('#DeleteCompanyBranchs').modal("show");
        }
    }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function EditBranche() {
    var companyBranchEditFrm = $('#CompanyBranch-Edit');
    if (companyBranchEditFrm.valid()) {
        ShowSpinner();
        $.ajax({
            type: companyBranchEditFrm.attr('method'),
            url: companyBranchEditFrm.attr('action'),
            data: companyBranchEditFrm.serialize(),
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                HideSpinner();
                if (data.success) {
                    toastr.success(editMassege);
                    $('#EditCompanyBranchs').modal("hide");
                    if (typeof GetBranches === "function")
                        GetBranches();
                    else
                        Get()
                }
                else
                    toastr.error(data.error);

            }, error: function (data) {
                HideSpinner();
                toastr.error(editMassegeError);
            },
        });
    }
}

function ConfirmDeleteBranch(id) {
    ShowSpinner();
    $.post("/Companies/CompanyBranches/DeleteCompanyBranch/",
        { id: id },
        function (data) {
            if (data !== null) {
                toastr.success(deleteMassege);
                $('#DeleteCompanyBranchs').modal("hide");
                HideSpinner();
                if (typeof GetBranches === "function")
                    GetBranches();
                else
                    Get()
            } else {
                HideSpinner();
                toastr.error(deleteMassegeError);
            }
        }).fail(function (jqXHR, textStatus, error) {
        HideSpinner();
    });
}

function onDdlChanged2(langId, companyId) {

    ShowEditCompanyBranch($('#CompanyBranchId').val(), langId.value, companyId);
}

function GetData1(data) {
    if (typeof GetNotes === "undefined")
        $('#main').html(data);
    else
        $('#GetData').html(data);
}